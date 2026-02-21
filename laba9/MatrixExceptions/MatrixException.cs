namespace MatrixExceptionsLibrary
{
    /// <summary>
    /// Базовое исключение для ошибок матриц
    /// </summary>
    public class MatrixException : Exception
    {
        public MatrixException(string message) : base(message) { }
    }
}
