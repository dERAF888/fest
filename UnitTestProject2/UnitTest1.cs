using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            {

                decimal ticketPrice = 3500;
                decimal Balance = 4000;
                decimal expectedBalance = Balance - ticketPrice;


                decimal actualBalance = Balance - ticketPrice;


                Assert.AreEqual(expectedBalance, actualBalance, "Остаток баланса");
            }

        }
    }
}
    

