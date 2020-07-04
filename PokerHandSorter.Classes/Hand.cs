using PokerHandSorter.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandSorter.Classes
{
    public class Hand : IComparable
    {
        public List<Card> Cards { get; set; }

        public HandType handType { get; set; }

        public int rank { get; set; }

        public Hand()
        {
            this.Cards = new List<Card>();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Hand otherHand = obj as Hand;
            if (otherHand != null)
                return this.rank.CompareTo(otherHand.rank);
            else
                throw new ArgumentException("Invalid Hand");
        }
    }
}
