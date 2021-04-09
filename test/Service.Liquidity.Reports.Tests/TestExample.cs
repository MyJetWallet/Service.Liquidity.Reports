using System;
using System.Globalization;
using NUnit.Framework;

namespace Service.Liquidity.Reports.Tests
{
    public class TestExample
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            var a = 1.0;
            var b = 0.1;
            var c = 0.02;

            var price = 100;

            var dt = (a + b + c)*price;
            var res = Math.Round((a + b + c)*price, 8);

            var r1 = Math.Round(dt, 2, MidpointRounding.ToPositiveInfinity);

            var r2 = Math.Round(Math.Round(dt, 4), 2, MidpointRounding.ToPositiveInfinity);


            Console.WriteLine(dt);
            Console.WriteLine(r1);
            Console.WriteLine(r2);
            Console.WriteLine(res);

            Assert.AreNotEqual(112, dt);
            Assert.AreEqual(112, res);
        }
    }
}
