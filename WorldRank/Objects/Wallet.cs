using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Enums;
using WorldRank.Interfaces;

namespace WorldRank.Objects
{
    public class Wallet(Currency currency) : IWallet
    {
        public decimal Balance { get; private set; }
        public Currency Currency { get; } = currency;
        public bool IsBlocked { get; set; }


        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit amount must be positive.", nameof(amount));

            if (IsBlocked) throw new InvalidOperationException("This wallet is blocked");

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.", nameof(amount));

            if (IsBlocked)
                throw new InvalidOperationException("Cannot withdraw from a blocked wallet.");

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds.");

            Balance -= amount;
        }
    }
}
