using FiguresLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DrawLibrary
{
    internal class WFADraw : IDraw
    {
        public Graphics graphics;
        public WFADraw (ref Graphics graphics)
        {
            this.graphics = graphics;
        }
        public void Draw(Figure figure)
        {
            if (figure != null && graphics != null)
            {
                Brush brush = SolidBrush(figure.color);
                switch (figure.FigureType)
                {
                    case TypeFigure.Circle:
                        Circle circle = figure as Circle;
                        int r = (int)Math.Round(circle.getR());
                        int wdth = 2 * r;
                        int hght = wdth;
                        int x = (int)circle.points[0].x - r;
                        int y = (int)circle.points[0].y - r;
                        graphics.FillEllipse(brush, x, y, wdth, hght);
                        break;
                    case TypeFigure.Quadrant: 
                        Quadrant quadrant = figure as Quadrant;
                        int x1 = (int)quadrant.points[0].x;
                        int y1 = (int)quadrant.points[0].y;
                        int wdth1 = (int)quadrant.getA();
                        int hght1 = wdth1;
                        graphics.FillRectangle(brush, x1, y1, wdth1, hght1);
                        break;
                }
            }
        }
    }
}
