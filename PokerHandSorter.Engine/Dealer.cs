using System;
using System.Collections.Generic;
using PokerHandSorter.Classes;
using PokerHandSorter.Data;
using PokerHandSorter.Constants;
using handSorter.Engine;
using System.Linq;

namespace PokerHandSorter.Engine
{
    public class Dealer
    {
        public FileReader fr { get; set; }

        public Dealer(string FilePath)
        {
            this.fr = new FileReader(FilePath);
        }

        /// <summary>
        /// Assigns cards and hands to players
        /// </summary>
        /// <param name="players"></param>
        public void DealCardsToPlayers(params Player[] players)
        {
            string HandsWithCards = fr.ReadStreamOfHands();

            int handNum = 0;

            foreach(string handWithCards in HandsWithCards.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                int cardNum = 0;

                players.ToList().ForEach(player => player.Hands.Add(new Hand()));

                foreach (string cardInfo in handWithCards.Split(' '))
                {
                    Card card = new Card((Suit)Enum.Parse(typeof(Suit), cardInfo.Substring(cardInfo.Length - 1)),
                                        (Value)Enum.Parse(typeof(Value), cardInfo.Substring(0, cardInfo.Length - 1)));

                    players[cardNum / PokerHandSorter.Constants.Constants.CardsPerPlayer].Hands[handNum].Cards.Add(card);                   

                    cardNum++;
                }

                handNum++;
            }           

        }
    }
}
