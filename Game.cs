using System;

namespace TicTacToe_AI
{
    class Game
    {
        public void TestAI()
        {
            AI _ai = new AI();
            string[] Board;
            int result;

            Board = new string[] { "O", " ", "O",
                                   "X", " ", "X",
                                   " ", " ", " " };
            result = _ai.aiTurn(Board, "X");
            if (result != 4)
            {
                Console.WriteLine("A. Result should be 4, got " + result);
            }

            Board = new string[] { " ", " ", " ",
                                   " ", " ", " ",
                                   " ", " ", " " };
            result = _ai.aiTurn(Board, "O");
            if (result != 0)
            {
                Console.WriteLine("B. Result should be 0, got " + result);
            }

            Board = new string[] { "O", "X", "O",
                                   " ", "X", "X",
                                   "O", " ", "X" };
            result = _ai.aiTurn(Board, "O");
            if (result != 3)
            {
                Console.WriteLine("C. Result should be 3, got " + result);
            }

            Board = new string[] { "O", " ", "O",
                                   " ", "X", " ",
                                   " ", " ", " " };
            result = _ai.aiTurn(Board, "X");
            if (result != 1)
            {
                Console.WriteLine("D. Result should be 1, got " + result);
            }

            Board = new string[] { "X", "O", " ",
                                   " ", "X", " ",
                                   " ", " ", "O" };
            result = _ai.aiTurn(Board, "X");
            if (result != 3)
            {
                Console.WriteLine("E. Result should be 3, got " + result);
            }

            Board = new string[] { "X", "O", "X",
                                   "O", "X", "X",
                                   " ", " ", "O" };
            result = _ai.aiTurn(Board, "X");
            if (result != 6)
            {
                Console.WriteLine("F. Result should be 6, got " + result);
            }

            Board = new string[] { "X", "O", " ", 
                                   " ", "X", " ",
                                   " ", " ", " " };
            result = _ai.aiTurn(Board, "O");
            if (result != 8)
            {
                Console.WriteLine("G. Result should be 8, got " + result);
            }

            Board = new string[] { "O", "X", " ", 
                                   " ", "O", " ",
                                   " ", " ", " " };
            result = _ai.aiTurn(Board, "X");
            if (result != 8)
            {
                Console.WriteLine("H. Result should be 8, got " + result);
            }

            Console.WriteLine("Finished!");
        }

        public Game(bool testing)
        {
            if (testing)
            {
                TestAI();
                return;
            }
            string[] Board = initBoard();
            Player Human = new Player { _Name = "human", _PlayerValue = "" };
            Player Computer = new Player { _Name = "computer", _PlayerValue = "" };
            initPlayerValues(Human, Computer);
            string whoWon = startTurns(Board, Human, Computer);
            Console.Clear();
            printBoard(Board);
            Console.WriteLine(whoWon + " won");
        }
        public string[] initBoard() // Initialise the board
        {
            string[] Board = new string[9];
            for (int i = 0; i < 9; i++)
            {
                Board[i] = " ";
            }
            return Board;
        }

        public void printBoard(string[] Board) // Prints the board
        {
            Console.WriteLine("┌─┬─┬─┐");
            Console.WriteLine(String.Format("│{0}│{1}│{2}│", Board[0], Board[1], Board[2]));
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine(String.Format("│{0}│{1}│{2}│", Board[3], Board[4], Board[5]));
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine(String.Format("│{0}│{1}│{2}│", Board[6], Board[7], Board[8]));
            Console.WriteLine("└─┴─┴─┘");
        }
        public string checkWin(string[] Board, string playerValue) // Checks if Noughts or Crosses won
        {
            /*
            Winning Board States
            [0,1,2] [3,4,5] [6,7,8] rows
            [0,3,6] [1,4,7] [2,5,8] columns
            [0,4,8] [2,4,6] diagonals
            */

            if (Board[4] == playerValue)//Spot with most commonly occuring win
            {
                int[][] winStates = { new int[] { 0, 4, 8 }, new int[] { 3, 4, 5 }, new int[] { 1, 4, 7 }, new int[] { 2, 4, 6 } }; // Possible win states if the player owns the spot 4
                if (checkWinStates(Board, playerValue, winStates)) { return "w"; }
            }
            else if (Board[0] == playerValue)//2nd most common
            {
                int[][] winStates = { new int[] { 0, 1, 2 }, new int[] { 0, 3, 6 } }; // Possible win states if the player owns the spot 0
                if (checkWinStates(Board, playerValue, winStates)) { return "w"; }
            }
            else if (Board[8] == playerValue)//Rules out rest of options
            {
                int[][] winStates = { new int[] { 6, 7, 8 }, new int[] { 2, 5, 8 } }; // Possible win states if the player owns the spot 8
                if (checkWinStates(Board, playerValue, winStates)) { return "w"; }
            }

            for (int i = 0; i < 9; i++)// Checks if the board is full
            {
                if (Board[i] == " ")
                {
                    return ""; // Board isn't full
                }
            }
            return "d"; // Board is full and hasn't won
        }

        public Boolean checkWinStates(string[] Board, string playerValue, int[][] winStates) // Checks through the inserted win states
        {
            for (int i = 0; i < winStates.Length; i++)// Iterate through winStates
            {
                int matches = 0;
                for (int j = 0; j < 3; j++) // Iterate through the values in the winStates
                {
                    if (Board[winStates[i][j]] == playerValue) { matches++; } // Checking whether or not the Board matches the win state
                    else { break; }
                }
                if (matches == 3)// If all 3 match then the player who matched them wins
                {
                    return true;
                }
            }
            return false;
        }

        public void initPlayerValues(Player Human, Player Computer) // Assign what player is Crosses or Noughts
        {
            Random rnd = new Random();
            int randNum = rnd.Next(2);
            string humanVal;
            string compVal;

            if (randNum == 0)
            {
                humanVal = "X";
                compVal = "O";
            }
            else
            {
                humanVal = "O";
                compVal = "X";
            }

            Human._PlayerValue = humanVal;
            Computer._PlayerValue = compVal;
        }

        public string startTurns(string[] Board, Player Human, Player Computer) // Start the game
        {
            if (Human._PlayerValue == "X")
            {
                humanTurn(Board, Human._PlayerValue);
            }

            AI ai = new AI();
            string checkWon;
            while (true)
            {
                computerTurn(Board, Computer._PlayerValue, ai);
                checkWon = checkWin(Board, Computer._PlayerValue);
                if (checkWon == "w")
                {
                    return "Computer";
                }
                else if (checkWon == "d")
                {
                    return "No one";
                }

                humanTurn(Board, Human._PlayerValue);
                checkWon = checkWin(Board, Human._PlayerValue);
                if (checkWon == "w")
                {
                    return "Human";
                }
                else if (checkWon == "d")
                {
                    return "No one";
                }
            }
        }

        public void humanTurn(string[] Board, string playerValue)
        {
            Console.Clear();
            Console.WriteLine("You are " + playerValue + "'s");
            printBoard(Board);
            Console.WriteLine("Enter a number from 1-9:");
            int selection = getInput(Board);
            Board[selection] = playerValue;
        }

        public void computerTurn(string[] Board, string playerValue, AI ai)
        {
            int computerChoice = ai.aiTurn(Board, playerValue);
            Board[computerChoice] = playerValue;
        }

        public int getInput(string[] Board)
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    int selection = Convert.ToInt32(input) - 1;
                    if (selection >= 0 && selection < 9 && Board[selection] == " ")
                    {
                        return selection;
                    }
                }
                catch { }
            }
        }
        static void Main(string[] args)
        {
            bool test = false;
            if (args.Length >= 2)
            {
                if (args[1] == "test")
                {
                    test = true;
                }
            }
            Game TicTacToe = new Game(test);
        }
    }

    class Player
    {
        public string _Name { get; set; }
        public string _PlayerValue { get; set; }
    }
}