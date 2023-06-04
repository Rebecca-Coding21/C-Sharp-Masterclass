using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameUI
{
    public abstract class Deck
    {
        protected List<PlayingCardModel> fullDeck = new List<PlayingCardModel>();
        protected List<PlayingCardModel> drawPile = new List<PlayingCardModel>();   //Zugstapel
        protected List<PlayingCardModel> discardPile = new List<PlayingCardModel>(); //Abwurf/Ablegestabel

        protected void CreateDeck()
        {
            fullDeck.Clear(); //Clear memory from the old Deck, list free again

            for (int suit = 0; suit < 4; suit++)
            {
                for (int val = 0; val < 13; val++)
                {
                    fullDeck.Add(new PlayingCardModel { Suit = (CardSuit)suit, Value = (CardValue)val });
                }
            }
        }

        public virtual void ShuffleDeck()
        {
            //var rand = new Random();
            //var randomList = imagesEasy.OrderBy(x => rand.Next()).ToList(); -> rand.next creates next random number 
            var rnd = new Random();
            drawPile = fullDeck.OrderBy(x => rnd.Next()).ToList(); //randomly ordering
        }

        //public int GetOrder(PlayingCard card)
        //{
        //    if (card.Suit == CardSuit.Clubs)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        //-> same as:
        //x => (means call the following code)  rnd.Next()

        public abstract List<PlayingCardModel> DealCards();

        protected virtual PlayingCardModel DrawOneCard()
        {
            PlayingCardModel output = drawPile.Take(1).First();  //put first item into PlayingCard (is only one item), doesn't remove card from drawpile
            drawPile.Remove(output);
            return output;
        }
    }
}
