using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Triangles.Validators
{
    public class TriangleValidator
    {
        private Point _point1 { get; }
        private Point _point2 { get; }
        private Point _point3 { get; }
        public TriangleValidator(Point point1, Point point2, Point point3)
        {
            _point1 = point1;
            _point2 = point2;
            _point3 = point3;
        }

        public bool IsValid()
        {
            return IsValidTriangle()
                && IsTrianglesRotationValid()
                && AreVerticesOn10by10Grid()
                && AreTrianglesRightSidesLength10()
                && IsTriangleWithinTheValidGrid();
        }
        private List<Point> GetPoints()
        {
            List<Point> points = new List<Point>();
            points.Add(_point1);
            points.Add(_point2);
            points.Add(_point3);
            return points;
        }

        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        private bool IsValidTriangle()
        {

            double distanceA = GetDistance(_point1.X, _point1.Y, _point2.X, _point2.Y);
            double distanceB = GetDistance(_point1.X, _point1.Y, _point3.X, _point3.Y);
            double distanceC = GetDistance(_point2.X, _point2.Y, _point3.X, _point3.Y);

            return ((distanceA + distanceB > distanceC &&
                distanceA + distanceC > distanceB &&
                distanceB + distanceC > distanceA));
        }

        private bool AreVerticesOn10by10Grid()
        {
            List<Point> points = GetPoints();
            bool isValid = true;

            foreach (Point point in points)
            {
                if (point.X % 10 != 0)
                {
                    isValid = false;
                }
                if (point.Y % 10 != 0)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        private bool AreTrianglesRightSidesLength10()
        {
            List<Point> points = GetPoints();
            int maxX = Int32.MinValue;
            int maxY = Int32.MinValue;

            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;

            foreach (Point point in points)
            {
                if (point.X < minX)
                {
                    minX = point.X;
                }

                if (point.X > maxX)
                {
                    maxX = point.X;
                }
                if (point.Y < minY)
                {
                    minY = point.Y;
                }

                if (point.Y > maxY)
                {
                    maxY = point.Y;
                }
            }
            return (maxY - minY == 10 && maxX - minX == 10);
        }

        private bool IsTriangleWithinTheValidGrid()
        {
            List<Point> points = GetPoints();
            bool isValid = true;
            foreach (Point point in points)
            {
                if (point.X < 0 || point.X > 60)
                {
                    isValid = false;
                }
                if (point.Y < 0 || point.Y > 60)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        private bool IsTrianglesRotationValid()
        {
            double distanceA = GetDistance(_point1.X, _point1.Y, _point2.X, _point2.Y);
            double distanceB = GetDistance(_point1.X, _point1.Y, _point3.X, _point3.Y);
            double distanceC = GetDistance(_point2.X, _point2.Y, _point3.X, _point3.Y);

            string hypot = distanceA > distanceB ? "a" : "b";
            hypot = distanceB > distanceC ? "b" : "c";

            Point hypotPoint1 = _point1;
            Point hypotPoint2 = _point2;
            if (hypot == "b")
            {
                hypotPoint1 = _point1;
                hypotPoint2 = _point3;
            }
            if (hypot == "c")
            {
                hypotPoint1 = _point2;
                hypotPoint2 = _point3;
            }

            if (hypotPoint2.Y > hypotPoint1.Y)
            {
                Point temp = hypotPoint1;
                hypotPoint1 = hypotPoint2;
                hypotPoint2 = temp;
            }

            return hypotPoint2.X < hypotPoint1.X;

        }
    }
}
