using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    /// <summary>
    /// The main entry point of the application. Demonstrates the usage of the BankAccount class.
    /// </summary>
    static class Program
    {
        public static void Main(string[] args)
        {
            BankAccount ba = new BankAccount("Mr. Bryan Walton", 11.99, 1000.00);

            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine($"Current balance is ${ba.Balance}");

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }

    public class BankAccount
    {
        private readonly string m_customerName;

        private double m_balance;

        private bool m_frozen = false;

        private readonly double credit_limit; // Credit limit

        public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";
        public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";
        public const string CreditAmountExceedsBalanceMessage = "Credit amount exceeds balance";
        public const string CreditAmountLessThanZeroMessage = "Credit amount is less than zero";
        public const string DebitMaxDoubleValueMessage = "Debit exceeds max limit";
        public const string CreditMaxDoubleValueMessage = "Credit exceeds max limit";


        private int failedAttempts;
        private const int MaxFailedAttempts = 3;

        /// <summary>
        /// Initializes a new instance of the BankAccount class with a customer name, initial balance, and credit limit.
        /// </summary>
        public BankAccount(string customerName, double balance, double allowed_credit_limit)
        {
            m_customerName = customerName; // Set the customer name
            m_balance = balance; // Set the initial balance
            credit_limit = allowed_credit_limit; // Set the credit limit
            failedAttempts = 0; // Initialize failed attempts
        }

        public string CustomerName
        {
            get { return m_customerName; }
        }

        public double Balance
        {
            get { return m_balance; }
        }

        /// <param name="amount"></param>

        public void Debit(double amount)
        {
            if (m_frozen)
            {
                throw new InvalidOperationException("Account is frozen");
            }

            if (amount > m_balance)
            {
                failedAttempts++;
                if (failedAttempts >= MaxFailedAttempts)
                {
                    FreezeAccount();
                }
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage);
            }

            if (amount < 0)
            {
                failedAttempts++;
                if (failedAttempts >= MaxFailedAttempts)
                {
                    FreezeAccount();
                }
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);
            }

            failedAttempts = 0; // Reset on success
            m_balance -= amount;
        }

        public void Credit(double amount)
        {
            if (this.m_frozen)
            {
                throw new InvalidOperationException("Account is frozen");
            }

            if (amount < 0)
            {
                failedAttempts++;
                if (failedAttempts >= MaxFailedAttempts)
                {
                    FreezeAccount();
                }
                throw new ArgumentOutOfRangeException("amount", amount, CreditAmountLessThanZeroMessage);
            }

            if (amount > this.credit_limit)
            {
                failedAttempts++;
                if (failedAttempts >= MaxFailedAttempts)
                {
                    FreezeAccount();
                }
                throw new ArgumentOutOfRangeException("amount", amount, CreditAmountExceedsBalanceMessage);
            }

            failedAttempts = 0; // Reset on success
            this.m_balance += amount;
        }

        private void FreezeAccount()
        {
            m_frozen = true;
        }

        private void UnfreezeAccount()
        {
            m_frozen = false;
        }

        public void SetAccountStatus()
        {
            if (this.m_frozen)
            {
                UnfreezeAccount();
            }
            else
            {
                FreezeAccount();
            }
        }
    }
}
