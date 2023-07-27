using QuanLyCuaHangMyPham.DAO;
using QuanLyCuaHangMyPham.DTO;
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
    public partial class fProduct : Form
    {
        public fProduct(string displayName)
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

        private void LoadProduct()
        {
            displayName.Text = fSell.DisplayName;

            dtgvListProduct.DataSource = DataProvider.Instance.ExecuteQuery("SELECT SP.MaSP AS [ID], TenSP AS [Tên sản phẩm], PL.TenPL AS [Phân loại], GiaNhap AS [Giá nhập], Gia AS [Giá bán], DonViTinh AS [Đơn vị tính], SoLuongTon AS [Số lượng tồn], XuatXu AS [Xuất xứ], DT.TenDT AS [Nhà cung cấp], SP.MoTa AS [Mô tả] FROM dbo.SanPham SP, dbo.DoiTac DT, dbo.PhanLoaiSP PL WHERE SP.NhaCC = DT.MaDT AND SP.PhanLoai = PL.MaPL AND SP.TrangThai = 1");
        }

        //public void createNewCell()
        //{
        //    List<Product> listProduct = ProductDAO.Instance.getListProduct();

        //    int a = listProduct.Count;
        //    int i = 1;
        //    foreach (var item in listProduct)
        //    {
        //        Label count = new Label();
        //        Label id = new Label();
        //        Label name = new Label();
        //        Label priceIn = new Label();
        //        Label priceOut = new Label();
        //        Label amount = new Label();


        //        tableListProduct.Controls.Add(count, 0, i); // column, row; c start 0, r start 1
        //        tableListProduct.Controls.Add(id, 1, i);
        //        tableListProduct.Controls.Add(name, 2, i);
        //        tableListProduct.Controls.Add(priceIn, 3, i);
        //        tableListProduct.Controls.Add(priceOut, 4, i);
        //        tableListProduct.Controls.Add(amount, 5, i);

        //        tableListProduct.Size = new Size(tableListProduct.Width, tableListProduct.Height + 60);

        //        count.Text = "" + i++;
        //        id.DataBindings.Add(new Binding("Text", item, "maSP"));
        //        name.DataBindings.Add(new Binding("Text", item, "tenSP"));
        //        priceIn.DataBindings.Add(new Binding("Text", item, "giaNhap"));
        //        priceOut.DataBindings.Add(new Binding("Text", item, "giaBan"));
        //        amount.DataBindings.Add(new Binding("Text", item, "soLuong"));

        //        count.Anchor = AnchorStyles.Left;
        //        id.Anchor = AnchorStyles.Left;
        //        name.Anchor = AnchorStyles.Left;
        //        priceIn.Anchor = AnchorStyles.Left;
        //        priceOut.Anchor = AnchorStyles.Left;
        //        amount.Anchor = AnchorStyles.Left;

        //        count.Font = new Font(count.Font.FontFamily, 10);
        //        id.Font = new Font(count.Font.FontFamily, 10);
        //        name.Font = new Font(count.Font.FontFamily, 10);
        //        priceIn.Font = new Font(count.Font.FontFamily, 10);
        //        priceOut.Font = new Font(count.Font.FontFamily, 10);
        //        amount.Font = new Font(count.Font.FontFamily, 10);
        //        //amount.Anchor = AnchorStyles.None;
        //        //amount.Anchor = AnchorStyles.Right;

        //        tableListProduct.RowCount += 1;
        //        tableListProduct.RowStyles.Add(new RowStyle(SizeType.Absolute, 60f));
        //    }
        //}

        #region
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

        private void OpenForm(Form f, string type = null)
        {
            if (type == "o")
            {
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    LoadProduct();
                }
            }
            else
            {
                this.Tag = f;
                f.Show();
                this.Close();

            }
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // createNewCell();
        }

        public static int checkAddDelFix = 0;

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            checkAddDelFix = 1;
            OpenForm(new AddProduct(), "o");
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            checkAddDelFix = 2;
            OpenForm(new AddProduct(), "o");
        }

        private void fProduct_Load(object sender, EventArgs e)
        {
            navForAdmin.Visible = false;
            LoadProduct();
            dtgvListProduct.Columns[0].ReadOnly = true;
            dtgvListProduct.Columns[2].ReadOnly = true;
            dtgvListProduct.Columns[8].ReadOnly = true;
        }

        private void nav_purchase_Click(object sender, EventArgs e)
        {
            //OpenForm(new fTransaction());
        }

        private void getFilterLoai(Panel p)
        {
            if (p.Visible == false)
                p.Visible = true;
            else
                p.Visible = false;
        }

        //private void dtgvListProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //if (e.RowIndex < 0)
        //{
        //    return;
        //}
        //dtgvListProduct.Rows[e.RowIndex].Selected = true;
        //}

        private void dtgvListProduct_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string productName = dtgvListProduct.CurrentRow.Cells[1].Value.ToString();
                if (MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm {productName}?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int amount = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.SanPham SET TrangThai = 0 WHERE MaSP = {this.dtgvListProduct.CurrentRow.Cells[0].Value}");
                    if (amount > 0)
                    {
                        MessageBox.Show($"Xóa thành công sản phẩm {productName}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProduct();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void dtgvListProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //dtgvListProduct.MultiSelect = false;

            int indexColumn = dtgvListProduct.CurrentCell.ColumnIndex;
            //MessageBox.Show(indexColumn.ToString());

            int result = 0;

            try
            {
                switch (indexColumn)
                {
                    case 1:
                        result = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.SanPham SET TenSP = N'{dtgvListProduct.CurrentRow.Cells[1].Value}' WHERE MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                        break;
                    case 3:
                        result = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.SanPham SET GiaNhap = {dtgvListProduct.CurrentRow.Cells[3].Value} WHERE MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                        break;
                    case 4:
                        result = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.SanPham SET Gia ={dtgvListProduct.CurrentRow.Cells[4].Value} WHERE MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                        break;
                    case 5:
                        result = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.SanPham SET DonViTinh = N'{dtgvListProduct.CurrentRow.Cells[5].Value}' WHERE MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                        break;
                    case 6:
                        result = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.SanPham SET SoLuongTon = {dtgvListProduct.CurrentRow.Cells[6].Value} WHERE MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                        break;
                    case 7:
                        result = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.SanPham SET XuatXu = N'{dtgvListProduct.CurrentRow.Cells[7].Value}' WHERE MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                        break;
                    case 9:
                        result = DataProvider.Instance.ExecuteNonQuery($"UPDATE dbo.SanPham SET MoTa = N'{dtgvListProduct.CurrentRow.Cells[9].Value}' WHERE MaSP = {dtgvListProduct.CurrentRow.Cells[0].Value}");
                        break;
                    default:
                        break;
                }

                if (result > 0)
                {
                    LoadProduct();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //MessageBox.Show("Vui lòng nhấn 1 trong các nút sau khi kết thúc chỉnh sửa!\n\n\t1. Enter\n\t2. Tab\n\t3. sCác phím mũi tên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            dtgvListProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SELECT SP.MaSP AS [ID], TenSP AS [Tên sản phẩm], PL.TenPL AS [Phân loại], GiaNhap AS [Giá nhập], Gia AS [Giá bán], DonViTinh AS [Đơn vị tính], SoLuongTon AS [Số lượng tồn], XuatXu AS [Xuất xứ], DT.TenDT AS [Nhà cung cấp], SP.MoTa AS [Mô tả] FROM dbo.SanPham SP, dbo.DoiTac DT, dbo.PhanLoaiSP PL WHERE SP.NhaCC = DT.MaDT AND SP.PhanLoai = PL.MaPL AND SP.TrangThai = 1 AND dbo.USF_ConvertToUnsign(TenSP) LIKE dbo.USF_ConvertToUnsign(N'{txtSearchProduct.Text}') + '%'");
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
                dtgvListProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SELECT SP.MaSP AS [ID], TenSP AS [Tên sản phẩm], PL.TenPL AS [Phân loại], GiaNhap AS [Giá nhập], Gia AS [Giá bán], DonViTinh AS [Đơn vị tính], SoLuongTon AS [Số lượng tồn], XuatXu AS [Xuất xứ], DT.TenDT AS [Nhà cung cấp], SP.MoTa AS [Mô tả] FROM dbo.SanPham SP, dbo.DoiTac DT, dbo.PhanLoaiSP PL WHERE SP.NhaCC = DT.MaDT AND SP.PhanLoai = PL.MaPL AND PL.TenPL = N'{cbContent.Text}'");
            }
            else if (cbInitial.SelectedItem.Equals("Đối tác") == true)
            {
                dtgvListProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SELECT SP.MaSP AS [ID], TenSP AS [Tên sản phẩm], PL.TenPL AS [Phân loại], GiaNhap AS [Giá nhập], Gia AS [Giá bán], DonViTinh AS [Đơn vị tính], SoLuongTon AS [Số lượng tồn], XuatXu AS [Xuất xứ], DT.TenDT AS [Nhà cung cấp], SP.MoTa AS [Mô tả] FROM dbo.SanPham SP, dbo.DoiTac DT, dbo.PhanLoaiSP PL WHERE SP.NhaCC = DT.MaDT AND SP.PhanLoai = PL.MaPL AND DT.TenDT = N'{cbContent.Text}'");
            }
        }

        private void btnDisableFilter_Click(object sender, EventArgs e)
        {
            cbInitial.Text = "";
            cbContent.DataSource = null;
            dtgvListProduct.DataSource = DataProvider.Instance.ExecuteQuery($"SELECT SP.MaSP AS [ID], TenSP AS [Tên sản phẩm], PL.TenPL AS [Phân loại], GiaNhap AS [Giá nhập], Gia AS [Giá bán], DonViTinh AS [Đơn vị tính], SoLuongTon AS [Số lượng tồn], XuatXu AS [Xuất xứ], DT.TenDT AS [Nhà cung cấp], SP.MoTa AS [Mô tả] FROM dbo.SanPham SP, dbo.DoiTac DT, dbo.PhanLoaiSP PL WHERE SP.NhaCC = DT.MaDT AND SP.PhanLoai = PL.MaPL AND SP.TrangThai = 1");
        }

        private void fProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
