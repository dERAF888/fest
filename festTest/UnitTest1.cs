using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using Microsoft.Analytics.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using fest;

namespace festTest
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
 
        }

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {

            bilet form = new bilet();
            decimal initialBalance;
            decimal rechargeAmount = 500;

            // Act
            form.textBox1.Text = rechargeAmount.ToString(); // Ввод суммы пополнения
            form.button1_Click(null, null); // Вызов метода для пополнения

            // Assert
            decimal expectedBalance = initialBalance + rechargeAmount;
            Assert.Equal(expectedBalance.ToString(), form.label2.Text); // Проверка, что баланс увеличился на сумму пополнения
        }

        }
    }
}
