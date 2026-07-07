using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Enums;

namespace WorldRank.Interfaces
{
    public interface IWallet
    {
        decimal Balance { get; }
        CurrencyEnums Currency { get; }
        bool IsBlocked { get; set; }
    }
}
