using BankApp;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MS_BankAccountTests
{
    [TestClass]
    public class BankAccountDebitTests
    {
        /// <summary>
        /// Verifies that debiting a valid amount updates the account balance correctly.
        /// </summary>
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            double creditLimit = 0.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        /// <summary>
        /// Verifies that debiting a negative amount throws an ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            double creditLimit = 0.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act & Assert
            try
            {
                account.Debit(debitAmount);
                Assert.Fail("The expected ArgumentOutOfRangeException was not thrown!");
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
            }
        }

        /// <summary>
        /// Verifies that debiting an amount greater than the account balance throws an ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 100.00;
            double creditLimit = 0.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act & Assert
            try
            {
                account.Debit(debitAmount);
                Assert.Fail("The expected ArgumentOutOfRangeException was not thrown!");
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
            }
        }

        /// <summary>
        /// Verifies that debiting the maximum double value reduces the account balance to zero.
        /// </summary>
        [TestMethod]
        public void Debit_WithMaxDoubleValue_ShouldReduceBalanceToZero()
        {
            // Arrange
            double beginningBalance = double.MaxValue;
            double debitAmount = double.MaxValue;
            double creditLimit = 0.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act
            account.Debit(debitAmount);

            // Assert
            double expected = 0.0;
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Balance should be zero after debiting double.MaxValue");
        }

        /// <summary>
        /// Verifies that debiting the smallest positive value updates the account balance correctly.
        /// </summary>
        [TestMethod]
        public void Debit_WithSmallestPositiveValue_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = double.Epsilon; // Smallest positive value
            double expected = beginningBalance - debitAmount;
            double creditLimit = 0.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account balance not updated correctly with smallest positive debit amount.");
        }

        /// <summary>
        /// Verifies that debiting an account that is frozen throws an exception.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAccountIsFrozen_ShouldThrowException()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 5.00;
            double creditLimit = 0.00; // Assuming a credit limit for the test
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, creditLimit);

            // Simulate freezing the account
            typeof(BankAccount)
                .GetMethod("FreezeAccount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(account, null);

            // Act & Assert
            try
            {
                account.Debit(debitAmount);
                Assert.Fail("The expected Exception was not thrown!");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Account is frozen");
            }
        }
    }
}
