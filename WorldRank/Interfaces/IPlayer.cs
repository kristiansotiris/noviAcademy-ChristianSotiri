using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank.Interfaces
{
    public interface IPlayer
    {
        public int Id { get; }
        public string Name { get; set; }
        public int Score { get; set; }

    }
}
