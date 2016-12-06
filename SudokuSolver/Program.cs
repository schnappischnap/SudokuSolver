using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchnappiSchnap.Solvers;
using SchnappiSchnap.Extensions;

namespace Program
{
    static class Program
    {
        static void Main()
        {
            int[,] board = new int[,] {
                { 3, 0, 2, 0, 6, 0, 0, 0, 0 },
                { 0, 0, 9, 7, 0, 0, 0, 0, 0 },
                { 0, 6, 1, 9, 0, 0, 0, 0, 0 },
                { 0, 0, 7, 8, 9, 0, 3, 0, 0 },
                { 5, 0, 0, 0, 0, 0, 0, 0, 9 },
                { 0, 0, 8, 0, 2, 6, 1, 0, 0 },
                { 0, 0, 0, 0, 0, 8, 6, 9, 0 },
                { 0, 0, 0, 0, 0, 4, 7, 0, 0 },
                { 0, 0, 0, 0, 5, 0, 8, 0, 1 }
            };

            SudokuSolver solver = new SudokuSolver(board);

            int[,] solved = (int[,])board.Clone();
            if (!solver.TrySolveBoard(out solved))
                Console.Write("Sorry 'bout it");
            else
                Print9x9Board(solved, board);

            Console.ReadKey();
        }

        static void Print9x9Board(int[,] board)
        {
            if (board.GetLength(0) != 9 && board.GetLength(1) != 9)
                throw new ArgumentException("Sodoku writer board isn't 9x9", "board");
            

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; ++x)
                {
                    Console.Write(board[y, x] + " ", ConsoleColor.White, ConsoleColor.Black);

                    if ((x + 1) % 3 == 0 && (x + 1) != 9)
                        Console.Write("| ");
                }

                Console.WriteLine();

                if ((y + 1) % 3 == 0 && (y + 1) != 9)
                    Console.WriteLine("- - - + - - - + - - -");
            }            
        }

        static void Print9x9Board(int[,] board, int[,] originalBoard)
        {
            if (board.GetLength(0) != 9 && board.GetLength(1) != 9)
                throw new ArgumentException("Sodoku writer board isn't 9x9", "board");


            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; ++x)
                {
                    if (originalBoard[y, x] == 0)
                    {
                        PrintColoured(board[y, x].ToString(), ConsoleColor.Green, ConsoleColor.Black);
                        Console.Write(" ");
                    }
                    else
                        PrintColoured(board[y, x] + " ", ConsoleColor.White, ConsoleColor.Black);

                    if ((x + 1) % 3 == 0 && (x + 1) != 9)
                        Console.Write("| ");
                }

                Console.WriteLine();

                if ((y + 1) % 3 == 0 && (y + 1) != 9)
                    Console.WriteLine("- - - + - - - + - - -");
            }
        }

        static private void PrintColoured(string value, ConsoleColor fg, ConsoleColor bg)
        {
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            Console.Write(value);
            Console.ResetColor();
        }
    }
}
