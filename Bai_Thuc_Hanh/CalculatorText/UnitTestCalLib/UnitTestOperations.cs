using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestCalLib
{
    [TestClass]
    public class UnitTestOperations
    {
        [TestMethod]
        public void SummationTest()
        {
            // Chuẩn bị bộ dữ liệu
            var testDatas = new TestData[]
            {
                new TestData(1.1,1.2,2.3),
                new TestData(2,3,5),
                new TestData(2.4,3.9,6.3),
                new TestData(10, 100, 110)
            };
            Operations op = new Operations();
            //Phương thức Test
            foreach (var item in testDatas)
            {
                // Gọi phương thức phép Cộng
                var res = op.Summation(item.a, item.b);
                //Kiểm tra kết quả
                Assert.AreEqual(item.result, res);
            }
        }

        [TestMethod]
        public void SubtractionTest()
        {
            // Chuẩn bị bộ dữ liệu
            var testDatas = new TestData[]
            {
                new TestData(3.1,1.1,2.0),
                new TestData(4,3,1),
                new TestData(2.4,1.9,0.5),
                new TestData(10, 3, 7)
            };
            Operations op = new Operations();
            //Phương thức Test
            foreach (var item in testDatas)
            {
                // Gọi phương thức phép Trừ
                var res = op.Subtraction(item.a, item.b);
                //Kiểm tra kết quả
                Assert.AreEqual(item.result, res);
            }
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            // Chuẩn bị bộ dữ liệu
            var testDatas = new TestData[]
            {
                new TestData(2,1.1,2.2),
                new TestData(4,3,12),
                new TestData(2.5,1,2.5),
                new TestData(1, 3, 3)
            };
            Operations op = new Operations();
            //Phương thức Test
            foreach (var item in testDatas)
            {
                // Gọi phương thức phép Nhân
                var res = op.Multiplication(item.a, item.b);
                //Kiểm tra kết quả
                Assert.AreEqual(item.result, res);
            }
        }

        [TestMethod]
        public void DivisionTest()
        {
            // Chuẩn bị bộ dữ liệu
            var testDatas = new TestData[]
            {
                new TestData(3,3,1),
                new TestData(4,2,2),
                new TestData(2.5,2,1.25),
                new TestData(10, 2, 5)
            };
            Operations op = new Operations();
            //Phương thức Test
            foreach (var item in testDatas)
            {
                // Gọi phương thức phép Chia
                var res = op.Division(item.a, item.b);
                //Kiểm tra kết quả
                Assert.AreEqual(item.result, res);
            }
        }
    }
}