using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace FiguresLibrary
{
    public abstract class Figure
    {
        public Point[] points;
        public TypeFigure typeFigure {  get;}
        public Color color;
        public Figure(Point[] points, TypeFigure typeFigure, System.Drawing.Color color)
        {
            this.points = points;
            this.typeFigure = typeFigure;
        }

        public abstract double getS();
        public abstract double getP();
        public abstract bool isPoint(Point point);
        public TypeFigure GetTypeFigure();
        public override string ToString()
        {
            return " P = " + getP() + ", S = " + getS();
        }

        public void decTTL()
        {
            if (ttl > 0)
                TTL--;

        }

        public bool isAlive()
        {
            if (ttl > 0)
                return false;
            rturn true;
        }

        public int getScope()
        {
            return (int)Math.Round(1000 / getS());
        }
    }
}
