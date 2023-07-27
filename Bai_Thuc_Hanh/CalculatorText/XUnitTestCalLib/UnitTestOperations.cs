using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalLib;

namespace XUnitTestCalLib
{
    public class UnitTestOperations
    {
        public Operations op = new Operations();

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-10, 2, -8)]
        [InlineData(100, 72, 172)]
        public void SummationTest(int a, int b, int expectedRes)
        {
            // Chạy phép toán
            var res = op.Summation(a, b);
            // Kiểm tra kết quả
            Assert.True(expectedRes == res);
        }

        [Theory]
        [InlineData(10, 2, 8)]
        [InlineData(25, 20, 5)]
        [InlineData(100, 72, 28)]
        public void SubtractionTest(int a, int b, int expectedRes)
        {
            // Chạy phép toán
            var res = op.Subtraction(a, b);
            // Kiểm tra kết quả
            Assert.True(expectedRes == res);
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(4, 9, 36)]
        [InlineData(10, 42, 420)]
        public void MultiplicationTest(int a, int b, int expectedRes)
        {
            // Chạy phép toán
            var res = op.Multiplication(a, b);
            // Kiểm tra kết quả
            Assert.True(expectedRes == res);
        }

        [Theory]
        [InlineData(9, 3, 3)]
        [InlineData(6, 2, 3)]
        [InlineData(100, 50, 2)]
        public void DivisionTest(int a, int b, double expectedRes)
        {
            // Chạy phép toán
            var res = op.Division(a, b);
            // Kiểm tra kết quả
            Assert.True(expectedRes == res);
        }
    }
}