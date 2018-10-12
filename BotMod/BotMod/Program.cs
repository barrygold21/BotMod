using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotMod {

    public class GlobalVars {
          //===========================\\
         //      GLOBAL VARIABLES       \\
        //===============================\\
        public static string robotsName = "Fred";
        public const int landscapeSize = 10;
        public static string[,] landscapeData = new string[landscapeSize, landscapeSize];
        public static char[,] landscape = new char[landscapeSize, landscapeSize];
        public static readonly string robotCell = "R";
        public static readonly string emptyCell = ".";
        public static readonly string personCell = "P";
        public static int powerRemaining = 150;
        public static int pplSaved = 0;
        public static string TractionType = "Wheels";
        public static string PassengerBaySize = "Small";
        //
        public struct RobLocation {
            public int currentX, currentY;
            public int newX, newY;
        }
        public static RobLocation RobLoc = new RobLocation();
        //
        public struct PassengerBay {
            public string Slot1, Slot2, Slot3;
        }
        public static PassengerBay PasBay = new PassengerBay();
        //
        public struct PeopleSaved {
            public bool person1, person2, person3, person4;
        }
        public static PeopleSaved personIsSaved = new PeopleSaved();
        //
    }

    class Program {

        static void Main() {
            Console.Clear();
            TitleScreen();
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey(true);
            MainMenu();
        }

        public static void MainMenu() {
            bool userQuit = false;
            do {
                bool mnuChoValid = false;
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
                        mnuChoValid = true;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Please enter a number!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        System.Threading.Thread.Sleep(1000);
                    }
                } while (!mnuChoValid);
                if (mCho == 1) {
                    PlayGame();
                }
                else if (mCho == 2) {
                    ModifyRobot();
                }
                else if (mCho == 3) {
                    Rules();
                }
                else if (mCho == 4) {
                    userQuit = true;
                    Environment.Exit(0);
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Valid choice not entered. Please enter an integer between 1 and 4.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(1000);
                }
            } while (!userQuit);
        }

        public static void ModifyRobot() {
            bool goBackToMainMenu = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("             Modify Robot             ");
                Console.WriteLine();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" 1. Name Robot ({0})                  ", GlobalVars.robotsName);
                Console.WriteLine(" 2. Change Traction ({0})             ", GlobalVars.TractionType);
                Console.WriteLine(" 3. Change Passenger Bay Size ({0})   ", GlobalVars.PassengerBaySize);
                Console.WriteLine(" 4. Go To Main Menu                   ");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine();
                Console.WriteLine("Please enter your choice (1 - 4). . .");
                Console.WriteLine();
                string userChoice = Console.ReadLine();
                bool isNumeric = int.TryParse(userChoice, out mCho);
                if (isNumeric) {
                    if (mCho == 1) {
                        NameRobot();
                    }
                    else if (mCho == 2) {
                        SelectTractionType();
                    }
                    else if (mCho == 3) {
                        ChangeBaySize();
                    }
                    else if (mCho == 4) {
                        goBackToMainMenu = true;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Valid choice not entered. Please enter an integer between 1 and 4.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Please enter a number!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(1000);
                }
            } while (!goBackToMainMenu);
        }

        public static void NameRobot() {
            bool nameValid = false;
            bool nameConfirmed = false;
            do {
                do {
                    Console.Clear();
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("Current Robot Name: {0}", GlobalVars.robotsName);

                    Console.WriteLine("Please enter a valid name for your robot.");
                    Console.WriteLine("Make sure it starts with a capital letter and is more than three characters long.");
                    GlobalVars.robotsName = Console.ReadLine();
                    string rbFChr = GlobalVars.robotsName.Substring(0, 1);
                    string rbFChrUpper = rbFChr.ToUpper();
                    if (GlobalVars.robotsName.Length < 3 && rbFChr != rbFChrUpper) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Robot's name must be more than three characters long and start with a capital letter.");
                        Console.ForegroundColor = ConsoleColor.White;
                        nameValid = false;
                    }
                    else if (rbFChr != rbFChrUpper) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Robot's name must start with a capital letter.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        nameValid = false;
                    }
                    else if (GlobalVars.robotsName.Length < 3) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Robot's name must be more than three characters long.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        nameValid = false;
                    }
                    else {
                        nameValid = true;
                    }
                } while (nameValid == false);
                bool confirmationvalid = false;
                do {
                    Console.Clear();
                    Console.WriteLine("Robot name is valid. Are you sure you want to change your robot's name? (Y/N)");
                    string rNmConfQ = Console.ReadLine();
                    rNmConfQ = rNmConfQ.ToUpper();
                    if (rNmConfQ == "Y") {
                        nameConfirmed = true;
                        confirmationvalid = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("New robot name confirmed.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (rNmConfQ == "N") {
                        nameConfirmed = false;
                        confirmationvalid = true;
                        Console.WriteLine("Robot name not confirmed. You will be sent back to the name robot menu.");
                    }
                    else {
                        confirmationvalid = false;
                        Console.WriteLine("Please enter a valid character!");
                        System.Threading.Thread.Sleep(500);
                    }
                } while (confirmationvalid == false);
                if (nameConfirmed == false) {
                    GlobalVars.robotsName = "Fred";
                }
            } while (nameConfirmed == false);
        }

        public static void SelectTractionType() {
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
                    if (mCho == 1) {
                        inputValid = true;
                        GlobalVars.TractionType = "Wheels";
                        Console.WriteLine("Traction type successfully set to {0}", GlobalVars.TractionType);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else if (mCho == 2) {
                        inputValid = true;
                        GlobalVars.TractionType = "Tracks";
                        Console.WriteLine("Traction type successfully set to {0}", GlobalVars.TractionType);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else if (mCho == 3) {
                        inputValid = true;
                        GlobalVars.TractionType = "Skis";
                        Console.WriteLine("Traction type successfully set to {0}", GlobalVars.TractionType);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Valid choice not entered. Please enter an integer between 1 and 3.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Please enter a number!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(1000);
                }
            } while (!inputValid);
        }

        public static void ChangeBaySize() {
            bool inputValid = false;
            int mCho = 0;
            do {
                Console.Clear();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Current passenger bay size: {0}         ", GlobalVars.PassengerBaySize);
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
                    if (mCho == 1) {
                        inputValid = true;
                        GlobalVars.PassengerBaySize = "Small";
                        Console.WriteLine("Passenger bay size sucessfully set to {0}", GlobalVars.PassengerBaySize);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else if (mCho == 2) {
                        inputValid = true;
                        GlobalVars.PassengerBaySize = "Med";
                        Console.WriteLine("Passenger bay size sucessfully set to {0}", GlobalVars.PassengerBaySize);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else if (mCho == 3) {
                        inputValid = true;
                        GlobalVars.PassengerBaySize = "Large";
                        Console.WriteLine("Passenger bay size sucessfully set to {0}", GlobalVars.PassengerBaySize);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Valid choice not entered. Please enter an integer between 1 and 3.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Please enter a number!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(1000);
                }
            } while (!inputValid);
        }

        public static void PlayGame() {
            Console.Clear();
            InitialiseLandscapeData();
            CompleteInitialisations();
            EmptyLandscape();
            InitialiseLandscape();
            bool gameOver = false;
            bool gameWon = false;
            do {
                DrawLandscape();
                DisplayInfo();
                string move = EnterMove();
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
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

        public static void InitialiseLandscapeData() {
            using (StreamReader fileReader = new StreamReader(@"C:\Users\Conor\Documents\boarddata.txt")) {
                string line;
                int numeric = 0;
                int k = GlobalVars.landscapeSize - 1;
                int i = 0;
                int j = 0;
                do {
                    if (i == k) {
                        break;
                    }
                    line = fileReader.ReadLine();
                    bool isNumeric = int.TryParse(line, out numeric);
                    if (!isNumeric) {
                        line = line.Substring(0, 1);
                        GlobalVars.landscapeData[i, j] = line;
                        if (j == k) {
                            j = 0;
                            i++;
                        }
                        else {
                            j++;
                        }
                    }
                } while (!fileReader.EndOfStream);
            }
        }

        public static void CompleteInitialisations() {
            int k = GlobalVars.landscapeSize;
            for (int i = 0; i < k; i++) {
                for (int j = 0; j < k; j++) {
                    if (GlobalVars.landscape[i, j] == Convert.ToChar(GlobalVars.robotCell)) {
                        GlobalVars.RobLoc.currentX = i;
                        GlobalVars.RobLoc.currentY = j;
                        break;
                    }
                }
            }
        }

        public static void EmptyLandscape() {
            int k = GlobalVars.landscapeSize;
            for (int i = 0; i < k; i++) {
                for (int j = 0; j < k; j++) {
                    GlobalVars.landscape[i, j] = Convert.ToChar(GlobalVars.emptyCell);
                }
            }
        }

        public static void InitialiseLandscape() {
            GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] = Convert.ToChar(GlobalVars.robotCell);
            GlobalVars.landscape[0, 2] = Convert.ToChar(GlobalVars.personCell);
            GlobalVars.landscape[2, 7] = Convert.ToChar(GlobalVars.personCell);
            GlobalVars.landscape[6, 4] = Convert.ToChar(GlobalVars.personCell);
            GlobalVars.landscape[6, 9] = Convert.ToChar(GlobalVars.personCell);
            GlobalVars.landscape[3, 6] = Convert.ToChar("B");
        }

        public static void DrawLandscape() {
            Console.WriteLine();
            int k = GlobalVars.landscapeSize;
            for (int i = 0; i < k; i++) {
                for (int j = 0; j < k; j++) {
                    if (GlobalVars.landscape[i, j] == GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY]) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(GlobalVars.landscape[i, j]);
                        Console.Write(" ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else {
                        if (GlobalVars.landscapeData[i, j] == "G") {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(GlobalVars.landscape[i, j]);
                            Console.Write(" ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (GlobalVars.landscapeData[i, j] == "R") {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(GlobalVars.landscape[i, j]);
                            Console.Write(" ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (GlobalVars.landscapeData[i, j] == "W") {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(GlobalVars.landscape[i, j]);
                            Console.Write(" ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public static void DisplayInfo() {
            //
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Power Remaining: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(GlobalVars.powerRemaining);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            //
            Console.Write("Traction type: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(GlobalVars.TractionType);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            //
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Passenger bay 1: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(GlobalVars.PasBay.Slot1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            if (GlobalVars.PassengerBaySize == "Med") {
                //
                Console.Write("Passenger bay 2: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(GlobalVars.PasBay.Slot2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            else if (GlobalVars.PassengerBaySize == "Large") {
                //
                Console.Write("Passenger bay 2: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(GlobalVars.PasBay.Slot2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                //
                Console.Write("Passenger bay 3: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(GlobalVars.PasBay.Slot3);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            //
            Console.Write("People saved: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(GlobalVars.pplSaved);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static string EnterMove() {
            string move;
            Console.ForegroundColor = ConsoleColor.White;
            bool moveIsValid = false;
            bool moveIsWithinBounds = false;
            string entermove;
            do {
                do {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a move for {0} to take:", GlobalVars.robotsName);
                    entermove = Console.ReadLine();
                    entermove = entermove.ToUpper();
                    if (entermove == "N" || entermove == "E" || entermove == "S" || entermove == "W") {
                        moveIsValid = true;
                    }
                    else {
                        moveIsValid = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" Move invalid! You have entered a letter that is not a direction. Please try again.");
                        Console.WriteLine();
                    }
                } while (!moveIsValid);
                if (GlobalVars.RobLoc.currentX == 0 && entermove == "N") {
                    moveIsWithinBounds = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" Move invalid! You have attempted to move out of the grid. Please try again.");
                    Console.WriteLine();
                }
                else if (GlobalVars.RobLoc.currentY == 0 && entermove == "W") {
                    moveIsWithinBounds = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" Move invalid! You have attempted to move out of the grid. Please try again.");
                    Console.WriteLine();
                }
                else if (GlobalVars.RobLoc.currentY == 9 && entermove == "E") {
                    moveIsWithinBounds = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" Move invalid! You have attempted to move out of the grid. Please try again.");
                    Console.WriteLine();
                }
                else if (GlobalVars.RobLoc.currentX == 9 && entermove == "S") {
                    moveIsWithinBounds = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" Move invalid! You have attempted to move out of the grid. Please try again.");
                    Console.WriteLine();
                }
                else {
                    Console.WriteLine("Move Valid! Moving {0} in the direction of {1}.", GlobalVars.robotsName, entermove);
                    moveIsWithinBounds = true;
                }
            } while (!moveIsWithinBounds);
            move = entermove;
            return move;
        }

        public static void MoveRobot(string move) {
            bool slot2WasFilled = false;
            GlobalVars.RobLoc.newX = GlobalVars.RobLoc.currentX;
            GlobalVars.RobLoc.newY = GlobalVars.RobLoc.currentY;
            if (move == "N") {
                GlobalVars.RobLoc.newX--;
            }
            else if (move == "E") {
                GlobalVars.RobLoc.newY++;
            }
            else if (move == "S") {
                GlobalVars.RobLoc.newX++;
            }
            else if (move == "W") {
                GlobalVars.RobLoc.newY--;
            }
            GlobalVars.powerRemaining = GlobalVars.powerRemaining - 1;
            if (GlobalVars.PasBay.Slot2 == null) {
                PickUpPerson(move);
            }
            else if (GlobalVars.PasBay.Slot2 != null) {
                slot2WasFilled = true;
            }
            DropPeople(move);
            EraseRobot(slot2WasFilled);
            GlobalVars.RobLoc.currentX = GlobalVars.RobLoc.newX;
            GlobalVars.RobLoc.currentY = GlobalVars.RobLoc.newY;
            ReplaceRobot();
        }

        public static void PickUpPerson(string move) {
            if (GlobalVars.landscape[GlobalVars.RobLoc.newX, GlobalVars.RobLoc.newY] == Convert.ToChar(GlobalVars.personCell)) {
                if (GlobalVars.PasBay.Slot1 == null) {
                    GlobalVars.PasBay.Slot1 = GlobalVars.personCell;
                }
                else if (GlobalVars.PasBay.Slot2 == null) {
                    GlobalVars.PasBay.Slot2 = GlobalVars.personCell;
                }
            }
            if (GlobalVars.landscape[GlobalVars.RobLoc.newX, GlobalVars.RobLoc.newY] == GlobalVars.landscape[0, 2]) {
                GlobalVars.personIsSaved.person1 = true;
            }
            else if (GlobalVars.landscape[GlobalVars.RobLoc.newX, GlobalVars.RobLoc.newY] == GlobalVars.landscape[2, 7]) {
                GlobalVars.personIsSaved.person2 = true;
            }
            else if (GlobalVars.landscape[GlobalVars.RobLoc.newX, GlobalVars.RobLoc.newY] == GlobalVars.landscape[6, 4]) {
                GlobalVars.personIsSaved.person3 = true;
            }
            else if (GlobalVars.landscape[GlobalVars.RobLoc.newX, GlobalVars.RobLoc.newY] == GlobalVars.landscape[6, 9]) {
                GlobalVars.personIsSaved.person4 = true;
            }
        }

        public static void DropPeople(string move) {
            if (GlobalVars.landscape[GlobalVars.RobLoc.newX, GlobalVars.RobLoc.newY] == Convert.ToChar("B")) {
                if (GlobalVars.PasBay.Slot1 == GlobalVars.personCell || GlobalVars.PasBay.Slot2 == GlobalVars.personCell) {
                    if (GlobalVars.PasBay.Slot2 == GlobalVars.personCell) {
                        GlobalVars.PasBay.Slot2 = null;
                        GlobalVars.PasBay.Slot1 = null;
                        GlobalVars.pplSaved = GlobalVars.pplSaved + 2;
                    }
                    else if (GlobalVars.PasBay.Slot1 == GlobalVars.personCell) {
                        GlobalVars.PasBay.Slot1 = null;
                        GlobalVars.pplSaved = GlobalVars.pplSaved + 1;
                    }
                }
            }
        }

        public static void EraseRobot(bool slot2WasFilled) {
            if (slot2WasFilled == true) {
                if (GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] == GlobalVars.landscape[3, 6]) {
                    GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] = Convert.ToChar("B");
                }
                else if (GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] == GlobalVars.landscape[0, 2]) {
                    if (GlobalVars.personIsSaved.person1) {
                        GlobalVars.landscape[0, 2] = Convert.ToChar(GlobalVars.emptyCell);
                    }
                    else {
                        GlobalVars.landscape[0, 2] = Convert.ToChar(GlobalVars.personCell);
                    }
                }
                else if (GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] == GlobalVars.landscape[2, 7]) {
                    if (GlobalVars.personIsSaved.person2) {
                        GlobalVars.landscape[2, 7] = Convert.ToChar(GlobalVars.emptyCell);
                    }
                    else {
                        GlobalVars.landscape[2, 7] = Convert.ToChar(GlobalVars.personCell);
                    }
                }
                else if (GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] == GlobalVars.landscape[6, 4]) {
                    if (GlobalVars.personIsSaved.person3) {
                        GlobalVars.landscape[6, 4] = Convert.ToChar(GlobalVars.emptyCell);
                    }
                    else {
                        GlobalVars.landscape[6, 4] = Convert.ToChar(GlobalVars.personCell);
                    }
                }
                else if (GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] == GlobalVars.landscape[6, 9]) {
                    if (GlobalVars.personIsSaved.person4) {
                        GlobalVars.landscape[6, 9] = Convert.ToChar(GlobalVars.emptyCell);
                    }
                    else {
                        GlobalVars.landscape[6, 9] = Convert.ToChar(GlobalVars.personCell);
                    }
                }
                else {
                    GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] = Convert.ToChar(GlobalVars.emptyCell);
                }
            }
            else {
                if (GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] == GlobalVars.landscape[3, 6]) {
                    GlobalVars.landscape[3, 6] = Convert.ToChar("B");
                }
                else {
                    GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] = Convert.ToChar(GlobalVars.emptyCell);
                }
            }
        }

        public static void ReplaceRobot() {
            GlobalVars.landscape[GlobalVars.RobLoc.currentX, GlobalVars.RobLoc.currentY] = Convert.ToChar(GlobalVars.robotCell);
        }

        public static void YouWon() {
            Console.ForegroundColor = ConsoleColor.White;
            bool userPlayAgain = false;
            do {
                Console.Clear();
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

        public static void YouLost() {
            Console.ForegroundColor = ConsoleColor.White;
            bool userPlayAgain = false;
            do {
                Console.Clear();
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

        public static void Rules() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Rules:                                                   ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. The robot always starts in the top left of the grid.  ");
            Console.WriteLine("2. The robot can move in 4 directions (N, E, S, W).      ");
            Console.WriteLine("3. The robot cannot make diagonal moves.                 ");
            Console.WriteLine("4. The robot cannot move beyond the edge of the grid.    ");
            Console.WriteLine("5. The robot can carry two people in the passenger bay.  ");
            Console.WriteLine("6. If there is no room in the passenger bay, the person  ");
            Console.WriteLine("   will be left behind and won't be picked up            ");
            Console.WriteLine("7. The robot has 60 power units.                         ");
            Console.WriteLine("8. The player wins if all people are dropped off at base ");
            Console.WriteLine("   camp before the robot runs out of power.              ");
            Console.WriteLine("9. If the robot runs out of power before all players are ");
            Console.WriteLine("   at the base, the player loses.                        ");
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

        public static void TitleScreen() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = ("BotMod");
            Console.WriteLine(@"-----------------------------------------------------------------------");
            Console.WriteLine(@"  ________  ________  _________  _____ ______   ________  ________     ");
            Console.WriteLine(@" |\   __  \|\   __  \|\___   ___\\   _ \  _   \|\   __  \|\   ___ \    ");
            Console.WriteLine(@" \ \  \|\ /\ \  \|\  \|___ \  \_\ \  \\\__\ \  \ \  \|\  \ \  \_|\ \   ");
            Console.WriteLine(@"  \ \   __  \ \  \\\  \   \ \  \ \ \  \\|__| \  \ \  \\\  \ \  \ \\ \  ");
            Console.WriteLine(@"   \ \  \|\  \ \  \\\  \   \ \  \ \ \  \    \ \  \ \  \\\  \ \  \_\\ \ ");
            Console.WriteLine(@"    \ \_______\ \_______\   \ \__\ \ \__\    \ \__\ \_______\ \_______\");
            Console.WriteLine(@"     \|_______|\|_______|    \|__|  \|__|     \|__|\|_______|\|_______|");
            Console.WriteLine(@"-----------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("A Robot Game");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}