using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansTentamenManager
{
    public class ResultModel
    {
        public class Student
        {
            public int number { get; set; }
            public string name { get; set; }
        }

        public class McQuestion
        {
            public string question { get; set; }
            public string answer { get; set; }
            public string correctanswer { get; set; }
            public int score { get; set; }
        }
        public class OpenQuestion
        {
            public string question { get; set; }
            public int score { get; set; }
            public string reason { get; set; }
        }

        public class Question
        {
            public string question { get; set; }
            public int testScore { get; set; }
            public string manualScore { get; set; } //can be empty
            public string manualReason { get; set; }
            public string testErrors { get; set; }
        }

        public Student student { get; set; } = new Student();

        public string ManualCorrector { get; set; }
        public decimal Grade { get; set; }
        public int TotalPoints { get; set; }
        
        public int mcTotalPoints { get; set; }
        public List<McQuestion> mc { get; set; } = new List<McQuestion>();
        public List<OpenQuestion> open { get; set; } = new List<OpenQuestion>();

        public List<Question> questions { get; set; } = new List<Question>();

    }
}
