using System;
using WorldRank.Interfaces;

namespace WorldRank.Objects
{
    public class Player(string name, int score = 0) : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public int Score { get; private set; } = score;
        int IPlayer.Score { get => Score; set => Score = value; }

       
    }
}
