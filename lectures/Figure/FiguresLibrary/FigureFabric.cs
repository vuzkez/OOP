using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLibrary
{
    public class FigureFabric
    {
        public static Figure getNewFigure(int maxX,int maxY,int minX = 0,int minY = 0)
        {
            Figure figure = null;
            Random rnd = new Random();

            int typeFigure = rnd.Next(0,2);
            int x1 = rnd.Next(minX,maxX);
            int y1 = rnd.Next(minY,maxY);
            int side = rnd.Next(1,Math.Min(maxX,maxY) / 8);
            Point[] points =
            {
                new Point(x1,y1),
                new Point(x1 + side, y1 + side)
            };

            switch (typeFigure)
            {
                // Square
                case 0:
                    figure = new Quadrant(points);
                    break;
                // Circle
                case 1:
                    figure = new Circle(points);
                    break;
            }
            return figure;
        }
    }
}
