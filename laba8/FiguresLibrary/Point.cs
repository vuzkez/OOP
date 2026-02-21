namespace FiguresLibrary
{
    /// <summary>
    /// Класс точки в двумерном пространстве
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Координата X точки
        /// </summary>
        public int x { get; }

        /// <summary>
        /// Координата Y точки
        /// </summary>
        public int y { get; }

        /// <summary>
        /// Конструктор точки
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}