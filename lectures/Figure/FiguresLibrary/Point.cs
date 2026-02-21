using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLibrary
{
    public struct Point
    {
        public double x;
        public double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double getLen(Point pnt)
        {
            double len = 0;
            len = Math.Sqrt(Math.Pow(pnt.x - x, 2) + Math.Pow(pnt.y - y, 2));
            return len;
        }
    }
}
