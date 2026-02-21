namespace FiguresLibrary
{
    /// <summary>
    /// Класс окружности
    /// </summary>
    public class Circle : Figure, IComparable<Circle>
    {
        /// <summary>
        /// Радиус окружности
        /// </summary>
        private double _radius;

        /// <summary>
        /// Точка на окружности
        /// </summary>
        private Point _edgePoint;

        /// <summary>
        /// Центр окружности
        /// </summary>
        private Point _centerPoint;

        /// <summary>
        /// Конструктор окружности
        /// </summary>
        /// <param name="typeFigure">Тип фигуры</param>
        /// <param name="colorFigure">Цвет фигуры</param>
        /// <param name="centerPoint">Центр окружности</param>
        /// <param name="edgePoint">Точка на окружности</param>
        public Circle(string typeFigure, string colorFigure, Point centerPoint, Point edgePoint) : base(typeFigure, colorFigure)
        {
            if (centerPoint.x == edgePoint.x && edgePoint.y == centerPoint.y)
                throw new ArgumentException("Радиус 0!");
            _edgePoint = edgePoint;
            _centerPoint = centerPoint;
            _radius = Math.Sqrt(Math.Pow((_edgePoint.x - _centerPoint.x), 2) + Math.Pow((_edgePoint.y - _centerPoint.y), 2));
        }

        /// <summary>
        /// Вычисляет площадь окружности
        /// </summary>
        /// <returns>Площадь окружности</returns>
        public override double GetArea() => Math.PI * _radius * _radius;

        /// <summary>
        /// Вычисляет длину окружности
        /// </summary>
        /// <returns>Длина окружности</returns>
        public double GetLength() => 2 * _radius * Math.PI;

        /// <summary>
        /// Проверяет находится ли окружность в первой четверти координатной плоскости
        /// </summary>
        /// <returns>True если окружность в первой четверти</returns>
        public bool IsInFirstQuarter()
        {
            return _centerPoint.x >= _radius && _centerPoint.y >= _radius;
        }

        /// <summary>
        /// Возвращает информацию об окружности
        /// </summary>
        /// <returns>Строка с информацией об окружности</returns>
        public override string GetInfo()
        {
            return $"Радиус окружности: {_radius}\nПлощадь окружности: {GetArea()}\nЦвет: {ColorFigure}\nТип: {TypeFigure}";
        }

        /// <summary>
        /// Сравнивает окружности по площади
        /// </summary>
        /// <param name="circle">Окружность для сравнения</param>
        /// <returns>Результат сравнения</returns>
        public int CompareTo(Circle? circle)
        {
            if (circle == null)
                return 1;
            return GetArea().CompareTo(circle.GetArea());
        }

        /// <summary>
        /// Возвращает центр окружности
        /// </summary>
        /// <returns>Точка центра окружности</returns>
        public Point GetCenter() => _centerPoint;

        /// <summary>
        /// Возвращает точку на окружности
        /// </summary>
        /// <returns>Точка на окружности</returns>
        public Point GetEdgePoint() => _edgePoint;
    }
}