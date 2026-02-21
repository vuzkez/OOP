using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixExceptionsLibrary
{
    /// <summary>
    /// Исключение при выходе за границы матрицы
    /// </summary>
    public class MatrixIndexOutOfRangeException : MatrixException
    {
        public int Row { get; }
        public int Col { get; }
        public int MaxRows { get; }
        public int MaxCols { get; }

        public MatrixIndexOutOfRangeException(int row, int col, int maxRows, int maxCols)
            : base($"Индекс [{row}, {col}] вне границ матрицы {maxRows}x{maxCols}")
        {
            Row = row;
            Col = col;
            MaxRows = maxRows;
            MaxCols = maxCols;
        }
    }
}
