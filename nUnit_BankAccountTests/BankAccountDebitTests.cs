using System;
using NUnit.Framework;
using BankApp;
namespace nUnit_BankAccountTests
{
    [TestFixture]
    public class BankAccountDebitTests
    {

        /// <summary>
        /// Verifies that debiting a valid amount updates the account balance correctly.
        /// </summary>
        [Test]
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
            Assert.That(actual, Is.EqualTo(expected).Within(0.001), "Account not debited correctly");
        }

        /// <summary>
        /// Verifies that debiting a negative amount throws an ArgumentOutOfRangeException.
        /// </summary>
        [Test]
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
                StringAssert.Contains(BankAccount.DebitAmountLessThanZeroMessage, e.Message);
            }
        }

        /// <summary>
        /// Verifies that debiting an amount greater than the account balance throws an ArgumentOutOfRangeException.
        /// </summary>
        [Test]
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
                StringAssert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, e.Message);
            }
        }

        /// <summary>
        /// Verifies that debiting the maximum double value reduces the account balance to zero.
        /// </summary>
        [Test]
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
            Assert.That(account.Balance, Is.EqualTo(0.0).Within(0.001), "Balance should be zero after debiting double.MaxValue");
        }

        /// <summary>
        /// Verifies that debiting the smallest positive value updates the account balance correctly.
        /// </summary>
        [Test]
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
            Assert.That(actual, Is.EqualTo(expected).Within(0.001), "Account balance not updated correctly with smallest positive debit amount.");
        }

        /// <summary>
        /// Verifies that debiting an account that is frozen throws an exception.
        /// </summary>
        [Test]
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
                StringAssert.Contains("Account is frozen", e.Message);
            }
        }
    }
}