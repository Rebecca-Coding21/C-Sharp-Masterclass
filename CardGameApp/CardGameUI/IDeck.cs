using System.Collections.Generic;

namespace CardGameUI
{
    public interface IDeck
    {
        List<PlayingCardModel> DealCards();
        List<PlayingCardModel> RequestCards(List<PlayingCardModel> cardsToDiscard);
    }
}