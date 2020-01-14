using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace AvansTentamenManager
{
    public class Exam
    {
        public string name;
        public string zipPath;

        private ExamConfig config;

        public bool isTested { get {
                return File.Exists(Path.Combine(config.outPath, name + ".json"));
            }
        }

        public JToken lastResult { get {
                var allResults = JArray.Parse(File.ReadAllText(Path.Combine(config.outPath, name + ".json")));
                return allResults.Last;
            }
        }

        public Exam(ExamConfig config)
        {
            this.config = config;
        }

        internal void Test(TestManager manager)
        {
            if (zipPath == null || zipPath == "")
                return;

            string path = Path.Combine(config.tempPath, name);
            createTempPath(path);
            extractProjectZip(path);
            if (File.Exists(Path.Combine(path, name + ".zip")))
                extractZip(path);
            else if (File.Exists(Path.Combine(path, name + ".rar")))
                extractRar(path);
            else
            {
                MessageBox.Show("Error, could not extract " + Path.Combine(path, name + ".rar"));
                return;
            }

            string projectDir = findProjectDir(path);
            if (projectDir == "")
            {
                MessageBox.Show("Error finding project directory for user " + name);
                return;
            }

            string project = "java12";

            if(File.Exists(Path.Combine(projectDir, ".idea/misc.xml")))
            {
                var doc = new XmlDocument();
                doc.Load(Path.Combine(projectDir, ".idea/misc.xml"));
                try
                {
                    if (doc.DocumentElement["component"].Attributes["languageLevel"].Value == "JDK_1_8")
                        project = "java8";
                }
                catch (Exception e) { }

            }



            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (project == "java8")
                startInfo.FileName = Path.Combine(Environment.CurrentDirectory, "TestRunners", "java", "run8.bat");
            else if(project == "java12")
                startInfo.FileName = Path.Combine(Environment.CurrentDirectory, "TestRunners", "java", "run12.bat");
            startInfo.WorkingDirectory = Path.Combine(Environment.CurrentDirectory, "TestRunners", "java");
            startInfo.Arguments = "\"" + config.outPath.Replace("\\", "/") + "\" \"" + projectDir.Replace("\\", "/") + "\"";
            startInfo.ErrorDialog = true;

            var process = Process.Start(startInfo);
            process.WaitForExit();// Waits here for the process to exit.


            File.Delete(Path.Combine(config.outPath, name + ".json"));
            try
            {
                File.Copy(Path.Combine(projectDir, "log.json"), Path.Combine(config.outPath, name + ".json"));
            }catch(Exception)
            { }

        }

        private string findProjectDir(string path)
        {
            var entries = Directory.GetFiles(path, "*.iml", SearchOption.AllDirectories);
            foreach(var entry in entries)
            {
                string dir = Path.GetDirectoryName(entry);
                if (Directory.Exists(Path.Combine(dir, "src")))
                    return dir;
            }
            return "";
        }

        private void createTempPath(string path)
        {
            while(Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }catch(Exception)
                {
                    MessageBox.Show("Error deleting temp path " + path);
                    Thread.Sleep(1000);
                }
                Thread.Sleep(100);
            }
            Directory.CreateDirectory(path);
        }

        private void extractProjectZip(string path)
        {
            using (var fs = new FileStream(config.zipFileName, FileMode.Open))
            {
                using (var zip = new ZipArchive(fs))
                {
                    var entry = zip.GetEntry(this.zipPath);
                    Stream stream = entry.Open();
                    using (FileStream outStream = new FileStream(Path.Combine(path, name + Path.GetExtension(entry.FullName)), FileMode.CreateNew))
                    {
                        stream.CopyTo(outStream);
                    }
                }
            }
        }
        private void extractZip(string path)
        {
            ImportWhiteList wl = ImportWhiteList.Instance;
            using (var fs = new FileStream(Path.Combine(path, name + ".zip"), FileMode.Open))
            {
                using (var zip = new ZipArchive(fs))
                {
                    foreach (var entry in zip.Entries)
                    {
                        if (entry.FullName.EndsWith("/"))
                            Directory.CreateDirectory(Path.Combine(path, entry.FullName));
                        else
                        {
                            var outFile = Path.Combine(path, entry.FullName);
                            if (!Directory.Exists(Path.GetDirectoryName(outFile)))
                                Directory.CreateDirectory(Path.GetDirectoryName(outFile));
                            try
                            {
                                using (FileStream outStream = new FileStream(Path.Combine(path, entry.FullName), FileMode.CreateNew))
                                {
                                    using (var stream = entry.Open())
                                    {
                                        if (entry.FullName.EndsWith(".java"))
                                        {
                                            using (var sr = new StreamReader(stream))
                                            {
                                                using (var sw = new StreamWriter(outStream))
                                                {
                                                    while (!sr.EndOfStream) {
                                                        string line = sr.ReadLine();
                                                        bool ignore = false;
                                                        if (line.Trim().StartsWith("import"))
                                                        {
                                                            string import = line;
                                                            import = import.Replace("\t", " ");
                                                            while (import.Contains("  "))
                                                                import = import.Replace("  ", " ");
                                                            import = import.Substring(import.IndexOf(" ") + 1);
                                                            if (!wl.IsWhiteListed(import))
                                                                ignore = true;
                                                        }
                                                        if(!ignore)
                                                            sw.WriteLine(line);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            stream.CopyTo(outStream);
                                        }
                                    }
                                }
                            }
                            catch (System.IO.DirectoryNotFoundException e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }
                }
            }
        }

        private void extractRar(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "c:\\program files\\winrar\\winrar.exe";
            startInfo.WorkingDirectory = path;
            startInfo.Arguments = "x \"" + Path.Combine(path, name + ".rar") + "\"";
            startInfo.ErrorDialog = true;

            var process = Process.Start(startInfo);
            process.WaitForExit();// Waits here for the process to exit.

        }
    }
}