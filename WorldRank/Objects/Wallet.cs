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

        
    }
}
