namespace Bai05
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTitle;
        private Button btnRandom;
        private Button btnAdd;

        private TabControl tabControl;
        private TabPage tabAll;
        private TabPage tabMine;

        public FlowLayoutPanel flowAll;
        private FlowLayoutPanel flowMine;

        private Panel pnlStatus;
        private Label lblWelcome;
        private LinkLabel btnLogout;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnLogin = new Button();
            lblPasswordLogin = new Label();
            txtPasswordLogin = new TextBox();
            lblEmailLogin = new Label();
            txtEmailLogin = new TextBox();
            lblTitle = new Label();
            btnRandom = new Button();
            btnAdd = new Button();
            tabControl = new TabControl();
            tabAll = new TabPage();
            flowAll = new FlowLayoutPanel();
            tabMine = new TabPage();
            flowMine = new FlowLayoutPanel();
            pnlStatus = new Panel();
            lblWelcome = new Label();
            btnLogout = new LinkLabel();
            btnRefresh = new Button();
            pnlHeader.SuspendLayout();
            tabControl.SuspendLayout();
            tabAll.SuspendLayout();
            tabMine.SuspendLayout();
            pnlStatus.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.WhiteSmoke;
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Controls.Add(btnLogin);
            pnlHeader.Controls.Add(lblPasswordLogin);
            pnlHeader.Controls.Add(txtPasswordLogin);
            pnlHeader.Controls.Add(lblEmailLogin);
            pnlHeader.Controls.Add(txtEmailLogin);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnRandom);
            pnlHeader.Controls.Add(btnAdd);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(16, 12, 16, 12);
            pnlHeader.Size = new Size(1049, 200);
            pnlHeader.TabIndex = 2;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogin.BackColor = Color.Moccasin;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.Location = new Point(456, 96);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(150, 40);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblPasswordLogin
            // 
            lblPasswordLogin.AutoSize = true;
            lblPasswordLogin.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPasswordLogin.Location = new Point(38, 130);
            lblPasswordLogin.Name = "lblPasswordLogin";
            lblPasswordLogin.Size = new Size(64, 15);
            lblPasswordLogin.TabIndex = 6;
            lblPasswordLogin.Text = "Password:";
            // 
            // txtPasswordLogin
            // 
            txtPasswordLogin.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPasswordLogin.Location = new Point(127, 127);
            txtPasswordLogin.Name = "txtPasswordLogin";
            txtPasswordLogin.Size = new Size(308, 21);
            txtPasswordLogin.TabIndex = 5;
            txtPasswordLogin.UseSystemPasswordChar = true;
            // 
            // lblEmailLogin
            // 
            lblEmailLogin.AutoSize = true;
            lblEmailLogin.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmailLogin.Location = new Point(38, 86);
            lblEmailLogin.Name = "lblEmailLogin";
            lblEmailLogin.Size = new Size(42, 15);
            lblEmailLogin.TabIndex = 4;
            lblEmailLogin.Text = "Email:";
            // 
            // txtEmailLogin
            // 
            txtEmailLogin.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEmailLogin.Location = new Point(127, 83);
            txtEmailLogin.Name = "txtEmailLogin";
            txtEmailLogin.Size = new Size(308, 21);
            txtEmailLogin.TabIndex = 3;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Chocolate;
            lblTitle.Location = new Point(12, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(336, 51);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HÔM NAY ĂN GÌ?";
            // 
            // btnRandom
            // 
            btnRandom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRandom.BackColor = Color.Moccasin;
            btnRandom.FlatStyle = FlatStyle.Flat;
            btnRandom.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnRandom.Location = new Point(702, 145);
            btnRandom.Name = "btnRandom";
            btnRandom.Size = new Size(150, 40);
            btnRandom.TabIndex = 1;
            btnRandom.Text = "Ăn gì giờ?";
            btnRandom.UseVisualStyleBackColor = false;
            btnRandom.Click += btnRandom_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.BackColor = Color.Wheat;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAdd.Location = new Point(867, 145);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(170, 40);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Thêm món ăn";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabAll);
            tabControl.Controls.Add(tabMine);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.Location = new Point(0, 200);
            tabControl.Name = "tabControl";
            tabControl.Padding = new Point(12, 6);
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1049, 684);
            tabControl.TabIndex = 0;
            // 
            // tabAll
            // 
            tabAll.Controls.Add(flowAll);
            tabAll.Location = new Point(4, 32);
            tabAll.Name = "tabAll";
            tabAll.Size = new Size(1041, 648);
            tabAll.TabIndex = 0;
            tabAll.Text = "All";
            tabAll.UseVisualStyleBackColor = true;
            // 
            // flowAll
            // 
            flowAll.AutoScroll = true;
            flowAll.BackColor = Color.White;
            flowAll.Dock = DockStyle.Fill;
            flowAll.FlowDirection = FlowDirection.TopDown;
            flowAll.Location = new Point(0, 0);
            flowAll.Name = "flowAll";
            flowAll.Padding = new Padding(12);
            flowAll.Size = new Size(1041, 648);
            flowAll.TabIndex = 0;
            flowAll.WrapContents = false;
            // 
            // tabMine
            // 
            tabMine.Controls.Add(flowMine);
            tabMine.Location = new Point(4, 32);
            tabMine.Name = "tabMine";
            tabMine.Size = new Size(1041, 648);
            tabMine.TabIndex = 1;
            tabMine.Text = "Tôi đóng góp";
            tabMine.UseVisualStyleBackColor = true;
            // 
            // flowMine
            // 
            flowMine.AutoScroll = true;
            flowMine.BackColor = Color.White;
            flowMine.Dock = DockStyle.Fill;
            flowMine.FlowDirection = FlowDirection.TopDown;
            flowMine.Location = new Point(0, 0);
            flowMine.Name = "flowMine";
            flowMine.Padding = new Padding(12);
            flowMine.Size = new Size(1041, 648);
            flowMine.TabIndex = 0;
            flowMine.WrapContents = false;
            // 
            // pnlStatus
            // 
            pnlStatus.BackColor = Color.WhiteSmoke;
            pnlStatus.Controls.Add(lblWelcome);
            pnlStatus.Controls.Add(btnLogout);
            pnlStatus.Dock = DockStyle.Bottom;
            pnlStatus.Location = new Point(0, 836);
            pnlStatus.Name = "pnlStatus";
            pnlStatus.Padding = new Padding(12, 6, 12, 6);
            pnlStatus.Size = new Size(1049, 48);
            pnlStatus.TabIndex = 1;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 9.5F);
            lblWelcome.Location = new Point(14, 15);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(171, 17);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, (chưa đăng nhập)";
            // 
            // btnLogout
            // 
            btnLogout.AutoSize = true;
            btnLogout.Font = new Font("Segoe UI", 9.5F);
            btnLogout.Location = new Point(988, 15);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(49, 17);
            btnLogout.TabIndex = 1;
            btnLogout.TabStop = true;
            btnLogout.Text = "Logout";
            btnLogout.LinkClicked += btnLogout_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.Moccasin;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnRefresh.Location = new Point(12, 164);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(87, 30);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1049, 884);
            Controls.Add(pnlStatus);
            Controls.Add(tabControl);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1049, 884);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hôm nay ăn gì?";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tabControl.ResumeLayout(false);
            tabAll.ResumeLayout(false);
            tabMine.ResumeLayout(false);
            pnlStatus.ResumeLayout(false);
            pnlStatus.PerformLayout();
            ResumeLayout(false);
        }

        private TextBox txtEmailLogin;
        private Label lblEmailLogin;
        private Label lblPasswordLogin;
        private TextBox txtPasswordLogin;
        private Button btnLogin;
        private Button btnRefresh;
    }

        #endregion
    }
