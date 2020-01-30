using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvansTentamenManager
{
    partial class MainWindow
    {
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
                        excelFile = new XSSFWorkbook(file);
                    }
                }
            }
        }

        private void btnImportStudentListExcel_Click(object sender, EventArgs e)
        {
            try
            {
                excelFile = new XSSFWorkbook(config.excelFileName);
            } catch(InvalidDataException ex)
            {
                MessageBox.Show("Error opening excel file: " + ex);
                return;
            }

            List<string> sheetNames = new List<string>();
            for (int i = 0; i < excelFile.NumberOfSheets; i++)
                sheetNames.Add(excelFile.GetSheetAt(i).SheetName);

            string sheet = ListDialog.Pick("Please select the sheet with the students", sheetNames);
            if (sheet == "")
                return;
            var studentSheet = excelFile.GetSheet(sheet);
            config.studentSheetName = txtStudentTabName.Text = studentSheet.SheetName;

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
            numHeaderRow.Value = config.rowIndex = rows.IndexOf(rowRes);


            List<string> columns = new List<string>();
            for (int i = 0; i < 30; i++)
            {
                var row = studentSheet.GetRow(config.rowIndex);

                var cell = row.GetCell(i);
                if (cell != null && cell.CellType == CellType.String)
                    columns.Add(cell.StringCellValue);
                else
                    columns.Add("");
            }

            string idColumnStr = ListDialog.Pick("Which one has the student number?", columns);
            string firstNameColumnStr = ListDialog.Pick("Which one has the first name?", columns);
            string lastNameColumnStr = ListDialog.Pick("Which one has the last name?", columns);
            string emailColumnStr = ListDialog.Pick("Which one has the email address", columns);

            numStudentIdCol.Value = columns.IndexOf(idColumnStr);
            numStudentFirstNameCol.Value = columns.IndexOf(firstNameColumnStr);
            numStudentLastNameCol.Value = columns.IndexOf(lastNameColumnStr);
            numStudentEmailCol.Value = columns.IndexOf(emailColumnStr);

            SaveConfig();

            excelFile.Close();
        }

        private void btnStudentsFromExternal_Click(object sender, EventArgs e)
        {
           // excelFile = new XSSFWorkbook("C:\\Users\\johan\\Avans Hogeschool\\AEI Technische Informatica - Documents\\TI-1.2\\TMTI-OGP1 Object Georienteerd programmeren 1\\Toetsing\\Hertentamen\\students.csv");
        }


        private void btnImportStudentsFromBb_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.FileName = Path.Combine(config.outPath, "students.csv");
                fbd.InitialDirectory = config.outPath;
                fbd.Filter = "(*.csv)|*.csv|All Files (*.*)|*.*";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    using (TextFieldParser parser = new TextFieldParser(fbd.FileName))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(";");

                        if(!File.Exists(config.excelFileName))
                        {
                            excelFile = new XSSFWorkbook();
                            SaveExcel();
                        }
                        else
                        {
                            using (FileStream file = new FileStream(config.excelFileName, FileMode.Open, FileAccess.Read))
                            {
                                excelFile = new XSSFWorkbook(file);
                            }
                        }
                        if (config.studentSheetName == "")
                            txtStudentTabName.Text = config.studentSheetName = "Students";
                        var sheet = excelFile.CreateSheet(config.studentSheetName);
                        int rowIndex = 0;
                        while(!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();
                            var row = sheet.CreateRow(rowIndex);
                            for(int cell = 0; cell < fields.Length; cell++)
                            {
                                try
                                {
                                    if (cell == 0)
                                        row.CreateCell(cell).SetCellValue(int.Parse(fields[cell]));
                                    else
                                        row.CreateCell(cell).SetCellValue(fields[cell]);
                                }catch(FormatException)
                                {
                                }
                            }
                            rowIndex++;
                        }

                        numHeaderRow.Value = 0M;
                        numStudentIdCol.Value = 0M;
                        numStudentFirstNameCol.Value = 2;
                        numStudentLastNameCol.Value = 1;
                        numStudentEmailCol.Value = 4;
                        SaveExcel();

                    }
                }
            }

        }

        private void btnAddResults_Click(object sender, EventArgs e)
        {
            using (FileStream file = new FileStream(config.excelFileName, FileMode.Open, FileAccess.Read))
            {
                excelFile = new XSSFWorkbook(file);
            }

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
            header.CreateCell(i).SetCellValue("Compile errors");



            int rowIndex = 1;
            foreach (var exam in exams)
            {
               
                if (!exam.isTested)
                    continue;
                JToken lastRun = exam.lastResult;

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
                int errorCount = 0;
                foreach(KeyValuePair<string, JToken> c in lastRun["compile"].Value<JObject>())
                {
                    if (c.Value.Value<string>().ToLower().Contains("error"))
                        errorCount++;
                }

                row.CreateCell(i, CellType.Numeric).SetCellValue(errorCount);
                rowIndex++;
            }
            SaveExcel();
        }

        private List<string> GetExercises(IEnumerable<Exam> exams)
        {
            List<string> exercises = new List<string>();
            foreach (var exam in exams)
            {
                if (!exam.isTested)
                    continue;
                JToken lastRun = exam.lastResult;
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
            try
            {
                using (FileStream stream = new FileStream(config.excelFileName, FileMode.Create, FileAccess.Write))
                {
                    excelFile.Write(stream);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(String.Format("Exception: {0}", ex.Message));
            }
        }

        private void btnAddTheory_Click(object sender, EventArgs e)
        {
            using (FileStream file = new FileStream(config.excelFileName, FileMode.Open, FileAccess.Read))
            {
                excelFile = new XSSFWorkbook(file);
            }
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
            for (int i = 5; i < 5 + config.mcCount; i++)
            {
                answerRow.CreateCell(i, CellType.String).SetCellValue("A");
                pointsRow.CreateCell(i, CellType.Numeric).SetCellValue(config.mcScore);
                questionIndexRow.CreateCell(i, CellType.String).SetCellValue("0" + CellReference.ConvertNumToColString(q++));
            }
            for (int i = 5 + config.mcCount + 1; i < 5 + config.mcCount + 1 + config.openCount * 2; i += 2)
            {
                pointsRow.CreateCell(i, CellType.Numeric).SetCellValue(config.openScore);
                questionIndexRow.CreateCell(i, CellType.String).SetCellValue("0" + CellReference.ConvertNumToColString(q++));
            }
            questionIndexRow.CreateCell(5 + config.mcCount + 2 + config.openCount * 2).SetCellValue("Totaal");

            var exams = manager.Exams;

            var header = sheet.CreateRow(3);
            int rowIndex = 4;
            foreach (var exam in exams)
            {
                if (!exam.isTested)
                    continue;
                JToken lastRun = exam.lastResult;

                var row = sheet.CreateRow(rowIndex);

                row.CreateCell(0, CellType.Numeric).SetCellValue((int)lastRun["studentid"]);
                row.CreateCell(1, CellType.Formula).SetCellFormula($"VLOOKUP(A{rowIndex + 1}, {config.studentSheetName}!{CellReference.ConvertNumToColString(config.idColumn)}:ZZ, {config.firstNameColumn+1}, FALSE)");
                row.CreateCell(2, CellType.Formula).SetCellFormula($"VLOOKUP(A{rowIndex + 1}, {config.studentSheetName}!{CellReference.ConvertNumToColString(config.idColumn)}:ZZ, {config.lastNameColumn+1}, FALSE)");
                row.CreateCell(3);
                row.CreateCell(4);

                for (int i = 5; i < 5 + config.mcCount; i++)
                    row.CreateCell(i, CellType.String).SetCellValue(" ");

                string sumMc = "";
                for (int i = 0; i < config.mcCount; i++)
                    sumMc += "IF($" + CellReference.ConvertNumToColString(i + 5) + "" + (rowIndex + 1) + " = $" + CellReference.ConvertNumToColString(i + 5) + "$2, $" + CellReference.ConvertNumToColString(i + 5) + "$3, 0) + ";
                sumMc += 0;

                row.CreateCell(5 + config.mcCount, CellType.Formula).SetCellFormula(sumMc);

                for (int i = 5 + config.mcCount + 1; i < 5 + config.mcCount + 1 + config.openCount * 2; i += 2)
                {
                    row.CreateCell(i, CellType.Numeric).SetCellValue(0);
                    row.CreateCell(i + 1, CellType.String).SetCellValue("");
                }

                string sumFormula = "SUM(";
                sumFormula += "$" + CellReference.ConvertNumToColString(5 + config.mcCount) + "" + (rowIndex + 1) + ", ";
                for (int i = 5 + config.mcCount + 1; i < 5 + config.mcCount + 1 + config.openCount * 2; i += 2)
                    sumFormula += "$" + CellReference.ConvertNumToColString(i) + "" + (rowIndex + 1) + ", ";

                sumFormula += "0)";
                row.CreateCell(5 + config.mcCount + 2 + config.openCount * 2, CellType.Formula).SetCellFormula(sumFormula);



                rowIndex++;
            }
            SaveExcel();
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            using (FileStream file = new FileStream(config.excelFileName, FileMode.Open, FileAccess.Read))
            {
                excelFile = new XSSFWorkbook(file);
            }
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
            header.CreateCell(4 + 3 * exercises.Count + 1).SetCellValue("Correctie");

            int rowIndex = 1;
            foreach (var exam in exams)
            {
                if (!exam.isTested)
                    continue;
                JToken lastRun = exam.lastResult;

                var row = sheet.CreateRow(rowIndex);

                row.CreateCell(0, CellType.Numeric).SetCellValue((int)lastRun["studentid"]);
                row.CreateCell(1, CellType.Formula).SetCellFormula($"VLOOKUP(A{rowIndex + 1}, {config.studentSheetName}!{CellReference.ConvertNumToColString(config.idColumn)}:ZZ, {config.firstNameColumn + 1}, FALSE)");
                row.CreateCell(2, CellType.Formula).SetCellFormula($"VLOOKUP(A{rowIndex + 1}, {config.studentSheetName}!{CellReference.ConvertNumToColString(config.idColumn)}:ZZ, {config.lastNameColumn + 1}, FALSE)");


                int i = 4;
                foreach (var exercise in exercises)
                {
                    int score = 0;
                    if (lastRun["test"]["scores"][exercise] != null)
                        score = lastRun["test"]["scores"][exercise].Value<int>();
                    //                    var cell = row.CreateCell(i, CellType.Numeric);
                    //                    cell.SetCellValue(score
                    var cell = row.CreateCell(i, CellType.Formula);
                    cell.SetCellFormula($"VLOOKUP(A{rowIndex + 1},'{config.sheetTestResultName}'!A:ZZ, {((i-4)/3)*2 + 2}, FALSE)");

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
                    correction += "IF(ISNUMBER(" + CellReference.ConvertNumToColString(4 + i * 3 + 1) + (rowIndex + 1) + ")," + CellReference.ConvertNumToColString(4 + i * 3 + 1) + (rowIndex + 1) + "-" + CellReference.ConvertNumToColString(4 + i * 3) + (rowIndex + 1) + ",0) + ";

                correction += "0";
                row.CreateCell(5 + 3 * exercises.Count, CellType.Formula).SetCellFormula(correction);

                rowIndex++;
            }

            SaveExcel();
        }

        private void btnAddOverview_Click(object sender, EventArgs e)
        {
            using (FileStream file = new FileStream(config.excelFileName, FileMode.Open, FileAccess.Read))
            {
                excelFile = new XSSFWorkbook(file);
            }
            var exams = manager.Exams;
            List<string> exercises = GetExercises(exams);

            string sheetName = txtOverviewTabName.Text;
            if (excelFile.GetSheet(sheetName) != null)
            {
                if (DialogResult.Yes != MessageBox.Show("This sheet already exists. Would you like to replace it? This will overwrite any data in the sheet!", "Warning", MessageBoxButtons.YesNo))
                    return;
                excelFile.RemoveSheetAt(excelFile.GetSheetIndex(sheetName));
            }
            ISheet studentenSheet = excelFile.GetSheet(config.studentSheetName);
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
            header.CreateCell(9).SetCellValue("Compile errors");


            for (int i = 1; i <= studentenSheet.LastRowNum+1; i++)
            {
                var row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellFormula(config.studentSheetName + "!" + CellReference.ConvertNumToColString(config.idColumn) + (config.rowIndex + 1 + i));
                row.CreateCell(1).SetCellFormula(config.studentSheetName + "!" + CellReference.ConvertNumToColString(config.firstNameColumn) + (config.rowIndex + 1 + i));
                row.CreateCell(2).SetCellFormula(config.studentSheetName + "!" + CellReference.ConvertNumToColString(config.lastNameColumn) + (config.rowIndex + 1 + i));


                string id = new CellReference(row.Cells[0]).FormatAsString();

                //=VLOOKUP(A2, Override!A:AS, 44, FALSE)
                row.CreateCell(3).SetCellFormula($"VLOOKUP({id}, {txtOverrideTabName.Text}!A:{CellReference.ConvertNumToColString(exercises.Count * 3 + 10)}, {exercises.Count * 3 + 5}, FALSE)");
                //=VLOOKUP(A2, Override!A:AS, 45, FALSE)
                row.CreateCell(4).SetCellFormula($"VLOOKUP({id}, {txtOverrideTabName.Text}!A:{CellReference.ConvertNumToColString(exercises.Count * 3 + 10)}, {exercises.Count * 3 + 6}, FALSE)");
                //=VLOOKUP(A2,Theory!A:I, 9,FALSE)
                row.CreateCell(5).SetCellFormula($"VLOOKUP({id}, {txtTheoryTabName.Text}!A:{CellReference.ConvertNumToColString(5 + config.mcCount + 2 + config.openCount * 2)}, {6 + config.mcCount + 2 + config.openCount * 2}, FALSE)");
                //=D2+E2+F2
                row.CreateCell(6).SetCellFormula($"{new CellReference(row.Cells[3]).FormatAsString()}+{new CellReference(row.Cells[4]).FormatAsString()}+{new CellReference(row.Cells[5]).FormatAsString()}");
                //=G2/10
                row.CreateCell(7).SetCellFormula($"MIN(10,{new CellReference(row.Cells[6]).FormatAsString()}/"+config.scoreDivider+"*10)");
                row.CreateCell(8).SetCellValue("");
                //=
                row.CreateCell(9).SetCellFormula($"VLOOKUP({id}, '{txtResultTabName.Text}'!A:{CellReference.ConvertNumToColString(exercises.Count*2+2)}, {exercises.Count*2+2}, FALSE)");
            }

            SaveExcel();
        }



    }
}
