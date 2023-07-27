using CosmeticLib;

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
    }
}
