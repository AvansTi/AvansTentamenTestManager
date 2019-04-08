using AvansTentamenManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace AvansTentamenManager
{
    public class TestManager
    {
        public ExamConfig config;

        public IEnumerable<Exam> Exams 
            { get {
                
                List<Exam> exams = new List<Exam>();
                using (var fs = new FileStream(config.zipFileName, FileMode.Open))
                {
                    using (var zip = new ZipArchive(fs))
                    {
                        foreach (var entry in zip.Entries)
                        {
                            if (entry.Name.EndsWith(".zip") || entry.Name.EndsWith(".rar"))
                            {
                                Exam exam = new Exam(config);

                                exam.name = entry.Name;
                                if (exam.name.Contains("_"))
                                    exam.name = exam.name.Substring(exam.name.IndexOf("_") + 1);
                                if (exam.name.Contains("_"))
                                    exam.name = exam.name.Substring(0, exam.name.IndexOf("_"));
                                exam.zipPath = entry.Name;
                                exams.Add(exam);
                            }
                        }
                    }
                }
                return exams;
            } }


        internal bool TestLibSetup()
        {
            return Directory.Exists(Path.Combine(config.outPath, "lib"));
        }

        internal bool TestTestSetup()
        {
            return Directory.Exists(Path.Combine(config.outPath, "tests"));
        }
    }
}
