using System;
using System.Text;
using System.Threading.Tasks;

namespace CardGameUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to play Poker (P) or Blackjack (B): ");
            string input = Console.ReadLine();
            IDeck deck;

            if (input.ToLower() == "p")
            {
                deck = new PokerDeck();
            }
            else
            {
                deck = new BlackJackDeck();
            }

            var hand = deck.DealCards();
            foreach (var card in hand)
            {
                Console.WriteLine($"{card.Value.ToString()} of { card.Suit.ToString() } ");
            }

            Console.ReadLine();
        }
    }
}
