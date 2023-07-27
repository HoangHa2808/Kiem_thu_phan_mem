using CosmeticLib;
using CosmeticLib.DAO;
using System.Data;

namespace QuanLyCuaHangMyPhamTests
{
    public class SellTests
    {
        [Theory]
        [InlineData("69000 đ")]
        [InlineData("69000,00 đ")]
        [InlineData("69.000,00 đ")]
        public void ConvertPrice(string price)
        {
            Assert.Equal(69000, fSell.ConvertPrice(price));
        }

        [Fact]
        public void ShowListProduct()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT MaSP AS [ID], TenSP AS [Tên], Gia AS [Giá], SoLuongTon AS [Số Lượng tồn], MoTa AS [Mô tả] FROM dbo.SanPham WHERE TrangThai = 1 AND SoLuongTon > 0");
            Assert.NotNull(data);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShowBillInfor(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT SP.TenSP, CT.SoLuong, SP.Gia, CT.ThanhTien FROM dbo.CTHoaDon CT, dbo.SanPham SP WHERE CT.MaSP = SP.MaSP AND CT.MaHD = {id}");
            Assert.Equal(true, data.Rows.Count > 0);
        }

        [Fact]
        public void ShowQueueBill()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT MaHD, TenDT, NgayLap FROM dbo.HoaDon HD, dbo.DoiTac DT WHERE NgayTT IS NULL AND HD.Loai = 1 AND HD.MaKH = DT.MaDT AND DT.Loai = 0");
            Assert.NotNull(data);
        }
    }
}
