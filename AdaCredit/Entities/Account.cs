using AdaCredit.UseCases;
using Bogus;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Entities
{
    public sealed class Account
    {
        public string Number { get; private set; }
        public string Branch { get; private set; }
        public decimal Balance { get; private set; }

        public Account(string accountNumber)
        {
            Number = accountNumber;
            Branch = "0001";
            Balance = 0;
        }
        public Account(string accountNumber, decimal balance)
        {
            Number = accountNumber;
            Balance = balance;
            Branch = "0001";
        }

        public bool Deposit(decimal amount)
        {
            Balance = Balance + amount;
            UpdateAccount.Execute(this);
            return true;
        }

        public bool Withdraw(decimal fee, decimal amount)
        {
            var valueDiscounted = fee + amount;
            if(Balance >= valueDiscounted)
            {
                Balance = Balance - valueDiscounted;
                UpdateAccount.Execute(this);
                return true;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return $"{Branch},{Number},{Balance.ToString(new CultureInfo("en-US"))}";
        }
    }
}
