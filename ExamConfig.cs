using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansTentamenManager
{
    class ExamConfig
    {
        public string excelFileName { get; set;}
        public string studentSheetName { get; set; }
        public int rowIndex { get; set; } = 5; //TODO: rename to studentSheetFirstRowIndex
        public int idColumn { get; set; } = 1;//TODO: rename to studentSheetStudentIdIndex
        public int firstNameColumn { get; set; } = 4;
        public int lastNameColumn { get; set; } = 7;


        public int mcCount { get; set; }
        public int mcScore { get; set; }
        public int openCount { get; set; }
        public int openScore { get; set; }


    }
}
