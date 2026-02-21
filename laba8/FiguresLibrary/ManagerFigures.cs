namespace FiguresLibrary
{
    /// <summary>
    /// Класс для работы с коллекцией фигур
    /// </summary>
    public class ManagerFigures
    {
        /// <summary>
        /// Список фигур
        /// </summary>
        public List<Figure> figures = new List<Figure>();

        /// <summary>
        /// Загружает фигуры из файла
        /// </summary>
        /// <param name="path">Путь к файлу с данными фигур</param>
        public ManagerFigures(string path)
        {
            string? line;
            string[] parts = new string[6];
            if (!File.Exists(path))
            {
                throw new ArgumentException("Такого файла нет!");
            }

            using (StreamReader reader = new StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    parts = line.Split();
                    string type = parts[0];
                    string color = parts[parts.Length - 1];

                    if (type.ToLower() == "окружность" && parts.Length == 6)
                    {
                        try
                        {
                            Point center = new Point(int.Parse(parts[1]), int.Parse(parts[2]));
                            Point edge = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
                            figures.Add(new Circle(type, color, center, edge));
                        }
                        catch (FormatException ex)
                        {
                            throw new ArgumentException($"Ошибка в данных окружности: {line}", ex);
                        }
                    }
                    else if (type.ToLower() == "многоугольник")
                    {
                        try
                        {
                            int sideLength = int.Parse(parts[1]);
                            int numberOfSides = parts.Length - 2;

                            int[] sides = new int[numberOfSides];
                            for (int i = 0; i < numberOfSides; i++)
                            {
                                sides[i] = sideLength;
                            }

                            figures.Add(new RegularPolygons(type, color, sides));
                        }
                        catch (FormatException ex)
                        {
                            throw new ArgumentException($"Ошибка в данных многоугольника: {line}", ex);
                        }
                    }
                }
            }
        }
    }
}