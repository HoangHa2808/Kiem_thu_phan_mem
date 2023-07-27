using CosmeticLib.DAO;
using System.Data;

namespace QuanLyCuaHangMyPhamTests
{
    public class BillTests
    {
        [Fact]
        public void ShowBill()
        {
            DateTime fromDate = new DateTime(2023, 4, 1);
            DateTime toDate = new DateTime(2023, 4, 30);
            DataTable data = DataProvider.Instance.ExecuteQuery($"SET DATEFORMAT dmy SELECT MaHD AS [Mã HD], TK.Ten AS [Người lập HD], DT.TenDT AS [Tên đối tác | Khách hàng], NgayLap AS [Ngày lập HD], NgayTT AS [Ngày thanh toán HD], GiamGia AS [Giảm giá], TongTien AS [Tổng tiền] FROM dbo.HoaDon HD, dbo.TaiKhoan TK, dbo.DoiTac DT WHERE NgayTT IS NOT NULL AND NgayLap >= '{fromDate.ToString("dd/MM/yyyy").Split(' ')[0]}' AND NgayLap <= '{toDate.ToString("dd/MM/yyyy").Split(' ')[0]}' AND HD.TrangThai = 1 AND HD.MaTK = TK.ID AND HD.MaKH = DT.MaDT");
            Assert.NotNull(data);
        }
    }
}
