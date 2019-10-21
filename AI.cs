using System.Collections.Generic;
using System;

namespace TicTacToe_AI
{

    class AI
    {
        private bool debug = false;

        public AI() { }

        public int aiTurn(string[] board, string aiToken)
        {
            float[] scores = new float[9];
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != " ")
                {
                    scores[i] = -1000;
                    continue;
                }
                scores[i] = aiFindRecurse(board, i, aiToken);
            }

            if(debug) {
                printScores(scores);
            }
            int spot = 0;
            float highscore = 0;
            bool firstScore = true;
            for (int i = 0; i < 9; i++)
            {
                if (scores[i] == -1000)
                {
                    continue;
                }
                if (firstScore || scores[i] > highscore)
                {
                    highscore = scores[i];
                    spot = i;
                    firstScore = false;
                }
            }

            return spot;
        }

        private float aiFindRecurse(string[] board, int spot, string aiToken)
        {
            string[] boardCopy = copyBoard(board);

            boardCopy[spot] = aiToken;
            if (checkWin(boardCopy, aiToken))
            {
                return 10;
            }
            else if (checkDraw(boardCopy))
            {
                return 0;
            }

            float score = fakePlayer(boardCopy, aiToken);
            return score;
        }

        public void printBoard(string[] board) // Prints the board
        {
            Console.WriteLine("┌─┬─┬─┐");
            Console.WriteLine(String.Format("│{0}│{1}│{2}│", board[0], board[1], board[2]));
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine(String.Format("│{0}│{1}│{2}│", board[3], board[4], board[5]));
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine(String.Format("│{0}│{1}│{2}│", board[6], board[7], board[8]));
            Console.WriteLine("└─┴─┴─┘");
        }

        public void printScores(float[] scores) // Prints the scores
        {
            Console.WriteLine("┌─┬─┬─┐");
            Console.WriteLine(String.Format("│{0}│{1}│{2}│", scores[0], scores[1], scores[2]));
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine(String.Format("│{0}│{1}│{2}│", scores[3], scores[4], scores[5]));
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine(String.Format("│{0}│{1}│{2}│", scores[6], scores[7], scores[8]));
            Console.WriteLine("└─┴─┴─┘");
        }

        private float fakePlayer(string[] board, string aiToken)
        {
            string playerToken;
            if (aiToken == "X")
            {
                playerToken = "O";
            }
            else
            {
                playerToken = "X";
            }

            float score = 0;
            int scoreCount = 0;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != " ")
                {
                    continue;
                }
                string[] boardCopy = copyBoard(board);
                boardCopy[i] = playerToken;
                if (checkWin(boardCopy, playerToken))
                {
                    if (debug)
                    {
                        printBoard(board);
                        Console.WriteLine("Player win @ spot " + i);
                    }
                    return -10;
                }
                else
                {
                    float highScore = 0;
                    int bestSpot = 0;
                    bool firstScore = true;
                    for (int j = 0; j < 9; j++)
                    {
                        if (boardCopy[j] != " ")
                        {
                            continue;
                        }
                        var aiScore = aiFindRecurse(boardCopy, j, aiToken);
                        if(firstScore || aiScore>highScore) {
                            firstScore = false;
                            highScore = aiScore;
                            bestSpot=j;
                        }
                    }

                    // printBoard(boardCopy);
                    // Console.WriteLine("Best AI spot " + bestSpot + " with score " + highScore);
                    score+=highScore; // Assume AI will choose highest scoring branches again in the future
                    scoreCount++;
                }
            }
    
            float adjustedScore = 0;
            if( scoreCount!=0) {
                adjustedScore = score / scoreCount;
            }                            
            if(debug) 
            {
                printBoard(board);                
                Console.WriteLine("Returning score " + adjustedScore);
            }
            
            return adjustedScore;
        }

        private bool checkWin(string[] board, string playerToken)
        {
            /*
            Winning Board States
            [0,1,2] [3,4,5] [6,7,8] rows
            [0,3,6] [1,4,7] [2,5,8] columns
            [0,4,8] [2,4,6] diagonals
            */

            int[][] winStates = {new int[] {0,1,2},new int[] {3,4,5},new int[] {6,7,8},
                                 new int[] {0,3,6},new int[] {1,4,7},new int[] {2,5,8},
                                 new int[] {0,4,8},new int[] {2,4,6}}; // the indexes where it has wins
            for (int i = 0; i < 8; i++) // 8 is length of winstates
            {
                int num = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (board[winStates[i][j]] == playerToken)
                    {
                        num++;
                    }
                }
                if (num == 3)
                {
                    return true;
                }
            }

            return false;
        }


        private bool checkDraw(string[] board)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == " ") // Check for blank squares
                {
                    return false;
                }
            }
            return true;
        }

        private string[] copyBoard(string[] board)
        {
            string[] copy = new string[9];
            for (int i = 0; i < 9; i++)
            {
                copy[i] = board[i];
            }

            return copy;
        }
    }

    class OpenSpot
    {
        public int spot { get; set; }
        public int score { get; set; }
    }
}