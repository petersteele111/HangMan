using System;

namespace HangMan
{
    class HangMan
    {
        /// <summary>
        /// Displays the HangMan in various states based on how many incorrect
        /// guess have been made
        /// </summary>
        /// <param name="p1">Player Object that tracks player progression</param>
        public static void DisplayHangMan(Player p1)
        {
            switch (p1.GuessesLeft)
            {
                case 6:
                    DisplayGallows();
                    break;
                case 5:
                    DisplayHead();
                    break;
                case 4:
                    DisplayBody();
                    break;
                case 3:
                    DisplayLeftArm();
                    break;
                case 2:
                    DisplayRightArm();
                    break;
                case 1:
                    DisplayLeftLeg();
                    break;
                case 0:
                    DisplayRightLeg();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Displays the Gallows to the Screen
        /// </summary>
        public static void DisplayGallows()
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(@"
     ______________________
    |                      |
    |    ______________    |
    |   |              |___| 
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |     
    |   |
    |   |
    |   |
    |   |
    |   |
");
        }

        /// <summary>
        /// Displays the Head
        /// </summary>
        public static void DisplayHead()
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(@"
     ______________________
    |                      |
    |    ______________    |
    |   |             _|___|_ 
    |   |            /  ___  \ 
    |   |           |  |   |  |
    |   |           |  |0 0|  |
    |   |           |  | 0 |  |
    |   |           |  |xxx|  |
    |   |           |  |___|  |
    |   |            \_______/ 
    |   |       
    |   |       
    |   |       
    |   |       
    |   |       
    |   |      
    |   |     
    |   |
    |   |
    |   |
    |   |
    |   |
");
        }

        /// <summary>
        /// Displays the Body
        /// </summary>
        public static void DisplayBody()
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(@"
     ______________________
    |                      |
    |    ______________    |
    |   |             _|___|_ 
    |   |            /  ___  \ 
    |   |           |  |   |  |
    |   |           |  |0 0|  |
    |   |           |  | 0 |  |
    |   |           |  |xxx|  |
    |   |           |  |___|  |
    |   |            \_______/ 
    |   |              |  |
    |   |             /    \
    |   |            |      |
    |   |            |      |
    |   |            |      |
    |   |            |______|
    |   |     
    |   |
    |   |
    |   |
    |   |
    |   |
");
        }

        /// <summary>
        /// Displays the Left Arm
        /// </summary>
        public static void DisplayLeftArm()
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(@"
     ______________________
    |                      |
    |    ______________    |
    |   |             _|___|_ 
    |   |            /  ___  \ 
    |   |           |  |   |  |
    |   |           |  |0 0|  |
    |   |           |  | 0 |  |
    |   |           |  |xxx|  |
    |   |           |  |___|  |
    |   |            \_______/ 
    |   |              |  |
    |   |          __ /    \
    |   |         / _|      |
    |   |        / / |      |
    |   |       / /  |      |
    |   |      / /   |______|
    |   |     (__)
    |   |
    |   |
    |   |
    |   |
    |   |
");
        }

        /// <summary>
        /// Displays the Right Arm
        /// </summary>
        public static void DisplayRightArm()
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(@"
     ______________________
    |                      |
    |    ______________    |
    |   |             _|___|_ 
    |   |            /  ___  \ 
    |   |           |  |   |  |
    |   |           |  |0 0|  |
    |   |           |  | 0 |  |
    |   |           |  |xxx|  |
    |   |           |  |___|  |
    |   |            \_______/ 
    |   |              |  |
    |   |          __ /    \ __
    |   |         / _|      |_ \
    |   |        / / |      | \ \
    |   |       / /  |      |  \ \
    |   |      / /   |______|   \ \
    |   |     (__)              (__)
    |   |
    |   |
    |   |
    |   |
    |   |
");
        }

        /// <summary>
        /// Displays the Left Leg
        /// </summary>
        public static void DisplayLeftLeg()
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(@"
     ______________________
    |                      |
    |    ______________    |
    |   |             _|___|_ 
    |   |            /  ___  \ 
    |   |           |  |   |  |
    |   |           |  |0 0|  |
    |   |           |  | 0 |  |
    |   |           |  |xxx|  |
    |   |           |  |___|  |
    |   |            \_______/ 
    |   |              |  |
    |   |          __ /    \ __
    |   |         / _|      |_ \
    |   |        / / |      | \ \
    |   |       / /  |      |  \ \
    |   |      / /   |______|   \ \
    |   |     (__)   |      |   (__)
    |   |            |      |
    |   |            |  |
    |   |            |  |
    |   |           _|  |
    |   |          (____|  
");
        }

        /// <summary>
        /// Displays the Right Leg
        /// </summary>
        public static void DisplayRightLeg()
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(@"
     ______________________
    |                      |
    |    ______________    |
    |   |             _|___|_ 
    |   |            /  ___  \ 
    |   |           |  |   |  |
    |   |           |  |X X|  |
    |   |           |  | 0 |  |
    |   |           |  |xxx|  |
    |   |           |  |___|  |
    |   |            \_______/ 
    |   |              |  |
    |   |          __ /    \ __
    |   |         / _|      |_ \
    |   |        / / |      | \ \
    |   |       / /  |      |  \ \
    |   |      / /   |______|   \ \
    |   |     (__)   |      |   (__)
    |   |            |      |
    |   |            |  ||  |
    |   |            |  ||  |
    |   |           _|  ||  |_
    |   |          (____||____)
");
        }
    }
}
