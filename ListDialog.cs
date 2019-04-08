using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvansTentamenManager
{
    public partial class ListDialog : Form
    {
        public string Value { get { return listbox.Text; } }
        public ListDialog()
        {
            InitializeComponent();
        }

        public ListDialog(IEnumerable<string> items)
        {
            InitializeComponent();

            foreach (var s in items)
            {
                listbox.Items.Add(s);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public static string Pick(string title, IEnumerable<string> items)
        {
            using (var form = new ListDialog(items))
            {
                form.label.Text = title;
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                    return form.Value;
            }
            return "";
        }
        public static string Pick(string title, params string[] items)
        {
            using (var form = new ListDialog(items))
            {
                form.label.Text = title;
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                    return form.Value;
            }
            return "";
        }

        private void listbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(null, null);
        }
    }
}
