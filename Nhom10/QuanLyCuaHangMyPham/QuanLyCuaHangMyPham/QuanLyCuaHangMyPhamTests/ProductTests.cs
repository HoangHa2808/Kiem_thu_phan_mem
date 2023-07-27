using CosmeticLib.DAO;
using System.Data;

namespace QuanLyCuaHangMyPhamTests
{
    public class ProductTests
    {
        [Fact]
        public void LoadProduct()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT SP.MaSP AS [ID], TenSP AS [Tên sản phẩm], PL.TenPL AS [Phân loại], GiaNhap AS [Giá nhập], Gia AS [Giá bán], DonViTinh AS [Đơn vị tính], SoLuongTon AS [Số lượng tồn], XuatXu AS [Xuất xứ], DT.TenDT AS [Nhà cung cấp], SP.MoTa AS [Mô tả] FROM dbo.SanPham SP, dbo.DoiTac DT, dbo.PhanLoaiSP PL WHERE SP.NhaCC = DT.MaDT AND SP.PhanLoai = PL.MaPL AND SP.TrangThai = 1");
            Assert.NotNull(data);
        }
    }
}
