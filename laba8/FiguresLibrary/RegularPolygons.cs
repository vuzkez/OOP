namespace FiguresLibrary
{
    /// <summary>
    /// Класс правильного многоугольника
    /// </summary>
    public class RegularPolygons : Figure, IComparable<RegularPolygons>
    {
        /// <summary>
        /// Массив сторон многоугольника
        /// </summary>
        private int[] _sides;

        /// <summary>
        /// Конструктор правильного многоугольника
        /// </summary>
        /// <param name="typeFigure">Тип фигуры</param>
        /// <param name="colorFigure">Цвет фигуры</param>
        /// <param name="sides">Массив сторон многоугольника</param>
        public RegularPolygons(string typeFigure, string colorFigure, params int[] sides)
        : base(typeFigure, colorFigure)
        {
            foreach (int i in sides)
                if (i <= 0)
                    throw new ArgumentException("Размер стороны меньше или равен 0!");
            if (sides.Length < 3)
                throw new ArgumentException("Меньше 3 сторон!");
            if (!sides.All(s => s == sides[0]))
                throw new ArgumentException("Стороны разные!");
            _sides = sides;
        }

        /// <summary>
        /// Вычисляет площадь правильного многоугольника
        /// </summary>
        /// <returns>Площадь многоугольника</returns>
        public override double GetArea()
        {
            double sideLength = _sides[0];
            int n = _sides.Length;
            double area = (n * sideLength * sideLength) / (4.0 * Math.Tan(Math.PI / n));
            return Math.Round(area, 2);
        }

        /// <summary>
        /// Возвращает информацию о многоугольнике
        /// </summary>
        /// <returns>Строка с информацией о многоугольнике</returns>
        public override string GetInfo()
        {
            return $"Тип фигуры: {GetTypeFigure()}\nПлощадь фигуры: {GetArea()}\nЦвет: {ColorFigure}";
        }

        /// <summary>
        /// Сравнивает многоугольники по площади
        /// </summary>
        /// <param name="regularPolygons">Многоугольник для сравнения</param>
        /// <returns>Результат сравнения</returns>
        public int CompareTo(RegularPolygons? regularPolygons)
        {
            if (regularPolygons == null)
                return 1;
            return GetArea().CompareTo(regularPolygons.GetArea());
        }

        /// <summary>
        /// Определяет тип фигуры на основе количества сторон
        /// </summary>
        /// <returns>Название типа фигуры</returns>
        private string GetTypeFigure()
        {
            if (_sides.Length > 4)
            {
                return $"правильный многоугольник с {_sides.Length} сторонами";
            }
            else if (_sides.Length == 4)
            {
                return "квадрат";
            }
            else
            {
                return "треугольник";
            }
        }
    }
}