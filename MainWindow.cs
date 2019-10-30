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
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AvansTentamenManager
{
    public partial class MainWindow : Form
    {
        TestManager manager = new TestManager();
        public const string checkbox = "✓";
        ExamConfig config = new ExamConfig();
        XSSFWorkbook excelFile;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            txtOutputPath.Text = "C:\\Users\\johan\\Avans Hogeschool\\AEI Technische Informatica - TI-1.1 TMTI-OGP0\\Studentenresultaten\\auto";
            //txtOutputPath.Text = "D:\\Avans OneDrive\\Avans Hogeschool\\AEI Technische Informatica - Documents\\TI-1.2\\TMTI-OGP1 Object Georienteerd programmeren 1\\Toetsing\\Hertentamen";
            //BtnGeneratePdfs_Click(null, null);
            //BtnSendEmails_Click(null, null);
            //Environment.Exit(0);

            while (tabControl1.TabCount > 1)
                tabControl1.TabPages.RemoveAt(1);

        }

        private void btnPickOutputPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = txtOutputPath.Text;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtOutputPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            txtConfigFile.Text = Path.Combine(txtOutputPath.Text, "config.yaml");
            config.outPath = txtOutputPath.Text;
        }

        private void btnStep1Next_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtConfigFile.Text))
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(new CamelCaseNamingConvention())
                    .Build();
                config = deserializer.Deserialize<ExamConfig>(File.ReadAllText(txtConfigFile.Text));
            }
            else
            {
                config.tempPath = Path.Combine(Path.GetTempPath(), "ATM");
                config.excelFileName = Path.Combine(txtOutputPath.Text, "Results.xlsx");
            }
            manager.config = config;
            config.outPath = txtOutputPath.Text;

            if(!tabControl1.TabPages.Contains(tabExamSettings))
                tabControl1.TabPages.Add(tabExamSettings);
            tabControl1.SelectedTab = tabExamSettings;

            txtZipFile.Text = config.zipFileName;
            txtExcelFile.Text = config.excelFileName;
            txtTempPath.Text = config.tempPath;
            txtSubjectCode.Text = config.subjectCode;
            txtSubjectName.Text = config.subjectName;
            numTotalScore.Value = config.scoreDivider;
            numMcCount.Value = config.mcCount;
            numMcScore.Value = config.mcScore;
            numOpenCount.Value = config.openCount;
            numOpenScore.Value = config.openScore;


            numHeaderRow.Value = config.rowIndex;
            numStudentIdCol.Value = config.idColumn;
            numStudentFirstNameCol.Value = config.firstNameColumn;
            numStudentLastNameCol.Value = config.lastNameColumn;
            numStudentEmailCol.Value = config.emailColumn;

            txtStudentTabName.Text = config.studentSheetName;
            txtResultTabName.Text = config.sheetTestResultName;
            txtTheoryTabName.Text = config.sheetTheoryName;
            txtOverrideTabName.Text = config.sheetOverrideName;
            txtOverviewTabName.Text = config.sheetOverviewName;
        }

        public void SaveConfig()
        {
            config.studentSheetName = txtStudentTabName.Text;
            config.rowIndex = (int)numHeaderRow.Value;
            config.idColumn = (int)numStudentIdCol.Value;
            config.firstNameColumn = (int)numStudentFirstNameCol.Value;
            config.lastNameColumn = (int)numStudentLastNameCol.Value;
            config.emailColumn = (int)numStudentEmailCol.Value;

            config.outPath = txtOutputPath.Text;
            config.zipFileName = txtZipFile.Text;
            config.excelFileName = txtExcelFile.Text;
            config.tempPath = txtTempPath.Text;
            config.subjectCode = txtSubjectCode.Text;
            config.subjectName = txtSubjectName.Text;
            config.scoreDivider = (int)numTotalScore.Value;
            config.mcCount = (int)numMcCount.Value;
            config.mcScore = (int)numMcScore.Value;
            config.openCount = (int)numOpenCount.Value;
            config.openScore = (int)numOpenScore.Value;
            var serializer = new SerializerBuilder()
                   .WithNamingConvention(new CamelCaseNamingConvention())
                   .Build();
            File.WriteAllText(txtConfigFile.Text, serializer.Serialize(config));
        }


        private void btnStep2Next_Click(object sender, EventArgs e)
        {
            SaveConfig();
            if (!tabControl1.TabPages.Contains(tabMailPdf))
            {
                tabControl1.TabPages.Add(tabSetupTests);
                tabControl1.TabPages.Add(tabCreateExcel);
                tabControl1.TabPages.Add(tabCreatePdf);
                tabControl1.TabPages.Add(tabMailPdf);
                tabControl1.SelectedTab = tabSetupTests;
            }
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


        TabPage lastTab = null;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabSetupTests)
                timerSetupTests.Start();
            else
                timerSetupTests.Stop();

            if(tabControl1.SelectedTab == tabCreateExcel)
            {
                txtStudentTabName.Text = config.studentSheetName;
                numHeaderRow.Value = config.rowIndex;
                numStudentIdCol.Value = config.idColumn;
                numStudentFirstNameCol.Value = config.firstNameColumn;
                numStudentLastNameCol.Value = config.lastNameColumn;
                numStudentEmailCol.Value = config.emailColumn;
            }

            if (lastTab == tabExamSettings && File.Exists(txtConfigFile.Text))
                SaveConfig();

            if (tabControl1.SelectedTab == tabRunTests)
                UpdateTestTab();

            lastTab = tabControl1.SelectedTab;
        }


        

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
