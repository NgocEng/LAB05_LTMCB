namespace Bai05
{
    partial class AddFoodForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTotal = new Label();
            txtTotal = new TextBox();
            lstviewMain = new ListView();
            Email = new ColumnHeader();
            From = new ColumnHeader();
            Time = new ColumnHeader();
            lblTitle = new Label();
            SuspendLayout();
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(36, 91);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(40, 15);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "Total: ";
            // 
            // txtTotal
            // 
            txtTotal.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotal.Location = new Point(84, 87);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(49, 23);
            txtTotal.TabIndex = 7;
            // 
            // lstviewMain
            // 
            lstviewMain.Columns.AddRange(new ColumnHeader[] { Email, From, Time });
            lstviewMain.Location = new Point(38, 133);
            lstviewMain.Name = "lstviewMain";
            lstviewMain.Size = new Size(608, 278);
            lstviewMain.TabIndex = 9;
            lstviewMain.UseCompatibleStateImageBehavior = false;
            lstviewMain.View = View.Details;
            lstviewMain.DoubleClick += lstviewMain_DoubleClick;
            // 
            // Email
            // 
            Email.Text = "Email";
            Email.Width = 324;
            // 
            // From
            // 
            From.Text = "From";
            From.Width = 214;
            // 
            // Time
            // 
            Time.Text = "Thời gian";
            Time.Width = 122;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Chocolate;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(624, 51);
            lblTitle.TabIndex = 10;
            lblTitle.Text = "HÃY CHỌN EMAIL ĐỂ THÊM MÓN";
            // 
            // AddFoodForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 422);
            Controls.Add(lblTitle);
            Controls.Add(lstviewMain);
            Controls.Add(txtTotal);
            Controls.Add(lblTotal);
            Name = "AddFoodForm";
            Text = "Bai2";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.ListView lstviewMain;
        private System.Windows.Forms.ColumnHeader Email;
        private System.Windows.Forms.ColumnHeader From;
        private System.Windows.Forms.ColumnHeader Time;
        private Label lblTitle;
    }
}

