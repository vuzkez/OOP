using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixExceptionsLibrary
{
    /// <summary>
    /// Исключение при некорректных размерах матрицы
    /// </summary>
    public class MatrixSizeException : MatrixException
    {
        public MatrixSizeException(string message) : base(message) { }
    }
}
