namespace AvansTentamenManager
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ColumnHeader Foldernaam;
            System.Windows.Forms.ColumnHeader Getest;
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabOpenFolder = new System.Windows.Forms.TabPage();
            this.txtConfigFile = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnStep1Next = new System.Windows.Forms.Button();
            this.btnPickOutputPath = new System.Windows.Forms.Button();
            this.tabExamSettings = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.txtExcelFile = new System.Windows.Forms.TextBox();
            this.numOpenScore = new System.Windows.Forms.NumericUpDown();
            this.numOpenCount = new System.Windows.Forms.NumericUpDown();
            this.numMcScore = new System.Windows.Forms.NumericUpDown();
            this.numMcCount = new System.Windows.Forms.NumericUpDown();
            this.numTotalScore = new System.Windows.Forms.NumericUpDown();
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.btnStep2Next = new System.Windows.Forms.Button();
            this.btnPickTempPath = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSubjectName = new System.Windows.Forms.TextBox();
            this.txtSubjectCode = new System.Windows.Forms.TextBox();
            this.txtTempPath = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPickZipfile = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtZipFile = new System.Windows.Forms.TextBox();
            this.tabSetupTests = new System.Windows.Forms.TabPage();
            this.btnSetupTestNext = new System.Windows.Forms.Button();
            this.lblSetupTests = new System.Windows.Forms.Label();
            this.lblSetupLibs = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabRunTests = new System.Windows.Forms.TabPage();
            this.listTestUsers = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabCreateExcel = new System.Windows.Forms.TabPage();
            this.numStudentEmailCol = new System.Windows.Forms.NumericUpDown();
            this.numStudentLastNameCol = new System.Windows.Forms.NumericUpDown();
            this.numStudentFirstNameCol = new System.Windows.Forms.NumericUpDown();
            this.numStudentIdCol = new System.Windows.Forms.NumericUpDown();
            this.numHeaderRow = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.txtStudentTabName = new System.Windows.Forms.TextBox();
            this.txtOverviewTabName = new System.Windows.Forms.TextBox();
            this.btnAddOverview = new System.Windows.Forms.Button();
            this.txtOverrideTabName = new System.Windows.Forms.TextBox();
            this.btnManual = new System.Windows.Forms.Button();
            this.txtTheoryTabName = new System.Windows.Forms.TextBox();
            this.btnAddTheory = new System.Windows.Forms.Button();
            this.txtResultTabName = new System.Windows.Forms.TextBox();
            this.btnAddResults = new System.Windows.Forms.Button();
            this.btnImportStudentsFromBb = new System.Windows.Forms.Button();
            this.btnStudentsFromExternal = new System.Windows.Forms.Button();
            this.btnImportStudentListExcel = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabValidate = new System.Windows.Forms.TabPage();
            this.tabCreatePdf = new System.Windows.Forms.TabPage();
            this.btnGeneratePdfs = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabMailPdf = new System.Windows.Forms.TabPage();
            this.btnSendEmails = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.timerSetupTests = new System.Windows.Forms.Timer(this.components);
            Foldernaam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Getest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabOpenFolder.SuspendLayout();
            this.tabExamSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOpenScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOpenCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMcScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMcCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalScore)).BeginInit();
            this.tabSetupTests.SuspendLayout();
            this.tabRunTests.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabCreateExcel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStudentEmailCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStudentLastNameCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStudentFirstNameCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStudentIdCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeaderRow)).BeginInit();
            this.tabCreatePdf.SuspendLayout();
            this.tabMailPdf.SuspendLayout();
            this.SuspendLayout();
            // 
            // Foldernaam
            // 
            Foldernaam.Text = "Folder";
            Foldernaam.Width = 156;
            // 
            // Getest
            // 
            Getest.Text = "Tested";
            Getest.Width = 150;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Last Test";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabOpenFolder);
            this.tabControl1.Controls.Add(this.tabExamSettings);
            this.tabControl1.Controls.Add(this.tabSetupTests);
            this.tabControl1.Controls.Add(this.tabRunTests);
            this.tabControl1.Controls.Add(this.tabCreateExcel);
            this.tabControl1.Controls.Add(this.tabValidate);
            this.tabControl1.Controls.Add(this.tabCreatePdf);
            this.tabControl1.Controls.Add(this.tabMailPdf);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabOpenFolder
            // 
            this.tabOpenFolder.Controls.Add(this.txtConfigFile);
            this.tabOpenFolder.Controls.Add(this.label16);
            this.tabOpenFolder.Controls.Add(this.label6);
            this.tabOpenFolder.Controls.Add(this.txtOutputPath);
            this.tabOpenFolder.Controls.Add(this.btnStep1Next);
            this.tabOpenFolder.Controls.Add(this.btnPickOutputPath);
            this.tabOpenFolder.Location = new System.Drawing.Point(4, 22);
            this.tabOpenFolder.Name = "tabOpenFolder";
            this.tabOpenFolder.Padding = new System.Windows.Forms.Padding(3);
            this.tabOpenFolder.Size = new System.Drawing.Size(792, 424);
            this.tabOpenFolder.TabIndex = 0;
            this.tabOpenFolder.Text = "Open Folder";
            this.tabOpenFolder.UseVisualStyleBackColor = true;
            // 
            // txtConfigFile
            // 
            this.txtConfigFile.Location = new System.Drawing.Point(124, 48);
            this.txtConfigFile.Name = "txtConfigFile";
            this.txtConfigFile.ReadOnly = true;
            this.txtConfigFile.Size = new System.Drawing.Size(324, 20);
            this.txtConfigFile.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(22, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "Config File";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Output Path";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(124, 22);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(324, 20);
            this.txtOutputPath.TabIndex = 1;
            this.txtOutputPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // btnStep1Next
            // 
            this.btnStep1Next.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStep1Next.Location = new System.Drawing.Point(599, 379);
            this.btnStep1Next.Name = "btnStep1Next";
            this.btnStep1Next.Size = new System.Drawing.Size(185, 37);
            this.btnStep1Next.TabIndex = 0;
            this.btnStep1Next.Text = "Next";
            this.btnStep1Next.UseVisualStyleBackColor = true;
            this.btnStep1Next.Click += new System.EventHandler(this.btnStep1Next_Click);
            // 
            // btnPickOutputPath
            // 
            this.btnPickOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickOutputPath.Location = new System.Drawing.Point(481, 25);
            this.btnPickOutputPath.Name = "btnPickOutputPath";
            this.btnPickOutputPath.Size = new System.Drawing.Size(185, 37);
            this.btnPickOutputPath.TabIndex = 0;
            this.btnPickOutputPath.Text = "Open";
            this.btnPickOutputPath.UseVisualStyleBackColor = true;
            this.btnPickOutputPath.Click += new System.EventHandler(this.btnPickOutputPath_Click);
            // 
            // tabExamSettings
            // 
            this.tabExamSettings.Controls.Add(this.label17);
            this.tabExamSettings.Controls.Add(this.txtExcelFile);
            this.tabExamSettings.Controls.Add(this.numOpenScore);
            this.tabExamSettings.Controls.Add(this.numOpenCount);
            this.tabExamSettings.Controls.Add(this.numMcScore);
            this.tabExamSettings.Controls.Add(this.numMcCount);
            this.tabExamSettings.Controls.Add(this.numTotalScore);
            this.tabExamSettings.Controls.Add(this.txtLanguage);
            this.tabExamSettings.Controls.Add(this.btnStep2Next);
            this.tabExamSettings.Controls.Add(this.btnPickTempPath);
            this.tabExamSettings.Controls.Add(this.label7);
            this.tabExamSettings.Controls.Add(this.txtSubjectName);
            this.tabExamSettings.Controls.Add(this.txtSubjectCode);
            this.tabExamSettings.Controls.Add(this.txtTempPath);
            this.tabExamSettings.Controls.Add(this.label15);
            this.tabExamSettings.Controls.Add(this.label14);
            this.tabExamSettings.Controls.Add(this.label12);
            this.tabExamSettings.Controls.Add(this.label13);
            this.tabExamSettings.Controls.Add(this.label10);
            this.tabExamSettings.Controls.Add(this.label9);
            this.tabExamSettings.Controls.Add(this.btnPickZipfile);
            this.tabExamSettings.Controls.Add(this.label11);
            this.tabExamSettings.Controls.Add(this.label8);
            this.tabExamSettings.Controls.Add(this.label5);
            this.tabExamSettings.Controls.Add(this.txtZipFile);
            this.tabExamSettings.Location = new System.Drawing.Point(4, 22);
            this.tabExamSettings.Name = "tabExamSettings";
            this.tabExamSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabExamSettings.Size = new System.Drawing.Size(792, 424);
            this.tabExamSettings.TabIndex = 7;
            this.tabExamSettings.Text = "Exam Settings";
            this.tabExamSettings.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(47, 53);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "Excel File";
            // 
            // txtExcelFile
            // 
            this.txtExcelFile.Location = new System.Drawing.Point(300, 50);
            this.txtExcelFile.Name = "txtExcelFile";
            this.txtExcelFile.Size = new System.Drawing.Size(268, 20);
            this.txtExcelFile.TabIndex = 25;
            // 
            // numOpenScore
            // 
            this.numOpenScore.Location = new System.Drawing.Point(300, 323);
            this.numOpenScore.Name = "numOpenScore";
            this.numOpenScore.Size = new System.Drawing.Size(268, 20);
            this.numOpenScore.TabIndex = 24;
            // 
            // numOpenCount
            // 
            this.numOpenCount.Location = new System.Drawing.Point(300, 300);
            this.numOpenCount.Name = "numOpenCount";
            this.numOpenCount.Size = new System.Drawing.Size(268, 20);
            this.numOpenCount.TabIndex = 23;
            // 
            // numMcScore
            // 
            this.numMcScore.Location = new System.Drawing.Point(300, 266);
            this.numMcScore.Name = "numMcScore";
            this.numMcScore.Size = new System.Drawing.Size(268, 20);
            this.numMcScore.TabIndex = 22;
            // 
            // numMcCount
            // 
            this.numMcCount.Location = new System.Drawing.Point(300, 243);
            this.numMcCount.Name = "numMcCount";
            this.numMcCount.Size = new System.Drawing.Size(268, 20);
            this.numMcCount.TabIndex = 21;
            // 
            // numTotalScore
            // 
            this.numTotalScore.Location = new System.Drawing.Point(300, 210);
            this.numTotalScore.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numTotalScore.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTotalScore.Name = "numTotalScore";
            this.numTotalScore.Size = new System.Drawing.Size(268, 20);
            this.numTotalScore.TabIndex = 20;
            this.numTotalScore.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtLanguage
            // 
            this.txtLanguage.Location = new System.Drawing.Point(300, 357);
            this.txtLanguage.Name = "txtLanguage";
            this.txtLanguage.Size = new System.Drawing.Size(268, 20);
            this.txtLanguage.TabIndex = 18;
            this.txtLanguage.Text = "java";
            // 
            // btnStep2Next
            // 
            this.btnStep2Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStep2Next.Location = new System.Drawing.Point(599, 379);
            this.btnStep2Next.Name = "btnStep2Next";
            this.btnStep2Next.Size = new System.Drawing.Size(185, 37);
            this.btnStep2Next.TabIndex = 17;
            this.btnStep2Next.Text = "Next";
            this.btnStep2Next.UseVisualStyleBackColor = true;
            this.btnStep2Next.Click += new System.EventHandler(this.btnStep2Next_Click);
            // 
            // btnPickTempPath
            // 
            this.btnPickTempPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickTempPath.Location = new System.Drawing.Point(601, 65);
            this.btnPickTempPath.Name = "btnPickTempPath";
            this.btnPickTempPath.Size = new System.Drawing.Size(185, 37);
            this.btnPickTempPath.TabIndex = 16;
            this.btnPickTempPath.Text = "Pick";
            this.btnPickTempPath.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Temporary Path";
            // 
            // txtSubjectName
            // 
            this.txtSubjectName.Location = new System.Drawing.Point(300, 152);
            this.txtSubjectName.Name = "txtSubjectName";
            this.txtSubjectName.Size = new System.Drawing.Size(268, 20);
            this.txtSubjectName.TabIndex = 14;
            // 
            // txtSubjectCode
            // 
            this.txtSubjectCode.Location = new System.Drawing.Point(300, 129);
            this.txtSubjectCode.Name = "txtSubjectCode";
            this.txtSubjectCode.Size = new System.Drawing.Size(268, 20);
            this.txtSubjectCode.TabIndex = 14;
            // 
            // txtTempPath
            // 
            this.txtTempPath.Location = new System.Drawing.Point(300, 74);
            this.txtTempPath.Name = "txtTempPath";
            this.txtTempPath.Size = new System.Drawing.Size(268, 20);
            this.txtTempPath.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(47, 364);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Code Language";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(47, 325);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(123, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "Score per open question";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(47, 268);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Score per MC question";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(47, 302);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Number of open questions";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 245);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Number of MC questions";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(47, 212);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Total score";
            // 
            // btnPickZipfile
            // 
            this.btnPickZipfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickZipfile.Location = new System.Drawing.Point(601, 22);
            this.btnPickZipfile.Name = "btnPickZipfile";
            this.btnPickZipfile.Size = new System.Drawing.Size(185, 37);
            this.btnPickZipfile.TabIndex = 11;
            this.btnPickZipfile.Text = "Open";
            this.btnPickZipfile.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(47, 159);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Subject name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Subject code";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Zipfile";
            // 
            // txtZipFile
            // 
            this.txtZipFile.Location = new System.Drawing.Point(300, 31);
            this.txtZipFile.Name = "txtZipFile";
            this.txtZipFile.Size = new System.Drawing.Size(268, 20);
            this.txtZipFile.TabIndex = 9;
            // 
            // tabSetupTests
            // 
            this.tabSetupTests.Controls.Add(this.btnSetupTestNext);
            this.tabSetupTests.Controls.Add(this.lblSetupTests);
            this.tabSetupTests.Controls.Add(this.lblSetupLibs);
            this.tabSetupTests.Controls.Add(this.label2);
            this.tabSetupTests.Controls.Add(this.label1);
            this.tabSetupTests.Location = new System.Drawing.Point(4, 22);
            this.tabSetupTests.Name = "tabSetupTests";
            this.tabSetupTests.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetupTests.Size = new System.Drawing.Size(792, 424);
            this.tabSetupTests.TabIndex = 6;
            this.tabSetupTests.Text = "Setup Tests";
            this.tabSetupTests.UseVisualStyleBackColor = true;
            // 
            // btnSetupTestNext
            // 
            this.btnSetupTestNext.Enabled = false;
            this.btnSetupTestNext.Location = new System.Drawing.Point(709, 393);
            this.btnSetupTestNext.Name = "btnSetupTestNext";
            this.btnSetupTestNext.Size = new System.Drawing.Size(75, 23);
            this.btnSetupTestNext.TabIndex = 2;
            this.btnSetupTestNext.Text = "Next";
            this.btnSetupTestNext.UseVisualStyleBackColor = true;
            this.btnSetupTestNext.Click += new System.EventHandler(this.btnSetupTestNext_Click);
            // 
            // lblSetupTests
            // 
            this.lblSetupTests.AutoSize = true;
            this.lblSetupTests.Location = new System.Drawing.Point(239, 83);
            this.lblSetupTests.Name = "lblSetupTests";
            this.lblSetupTests.Size = new System.Drawing.Size(0, 13);
            this.lblSetupTests.TabIndex = 1;
            // 
            // lblSetupLibs
            // 
            this.lblSetupLibs.AutoSize = true;
            this.lblSetupLibs.Location = new System.Drawing.Point(239, 40);
            this.lblSetupLibs.Name = "lblSetupLibs";
            this.lblSetupLibs.Size = new System.Drawing.Size(0, 13);
            this.lblSetupLibs.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tests";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Libraries";
            // 
            // tabRunTests
            // 
            this.tabRunTests.Controls.Add(this.listTestUsers);
            this.tabRunTests.Controls.Add(this.toolStrip1);
            this.tabRunTests.Location = new System.Drawing.Point(4, 22);
            this.tabRunTests.Name = "tabRunTests";
            this.tabRunTests.Padding = new System.Windows.Forms.Padding(3);
            this.tabRunTests.Size = new System.Drawing.Size(792, 424);
            this.tabRunTests.TabIndex = 1;
            this.tabRunTests.Text = "Run Tests";
            // 
            // listTestUsers
            // 
            this.listTestUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            Foldernaam,
            Getest,
            columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listTestUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTestUsers.Location = new System.Drawing.Point(3, 28);
            this.listTestUsers.Name = "listTestUsers";
            this.listTestUsers.Size = new System.Drawing.Size(786, 393);
            this.listTestUsers.TabIndex = 0;
            this.listTestUsers.UseCompatibleStateImageBehavior = false;
            this.listTestUsers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Test Score";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Compile errors";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Student Number";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(786, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(74, 22);
            this.toolStripButton2.Text = "Run all tests";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(88, 22);
            this.toolStripButton1.Text = "Run single test";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tabCreateExcel
            // 
            this.tabCreateExcel.Controls.Add(this.numStudentEmailCol);
            this.tabCreateExcel.Controls.Add(this.numStudentLastNameCol);
            this.tabCreateExcel.Controls.Add(this.numStudentFirstNameCol);
            this.tabCreateExcel.Controls.Add(this.numStudentIdCol);
            this.tabCreateExcel.Controls.Add(this.numHeaderRow);
            this.tabCreateExcel.Controls.Add(this.label22);
            this.tabCreateExcel.Controls.Add(this.txtStudentTabName);
            this.tabCreateExcel.Controls.Add(this.txtOverviewTabName);
            this.tabCreateExcel.Controls.Add(this.btnAddOverview);
            this.tabCreateExcel.Controls.Add(this.txtOverrideTabName);
            this.tabCreateExcel.Controls.Add(this.btnManual);
            this.tabCreateExcel.Controls.Add(this.txtTheoryTabName);
            this.tabCreateExcel.Controls.Add(this.btnAddTheory);
            this.tabCreateExcel.Controls.Add(this.txtResultTabName);
            this.tabCreateExcel.Controls.Add(this.btnAddResults);
            this.tabCreateExcel.Controls.Add(this.btnImportStudentsFromBb);
            this.tabCreateExcel.Controls.Add(this.btnStudentsFromExternal);
            this.tabCreateExcel.Controls.Add(this.btnImportStudentListExcel);
            this.tabCreateExcel.Controls.Add(this.label21);
            this.tabCreateExcel.Controls.Add(this.label20);
            this.tabCreateExcel.Controls.Add(this.label19);
            this.tabCreateExcel.Controls.Add(this.label18);
            this.tabCreateExcel.Controls.Add(this.label4);
            this.tabCreateExcel.Controls.Add(this.label3);
            this.tabCreateExcel.Location = new System.Drawing.Point(4, 22);
            this.tabCreateExcel.Name = "tabCreateExcel";
            this.tabCreateExcel.Size = new System.Drawing.Size(792, 424);
            this.tabCreateExcel.TabIndex = 2;
            this.tabCreateExcel.Text = "Create Excel file";
            this.tabCreateExcel.UseVisualStyleBackColor = true;
            // 
            // numStudentEmailCol
            // 
            this.numStudentEmailCol.Location = new System.Drawing.Point(270, 177);
            this.numStudentEmailCol.Name = "numStudentEmailCol";
            this.numStudentEmailCol.Size = new System.Drawing.Size(318, 20);
            this.numStudentEmailCol.TabIndex = 15;
            // 
            // numStudentLastNameCol
            // 
            this.numStudentLastNameCol.Location = new System.Drawing.Point(270, 151);
            this.numStudentLastNameCol.Name = "numStudentLastNameCol";
            this.numStudentLastNameCol.Size = new System.Drawing.Size(318, 20);
            this.numStudentLastNameCol.TabIndex = 15;
            // 
            // numStudentFirstNameCol
            // 
            this.numStudentFirstNameCol.Location = new System.Drawing.Point(270, 128);
            this.numStudentFirstNameCol.Name = "numStudentFirstNameCol";
            this.numStudentFirstNameCol.Size = new System.Drawing.Size(318, 20);
            this.numStudentFirstNameCol.TabIndex = 15;
            // 
            // numStudentIdCol
            // 
            this.numStudentIdCol.Location = new System.Drawing.Point(270, 104);
            this.numStudentIdCol.Name = "numStudentIdCol";
            this.numStudentIdCol.Size = new System.Drawing.Size(318, 20);
            this.numStudentIdCol.TabIndex = 15;
            // 
            // numHeaderRow
            // 
            this.numHeaderRow.Location = new System.Drawing.Point(270, 81);
            this.numHeaderRow.Name = "numHeaderRow";
            this.numHeaderRow.Size = new System.Drawing.Size(318, 20);
            this.numHeaderRow.TabIndex = 15;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(25, 177);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(108, 13);
            this.label22.TabIndex = 14;
            this.label22.Text = "Student email column";
            // 
            // txtStudentTabName
            // 
            this.txtStudentTabName.Location = new System.Drawing.Point(270, 54);
            this.txtStudentTabName.Name = "txtStudentTabName";
            this.txtStudentTabName.Size = new System.Drawing.Size(318, 20);
            this.txtStudentTabName.TabIndex = 13;
            // 
            // txtOverviewTabName
            // 
            this.txtOverviewTabName.Location = new System.Drawing.Point(270, 312);
            this.txtOverviewTabName.Name = "txtOverviewTabName";
            this.txtOverviewTabName.Size = new System.Drawing.Size(156, 20);
            this.txtOverviewTabName.TabIndex = 12;
            this.txtOverviewTabName.Text = "Overview";
            // 
            // btnAddOverview
            // 
            this.btnAddOverview.Location = new System.Drawing.Point(432, 312);
            this.btnAddOverview.Name = "btnAddOverview";
            this.btnAddOverview.Size = new System.Drawing.Size(156, 23);
            this.btnAddOverview.TabIndex = 11;
            this.btnAddOverview.Text = "Add overview";
            this.btnAddOverview.UseVisualStyleBackColor = true;
            this.btnAddOverview.Click += new System.EventHandler(this.btnAddOverview_Click);
            // 
            // txtOverrideTabName
            // 
            this.txtOverrideTabName.Location = new System.Drawing.Point(270, 286);
            this.txtOverrideTabName.Name = "txtOverrideTabName";
            this.txtOverrideTabName.Size = new System.Drawing.Size(156, 20);
            this.txtOverrideTabName.TabIndex = 10;
            this.txtOverrideTabName.Text = "Override";
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(432, 286);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(156, 23);
            this.btnManual.TabIndex = 9;
            this.btnManual.Text = "Add Manual correction";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // txtTheoryTabName
            // 
            this.txtTheoryTabName.Location = new System.Drawing.Point(270, 257);
            this.txtTheoryTabName.Name = "txtTheoryTabName";
            this.txtTheoryTabName.Size = new System.Drawing.Size(156, 20);
            this.txtTheoryTabName.TabIndex = 8;
            this.txtTheoryTabName.Text = "Theory";
            // 
            // btnAddTheory
            // 
            this.btnAddTheory.Location = new System.Drawing.Point(432, 257);
            this.btnAddTheory.Name = "btnAddTheory";
            this.btnAddTheory.Size = new System.Drawing.Size(156, 23);
            this.btnAddTheory.TabIndex = 7;
            this.btnAddTheory.Text = "Add Theory";
            this.btnAddTheory.UseVisualStyleBackColor = true;
            this.btnAddTheory.Click += new System.EventHandler(this.btnAddTheory_Click);
            // 
            // txtResultTabName
            // 
            this.txtResultTabName.Location = new System.Drawing.Point(270, 231);
            this.txtResultTabName.Name = "txtResultTabName";
            this.txtResultTabName.Size = new System.Drawing.Size(156, 20);
            this.txtResultTabName.TabIndex = 6;
            this.txtResultTabName.Text = "TestResults";
            // 
            // btnAddResults
            // 
            this.btnAddResults.Location = new System.Drawing.Point(432, 231);
            this.btnAddResults.Name = "btnAddResults";
            this.btnAddResults.Size = new System.Drawing.Size(156, 23);
            this.btnAddResults.TabIndex = 5;
            this.btnAddResults.Text = "Add results";
            this.btnAddResults.UseVisualStyleBackColor = true;
            this.btnAddResults.Click += new System.EventHandler(this.btnAddResults_Click);
            // 
            // btnImportStudentsFromBb
            // 
            this.btnImportStudentsFromBb.Location = new System.Drawing.Point(594, 14);
            this.btnImportStudentsFromBb.Name = "btnImportStudentsFromBb";
            this.btnImportStudentsFromBb.Size = new System.Drawing.Size(156, 23);
            this.btnImportStudentsFromBb.TabIndex = 4;
            this.btnImportStudentsFromBb.Text = "From blackboard csv";
            this.btnImportStudentsFromBb.UseVisualStyleBackColor = true;
            this.btnImportStudentsFromBb.Click += new System.EventHandler(this.btnImportStudentsFromBb_Click);
            // 
            // btnStudentsFromExternal
            // 
            this.btnStudentsFromExternal.Location = new System.Drawing.Point(432, 14);
            this.btnStudentsFromExternal.Name = "btnStudentsFromExternal";
            this.btnStudentsFromExternal.Size = new System.Drawing.Size(156, 23);
            this.btnStudentsFromExternal.TabIndex = 4;
            this.btnStudentsFromExternal.Text = "From external Excel";
            this.btnStudentsFromExternal.UseVisualStyleBackColor = true;
            this.btnStudentsFromExternal.Click += new System.EventHandler(this.btnStudentsFromExternal_Click);
            // 
            // btnImportStudentListExcel
            // 
            this.btnImportStudentListExcel.Location = new System.Drawing.Point(270, 14);
            this.btnImportStudentListExcel.Name = "btnImportStudentListExcel";
            this.btnImportStudentListExcel.Size = new System.Drawing.Size(156, 23);
            this.btnImportStudentListExcel.TabIndex = 1;
            this.btnImportStudentListExcel.Text = "In existing Excel";
            this.btnImportStudentListExcel.UseVisualStyleBackColor = true;
            this.btnImportStudentListExcel.Click += new System.EventHandler(this.btnImportStudentListExcel_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(25, 153);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(129, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Student last name column";
            this.label21.Click += new System.EventHandler(this.label3_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(25, 130);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(129, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Student first name column";
            this.label20.Click += new System.EventHandler(this.label3_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(25, 106);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Student ID column";
            this.label19.Click += new System.EventHandler(this.label3_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(25, 84);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Header Row";
            this.label18.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Student Sheet Name";
            this.label4.Click += new System.EventHandler(this.label3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Import student list";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // tabValidate
            // 
            this.tabValidate.Location = new System.Drawing.Point(4, 22);
            this.tabValidate.Name = "tabValidate";
            this.tabValidate.Size = new System.Drawing.Size(792, 424);
            this.tabValidate.TabIndex = 3;
            this.tabValidate.Text = "Validate Excel Results";
            this.tabValidate.UseVisualStyleBackColor = true;
            // 
            // tabCreatePdf
            // 
            this.tabCreatePdf.Controls.Add(this.btnGeneratePdfs);
            this.tabCreatePdf.Controls.Add(this.button4);
            this.tabCreatePdf.Controls.Add(this.button1);
            this.tabCreatePdf.Location = new System.Drawing.Point(4, 22);
            this.tabCreatePdf.Name = "tabCreatePdf";
            this.tabCreatePdf.Size = new System.Drawing.Size(792, 424);
            this.tabCreatePdf.TabIndex = 4;
            this.tabCreatePdf.Text = "Create PDFs";
            this.tabCreatePdf.UseVisualStyleBackColor = true;
            // 
            // btnGeneratePdfs
            // 
            this.btnGeneratePdfs.Location = new System.Drawing.Point(206, 76);
            this.btnGeneratePdfs.Name = "btnGeneratePdfs";
            this.btnGeneratePdfs.Size = new System.Drawing.Size(232, 23);
            this.btnGeneratePdfs.TabIndex = 3;
            this.btnGeneratePdfs.Text = "Generate";
            this.btnGeneratePdfs.UseVisualStyleBackColor = true;
            this.btnGeneratePdfs.Click += new System.EventHandler(this.BtnGeneratePdfs_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(206, 47);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(232, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Select latex template";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(206, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select excel file";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabMailPdf
            // 
            this.tabMailPdf.Controls.Add(this.btnSendEmails);
            this.tabMailPdf.Controls.Add(this.button5);
            this.tabMailPdf.Location = new System.Drawing.Point(4, 22);
            this.tabMailPdf.Name = "tabMailPdf";
            this.tabMailPdf.Size = new System.Drawing.Size(792, 424);
            this.tabMailPdf.TabIndex = 5;
            this.tabMailPdf.Text = "Send pdf in mail";
            this.tabMailPdf.UseVisualStyleBackColor = true;
            // 
            // btnSendEmails
            // 
            this.btnSendEmails.Location = new System.Drawing.Point(127, 97);
            this.btnSendEmails.Name = "btnSendEmails";
            this.btnSendEmails.Size = new System.Drawing.Size(232, 23);
            this.btnSendEmails.TabIndex = 4;
            this.btnSendEmails.Text = "Send Emails";
            this.btnSendEmails.UseVisualStyleBackColor = true;
            this.btnSendEmails.Click += new System.EventHandler(this.BtnSendEmails_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(127, 13);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(232, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "Select excel file";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // timerSetupTests
            // 
            this.timerSetupTests.Interval = 1000;
            this.timerSetupTests.Tick += new System.EventHandler(this.timerSetupTests_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainWindow";
            this.Text = "TentamenManager";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabOpenFolder.ResumeLayout(false);
            this.tabOpenFolder.PerformLayout();
            this.tabExamSettings.ResumeLayout(false);
            this.tabExamSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOpenScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOpenCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMcScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMcCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalScore)).EndInit();
            this.tabSetupTests.ResumeLayout(false);
            this.tabSetupTests.PerformLayout();
            this.tabRunTests.ResumeLayout(false);
            this.tabRunTests.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabCreateExcel.ResumeLayout(false);
            this.tabCreateExcel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStudentEmailCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStudentLastNameCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStudentFirstNameCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStudentIdCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeaderRow)).EndInit();
            this.tabCreatePdf.ResumeLayout(false);
            this.tabMailPdf.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabOpenFolder;
        private System.Windows.Forms.TabPage tabRunTests;
        private System.Windows.Forms.TabPage tabCreateExcel;
        private System.Windows.Forms.TabPage tabValidate;
        private System.Windows.Forms.TabPage tabCreatePdf;
        private System.Windows.Forms.TabPage tabMailPdf;
        private System.Windows.Forms.Button btnPickOutputPath;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.TabPage tabSetupTests;
        private System.Windows.Forms.Label lblSetupTests;
        private System.Windows.Forms.Label lblSetupLibs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerSetupTests;
        private System.Windows.Forms.Button btnSetupTestNext;
        private System.Windows.Forms.ListView listTestUsers;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox txtOverrideTabName;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.TextBox txtTheoryTabName;
        private System.Windows.Forms.Button btnAddTheory;
        private System.Windows.Forms.TextBox txtResultTabName;
        private System.Windows.Forms.Button btnAddResults;
        private System.Windows.Forms.Button btnStudentsFromExternal;
        private System.Windows.Forms.Button btnImportStudentListExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOverviewTabName;
        private System.Windows.Forms.Button btnAddOverview;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGeneratePdfs;
        private System.Windows.Forms.Button btnSendEmails;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtConfigFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabExamSettings;
        private System.Windows.Forms.Button btnPickZipfile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtZipFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnPickTempPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTempPath;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnStep1Next;
        private System.Windows.Forms.Button btnStep2Next;
        private System.Windows.Forms.TextBox txtSubjectName;
        private System.Windows.Forms.TextBox txtSubjectCode;
        private System.Windows.Forms.TextBox txtLanguage;
        private System.Windows.Forms.NumericUpDown numOpenScore;
        private System.Windows.Forms.NumericUpDown numOpenCount;
        private System.Windows.Forms.NumericUpDown numMcScore;
        private System.Windows.Forms.NumericUpDown numMcCount;
        private System.Windows.Forms.NumericUpDown numTotalScore;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtExcelFile;
        private System.Windows.Forms.TextBox txtStudentTabName;
        private System.Windows.Forms.NumericUpDown numStudentEmailCol;
        private System.Windows.Forms.NumericUpDown numStudentLastNameCol;
        private System.Windows.Forms.NumericUpDown numStudentFirstNameCol;
        private System.Windows.Forms.NumericUpDown numStudentIdCol;
        private System.Windows.Forms.NumericUpDown numHeaderRow;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnImportStudentsFromBb;
    }
}

