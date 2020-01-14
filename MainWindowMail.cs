using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansTentamenManager
{
    partial class MainWindow
    {

        private void BtnSendEmails_Click(object sender, EventArgs e)
        {
            using (FileStream file = new FileStream(config.excelFileName, FileMode.Open, FileAccess.Read))
            {
                excelFile = new XSSFWorkbook(file);
            }
            var studentSheet = excelFile.GetSheet(config.studentSheetName);

            var files = Directory.GetFiles(config.outPath);
            foreach (var file in files)
            {
                if (Path.GetExtension(file) != ".pdf")
                    continue;
                int studentId = int.Parse(Path.GetFileNameWithoutExtension(file));
                Console.Write(file + " -> ");

                string email = "";
                string firstName = "";
                for (int i = config.rowIndex; i <= studentSheet.LastRowNum; i++)
                {
                    if (studentSheet.GetRow(i).GetCell(config.idColumn) != null &&
                        studentSheet.GetRow(i).GetCell(config.idColumn).CellType == CellType.Numeric &&
                        (int)studentSheet.GetRow(i).GetCell(config.idColumn).NumericCellValue == studentId)
                    {
                        email = studentSheet.GetRow(i).GetCell(config.emailColumn).StringCellValue;
                        firstName = studentSheet.GetRow(i).GetCell(config.firstNameColumn).StringCellValue;
                    }
                }

                Console.WriteLine(email);
                String message = "Beste " + firstName + ",\n" +
                    "\n" +
                    "Hierbij ontvang je een automatisch gegenereerd rapport van jouw OGP0 tentamen\n" +
                    "Dit document kun je gebruiken ter inzage van jouw tentamen, om te zien waar wij punten voor hebben gerekend.\n" +
                    "In de tabel bij de praktijkopgaven in de 3e kolom het aantal punten dat je wel hebt gekregen, en in de 4e kolom de uitleg waarom je deze hoeveelheid punten hebt gekregen\n" +
                    "\n" +
                    "Als je het niet eens bent met de beoordeling, je wilt een uitleg over de opgaven of antwoorden, zien we je graag bij de inzage.\n" +
                    "\n" +
                    "met vriendelijke groet,\n" +
                    "Johan, Etienne en Maurice";

               // GmailMailer.SendMail("jgc.talboom@avans.nl", "Resultaat tentamen OGP0", message, file);
                GmailMailer.SendMail(email, "Resultaat tentamen OGP0", message, file);

            }
        }

    }
}
