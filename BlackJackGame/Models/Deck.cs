using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJackGame.Models
{
    public class Deck
    {
        public static Stack<Card> CreateDeck()
        {
            // stack to store cards
            Stack<Card> CardDeck = new Stack<Card>();

            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Card card = new Card();

                    // get card values
                    if (i <= 8)
                    {                        
                        card.NumVal = i + 2;
                        card.FaceVal = (i + 2).ToString();
                    }
                    else
                    {                        
                        switch (i)
                        {
                            case 9:
                                card.NumVal = 10;
                                card.FaceVal = "J";
                                break;
                            case 10:
                                card.NumVal = 10;
                                card.FaceVal = "Q";
                                break;
                            case 11:
                                card.NumVal = 10;
                                card.FaceVal = "K";
                                break;
                            case 12:
                                card.NumVal = 11;
                                card.FaceVal = "A";
                                break;
                        }
                    }
                    // generate suits
                    switch (j)
                    {
                        case 0:
                            // Clubs
                            card.Suit = "C";
                            break;
                        case 1:
                            // Diamonds
                            card.Suit = "D";
                            break;
                        case 2:
                            // Hearts
                            card.Suit = "H";
                            break;
                        case 3:
                            // Spades
                            card.Suit = "S";
                            break;
                    }
                    // get card images
                    card.ImageSrc = $"img/cards/{card.FaceVal}{card.Suit}.png";

                    // adds card to deck
                    CardDeck.Push(card);
                }
            }

            return CardDeck;
        }

        public static Stack<Card> ShuffleDeck(Stack<Card> cardDeck)
        {
            // shuffling deck
            var cardList = cardDeck.ToArray();

            // clear original deck
            cardDeck.Clear();

            // shuffling cards inside list
            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int firstCard = rnd.Next(0, 52);
                int secondCard = rnd.Next(0, 52);
                if (firstCard != secondCard)
                {
                    var temp = cardList[firstCard];
                    cardList[firstCard] = cardList[secondCard];
                    cardList[secondCard] = temp;
                }
            }

            // repopulate shuffled deck
            foreach (var card in cardList)
            {
                cardDeck.Push(card);
            }
            return cardDeck;
        }
    }
}