using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerHandSorter.Engine;
using PokerHandSorter.Classes;


namespace PokerHandSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide path of the input file.");
                    Console.ReadKey();
                }
                else
                {
                    Dealer dealer = new Dealer(string.Join(" ",args));
                    Comparer comparer = new Comparer();

                    Player player1 = new Player(), player2 = new Player();

                    // Read input from file and create hands with cards for each player
                    dealer.DealCardsToPlayers(player1, player2);

                    // Compare the each hand to determine a winner
                    comparer.CompareHands(player1, player2);

                    // Output
                    Console.WriteLine(string.Format("Player 1 : {0}", player1.ToString()));
                    Console.WriteLine(string.Format("Player 2 : {0}", player2.ToString()));

                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("An error occured : {0}",ex.Message));
                Console.ReadKey();
            }

        }

        
    }
}
