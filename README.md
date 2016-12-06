# SudokuSolver
A C# console application to solve 9x9 sudoku boards.

- The SudokuSolver class can solve any valid sudoku boards, regardless of size.
- The board is solved recursively using the `SolveCell` method:

```
SolveCell(currentCoordinate):
  If currentCoordinate is off the board:
    Return true
  If currentCoordinate is in original board:
    If SolveCell(nextCoordinate) == true:
      Return true
    Else:
      Return false
  For i = 0, i < board length, ++i:
    If currentCoordinate is not in row, col or square:
      Set coordinate to i
      If SolveCell(nextCoordinate) == true:
        Return true
  Set coordinate to 0
  Return false
```
