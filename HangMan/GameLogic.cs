using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace HangMan
{
    class GameLogic
    {
        #region Class Properties

        public List<char> Asterisks { get; set; }
        public List<char> Characters { get; set; }
        public string[] RandomWords { get; set; }
        public string HiddenWord { get; set; }

        #endregion

        #region Class Constructor

        public GameLogic()
        {
            RandomWords = new string[] { "dairy", "changling", "deer", "maverick", "ibiza", "general", "calisthenics", "goal", "horses", "sarcasm", "pirouline", "rental", "capstone", "programming", "mic", "kappa", "black", "bacon", "strawberry", "led" };
        }

        #endregion

        #region GameSetup

        /// <summary>
        /// Gets a random word from the Array
        /// </summary>
        /// <param name="randomWords">Name of Array containing random words</param>
        /// <returns></returns>
        public static string GetWord(string[] randomWords)
        {
            Random ranNumberGenerator = new Random();
            int randomNumber;
            randomNumber = ranNumberGenerator.Next(0, 20);

            string hiddenWord = randomWords[randomNumber];
            return hiddenWord;
        }

        /// <summary>
        /// Converts the randomly selected word to all asterisks
        /// </summary>
        /// <param name="hiddenWord">Name of String of random word selected</param>
        /// <returns></returns>
        public static List<char> ConvertToAsterisk(string hiddenWord)
        {

            List<char> asterisks = new List<char>();
            for (int i = 0; i < hiddenWord.Length; i++)
            {
                asterisks.Add('*');
            }
            return asterisks;
        }

        /// <summary>
        /// Explode hiddenWord into individual characters and add them to a List<char>
        /// </summary>
        /// <param name="hiddenWord">Name of String of random hidden word selected</param>
        /// <returns></returns>
        public static List<char> ExplodeHiddenWord(string hiddenWord)
        {
            List<char> hiddenWordChars = new List<char>();
            hiddenWordChars.AddRange(hiddenWord);
            return hiddenWordChars;
        }

        #endregion

        #region GameLogic

        /// <summary>
        /// Promt the user to guess a letter in the word
        /// </summary>
        /// <returns>Returns char of user response</returns>
        public static char UserGuess(List<char> asterisks, Player p1)
        {
            char validatedResponse = ValidateUserGuess("Guess a letter: ", p1, asterisks);
            return validatedResponse;
        }

        /// <summary>
        /// Prompts the user for an input, and validates if the input is correct
        /// If it is not, an error message is displayed, and the question is asked again
        /// If the user fails three times to input a valid reponse, the program tells the user
        /// that they have failed too many times and exits the application to prevent infinite looping
        /// </summary>
        /// <param name="prompt">Prompt to display to the user</param>
        /// <param name="p1">Player Object that tracks player progress</param>
        /// <param name="asterisks">List<char> that tracks the progress of the word being guessed</param>
        /// <returns>Returns a char of the validated user input</returns>
        public static char ValidateUserGuess(string prompt, Player p1, List<char> asterisks)
        {
            bool IsValid = false;
            const int MAX_TRIES = 3;
            int attemptsMade = 1;
            char validatedResponse = '?';
            Console.CursorVisible = true;
            while (!IsValid)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                HangMan.DisplayHangMan(p1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(62, 19);
                Console.Write(prompt);
                Console.ForegroundColor = ConsoleColor.Green;
                string userResponse = Console.ReadLine().Trim().ToLower();

                if (userResponse.Length > 1)
                {
                    IsValid = false;
                }
                else
                {
                    IsValid = Regex.IsMatch(userResponse, @"(?i)^[a-z]{1}");
                }

                try
                {
                    Char.IsLetter(Convert.ToChar(userResponse));
                    validatedResponse = Convert.ToChar(userResponse);
                }
                catch (Exception)
                {
                    Console.SetCursorPosition(62, 20);
                    Console.WriteLine("ERROR: Input must be exactly one character!");
                }

                if (!IsValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(62, 21);
                    Console.WriteLine("Sorry, you did not input a valid character.");
                    DisplayContinuePrompt();
                    attemptsMade++;
                    DisplayMainScreen(asterisks, p1);
                    DisplayMiddleBar();
                }

                if (attemptsMade > MAX_TRIES)
                {
                    DisplayConsoleUI(p1, asterisks.Count);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(@"
  

          ______    _ _          _   _                                              _   _                     
         |  ____|  (_) |        | | | |                                            | | (_)                    
         | |__ __ _ _| | ___  __| | | |_ ___   ___    _ __ ___   __ _ _ __  _   _  | |_ _ _ __ ___   ___  ___ 
         |  __/ _` | | |/ _ \/ _` | | __/ _ \ / _ \  | '_ ` _ \ / _` | '_ \| | | | | __| | '_ ` _ \ / _ \/ __|
         | | | (_| | | |  __/ (_| | | || (_) | (_) | | | | | | | (_| | | | | |_| | | |_| | | | | | |  __/\__ \
         |_|  \__,_|_|_|\___|\__,_|  \__\___/ \___/  |_| |_| |_|\__,_|_| |_|\__, |  \__|_|_| |_| |_|\___||___/
                                                                             __/ |                            
                                                                            |___/                             
             ______      _ _   _               _   _            _____                                     
            |  ____|    (_) | (_)             | | | |          |  __ \                                    
            | |__  __  ___| |_ _ _ __   __ _  | |_| |__   ___  | |__) | __ ___   __ _ _ __ __ _ _ __ ___  
            |  __| \ \/ / | __| | '_ \ / _` | | __| '_ \ / _ \ |  ___/ '__/ _ \ / _` | '__/ _` | '_ ` _ \ 
            | |____ >  <| | |_| | | | | (_| | | |_| | | |  __/ | |   | | | (_) | (_| | | | (_| | | | | | |
            |______/_/\_\_|\__|_|_| |_|\__, |  \__|_| |_|\___| |_|   |_|  \___/ \__, |_|  \__,_|_| |_| |_|
                                        __/ |                                    __/ |                    
                                       |___/                                    |___/                     


");
                    DisplayContinuePrompt(2, 27);
                    Quit();
                }
            }
            return validatedResponse;
        }

        /// <summary>
        /// Checks the User Guess against the hidden word to see 
        /// if the user selected character matches any occurences in the hidden word
        /// </summary>
        /// <param name="hiddenWord">Name of String of random hidden word selected</param>
        /// <param name="userResponse">Name of Char of user response</param>
        /// <returns>Returns boolean if user response matched any occurence in the hidden word</returns>
        public static bool CheckUserGuess(string hiddenWord, char userResponse, Player p1)
        {
            if (p1.CharsGuessed.Contains(userResponse))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(62, 21);
                Console.WriteLine($"Sorry. {userResponse} has already been guessed");
                return false;
            }
            else if (hiddenWord.Contains(userResponse))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(62, 21);
                Console.WriteLine($"Yes! {userResponse} is in the word");
                Console.WriteLine();
                p1.CharsGuessed.Add(userResponse);
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(62, 21);
                Console.WriteLine($"Sorry. {userResponse} is not in the word");
                Console.WriteLine();
                p1.CharsGuessed.Add(userResponse);
                p1.GuessesLeft--;
                return false;
            }
        }

        /// <summary>
        /// Updates the Characters and Asterisks List's (List<char>)
        /// Loops through to each occurence of the user guess and matches it to the characters list.
        /// Takes the index of said character and deletes an asterisk inside of the Asterisk List at that index location
        /// Add's the user response character to the same index location in the Asterisk List
        /// Takes the index of the user response character in the Characters List and deletes it from the List
        /// Add's an asterisk (*) in that same index position inside the Character List to allow the next occurance of the character to be "first"
        /// Rinse and Repeat until all asterisks inside of the Asterisk List are updated with the user response character, and all characters inside of the Character List are updated with asterisks (*) in that characters position
        /// This allows each occurence of the user repsonse char to be find inside of the Lists
        /// </summary>
        /// <param name="userGuessChecked">Name of Boolean that validates the user response char was found inside of the character List</param>
        /// <param name="userResponse">Name of Char of user response</param>
        /// <param name="characters">Name of Characters List<char> that holds each character of the hidden word</char></param>
        /// <param name="asterisks">Name of the Asterisks List<char> that holds an asterisk for each character of the hidden word</char></param>
        /// <returns>Returns a tuple of two lists. Asterisks List<char> and Characters List<char></returns>
        public static (List<char> asterisks, List<char> characters) UpdateLists(bool userGuessChecked, char userResponse, List<char> characters, List<char> asterisks)
        {
            if (userGuessChecked)
            {
                while (characters.Contains(userResponse))
                {
                    int index = characters.IndexOf(userResponse);
                    asterisks.RemoveAt(index);
                    asterisks.Insert(index, userResponse);
                    characters.Remove(userResponse);
                    characters.Insert(index, '*');
                }
            }
            return (asterisks, characters);
        }

        /// <summary>
        /// Checks if the user has won the game
        /// Checks to see if the Asterisks List<char> contains any asterisks (*)
        /// If the Asterisks List<char> does not contain any asterisks (*), then the user has guessed all characters and thus guessed the word and won the game
        /// </summary>
        /// <param name="asterisks">Name of the Asterisks List<char> that holds an asterisk for each character of the hidden word<</param>
        /// <returns>Returns a Boolean to see if the user has won</returns>
        public static bool CheckIfUserWon(List<char> asterisks)
        {
            if (!asterisks.Contains('*'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Display Screens for Application

        /// <summary>
        /// Generate the main console for the UI
        /// </summary>
        public static void DisplayConsoleUI(Player p1, int WordLength = 0)
        {
            Console.SetWindowSize(120,29);
            // Setup UI for application
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < 120; i++)
            {
                Console.SetCursorPosition(0 + i, 0);
                Console.Write("-");
            }
            for (int i = 0; i < 120; i++)
            {
                Console.SetCursorPosition(0 + i, 4);
                Console.Write("-");
            }
            for (int i = 0; i < 120; i++)
            {
                Console.SetCursorPosition(0 + i, 28);
                Console.Write("-");
            }

            // Set UI Elements for Game
            Console.SetCursorPosition(10, 2);
            Console.Write($"Name: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(p1.Name);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(40, 2);
            Console.Write($"Word Length: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(WordLength);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(58, 2);
            Console.Write($"Score: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(p1.Score);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(70, 2);
            Console.Write($"Games Played: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(p1.GamesPlayed);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(90, 2);
            Console.Write($"Wrong Guesses Remaining: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(p1.GuessesLeft);

            Console.SetCursorPosition(0, 5);
        }

        /// <summary>
        /// Displays the middle bar of the application. This was done because when writing to the other half
        /// of the screen, if it was included with the GenerateConsoleUI method, there would be blank
        /// spaces introduced instead of a solid bar. Since C# doesn't have Z index or anything like that
        /// for a console, it is my hacky way around this issue.
        /// </summary>
        public static void DisplayMiddleBar()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < 23; i++)
            {
                Console.SetCursorPosition(60, 5 + i);
                Console.Write("|");
            }
        }

        /// <summary>
        /// Displays Intro Screen
        /// </summary>
        /// <param name="p1">Player object for UI parameters</param>
        public static void DisplayWelcomeScreen(Player p1)
        {
            DisplayConsoleUI(p1);
            DisplayMiddleBar();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(78, 13);
            Console.WriteLine("Welcome to HangMan v1.0");
            Console.SetCursorPosition(77, 16);
            Console.WriteLine("Developed By: Peter Steele");
            Console.SetCursorPosition(79, 18);
            Console.WriteLine("Created on: 11/18/2019");

            for (int i = 0; i < 17; i++)
            {

                Console.SetCursorPosition(0, 4);
                ConsoleColor[] colour = {ConsoleColor.Red,
                           ConsoleColor.White,
                           ConsoleColor.Yellow,
                           ConsoleColor.Magenta,
                           ConsoleColor.Blue,
                           ConsoleColor.DarkYellow,
                           ConsoleColor.Cyan,
                           ConsoleColor.Red,
                           ConsoleColor.DarkCyan,
                           ConsoleColor.DarkGreen,
                           ConsoleColor.DarkMagenta,
                           ConsoleColor.DarkRed,
                           ConsoleColor.DarkYellow,
                           ConsoleColor.Cyan,
                           ConsoleColor.Yellow,
                           ConsoleColor.White,
                           ConsoleColor.Green
                           };
                Console.ForegroundColor = colour[i];
                Console.Write(@"
       _    _                                         
      | |  | |                                        
      | |__| | __ _ _ __   __ _ _ __ ___   __ _ _ __  
      |  __  |/ _` | '_ \ / _` | '_ ` _ \ / _` | '_ \ 
      | |  | | (_| | | | | (_| | | | | | | (_| | | | |
      |_|  |_|\__,_|_| |_|\__, |_| |_| |_|\__,_|_| |_|
                           __/ |                      
                          |___/             
                       _    
                      | |          
                      | |__  _   _ 
                      | '_ \| | | |
                      | |_) | |_| |
                      |_.__/ \__, |
                              __/ |
                             |___/ 
    _____     _               _____ _            _      
   |  __ \   | |             / ____| |          | |     
   | |__) |__| |_ ___ _ __  | (___ | |_ ___  ___| | ___ 
   |  ___/ _ \ __/ _ \ '__|  \___ \| __/ _ \/ _ \ |/ _ \
   | |  |  __/ ||  __/ |     ____) | ||  __/  __/ |  __/
   |_|   \___|\__\___|_|    |_____/ \__\___|\___|_|\___|
");
                Thread.Sleep(300);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Displays the main screen for the user
        /// </summary>
        /// <param name="asterisks"></param>
        /// <param name="p1"></param>
        public static void DisplayMainScreen(List<char> asterisks, Player p1)
        {
            DisplayConsoleUI(p1, asterisks.Count);
            DisplayGameRules();
            DisplayHiddenWord(asterisks);
            Console.SetCursorPosition(62, 24);
            Console.Write("Guessed: ");
            foreach (var guess in p1.CharsGuessed)
            {
                Console.Write($"{guess},");
            }
        }

        /// <summary>
        /// Displays the user rules to the screen
        /// </summary>
        public static void DisplayGameRules()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(85, 5);
            Console.WriteLine("Game Rules");
            Console.SetCursorPosition(61, 8);
            Console.WriteLine("1. You must guess a single letter for the hidden word");
            Console.SetCursorPosition(61, 9);
            Console.WriteLine("2. Guess incorrectly, and a part of the hangman is drawn");
            Console.SetCursorPosition(61, 10);
            Console.WriteLine("3. Guess correctly and the word is updated with your guess");
            Console.SetCursorPosition(61, 11);
            Console.WriteLine("4. Fail 6 guesses, and lose the game");
            Console.SetCursorPosition(61, 12);
            Console.WriteLine("5. Guess all letters correctly and win!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Displays the hidden word to the screen
        /// </summary>
        /// <param name="asterisks"></param>
        public static void DisplayHiddenWord(List<char> asterisks)
        {
            Console.SetCursorPosition(62, 18);
            Console.Write("Word: ");
            foreach (var letter in asterisks)
            {
                Console.Write(letter);
            }
        }

        /// <summary>
        /// Displays the winning screen
        /// </summary>
        /// <param name="p1">Player Object that tracks if user won</param>
        /// <param name="asterisks">List<char> that displays the word</param>
        public static void DisplayGameWon(Player p1, List<char> asterisks)
        {
            DisplayContinuePrompt();
            DisplayConsoleUI(p1, asterisks.Count);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(@"
                    __          ___                        __          ___                       
                    \ \        / (_)                       \ \        / (_)                      
                     \ \  /\  / / _ _ __  _ __   ___ _ __   \ \  /\  / / _ _ __  _ __   ___ _ __ 
                      \ \/  \/ / | | '_ \| '_ \ / _ \ '__|   \ \/  \/ / | | '_ \| '_ \ / _ \ '__|
                       \  /\  /  | | | | | | | |  __/ |       \  /\  /  | | | | | | | |  __/ |   
                        \/  \/   |_|_| |_|_| |_|\___|_|        \/  \/   |_|_| |_|_| |_|\___|_|   
                                                                                                 
                         _____ _     _      _                _____  _                       
                        / ____| |   (_)    | |              |  __ \(_)                      
                       | |    | |__  _  ___| | _____ _ __   | |  | |_ _ __  _ __   ___ _ __ 
                       | |    | '_ \| |/ __| |/ / _ \ '_ \  | |  | | | '_ \| '_ \ / _ \ '__|
                       | |____| | | | | (__|   <  __/ | | | | |__| | | | | | | | |  __/ |   
                        \_____|_| |_|_|\___|_|\_\___|_| |_| |_____/|_|_| |_|_| |_|\___|_|   
                                                                                                                    
");
            Console.SetCursorPosition(2, 22);
            Console.Write("Word: ");
            foreach (var letter in asterisks)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(letter);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(2, 24);
            Console.WriteLine("Congratulations! You have guessed the word!");
            Console.SetCursorPosition(2, 25);
            Console.WriteLine("Thank you for playing.");
            p1.Score++;
            p1.GamesPlayed++;
        }

        public static void DisplayGameLost(Player p1, GameLogic game)
        {
            DisplayConsoleUI(p1, game.Asterisks.Count);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(@"
      

      
                                                  _____                         ____                 
                                                 / ____|                       / __ \                
                                                | |  __  __ _ _ __ ___   ___  | |  | |_   _____ _ __ 
                                                | | |_ |/ _` | '_ ` _ \ / _ \ | |  | \ \ / / _ \ '__|
                                                | |__| | (_| | | | | | |  __/ | |__| |\ V /  __/ |   
                                                 \_____|\__,_|_| |_| |_|\___|  \____/  \_/ \___|_|   
");

            Console.Write(@"
                                                         _____                            __
                                                        / ____|                        _ / /
                                                       | (___   ___  _ __ _ __ _   _  (_) | 
                                                        \___ \ / _ \| '__| '__| | | |   | | 
                                                        __ _) | (_) | |  | |  | |_| |  _| | 
                                                       |_____/ \___/|_|  |_|   \__, | (_) | 
                                                                                __/ |    \_\
                                                                               |___/        
");
            Console.SetCursorPosition(0, 5);
            HangMan.DisplayHangMan(p1);
            DisplayContinuePrompt(62, 27);
            p1.GamesPlayed++;
        }

        /// <summary>
        /// Displays the closing screen
        /// </summary>
        /// <param name="p1"></param>
        public static void DisplayClosingScreen(Player p1)
        {
            DisplayConsoleUI(p1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(@"
              _______ _                 _          ______           _____  _             _             
             |__   __| |               | |        |  ____|         |  __ \| |           (_)            
                | |  | |__   __ _ _ __ | | _____  | |__ ___  _ __  | |__) | | __ _ _   _ _ _ __   __ _ 
                | |  | '_ \ / _` | '_ \| |/ / __| |  __/ _ \| '__| |  ___/| |/ _` | | | | | '_ \ / _` |
                | |  | | | | (_| | | | |   <\__ \ | | | (_) | |    | |    | | (_| | |_| | | | | | (_| |
                |_|  |_| |_|\__,_|_| |_|_|\_\___/ |_|  \___/|_|    |_|    |_|\__,_|\__, |_|_| |_|\__, |
                                                                                    __/ |         __/ |
                                                                                   |___/         |___/ 

                 _____            __     __           _   _           _     _______ _                
                / ____|           \ \   / /          | \ | |         | |   |__   __(_)               
               | (___   ___  ___   \ \_/ /__  _   _  |  \| | _____  _| |_     | |   _ _ __ ___   ___ 
                \___ \ / _ \/ _ \   \   / _ \| | | | | . ` |/ _ \ \/ / __|    | |  | | '_ ` _ \ / _ \
                ____) |  __/  __/    | | (_) | |_| | | |\  |  __/>  <| |_     | |  | | | | | | |  __/
               |_____/ \___|\___|    |_|\___/ \__,_| |_| \_|\___/_/\_\\__|    |_|  |_|_| |_| |_|\___|
                                                                                       
                                                                                       
");
        }

        #endregion

        #region Leaderboard

        public static void DisplayLeaderboard(Player p1)
        {
            DisplayConsoleUI(p1);

            Console.Write(@"
                               _                    _           _                         _ 
                              | |                  | |         | |                       | |
                              | |     ___  __ _  __| | ___ _ __| |__   ___   __ _ _ __ __| |
                              | |    / _ \/ _` |/ _` |/ _ \ '__| '_ \ / _ \ / _` | '__/ _` |
                              | |___|  __/ (_| | (_| |  __/ |  | |_) | (_) | (_| | | | (_| |
                              |______\___|\__,_|\__,_|\___|_|  |_.__/ \___/ \__,_|_|  \__,_|            
");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\tPlayer\t\tScore\t\tDate");
            Console.WriteLine("\t\t\t\t\t------\t\t-----\t\t----");
            string fileLocation = "Data\\Leaderboard.txt";
            IEnumerable<string[]> leaderboard = File.ReadAllLines(fileLocation).Select(a => a.Split(','));
            foreach (string[] leader in leaderboard)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string name = leader[0];
                int.TryParse(leader[1], out int score);
                DateTime.TryParse(leader[2], out DateTime date);
                Console.WriteLine($"\t\t\t\t\t{name}\t\t{score}\t\t{date.ToString("d")}");
            }
        }

        public static void WriteScoreToFile(Player p1)
        {
            if (File.Exists("Data\\Leaderboard.txt"))
            {
                string leaderboardText = p1.Name + "," + p1.Score + "," + DateTime.Today + Environment.NewLine;
                File.AppendAllText("Data\\Leaderboard.txt", leaderboardText);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Displays the user Continue Prompt
        /// </summary>
        public static void DisplayContinuePrompt(int left = 62, int top = 27)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(left, top);
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        /// <summary>
        /// Quits the Application
        /// </summary>
        public static void Quit()
        {
            Environment.Exit(0);
        }

        #endregion
    }
}
