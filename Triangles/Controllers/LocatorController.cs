using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;

namespace TrianglesInReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocatorController : ControllerBase
    {

        string[] ROWS = { "A", "B", "C", "D", "E", "F" };

        private readonly ILogger<LocatorController> _logger;

        public LocatorController(ILogger<LocatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{vertex1}/{vertex2}/{vertex3}")]
        public string[] Get(string vertex1, string vertex2, string vertex3)
        {
            string[] coords = vertex1.Split(',');
            Point point1 = new Point(int.Parse(coords[0]), int.Parse(coords[1]));

            coords = vertex2.Split(',');
            Point point2 = new Point(int.Parse(coords[0]), int.Parse(coords[1]));


            coords = vertex3.Split(',');
            Point point3 = new Point(int.Parse(coords[0]), int.Parse(coords[1]));

            List<Point> points = new List<Point>();
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);

            double distanceA = GetDistance(point1.X, point1.Y, point2.X, point2.Y);

            double distanceB = GetDistance(point1.X, point1.Y, point3.X, point3.Y);

            double distanceC = GetDistance(point2.X, point2.Y, point3.X, point3.Y);

            if (!(distanceA + distanceB > distanceC &&
                distanceA + distanceC > distanceB &&
                distanceB + distanceC > distanceA))
            {

                return new string[] { "INVALID COORDINATES"};
            }
            string hypot = distanceA > distanceB ? "a" : "b";
            hypot = distanceB > distanceC ? "b" : "c";

            Point hypotPoint1 = point1;
            Point hypotPoint2 = point2;
            if (hypot == "b")
            {
                hypotPoint1 = point1;
                hypotPoint2 = point3;
            }
            if (hypot == "c")
            { 
                hypotPoint1 = point2;
                hypotPoint2 = point3;
            }

            if (hypotPoint2.Y > hypotPoint1.Y) {
                Point temp = hypotPoint1;
                hypotPoint1 = hypotPoint2;
                hypotPoint2 = temp;
            
            }

            if (hypotPoint2.X < hypotPoint1.X) {
                return new string[] { "INVALID COORDINATES" };
            }



            int minX = 61;
            int maxX = -1;
            int minY = 61;
            int maxY = 0;
            foreach (Point point in points) {
                if (point.X < minX) {
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

            if (maxY - minY != 10 || maxX - minX != 10) {
                return new string[] { "INVALID COORDINATES" };
            }

            int doubleVertexRightSide = 0;

            foreach (Point point in points)
            {
             
                if (point.X == maxX)
                {
                    doubleVertexRightSide++;
                }

            }
            String row = ROWS[minY/10];
            int col = -1;
            if (doubleVertexRightSide == 2)
            {
                col = maxX * 2 / 10;
            }
            else {
                col = (minX * 2 / 10) + 1 ;
            }

            return new string[] {row+col};
        }
        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
    }
  
}

