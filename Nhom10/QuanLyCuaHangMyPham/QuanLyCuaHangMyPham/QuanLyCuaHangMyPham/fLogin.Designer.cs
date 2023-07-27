namespace QuanLyCuaHangMyPham
{
    partial class fLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPassword = new System.Windows.Forms.Label();
            this.borderTxtPassword = new QuanLyCuaHangMyPham.CustomDesign.RoundedPanel();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.borderTxtUsername = new QuanLyCuaHangMyPham.CustomDesign.RoundedPanel();
            this.tbxUsername = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.borderTxtPassword.SuspendLayout();
            this.borderTxtUsername.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Controls.Add(this.borderTxtPassword);
            this.panel1.Controls.Add(this.lblUsername);
            this.panel1.Controls.Add(this.borderTxtUsername);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Location = new System.Drawing.Point(14, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 568);
            this.panel1.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPassword.ForeColor = System.Drawing.Color.DimGray;
            this.lblPassword.Location = new System.Drawing.Point(120, 193);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(92, 18);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "MẬT KHẨU";
            // 
            // borderTxtPassword
            // 
            this.borderTxtPassword.BackColor = System.Drawing.Color.White;
            this.borderTxtPassword.BorderColor = System.Drawing.Color.DarkGray;
            this.borderTxtPassword.BorderRadius = 5;
            this.borderTxtPassword.BorderSize = 1;
            this.borderTxtPassword.Controls.Add(this.tbxPassword);
            this.borderTxtPassword.ForeColor = System.Drawing.Color.Black;
            this.borderTxtPassword.Location = new System.Drawing.Point(122, 217);
            this.borderTxtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.borderTxtPassword.Name = "borderTxtPassword";
            this.borderTxtPassword.Size = new System.Drawing.Size(286, 53);
            this.borderTxtPassword.TabIndex = 1;
            // 
            // tbxPassword
            // 
            this.tbxPassword.BackColor = System.Drawing.Color.White;
            this.tbxPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbxPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbxPassword.Location = new System.Drawing.Point(11, 15);
            this.tbxPassword.Margin = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.Size = new System.Drawing.Size(263, 22);
            this.tbxPassword.TabIndex = 2;
            this.tbxPassword.Text = "1";
            this.tbxPassword.UseSystemPasswordChar = true;
            this.tbxPassword.Enter += new System.EventHandler(this.tbxPassword_Enter);
            this.tbxPassword.Leave += new System.EventHandler(this.tbxPassword_Leave);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUsername.ForeColor = System.Drawing.Color.DimGray;
            this.lblUsername.Location = new System.Drawing.Point(120, 81);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(143, 18);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "TÊN ĐĂNG NHẬP";
            // 
            // borderTxtUsername
            // 
            this.borderTxtUsername.BackColor = System.Drawing.Color.White;
            this.borderTxtUsername.BorderColor = System.Drawing.Color.DarkGray;
            this.borderTxtUsername.BorderRadius = 5;
            this.borderTxtUsername.BorderSize = 1;
            this.borderTxtUsername.Controls.Add(this.tbxUsername);
            this.borderTxtUsername.ForeColor = System.Drawing.Color.Black;
            this.borderTxtUsername.Location = new System.Drawing.Point(122, 104);
            this.borderTxtUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.borderTxtUsername.Name = "borderTxtUsername";
            this.borderTxtUsername.Size = new System.Drawing.Size(286, 53);
            this.borderTxtUsername.TabIndex = 0;
            // 
            // tbxUsername
            // 
            this.tbxUsername.BackColor = System.Drawing.Color.White;
            this.tbxUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbxUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbxUsername.Location = new System.Drawing.Point(11, 15);
            this.tbxUsername.Margin = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.tbxUsername.Name = "tbxUsername";
            this.tbxUsername.Size = new System.Drawing.Size(263, 23);
            this.tbxUsername.TabIndex = 1;
            this.tbxUsername.Text = "admin";
            this.tbxUsername.Enter += new System.EventHandler(this.tbxUsername_Enter);
            this.tbxUsername.Leave += new System.EventHandler(this.tbxUsername_Leave);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.btnLogin.Location = new System.Drawing.Point(122, 309);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(286, 60);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "ĐĂNG NHẬP";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // fLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(563, 600);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "fLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fLogin_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fLogin_FormClosed);
            this.Load += new System.EventHandler(this.fLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.borderTxtPassword.ResumeLayout(false);
            this.borderTxtPassword.PerformLayout();
            this.borderTxtUsername.ResumeLayout(false);
            this.borderTxtUsername.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button btnLogin;
        private Label lblPassword;
        private CustomDesign.RoundedPanel borderTxtPassword;
        private Label lblUsername;
        private CustomDesign.RoundedPanel borderTxtUsername;
        private TextBox tbxPassword;
        private TextBox tbxUsername;
    }
}