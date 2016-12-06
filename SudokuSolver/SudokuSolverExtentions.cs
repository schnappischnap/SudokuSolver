using System;

namespace SchnappiSchnap.Extensions
{
    public static class SudokuSolverExtentions
    {
        /// <summary>
        /// Determines if the integer is a square of another integer.
        /// </summary>
        /// <returns>True if the input is a perfect square, else false.</returns>
        public static bool IsPerfectSquare(this int i)
        {
            return Math.Sqrt(i) % 1 == 0;
        }
    }
}
