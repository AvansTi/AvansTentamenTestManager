using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansTentamenManager
{
    public class ExamConfig
    {
        public string outPath { get; set; }
        public string tempPath { get; set; }
        public string zipFileName { get; set; }
        public string excelFileName { get; set;}
        public string studentSheetName { get; set; } = "Studenten";
        public int rowIndex { get; set; } = 5; //TODO: rename to studentSheetFirstRowIndex
        public int idColumn { get; set; } = 1;//TODO: rename to studentSheetStudentIdIndex
        public int firstNameColumn { get; set; } = 4;
        public int lastNameColumn { get; set; } = 7;


        public int mcCount { get; set; } = 15;
        public int mcScore { get; set; } = 2;
        public int openCount { get; set; } = 5;
        public int openScore { get; set; } = 4;

        public int scoreDivider { get; set; } = 200;

        public string subjectCode { get; set; } = "OGP1";
        public string subjectName { get; set; } = "Object georienteerd programmeren 1";

        public string mailSubject { get; set; } = "OGP1 Resultaat";
        public string mailContent { get; set; } = "Beste $FirstName,\r\n" +
                    "\r\n" +
                    "Hierbij ontvang je een automatisch gegenereerd rapport van jouw OGP1 tentamen\r\n" +
                    "Dit document kun je gebruiken ter inzage van jouw tentamen, om te zien waar wij punten voor hebben gerekend.\r\n" +
                    "In de tabel bij de praktijkopgaven in de 3e kolom het aantal punten dat je wel hebt gekregen, en in de 4e kolom de uitleg waarom je deze hoeveelheid punten hebt gekregen\r\n" +
                    "\r\n" +
                    "Als je het niet eens bent met de beoordeling, je wilt een uitleg over de opgaven of antwoorden, zien we je graag bij de inzage.\r\n" +
                    "\r\n" +
                    "met vriendelijke groet,\r\n" +
                    "Paul en Maurice";
    }
}
