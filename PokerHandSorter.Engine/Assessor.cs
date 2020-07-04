using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PokerHandSorter.Classes;
using PokerHandSorter.Data;
using PokerHandSorter.Constants;

namespace handSorter.Engine
{
    public class Assessor
    {
        /// <summary>
        /// Returns the hand type for a hand as per available HandType Enum
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public HandType AssignHandType(Hand hand)
        {
            // Check if all cards are of same suit
            bool allOfSameSuit = hand.Cards.GroupBy(card => card.suit).Count() == 1;
            
            // Check if All five cards are in consecutive value order
            bool straight = IsStraight(hand);

            // Check if Ace is included. This is used to determine RoyalFlush or StraightFlush
            bool IncludesAce = hand.Cards.Exists(card => card.value == Value.A);

            if (allOfSameSuit && straight && IncludesAce)
                return HandType.RoyalFlush;

            if (allOfSameSuit && straight && !IncludesAce)
                return HandType.StraightFlush;            
            
            //Get cards of same value for checking other hand types
            List<Card> sameValueCards1, sameValueCards2;
            CardsOfSameValue(hand, out sameValueCards1, out sameValueCards2);
            
            if (sameValueCards1.Count == 4)
                return HandType.FourOfAKind;

            if (sameValueCards1.Count + sameValueCards2.Count == 5)
                return HandType.FullHouse;

            if (allOfSameSuit)
                return HandType.Flush;

            if (straight)
                return HandType.Straight;

            if (sameValueCards1.Count == 3)
                return HandType.ThreeOfAKind;

            if (sameValueCards1.Count + sameValueCards2.Count == 4)
                return HandType.TwoPairs;

            if (sameValueCards1.Count == 2)
                return HandType.Pair;

            return HandType.HighCard;
        }

        /// <summary>
        /// Checks if all cards are in consecutive value order
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        bool IsStraight(Hand hand)
        {

            hand.Cards.Sort((Card1, Card2) =>
            Card1.value.CompareTo(Card2.value));
           
            return              
                hand.Cards[0].value == hand.Cards[1].value - 1 &&
                hand.Cards[1].value == hand.Cards[2].value - 1 &&
                hand.Cards[2].value == hand.Cards[3].value - 1 &&
                hand.Cards[3].value == hand.Cards[4].value - 1;
        }

        /// <summary>
        /// Gets cards of same value type
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="sameValueCards1"></param>
        /// <param name="sameValueCards2"></param>
        public void CardsOfSameValue(Hand hand, out List<Card> sameValueCards1, out List<Card> sameValueCards2)
        {
            int index = 1;
            sameValueCards1 = CardsOfSameValue(hand, ref index);
            sameValueCards2 = CardsOfSameValue(hand, ref index);
        }

        /// <summary>
        /// Used to separate multiple same value type pairs
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        List<Card> CardsOfSameValue(Hand hand, ref int index)
        {
            List<Card> sameValueCards = new List<Card>();
            for (; index < Constants.CardsPerPlayer; index++)
            {
                Card currentCard = hand.Cards[index - 1];
                Card nextCard = hand.Cards[index];
                if (currentCard.value == nextCard.value)
                {
                    if (sameValueCards.Count == 0)
                        sameValueCards.Add(currentCard);
                    sameValueCards.Add(nextCard);
                }
                else if (sameValueCards.Count > 0)
                {
                    index++;
                    break;
                }
            }
            return sameValueCards;
        }
}
}
