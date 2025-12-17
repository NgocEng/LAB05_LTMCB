using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bai2
{
    public partial class Bai2 : Form
    {
        public Bai2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string email = textBox1.Text.Trim();
                string password = textBox2.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Email và Password không được để trống");
                    return;
                }

                listView1.Items.Clear();

                using (var client = new ImapClient())
                {
                    client.Connect("imap.gmail.com", 993, true);
                    client.Authenticate(email, password);

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    textBox3.Text = inbox.Count.ToString();
                    textBox4.Text = inbox.Recent.ToString();

                    int limit = Math.Min(20, inbox.Count);

                    for (int i = 0; i < limit; i++)
                    {
                        var message = inbox.GetMessage(i);

                        ListViewItem item = new ListViewItem(message.Subject);
                        item.SubItems.Add(message.From.ToString());
                        item.SubItems.Add(message.Date.LocalDateTime.ToString());

                        listView1.Items.Add(item);
                    }

                    client.Disconnect(true);
                }
            }
            catch (MailKit.Security.AuthenticationException)
            {
                MessageBox.Show("Sai email hoặc App Password");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc mail:\n" + ex.Message);
            }
        }
    }
}
