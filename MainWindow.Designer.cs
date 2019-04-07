namespace AvansTentamenManager2
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
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
            this.txtOverviewTabName = new System.Windows.Forms.TextBox();
            this.btnAddOverview = new System.Windows.Forms.Button();
            this.txtOverrideTabName = new System.Windows.Forms.TextBox();
            this.btnManual = new System.Windows.Forms.Button();
            this.txtTheoryTabName = new System.Windows.Forms.TextBox();
            this.btnAddTheory = new System.Windows.Forms.Button();
            this.txtResultTabName = new System.Windows.Forms.TextBox();
            this.btnAddResults = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectExportFile = new System.Windows.Forms.Button();
            this.btnImportStudentListExcel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabValidate = new System.Windows.Forms.TabPage();
            this.tabCreatePdf = new System.Windows.Forms.TabPage();
            this.tabMailPdf = new System.Windows.Forms.TabPage();
            this.timerSetupTests = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnGeneratePdfs = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnSendEmails = new System.Windows.Forms.Button();
            Foldernaam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Getest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabOpenFolder.SuspendLayout();
            this.tabSetupTests.SuspendLayout();
            this.tabRunTests.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabCreateExcel.SuspendLayout();
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
            this.tabOpenFolder.Controls.Add(this.txtPath);
            this.tabOpenFolder.Controls.Add(this.btnOpenFolder);
            this.tabOpenFolder.Location = new System.Drawing.Point(4, 22);
            this.tabOpenFolder.Name = "tabOpenFolder";
            this.tabOpenFolder.Padding = new System.Windows.Forms.Padding(3);
            this.tabOpenFolder.Size = new System.Drawing.Size(792, 424);
            this.tabOpenFolder.TabIndex = 0;
            this.tabOpenFolder.Text = "Open Folder";
            this.tabOpenFolder.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(50, 25);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(678, 20);
            this.txtPath.TabIndex = 1;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.Location = new System.Drawing.Point(50, 51);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(678, 319);
            this.btnOpenFolder.TabIndex = 0;
            this.btnOpenFolder.Text = "Open";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
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
            this.tabCreateExcel.Controls.Add(this.txtOverviewTabName);
            this.tabCreateExcel.Controls.Add(this.btnAddOverview);
            this.tabCreateExcel.Controls.Add(this.txtOverrideTabName);
            this.tabCreateExcel.Controls.Add(this.btnManual);
            this.tabCreateExcel.Controls.Add(this.txtTheoryTabName);
            this.tabCreateExcel.Controls.Add(this.btnAddTheory);
            this.tabCreateExcel.Controls.Add(this.txtResultTabName);
            this.tabCreateExcel.Controls.Add(this.btnAddResults);
            this.tabCreateExcel.Controls.Add(this.button3);
            this.tabCreateExcel.Controls.Add(this.button2);
            this.tabCreateExcel.Controls.Add(this.label4);
            this.tabCreateExcel.Controls.Add(this.btnSelectExportFile);
            this.tabCreateExcel.Controls.Add(this.btnImportStudentListExcel);
            this.tabCreateExcel.Controls.Add(this.label3);
            this.tabCreateExcel.Location = new System.Drawing.Point(4, 22);
            this.tabCreateExcel.Name = "tabCreateExcel";
            this.tabCreateExcel.Size = new System.Drawing.Size(792, 424);
            this.tabCreateExcel.TabIndex = 2;
            this.tabCreateExcel.Text = "Create Excel file";
            this.tabCreateExcel.UseVisualStyleBackColor = true;
            // 
            // txtOverviewTabName
            // 
            this.txtOverviewTabName.Location = new System.Drawing.Point(270, 255);
            this.txtOverviewTabName.Name = "txtOverviewTabName";
            this.txtOverviewTabName.Size = new System.Drawing.Size(156, 20);
            this.txtOverviewTabName.TabIndex = 12;
            this.txtOverviewTabName.Text = "Overview";
            // 
            // btnAddOverview
            // 
            this.btnAddOverview.Location = new System.Drawing.Point(432, 255);
            this.btnAddOverview.Name = "btnAddOverview";
            this.btnAddOverview.Size = new System.Drawing.Size(156, 23);
            this.btnAddOverview.TabIndex = 11;
            this.btnAddOverview.Text = "Add overview";
            this.btnAddOverview.UseVisualStyleBackColor = true;
            this.btnAddOverview.Click += new System.EventHandler(this.btnAddOverview_Click);
            // 
            // txtOverrideTabName
            // 
            this.txtOverrideTabName.Location = new System.Drawing.Point(270, 229);
            this.txtOverrideTabName.Name = "txtOverrideTabName";
            this.txtOverrideTabName.Size = new System.Drawing.Size(156, 20);
            this.txtOverrideTabName.TabIndex = 10;
            this.txtOverrideTabName.Text = "Override";
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(432, 229);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(156, 23);
            this.btnManual.TabIndex = 9;
            this.btnManual.Text = "Add Manual correction";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // txtTheoryTabName
            // 
            this.txtTheoryTabName.Location = new System.Drawing.Point(270, 200);
            this.txtTheoryTabName.Name = "txtTheoryTabName";
            this.txtTheoryTabName.Size = new System.Drawing.Size(156, 20);
            this.txtTheoryTabName.TabIndex = 8;
            this.txtTheoryTabName.Text = "Theory";
            // 
            // btnAddTheory
            // 
            this.btnAddTheory.Location = new System.Drawing.Point(432, 200);
            this.btnAddTheory.Name = "btnAddTheory";
            this.btnAddTheory.Size = new System.Drawing.Size(156, 23);
            this.btnAddTheory.TabIndex = 7;
            this.btnAddTheory.Text = "Add Theory";
            this.btnAddTheory.UseVisualStyleBackColor = true;
            this.btnAddTheory.Click += new System.EventHandler(this.btnAddTheory_Click);
            // 
            // txtResultTabName
            // 
            this.txtResultTabName.Location = new System.Drawing.Point(270, 174);
            this.txtResultTabName.Name = "txtResultTabName";
            this.txtResultTabName.Size = new System.Drawing.Size(156, 20);
            this.txtResultTabName.TabIndex = 6;
            this.txtResultTabName.Text = "TestResults";
            // 
            // btnAddResults
            // 
            this.btnAddResults.Location = new System.Drawing.Point(432, 174);
            this.btnAddResults.Name = "btnAddResults";
            this.btnAddResults.Size = new System.Drawing.Size(156, 23);
            this.btnAddResults.TabIndex = 5;
            this.btnAddResults.Text = "Add results";
            this.btnAddResults.UseVisualStyleBackColor = true;
            this.btnAddResults.Click += new System.EventHandler(this.btnAddResults_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(432, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(156, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "From external Excel";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(351, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "New File";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Select Excel file";
            // 
            // btnSelectExportFile
            // 
            this.btnSelectExportFile.Location = new System.Drawing.Point(270, 27);
            this.btnSelectExportFile.Name = "btnSelectExportFile";
            this.btnSelectExportFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectExportFile.TabIndex = 1;
            this.btnSelectExportFile.Text = "Select file";
            this.btnSelectExportFile.UseVisualStyleBackColor = true;
            this.btnSelectExportFile.Click += new System.EventHandler(this.btnSelectExportFile_Click);
            // 
            // btnImportStudentListExcel
            // 
            this.btnImportStudentListExcel.Location = new System.Drawing.Point(270, 68);
            this.btnImportStudentListExcel.Name = "btnImportStudentListExcel";
            this.btnImportStudentListExcel.Size = new System.Drawing.Size(156, 23);
            this.btnImportStudentListExcel.TabIndex = 1;
            this.btnImportStudentListExcel.Text = "In existing Excel";
            this.btnImportStudentListExcel.UseVisualStyleBackColor = true;
            this.btnImportStudentListExcel.Click += new System.EventHandler(this.btnImportStudentListExcel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 73);
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
            // timerSetupTests
            // 
            this.timerSetupTests.Interval = 1000;
            this.timerSetupTests.Tick += new System.EventHandler(this.timerSetupTests_Tick);
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(206, 47);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(232, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Select latex template";
            this.button4.UseVisualStyleBackColor = true;
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
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(127, 13);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(232, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "Select excel file";
            this.button5.UseVisualStyleBackColor = true;
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
            this.tabSetupTests.ResumeLayout(false);
            this.tabSetupTests.PerformLayout();
            this.tabRunTests.ResumeLayout(false);
            this.tabRunTests.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabCreateExcel.ResumeLayout(false);
            this.tabCreateExcel.PerformLayout();
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
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TextBox txtPath;
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectExportFile;
        private System.Windows.Forms.Button btnImportStudentListExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOverviewTabName;
        private System.Windows.Forms.Button btnAddOverview;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGeneratePdfs;
        private System.Windows.Forms.Button btnSendEmails;
        private System.Windows.Forms.Button button5;
    }
}

