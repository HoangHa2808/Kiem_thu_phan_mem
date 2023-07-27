using QuanLyCuaHangMyPham.DAO;
using QuanLyCuaHangMyPham.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangMyPham
{
    public partial class fTransaction : Form
    {
        public AccountDTO LoginAccount { get; set; }
        public fTransaction(AccountDTO account)
        {
            InitializeComponent();
            this.LoginAccount = account;
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

        private void nav_report_Click(object sender, EventArgs e)
        {
            //OpenForm(new fReport());
        }

        private void nav_bill_Click(object sender, EventArgs e)
        {
            //OpenForm(new fBill());
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            fTransactionProduct f = new fTransactionProduct();
            f.Show();
        }

        private void fTransaction_Load(object sender, EventArgs e)
        {
            txtDisplayName.Text = LoginAccount.Name;
            nav_product.Visible = false;
            nav_bill.Visible = false;
            nav_report.Visible = false;

            GetTypeAccount();
            ShowListProduct();
            ShowQueueBill();
            DisableEditColumn();
            if (dtgvListQueueBill.Rows.Count > 0)
            {
                ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
                txtName.Text = dtgvListQueueBill.CurrentRow.Cells[1].Value.ToString();
                DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT * FROM dbo.DoiTac WHERE TenDT = N'{txtName.Text}' AND Loai = 0 AND TrangThai = 1");
                foreach (DataRow item in data.Rows)
                {
                    txtAddress.Text = item.ItemArray[2].ToString();
                    txtPhoneNumber.Text = item.ItemArray[3].ToString();
                }
            }
            else
            {
                ShowBillInfor();
            }
        }

        private void GetTypeAccount()
        {
            bool type = fLogin.CheckTypeAccount;
            if (type)
                navForAdmin.Visible = true;
            else
                navForAdmin.Visible = false;
        }

        private void ShowListProduct()
        {
            dtgvListProduct.DataSource = DataProvider.Instance.ExecuteQuery("SELECT MaSP AS [ID], TenSP AS [Tên], Gia AS [Giá], SoLuongTon AS [Số Lượng tồn], MoTa AS [Mô tả] FROM dbo.SanPham WHERE TrangThai = 1");
        }

        int temp = 0;

        private void ShowBillInfor(int id)
        {
            DisableEditColumn();
            try
            {
                dtgvBillInfor.DataSource = DataProvider.Instance.ExecuteQuery($"SELECT SP.TenSP, CT.SoLuong, SP.Gia, CT.ThanhTien FROM dbo.CTHoaDon CT, dbo.SanPham SP WHERE CT.MaSP = SP.MaSP AND CT.MaHD = {id}");
            }
            catch (Exception)
            {
            }
            int sum = 0;
            for (int i = 0; i < dtgvBillInfor.Rows.Count; i++)
            {
                dtgvBillInfor.Rows[i].Cells[3].Value = (int)dtgvBillInfor.Rows[i].Cells[1].Value * (int)dtgvBillInfor.Rows[i].Cells[2].Value;
                sum += (int)dtgvBillInfor.Rows[i].Cells[3].Value;
            }
            temp = sum;
            lblTotalPrice.Text = sum.ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
            lblFinalPrice.Text = lblTotalPrice.Text;
        }

        private void ShowBillInfor()
        {
            DisableEditColumn();
            int sum = 0;
            for (int i = 0; i < dtgvBillInfor.Rows.Count; i++)
            {
                dtgvBillInfor.Rows[i].Cells[3].Value = (int)dtgvBillInfor.Rows[i].Cells[1].Value * (int)dtgvBillInfor.Rows[i].Cells[2].Value;
                sum += (int)dtgvBillInfor.Rows[i].Cells[3].Value;
            }
            temp = sum;
            lblTotalPrice.Text = sum.ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
            lblFinalPrice.Text = lblTotalPrice.Text;
        }

        private void ShowQueueBill()
        {
            dtgvListQueueBill.DataSource = DataProvider.Instance.ExecuteQuery("SELECT MaHD, TenDT, NgayLap FROM dbo.HoaDon HD, dbo.DoiTac DT WHERE NgayTT IS NULL AND HD.Loai = 0 AND HD.MaKH = DT.MaDT AND DT.Loai = 0");
        }

        private void btnAddQueueBill_Click(object sender, EventArgs e)
        {
            if (txtName.Text == null || txtName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            DataProvider.Instance.ExecuteQuery($"EXEC USP_AddPartner N'{txtName.Text}', N'{txtAddress.Text}', '{txtPhoneNumber.Text}', 0");
            int id = PartnerDAO.Instance.GetIDByPartner(txtName.Text, txtAddress.Text, txtPhoneNumber.Text);
            if (id != -1)
            {
                DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddBill {LoginAccount.Id}, {id}, NULL, 0, 0, 0");
                ShowQueueBill();
            }
        }

        private void dtgvListQueueBill_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    int id = (int)dtgvListQueueBill.CurrentRow.Cells[0].Value;
                    if (MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn {id}?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        if (DataProvider.Instance.ExecuteNonQuery($"EXEC USP_DeleteBill '{id}'") > 0)
                        {
                            MessageBox.Show($"Xóa thành công hóa đơn {id}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Có lỗi xảy ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                    if (dtgvListQueueBill.Rows.Count > 0)
                    {
                        ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
                    }
                    else
                    {
                        dtgvBillInfor.DataSource = null;
                    }
                }
                catch (Exception)
                {
                }
                ShowQueueBill();
                dtgvBillInfor.Controls.Clear();
            }
            else if (e.Button == MouseButtons.Left)
            {
                ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
                txtName.Text = dtgvListQueueBill.CurrentRow.Cells[1].Value.ToString();
                DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT * FROM dbo.DoiTac WHERE TenDT = N'{txtName.Text}' AND Loai = 0 AND TrangThai = 1");
                foreach (DataRow item in data.Rows)
                {
                    txtAddress.Text = item.ItemArray[2].ToString();
                    txtPhoneNumber.Text = item.ItemArray[3].ToString();
                }
            }
        }

        private void dtgvBillInfor_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa {dtgvBillInfor.CurrentRow.Cells[0].Value}?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (DataProvider.Instance.ExecuteNonQuery($"EXEC USP_DeleteBillInfor {dtgvListQueueBill.CurrentRow.Cells[0].Value}, N'{dtgvBillInfor.CurrentRow.Cells[0].Value}'") > 0)
                    {
                        MessageBox.Show($"Xóa thành công {dtgvBillInfor.CurrentRow.Cells[0].Value}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Có lỗi xảy ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
                }
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            if (txtDiscount.Text == "" || txtDiscount.Text == null)
            {
                MessageBox.Show("Vui lòng nhập giá trị tại ô giảm giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int discount = Convert.ToInt32(txtDiscount.Text);
                if (rdPercent.Checked == true)
                {
                    if (Int32.Parse(txtDiscount.Text) < 0 || Int32.Parse(txtDiscount.Text) > 100)
                    {
                        MessageBox.Show("Giá trị giảm giá phải >= 0 và <= 100.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    lblDiscount.Text = (temp / 100 * discount).ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
                    lblFinalPrice.Text = (temp - (temp / 100 * discount)).ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
                }
                else if (rdCost.Checked == true)
                {
                    if (Int32.Parse(txtDiscount.Text) < 0 || Int32.Parse(txtDiscount.Text) > temp)
                    {
                        MessageBox.Show($"Giá trị giảm giá phải >= 0 và <= {lblTotalPrice.Text}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    lblDiscount.Text = discount.ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
                    lblFinalPrice.Text = (temp - discount).ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
                }
            }
        }

        private int GetAmountByPorductName(string productName)
        {
            int amount = -1;
            for (int i = 0; i < dtgvListProduct.Rows.Count; i++)
            {
                if (productName.Equals(dtgvListProduct.Rows[i].Cells[1].Value.ToString()))
                {
                    amount = (int)dtgvListProduct.Rows[i].Cells[3].Value;
                    return amount;
                }
            }
            return amount;
        }

        private void dtgvBillInfor_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int amount = GetAmountByPorductName(dtgvBillInfor.CurrentRow.Cells[0].Value.ToString());
            if (/*(int)dtgvBillInfor.CurrentRow.Cells[1].Value > amount || */(int)dtgvBillInfor.CurrentRow.Cells[1].Value <= 0)
            {
                MessageBox.Show($"Số lượng xuất phải > 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
            }
            else
            {
                ShowBillInfor();
            }
        }

        private void DisableEditColumn()
        {
            for (int i = 0; i < dtgvBillInfor.Rows.Count; i++)
            {
                //dtgvBillInfor.Rows[i].Cells[0].ReadOnly = true;
                //dtgvBillInfor.Rows[i].Cells[2].ReadOnly = true;
                //dtgvBillInfor.Rows[i].Cells[3].ReadOnly = true;
                dtgvBillInfor.Columns[0].ReadOnly = true;
                dtgvBillInfor.Columns[2].ReadOnly = true;
                dtgvBillInfor.Columns[3].ReadOnly = true;
            }
        }

        private void dtgvBillInfor_Enter(object sender, EventArgs e)
        {
            DisableEditColumn();
        }

        private int ConvertPrice(string price)
        {
            int result = -1;
            if (price.Equals("0 đ"))
            {
                result = Convert.ToInt32(price.Split(' ')[0]);
                return result;
            }
            string s1 = price.Split(',')[0];
            if (s1.Equals(""))
            {
                result = Convert.ToInt32(s1.Split(' ')[0]);
                return result;
            }
            string[] s2 = s1.Split('.');
            string s3 = "";
            foreach (string item in s2)
            {
                s3 += item;
            }
            result = Convert.ToInt32(s3);
            return result;
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgvBillInfor.Rows.Count; i++)
            {
                DataProvider.Instance.ExecuteNonQuery($"EXEC USP_ImportCheckOut {dtgvListQueueBill.CurrentRow.Cells[0].Value}, N'{dtgvBillInfor.Rows[i].Cells[0].Value}', {dtgvBillInfor.Rows[i].Cells[1].Value}");
            }
            ShowListProduct();
            DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.HoaDon SET NgayTT = GETDATE (), GiamGia = {ConvertPrice(lblDiscount.Text)}, TongTien = {ConvertPrice(lblTotalPrice.Text)} WHERE MaHD = {dtgvListQueueBill.CurrentRow.Cells[0].Value}");
            DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.DoiTac SET TenDT = N'{txtName.Text}', DiaChi = N'{txtAddress.Text}', SDT = '{txtPhoneNumber.Text}' WHERE TenDT = N'{dtgvListQueueBill.CurrentRow.Cells[1].Value}'");

            dtgvBillInfor.DataSource = null;
            ShowQueueBill();
            if (dtgvListQueueBill.Rows.Count > 0)
            {
                ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
            }
            else
            {
                ShowBillInfor();
            }

            txtName.Text = "";
            txtAddress.Text = "";
            txtPhoneNumber.Text = "";
            lblTotalPrice.Text = "0 đ";
            lblDiscount.Text = "0 đ";
            lblFinalPrice.Text = "0 đ";
        }

        private void ShowListProduct(string query)
        {
            dtgvListProduct.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            ShowListProduct($"SELECT MaSP AS [ID], TenSP AS [Tên], Gia AS [Giá], SoLuongTon AS [Số Lượng tồn], MoTa AS [Mô tả] FROM dbo.SanPham WHERE TrangThai = 1 AND SoLuongTon > 0 AND dbo.USF_ConvertToUnsign(TenSP) LIKE dbo.USF_ConvertToUnsign(N'{txtSearchProduct.Text}') + '%'");
        }

        private void cbInitial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInitial.SelectedItem.Equals("Phân loại Sản phẩm") == true)
            {
                cbContent.DataSource = DataProvider.Instance.ExecuteQuery("SELECT TenPL FROM dbo.PhanLoaiSP WHERE TrangThai = 1");
                cbContent.DisplayMember = "TenPL";
            }
            else if (cbInitial.SelectedItem.Equals("Đối tác") == true)
            {
                cbContent.DataSource = DataProvider.Instance.ExecuteQuery("SELECT TenDT FROM dbo.DoiTac WHERE Loai = 1 AND TrangThai = 1");
                cbContent.DisplayMember = "TenDT";
            }
        }

        private void cbContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInitial.SelectedItem.Equals("Phân loại Sản phẩm") == true)
            {
                dtgvListProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SELECT MaSP AS [ID], TenSP AS [Tên], Gia AS [Giá], SoLuongTon AS [Số Lượng tồn], SP.MoTa AS [Mô tả] FROM dbo.PhanLoaiSP PL, dbo.SanPham SP WHERE SoLuongTon > 0 AND PL.TrangThai = 1 AND SP.TrangThai = 1 AND PL.MaPL = SP.PhanLoai AND PL.TenPL = N'{cbContent.Text}'");
            }
            else if (cbInitial.SelectedItem.Equals("Đối tác") == true)
            {
                dtgvListProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SELECT MaSP AS [ID], TenSP AS [Tên], Gia AS [Giá], SoLuongTon AS [Số Lượng tồn], SP.MoTa AS [Mô tả] FROM dbo.DoiTac DT, dbo.SanPham SP WHERE SoLuongTon > 0 AND DT.TrangThai = 1 AND SP.TrangThai = 1 AND DT.MaDT = SP.NhaCC AND DT.TenDT = N'{cbContent.Text}'");
            }
        }

        private void btnDisableFilter_Click(object sender, EventArgs e)
        {
            cbInitial.Text = "";
            cbContent.DataSource = null;
            ShowListProduct();
        }

        private void dtgvListProduct_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //if (dtgvListQueueBill.SelectedCells.Count == 0)
                //{
                //    MessageBox.Show("Vui lòng tạo và chọn 1 hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else
                //{
                //    object amount = DataProvider.Instance.ExecuteScalar($"SELECT * FROM dbo.CTHoaDon WHERE MaHD = {dtgvListQueueBill.CurrentRow.Cells[0].Value} AND MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                //    if (amount == null)
                //    {
                //        DataProvider.Instance.ExecuteQuery($"EXEC USP_AddBillInfor {dtgvListQueueBill.CurrentRow.Cells[0].Value}, {dtgvListProduct.CurrentRow.Cells[0].Value}, 1");
                //        ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
                //        dtgvBillInfor.CurrentRow.Cells[3].Value = (int)dtgvBillInfor.CurrentRow.Cells[1].Value * (int)dtgvBillInfor.CurrentRow.Cells[2].Value;
                //    }
                //    else
                //    {
                //        DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.CTHoaDon SET SoLuong = SoLuong + 1 WHERE MaHD = {dtgvListQueueBill.CurrentRow.Cells[0].Value} AND MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                //        ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
                //        dtgvBillInfor.CurrentRow.Cells[3].Value = (int)dtgvBillInfor.CurrentRow.Cells[1].Value * (int)dtgvBillInfor.CurrentRow.Cells[2].Value;
                //    }
                //}

                string nameOfPartner = "";

                if (dtgvListQueueBill.SelectedCells.Count == 0)
                {
                    if (txtName.Text != null && txtName.Text != "")
                    {
                        DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddPartner N'{txtName.Text}', N'{txtAddress.Text}', '{txtPhoneNumber.Text}', 0");
                        int id = PartnerDAO.Instance.GetIDByPartner(txtName.Text);
                        DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddBill {LoginAccount.Id}, {id}, NULL, 0, 0, 0");
                        nameOfPartner = txtName.Text;
                    }
                    else
                    {
                        DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddPartner N'Sys_Partner', N'{txtAddress.Text}', '{txtPhoneNumber.Text}', 0");
                        int id = PartnerDAO.Instance.GetIDByPartner("Sys_Partner");
                        DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddBill {LoginAccount.Id}, {id}, NULL, 0, 0, 0");
                        nameOfPartner = "Sys_Partner";
                    }

                    foreach (DataGridViewRow item in dtgvListQueueBill.Rows)
                    {
                        if (item.Cells[1].Value.Equals(nameOfPartner))
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    ShowQueueBill();
                }

                DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT * FROM dbo.CTHoaDon WHERE MaHD = {dtgvListQueueBill.CurrentRow.Cells[0].Value} AND MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                int amount = data.Rows.Count;

                if (amount == 0)
                {
                    DataProvider.Instance.ExecuteQuery($"EXEC USP_AddBillInfor {dtgvListQueueBill.CurrentRow.Cells[0].Value}, {dtgvListProduct.CurrentRow.Cells[0].Value}, 1");
                    ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
                    dtgvBillInfor.CurrentRow.Cells[3].Value = (int)dtgvBillInfor.CurrentRow.Cells[1].Value * (int)dtgvBillInfor.CurrentRow.Cells[2].Value;
                    return;
                }

                int rowIndex = 0;
                if (dtgvBillInfor.Rows.Count > 0)
                {
                    foreach (DataGridViewRow item in dtgvBillInfor.Rows)
                    {
                        if (item.Cells[0].Value.Equals(dtgvListProduct.CurrentRow.Cells[1].Value))
                        {
                            rowIndex = item.Index;
                            break;
                        }
                    }
                    if ((int)dtgvBillInfor.Rows[rowIndex].Cells[1].Value == (int)dtgvListProduct.CurrentRow.Cells[3].Value)
                    {
                        MessageBox.Show($"Số lượng xuất sản phẩm {dtgvListProduct.CurrentRow.Cells[1].Value} không được vượt quá số lượng tồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.CTHoaDon SET SoLuong = SoLuong + 1 WHERE MaHD = {dtgvListQueueBill.CurrentRow.Cells[0].Value} AND MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
                dtgvBillInfor.CurrentRow.Cells[3].Value = (int)dtgvBillInfor.CurrentRow.Cells[1].Value * (int)dtgvBillInfor.CurrentRow.Cells[2].Value;
            }
        }

        private void btnCreateQueueBill_Click(object sender, EventArgs e)
        {
            if (txtName.Text == null || txtName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int result = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.DoiTac SET TenDT = N'{txtName.Text}', DiaChi = N'{txtAddress.Text}', SDT = '{txtPhoneNumber.Text}' WHERE TenDT = 'Sys_Partner'");

            if (result > 0)
            {
                MessageBox.Show("Tạo thành công hóa đơn chờ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            dtgvBillInfor.DataSource = null;
            ShowQueueBill();
            if (dtgvListQueueBill.Rows.Count > 0)
            {
                ShowBillInfor((int)dtgvListQueueBill.CurrentRow.Cells[0].Value);
            }
            else
            {
                ShowBillInfor();
            }

            txtName.Text = "";
            txtAddress.Text = "";
            txtPhoneNumber.Text = "";
            lblTotalPrice.Text = "0 đ";
            lblDiscount.Text = "0 đ";
            lblFinalPrice.Text = "0 đ";
        }

        private void dtgvListProduct_Enter(object sender, EventArgs e)
        {
            ShowListProduct();
        }

        private void btnAddProduct_Click_1(object sender, EventArgs e)
        {
            OpenForm(new AddProduct(), "o");
        }

        private void OpenForm(Form f, string type = null)
        {
            if (type == "o")
            {
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    ShowListProduct();
                }
            }
            else
            {
                this.Tag = f;
                f.Show();
                this.Close();
            }
        }
    }
}
