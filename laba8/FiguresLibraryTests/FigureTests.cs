using Microsoft.VisualStudio.TestTools.UnitTesting;
using FiguresLibrary;
using System;

namespace FiguresTests
{
    [TestClass]
    public class FiguresTests
    {

        [TestMethod]
        public void Circle_GetArea_ShouldReturnCorrectArea()
        {
            Point center = new Point(0, 0);
            Point edge = new Point(0, 5);
            Circle circle = new Circle("окружность", "Blue", center, edge);
            double area = circle.GetArea();
            Assert.AreEqual(78.539, area, 0.001);
        }

        [TestMethod]
        public void Circle_GetLength_ShouldReturnCorrectCircumference()
        {
            Point center = new Point(0, 0);
            Point edge = new Point(0, 10);
            Circle circle = new Circle("окружность", "Green", center, edge);
            double length = circle.GetLength();
            Assert.AreEqual(62.831, length, 0.001);
        }

        [TestMethod]
        public void RegularPolygons_CreateTriangle_ShouldCalculateArea()
        {
            RegularPolygons triangle = new RegularPolygons("многоугольник", "Red", 3, 3, 3);
            Assert.AreEqual(3.89, triangle.GetArea(), 0.01);
        }

        [TestMethod]
        public void RegularPolygons_CreateSquare_ShouldCalculateArea()
        {
            RegularPolygons square = new RegularPolygons("многоугольник", "Blue", 5, 5, 5, 5);
            Assert.AreEqual(25, square.GetArea(), 0.01);
        }

        [TestMethod]
        public void ManagerFigures_LoadValidFile_ShouldCreateFigures()
        {
            string testFilePath = "test_figures.txt";
            System.IO.File.WriteAllText(testFilePath, "окружность 0 0 0 5 Red\nмногоугольник 3 3 3 Blue");

            try
            {
                ManagerFigures manager = new ManagerFigures(testFilePath);
                Assert.IsTrue(manager.figures.Count > 0);
            }
            finally
            {
                if (System.IO.File.Exists(testFilePath))
                    System.IO.File.Delete(testFilePath);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ManagerFigures_LoadNonExistentFile_ShouldThrowException()
        {
            ManagerFigures manager = new ManagerFigures("nonexistent.txt");
        }

        [TestMethod]
        public void RegularPolygons_CompareTo_ShouldSortByArea()
        {
            RegularPolygons small = new RegularPolygons("многоугольник", "Red", 3, 3, 3);
            RegularPolygons large = new RegularPolygons("многоугольник", "Blue", 5, 5, 5, 5);
            int result = small.CompareTo(large);
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void Circle_CompareTo_ShouldSortByArea()
        {
            Circle small = new Circle("окружность", "Red", new Point(0, 0), new Point(0, 5));
            Circle large = new Circle("окружность", "Blue", new Point(0, 0), new Point(0, 10));
            int result = small.CompareTo(large);
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void Circle_IsInFirstQuarter_ShouldReturnCorrectValue()
        {
            Circle circle1 = new Circle("окружность", "Green", new Point(10, 10), new Point(15, 15));
            Circle circle2 = new Circle("окружность", "Green", new Point(0, 0), new Point(5, 5));
            Assert.IsTrue(circle1.IsInFirstQuarter());
            Assert.IsFalse(circle2.IsInFirstQuarter());
        }

        [TestMethod]
        public void RegularPolygons_GetTypeFigure_ShouldReturnCorrectType()
        {
            RegularPolygons triangle = new RegularPolygons("многоугольник", "Red", 3, 3, 3);
            RegularPolygons square = new RegularPolygons("многоугольник", "Blue", 4, 4, 4, 4);
            RegularPolygons pentagon = new RegularPolygons("многоугольник", "Green", 5, 5, 5, 5, 5);

            Assert.IsTrue(triangle.GetInfo().Contains("треугольник"));
            Assert.IsTrue(square.GetInfo().Contains("квадрат"));
            Assert.IsTrue(pentagon.GetInfo().Contains("правильный многоугольник"));
        }
    }
}