using System;
using PokerHandSorter.Constants;

namespace PokerHandSorter.Classes
{
    public class Card
    {
        public Suit suit { get; set; }
        public Value value { get; set; }

        public Card(Suit _suit,Value _value)
        {
            this.suit = _suit;
            this.value = _value;
        }
    }
}
