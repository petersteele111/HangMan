using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    class Player
    {
        public bool UserWon { get; set; }
        public int GuessesLeft { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int GamesPlayed { get; set; }
        public List<char> CharsGuessed { get; set; }

        public Player()
        {
            UserWon = false;
            GuessesLeft = 6;
            Score = 0;
            CharsGuessed = new List<char>();
        }
    }



}
