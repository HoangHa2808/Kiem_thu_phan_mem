using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestCalLib
{
    public class TestData
    {
        public double a;
        public double b;
        public double result;

        public TestData(double a, double b, double res)
        {
            this.a = a;
            this.b = b;
            this.result = res;
        }
    }
}