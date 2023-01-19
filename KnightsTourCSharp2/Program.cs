using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace KnightsTourCSharp2
{
    internal class Program
    {

        /* Global Variables */

        // Size of our chess board.
        public const int N = 8;

        // Parallel arrays that show every possible legal move a knight can make on a chess board
        static int[] horizontal = new int[8] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] vertical   = new int[8] { -1, -2, -2, -1, 1, 2, 2, 1 };

        /* 2D Array that represents a chess board and the amount 
         * of legal moves that can be made from each square. */
        static int[,] access = new int[N, N]
        {   
            { 2, 3, 4, 4, 4, 4, 3, 2 },
            { 3, 4, 6, 6, 6, 6, 4, 3 },
            { 4, 6, 8, 8, 8, 8, 6, 4 },
            { 4, 6, 8, 8, 8, 8, 6, 4 },
            { 4, 6, 8, 8, 8, 8, 6, 4 },
            { 4, 6, 8, 8, 8, 8, 6, 4 },
            { 3, 4, 6, 6, 6, 6, 4, 3 },
            { 2, 3, 4, 4, 4, 4, 3, 2 }
        };


        static void Main()
        {
            int[,] board = new int[N, N];

            int startRow = 3;
            int startCol = 3;

            int counter = runTour(board, startRow, startCol);

            printBoard(board, counter);

            Console.Write("Highest move completed: {0}", counter);
            Console.ReadLine();
        }

        static int runTour(int[,] board, int startRow, int startCol)
        {
            /* At the start of the Knights Tour, there are valid
             * moves from every square so this will start false */
            bool noValidMove = false;

            /* Setting up our starting point. This square will be 
             * marked as 1, the first square the knight is on. */
            int moveCounter = 1;
            int curRow = startRow;
            int curCol = startCol;

            // Set value of the starting square to 1 to mark first position
            board[curRow,curCol] = moveCounter;

            /* Contained in the while loop is the moving of the knight 
             * mechanism. This while loop should run 64 times */
            while (moveCounter <= N*N && noValidMove == false)
            {
                int bestAccess = 10;
                int[] accessMoves = new int[8]; // This array keeps track of our accessibility for each move
                int bestMove = 10; // The move value with the least accessibility. 

                /* For loop checks for the least accesible move and then chooses that move. */
                for (int move = 0; move < horizontal.Length; move++)
                {
                    /* If the move doesnt take the Knight off the board, and the square has not been previously moved to... */
                    if (isValid(board, move, curRow, curCol))
                    {
                        accessMoves[move] = access[curRow + vertical[move], curCol + horizontal[move]];

                        /* If the move is valid, subtract one from the accessibility array, as no matter what, it cant 
                         * move to where you are now, then record the accessibility number in the array accessMoves */
                        if (accessMoves[move] > 0)
                        {
                            access[curRow + vertical[move], curCol + horizontal[move]]--;

                            if (accessMoves[move] <= bestAccess)
                            {
                                bestAccess = accessMoves[move];
                                bestMove = move;
                            }
                        }
                    }
                    if (move == 7)
                    {
                        if (bestAccess == 10)
                        {
                            noValidMove = true;
                        }
                        else
                        {
                            makeMove(board, ref curRow, ref curCol, bestMove);
                            moveCounter++;
                            board[curRow, curCol] = moveCounter;
                            break;
                        }
                    }
                }
            }
            return moveCounter;
        }

        /* Checks to ensure Knight is both on the board and is 
         * not moving to a spot it has already moved to before. */
        static bool isValid(int[,] board, int move, int row, int col)
        {
            if (onBoard(move, row, col) && squareCheck(board, move, row, col))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* Function ensures the Knight cannot go out of bounds, off of the chess board. */
        static bool onBoard(int move, int row, int col)
        {
            if (row + vertical[move] > (N - 1)
        || row + vertical[move] < 0)
            {
                return false;
            }
            else if (col + horizontal[move] > (N - 1)
                || col + horizontal[move] < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /* Function checks to make sure the chess piece has not already moved here previously */
        static bool squareCheck(int[,] board, int move, int row, int col)
        {
            if (board[row + vertical[move],col + horizontal[move]] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* Changes the row and column to the position of the valid move */
        static void makeMove(int[,] board, ref int curRow, ref int curCol, int bestMove)
        {
            curRow += vertical[bestMove];
            curCol += horizontal[bestMove];
        }

        static void printBoard(int[,] board, int counter)
        {
            Console.WriteLine("******************* BOARD ARRAY ******************************\n");
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    if (board[row,col] != counter)
                    {
                        Console.Write($"{board[row, col]}\t");
                    }
                    else
                    {
                        Console.Write($"*{board[row, col]}*\t");
                    }
                }
                Console.WriteLine("\n");
            }
        }
    }
}
