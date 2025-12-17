using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;


namespace Bai1
{
    public partial class Bai1 : Form
    {
        public Bai1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string from = textBox1.Text.Trim();
                string to = textBox2.Text.Trim();
                string subject = textBox3.Text.Trim();
                string body = textBox4.Text;

                if (string.IsNullOrWhiteSpace(to))
                {
                    MessageBox.Show("To không được để trống");
                    return;
                }

                if (string.IsNullOrWhiteSpace(from))
                {
                    MessageBox.Show("From không được để trống");
                    return;
                }

                try
                {
                    var addr = new MailAddress(to);
                }
                catch
                {
                    MessageBox.Show("Email nhận không hợp lệ");
                    return;
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", from));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;

                message.Body = new TextPart("plain")
                {
                    Text = body
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    string smtpHost = "smtp.gmail.com";
                    int smtpPort = 587;

                    string username = "lengocanhnguyen855@gmail.com";
                    string password = "fjeoqvtsmcfaxeqo";

                    client.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                    client.Authenticate(username, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                MessageBox.Show("Gửi email thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi email:\n" + ex.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
