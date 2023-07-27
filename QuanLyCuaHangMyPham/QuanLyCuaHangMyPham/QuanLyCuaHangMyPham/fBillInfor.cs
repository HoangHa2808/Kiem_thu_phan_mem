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
    public partial class fBillInfor : Form
    {
        public BillDTO ID { get; set; }

        public fBillInfor(BillDTO billDTO)
        {
            InitializeComponent();
            this.ID = billDTO;
        }

        private void fBillInfor_Load(object sender, EventArgs e)
        {
            if (ID.Type == false)
            {
                label3.Text = "Tên đối tác";
            }
            txtID.Text = ID.Id.ToString();
            txtAccountName.Text = ID.AccountName.ToString();
            txtPartnerName.Text = ID.PartnerName.ToString();
            txtDateCreate.Text = ID.DateCreate.ToString();
            txtDateCheckOut.Text = ID.DateCheckOut.ToString();
            txtDiscount.Text = ID.Discount.ToString();
            txtTotalPrice.Text = ID.TotalPrice.ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
            dtgvBillInfor.DataSource = DataProvider.Instance.ExecuteQuery($"SELECT TenSP AS [Tên sản phẩm], Gia AS [Đơn giá], SoLuong AS [Số lượng], SoLuong * Gia AS [Thành tiền] FROM dbo.CTHoaDon CT, dbo.SanPham SP WHERE MaHD = {ID.Id} AND CT.MaSP = SP.MaSP");
        }
    }
}
