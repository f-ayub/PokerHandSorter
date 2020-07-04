using System;

namespace PokerHandSorter.Constants
{
    /// <summary>
    /// Clubs = C
    /// Diamonds = D
    /// Hearts = H
    /// Spades = S
    /// </summary>
    public enum Suit
    {
        C = 1,
        D = 2,
        H = 3,
        S = 4
    }


    /// <summary>
    /// Values of cards from 2-9 and below :
    /// Ten = T
    /// Jack = J
    /// Queen = Q
    /// King = K
    /// Ace = A
    /// </summary>
    public enum Value
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        T = 10,
        J = 11,
        Q = 12,
        K = 13,
        A = 14,
    }

    /// <summary>
    /// Hand Types as per rank in ascending order
    /// </summary>
    public enum HandType
    {

        HighCard = 1,
        Pair = 2,
        TwoPairs = 3,
        ThreeOfAKind = 4,
        Straight = 5,
        Flush = 6,
        FullHouse = 7,
        FourOfAKind = 8,
        StraightFlush = 9,
        RoyalFlush = 10
    }

    public class Constants
    {
        public static int CardsPerPlayer = 5;
    }
}
