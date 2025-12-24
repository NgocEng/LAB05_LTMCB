using MailKit;
using MailKit.Net.Imap;
using System.Data.SQLite;
using System.Runtime.InteropServices.Marshalling;
using System.Windows.Forms;

namespace Bai05
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeDefaultValues();
        }
        private string connString = "Data Source=testDatabase1;Version=3;";
        private ImapClient? client;

        private void InitializeDefaultValues()
        {
            txtEmailLogin.Text = "leminhkhang950@gmail.com";
            lblWelcome.Text = "Vui lòng đăng nhập";
            btnLogout.Visible = false;
            btnAdd.Visible = false;
            btnRandom.Visible = false;
            btnRefresh.Visible = false;
            txtEmailLogin.ReadOnly = false;
            txtPasswordLogin.ReadOnly = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmailLogin.Text.Trim();
                string password = txtPasswordLogin.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Email và Password không được để trống");
                    return;
                }
                client = new ImapClient();
                client.Connect("imap.gmail.com", 993, true);
                client.Authenticate(email, password);

                GlobalSession.Email = email;
                GlobalSession.Password = password;
                MessageBox.Show("Đăng nhập thành công!");
                lblWelcome.Text = $"Chào mừng, {email}";
                btnLogout.Visible = true;
                btnRandom.Visible = true;
                btnAdd.Visible = true;
                btnRefresh.Visible = true;
                txtEmailLogin.ReadOnly = true;
                txtPasswordLogin.ReadOnly = true;
                LoadDisContributions();
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

        private void btnRefresh_Click(Object sender, EventArgs e)
        {
            LoadDisContributions();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                if (client != null && client.IsConnected)
                {
                    client.Disconnect(true);
                    client.Dispose();
                    client = null;
                }
                MessageBox.Show("Đăng xuất thành công !");
                txtEmailLogin.Clear();
                txtPasswordLogin.Clear();
                InitializeDefaultValues();
                flowAll.Controls.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng xuất:\n" + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddFoodForm addfoodform = new AddFoodForm();
            MessageBox.Show("Đang hiển thị 50 email gần nhất");
            addfoodform.Owner = this;
            addfoodform.ShowDialog();
        }
        private void LoadDisContributions()
        {
            flowAll.Controls.Clear();

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string sql = "SELECT DishName, ImageUrl, ContributorName FROM DishContributions";

                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Tạo Panel cho từng món ăn
                        Panel itemPanel = new Panel();
                        itemPanel.Width = 250;
                        itemPanel.Height = 100;
                        itemPanel.Margin = new Padding(5);
                        itemPanel.BorderStyle = BorderStyle.FixedSingle;

                        // Tên Món Ăn
                        Label lblDish = new Label();
                        lblDish.Text = reader["DishName"].ToString();
                        lblDish.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        lblDish.AutoSize = true;
                        lblDish.Location = new Point(5, 5);

                        // Người thêm
                        Label lblContributor = new Label();
                        lblContributor.Text = "By: " + reader["ContributorName"].ToString();
                        lblContributor.AutoSize = true;
                        lblContributor.Location = new Point(5, 30);

                        // URL Ảnh
                        Label lblImage = new Label();
                        lblImage.Text = reader["ImageUrl"].ToString();
                        lblImage.AutoSize = true;
                        lblImage.Location = new Point(5, 55);

                        // Thêm các Control vào Panel
                        itemPanel.Controls.Add(lblDish);
                        itemPanel.Controls.Add(lblContributor);
                        itemPanel.Controls.Add(lblImage);

                        // Thêm itemPanel vào flowAll
                        flowAll.Controls.Add(itemPanel);
                    }
                }
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            try
            {
                var dish = GetRandomDishFromSqlite();
                if (dish == null)
                {
                    MessageBox.Show("Không có dữ liệu món ăn.");
                    return;
                }
                flowAll.Controls.Clear();
                flowAll.Controls.Add(BuildDishPanel(dish));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy món random:\n" + ex.Message);
            }
        }

        private class DishDto
        {
            public int Id { get; set; }
            public string DishName { get; set; } = "";
            public string ImageUrl { get; set; } = "";
            public string ContributorName { get; set; } = "";
            public DateTime CreatedAt { get; set; }
        }

        private DishDto? GetRandomDishFromSqlite()
        {
            const string sql = @"
                SELECT Id, DishName, ImageUrl, ContributorName, CreatedAt
                FROM DishContributions
                ORDER BY RANDOM()
                LIMIT 1;";

            using (var conn = new SQLiteConnection(connString))
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new DishDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        DishName = reader.GetString(reader.GetOrdinal("DishName")),
                        ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                        ContributorName = reader.GetString(reader.GetOrdinal("ContributorName")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                    };
                }
            }
        }

        private Control BuildDishPanel(DishDto dish)
        {
            var panel = new Panel
            {
                Width = 300,
                Height = 140,
                Margin = new Padding(6),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblTitle = new Label
            {
                Text = dish.DishName,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(8, 8)
            };

            var lblBy = new Label
            {
                Text = $"By: {dish.ContributorName}",
                AutoSize = true,
                Location = new Point(8, 35)
            };

            var lblImage = new Label()
            {
                Text = dish.ImageUrl,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(5, 55)
            };

            /* Nếu muốn hiện ảnh thật từ URL
             var pic = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(8, 75),
                Size = new Size(280, 55)
            };
            try { pic.Load(dish.ImageUrl); } catch {  bỏ qua nếu URL không hợp lệ  }
            panel.Controls.Add(pic); */

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblBy);
            panel.Controls.Add(lblImage);
            return panel;
        }
    }
}
