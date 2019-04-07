using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansTentamenManager2
{
    public class TestManager
    {
        private List<ITestManagerEvents> handlers = new List<ITestManagerEvents>();
        private string _path;
        public string Path {
            get { return _path; }
            set {
                this._path = value;
                handlers.ForEach(h => h.OnPathSelected());
            }
        }

        public IEnumerable<Exam> Exams 
            { get {
                List<Exam> exams = new List<Exam>();
                var directories = Directory.GetDirectories(_path);

                foreach(var dir in directories)
                {
                    if (dir.EndsWith("tests") || dir.EndsWith("lib") || dir.EndsWith("finalOut") || dir.EndsWith("pdf"))
                        continue;
                    Exam exam = new Exam();

                    exam.name = dir.Substring(_path.Length + 1);
                    exam.name = exam.name.Substring(exam.name.IndexOf("_") + 1);
                    exam.name = exam.name.Substring(0, exam.name.IndexOf("_"));

                    try
                    {
                        var projectFiles = Directory.EnumerateFiles(dir, "*.iml", SearchOption.AllDirectories);
                        if (projectFiles.Count() > 0)
                        {
                            exam.projectPath = projectFiles.First();
                            exam.projectPath = exam.projectPath.Substring(0, exam.projectPath.LastIndexOf("\\") + 1);
                        }
                        exams.Add(exam);
                    } catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }



                return exams;
            } }

        internal void Register(ITestManagerEvents eventHandler)
        {
            handlers.Add(eventHandler);
        }

        internal bool TestLibSetup()
        {
            return Directory.Exists(this.Path + "\\lib");
        }

        internal bool TestTestSetup()
        {
            return Directory.Exists(this.Path + "\\tests");
        }
    }
}
