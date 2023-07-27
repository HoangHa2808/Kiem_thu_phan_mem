using QuanLyCuaHangMyPham.DAO;
using QuanLyCuaHangMyPham.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangMyPham
{
    public partial class fBill : Form
    {
        public fBill(string displayName)
        {
            InitializeComponent();
            GetTypeAccount();
            txtDisplayName.Text = displayName;
        }

        private void GetTypeAccount()
        {
            bool type = fLogin.CheckTypeAccount;
            if (type)
                navForAdmin.Visible = true;
            else
                navForAdmin.Visible = false;
        }

        private void OpenForm(Form f)
        {
            this.Tag = f;
            f.ShowDialog();
            this.Show();
        }

        private void nav_sell_Click(object sender, EventArgs e)
        {
            //OpenForm(new fSell());
            this.Close();
        }

        private void nav_product_Click(object sender, EventArgs e)
        {
            //OpenForm(new fProduct());
        }

        private void nav_purchase_Click(object sender, EventArgs e)
        {
            //OpenForm(new fTransaction());
        }

        private void nav_bill_Click(object sender, EventArgs e)
        {

        }

        private void nav_report_Click(object sender, EventArgs e)
        {
            //OpenForm(new fReport());
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dropShadow(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            Color[] shadow = new Color[3];
            shadow[0] = Color.FromArgb(181, 181, 181);
            shadow[1] = Color.FromArgb(195, 195, 195);
            shadow[2] = Color.FromArgb(211, 211, 211);
            Pen pen = new Pen(shadow[0]);
            using (pen)
            {
                foreach (Panel p in panel.Controls.OfType<Panel>())
                {
                    Point pt = p.Location;
                    pt.Y += p.Height;
                    for (var sp = 0; sp < 3; sp++)
                    {
                        pen.Color = shadow[sp];
                        e.Graphics.DrawLine(pen, pt.X, pt.Y, pt.X + p.Width - 1, pt.Y);
                        pt.Y++;
                    }
                }
            }
        }

        private void ShowBill(string query)
        {
            dtgvBill.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void fBill_Load(object sender, EventArgs e)
        {
            displayName.Text = fSell.DisplayName;
            nav_product.Visible = false;
            nav_purchase.Visible = false;
            nav_report.Visible = false;

            dtpkFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);

            string query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
            ShowBill(query);

            DateTime fromDate = new DateTime(dtpkFromDate.Value.Year, 1, 1);
            DateTime toDate = new DateTime(dtpkFromDate.Value.Year, 12, 31);

            UpdateFieldInfor();
        }

        private void UpdateFieldInfor()
        {
            if (dtgvBill.Rows.Count > 0)
            {
                DataTable amount = DataProvider.Instance.ExecuteQuery($"SET DATEFORMAT dmy SELECT dbo.USF_GetAmountByDate('{dtpkFromDate.Value.ToString("dd/MM/yyyy").Split(' ')[0]}', '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}')");
                lblAmount.Text = amount.Rows[0].ItemArray[0].ToString();

                double income = 0;
                foreach (DataGridViewRow item in dtgvBill.Rows)
                {
                    income += (double)item.Cells[6].Value;
                }
                lblIncome.Text = income.ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));

                DataTable profit = DataProvider.Instance.ExecuteQuery($"SET DATEFORMAT dmy SELECT dbo.USF_GetProfitByDate('{dtpkFromDate.Value.ToString("dd/MM/yyyy").Split(' ')[0]}', '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}')");
                lblProfit.Text = ((int)profit.Rows[0].ItemArray[0]).ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));

                DateTime fromDate = new DateTime(dtpkFromDate.Value.Year, 1, 1);
                DateTime toDate = new DateTime(dtpkFromDate.Value.Year, 12, 31);
                DataTable profitOfYear = DataProvider.Instance.ExecuteQuery($"SET DATEFORMAT dmy SELECT dbo.USF_GetProfitByDate('{fromDate.ToString("dd/MM/yyyy").Split(' ')[0]}', '{toDate.ToString("dd/MM/yyyy").Split(' ')[0]}')");
                lblProfitOfYear.Text = ((int)profit.Rows[0].ItemArray[0]).ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
            }
        }

        private void CheckBoxCondition()
        {
            string query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
            if (cbImport.Checked && cbExport.Checked)
            {
                ShowBill(query);
            }
            else if (cbImport.Checked)
            {
                query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT AND HD.Loai = 0";
                ShowBill(query);
            }
            else if (cbExport.Checked)
            {
                query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT AND HD.Loai = 1";
                ShowBill(query);
            }
            else
            {
                dtgvBill.DataSource = null;
            }
        }

        private void cbImport_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxCondition();
        }

        private void cbExport_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxCondition();
        }

        private void dtgvBill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool type = true;
            DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT Loai FROM dbo.HoaDon WHERE MaHD = {dtgvBill.CurrentRow.Cells[0].Value}");
            foreach (DataRow item in data.Rows)
            {
                type = (bool)item.ItemArray[0];
            }

            int id = (int)dtgvBill.CurrentRow.Cells[0].Value;
            string accountName = dtgvBill.CurrentRow.Cells[1].Value.ToString();
            string partnerName = dtgvBill.CurrentRow.Cells[2].Value.ToString();
            DateTime? dateCreate = (DateTime?)dtgvBill.CurrentRow.Cells[3].Value;
            DateTime? dateCheckOut = (DateTime?)dtgvBill.CurrentRow.Cells[4].Value;
            int discount = (int)dtgvBill.CurrentRow.Cells[5].Value;
            double totalPrice = (double)dtgvBill.CurrentRow.Cells[6].Value;

            BillDTO currentBill = new BillDTO(id, accountName, partnerName, dateCreate, dateCheckOut, discount, totalPrice, type);
            OpenForm(new fBillInfor(currentBill));
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            CheckBoxCondition();
            UpdateFieldInfor();
        }

        private void dtgvBill_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int amount = 0;
                if (MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn {dtgvBill.CurrentRow.Cells[0].Value}?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    amount = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.HoaDon SET TrangThai = 0 WHERE MaHD = {dtgvBill.CurrentRow.Cells[0].Value}");
                    if (amount > 0)
                    {
                        MessageBox.Show($"Xóa thành công hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
                        ShowBill(query);
                    }
                    else
                    {
                        MessageBox.Show($"Có lỗi xảy ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void nmValue_ValueChanged(object sender, EventArgs e)
        {
            string query = "";
            if (cbInitial.SelectedItem == null || cbContent.SelectedItem == null)
            {
                return;
            }
            if (cbInitial.SelectedItem.Equals("Tổng tiền"))
            {
                switch (cbContent.SelectedItem.ToString())
                {
                    case ">":
                        query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE TongTien > {nmValue.Value} AND NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
                        break;
                    case "<":
                        query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE TongTien < {nmValue.Value} AND NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
                        break;
                    case "=":
                        query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE TongTien = {nmValue.Value} AND NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
                        break;
                    default:
                        break;
                }
                dtgvBill.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
            else if (cbInitial.SelectedItem.Equals("Giảm giá"))
            {
                switch (cbContent.SelectedItem.ToString())
                {
                    case ">":
                        query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE GiamGia > {nmValue.Value} AND NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
                        break;
                    case "<":
                        query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE GiamGia < {nmValue.Value} AND NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
                        break;
                    case "=":
                        query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE GiamGia = {nmValue.Value} AND NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
                        break;
                    default:
                        break;
                }
                dtgvBill.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
        }

        private void btnDisableFilter_Click(object sender, EventArgs e)
        {
            string query = $"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE NgayTT IS NOT NULL AND NgayLap >= '{dtpkFromDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{dtpkToDate.Value.Date.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT";
            ShowBill(query);
            cbInitial.SelectedItem = null;
            cbContent.SelectedItem = null;
            nmValue.Value = 0;
        }

        private void cbInitial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInitial.SelectedItem != null)
            {
                cbContent.Visible = true;
            }
            if (cbContent.SelectedItem != null)
            {
                this.nmValue_ValueChanged(this, null);
            }
        }

        private void cbContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbContent.SelectedItem != null)
            {
                nmValue.Visible = true;
                btnDisableFilter.Visible = true;
                this.nmValue_ValueChanged(this, null);
            }
        }

        private void dtgvBill_DataSourceChanged(object sender, EventArgs e)
        {
            UpdateFieldInfor();
        }
    }
}
