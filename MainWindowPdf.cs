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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvansTentamenManager
{
    partial class MainWindow
    {

        private void BtnGeneratePdfs_Click(object sender, EventArgs e)
        {
            var razorconfig = new TemplateServiceConfiguration();
            razorconfig.EncodedStringFactory = new RawStringFactory(); // Raw string encoding.
            razorconfig.Language = Language.CSharp;

            string texpath = Path.Combine(config.tempPath, "tex");
            while (Directory.Exists(texpath))
            {
                try
                {
                    Directory.Delete(texpath, true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deleting temp path " + texpath);
                    Thread.Sleep(1000);
                }
                Thread.Sleep(100);
            }
            Directory.CreateDirectory(texpath);

            CopyFiles("Templates/v1", texpath, false);





            using (var service = RazorEngineService.Create(razorconfig))
            {
                string template = File.ReadAllText("Templates/v1/template.tex");
                service.Compile(template, "templateKey", typeof(ResultModel));

                using (FileStream file = new FileStream(config.excelFileName, FileMode.Open, FileAccess.Read))
                {
                    excelFile = new XSSFWorkbook(file);
                }

                for (int index = 1; index <= excelFile.GetSheet(txtOverviewTabName.Text).LastRowNum; index++)
                {
                    var overviewRow = excelFile.GetSheet(txtOverviewTabName.Text).GetRow(index);
                    if (overviewRow == null || overviewRow.GetCell(0) == null)
                        continue;
                    int studentId = (int)overviewRow.Cells[0].NumericCellValue;
                    if (studentId == 0)
                        continue;
                 //   if (File.Exists(Path.Combine(config.outPath, studentId + ".pdf")))
                 //       continue;

                    var overrideSheet = excelFile.GetSheet(txtOverrideTabName.Text);
                    IRow overrideRow = null;
                    for (int i = 1; i <= overrideSheet.LastRowNum; i++)
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
                        if (theorySheet.GetRow(i) != null && theorySheet.GetRow(i).GetCell(0) != null && (int)theorySheet.GetRow(i).GetCell(0).NumericCellValue == studentId)
                            theoryRow = theorySheet.GetRow(i);

                    if (theoryRow == null && overrideRow == null && testRow == null)
                        continue;


                    ResultModel model = new ResultModel()
                    {
                        student = new ResultModel.Student()
                        {
                            name = overviewRow.GetCell(1).StringCellValue + " " + overviewRow.GetCell(2).StringCellValue,
                            number = studentId
                        },
                    };
                    model.subjectCode = config.subjectCode;
                    model.subjectName = config.subjectName;

                    try { model.TotalPoints = (int)overviewRow.GetCell(6).NumericCellValue; } catch (Exception) { }
                    try { model.Grade = (decimal)overviewRow.GetCell(7).NumericCellValue; } catch (Exception) { }
                    try { model.ManualCorrector = overviewRow.GetCell(8).StringCellValue; } catch (Exception) { }




                    model.mcTotalPoints = 0;
                    model.mcPoints = 0;
                    for (int i = 5; i < 5 + config.mcCount; i++)
                    {
                        model.mc.Add(new ResultModel.McQuestion()
                        {
                            question = theorySheet.GetRow(0).GetCell(i).StringCellValue,
                            answer = theoryRow.GetCell(i).StringCellValue,
                            score = theoryRow.GetCell(i).StringCellValue == theorySheet.GetRow(1).GetCell(i).StringCellValue ? (int)theorySheet.GetRow(2).GetCell(i).NumericCellValue : 0,
                            correctanswer = theorySheet.GetRow(1).GetCell(i).StringCellValue
                        });
                        model.mcTotalPoints += (int)theorySheet.GetRow(2).GetCell(i).NumericCellValue;
                        if(theoryRow.GetCell(i).StringCellValue == theorySheet.GetRow(1).GetCell(i).StringCellValue)
                            model.mcPoints += (int)theorySheet.GetRow(2).GetCell(i).NumericCellValue;
                    }
                    model.openTotalPoints = 0;
                    for (int i = 5 + config.mcCount + 1; i < 5 + config.mcCount + 1 + config.openCount * 2; i += 2)
                    {
                        model.openTotalPoints += (int)theorySheet.GetRow(2).GetCell(i).NumericCellValue;
                        model.openPoints += (int)theoryRow.GetCell(i).NumericCellValue;
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


                    model.testPoints = 0;
                    var exams = manager.Exams;
                    List<string> exercises = GetExercises(exams);
                    for (int i = 0; i < exercises.Count; i++)
                    {
                        if (testRow == null)
                            continue;
                        var q = new ResultModel.Question()
                        {
                            question = escapeLatex(exercises[i]),
                            testScore = (int)overrideRow?.GetCell(4 + 3 * i + 0).NumericCellValue
                        };
                        if (q.question.ToLower().EndsWith("teststudentnummer"))
                            q.question = q.question.Substring(0, q.question.Length - 33); // _ is escaped
                        if (q.question.ToLower().StartsWith("opgave"))
                            q.question = q.question.Substring(6);

                        if (testRow?.GetCell(1 + 2 * i + 1) != null && testRow?.GetCell(1 + 2 * i + 1).CellType == CellType.String)
                            q.testErrors = escapeLatex(testRow?.GetCell(1 + 2 * i + 1)?.StringCellValue);

                        if (overrideRow.GetCell(4 + 3 * i + 1) != null && overrideRow.GetCell(4 + 3 * i + 1).CellType == CellType.Numeric)
                        {
                            q.manualScore = overrideRow.GetCell(4 + 3 * i + 1).NumericCellValue + "";
                            model.testPoints += (int)overrideRow?.GetCell(4 + 3 * i + 1).NumericCellValue;
                        }
                        else 
                            model.testPoints += (int)overrideRow?.GetCell(4 + 3 * i + 0).NumericCellValue;

                        if (overrideRow.GetCell(4 + 3 * i + 2) != null && overrideRow.GetCell(4 + 3 * i + 2).CellType == CellType.String)
                            q.manualReason = escapeLatex(overrideRow.GetCell(4 + 3 * i + 2).StringCellValue);

                        model.questions.Add(q);
                    }





                    var result = service.Run("templateKey", typeof(ResultModel), model);

                    File.WriteAllText(Path.Combine(texpath, model.student.number + ".tex"), result);
                    if (File.Exists(Path.Combine(texpath, model.student.number + ".pdf")))
                        File.Delete(Path.Combine(texpath, model.student.number + ".pdf"));

                    Process process = new Process();
                    // Configure the process using the StartInfo properties.
                    process.StartInfo.FileName = "pdflatex";
                    process.StartInfo.WorkingDirectory = texpath;
                    process.StartInfo.Arguments = "-quiet " + model.student.number + ".tex";
                    process.Start();
                    process.WaitForExit();// Waits here for the process to exit.

                    if (File.Exists(Path.Combine(config.outPath, model.student.number + ".pdf")))
                        File.Delete(Path.Combine(config.outPath, model.student.number + ".pdf"));
                    File.Move(
                        Path.Combine(texpath, model.student.number + ".pdf"), 
                        Path.Combine(config.outPath, model.student.number + ".pdf"));
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

        static void CopyFiles(String pathFrom, String pathTo, Boolean filesOnly)
        {
            foreach (String file in Directory.GetFiles(pathFrom))
            {
                // Copy the current file to the new path. 
                File.Copy(file, Path.Combine(pathTo, Path.GetFileName(file)), true);

                // Get all the directories in the current path. 
                foreach (String directory in Directory.GetDirectories(pathFrom))
                {
                    // If files only is true then recursively get all the files. They will be all put in the original "PathTo" location 
                    // without the directories they were in. 
                    if (filesOnly)
                    {
                        // Get the files from the current directory in the loop. 
                        CopyFiles(directory, pathTo, filesOnly);
                    }
                    else
                    {
                        // Create a new path for the current directory in the new location.                      
                        var newDirectory = Path.Combine(pathTo, new DirectoryInfo(directory).Name);

                        // Copy the directory over to the new path location if it does not already exist. 
                        if (!Directory.Exists(newDirectory))
                        {
                            Directory.CreateDirectory(newDirectory);
                        }

                        // Call this routine again with the new path. 
                        CopyFiles(directory, newDirectory, filesOnly);
                    }
                }
            }
        }
    }
}
