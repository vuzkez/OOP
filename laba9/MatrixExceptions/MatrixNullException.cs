using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixExceptionsLibrary
{
    /// <summary>
    /// Исключение при передаче null
    /// </summary>
    public class MatrixNullException : MatrixException
    {
        public MatrixNullException(string paramName) : base($"Параметр '{paramName}' не может быть null") { }
    }
}
