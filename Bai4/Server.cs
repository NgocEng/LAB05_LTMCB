using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Cần thiết để dùng .FirstOrDefault
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab03_Bai04
{
    public partial class ServerForm : Form
    {
        TcpListener listener;
        bool isRunning = false;
        List<TcpClient> clients = new List<TcpClient>();
        object clientsLock = new object();

        public ServerForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            // 1. Cài đặt Port mặc định
            if (string.IsNullOrEmpty(txtPort.Text)) txtPort.Text = "8080";

         
            InitHistoryGrid();
        }

        // --- HÀM KHỞI TẠO CỘT CHO BẢNG HISTORY ---
        private void InitHistoryGrid()
        {
          
            if (dvgHistory == null) return;

            dvgHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvgHistory.ColumnCount = 8; // Tổng cộng 8 cột

            dvgHistory.Columns[0].Name = "Thời gian";
            dvgHistory.Columns[1].Name = "Khách hàng";
            dvgHistory.Columns[2].Name = "Email";
            dvgHistory.Columns[3].Name = "Phim";
            dvgHistory.Columns[4].Name = "Phòng";
            dvgHistory.Columns[5].Name = "Ghế";
            dvgHistory.Columns[6].Name = "Giá vé";
            dvgHistory.Columns[7].Name = "Trạng thái"; 
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (isRunning) return;

            if (!int.TryParse(txtPort.Text, out int port))
            {
                MessageBox.Show("Port không hợp lệ!");
                return;
            }

            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                isRunning = true;

                Log("Server đã khởi động và đang chờ kết nối...");

              
                Thread t = new Thread(ListenLoop) { IsBackground = true };
                t.Start();

                btnListen.Enabled = false;
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở Port: " + ex.Message);
            }
        }

        private void ListenLoop()
        {
            try
            {
                while (isRunning)
                {
                    var client = listener.AcceptTcpClient();
                    lock (clientsLock) clients.Add(client);

                    Log("Có Client mới kết nối!");

                    // Tạo luồng xử lý riêng cho từng Client
                    Thread clientThread = new Thread(() => HandleClient(client)) { IsBackground = true };
                    clientThread.Start();
                }
            }
            catch (SocketException) { }
            catch (Exception ex) { Log("Lỗi ListenLoop: " + ex.Message); }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            try
            {
                var ns = client.GetStream();
                var sr = new System.IO.StreamReader(ns, Encoding.UTF8);
                var sw = new System.IO.StreamWriter(ns, Encoding.UTF8) { AutoFlush = true };

                // Gửi dữ liệu ban đầu cho khách
                SendMoviesToClient(sw);
                SendAllSeatsToClient(sw);

                string line;
                // Đọc liên tục dữ liệu từ Client gửi lên
                while ((line = sr.ReadLine()) != null)
                {
                    ProcessLine(line.Trim(), sw);
                }
            }
            catch (Exception)
            {
                // Client ngắt kết nối đột ngột thì bỏ qua lỗi
            }
            finally
            {
                try { client.Close(); } catch { }
                lock (clientsLock) clients.Remove(client);
                Log("Client đã ngắt kết nối.");
            }
        }

        // --- XỬ LÝ LỆNH TỪ CLIENT ---
        private void ProcessLine(string line, System.IO.StreamWriter sw)
        {
            Log("RCV: " + line);
            var parts = line.Split('|');
            var cmd = parts[0];

            if (cmd == "BOOK")
            {
                // Format: BOOK|Movie|Room|Seat|Name|Email
                if (parts.Length >= 6)
                {
                    var movieTitle = parts[1];
                    var room = parts[2];
                    var seat = parts[3];
                    var cname = parts[4];
                    var email = parts[5];

                    // 1. Lấy giá vé
                    decimal price = 0;
                    var movieInfo = TicketData.Movies.FirstOrDefault(m => m.Title == movieTitle);
                    if (movieInfo != null) price = movieInfo.BasePrice;

                    // 2. Kiểm tra Email hợp lệ đơn giản (phải có @ và dấu chấm)
                    bool isValidEmail = email.Contains("@") && email.Contains(".");

                    // 3. Biến lưu kết quả đặt vé
                    bool bookingSuccess = false;

                    if (isValidEmail)
                    {
                        // Chỉ thử đặt vé nếu Email đúng
                        bookingSuccess = TicketData.TryBook(room, seat);
                    }

                    // --- XỬ LÝ KẾT QUẢ ---
                    if (isValidEmail && bookingSuccess)
                    {
                        // TRƯỜNG HỢP: THÀNH CÔNG
                        sw.WriteLine($"BOOK_OK|{room}|{seat}");
                        Broadcast($"UPDATE|{room}|{seat}|booked");
                        Log($"OK: {cname} đặt ghế {seat}");

                        // Ghi lịch sử: Thành công (Màu Xanh)
                        AddToHistory(cname, email, movieTitle, room, seat, price, "Thành công");
                    }
                    else
                    {
                        // TRƯỜNG HỢP: THẤT BẠI (Do sai Email HOẶC Ghế đã bán)
                        string reason = !isValidEmail ? "fail_email" : "fail_seat";
                        sw.WriteLine($"BOOK_FAIL|{room}|{seat}|{reason}");

                        string logMsg = !isValidEmail ? $"Lỗi Email: {email}" : $"Ghế {seat} đã bán";
                        Log($"FAIL: {logMsg}");

                        // Ghi lịch sử: Thất bại (Màu Đỏ)
                        AddToHistory(cname, email, movieTitle, room, seat, price, "Thất bại");
                    }
                }
            }
            else if (cmd == "SEATS")
            {
                if (parts.Length >= 2 && TicketData.Rooms.ContainsKey(parts[1]))
                {
                    var r = TicketData.Rooms[parts[1]];
                    var list = new List<string>();
                    lock (TicketData.DataLock)
                        foreach (var s in r.Seats.Values) list.Add($"{s.Id}:{s.Status}");
                    sw.WriteLine("SEATS|" + parts[1] + "|" + string.Join(",", list));
                }
            }
            else if (cmd == "LIST")
            {
                SendMoviesToClient(sw);
                SendAllSeatsToClient(sw);
            }
        }

        // --- HÀM GHI LỊCH SỬ CÓ MÀU ---
        private void AddToHistory(string name, string email, string movie, string room, string seat, decimal price, string status)
        {
            // Đảm bảo chạy trên luồng giao diện chính
            if (dvgHistory.InvokeRequired)
            {
                dvgHistory.Invoke(new Action(() => AddToHistory(name, email, movie, room, seat, price, status)));
                return;
            }

            // Thêm dòng mới
            int index = dvgHistory.Rows.Add(
                DateTime.Now.ToString("dd/MM HH:mm"),
                name,
                email,
                movie,
                room,
                seat,
                price.ToString("N0") + " VND",
                status
            );

            // Tô màu chữ dựa trên trạng thái
            if (status == "Thành công")
            {
                dvgHistory.Rows[index].Cells[7].Style.ForeColor = Color.Green;
                dvgHistory.Rows[index].Cells[7].Style.Font = new Font(dvgHistory.Font, FontStyle.Bold);
            }
            else
            {
                dvgHistory.Rows[index].Cells[7].Style.ForeColor = Color.Red;
                dvgHistory.Rows[index].Cells[7].Style.Font = new Font(dvgHistory.Font, FontStyle.Italic);
            }

            // Tự động cuộn xuống dòng cuối cùng
            dvgHistory.FirstDisplayedScrollingRowIndex = index;
        }

        // --- CÁC HÀM HỖ TRỢ ---

        static void SendMoviesToClient(System.IO.StreamWriter sw)
        {
            var parts = new List<string>();
            foreach (var m in TicketData.Movies)
            {
                // Format: Title#Price#Rooms#PosterUrl
                parts.Add($"{m.Title}#{m.BasePrice}#{string.Join(",", m.Rooms)}#{m.PosterUrl}");
            }
            sw.WriteLine("MOVIES|" + string.Join(";", parts));
        }

        static void SendAllSeatsToClient(System.IO.StreamWriter sw)
        {
            lock (TicketData.DataLock)
            {
                foreach (var r in TicketData.Rooms.Values)
                {
                    var list = new List<string>();
                    foreach (var s in r.Seats.Values) list.Add($"{s.Id}:{s.Status}");
                    sw.WriteLine("SEATS|" + r.Name + "|" + string.Join(",", list));
                }
            }
        }

        void Broadcast(string msg)
        {
            lock (clientsLock)
            {
                foreach (var c in clients.ToArray())
                {
                    try
                    {
                        if (c.Connected)
                        {
                            var sw = new System.IO.StreamWriter(c.GetStream(), Encoding.UTF8) { AutoFlush = true };
                            sw.WriteLine(msg);
                        }
                    }
                    catch { }
                }
            }
        }

        void Log(string s)
        {
            if (dgvLog.InvokeRequired)
            {
                dgvLog.Invoke(new Action(() => Log(s)));
                return;
            }
            dgvLog.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), s);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isRunning = false;
            try { listener?.Stop(); } catch { }

            lock (clientsLock)
            {
                foreach (var c in clients) try { c.Close(); } catch { }
                clients.Clear();
            }

            btnListen.Enabled = true;
            btnStop.Enabled = false;
            Log("Server đã dừng.");
        }

        // Các sự kiện tự sinh (để tránh lỗi Designer)
        private void label1_Click(object sender, EventArgs e) { }
        private void txtPort_TextChanged(object sender, EventArgs e) { }
        private void dgvLog_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}