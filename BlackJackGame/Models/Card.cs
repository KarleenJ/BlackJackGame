using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJackGame.Models
{
    public class Card
    {
        public string Suit { get; set; }
        public string FaceVal { get; set; }
        public int NumVal { get; set; }
        public string ImageSrc { get; set; }
    }
}