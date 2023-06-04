using System.Collections.Generic;

namespace CardGameUI
{
    public class BlackJackDeck : Deck, IDeck
    {
        public BlackJackDeck()
        {
            CreateDeck();
            ShuffleDeck();
        }

        public override List<PlayingCardModel> DealCards()
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();
            for (int i = 0; i < 2; i++)
            {
                output.Add(DrawOneCard());
            }

            return output;
        }
        
        //public PlayingCardModel RequestCard()
        //{
        //    return DrawOneCard();
        //}
        public List<PlayingCardModel> RequestCards(List<PlayingCardModel> cardsToDiscard)
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();
           
            output.Add(DrawOneCard());
            
            return output;
        }
    }
}
