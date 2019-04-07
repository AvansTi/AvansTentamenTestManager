using System;
using System.Diagnostics;

namespace AvansTentamenManager2
{
    public class Exam
    {
        public string name;
        public string projectPath;

        internal void Test(TestManager manager)
        {
            if (projectPath == null || projectPath == "")
                return;
            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = "run.bat";
            process.StartInfo.WorkingDirectory = "TestRunners\\java";
            process.StartInfo.Arguments = "\"" + manager.Path.Replace("\\", "/") + "\" \"" + projectPath.Replace("\\", "/") + "\"";
            process.Start();
            process.WaitForExit();// Waits here for the process to exit.
        }
    }
}