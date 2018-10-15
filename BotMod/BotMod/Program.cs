using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotMod {

    public class GlobalVars {
        public static string robotsName = "Fred";
        public static int passengers = 0;
        public static int maxPassengers = 0;
        public static int traction;
        public const int landscapeSize = 10;
        public static int[,] terrain = new int[landscapeSize, landscapeSize];
        public static int[,] landscape = new int[landscapeSize, landscapeSize];
        public static int powerRemaining = 150;
        public static int pplSaved = 0;
        public static string TractionType = "Wheels";
        public static string passengerBaySize = "Small";
        public static int x;
        public static int y;
        public static readonly int[,] powerLoss = new int[3, 3] {
                {1, 2, 3},
                {2, 1, 2},
                {3, 3, 1},
        };
    }

    class Program {

        static void Main() {
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
            MainMenu();
        }

        static bool Error(int errorCode) {
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
                    Console.WriteLine(" Move invalid! You have attempted to move out of the grid. Please try again.");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Unknown error code.");               
                    break;
            }
            System.Threading.Thread.Sleep(800);
            return false;
        }

        static void MainMenu() {
            bool inputValid = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("    Main Menu    ");
                Console.WriteLine();
                Console.WriteLine("~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" 1. Play Game    ");
                Console.WriteLine(" 2. Modify Robot ");
                Console.WriteLine(" 3. Rules        ");
                Console.WriteLine(" 4. Quit         ");
                Console.WriteLine("~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
                Console.WriteLine("Please enter your choice (1 - 4). . .");
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
                            inputValid = false; // but actually true
                            ModifyRobot();
                            break;
                        case 3:
                            Rules();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            inputValid = Error(2);
                            break;
                    }
                }
                else {
                    inputValid = Error(1);
                }
            } while (!inputValid);
        }

        static void ModifyRobot() {
            bool goBackToMainMenu = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("             Modify Robot             ");
                Console.WriteLine();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" 1. Name Robot ({0})                  ", GlobalVars.robotsName);
                Console.WriteLine(" 2. Change Traction ({0})             ", GlobalVars.TractionType);
                Console.WriteLine(" 3. Change Passenger Bay Size ({0})   ", GlobalVars.passengerBaySize);
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
                            Error(3);
                            break;
                    }
                }
                else {
                    Error(1);
                }
            } while (!goBackToMainMenu);
        }

        static void NameRobot() {
            bool inputValid = false;
            do {
                Console.Clear();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Current Robot Name: {0}", GlobalVars.robotsName);
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Please enter a valid name for your robot.");
                Console.WriteLine("Make sure it starts with a capital letter and is more than three characters long.");
                string userInput = Console.ReadLine();
                string rbFChr = GlobalVars.robotsName.Substring(0, 1);
                string rbFChrUpper = rbFChr.ToUpper();
                if (userInput.Length > 3 && rbFChr == rbFChrUpper) {
                    GlobalVars.robotsName = userInput;
                    Console.WriteLine("Name set to {0}", GlobalVars.robotsName);
                    System.Threading.Thread.Sleep(800);
                    inputValid = true;
                }
                else {
                    inputValid = Error(6);
                }
            } while (inputValid == false);
        }

        static void SelectTractionType() {
            bool inputValid = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Current traction type: {0}         ", GlobalVars.TractionType);
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
                Console.WriteLine();
                Console.WriteLine("Please enter which traction type you wish to use (1 - 3):");
                Console.WriteLine();
                string userInput = Console.ReadLine();
                bool isNumeric = int.TryParse(userInput, out mCho);
                if (isNumeric) {
                    inputValid = true;
                    switch (mCho) {
                        case 1:
                            GlobalVars.TractionType = "Wheels";
                            break;
                        case 2:
                            GlobalVars.TractionType = "Tracks";
                            break;
                        case 3:
                            GlobalVars.TractionType = "Skis";
                            break;
                        default:
                            inputValid = false;
                            Error(3);
                            break;
                    }
                }
                else {
                    Error(2);
                }
            } while (!inputValid);
            Console.WriteLine("Traction type successfully set to {0}", GlobalVars.TractionType);
            System.Threading.Thread.Sleep(800);
        }

        static void ChangeBaySize() {
            bool inputValid = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Current passenger bay size: {0}         ", GlobalVars.passengerBaySize);
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
                            GlobalVars.passengerBaySize = "Small";
                            break;
                        case 2:
                            GlobalVars.passengerBaySize = "Med";
                            break;
                        case 3:
                            GlobalVars.passengerBaySize = "Large";
                            break;
                        default:
                            inputValid = false;
                            Error(3);
                            break;
                    }
                }
                else {
                    Error(2);
                }
            } while (!inputValid);
            Console.WriteLine("Passenger bay size successfully set to {0}", GlobalVars.passengerBaySize);
            System.Threading.Thread.Sleep(800);
        }

        static void PlayGame() {
            Console.Clear();
            Init();
            bool gameOver = false;
            bool gameWon = false;
            do {
                DrawLandscape();
                DisplayInfo();
                string move = EnterMove();
                Console.WriteLine();
                System.Threading.Thread.Sleep(100);
                MoveRobot(move);
                Console.Clear();
                if (GlobalVars.powerRemaining == 0 && GlobalVars.pplSaved == 4) {
                    gameWon = true;
                    gameOver = true;
                    break;
                }
                else if (GlobalVars.powerRemaining == 0) {
                    gameOver = true;
                    break;
                }
                else if (GlobalVars.pplSaved == 4) {
                    gameWon = true;
                    break;
                }
                else {
                    gameOver = false;
                    gameWon = false;
                }
            } while (!gameOver || !gameWon);
            if (gameWon) {
                YouWon();
            }
            else if (gameOver) {
                YouLost();
            }
            else if (gameWon && gameOver) {
                YouWon();
            }
        }

        static void Init() {
            int k = GlobalVars.landscapeSize - 1;
            string filePath = System.IO.Path.GetFullPath("boarddata.txt");
            using (StreamReader fileReader = new StreamReader(filePath)) {
                string line;
                int i = 0;
                int j = 0;
                do {
                    if (i == k + 1) {
                        break;
                    }
                    line = fileReader.ReadLine();
                    int numeric = Convert.ToInt32(line);
                    GlobalVars.terrain[i, j] = numeric;
                    if (j == k) {
                        j = 0;
                        i++;
                    }
                    else {
                        j++;
                    }
                } while (!fileReader.EndOfStream);
            }
            k++;
            for (int i = 0; i < k; i++) {
                for (int j = 0; j < k; j++) {
                    GlobalVars.landscape[i, j] = 0;
                }
            }
            switch (GlobalVars.passengerBaySize) {
                case "Small":
                    GlobalVars.maxPassengers = 1;
                    break;
                case "Med":
                    GlobalVars.maxPassengers = 2;
                    break;
                case "Large":
                    GlobalVars.maxPassengers = 3;
                    break;
            }
            switch (GlobalVars.TractionType) {
                case "Wheels":
                    GlobalVars.traction = 0;
                    break;
                case "Tracks":
                    GlobalVars.traction = 1;
                    break;
                case "Skis":
                    GlobalVars.traction = 2;
                    break;

            }
            GlobalVars.x = 0;
            GlobalVars.y = 0;
            GlobalVars.pplSaved = 0;
            GlobalVars.powerRemaining = 150;
            GlobalVars.landscape[0, 2] = 1;
            GlobalVars.landscape[2, 7] = 1;
            GlobalVars.landscape[6, 4] = 1;
            GlobalVars.landscape[6, 9] = 1;
            GlobalVars.landscape[3, 6] = 2;
        }

        static void DrawLandscape() {
            Console.WriteLine();
            int k = GlobalVars.landscapeSize;
            for (int i = 0; i < k; i++) {
                for (int j = 0; j < k; j++) {
                    if (i == GlobalVars.x && j == GlobalVars.y) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("R");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else {
                        switch (GlobalVars.terrain[i, j]) {
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
                        switch (GlobalVars.landscape[i, j]) {
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

        static void DisplayInfo() {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Power Remaining: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(GlobalVars.powerRemaining);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.Write("Traction type: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(GlobalVars.TractionType);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Passengers: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(GlobalVars.passengers);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.Write("People saved: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(GlobalVars.pplSaved);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static string EnterMove() {
            string move;
            Console.ForegroundColor = ConsoleColor.White;
            bool moveIsValid = false;
            string inputMove;
            do {
                Console.WriteLine();
                Console.WriteLine("Please enter a move for {0} to take:", GlobalVars.robotsName);
                inputMove = Console.ReadLine();
                inputMove = inputMove.ToUpper();
                if (inputMove == "N" || inputMove == "E" || inputMove == "S" || inputMove == "W") {
                    moveIsValid = true;
                    switch (inputMove) {
                        case "N":
                            if (GlobalVars.x == 0) {
                                moveIsValid = false;
                                Error(4);
                            }
                            break;
                        case "E":
                            if (GlobalVars.y == 9) {
                                moveIsValid = false;
                                Error(4);
                            }
                            break;
                        case "S":
                            if (GlobalVars.x == 9) {
                                moveIsValid = false;
                                Error(4);
                            }
                            break;
                        case "W":
                            if (GlobalVars.y == 0) {
                                moveIsValid = false;
                                Error(4);
                            }
                            break;
                        default:
                            moveIsValid = true;
                            break;
                    }
                }
                else {
                    moveIsValid = false;
                    Console.WriteLine(" Move invalid! You have entered a letter that is not a direction. Please try again.");
                }
            } while (!moveIsValid);
            move = inputMove;
            Console.WriteLine("Move Valid! Moving {0} in the direction of {1}.", GlobalVars.robotsName, inputMove);
            System.Threading.Thread.Sleep(400);
            return move;
        }

        static void MoveRobot(string move) {
            switch (move) {
                case "N":
                    GlobalVars.x--;
                    break;
                case "E":
                    GlobalVars.y++;
                    break;
                case "S":
                    GlobalVars.x++;
                    break;
                case "W":
                    GlobalVars.y--;
                    break;
            }
            CalculateSlide(move);
            UpdatePowerRemaining();
            if (GlobalVars.landscape[GlobalVars.x, GlobalVars.y] == 1) {
                PickUpPerson();
            }
            if (GlobalVars.landscape[GlobalVars.x, GlobalVars.y] == 2) {
                DropPeople(move);
            }    
        }

        static void CalculateSlide(string move) {
            if (GlobalVars.terrain[GlobalVars.x, GlobalVars.y] == 2) {
                Random slideRNG = new Random();
                int slide = slideRNG.Next(1, 9);
                if (move == "N") {
                    if (GlobalVars.x - 1 != -1) {
                        if (slide == 1) {
                            GlobalVars.x--;
                        }
                    }
                }
                else if (move == "E") {
                    if (GlobalVars.y + 1 != 10) {
                        if (slide == 1) {
                            GlobalVars.y++;
                        }
                    }
                }
                else if (move == "S") {
                    if (GlobalVars.x + 1 != 10) {
                        if (slide == 1) {
                            GlobalVars.x++;
                        }
                    }
                }
                else if (move == "W") {
                    if (GlobalVars.y - 1 != -1) {
                        if (slide == 1) {
                            GlobalVars.y--;
                        }
                    }
                }
            }
        }

        static void UpdatePowerRemaining() {
            GlobalVars.powerRemaining -= GlobalVars.maxPassengers - 1;
            GlobalVars.powerRemaining -= GlobalVars.powerLoss[GlobalVars.traction, GlobalVars.terrain[GlobalVars.x, GlobalVars.y]];
            if (GlobalVars.landscape[GlobalVars.x, GlobalVars.y] == 1) {
                if (GlobalVars.passengers < GlobalVars.maxPassengers) {
                    GlobalVars.passengers++;
                    GlobalVars.landscape[GlobalVars.x, GlobalVars.y] = 0;
                }
            }
        }

        static void PickUpPerson() {
            if (GlobalVars.passengers < GlobalVars.maxPassengers) {
                GlobalVars.passengers++;
                GlobalVars.landscape[GlobalVars.x, GlobalVars.y] = 0;
            }
        }

        static void DropPeople(string move) {
                GlobalVars.pplSaved += GlobalVars.passengers;
                GlobalVars.passengers = 0;
        }

        static void YouWon() {
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
                else {
                    userPlayAgain = false;
                    Console.Clear();
                    Console.WriteLine("Please enter a valid character!");
                    System.Threading.Thread.Sleep(500);
                }
            } while (!userPlayAgain);
        }

        static void YouLost() {
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
                else {
                    userPlayAgain = false;
                    Console.Clear();
                    Console.WriteLine("Please enter a valid character!");
                    System.Threading.Thread.Sleep(500);
                }
            } while (!userPlayAgain);
        }

        static void Rules() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Rules:                                                           ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. The robot always starts in the top left of the grid.          ");
            Console.WriteLine("2. The robot can move in 4 directions (N, E, S, W).              ");
            Console.WriteLine("3. The robot cannot make diagonal moves.                         ");
            Console.WriteLine("4. The robot cannot move beyond the edge of the grid.            ");
            Console.WriteLine("5. The robot can carry one, two or three people in the           ");
            Console.WriteLine("   passenger bay, depending on which size you are using:         ");
            Console.WriteLine("   If you are using a small passenger bay, you get one slot.     ");
            Console.WriteLine("   If you are using a medium passenger bay, you get two.         ");
            Console.WriteLine("   ... And if you are using a large passenger bay, you get three!");
            Console.WriteLine("    Each passenger bay size uses zero, one and two extra power   ");
            Console.WriteLine("    units respectively.                                          ");
            Console.WriteLine("6. There are three traction types: Wheels, Tracks and Skis.      ");
            Console.WriteLine("   This table gives all information you need about traction types:");
            Console.WriteLine();
            Console.WriteLine("   Traction type | On Grass | On Rocks | On Ice  ");
            Console.WriteLine("   Wheels        | -1 Power | -2 Power | -3 Power");
            Console.WriteLine("   Tracks        | -2 Power | -1 Power | -2 Power");
            Console.WriteLine("   Skis          | -2 Power | -3 Power | -1 Power");
            Console.WriteLine();
            Console.WriteLine("7. If there is no room in the passenger bay, the person will be  ");
            Console.WriteLine("   left behind and won't be picked up.                           ");
            Console.WriteLine("8. The robot has 150 power units.                                ");
            Console.WriteLine("9. The player wins if all people are dropped off at base         ");
            Console.WriteLine("   camp before the robot runs out of power.                      ");
            Console.WriteLine("10 . If the robot runs out of power before all players are       ");
            Console.WriteLine("   at the base, the player loses.                                ");
            Console.WriteLine();
            Console.WriteLine("Each piece on the board and the meaning for it:");
            Console.WriteLine();
            Console.WriteLine("Piece  |   Object    ");
            Console.WriteLine("R      |   Robot     ");
            Console.WriteLine("P      |   Person    ");
            Console.WriteLine("B      |   Base Camp ");
            Console.WriteLine(".      |   Empty Cell");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey(true);
        }
    }
}