using BankApp;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnit_BankAccountTests
{

    public class BankAccountCreditTests
    {
        [Fact]
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
            Assert.Equal(expected, account.Balance);
        }

        [Fact]
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
                Assert.Contains("amount", e.Message);
                return;
            }

            Assert.Fail("Expected exception for negative credit amount was not thrown.");
        }

        [Fact]
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
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Credit_WithMaxDoubleValue_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 0;
            double creditLimit = double.MaxValue - beginningBalance; // Assuming a credit limit for the test
            double creditAmount = creditLimit;
            double expected = beginningBalance + creditAmount;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.Equal(expected, actual, 3);
        }

        [Fact]
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
            Assert.Equal(expected, actual);
        }

        [Fact]
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
                Assert.Contains(e.Message, "Account is frozen");
                return;
            }

            Assert.Fail("Expected exception for frozen account was not thrown.");
        }
    }
}
