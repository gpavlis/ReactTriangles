using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using TrianglesInReact.Controllers;

namespace TrianglesTest1
{
    [TestClass]
    public class RendererControllerTests
    {

        [TestMethod]
        public void GettingAnEvenPositionTriangle()
        {
            RendererController renderer = new RendererController(null);
            string[] ROWS = { "A", "B", "C", "D", "E", "F" };
            foreach (string row in ROWS)
            {
                for (int i = 1; i < 13; i++)
                {
                    Point[] pixelsOfTriangle = renderer.Get(row, i);
                    Point[] vertices = FindVertices(pixelsOfTriangle);
                    LocatorController locatorController = new LocatorController(null);
                    string[] position = locatorController.Get(vertices[0].X + "," + vertices[0].Y, vertices[1].X + "," + vertices[1].Y, vertices[2].X + "," + vertices[2].Y);
                    Assert.AreEqual(row+i, position[0]);
                }
            }
        }

        private Point[] FindVertices(Point[] pixelsOfTriangle)
        {
            int maxX = GetMax_X(pixelsOfTriangle);
            int maxY = GetMax_Y(pixelsOfTriangle);
            int minX = GetMin_X(pixelsOfTriangle);
            int minY = GetMin_Y(pixelsOfTriangle);

            List<Point> points = new List<Point>();
            foreach (Point point in pixelsOfTriangle) {
                if ((point.X == maxX || point.X == minX) && (point.Y == maxY || point.Y == minY)) {
                    points.Add(point);
                }
            }
            return points.ToArray();
        }

        private int GetMax_X(Point[] points)
        {
            int maxX = Int32.MinValue;

            foreach (Point point in points)
            {
                if (point.X > maxX)
                {
                    maxX = point.X;
                }
            }
            return maxX;
        }

        private int GetMin_X(Point[] points)
        {
            int minX = Int32.MaxValue;
            foreach (Point point in points)
            {
                if (point.X < minX)
                {
                    minX = point.X;
                }
            }
            return minX;
        }

        private int GetMin_Y(Point[] points)
        {
            int minY = Int32.MaxValue;
            foreach (Point point in points)
            {
                if (point.Y < minY)
                {
                    minY = point.Y;
                }
            }
            return minY;
        }
        private int GetMax_Y(Point[] points)
        {
            int maxY = Int32.MinValue;
            foreach (Point point in points)
            {
                if (point.Y > maxY)
                {
                    maxY = point.Y;
                }
            }
            return maxY;
        }
    }
}
