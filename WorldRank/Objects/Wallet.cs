using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Enums;

namespace WorldRank.Objects
{
    public class Wallet
    {
        public decimal Balance { get; private set; }
        public CurrencyEnums Currency { get; set; }
        public bool IsBlocked { get; set; }
        
    }
}
