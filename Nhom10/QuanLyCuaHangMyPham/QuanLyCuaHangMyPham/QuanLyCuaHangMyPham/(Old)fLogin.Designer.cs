//namespace QuanLyCuaHangMyPham
//{
//    partial class fLogin
//    {
//        /// <summary>
//        ///  Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        ///  Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        ///  Required method for Designer support - do not modify
//        ///  the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.panel1 = new System.Windows.Forms.Panel();
//            this.btnLogin = new System.Windows.Forms.Button();
//            this.panel2 = new System.Windows.Forms.Panel();
//            this.label1 = new System.Windows.Forms.Label();
//            this.tbxPassword = new System.Windows.Forms.TextBox();
//            this.panel3 = new System.Windows.Forms.Panel();
//            this.label2 = new System.Windows.Forms.Label();
//            this.tbxUsername = new System.Windows.Forms.TextBox();
//            this.panel1.SuspendLayout();
//            this.panel2.SuspendLayout();
//            this.panel3.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // panel1
//            // 
//            this.panel1.Controls.Add(this.btnLogin);
//            this.panel1.Controls.Add(this.panel2);
//            this.panel1.Controls.Add(this.panel3);
//            this.panel1.Location = new System.Drawing.Point(14, 16);
//            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.panel1.Name = "panel1";
//            this.panel1.Size = new System.Drawing.Size(531, 568);
//            this.panel1.TabIndex = 0;
//            // 
//            // btnLogin
//            // 
//            this.btnLogin.Location = new System.Drawing.Point(133, 149);
//            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.btnLogin.Name = "btnLogin";
//            this.btnLogin.Size = new System.Drawing.Size(153, 60);
//            this.btnLogin.TabIndex = 2;
//            this.btnLogin.Text = "Đăng nhập";
//            this.btnLogin.UseVisualStyleBackColor = true;
//            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
//            // 
//            // panel2
//            // 
//            this.panel2.Controls.Add(this.label1);
//            this.panel2.Controls.Add(this.tbxPassword);
//            this.panel2.Location = new System.Drawing.Point(3, 68);
//            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.panel2.Name = "panel2";
//            this.panel2.Size = new System.Drawing.Size(362, 44);
//            this.panel2.TabIndex = 1;
//            this.panel2.TabStop = true;
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(3, 8);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(70, 20);
//            this.label1.TabIndex = 0;
//            this.label1.Text = "Mật khẩu";
//            // 
//            // tbxPassword
//            // 
//            this.tbxPassword.Location = new System.Drawing.Point(152, 4);
//            this.tbxPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.tbxPassword.Name = "tbxPassword";
//            this.tbxPassword.PasswordChar = '*';
//            this.tbxPassword.Size = new System.Drawing.Size(185, 27);
//            this.tbxPassword.TabIndex = 1;
//            this.tbxPassword.Text = "1";
//            // 
//            // panel3
//            // 
//            this.panel3.Controls.Add(this.label2);
//            this.panel3.Controls.Add(this.tbxUsername);
//            this.panel3.Location = new System.Drawing.Point(3, 4);
//            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.panel3.Name = "panel3";
//            this.panel3.Size = new System.Drawing.Size(362, 44);
//            this.panel3.TabIndex = 0;
//            this.panel3.TabStop = true;
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.Location = new System.Drawing.Point(3, 8);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(107, 20);
//            this.label2.TabIndex = 0;
//            this.label2.Text = "Tên đăng nhập";
//            // 
//            // tbxUsername
//            // 
//            this.tbxUsername.Location = new System.Drawing.Point(152, 4);
//            this.tbxUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.tbxUsername.Name = "tbxUsername";
//            this.tbxUsername.Size = new System.Drawing.Size(185, 27);
//            this.tbxUsername.TabIndex = 0;
//            this.tbxUsername.Text = "admin";
//            // 
//            // fLogin
//            // 
//            this.AcceptButton = this.btnLogin;
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(563, 600);
//            this.Controls.Add(this.panel1);
//            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.Name = "fLogin";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.Text = "Đăng nhập";
//            this.Load += new System.EventHandler(this.fLogin_Load);
//            this.panel1.ResumeLayout(false);
//            this.panel2.ResumeLayout(false);
//            this.panel2.PerformLayout();
//            this.panel3.ResumeLayout(false);
//            this.panel3.PerformLayout();
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private Panel panel1;
//        private TextBox tbxUsername;
//        private Button btnLogin;
//        private Panel panel2;
//        private Label label1;
//        private TextBox tbxPassword;
//        private Panel panel3;
//        private Label label2;
//    }
//}