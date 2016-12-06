using System;
using SchnappiSchnap.Extensions;

namespace SchnappiSchnap.Solvers
{
    public class SudokuSolver
    {
        private struct Coordinate
        {
            public int X { get; }
            public int Y { get; }

            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private readonly int[,] inputBoard;
        private readonly int length;
        private readonly int squareSize;
        private int[,] outputBoard;

        public SudokuSolver(int [,] board)
        {
            if (board.GetLength(0) != board.GetLength(1))
                throw new ArgumentException("Sodoku solver board isn't square", "board");

            if (!board.GetLength(0).IsPerfectSquare())
                throw new ArgumentException("Sodoku solver board length isn't a perfect square", "board");

            inputBoard = board;
            length = board.GetLength(0);
            squareSize = Convert.ToInt32(Math.Sqrt(length));
            outputBoard = (int[,])inputBoard.Clone();

            for (int y = 0; y < board.GetLength(0); ++y)
            {
                for (int x = 0; x < board.GetLength(0); ++x)
                {
                    if (board[y, x] == 0)
                        continue;
                    if (InRow(new Coordinate(x, y), board[y, x]) ||
                        InColumn(new Coordinate(x, y), board[y, x]) ||
                        InSquare(new Coordinate(x, y), board[y, x]))
                        throw new ArgumentException("Sodoku solver input has errors", "board");
                }
            }
        }

        /// <summary>
        /// Attempts to solve the sodoku board. 
        /// </summary>
        /// <param name="solvedBoard">When the method returns successfully, 
        /// contains the solved board, else the input board.</param>
        /// <returns>True if the board was solved successfully, else false.</returns>
        public bool TrySolveBoard(out int[,] solvedBoard)
        {
            if(SolveCell(new Coordinate(0, 0)))
            {
                solvedBoard = outputBoard;
                return true;
            }
            else
            {
                solvedBoard = inputBoard;
                return false;
            }
        }

        /// <summary>
        /// Recursively attempts to solve the sudoku board.
        /// </summary>
        /// <param name="coord">The coordinate of the current cell to be solved.</param>
        /// <returns>True if the board was solved successfully, else false.</returns>
        private bool SolveCell(Coordinate coord)
        {
            Coordinate nextCoord = NextCoordinate(coord, length);

            // The coordinate is off the board, the board is solved.
            if (coord.Y >= length)
                return true;

            if (InOriginal(coord))
            {
                if (SolveCell(nextCoord))
                    return true;
                else
                    return false;
            }

            for (int i = 1; i <= length; ++i)
            {
                if(!InRow(coord,i) && !InColumn(coord,i) && !InSquare(coord,i))
                {
                    outputBoard[coord.Y, coord.X] = i;
                    if (SolveCell(nextCoord))
                        return true;
                }
            }
            
            outputBoard[coord.Y, coord.X] = 0;
            return false;
        }

        /// <summary>
        /// Determines whether the cell at the specified coordinate is in 
        /// the input board.
        /// </summary>
        /// <param name="coord">The coordinate to test.</param>
        /// <returns>True if the cell at the specified coordinate is in 
        /// the input board, else false.</returns>
        private bool InOriginal(Coordinate coord)
        {
            return inputBoard[coord.Y, coord.X] != 0;
        }

        /// <summary>
        /// Determines whether the value can be found in the same row of the 
        /// output board as the specified coordinate.
        /// </summary>
        /// <param name="coord">The coordinate to test.</param>
        /// <param name="value">The value to test.</param>
        /// <returns>True if the row of the specified coordinate of the output 
        /// board contains the specified value, else false.</returns>
        private bool InRow(Coordinate coord, int value)
        {
            for(int i = 0; i < length; ++i)
            {
                if (i != coord.X && outputBoard[coord.Y, i] == value)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the value can be found in the same column of the 
        /// output board as the specified coordinate.
        /// </summary>
        /// <param name="coord">The coordinate to test.</param>
        /// <param name="value">The value to test.</param>
        /// <returns>True if the column of the specified coordinate of the 
        /// output board contains the specified value, else false.</returns>
        private bool InColumn(Coordinate coord, int value)
        {
            for (int i = 0; i < length; ++i)
            {
                if (i != coord.Y && outputBoard[i, coord.X] == value)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the value can be found in the same square of 
        /// the output board as the specified coordinate.
        /// </summary>
        /// <param name="coord">The coordinate to test.</param>
        /// <param name="value">The value to test.</param>
        /// <returns>True if the square of the specified coordinate of the 
        /// output board contains the specified value, else false.</returns>
        private bool InSquare(Coordinate coord, int value)
        {
            int xStart = coord.X - (coord.X % squareSize);
            int yStart = coord.Y - (coord.Y % squareSize);

            for (int y = yStart; y < (yStart + squareSize); ++y)
            {
                for(int x = xStart; x < (xStart + squareSize); ++x)
                {
                    if ((x != coord.X || y != coord.Y) && outputBoard[y, x] == value)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the next coordinate, reading left-to-right and top-to-bottom.
        /// </summary>
        /// <param name="currentCoord"></param>
        /// <param name="gridLength"></param>
        /// <returns>(x+1, y) if x+1 is less than gridLength, else (0, y+1)</returns>
        private Coordinate NextCoordinate(Coordinate currentCoord, int gridLength)
        {
            if(currentCoord.X + 1 < gridLength)
                return new Coordinate(currentCoord.X + 1, currentCoord.Y);
            else
                return new Coordinate(0, currentCoord.Y + 1);
        }

    }
}
