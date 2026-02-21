using FiguresLibrary;

namespace ConsoleFigure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Figure> figures = new List<Figure>();
            int n = 10;

            for (int i = 0; i < n; i++)
            {
                Figure figure = FigureFabric.getNewFigure(1000,1000);
                figures.Add(figure);
            }

            foreach (Figure figure in figures)
                Console.WriteLine(figure);

            Console.ReadLine();
        }
    }
}
