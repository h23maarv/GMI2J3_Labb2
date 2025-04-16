﻿using MS_BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS_BankAccountTests
{
    [TestClass]
    public class BankAccountCreditTests
    {
        [TestMethod]
        [Timeout(2000)]  // Milliseconds
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 100.00;
            double creditAmount = 50.00;
            double expected = 150.00;
            double creditLimit = 200.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act
            account.Credit(creditAmount);

            // Assert
            Assert.AreEqual(expected, account.Balance, 0.001, "Credit did not update balance correctly");
        }

        [TestMethod]
        public void Credit_WithNegativeAmount_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 100.00;
            double creditAmount = -25.00;
            double creditLimit = 200.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act + Assert
            try
            {
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "amount");
                return;
            }

            Assert.Fail("Expected exception for negative credit amount was not thrown.");
        }

        [TestMethod]
        public void Credit_WithZeroAmount_BalanceUnchanged()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = 0.00;
            double expected = beginningBalance;
            double creditLimit = 200.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account balance should remain unchanged when crediting zero.");
        }

        [TestMethod]
        public void Credit_WithMaxDoubleValue_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditLimit = 200.00; // Assuming a credit limit for the test
            double creditAmount = creditLimit;
            double expected = beginningBalance + creditAmount;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account balance not updated correctly with maximum credit amount.");
        }

        [TestMethod]
        public void Credit_WithSmallestPositiveValue_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = double.Epsilon; // Smallest positive value
            double expected = beginningBalance + creditAmount;
            double creditLimit = 200.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account balance not updated correctly with smallest positive credit amount.");
        }

        [TestMethod]
        public void Credit_WhenAccountIsFrozen_ShouldThrowException()
        {
            // Arrange
            double creditLimit = 200.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Frozen", 100.00, creditLimit);

            // Freezes the account using reflection (since FreezeAccount is private)
            typeof(BankAccount)
                .GetMethod("FreezeAccount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .Invoke(account, null);

            // Act + Assert
            try
            {
                account.Credit(10.00);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Account is frozen");
                return;
            }

            Assert.Fail("Expected exception for frozen account was not thrown.");
        }
    }
}
