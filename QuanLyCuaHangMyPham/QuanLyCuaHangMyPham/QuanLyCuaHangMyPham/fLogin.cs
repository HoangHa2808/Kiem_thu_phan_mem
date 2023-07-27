using QuanLyCuaHangMyPham.DAO;
using QuanLyCuaHangMyPham.DTO;
using System.Data;

namespace QuanLyCuaHangMyPham
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        public AccountDTO LoginAccount { get; set; }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Login(tbxUsername.Text, tbxPassword.Text))
            {
                this.LoginAccount = AccountDAO.Instance.GetAccountByUserName(tbxUsername.Text);

                CheckTypeAccount = AccountDAO.Instance.CheckTypeAccount(tbxUsername.Text, tbxPassword.Text);

                this.DialogResult = DialogResult.OK;

                //fSell f = new fSell(loginAccount);
                //this.Hide();
                //f.ShowDialog();
                //this.Show();
            }
            else
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo");
        }

        public static bool CheckTypeAccount = false;

        public static string getDisplayName = "";

        private bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }

        private void tbxUsername_Enter(object sender, EventArgs e)
        {
            borderTxtUsername.BorderColor = Color.FromArgb(74, 175, 255);
            lblUsername.ForeColor = Color.FromArgb(74, 175, 255);
        }

        private void tbxUsername_Leave(object sender, EventArgs e)
        {
            borderTxtUsername.BorderColor = Color.DarkGray;
            lblUsername.ForeColor = Color.DarkGray;
        }

        private void tbxPassword_Enter(object sender, EventArgs e)
        {
            borderTxtPassword.BorderColor = Color.FromArgb(74, 175, 255);
            lblPassword.ForeColor = Color.FromArgb(74, 175, 255);
        }

        private void tbxPassword_Leave(object sender, EventArgs e)
        {
            borderTxtPassword.BorderColor = Color.DarkGray;
            lblPassword.ForeColor = Color.DarkGray;
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void fLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }
    }
}