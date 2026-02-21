using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLibrary
{
    public class Quadrant : Figure
    {
        public Quadrant(Point[] points,int ttl, System.Drawing.Color color) : base(points,ttl,TypeFigure.Quadrant)
        {
        }

        public double getA()
        {
            double diag = points[0].getLen(points[1]);

            return diag / Math.Sqrt(2);
        }


        public override double getP()
        {
            return 4 * getA();
        }

        public override double getS()
        {
            return getA() * getA();
        }

        public override bool isPoint(Point point)
        {
            //Point pnt2 = new Point(points[1].x, points[0].y);
            //Point pnt4 = new Point(points[0].x, points[1].y);
            bool isPnt = false;
            if (point.x >= points[0].x 
                && point.x <= points[1].x
                && point.y >= points[1].y
                && point.y <= points[0].y)
            {
                isPnt = true;
            }
            return isPnt;
        }

        public override string ToString()
        {
            return "Квадрат: P = " + getP() + ", S = "  + getS();
        }
    }
}
