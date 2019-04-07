using AvansTentamenManager;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvansTentamenManager2
{
    public partial class MainWindow : Form, ITestManagerEvents
    {
        TestManager manager = new TestManager();
        public const string checkbox = "✓";


        public MainWindow()
        {
            InitializeComponent();
            manager.Register(this);
        }

        Timer pathTimer = new Timer();
        private void MainWindow_Load(object sender, EventArgs e)
        {
            txtPath.Text = "D:\\tentamen";
            manager.Path = txtPath.Text;
            //            BtnGeneratePdfs_Click(null, null);
            BtnSendEmails_Click(null, null);
            Environment.Exit(0);

            pathTimer.Interval = 1000;
            pathTimer.Tick += (s, ee) =>
            {
                manager.Path = txtPath.Text;
                pathTimer.Stop();
            };
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = txtPath.Text;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath.Text = fbd.SelectedPath;
                    manager.Path = fbd.SelectedPath;
                }
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            pathTimer.Stop();
            pathTimer.Start();
        }

        public void OnPathSelected()
        {
            tabControl1.SelectedTab = tabSetupTests;
        }

        private void timerSetupTests_Tick(object sender, EventArgs e)
        {
            bool ok = true;

            if (manager.TestLibSetup())
            {
                lblSetupLibs.Text = checkbox;
                lblSetupLibs.ForeColor = Color.Green;
            }
            else
            {
                lblSetupLibs.Text = "X";
                lblSetupLibs.ForeColor = Color.Red;
                ok = false;
            }
            if (manager.TestTestSetup())
            {
                lblSetupTests.Text = checkbox;
                lblSetupTests.ForeColor = Color.Green;
            }
            else
            {
                lblSetupTests.Text = "X";
                lblSetupTests.ForeColor = Color.Red;
                ok = false;
            }

            btnSetupTestNext.Enabled = ok;
            if (ok)
                btnSetupTestNext_Click(null, null);
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabSetupTests)
                timerSetupTests.Start();
            else
                timerSetupTests.Stop();


            if (tabControl1.SelectedTab == tabRunTests)
            {
                UpdateTestTab();
            }
        }

        private void UpdateTestTab()
        {
            var directories = manager.Exams;


            listTestUsers.Items.Clear();
            foreach (var dir in directories)
            {

                ListViewItem item = new ListViewItem(dir.name);

                bool isTested = File.Exists(dir.projectPath + "/log.json");
                item.Tag = dir;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, isTested + ""));

                listTestUsers.Items.Add(item);

                if (isTested)
                {
                    JArray results = JArray.Parse(File.ReadAllText(dir.projectPath + "/log.json"));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, results.Last["time"] + ""));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, results.Last["test"]["score"] + ""));

                    bool compileError = false;
                    foreach (var c in results.Last["compile"])
                        if (((string)c).Contains("Compile error"))
                            compileError = true;

                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, compileError + ""));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, results.Last["studentid"] + ""));
                }
                else
                {
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "-"));
                }
            }

            listTestUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnSetupTestNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabRunTests;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (listTestUsers.SelectedItems.Count == 0)
                return;

            Exam exam = (Exam)listTestUsers.SelectedItems[0].Tag;
            exam.Test(manager);
            UpdateTestTab();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listTestUsers.Items)
            {
                Exam exam = (Exam)item.Tag;
                exam.Test(manager);
            }
            UpdateTestTab();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        XSSFWorkbook excelFile;
        string excelFileName;
        ISheet studentSheet;
        string studentSheetName = "Studenten";

        int rowIndex = 5;
        int idColumn = 1;
        int firstNameColumn = 4;
        int lastNameColumn = 7;

        int mcCount = 0;
        int openCount = 1;


        int mcScore = 2;
        int openScore = 4;

        private void btnSelectExportFile_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.FileName = "c:\\users\\johan\\desktop\\Resultaat.xlsx";
                fbd.InitialDirectory = "c:\\users\\johan\\desktop";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    using (FileStream file = new FileStream(fbd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        excelFileName = fbd.FileName;
                        excelFile = new XSSFWorkbook(file);
                    }
                }
            }
        }

        private void btnImportStudentListExcel_Click(object sender, EventArgs e)
        {
            List<string> sheetNames = new List<string>();
            for (int i = 0; i < excelFile.NumberOfSheets; i++)
                sheetNames.Add(excelFile.GetSheetAt(i).SheetName);

            string sheet = ListDialog.Pick("Please select the sheet with the students", sheetNames);
            if (sheet == "")
                return;
            studentSheet = excelFile.GetSheet(sheet);
            studentSheetName = studentSheet.SheetName;

            List<string> rows = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                var row = studentSheet.GetRow(i);
                if (row == null)
                {
                    rows.Add("");
                    continue;
                }

                string rowStr = "";
                for (int ii = 0; ii < 10; ii++)
                {
                    var cell = row.GetCell(ii);
                    if (cell != null && cell.CellType == CellType.String)
                        rowStr += cell.StringCellValue + ", ";
                }
                rows.Add(rowStr);
            }
            string rowRes = ListDialog.Pick("Please select the row header", rows);
            rowIndex = rows.IndexOf(rowRes);


            List<string> columns = new List<string>();
            for (int i = 0; i < 30; i++)
            {
                var row = studentSheet.GetRow(rowIndex);

                var cell = row.GetCell(i);
                if (cell != null && cell.CellType == CellType.String)
                    columns.Add(cell.StringCellValue);
                else
                    columns.Add("");
            }

            string idColumnStr = ListDialog.Pick("Which one has the student number?", columns);
            string firstNameColumnStr = ListDialog.Pick("Which one has the first name?", columns);
            string lastNameColumnStr = ListDialog.Pick("Which one has the last name?", columns);

            idColumn = columns.IndexOf(idColumnStr);
            firstNameColumn = columns.IndexOf(firstNameColumnStr);
            lastNameColumn = columns.IndexOf(lastNameColumnStr);

        }

        private void btnAddResults_Click(object sender, EventArgs e)
        {
            string sheetName = txtResultTabName.Text;
            if (excelFile.GetSheet(sheetName) != null)
                excelFile.RemoveSheetAt(excelFile.GetSheetIndex(sheetName));

            ISheet sheet = excelFile.CreateSheet(sheetName);

            var exams = manager.Exams;

            var header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("Studentnumber");

            List<string> exercises = GetExercises(exams);

            int i = 1;
            foreach (var exercise in exercises)
            {
                header.CreateCell(i).SetCellValue(exercise);
                header.CreateCell(i + 1).SetCellValue(exercise + "_result");
                i += 2;
            }



            int rowIndex = 1;
            foreach (var dir in exams)
            {
                bool isTested = File.Exists(dir.projectPath + "/log.json");
                if (!isTested)
                    continue;
                JArray results = JArray.Parse(File.ReadAllText(dir.projectPath + "/log.json"));
                JToken lastRun = results.Last;

                var row = sheet.CreateRow(rowIndex);

                row.CreateCell(0, CellType.Numeric).SetCellValue((int)lastRun["studentid"]);

                i = 1;
                foreach (var exercise in exercises)
                {
                    int score = 0;
                    if (lastRun["test"]["scores"][exercise] != null)
                        score = lastRun["test"]["scores"][exercise].Value<int>();

                    row.CreateCell(i, CellType.Numeric).SetCellValue(score);

                    List<string> errors = new List<string>();
                    foreach (KeyValuePair<string, JToken> pair in lastRun["test"]["errors"].Value<JObject>())
                        if (pair.Key.StartsWith(exercise))
                            errors.Add(pair.Value.Value<string>());

                    row.CreateCell(i + 1, CellType.String).SetCellValue(string.Join("\n", errors));
                    i += 2;
                }
                rowIndex++;
            }
            SaveExcel();
        }

        private List<string> GetExercises(IEnumerable<Exam> directories)
        {
            List<string> exercises = new List<string>();
            foreach (var dir in directories)
            {
                bool isTested = File.Exists(dir.projectPath + "/log.json");
                if (!isTested)
                    continue;
                JArray results = JArray.Parse(File.ReadAllText(dir.projectPath + "/log.json"));
                JToken lastRun = results.Last;
                JObject scores = lastRun["test"]["scores"].Value<JObject>();
                foreach (KeyValuePair<string, JToken> property in scores)
                    if (!exercises.Contains(property.Key))
                        exercises.Add(property.Key);
            }

            exercises.Sort();
            return exercises;
        }

        private void SaveExcel()
        {
            using (FileStream stream = new FileStream(excelFileName, FileMode.Create, FileAccess.Write))
            {
                excelFile.Write(stream);
            }
        }

        private void btnAddTheory_Click(object sender, EventArgs e)
        {
            string sheetName = txtTheoryTabName.Text;
            if (excelFile.GetSheet(sheetName) != null)
            {
                if (DialogResult.Yes != MessageBox.Show("This sheet already exists. Would you like to replace it? This will overwrite any data in the sheet!", "Warning", MessageBoxButtons.YesNo))
                    return;
                excelFile.RemoveSheetAt(excelFile.GetSheetIndex(sheetName));
            }



            ISheet sheet = excelFile.CreateSheet(sheetName);

            var questionIndexRow = sheet.CreateRow(0);
            int q = 0;
            var answerRow = sheet.CreateRow(1);
            var pointsRow = sheet.CreateRow(2);
            for (int i = 5; i < 5 + mcCount; i++)
            {
                answerRow.CreateCell(i, CellType.String).SetCellValue("A");
                pointsRow.CreateCell(i, CellType.Numeric).SetCellValue(mcScore);
                questionIndexRow.CreateCell(i, CellType.String).SetCellValue("0" + CellReference.ConvertNumToColString(q++));
            }
            for (int i = 5 + mcCount + 1; i < 5 + mcCount + 1 + openCount * 2; i += 2)
            {
                pointsRow.CreateCell(i, CellType.Numeric).SetCellValue(openScore);
                questionIndexRow.CreateCell(i, CellType.String).SetCellValue("0" + CellReference.ConvertNumToColString(q++));
            }
            questionIndexRow.CreateCell(5 + mcCount + 2 + openCount * 2).SetCellValue("Totaal");

            var directories = manager.Exams;

            var header = sheet.CreateRow(3);
            int rowIndex = 4;
            foreach (var dir in directories)
            {
                bool isTested = File.Exists(dir.projectPath + "/log.json");
                if (!isTested)
                    continue;
                JArray results = JArray.Parse(File.ReadAllText(dir.projectPath + "/log.json"));
                JToken lastRun = results.Last;

                var row = sheet.CreateRow(rowIndex);

                row.CreateCell(0, CellType.Numeric).SetCellValue((int)lastRun["studentid"]);
                row.CreateCell(1, CellType.Formula).SetCellFormula("VLOOKUP(A"+(rowIndex+1)+", Studenten!B:H, 4, FALSE)");
                row.CreateCell(2, CellType.Formula).SetCellFormula("VLOOKUP(A"+(rowIndex+1)+", Studenten!B:H, 7, FALSE)");
                row.CreateCell(3);
                row.CreateCell(4);

                for (int i = 5; i < 5 + mcCount; i++)
                    row.CreateCell(i, CellType.String).SetCellValue(" ");

                string sumMc = "";
                for (int i = 0; i < mcCount; i++)
                    sumMc += "IF($"+CellReference.ConvertNumToColString(i+5)+""+(rowIndex+1)+" = $" + CellReference.ConvertNumToColString(i + 5) + "$2, $" + CellReference.ConvertNumToColString(i + 5) + "$3, 0) + ";
                sumMc += 0;

                row.CreateCell(5 + mcCount, CellType.Formula).SetCellFormula(sumMc);

                for (int i = 5 + mcCount + 1; i < 5 + mcCount + 1 + openCount * 2; i += 2)
                {
                    row.CreateCell(i, CellType.Numeric).SetCellValue(0);
                    row.CreateCell(i + 1, CellType.String).SetCellValue("");
                }

                string sumFormula = "SUM(";
                sumFormula += "$" + CellReference.ConvertNumToColString(5 + mcCount) + "" + (rowIndex + 1) + ", ";
                for (int i = 5 + mcCount + 1; i < 5 + mcCount + 1 + openCount * 2; i += 2)
                    sumFormula += "$" + CellReference.ConvertNumToColString(i) + "" + (rowIndex + 1) + ", ";

                sumFormula += "0)";
                row.CreateCell(5 + mcCount + 2 + openCount * 2, CellType.Formula).SetCellFormula(sumFormula);



                rowIndex++;
            }
            SaveExcel();
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            string sheetName = txtOverrideTabName.Text;
            if (excelFile.GetSheet(sheetName) != null)
            {
                if (DialogResult.Yes != MessageBox.Show("This sheet already exists. Would you like to replace it? This will overwrite any data in the sheet!", "Warning", MessageBoxButtons.YesNo))
                    return;
                excelFile.RemoveSheetAt(excelFile.GetSheetIndex(sheetName));
            }

            ISheet sheet = excelFile.CreateSheet(sheetName);

            var header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("StudentNumber");
            header.CreateCell(1).SetCellValue("First name");
            header.CreateCell(2).SetCellValue("Last name");

            var exams = manager.Exams;
            List<string> exercises = GetExercises(exams);

            ICellStyle blockedStyle = excelFile.CreateCellStyle();
            blockedStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            blockedStyle.FillPattern = FillPattern.SolidForeground;

            for (int i = 0; i < exercises.Count; i++)
            {
                sheet.SetDefaultColumnStyle(4 + 3 * i, blockedStyle);
                header.CreateCell(4 + 3 * i + 0).SetCellValue(exercises[i]);
                header.CreateCell(4 + 3 * i + 1).SetCellValue("Correctie punten");
                header.CreateCell(4 + 3 * i + 2).SetCellValue("Reden");
                sheet.SetColumnWidth(4 + 3 * i + 0, 1000);
                sheet.SetColumnWidth(4 + 3 * i + 1, 1000);
                sheet.SetColumnWidth(4 + 3 * i + 2, 1000);
            }
            header.CreateCell(4 + 3 * exercises.Count).SetCellValue("Totaal test");
            header.CreateCell(4 + 3 * exercises.Count+1).SetCellValue("Correctie");

            int rowIndex = 1;
            foreach (var dir in exams)
            {
                bool isTested = File.Exists(dir.projectPath + "/log.json");
                if (!isTested)
                    continue;
                JArray results = JArray.Parse(File.ReadAllText(dir.projectPath + "/log.json"));
                JToken lastRun = results.Last;

                var row = sheet.CreateRow(rowIndex);

                row.CreateCell(0, CellType.Numeric).SetCellValue((int)lastRun["studentid"]);
                row.CreateCell(1, CellType.Formula).SetCellFormula("VLOOKUP(A" + (rowIndex + 1) + ", Studenten!B:H, 4, FALSE)");
                row.CreateCell(2, CellType.Formula).SetCellFormula("VLOOKUP(A" + (rowIndex + 1) + ", Studenten!B:H, 7, FALSE)");


                int i = 4;
                foreach (var exercise in exercises)
                {
                    int score = 0;
                    if (lastRun["test"]["scores"][exercise] != null)
                        score = lastRun["test"]["scores"][exercise].Value<int>();
                    var cell = row.CreateCell(i, CellType.Numeric);
                    cell.SetCellValue(score);
                    cell.CellStyle = blockedStyle;
                    i += 3;
                }

                string sum = "SUM(";
                for (i = 0; i < exercises.Count; i++)
                    sum += CellReference.ConvertNumToColString(4 + i * 3) + (rowIndex + 1) + ", ";
                sum += "0)";
                row.CreateCell(4 + 3 * exercises.Count, CellType.Formula).SetCellFormula(sum);

                string correction = "";
                for (i = 0; i < exercises.Count; i++)
                    correction += "IF(ISNUMBER("+ CellReference.ConvertNumToColString(4 + i * 3+1) + (rowIndex+1)+ ")," + CellReference.ConvertNumToColString(4 + i * 3+1) + (rowIndex + 1) + "-" + CellReference.ConvertNumToColString(4 + i * 3) + (rowIndex + 1) + ",0) + ";

                correction += "0";
                row.CreateCell(5 + 3 * exercises.Count, CellType.Formula).SetCellFormula(correction);

                rowIndex++;
            }

            SaveExcel();
        }

        private void btnAddOverview_Click(object sender, EventArgs e)
        {
            var exams = manager.Exams;
            List<string> exercises = GetExercises(exams);

            string sheetName = txtOverviewTabName.Text;
            if (excelFile.GetSheet(sheetName) != null)
            {
                if (DialogResult.Yes != MessageBox.Show("This sheet already exists. Would you like to replace it? This will overwrite any data in the sheet!", "Warning", MessageBoxButtons.YesNo))
                    return;
                excelFile.RemoveSheetAt(excelFile.GetSheetIndex(sheetName));
            }

            ISheet sheet = excelFile.CreateSheet(sheetName);
            var header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("StudentNumber");
            header.CreateCell(1).SetCellValue("First name");
            header.CreateCell(2).SetCellValue("Last name");
            header.CreateCell(3).SetCellValue("Automatic Test");
            header.CreateCell(4).SetCellValue("Manual Correction");
            header.CreateCell(5).SetCellValue("Theory");
            header.CreateCell(6).SetCellValue("Total");
            header.CreateCell(7).SetCellValue("Grade");
            header.CreateCell(8).SetCellValue("Manual grading by");


            for (int i = 1; i < 500; i++)
            {
                var row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellFormula(studentSheetName + "!" + CellReference.ConvertNumToColString(idColumn) + (rowIndex + 1 + i));
                row.CreateCell(1).SetCellFormula(studentSheetName + "!" + CellReference.ConvertNumToColString(firstNameColumn) + (rowIndex + 1 + i));
                row.CreateCell(2).SetCellFormula(studentSheetName + "!" + CellReference.ConvertNumToColString(lastNameColumn) + (rowIndex + 1 + i));


                string id = new CellReference(row.Cells[0]).FormatAsString();

                //=VLOOKUP(A2, Override!A:AS, 44, FALSE)
                row.CreateCell(3).SetCellFormula($"VLOOKUP({id}, {txtOverrideTabName.Text}!A:{CellReference.ConvertNumToColString(exercises.Count * 3 + 10)}, {exercises.Count * 3 + 5}, FALSE)");
                //=VLOOKUP(A2, Override!A:AS, 45, FALSE)
                row.CreateCell(4).SetCellFormula($"VLOOKUP({id}, {txtOverrideTabName.Text}!A:{CellReference.ConvertNumToColString(exercises.Count * 3 + 10)}, {exercises.Count * 3 + 6}, FALSE)");
                //=VLOOKUP(A2,Theory!A:I, 9,FALSE)
                row.CreateCell(5).SetCellFormula($"VLOOKUP({id}, {txtTheoryTabName.Text}!A:{CellReference.ConvertNumToColString(5 + mcCount + 2 + openCount * 2)}, {5 + mcCount + 2 + openCount * 2}, FALSE)");
                //=D2+E2+F2
                row.CreateCell(6).SetCellFormula($"{new CellReference(row.Cells[3]).FormatAsString()}+{new CellReference(row.Cells[4]).FormatAsString()}+{new CellReference(row.Cells[5]).FormatAsString()}");
                //=G2/10
                row.CreateCell(7).SetCellFormula($"MIN(10,{new CellReference(row.Cells[6]).FormatAsString()}/100*10)");
            }

            SaveExcel();
        }



        private void BtnGeneratePdfs_Click(object sender, EventArgs e)
        {
            var config = new TemplateServiceConfiguration();
            config.EncodedStringFactory = new RawStringFactory(); // Raw string encoding.
            config.Language = Language.CSharp;


            using (var service = RazorEngineService.Create(config))
            {
                string template = File.ReadAllText("Templates/v1/template.tex");
                service.Compile(template, "templateKey", typeof(ResultModel));

                excelFile = new XSSFWorkbook("c:\\users\\johan\\desktop\\Resultaat.xlsx");
                for(int index = 1; index <= excelFile.GetSheet(txtOverviewTabName.Text).LastRowNum; index++)
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
                    try{ model.TotalPoints = (int)overviewRow.GetCell(6).NumericCellValue; } catch (Exception){}
                    try{ model.Grade = (decimal)overviewRow.GetCell(7).NumericCellValue; } catch (Exception){}
                    try{ model.ManualCorrector = overviewRow.GetCell(8).StringCellValue; } catch (Exception){}





                    for (int i = 5; i < 5 + mcCount; i++)
                    {
                        model.mc.Add(new ResultModel.McQuestion()
                        {

                        });
                    }
                    for (int i = 4 + mcCount + 1; i < 4 + mcCount + 1 + openCount * 2; i += 2)
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
                    for(int i = 0; i < exercises.Count; i++)
                    {
                        if (testRow == null)
                            continue;
                        var q = new ResultModel.Question()
                        {
                            question = escapeLatex(exercises[i]),
                            testErrors = escapeLatex(testRow?.GetCell(1 + 2 * i + 1)?.StringCellValue),
                            testScore = (int)overrideRow?.GetCell(4 + 3 * i + 0).NumericCellValue
                        };

                        if(q.question.ToLower().EndsWith("teststudentnummer"))
                            q.question = q.question.Substring(0, q.question.Length-33); // _ is escaped
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
            text = text.Replace("\n", "\\\\newline");
            text = text.Replace("&", "\\\\&");
            text = text.Replace("_", "\\textunderscore ");
            text = text.Replace("<", "\\guillemotleft ");
            text = text.Replace(">", "\\guillemotright ");

            return text;
        }

        private void BtnSendEmails_Click(object sender, EventArgs e)
        {
            excelFile = new XSSFWorkbook("c:\\users\\johan\\desktop\\Resultaat.xlsx");

            var studentSheet = excelFile.GetSheet(studentSheetName);

            var files = Directory.GetFiles("D:\\Tentamen\\pdf");
            foreach(var file in files)
            {
                if (Path.GetExtension(file) != ".pdf")
                    continue;
                int studentId = int.Parse(Path.GetFileNameWithoutExtension(file));
                Console.Write(file + " -> ");

                string email = "";
                string firstName = "";
                for(int i = rowIndex; i <= studentSheet.LastRowNum; i++)
                {
                    if (studentSheet.GetRow(i).GetCell(idColumn) != null &&
                        studentSheet.GetRow(i).GetCell(idColumn).CellType == CellType.Numeric &&
                        (int)studentSheet.GetRow(i).GetCell(idColumn).NumericCellValue == studentId)
                    {
                        email = studentSheet.GetRow(i).GetCell(9).StringCellValue; // TODO: fix hardcoded value
                        firstName = studentSheet.GetRow(i).GetCell(firstNameColumn).StringCellValue;
                    }
                }

                Console.WriteLine(email);
                String message = "Beste " + firstName + ",\r\n" +
                    "\r\n" +
                    "Hierbij ontvang je een automatisch gegenereerd rapport van jouw OGP1 tentamen\r\n" +
                    "Dit document kun je gebruiken ter inzage van jouw tentamen, om te zien waar wij punten voor hebben gerekend.\r\n" +
                    "In de tabel bij de praktijkopgaven in de 3e kolom het aantal punten dat je wel hebt gekregen, en in de 4e kolom de uitleg waarom je deze hoeveelheid punten hebt gekregen\r\n" +
                    "\r\n" +
                    "Als je het niet eens bent met de beoordeling, je wilt een uitleg over de opgaven of antwoorden, zien we je graag bij de inzage.\r\n" +
                    "\r\n" +
                    "met vriendelijke groet,\r\n" +
                    "Paul en Maurice";

                GmailMailer.SendMail("jgc.talboom@avans.nl", "Resultaat tentamen OOGP2", message, file);

                break;

            }
        }
    }
}
