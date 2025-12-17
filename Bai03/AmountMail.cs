using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai03
{
    public partial class AmountMail : Form
    {
        public string InputText => txtInput.Text;

        public AmountMail(string title, string promptText, string defaultValue)
        {
            InitializeComponent();
            SetupDialog(title, promptText, defaultValue);
        }

        private void SetupDialog(string title, string promptText, string defaultValue)
        {
            this.Text = title;
            lblPrompt.Text = promptText;
            txtInput.Text = defaultValue;
        }

        public static string Show(string title, string promptText, string defaultValue)
        {
            using (var dialog = new AmountMail(title, promptText, defaultValue))
            {
                return dialog.ShowDialog() == DialogResult.OK ? dialog.InputText : null;
            }
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
