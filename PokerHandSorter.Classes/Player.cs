using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandSorter.Classes
{
    public class Player
    {
        public List<Hand> Hands { get; set; }

        public int Wins { get; set; }

        public Player()
        {
            this.Wins = 0;
            this.Hands = new List<Hand>();
        }

        public override string ToString()
        {
            return Wins.ToString();
        }

    }
}
