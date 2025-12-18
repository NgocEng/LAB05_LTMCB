using System.Drawing;
using System.Windows.Forms;

namespace Lab03_Bai04
{
    partial class ClientForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.cmbMovie = new System.Windows.Forms.ComboBox();
            this.cmbRoom = new System.Windows.Forms.ComboBox();
            this.panelSeats = new System.Windows.Forms.Panel();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBook = new System.Windows.Forms.Button();
            this.txtSelected = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.btnCancelSelection = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox(); // MỚI
            this.picPoster = new System.Windows.Forms.PictureBox(); // MỚI
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label(); // Label cho Email
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(38, 10);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(120, 27);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "127.0.0.1";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(243, 10);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(60, 27);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "8080";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(340, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 28);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Kết nối";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(437, 10);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(80, 28);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Ngắt";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // cmbMovie
            // 
            this.cmbMovie.Location = new System.Drawing.Point(142, 68);
            this.cmbMovie.Name = "cmbMovie";
            this.cmbMovie.Size = new System.Drawing.Size(201, 28);
            this.cmbMovie.TabIndex = 4;
            this.cmbMovie.SelectedIndexChanged += new System.EventHandler(this.cmbMovie_SelectedIndexChanged);
            // 
            // cmbRoom
            // 
            this.cmbRoom.Location = new System.Drawing.Point(533, 68);
            this.cmbRoom.Name = "cmbRoom";
            this.cmbRoom.Size = new System.Drawing.Size(80, 28);
            this.cmbRoom.TabIndex = 5;
            this.cmbRoom.SelectedIndexChanged += new System.EventHandler(this.cmbRoom_SelectedIndexChanged);
            // 
            // panelSeats
            // 
            this.panelSeats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSeats.AutoScroll = true;
            this.panelSeats.Location = new System.Drawing.Point(12, 122);
            this.panelSeats.Name = "panelSeats";
            this.panelSeats.Size = new System.Drawing.Size(420, 240);
            this.panelSeats.TabIndex = 6;
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvLog.Location = new System.Drawing.Point(12, 380);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersWidth = 62;
            this.dgvLog.Size = new System.Drawing.Size(755, 140);
            this.dgvLog.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Thời gian";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Nội dung";
            this.dataGridViewTextBoxColumn2.Width = 500;
            // 
            // btnBook
            // 
            this.btnBook.Location = new System.Drawing.Point(450, 280); // Đẩy xuống một chút
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(100, 35);
            this.btnBook.TabIndex = 8;
            this.btnBook.Text = "Đặt vé";
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);
            // 
            // txtSelected
            // 
            this.txtSelected.Location = new System.Drawing.Point(450, 234);
            this.txtSelected.Name = "txtSelected";
            this.txtSelected.ReadOnly = true;
            this.txtSelected.Size = new System.Drawing.Size(200, 27);
            this.txtSelected.TabIndex = 9;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(450, 330);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(300, 36);
            this.lblTotal.TabIndex = 10;
            this.lblTotal.Text = "Tổng: 0 VNĐ";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(450, 132);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(200, 27);
            this.txtCustomer.TabIndex = 11;
            // 
            // txtEmail (MỚI)
            // 
            this.txtEmail.Location = new System.Drawing.Point(450, 182);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 27);
            this.txtEmail.TabIndex = 19;
            // 
            // picPoster (MỚI)
            // 
            this.picPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPoster.Location = new System.Drawing.Point(670, 68);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(120, 180); // Kích thước poster chuẩn
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoster.TabIndex = 20;
            this.picPoster.TabStop = false;
            // 
            // btnCancelSelection
            // 
            this.btnCancelSelection.Location = new System.Drawing.Point(560, 280);
            this.btnCancelSelection.Name = "btnCancelSelection";
            this.btnCancelSelection.Size = new System.Drawing.Size(100, 35);
            this.btnCancelSelection.TabIndex = 12;
            this.btnCancelSelection.Text = "Hủy chọn";
            this.btnCancelSelection.Click += new System.EventHandler(this.btnCancelSelection_Click);
            // 
            // Labels
            // 
            this.label1.AutoSize = true; this.label1.Location = new System.Drawing.Point(8, 15); this.label1.Text = "IP:";
            this.label2.AutoSize = true; this.label2.Location = new System.Drawing.Point(199, 15); this.label2.Text = "Port:";
            this.label3.AutoSize = true; this.label3.Location = new System.Drawing.Point(446, 109); this.label3.Text = "Họ Tên:";
            this.label4.AutoSize = true; this.label4.Location = new System.Drawing.Point(446, 211); this.label4.Text = "Ghế đặt:";
            this.label5.AutoSize = true; this.label5.Location = new System.Drawing.Point(8, 71); this.label5.Text = "Danh sách phim:";
            this.label6.AutoSize = true; this.label6.Location = new System.Drawing.Point(388, 71); this.label6.Text = "Danh sách phòng:";
            this.label7.AutoSize = true; this.label7.Location = new System.Drawing.Point(446, 160); this.label7.Text = "Email:"; // Label Email

            // 
            // ClientForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 545);
            this.Controls.Add(this.picPoster); // Thêm Poster
            this.Controls.Add(this.txtEmail);  // Thêm ô Email
            this.Controls.Add(this.label7);    // Thêm Label Email
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.cmbMovie);
            this.Controls.Add(this.cmbRoom);
            this.Controls.Add(this.panelSeats);
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.txtSelected);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.btnCancelSelection);
            this.Name = "ClientForm";
            this.Text = "Cinema Client - Booking & Email";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.ComboBox cmbMovie;
        private System.Windows.Forms.ComboBox cmbRoom;
        private System.Windows.Forms.Panel panelSeats;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.TextBox txtSelected;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Button btnCancelSelection;
        private System.Windows.Forms.TextBox txtEmail; // Ô nhập Email
        private System.Windows.Forms.PictureBox picPoster; // Ô hiển thị Poster
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}