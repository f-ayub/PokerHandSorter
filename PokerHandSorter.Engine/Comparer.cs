using PokerHandSorter.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PokerHandSorter.Constants;
using handSorter.Engine;

namespace PokerHandSorter.Engine
{
    public class Comparer
    {
        Assessor assessor = new Assessor();

        /// <summary>
        /// Compares All Hands for the players and updates the Wins property of the winning Player by +1
        /// </summary>
        /// <param name="players"></param>
        public void CompareHands(params Player[] players)
        {
            //assign hand type to each hand
            players.ToList().ForEach(
                    player => player.Hands.ToList().ForEach(
                        hand => hand.handType = assessor.AssignHandType(hand)
                        )
                    );

            for (int i = 0; i < players[0].Hands.Count; i++)
            {
                List<Hand> handsInGame = new List<Hand>();

                handsInGame.Add(players[0].Hands[i]);
                handsInGame.Add(players[1].Hands[i]);

                //Sort in ascending order by hand type.
                handsInGame.Sort((comparisonItem1, comparisonItem2) =>
                comparisonItem1.handType.CompareTo(comparisonItem2.handType));

                //Group by hand type.
                var handTypeGroups = handsInGame.GroupBy(comparisonItem =>
                comparisonItem.handType).ToList();

                //Compare hands in groups.
                int rank = 0;
                handTypeGroups.ForEach(handTypeGroup =>
                {
                    //Get comparison items in this group.
                    var comparisonItemsInGroup = handTypeGroup.ToList();

                    //Rank must be incremented for every group.
                    rank++;

                    //Process single hand group.
                    if (comparisonItemsInGroup.Count == 1)
                        comparisonItemsInGroup[0].rank = rank;

                    //Process multi hand group.
                    else
                    {
                        //Sort in descending order by winning hand. 
                        comparisonItemsInGroup.Sort((comparisonItem1, comparisonItem2) => -1 *
                        CompareHandsOfSameType(
                        comparisonItem1, comparisonItem2, comparisonItem1.handType));

                        //Assign current rank to first hand in group if hand type is not Royal Flush.
                        if(comparisonItemsInGroup.ToList().Count(hand=> hand.handType == HandType.RoyalFlush) != 2)
                            comparisonItemsInGroup[0].rank = rank;                        
                    }
                });

                //update Wins by + 1 for the winning player accordingly
                if (players[0].Hands[i].CompareTo(players[1].Hands[i]) > 0)
                    players[0].Wins += 1;
                else if (players[0].Hands[i].CompareTo(players[1].Hands[i]) < 0)
                    players[1].Wins += 1;                
            }
        }

        /// <summary>
        /// Compares hands of same type to determine a winner 
        /// </summary>
        /// <param name="player1_hand"></param>
        /// <param name="player2_hand"></param>
        /// <param name="handtype"></param>
        /// <returns></returns>
        int CompareHandsOfSameType(Hand player1_hand, Hand player2_hand, HandType handtype)
        {
            int result;
            //Compare the hands.
            switch (handtype)
            {
                case HandType.StraightFlush:
                case HandType.Straight:
                    return CompareHandsOfSameType(player1_hand.Cards[4], player2_hand.Cards[4]);
                case HandType.Flush:
                case HandType.HighCard:
                    for (int i = 4; i >= 0; i--)
                    {
                        result = CompareHandsOfSameType(player1_hand.Cards[i], player2_hand.Cards[i]);
                        
                        if (result != 0)
                            return result;
                    }
                    return 0;
                case HandType.RoyalFlush:
                    return 0;
            }

            
            List<Card> hand1SameCardSet1, hand1SameCardSet2;
            assessor.CardsOfSameValue(
            player1_hand, out hand1SameCardSet1, out hand1SameCardSet2);

            List<Card> hand2SameCardSet1, hand2SameCardSet2;
            assessor.CardsOfSameValue(
            player2_hand, out hand2SameCardSet1, out hand2SameCardSet2);
            

            switch (handtype)
            {
                case HandType.FourOfAKind:
                case HandType.FullHouse:
                case HandType.ThreeOfAKind:
                case HandType.Pair:
                    
                    result = CompareHandsOfSameType(
                        hand1SameCardSet1[0], hand2SameCardSet1[0]);
                    if (result != 0)
                        return result;

                    for (int i = 4; i >= hand1SameCardSet1.Count; i--)
                    {
                        result = CompareHandsOfSameType(player1_hand.Cards[i], player2_hand.Cards[i]);

                        if (result != 0)
                            return result;
                    }

                    return 0;

                case HandType.TwoPairs:
                    
                    result = CompareHandsOfSameType(
                        hand1SameCardSet1[0], hand2SameCardSet1[0]);
                    if (result != 0)
                        return result;

                    
                    result = CompareHandsOfSameType(
                        hand1SameCardSet2[0], hand2SameCardSet2[0]);
                    if (result != 0)
                        return result;

                    var remainingCard1 = player1_hand.Cards.Where(card => !hand1SameCardSet1.Contains(card) && !hand1SameCardSet2.Contains(card)).ToList()[0];
                    var remainingCard2 = player2_hand.Cards.Where(card => !hand2SameCardSet1.Contains(card) && !hand2SameCardSet2.Contains(card)).ToList()[0];
                    return CompareHandsOfSameType(remainingCard1, remainingCard2);
            }
            
            throw new Exception("Hand comparison failed for one of the hands.");
        }


        int CompareHandsOfSameType(Card player1_card, Card player2_card)
        {
            //Get card's int values.
            int player1_cardIntValue = (int)player1_card.value;
            int pplayer2_cardIntValue = (int)player2_card.value;

            //Compare and return result.
            return player1_cardIntValue > pplayer2_cardIntValue ? 1 :
                player1_cardIntValue == pplayer2_cardIntValue ? 0 : -1;
        }
    }
}
