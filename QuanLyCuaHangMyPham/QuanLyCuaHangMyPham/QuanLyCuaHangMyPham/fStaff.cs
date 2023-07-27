using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangMyPham
{
    public partial class fStaff : Form
    {
        public fStaff()
        {
            InitializeComponent();
        }

        private void OpenForm(Form f)
        {
            this.Tag = f;
            f.Show();
            this.Close();
        }

        private void nav_product_Click(object sender, EventArgs e)
        {
            //OpenForm((new fProduct()));
        }

        private void nav_purchase_Click(object sender, EventArgs e)
        {
            //OpenForm((new fTransaction()));
        }

        private void nav_sell_Click(object sender, EventArgs e)
        {
            OpenForm(new fSell());
        }

        private void nav_report_Click(object sender, EventArgs e)
        {
            //OpenForm(new fReport());
        }
    }
}
