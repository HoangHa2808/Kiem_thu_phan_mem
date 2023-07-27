using QuanLyCuaHangMyPham.DAO;
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
    public partial class fReport : Form
    {
        public fReport(string displayName)
        {
            InitializeComponent();
            txtDisplayName.Text = displayName;
        }
        private void OpenForm(Form f)
        {
            this.Tag = f;
            f.Show();
            this.Close();
        }
        private void nav_product_Click(object sender, EventArgs e)
        {
            //OpenForm(new fProduct());
        }

        private void nav_sell_Click(object sender, EventArgs e)
        {
            //OpenForm(new fSell());
            this.Close();
        }


        private void nav_purchase_Click(object sender, EventArgs e)
        {
            //OpenForm(new fTransaction());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nav_bill_Click(object sender, EventArgs e)
        {
            //OpenForm(new fBill());
        }

        private void fReport_Load(object sender, EventArgs e)
        {
            nav_product.Visible = false;
            nav_purchase.Visible = false;
            nav_bill.Visible = false;

            dtpkFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);

            DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT MaSP, SoLuongTon FROM dbo.SanPham");
            lblAmountProduct.Text = data.Rows.Count.ToString();
            int amountInStock = 0;
            foreach (DataRow item in data.Rows)
            {
                amountInStock += (int)item.ItemArray[1];
            }
            lblAmountInStock.Text = amountInStock.ToString();

            dtgvBestSellProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SET DATEFORMAT dmy EXEC USP_GetListBestSellProduct '{dtpkFromDate.Value.Date.ToString("dd/MM/yyy").Split(' ')[0]}', '{dtpkToDate.Value.Date.ToString("dd/MM/yyy").Split(' ')[0]}'");

            dtgvBadSellProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SET DATEFORMAT dmy EXEC USP_GetListBadSellProduct '{dtpkFromDate.Value.Date.ToString("dd/MM/yyy").Split(' ')[0]}', '{dtpkToDate.Value.Date.ToString("dd/MM/yyy").Split(' ')[0]}'");

            if (dtgvBestSellProduct.Rows.Count > 0)
            {
                int amount = 0;
                foreach (DataGridViewRow item in dtgvBestSellProduct.Rows)
                {
                    amount += (int)item.Cells[2].Value;
                }
                lblBestSell.Text = amount.ToString();
            }
            if (dtgvBadSellProduct.Rows.Count > 0)
            {
                int amount = 0;
                foreach (DataGridViewRow item in dtgvBadSellProduct.Rows)
                {
                    amount += (int)item.Cells[2].Value;
                }
                lblBadSell.Text = amount.ToString();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT MaSP, SoLuongTon FROM dbo.SanPham");
            lblAmountProduct.Text = data.Rows.Count.ToString();
            int amountInStock = 0;
            foreach (DataRow item in data.Rows)
            {
                amountInStock += (int)item.ItemArray[1];
            }
            lblAmountInStock.Text = amountInStock.ToString();

            dtgvBestSellProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SET DATEFORMAT dmy EXEC USP_GetListBestSellProduct '{dtpkFromDate.Value.Date.ToString("dd/MM/yyy").Split(' ')[0]}', '{dtpkToDate.Value.Date.ToString("dd/MM/yyy").Split(' ')[0]}'");

            dtgvBadSellProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SET DATEFORMAT dmy EXEC USP_GetListBadSellProduct '{dtpkFromDate.Value.Date.ToString("dd/MM/yyy").Split(' ')[0]}', '{dtpkToDate.Value.Date.ToString("dd/MM/yyy").Split(' ')[0]}'");

            if (dtgvBestSellProduct.Rows.Count > 0)
            {
                int amount = 0;
                foreach (DataGridViewRow item in dtgvBestSellProduct.Rows)
                {
                    amount += (int)item.Cells[2].Value;
                }
                lblBestSell.Text = amount.ToString();
            }
            if (dtgvBadSellProduct.Rows.Count > 0)
            {
                int amount = 0;
                foreach (DataGridViewRow item in dtgvBadSellProduct.Rows)
                {
                    amount += (int)item.Cells[2].Value;
                }
                lblBadSell.Text = amount.ToString();
            }
        }
    }
}