using CosmeticLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangMyPhamTests
{
    public class TransactionTests
    {
        public class SellTests
        {
            [Theory]
            [InlineData("69000 đ")]
            [InlineData("69000,00 đ")]
            [InlineData("69.000,00 đ")]
            public void ConvertPrice(string price)
            {
                Assert.Equal(69000, fTransaction.ConvertPrice(price));
            }
        }
    }
}
