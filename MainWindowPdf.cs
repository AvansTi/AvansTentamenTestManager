using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansTentamenManager
{
    partial class MainWindow
    {

        private void BtnGeneratePdfs_Click(object sender, EventArgs e)
        {
            var razorconfig = new TemplateServiceConfiguration();
            razorconfig.EncodedStringFactory = new RawStringFactory(); // Raw string encoding.
            razorconfig.Language = Language.CSharp;


            using (var service = RazorEngineService.Create(razorconfig))
            {
                string template = File.ReadAllText("Templates/v1/template.tex");
                service.Compile(template, "templateKey", typeof(ResultModel));

                excelFile = new XSSFWorkbook("c:\\users\\johan\\desktop\\Resultaat.xlsx");
                for (int index = 1; index <= excelFile.GetSheet(txtOverviewTabName.Text).LastRowNum; index++)
                {
                    var overviewRow = excelFile.GetSheet(txtOverviewTabName.Text).GetRow(index);
                    int studentId = (int)overviewRow.Cells[0].NumericCellValue;
                    if (studentId == 0)
                        continue;
                    if (File.Exists("d:\\tentamen\\pdf\\" + studentId + ".pdf"))
                        continue;

                    var overrideSheet = excelFile.GetSheet(txtOverrideTabName.Text);
                    IRow overrideRow = null;
                    for (int i = 3; i <= overrideSheet.LastRowNum; i++)
                        if ((int)overrideSheet.GetRow(i).Cells[0].NumericCellValue == studentId)
                            overrideRow = overrideSheet.GetRow(i);

                    var testSheet = excelFile.GetSheet(txtResultTabName.Text);
                    IRow testRow = null;
                    for (int i = 1; i <= testSheet.LastRowNum; i++)
                        if ((int)testSheet.GetRow(i).Cells[0].NumericCellValue == studentId)
                            testRow = testSheet.GetRow(i);

                    var theorySheet = excelFile.GetSheet(txtTheoryTabName.Text);
                    IRow theoryRow = null;
                    for (int i = 1; i <= theorySheet.LastRowNum; i++)
                        if (theorySheet.GetRow(i) != null && (int)theorySheet.GetRow(i).Cells[0].NumericCellValue == studentId)
                            theoryRow = theorySheet.GetRow(i);


                    ResultModel model = new ResultModel()
                    {
                        student = new ResultModel.Student()
                        {
                            name = overviewRow.GetCell(1).StringCellValue + " " + overviewRow.GetCell(2).StringCellValue,
                            number = studentId
                        },
                    };
                    try { model.TotalPoints = (int)overviewRow.GetCell(6).NumericCellValue; } catch (Exception) { }
                    try { model.Grade = (decimal)overviewRow.GetCell(7).NumericCellValue; } catch (Exception) { }
                    try { model.ManualCorrector = overviewRow.GetCell(8).StringCellValue; } catch (Exception) { }





                    for (int i = 5; i < 5 + config.mcCount; i++)
                    {
                        model.mc.Add(new ResultModel.McQuestion()
                        {

                        });
                    }
                    for (int i = 4 + config.mcCount + 1; i < 4 + config.mcCount + 1 + config.openCount * 2; i += 2)
                    {
                        if (theoryRow != null)
                            model.open.Add(new ResultModel.OpenQuestion()
                            {
                                question = theorySheet.GetRow(0).GetCell(i).StringCellValue,
                                score = (int)theoryRow?.GetCell(i)?.NumericCellValue,
                                reason = theoryRow?.GetCell(i + 1)?.StringCellValue
                            });
                        else
                            model.open.Add(new ResultModel.OpenQuestion()
                            {
                                question = theorySheet.GetRow(0).GetCell(i).StringCellValue,
                                score = 0,
                                reason = "Geen antwoord"
                            });
                    }




                    var exams = manager.Exams;
                    List<string> exercises = GetExercises(exams);
                    for (int i = 0; i < exercises.Count; i++)
                    {
                        if (testRow == null)
                            continue;
                        var q = new ResultModel.Question()
                        {
                            question = escapeLatex(exercises[i]),
                            testErrors = escapeLatex(testRow?.GetCell(1 + 2 * i + 1)?.StringCellValue),
                            testScore = (int)overrideRow?.GetCell(4 + 3 * i + 0).NumericCellValue
                        };

                        if (q.question.ToLower().EndsWith("teststudentnummer"))
                            q.question = q.question.Substring(0, q.question.Length - 33); // _ is escaped
                        if (q.question.ToLower().StartsWith("opgave"))
                            q.question = q.question.Substring(6);


                        if (overrideRow.GetCell(4 + 3 * i + 1) != null && overrideRow.GetCell(4 + 3 * i + 1).CellType == CellType.Numeric)
                            q.manualScore = overrideRow.GetCell(4 + 3 * i + 1).NumericCellValue + "";

                        if (overrideRow.GetCell(4 + 3 * i + 2) != null && overrideRow.GetCell(4 + 3 * i + 2).CellType == CellType.String)
                            q.manualReason = escapeLatex(overrideRow.GetCell(4 + 3 * i + 2).StringCellValue);

                        model.questions.Add(q);
                    }





                    var result = service.Run("templateKey", typeof(ResultModel), model);

                    File.WriteAllText("d:\\tentamen\\pdf\\tex\\" + model.student.number + ".tex", result);
                    if (File.Exists("d:\\tentamen\\pdf\\tex\\" + model.student.number + ".pdf"))
                        File.Delete("d:\\tentamen\\pdf\\tex\\" + model.student.number + ".pdf");

                    Process process = new Process();
                    // Configure the process using the StartInfo properties.
                    process.StartInfo.FileName = "pdflatex";
                    process.StartInfo.WorkingDirectory = "d:\\tentamen\\pdf\\tex\\";
                    process.StartInfo.Arguments = "-quiet " + model.student.number + ".tex";
                    process.Start();
                    process.WaitForExit();// Waits here for the process to exit.

                    if (File.Exists("d:\\tentamen\\pdf\\" + model.student.number + ".pdf"))
                        File.Delete("d:\\tentamen\\pdf\\" + model.student.number + ".pdf");
                    File.Move("d:\\tentamen\\pdf\\tex\\" + model.student.number + ".pdf", "d:\\tentamen\\pdf\\" + model.student.number + ".pdf");
                }
            }

        }

        private string escapeLatex(string text)
        {
            if (text == null)
                return "";
            text = text.Trim();
            text = text.Replace("\n", "\\newline");
            text = text.Replace("&", "\\&");
            text = text.Replace("#", "\\#");
            text = text.Replace("%", "\\%");
            text = text.Replace("_", "\\textunderscore ");
            text = text.Replace("<", "\\guillemotleft ");
            text = text.Replace(">", "\\guillemotright ");

            return text;
        }

    }
}
