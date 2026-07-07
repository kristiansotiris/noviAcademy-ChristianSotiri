using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Enums;

namespace WorldRank.Interfaces
{
    public interface IWallet
    {
        decimal Balance { get; }
        Currency Currency { get; }
        bool IsBlocked { get; set; }

        void Deposit(decimal amount);
        void Withdraw(decimal amount);
    }
}
