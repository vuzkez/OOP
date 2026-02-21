using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawLibrary;

namespace FiguresLibrary
{
    public class Circle : Figure
    {
        public Circle(Point[] points) 
            : base(points, TypeFigure.Circle) 
        {
            
        }

        public double getR()
        {
            return points[0].getLen(points[1]);
        }

        public override double getP()
        {
            return 2 * Math.PI * getR();
        }

        public override double getS()
        {
            return Math.PI * getR() * getR();
        }

        public override bool isPoint(Point point)
        {
            bool isPnt = false;
            if (Math.Pow(point.x - points[0].x, 2) + Math.Pow(point.y - points[0].y, 2) <= Math.Pow(getR(), 2))
            {
                isPnt = true;
            }

            return isPnt;
        }

        public override string ToString()
        {
            return "Окружность: " + base.ToString();
        }
    }
}
