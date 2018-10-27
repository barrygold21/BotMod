using System;
using System.IO;
using BotMod.Properties;

namespace BotMod {

    public class Variables {
        public string robotsName = "Fred";
        public bool randomLocations = false;
        public bool reverseControls = false;
        public bool showDebugCommands = false;
        public static bool difficultyUnlocked;
        public int passengers = 0;
        public string tractionType = "Wheels";
        public int traction = 0;
        public string passengerBaySize = "Small";
        public int maxPassengers = 1;
        public const int landscapeSize = 10;
        public int[,] terrain = new int[landscapeSize, landscapeSize];
        public int[,] landscape = new int[landscapeSize, landscapeSize];
        public int powerRemaining;
        public int maxPower = 150;
        public int pplSaved = 0;
        public int x;
        public int y;
        public readonly int[,] powerLoss = new int[3, 3] {
                {1, 2, 3},
                {2, 1, 2},
                {3, 3, 1},
        };
    }

    class Program : Variables {

        static void Main() {
            Program p = new Program();
            difficultyUnlocked = IsDifficultyUnlocked();
            Console.Title = "BotMod - A Robot Game";
            bool flashControl = false;
            do {
                Console.Clear();
                if (flashControl) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("        BotMod        ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("Press any key to start");
                    System.Threading.Thread.Sleep(600);
                }
                else if (!flashControl) {
                    Console.WriteLine("        BotMod        ");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Press any key to start");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Threading.Thread.Sleep(600);
                }
                flashControl = !flashControl;
            } while (!Console.KeyAvailable);
            Console.ReadKey(true);
            p.MainMenu();
        }

        static bool IsDifficultyUnlocked() {
            switch (Settings.Default["DifficultyUnlocked"]) {
                case true:
                    return true;
                default:
                    return false;
            }
        }

        bool Error(int errorCode) {
            switch (errorCode) {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Please enter a number!");
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Valid choice not entered. Please enter an integer between 1 and 4.");
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Valid choice not entered. Please enter an integer between 1 and 3.");
                    break;
                case 4:
                    Console.WriteLine("Move invalid! You have attempted to move out of the grid. Please try again.");
                    break;
                case 5:
                    Console.WriteLine("Move invalid! You have entered a letter that is not a direction. Please try again.");
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Please enter a valid character!");
                    break;
                case 7:
                    Console.Clear();
                    Console.WriteLine("Valid choice not entered. Please enter an integer between 1 and 5.");
                    break;
                case 8:
                    Console.Clear();
                    Console.WriteLine("Difficulty selection is not unlocked. Beat the game once to unlock it!");
                    break;
                case 9:
                    Console.Clear();
                    Console.WriteLine("Please turn the setting either On or Off.");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Unknown error code.");
                    break;
            }
            System.Threading.Thread.Sleep(800);
            return false;
        }

        void MainMenu() {
            bool inputValid = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("      Main Menu      ");
                Console.WriteLine();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" 1. Play Game        ");
                Console.WriteLine(" 2. Modify Robot     ");
                if (!difficultyUnlocked) {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(" 3. Modify Difficulty");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else {
                    Console.WriteLine(" 3. Modify Difficulty");
                }
                Console.WriteLine(" 4. Rules            ");
                Console.WriteLine(" 5. Quit             ");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
                if (showDebugCommands) {
                    ShowDebugCommands();
                }

                Console.WriteLine("Please enter your choice (1 - 5). . .");
                Console.WriteLine();
                string userChoice = Console.ReadLine();
                bool isNumeric = int.TryParse(userChoice, out mCho);
                if (isNumeric == true) {
                    inputValid = true;
                    switch (mCho) {
                        case 1:
                            PlayGame();
                            break;
                        case 2:
                            inputValid = false;
                            ModifyRobot();
                            break;
                        case 3:
                            if (difficultyUnlocked) {
                                inputValid = false;
                                ChangeDifficulty();
                            }
                            else { inputValid = Error(8); }
                            break;
                        case 4:
                            Rules();
                            break;
                        case 5:
                            Environment.Exit(0);
                            break;
                        default:
                            inputValid = Error(7);
                            break;
                    }
                }
                else if (userChoice == "debug.ShowCommands") { showDebugCommands = !showDebugCommands; }
                else if (userChoice == "debug.ModifyDifficulty") { ChangeDifficulty(); } // this is handy
                else if (userChoice == "debug.RandomLocations") { randomLocations = !randomLocations; } // this is also handy
                else if (userChoice.StartsWith("debug.MaxPower")) { maxPower = int.Parse(userChoice.Substring(15, 3)); }
                else if (userChoice == "debug.LockDifficulty") { difficultyUnlocked = LockDifficulty(); }
                else if (userChoice == "debug.UnlockDifficulty") { difficultyUnlocked = UnlockDifficulty(); }
                // please always enter an integer, put a space after the string part too
                else { inputValid = Error(1); }
            } while (!inputValid);
        }

        void ShowDebugCommands() {
            using (var rules = new StringReader(Data.DebugCommands)) {
                string line;
                while ((line = rules.ReadLine()) != null) {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine();
        }

        void ModifyRobot() {
            bool goBackToMainMenu = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("             Modify Robot             ");
                Console.WriteLine();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" 1. Name Robot ({0})                  ", robotsName);
                Console.WriteLine(" 2. Change Traction ({0})             ", tractionType);
                Console.WriteLine(" 3. Change Passenger Bay Size ({0})   ", passengerBaySize);
                Console.WriteLine(" 4. Go To Main Menu                   ");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
                Console.WriteLine("Please enter your choice (1 - 4). . .");
                Console.WriteLine();
                string userChoice = Console.ReadLine();
                bool isNumeric = int.TryParse(userChoice, out mCho);
                if (isNumeric) {
                    switch (mCho) {
                        case 1:
                            NameRobot();
                            break;
                        case 2:
                            SelectTractionType();
                            break;
                        case 3:
                            ChangeBaySize();
                            break;
                        case 4:
                            goBackToMainMenu = true;
                            break;
                        default:
                            Error(2);
                            break;
                    }
                }
                else { Error(1); }
            } while (!goBackToMainMenu);
        }

        void NameRobot() {
            bool inputValid = false;
            do {
                Console.Clear();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Current Robot Name: {0}", robotsName);
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Please enter a valid name for your robot.");
                Console.WriteLine("Make sure it starts with a capital letter and is more than three characters long.");
                string userInput = Console.ReadLine();
                string rbFChr = robotsName.Substring(0, 1);
                string rbFChrUpper = rbFChr.ToUpper();
                if (userInput.Length > 3 && rbFChr == rbFChrUpper) {
                    robotsName = userInput;
                    Console.WriteLine("Name set to {0}", robotsName);
                    System.Threading.Thread.Sleep(800);
                    inputValid = true;
                }
                else { inputValid = Error(6); }
            } while (inputValid == false);
        }

        void SelectTractionType() {
            bool inputValid = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Current traction type: {0}         ", tractionType);
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
                Console.WriteLine("Here you can change what traction type you are using.");
                Console.WriteLine("You can use one of the following traaction types:");
                Console.WriteLine("1. Wheels");
                Console.WriteLine("2. Tracks");
                Console.WriteLine("3. Skis");
                Console.WriteLine();
                Console.WriteLine("If you want information on these traction types, you should");
                Console.WriteLine("go to the rules page to see how they work.");
                Console.WriteLine("Please enter which traction type you wish to use (1 - 3):");
                Console.WriteLine();
                Console.WriteLine();
                string userInput = Console.ReadLine();
                bool isNumeric = int.TryParse(userInput, out mCho);
                if (isNumeric) {
                    inputValid = true;
                    switch (mCho) {
                        case 1:
                            tractionType = "Wheels";
                            break;
                        case 2:
                            tractionType = "Tracks";
                            break;
                        case 3:
                            tractionType = "Skis";
                            break;
                        default:
                            inputValid = Error(3);
                            break;
                    }
                }
                else { Error(2); }
            } while (!inputValid);
            traction = mCho - 1;
            Console.WriteLine("Traction type successfully set to {0}", tractionType);
            System.Threading.Thread.Sleep(800);
        }

        void ChangeBaySize() {
            bool inputValid = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Current passenger bay size: {0}         ", passengerBaySize);
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
                Console.WriteLine("Here you can change how large your passenger bay is.");
                Console.WriteLine("Your passenger bay can be the following sizes:");
                Console.WriteLine("1. Small");
                Console.WriteLine("2. Med");
                Console.WriteLine("3. Large");
                Console.WriteLine();
                Console.WriteLine("If you want information on the passenger bay sizes, you should");
                Console.WriteLine("go to the rules page to see how they work.");
                Console.WriteLine();
                Console.WriteLine("Please enter which traction type you wish to use (1 - 3):");
                Console.WriteLine();
                string userInput = Console.ReadLine();
                bool isNumeric = int.TryParse(userInput, out mCho);
                if (isNumeric) {
                    inputValid = true;
                    switch (mCho) {
                        case 1:
                            passengerBaySize = "Small";
                            break;
                        case 2:
                            passengerBaySize = "Med";
                            break;
                        case 3:
                            passengerBaySize = "Large";
                            break;
                        default:
                            inputValid = Error(3);
                            break;
                    }
                }
                else { Error(2); }
            } while (!inputValid);
            maxPassengers = mCho;
            Console.WriteLine("Passenger bay size successfully set to {0}", passengerBaySize);
            System.Threading.Thread.Sleep(800);
        }

        void ChangeDifficulty() {
            bool goBackToMainMenu = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("     Modify Difficulty     ");
                Console.WriteLine();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" 1. Change Max Power Units ({0})", maxPower);
                Console.WriteLine(" 2. Change Random Spawning ({0})", randomLocations);
                Console.WriteLine(" 3. Reverse Controls ({0})", reverseControls);
                Console.WriteLine(" 4. Go To Main Menu        ");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
                Console.WriteLine("Please enter your choice (1 - 4). . .");
                Console.WriteLine();
                string userChoice = Console.ReadLine();
                bool isNumeric = int.TryParse(userChoice, out mCho);
                if (isNumeric) {
                    switch (mCho) {
                        case 1:
                            EnterMaxPowerUnits();
                            break;
                        case 2:

                            break;
                        case 3:
                            ChangeBaySize();
                            break;
                        case 4:
                            goBackToMainMenu = true;
                            break;
                        default:
                            Error(2);
                            break;
                    }
                }
                else { Error(1); }
            } while (!goBackToMainMenu);
        }

        void EnterMaxPowerUnits() {
            bool inputValid = false;
            do {
                Console.Clear();
                Console.WriteLine("Max Power Units: {0}", maxPower);
                Console.WriteLine("Please enter the max power units you wish to have:");
                string usersChoice = Console.ReadLine();
                bool isNumeric = int.TryParse(usersChoice, out int choice);
                if (isNumeric) {
                    if (choice > 0) {
                        maxPower = choice;
                        Console.WriteLine("Max Power set to {0}", maxPower);
                        System.Threading.Thread.Sleep(800);
                    }
                    else { inputValid = Error(1); }
                }
                else { inputValid = Error(1); }
            } while (!inputValid);
        }

        void ChangeRandomSpawning() {
            bool inputValid = false;
            do {
                Console.Clear();
                Console.WriteLine("Random Spawning is currently set to {0}", randomLocations);
                Console.WriteLine("Please enter whether you want random spawning on or off: ");
                string usersChoice = Console.ReadLine();
                usersChoice = usersChoice.ToUpper();
                inputValid = true;
                switch (usersChoice) {
                    case "ON":
                        randomLocations = true;
                        break;
                    case "OFF":
                        randomLocations = false;
                        break;
                    default:
                        inputValid = Error(9);
                        break;
                }
            } while (!inputValid);
            Console.WriteLine("Random spawning set to {0}", randomLocations);
            System.Threading.Thread.Sleep(800);
        }

        void ReverseControls() {
            bool inputValid = false;
            do {
                Console.Clear();
                Console.WriteLine("Reverse Controls is currently set to {0}", reverseControls);
                Console.WriteLine("Please enter whether you want Reverse Controls on or off: ");
                string usersChoice = Console.ReadLine();
                usersChoice = usersChoice.ToUpper();
                inputValid = true;
                switch (usersChoice) {
                    case "ON":
                        reverseControls = true;
                        break;
                    case "OFF":
                        reverseControls = false;
                        break;
                    default:
                        inputValid = Error(9);
                        break;
                }
            } while (!inputValid);
            Console.WriteLine("Reverse Controls set to {0}", reverseControls);
            System.Threading.Thread.Sleep(800);
        }

        bool LockDifficulty() {
            Console.WriteLine("Difficulty Locked.");
            System.Threading.Thread.Sleep(800);
            Settings.Default["DifficultyUnlocked"] = false;
            Settings.Default.Save();
            return false;
        }

        bool UnlockDifficulty() {
            Console.WriteLine("Difficulty Unlocked.");
            System.Threading.Thread.Sleep(800);
            Settings.Default["DifficultyUnlocked"] = true;
            Settings.Default.Save();
            return true;
        }

        void PlayGame() {
            Console.Clear();
            Init();
            bool gameOver = false;
            do {
                DrawLandscape();
                DisplayInfo();
                string move = EnterMove();
                UpdatePowerRemaining();
                Console.WriteLine();
                System.Threading.Thread.Sleep(100);
                MoveRobot(move);
                Console.Clear();
                Console.Clear();
                if (powerRemaining == 0 || pplSaved == 4) {
                    if (pplSaved == 4) {
                        YouWon();
                    }
                    else if (powerRemaining == 0) {
                        YouLost();
                    }
                    gameOver = true;
                }
            } while (!gameOver);
        }

        void Init() {
            int k = landscapeSize;
            using (var reader = new StringReader(Data.Terrain)) {
                string line;
                int lineContent;
                int i = 0;
                int j = 0;
                while ((line = reader.ReadLine()) != null) {
                    lineContent = Convert.ToInt32(line);
                    landscape[i, j] = 0;
                    terrain[i, j] = lineContent;
                    if (j == k - 1) {
                        j = 0;
                        i++;
                    }
                    else { j++; }
                    if (i == k) { break; }
                }
            }
            if (randomLocations) { RandomLocations(); }
            else { StandardLocations(); }
            pplSaved = 0;
            powerRemaining = maxPower;
        }

        void RandomLocations() {
            bool locationValid = false;
            Random randomLocation = new Random();
            int randomX;
            int randomY;
            locationValid = false;
            do {
                randomX = randomLocation.Next(0, 10);
                randomY = randomLocation.Next(0, 10);
                if (terrain[randomX, randomY] == 0) {
                    locationValid = true;
                }
                else { locationValid = false; }
            } while (!locationValid);
            x = randomX;
            y = randomY;
            for (int i = 0; i < 4; i++) {
                locationValid = false;
                do {
                    randomX = randomLocation.Next(0, 10);
                    randomY = randomLocation.Next(0, 10);
                    if (landscape[randomX, randomY] == 0) {
                        if (randomX != x && randomY != y) {
                            locationValid = true;
                        }
                        else { locationValid = false; }
                    }
                    else { locationValid = false; }
                } while (!locationValid);
                landscape[randomX, randomY] = 1;
            }
            locationValid = false;
            do {
                randomX = randomLocation.Next(0, 10);
                randomY = randomLocation.Next(0, 10);
                if (landscape[randomX, randomY] == 0) {
                    if (randomX != x && randomY != y) {
                        locationValid = true;
                    }
                    else { locationValid = false; }
                }
                else { locationValid = false; }
            } while (!locationValid);
            landscape[randomX, randomY] = 2;
        }

        void StandardLocations() {
            x = 0;
            y = 0;
            landscape[0, 2] = 1;
            landscape[2, 7] = 1;
            landscape[6, 4] = 1;
            landscape[6, 9] = 1;
            landscape[3, 6] = 2;
        }

        void DrawLandscape() {
            Console.WriteLine();
            int k = landscapeSize;
            for (int i = 0; i < k; i++) {
                for (int j = 0; j < k; j++) {
                    if (i == x && j == y) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("R");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else {
                        switch (terrain[i, j]) {
                            case 0:
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                        }
                        switch (landscape[i, j]) {
                            case 0:
                                Console.Write(".");
                                break;
                            case 1:
                                Console.Write("P");
                                break;
                            case 2:
                                Console.Write("B");
                                break;
                        }
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        void DisplayInfo() {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Power Remaining: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(powerRemaining);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Passengers: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(passengers);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.Write("People saved: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(pplSaved);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        string EnterMove() {
            Console.ForegroundColor = ConsoleColor.White;
            bool moveIsValid = false;
            string userInput;
            do {
                Console.WriteLine();
                Console.WriteLine("Please enter a move for {0} to take:", robotsName);
                userInput = Console.ReadLine();
                userInput = userInput.ToUpper();
                if (userInput == "N" || userInput == "E" || userInput == "S" || userInput == "W") {
                    moveIsValid = true;
                    if (reverseControls) {
                        switch (userInput) {
                            case "N":
                                userInput = "S";
                                break;
                            case "E":
                                userInput = "W";
                                break;
                            case "S":
                                userInput = "N";
                                break;
                            case "W":
                                userInput = "E";
                                break;
                        }
                    }
                    switch (userInput) {
                        case "N":
                            if (x == 0) {
                                moveIsValid = Error(4);
                            }
                            break;
                        case "E":
                            if (y == 9) {
                                moveIsValid = Error(4);
                            }
                            break;
                        case "S":
                            if (x == 9) {
                                moveIsValid = Error(4);
                            }
                            break;
                        case "W":
                            if (y == 0) {
                                moveIsValid = Error(4);
                            }
                            break;
                        default:
                            moveIsValid = true;
                            break;
                    }
                }
                else if (userInput == "DEBUG.BEATGAME") {
                    pplSaved = 4;
                    Console.WriteLine("All explorers saved. Enter a valid move to complete game");
                    moveIsValid = false;
                }
                else { moveIsValid = Error(5); }
            } while (!moveIsValid);
            Console.WriteLine("Move Valid! Moving {0} in the direction of {1}.", robotsName, userInput);
            System.Threading.Thread.Sleep(400);
            return userInput;
        }

        void MoveRobot(string move) {
            switch (move) {
                case "N":
                    x--;
                    break;
                case "E":
                    y++;
                    break;
                case "S":
                    x++;
                    break;
                case "W":
                    y--;
                    break;
            }
            if (terrain[x, y] == 2) { CalculateSlide(move); }
            if (landscape[x, y] == 1 && passengers < maxPassengers) {
                passengers++;
                landscape[x, y] = 0;
            }
            else if (landscape[x, y] == 2) {
                pplSaved += passengers;
                passengers = 0;
            }
        }

        void CalculateSlide(string move) {
            Random slideRNG = new Random();
            int slide = slideRNG.Next(1, 9);
            if (slide == 1) {
                switch (move) {
                    case "N":
                        if (x - 1 != -1) {
                            x--;
                        }
                        break;
                    case "E":
                        if (y + 1 != 10) {
                            y++;
                        }
                        break;
                    case "S":
                        if (x + 1 != 10) {

                            x++;
                        }
                        break;
                    case "W":
                        if (y - 1 != -1) {
                            y--;
                        }
                        break;
                }
            }
        }

        void UpdatePowerRemaining() {
            powerRemaining -= maxPassengers - 1;
            powerRemaining -= powerLoss[traction, terrain[x, y]];
        }

        void YouWon() {
            if (!difficultyUnlocked) {
                Settings.Default["DifficultyUnlocked"] = true;
                Settings.Default.Save();
                Console.Clear();
                Console.WriteLine("Difficulty Modifications Unlocked!");
                System.Threading.Thread.Sleep(1000);
            }
            Console.ForegroundColor = ConsoleColor.White;
            bool userPlayAgain = false;
            do {
                Console.Clear();
                DrawLandscape();
                DisplayInfo();
                Console.WriteLine();
                Console.WriteLine("Congratulations! You have succeeded in saving all the lost exlorers");
                Console.WriteLine("and returning them to base camp.");
                Console.WriteLine();
                Console.WriteLine("Would you like to play again? (Y/N)");
                string playAgain = Console.ReadLine();
                playAgain = playAgain.ToUpper();
                if (playAgain == "Y") {
                    userPlayAgain = true;
                    Main();
                }
                else if (playAgain == "N") {
                    userPlayAgain = false;
                    Console.WriteLine("Thank you again for playing!");
                    System.Threading.Thread.Sleep(500);
                    Environment.Exit(0);
                }
                else { userPlayAgain = Error(6); }
            } while (!userPlayAgain);
        }

        void YouLost() {
            Console.ForegroundColor = ConsoleColor.White;
            bool userPlayAgain = false;
            do {
                Console.Clear();
                DrawLandscape();
                DisplayInfo();
                Console.WriteLine();
                Console.WriteLine("Ouch! Unfortunately, you didn't manage to save the explorers");
                Console.WriteLine("and return them to base camp. Better luck next time!");
                Console.WriteLine();
                Console.WriteLine("Would you like to play again? (Y/N)");
                string playAgain = Console.ReadLine();
                playAgain = playAgain.ToUpper();
                if (playAgain == "Y") {
                    userPlayAgain = true;
                    Main();
                }
                else if (playAgain == "N") {
                    userPlayAgain = false;
                    Console.WriteLine("Thank you for playing!");
                    System.Threading.Thread.Sleep(500);
                    Environment.Exit(0);
                }
                else { userPlayAgain = Error(6); }
            } while (!userPlayAgain);
        }

        void Rules() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n Rules: \n");
            Console.ForegroundColor = ConsoleColor.White;
            using (var rules = new StringReader(Data.Rules)) {
                string line;
                while ((line = rules.ReadLine()) != null) {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("\n    Press any key to go back to the main menu. . .");
            Console.ReadKey(true);
        }
    }
}