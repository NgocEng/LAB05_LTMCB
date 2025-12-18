using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//tratrongtin
namespace Lab03_Bai04
{
    public partial class ClientForm : Form
    {
        TcpClient client;
        NetworkStream ns;
        Thread clientThread;
        List<string> selectedSeats = new List<string>();

        // Dictionary lưu đường dẫn Poster
        Dictionary<string, string> moviePosters = new Dictionary<string, string>();

        public ClientForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 
            KhoiTaoDuLieuGia();
        }

        private void KhoiTaoDuLieuGia()
        {
            // 1. Thêm Phim
            cmbMovie.Items.Add("Đào, phở và piano");
            cmbMovie.Items.Add("Mai");

            // 2. Cấu hình ảnh Poster (Đảm bảo file ảnh có trong bin/Debug)
            moviePosters.Add("Đào, phở và piano", "dao.jpg");
            moviePosters.Add("Mai", "mai.jpg");

            cmbMovie.SelectedIndex = 0;

            // 3. Thêm Phòng
            cmbRoom.Items.Add("1");
            cmbRoom.Items.Add("2");
            cmbRoom.SelectedIndex = 0;

            VeGhe();
        }

        // --- VẼ GHẾ VÀ GẮN SỰ KIỆN CLICK ---
        private void VeGhe()
        {
            panelSeats.Controls.Clear();
            selectedSeats.Clear();
            txtSelected.Text = "";
            lblTotal.Text = "Tổng: 0 VNĐ";

            int rows = 5;
            int cols = 5;
            int btnSize = 50;
            int gap = 10;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button btn = new Button();
                    string seatName = $"{(char)('A' + i)}{j + 1}";
                    btn.Text = seatName;
                    btn.Name = seatName; 
                    btn.Size = new Size(btnSize, btnSize);
                    btn.Location = new Point(j * (btnSize + gap) + 10, i * (btnSize + gap) + 10);
                    btn.BackColor = Color.White;

                    
                    btn.Click += BtnSeat_Click;

                    panelSeats.Controls.Add(btn);
                }
            }
        }

        // --- XỬ LÝ KHI CLICK VÀO GHẾ ---
        private void BtnSeat_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

           
            if (btn.BackColor == Color.Red)
            {
                MessageBox.Show($"Ghế {btn.Text} đã có người đặt!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        
            if (selectedSeats.Contains(btn.Text))
            {
                selectedSeats.Remove(btn.Text);
                btn.BackColor = Color.White;
            }
            else
            {
                selectedSeats.Add(btn.Text);
                btn.BackColor = Color.Yellow;
            }

           
            txtSelected.Text = string.Join(", ", selectedSeats);
            lblTotal.Text = $"Tổng: {selectedSeats.Count * 45000:N0} VNĐ";
        }

        // --- XỬ LÝ NÚT ĐẶT VÉ ---
        private void btnBook_Click(object sender, EventArgs e)
        {
            if (selectedSeats.Count == 0) return;
            if (string.IsNullOrEmpty(txtCustomer.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên và Email!");
                return;
            }
            if (ns == null)
            {
                MessageBox.Show("Chưa kết nối Server!");
                return;
            }

            string movie = cmbMovie.SelectedItem.ToString();
            string room = cmbRoom.SelectedItem.ToString();
            string name = txtCustomer.Text;
            string email = txtEmail.Text;

            try
            {
                // Gửi từng ghế lên Server
                foreach (var seat in selectedSeats)
                {
                    
                    string msg = $"BOOK|{movie}|{room}|{seat}|{name}|{email}\n";
                    byte[] data = Encoding.UTF8.GetBytes(msg);
                    ns.Write(data, 0, data.Length);
                    Thread.Sleep(50); // Nghỉ xíu để server kịp xử lý
                }

              
                string posterPath = moviePosters.ContainsKey(movie) ? moviePosters[movie] : "";
                string seatsStr = string.Join(", ", selectedSeats);
                new Thread(() => GuiEmailXacNhan(name, email, movie, seatsStr, posterPath)).Start();

              
                selectedSeats.Clear();
                txtSelected.Text = "";
                lblTotal.Text = "Tổng: 0 VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi dữ liệu: " + ex.Message);
            }
        }

        // --- NHẬN TIN NHẮN TỪ SERVER ---
        private void ReceiveData()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (client != null && client.Connected)
                {
                    int bytesRead = ns.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    
                        var commands = message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var cmd in commands)
                        {
                            ProcessMessage(cmd);
                        }
                    }
                }
            }
            catch { }
        }

        // --- XỬ LÝ LOGIC PHẢN HỒI ---
        private void ProcessMessage(string msg)
        {
            if (InvokeRequired) { Invoke(new Action(() => ProcessMessage(msg))); return; }

            var parts = msg.Split('|');
            var cmd = parts[0];

            if (cmd == "BOOK_OK")
            {
                string seat = parts[2];
                MessageBox.Show($"Đặt vé thành công ghế {seat}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateSeatStatus(seat, true); // Đổi màu đỏ
            }
            else if (cmd == "BOOK_FAIL")
            {
                string seat = parts[2];
                string reason = parts.Length > 3 ? parts[3] : "";

                if (reason == "fail_email")
                    MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show($"Ghế {seat} đã bị người khác đặt mất rồi!", "Chậm chân", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpdateSeatStatus(seat, true); // Đổi màu đỏ ngay
                }
            }
            else if (cmd == "UPDATE") // Server báo tin cho toàn bộ Client
            {
                // UPDATE|Room|Seat|booked
                string room = parts[1];
                string seat = parts[2];
                // Chỉ cập nhật nếu đúng phòng đang xem
                if (room == cmbRoom.SelectedItem.ToString())
                {
                    UpdateSeatStatus(seat, true);
                }
            }
            else if (cmd == "SEATS") // Load danh sách ghế ban đầu
            {
                // SEATS|Room|A1:booked,A2:Free...
                string roomRecv = parts[1];
                if (roomRecv == cmbRoom.SelectedItem.ToString() && parts.Length > 2)
                {
                    string[] list = parts[2].Split(',');
                    foreach (var item in list)
                    {
                        var info = item.Split(':'); // A1:booked
                        if (info.Length == 2)
                        {
                            bool isBooked = (info[1] == "booked" || info[1] == "Sold");
                            UpdateSeatStatus(info[0], isBooked);
                        }
                    }
                }
            }
        }

        // --- HÀM TÔ MÀU GHẾ ---
        private void UpdateSeatStatus(string seatName, bool isBooked)
        {
            foreach (Control c in panelSeats.Controls)
            {
                if (c is Button btn && btn.Text == seatName)
                {
                    if (isBooked)
                    {
                        btn.BackColor = Color.Red;
                        
                       
                    }
                    else
                    {
                        btn.BackColor = Color.White;
                    }

                  
                    if (isBooked && selectedSeats.Contains(seatName))
                    {
                        selectedSeats.Remove(seatName);
                        btn.BackColor = Color.Red;
                    }
                    break;
                }
            }
        }

        // --- HÀM GỬI EMAIL ---
        private void GuiEmailXacNhan(string hoTen, string emailKhach, string tenPhim, string ghe, string posterPath)
        {
            try
            {
                string fromEmail = "tratrongtin2000@gmail.com";
                string password = "ewra djms vctq qljs";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(emailKhach);
                mail.Subject = "Vé phim: " + tenPhim;
                mail.IsBodyHtml = true;
                mail.Body = $"<h1>Xác nhận đặt vé</h1><p>Khách hàng: {hoTen}</p><p>Phim: {tenPhim}</p><p>Ghế: {ghe}</p>";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential(fromEmail, password);
                smtp.Send(mail);
            }
            catch { }
        }

        // --- CÁC SỰ KIỆN KHÁC ---
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient();
                client.Connect(txtIP.Text, int.Parse(txtPort.Text));
                ns = client.GetStream();
                clientThread = new Thread(ReceiveData);
                clientThread.Start();
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                MessageBox.Show("Kết nối thành công!");

                // Gửi lệnh xin danh sách ghế ngay khi kết nối
                string room = cmbRoom.SelectedItem.ToString();
                byte[] data = Encoding.UTF8.GetBytes($"SEATS|{room}\n");
                ns.Write(data, 0, data.Length);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi kết nối: " + ex.Message); }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try { client.Close(); clientThread.Abort(); } catch { }
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void cmbMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            string movie = cmbMovie.SelectedItem.ToString();
            if (moviePosters.ContainsKey(movie) && File.Exists(moviePosters[movie]))
                picPoster.Image = Image.FromFile(moviePosters[movie]);
            else
                picPoster.Image = null;
            VeGhe();
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            VeGhe();
            // Nếu đã kết nối, xin lại dữ liệu ghế của phòng mới
            if (client != null && client.Connected)
            {
                string room = cmbRoom.SelectedItem.ToString();
                byte[] data = Encoding.UTF8.GetBytes($"SEATS|{room}\n");
                ns.Write(data, 0, data.Length);
            }
        }

        // Nút hủy chọn
        private void btnCancelSelection_Click(object sender, EventArgs e)
        {
            foreach (Control c in panelSeats.Controls)
            {
                if (c is Button btn && btn.BackColor == Color.Yellow) btn.BackColor = Color.White;
            }
            selectedSeats.Clear();
            txtSelected.Text = "";
            lblTotal.Text = "Tổng: 0 VNĐ";
        }

    }
}