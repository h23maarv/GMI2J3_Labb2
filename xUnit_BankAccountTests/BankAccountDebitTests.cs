using BankApp;

namespace xUnit_BankAccountTests
{
    public class BankAccountDebitTests
    {
        [Fact]
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
            Assert.Equal(expected, actual);
        }

        [Fact]
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
                Assert.Contains(BankAccount.DebitAmountLessThanZeroMessage, e.Message);
            }
        }

        [Fact]
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
                Assert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, e.Message);
            }
        }

        [Fact]
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
            Assert.Equal(0.0, account.Balance, 3); // precision på 3 decimaler
        }

        [Fact]
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
            Assert.Equal(expected, actual);
        }

        [Fact]
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
                Assert.Contains(e.Message, "Account is frozen");
            }
        }
    }
}