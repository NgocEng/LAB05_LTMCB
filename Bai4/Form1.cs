using Lab03_Bai04;

namespace Bai4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            ServerForm f = new ServerForm();
            f.Show();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            ClientForm f = new ClientForm();
            f.Show();
        }
    }
}
