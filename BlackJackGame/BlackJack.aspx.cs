using BlackJackGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BlackJackGame
{
    public partial class BlackJack : System.Web.UI.Page
    {
        // create a deck
        static Stack<Card> deck = new Stack<Card>();

        // arrays for hands
        static IList<Card> dealerHand = new List<Card>();
        static IList<Card> playerHand = new List<Card>();

        // variables
        static bool isStay = false;
        static double playerBalance = 0;
        static double bet = 0;
        protected void Page_Load(object sender, EventArgs e)
        {            
            NewGame();
        }

        //Hide and load elements on game start
        protected void NewGame()
        {
            // get shuffled deck
            deck = Deck.ShuffleDeck(Deck.CreateDeck());
                     
            playerBalance = 100;
            lblBalanceAmt.Text = playerBalance.ToString();
            BtnBet.Visible = true;
            BtnNewHand.Visible = false;

            
        }

        protected void btnBet_Click(object sender, EventArgs e)
        {
            // get bet value
            bet = Int32.Parse(ddlBet.SelectedValue.ToString());

            // deduct bet from the account
            playerBalance -= bet;

            if (playerBalance >= 0)
            {
                // show remaining account balance on the screen
                lblBalanceAmt.Text = playerBalance.ToString();

                // Deal a new hand
                NewHand();
               
                //show and hide controls
                ShowPlayerScoreControls();
                HideDealerScoreControls();
                ShowGameControls();
                BtnBet.Visible = false;
                BtnNewHand.Visible = false;
                BtnExit.Visible = true;
            }
            else lblResult.Text = "Insufficient funds";
           
        }

        protected void btnHit_Click(object sender, EventArgs e)
        {
            // addcard to hand
            playerHand.Add(deck.Pop());
            // show cards
            makeCards(dealerHand, playerHand);
            //  show player score
            tbPScore.Text = GetScore(playerHand).ToString();
            // check if player busts
            if (GetScore(playerHand) > 21)
            {
                HandOver();
                lblResult.Text = $"You bust!";
            }            
        }
        protected void BtnStay_Click(object sender, EventArgs e)
        {
            // get dealer and player scores
            int playerScore = GetScore(playerHand);
            int dealerScore = GetScore(dealerHand);

            // determine the winner
            GamePlay(playerScore, dealerScore);

            // call GameOver() Function
            HandOver();
        }

        protected void BtnNewHand_Click(object sender, EventArgs e)
        {
            NewHand();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveCards();
            HideDealerScoreControls();
            HidePlayerScoreControls();
            HideGameControls();
            HideResultControls();
            BtnExit.Visible = false;
            BtnBet.Visible = true;
        }

        protected void makeCards(IList<Card> dealerHand, IList<Card> playerHand)
        {
            switch (dealerHand.Count)
            {
                case 2:
                    imgDealerCard1.ImageUrl = isStay ? dealerHand[0].ImageSrc : "img/cards/gray_back.png";
                    imgDealerCard2.ImageUrl = dealerHand[1].ImageSrc;
                    imgDealerCard3.ImageUrl = "";
                    imgDealerCard4.ImageUrl = "";
                    imgDealerCard5.ImageUrl = "";
                    break;
                case 3:
                    imgDealerCard1.ImageUrl = dealerHand[0].ImageSrc;
                    imgDealerCard2.ImageUrl = dealerHand[1].ImageSrc;
                    imgDealerCard3.ImageUrl = dealerHand[2].ImageSrc;
                    imgDealerCard4.ImageUrl = "";
                    imgDealerCard5.ImageUrl = "";
                    break;
                case 4:
                    imgDealerCard1.ImageUrl = dealerHand[0].ImageSrc;
                    imgDealerCard2.ImageUrl = dealerHand[1].ImageSrc;
                    imgDealerCard3.ImageUrl = dealerHand[2].ImageSrc;
                    imgDealerCard4.ImageUrl = dealerHand[3].ImageSrc;
                    imgDealerCard5.ImageUrl = "";
                    break;
                case 5:
                    imgDealerCard1.ImageUrl = dealerHand[0].ImageSrc;
                    imgDealerCard2.ImageUrl = dealerHand[1].ImageSrc;
                    imgDealerCard3.ImageUrl = dealerHand[2].ImageSrc;
                    imgDealerCard4.ImageUrl = dealerHand[3].ImageSrc;
                    imgDealerCard5.ImageUrl = dealerHand[4].ImageSrc;
                    break;
            }

            switch (playerHand.Count)
            {
                case 2:
                    imgPlayerCard1.ImageUrl = playerHand[0].ImageSrc;
                    imgPlayerCard2.ImageUrl = playerHand[1].ImageSrc;
                    imgPlayerCard3.ImageUrl = "";
                    imgPlayerCard4.ImageUrl = "";
                    imgPlayerCard5.ImageUrl = "";
                    break;
                case 3:
                    imgPlayerCard1.ImageUrl = playerHand[0].ImageSrc;
                    imgPlayerCard2.ImageUrl = playerHand[1].ImageSrc;
                    imgPlayerCard3.ImageUrl = playerHand[2].ImageSrc;
                    imgPlayerCard4.ImageUrl = "";
                    imgPlayerCard5.ImageUrl = "";
                    break;
                case 4:
                    imgPlayerCard1.ImageUrl = playerHand[0].ImageSrc;
                    imgPlayerCard2.ImageUrl = playerHand[1].ImageSrc;
                    imgPlayerCard3.ImageUrl = playerHand[2].ImageSrc;
                    imgPlayerCard4.ImageUrl = playerHand[3].ImageSrc;
                    imgPlayerCard5.ImageUrl = "";
                    break;
                case 5:
                    imgPlayerCard1.ImageUrl = playerHand[0].ImageSrc;
                    imgPlayerCard2.ImageUrl = playerHand[1].ImageSrc;
                    imgPlayerCard3.ImageUrl = playerHand[2].ImageSrc;
                    imgPlayerCard4.ImageUrl = playerHand[3].ImageSrc;
                    imgPlayerCard5.ImageUrl = playerHand[4].ImageSrc;
                    break;
            }
        }

        protected void RemoveCards()
        {
            imgPlayerCard1.ImageUrl = "";
            imgPlayerCard2.ImageUrl = "";
            imgPlayerCard3.ImageUrl = "";
            imgPlayerCard4.ImageUrl = "";
            imgPlayerCard5.ImageUrl = "";

            imgDealerCard1.ImageUrl = "";
            imgDealerCard2.ImageUrl = "";
            imgDealerCard3.ImageUrl = "";
            imgDealerCard4.ImageUrl = "";
            imgDealerCard5.ImageUrl = "";
        }

        protected int GetScore(IList<Card> Hand)
        {
            int score = 0;
            bool hasAces = false;
            // var for Ace index
            int aceIndex = 0;

            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].NumVal == 11)
                {
                    hasAces = true;
                    aceIndex = i;
                }
                score += Hand[i].NumVal;
            }
            // if score > 21 => change value of the Ace
            if (score > 21 && hasAces)
            {
                Hand[aceIndex].NumVal = 1;
                score = GetScore(Hand);
            }
            return score;
        }
        protected void NewHand()
        {
                      

            // clear hands
            dealerHand.Clear();
            playerHand.Clear();
            
            //hide everything
            HideResultControls();
            HideDealerScoreControls();
            HidePlayerScoreControls();
            HideGameControls();
            
            DealCards();

            // set variables
            isStay = false;
            tbPScore.Text = GetScore(playerHand).ToString();
            lblBalanceAmt.Text = playerBalance.ToString();
        }
        
        protected void GamePlay(int playerScore, int dealerScore)
        {
            // check if score is over 21
            if (playerScore > 21)
            {
                lblResult.Text = $"Bust! Sorry :(";
                return;
            }

            //keeps adding cards while score <17
            while (dealerScore < 17)
            {
                dealerHand.Add(deck.Pop());
                dealerScore = GetScore(dealerHand);
                makeCards(dealerHand, playerHand);
            }            
          

            if (playerScore == 21)
            {
                if (dealerScore == 21)
                {
                    playerBalance += bet;
                    lblResult.Text = $"It's a tie!";
                    return;
                }
                else
                {
                    double win = bet * 1.5;
                    playerBalance += win;
                    lblResult.Text = $"You won!";
                    return;
                }
            }

            if (dealerScore == 21 && playerScore != 21)
            {
                lblResult.Text = $"Dealer Wins!";
                return;
            }

            // check if the dealer bust
            if (dealerScore > 21 && playerScore <= 21)
            {
                double win = bet * 2;
                playerBalance += win;
                lblResult.Text = $" Dealer busts! You won!";
                return;
            }

            if (playerScore > dealerScore)
            {
                double win = bet * 2;
                playerBalance += win;
                lblResult.Text = $"You won!";
                return;
            }
            else if (playerScore == dealerScore)
            {
                playerBalance += bet;
                lblResult.Text = $"It's a tie!";
                return;
            }
            else
            {
                lblResult.Text = $"Better luck next time!";
                return;
            }
        }

        protected void DealCards()
        {
            for (int i = 0; i < 2; i++)
                {
                    dealerHand.Add(deck.Pop());
                    playerHand.Add(deck.Pop());
                }
                makeCards(dealerHand, playerHand);               
        }

        protected void HandOver()
        {
            tbDScore.Text = GetScore(dealerHand).ToString();
            isStay = true;

            ShowResultControls();
            ShowNewHandControls();
            ShowDealerScoreControls();
            HideGameControls();
            makeCards(dealerHand, playerHand);                      
        }  

        protected void ShowResultControls()
        {
            lblResult.Visible = true;
         
        }

        protected void HideResultControls()
        {
            lblResult.Visible = false;
        }

        protected void ShowPlayerScoreControls()
        {
            lblPScore.Visible = true;
            tbPScore.Visible = true;
        }

        protected void HidePlayerScoreControls()
        {
            lblPScore.Visible = false;
            tbPScore.Visible = false;
        }

        protected void ShowDealerScoreControls()
        {
            lblDScore.Visible = true;
            tbDScore.Visible = true;
        }

        protected void HideDealerScoreControls()
        {
            lblDScore.Visible = false;
            tbDScore.Visible = false;
        }

        protected void HideGameControls()
        {
            BtnHit.Visible = false;
            BtnStay.Visible = false;
        }

        protected void ShowGameControls()
        {
            BtnHit.Visible = true;
            BtnStay.Visible = true;
        }

        protected void HideNewHandControls()
        {
            BtnNewHand.Visible = false;
        }

        protected void ShowNewHandControls()
        {
            BtnNewHand.Visible = true;
        }       
    }
}