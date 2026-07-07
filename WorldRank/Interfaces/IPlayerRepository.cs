using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank.Interfaces
{
    public interface IPlayerRepository
    {
        void AddPlayer(IPlayer player);
        IPlayer? FindPlayer(int playerId);
        void DeletePlayer(int playerId);
        IReadOnlyDictionary<int, List<IPlayer>> GroupPlayersByScore();
    }
}
