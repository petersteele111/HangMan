using System;
using System.Media;
using static HangMan.GameLogic;
using static HangMan.HangMan;

namespace HangMan
{
    class Program
    {
        /**********************************************************************
         * Title: HangMan
         * Application Type: Console
         * Description: Play HangMan in in the console!
         * Author: Peter Steele
         * Date Created: 11/16/2019
         * Last Modified: 11/18/2019
         *********************************************************************/

        /// <summary>
        /// Main entry point to the HangMan program 
        /// </summary>
        /// <param name="_0">Ignored Parameter</param>
        static void Main(string[] _0)
        {
            Player p1 = new Player();
            GameLogic game = new GameLogic();
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Chiptronical.wav";
            player.PlayLooping();

            bool EndGame = false;
            while (!EndGame)
            {
                StartGame(p1, game);
                EndGame = Continue(p1);
            }
            DisplayClosingScreen(p1);
            DisplayContinuePrompt(45, 27);
        }

        #region Program Flow

        /// <summary>
        /// Entry point to the HangMan game
        /// Instantiates two objects, one for the Player class, and one for the GameLogic class
        /// Displays the proper screens for the player
        /// Grabs a random word from the array, Converts it to all asterisks in a List<char>
        /// Explodes the HiddenWord into a List<char>
        /// Do-While loop to run the game
        /// 
        /// </summary>
        /// <param name="_0">Ignored Parameter</param>
        static void StartGame(Player p1, GameLogic game)
        {
            const int GUESSES_LEFT = 6;
            if (p1.GamesPlayed < 1)
            {
                DisplayWelcomeScreen(p1);
                DisplayConsoleUI(p1);
                DisplayMiddleBar();
                Console.SetCursorPosition(62, 5);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Please enter your name: ");
                Console.ForegroundColor = ConsoleColor.White;
                string userName = Console.ReadLine();
                p1.Name = userName;
            }
            else
            {
                p1.CharsGuessed.Clear();
                p1.UserWon = false;
                p1.GuessesLeft = GUESSES_LEFT;
            }

            game.HiddenWord = GetWord(game.RandomWords); // gets random word
            game.Asterisks = ConvertToAsterisk(game.HiddenWord); // converts the hidden word to asterisks and displays it to the screen
            game.Characters = ExplodeHiddenWord(game.HiddenWord); // separates the hidden word out as individual characters

            do
            {
                if (p1.GuessesLeft != 0 && !p1.UserWon)
                {
                    DisplayMainScreen(game.Asterisks, p1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    DisplayHangMan(p1);
                    DisplayMiddleBar();
                    char userResponse = UserGuess(game.Asterisks, p1); // checks for the user response to make sure it is a char and not a string
                    var (asterisks, _) = UpdateLists(CheckUserGuess(game.HiddenWord, userResponse, p1), userResponse, game.Characters, game.Asterisks);
                    p1.UserWon = CheckIfUserWon(asterisks);
                    if (p1.UserWon)
                    {
                        DisplayGameWon(p1, game.Asterisks);
                        DisplayContinuePrompt(2, 27);
                    }
                    else
                    {
                        DisplayContinuePrompt();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    DisplayHangMan(p1);
                    DisplayContinuePrompt();
                    DisplayConsoleUI(p1);
                    DisplayGameLost(p1, game);
                    break;
                }
            } while (!p1.UserWon);
        }

        /// <summary>
        /// Asks the user if they would like to continue playing
        /// </summary>
        /// <param name="p1">Player Object</param>
        /// <returns>Returns true if the player wants to quit, and false if they want to keep playing</returns>
        static bool Continue(Player p1)
        {
            DisplayConsoleUI(p1);
            DisplayMiddleBar();
            Console.SetCursorPosition(62, 5);
            Console.Write("Do you wish to play again? (Y)es or (N)o: ");
            string userResponse = Console.ReadLine().ToLower().Trim();
            if (userResponse == "y" || userResponse == "yes")
            {
                return false;
            }
            else
            {
                WriteScoreToFile(p1);
                DisplayLeaderboard(p1);
                DisplayContinuePrompt(2, 27);
                return true;
            }
        }

        #endregion
    }
}
