using System;

namespace SudokuSolver
{
    class SudokuBoard
    {
        private char[,] board;

        public SudokuBoard(char[,] initialBoard)
        {
            board = (char[,])initialBoard.Clone();
        }

        public void PrintBoard()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write(board[row, col] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        public bool Solve()
        {
            return SolveSudoku(0, 0);
        }

        private bool SolveSudoku(int row, int col)
        {
            if (row == 9)
            {
                row = 0;
                if (++col == 9)
                    return true;
            }
            if (board[row, col] != ' ')
                return SolveSudoku(row + 1, col);

            for (char num = '1'; num <= '9'; num++)
            {
                if (IsValidMove(row, col, num))
                {
                    board[row, col] = num;
                    if (SolveSudoku(row + 1, col))
                        return true;
                }
            }
            board[row, col] = ' ';
            return false;
        }

        private bool IsValidMove(int row, int col, char num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[row, i] == num) return false;
                if (board[i, col] == num) return false;
                int boxRowOffset = (row / 3) * 3;
                int boxColOffset = (col / 3) * 3;
                if (board[boxRowOffset + (i / 3), boxColOffset + (i % 3)] == num)
                    return false;
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            char[,] initialBoard = new char[,]
            {
{' ', ' ', '5', ' ', ' ', ' ', ' ', ' ', ' '},
{' ', ' ', ' ', ' ', ' ', '1', '8', ' ', ' '},
{'2', '7', '8', '3', '5', ' ', ' ', ' ', ' '},
{' ', ' ', ' ', ' ', '3', ' ', ' ', '2', '8'},
{' ', '6', ' ', ' ', '4', ' ', ' ', '1', ' '},
{'8', '1', ' ', ' ', '7', ' ', ' ', ' ', ' '},
{' ', ' ', ' ', ' ', '2', '5', '6', '3', '7'},
{' ', ' ', '3', '7', ' ', ' ', ' ', ' ', ' '},
{' ', ' ', ' ', ' ', ' ', ' ', '4', ' ', ' '}
            };

            SudokuBoard sudoku = new SudokuBoard(initialBoard);
            Console.WriteLine("Main board:");
            sudoku.PrintBoard();
            Console.WriteLine("Solving ...");
            if (sudoku.Solve())
            {
                Console.WriteLine("Sudoku solved:");
                sudoku.PrintBoard();
            }
            else
            {
                Console.WriteLine("Sudoku is unsolvable.");
            }

            Console.ReadLine();
        }
    }
}