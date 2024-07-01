using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using fest;

namespace UnitTestProject1
{
    [TestClass]
    public class TicketPurchaseTests
    {
        [TestMethod]
        public void BalanceForTicket()
        { 
            decimal ticketPrice = 3000;
            decimal Balance = 4500;

            bilet form = new bilet();

            bool Result = Balance >= ticketPrice;


            Assert.IsTrue(Result, "Покупка успешна");
        }
        [TestMethod]
        public void BalanceForTicketparttwo()
        {

            decimal ticketPrice = 3000;
            decimal Balance = 1500;


            bool Result = Balance >= ticketPrice;


            Assert.IsFalse(Result, "Недостаточно средств");
        }
    }
}

    
    

