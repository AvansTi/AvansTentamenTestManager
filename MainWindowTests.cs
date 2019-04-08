using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvansTentamenManager
{
    partial class MainWindow
    {
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (listTestUsers.SelectedItems.Count == 0)
                return;

            Exam exam = (Exam)listTestUsers.SelectedItems[0].Tag;
            exam.Test(manager);
            UpdateTestTab();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listTestUsers.Items)
            {
                Exam exam = (Exam)item.Tag;
                exam.Test(manager);
            }
            UpdateTestTab();
        }


        private void UpdateTestTab()
        {
            var exams = manager.Exams;


            listTestUsers.Items.Clear();
            foreach (var exam in exams)
            {

                ListViewItem item = new ListViewItem(exam.name);

                bool isTested = exam.isTested;
                item.Tag = exam;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, isTested + ""));

                listTestUsers.Items.Add(item);

                if (isTested)
                {
                    JToken result = exam.lastResult;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, result["time"] + ""));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, result["test"]["score"] + ""));

                    bool compileError = false;
                    foreach (var c in result["compile"])
                        if (((string)c).Contains("Compile error"))
                            compileError = true;

                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, compileError + ""));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, result["studentid"] + ""));
                }
                else
                {
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "-"));
                }
            }

            listTestUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnSetupTestNext_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabSetupTests);
            tabControl1.TabPages.Add(tabRunTests);
            tabControl1.SelectedTab = tabRunTests;
        }

    }
}
