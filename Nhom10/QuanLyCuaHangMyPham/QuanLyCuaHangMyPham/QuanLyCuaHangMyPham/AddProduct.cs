using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangMyPham;
using QuanLyCuaHangMyPham.DAO;
using QuanLyCuaHangMyPham.DTO;

namespace QuanLyCuaHangMyPham
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Phân loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (cbPartner.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (txbTenSP.Text == null || txbTenSP.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.cbCategory.SelectedItem.Equals("Thêm mới...") == true && this.cbPartner.SelectedItem.Equals("Thêm mới...") == true)
            {
                if (txtNameOfCategory.Text == null || txtNameOfCategory.Text == "" || txtNameOfPartner.Text == null || txtNameOfPartner.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập tên Phân loại và tên Nhà CC!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddCategory N'{txtNameOfCategory.Text}', N'{txtCategory.Text}'");
                DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddPartner N'{txtNameOfPartner.Text}', N'{txtAddress.Text}', '{txtPhoneNumber.Text}', 1");
            }
            else if (this.cbCategory.SelectedItem.Equals("Thêm mới...") == true && this.cbPartner.SelectedItem.Equals("Thêm mới...") == false)
            {
                if (txtNameOfCategory.Text == null || txtNameOfCategory.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập tên Phân loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddCategory N'{txtNameOfCategory.Text}', N'{txtCategory.Text}'");
            }
            else if (this.cbCategory.SelectedItem.Equals("Thêm mới...") == false && this.cbPartner.SelectedItem.Equals("Thêm mới...") == true)
            {
                if (txtNameOfPartner.Text == null || txtNameOfPartner.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập tên Nhà CC!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddPartner N'{txtNameOfPartner.Text}', N'{txtAddress.Text}', '{txtPhoneNumber.Text}', 1");
            }
            else
            {

            }

            int categoryID = 0;

            DataTable categoryData = DataProvider.Instance.ExecuteQuery($"SELECT MaPL FROM dbo.PhanLoaiSP WHERE TenPL = N'{txtNameOfCategory.Text}'");

            foreach (DataRow row in categoryData.Rows)
            {
                categoryID = (int)row.ItemArray[0];
            }

            int partnerID = 0;

            DataTable partnerData = DataProvider.Instance.ExecuteQuery($"SELECT MaDT FROM dbo.DoiTac WHERE TenDT = N'{txtNameOfPartner.Text}'");

            foreach (DataRow row in partnerData.Rows)
            {
                partnerID = (int)row.ItemArray[0];
            }

            int resutlCount = 0;

            resutlCount = DataProvider.Instance.ExecuteNonQuery($"EXEC USP_AddProduct N'{txbTenSP.Text}', {categoryID}, {nmGiaNhap.Value}, {nmGiaBan.Value}, N'{txtUnit.Text}', {nmAmount.Value}, N'{txbTenSP.Text}', {partnerID}, N'{txtProductDescription.Text}'");

            if (resutlCount > 0)
            {
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            pnNewCategory.Visible = false;
            pnNewPartner.Visible = false;
            panel13.Visible = false;
            panel14.Visible = false;
            panel15.Visible = false;

            DataTable category = DataProvider.Instance.ExecuteQuery("SELECT TenPL FROM dbo.PhanLoaiSP WHERE TrangThai = 1");
            foreach (DataRow row in category.Rows)
            {
                cbCategory.Items.Add(row.ItemArray[0]);
            }

            DataTable partner = DataProvider.Instance.ExecuteQuery("SELECT TenDT FROM dbo.DoiTac WHERE TrangThai = 1 AND Loai = 1");
            foreach (DataRow row in partner.Rows)
            {
                cbPartner.Items.Add(row.ItemArray[0]);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbCategory.SelectedItem.Equals("Thêm mới...") == true)
            {
                pnNewCategory.Visible = true;
                panel15.Visible = true;
            }
            else
            {
                pnNewCategory.Visible = false;
                panel15.Visible = false;
            }
        }

        private void cbPartner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbPartner.SelectedItem.Equals("Thêm mới...") == true)
            {
                pnNewPartner.Visible = true;
                panel13.Visible = true;
                panel14.Visible = true;
            }
            else
            {
                pnNewPartner.Visible = false;
                panel13.Visible = false;
                panel14.Visible = false;
            }
        }

        private void AddProduct_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void roundedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void roundedPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
