using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;
using Org.BouncyCastle.Math.Field;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bai05
{
    public partial class AddFoodForm : Form
    {
        public AddFoodForm()
        {
            InitializeComponent();
            LoadEmails();
        }

        private async void LoadEmails()
        {
            try
            {
                string email = GlobalSession.Email;
                string password = GlobalSession.Password;
                lstviewMain.Items.Clear();

                using (var client = new ImapClient())
                {
                    client.Connect("imap.gmail.com", 993, true);
                    client.Authenticate(email, password);

                    var inbox = client.Inbox;
                    if(!inbox.IsOpen)
                        await inbox.OpenAsync(FolderAccess.ReadOnly);

                    int totalEmails = inbox.Count;
                    txtTotal.Text = totalEmails.ToString();
                    int emailsToLoad = Math.Min(50, totalEmails);
                    int startIndex = totalEmails - emailsToLoad;
                    int endIndex = totalEmails - 1;

                    var summaries = await inbox.FetchAsync(startIndex, endIndex,
                        MessageSummaryItems.Envelope | MessageSummaryItems.InternalDate);

                    int limit = Math.Min(50, inbox.Count);

                    foreach (var item in summaries.Reverse())
                    {
                        string from = item.Envelope.From.FirstOrDefault()?.ToString() ?? "(No sender)";
                        string subject = item.Envelope.Subject ?? "(No subject)";
                        string datetime = item.InternalDate.HasValue
                            ? item.InternalDate.Value.ToString("dd/MM/yyyy HH:mm")
                            : "(No date)";

                        ListViewItem lvItem = new ListViewItem(subject);
                        lvItem.SubItems.Add(from);
                        lvItem.SubItems.Add(datetime);
                        lstviewMain.Items.Add(lvItem);
                    }
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc mail:\n" + ex.Message);
            }
        }
        private async void lstviewMain_DoubleClick(object sender, EventArgs e)
        {
            if (lstviewMain.SelectedItems.Count == 0) return;

            var confirm = MessageBox.Show("Bạn có muốn thêm email này vào database?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                string email = GlobalSession.Email;
                string password = GlobalSession.Password;

                using (var client = new ImapClient())
                {
                    await client.ConnectAsync("imap.gmail.com", 993, true);
                    await client.AuthenticateAsync(email, password);

                    var inbox = client.Inbox;
                    if (!inbox.IsOpen)
                        await inbox.OpenAsync(FolderAccess.ReadOnly);

                    int selectedIndex = lstviewMain.SelectedItems[0].Index;
                    int emailIndex = inbox.Count - 1 - selectedIndex;

                    var message = await inbox.GetMessageAsync(emailIndex);

                    // Kiểm tra Subject
                    if (message.Subject != "Đóng góp món ăn")
                        throw new FormatException("Email không đúng định dạng yêu cầu.");

                    string contributor = (message.From != null) ? message.From.ToString() : "Người đóng góp ẩn danh"; // Nếu không có tên
                    string? messageId = message.MessageId ?? Guid.NewGuid().ToString();
                    DateTime processedAt = DateTime.Now;

                    // Parse 
                    string body = message.TextBody ?? "";
                    var lines = body.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    using (var conn = new SQLiteConnection("Data Source=testDatabase1;Version=3;"))
                    {
                        conn.Open();

                        // Thêm các giá trị vào ProcessedEmails để tránh trùng lặp
                        string sqlEmail = "INSERT OR IGNORE INTO ProcessedEmails (MessageId, ProcessedAt) VALUES (@MessageId, @ProcessedAt)";
                        using (var cmd = new SQLiteCommand(sqlEmail, conn))
                        {
                            cmd.Parameters.AddWithValue("@MessageId", messageId);
                            cmd.Parameters.AddWithValue("@ProcessedAt", processedAt);
                            cmd.ExecuteNonQuery();
                        }

                        // Thêm Các giá trị của món ăn
                        bool DaThemMonAn = false;

                        foreach (var line in lines)
                        {
                            var parts = line.Split(';');
                            if (parts.Length != 2) continue;

                            string dishName = parts[0].Trim();
                            string imageUrl = parts[1].Trim();

                            // Kiểm tra trùng lặp
                            string checkSql =   @"SELECT COUNT(*) FROM DishContributions 
                                                WHERE MessageId = @MessageId AND DishName = @DishName";
                            using (var checkCmd = new SQLiteCommand(checkSql, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@MessageId", messageId);
                                checkCmd.Parameters.AddWithValue("@DishName", dishName);

                                long count = (long)checkCmd.ExecuteScalar();
                                if (count > 0)
                                {
                                    MessageBox.Show($"Những món ăn này đã được thêm vào từ trước: {dishName}",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    continue; 
                                }
                            }

                            // Nếu chưa có thì thêm mới
                            string sqlDish =    @"INSERT INTO DishContributions 
                                                (MessageId, DishName, ImageUrl, ContributorName, CreatedAt) 
                                                VALUES (@MessageId, @DishName, @ImageUrl, @ContributorName, @CreatedAt)";
                            using (var cmd = new SQLiteCommand(sqlDish, conn))
                            {
                                cmd.Parameters.AddWithValue("@MessageId", messageId);
                                cmd.Parameters.AddWithValue("@DishName", dishName);
                                cmd.Parameters.AddWithValue("@ImageUrl", imageUrl);
                                cmd.Parameters.AddWithValue("@ContributorName", contributor);
                                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }

                            DaThemMonAn = true;
                            // Add vào FlowLayoutPanel của MainForm
                            Panel itemPanel = new Panel { Width = 250, Height = 100, Margin = new Padding(5), BorderStyle = BorderStyle.FixedSingle };
                            itemPanel.Controls.Add(new Label { Text = dishName, AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(5, 5) });
                            itemPanel.Controls.Add(new Label { Text = contributor, AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(5, 30) });
                            itemPanel.Controls.Add(new Label { Text = imageUrl, AutoSize = true, Location = new Point(5, 55) });

                            if (this.Owner is MainForm mainForm)
                            {
                                mainForm.flowAll.Controls.Add(itemPanel);
                            }
                        }
                        if(DaThemMonAn)
                        {
                            MessageBox.Show("Đã thêm món ăn thành công vào database.", "Thành công");
                        }
                        else
                        {
                            MessageBox.Show("Không có món ăn mới nào để thêm vào database.");
                        }
                            client.Disconnect(true);
                    }
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Email không đúng định dạng:\n" + fex.Message, "Lỗi định dạng");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý email:\n" + ex.Message, "Lỗi");
            }
        }
    }
}
