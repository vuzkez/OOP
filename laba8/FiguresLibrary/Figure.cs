namespace FiguresLibrary
{
    /// <summary>
    /// Абстрактный базовый класс для геометрических фигур
    /// </summary>
    public abstract class Figure
    {
        private string _typeFigure;
        private string _colorFigure;

        /// <summary>
        /// Конструктор базового класса фигуры
        /// </summary>
        /// <param name="typeFigure">Тип фигуры</param>
        /// <param name="colorFigure">Цвет фигуры</param>
        public Figure(string typeFigure, string colorFigure)
        {
            _typeFigure = typeFigure;
            _colorFigure = colorFigure;
        }

        /// <summary>
        /// Вычисляет площадь фигуры
        /// </summary>
        /// <returns>Площадь фигуры</returns>
        public abstract double GetArea();

        /// <summary>
        /// Возвращает информацию о фигуре
        /// </summary>
        /// <returns>Строка с информацией о фигуре</returns>
        public abstract string GetInfo();

        /// <summary>
        /// Тип фигуры
        /// </summary>
        public string TypeFigure => _typeFigure;

        /// <summary>
        /// Цвет фигуры
        /// </summary>
        public string ColorFigure => _colorFigure;
    }
}