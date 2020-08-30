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
using Triangles.Validators;

namespace TrianglesInReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocatorController : ControllerBase
    {

        private string[]  ROWS = { "A", "B", "C", "D", "E", "F" };
        private readonly ILogger<LocatorController> _logger;

        public LocatorController(ILogger<LocatorController> logger)
        {
            _logger = logger;

        }

        [HttpGet("{vertex1}/{vertex2}/{vertex3}")]
        public string[] Get(string vertex1, string vertex2, string vertex3)
        {
            List<Point> points = new List<Point>();
            Point point1 = ParsePoint(vertex1);
            Point point2 = ParsePoint(vertex2);
            Point point3 = ParsePoint(vertex3);
            
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);

            TriangleValidator validator = new TriangleValidator(point1, point2, point3);
            if (!validator.IsValid()) {
                return new string[] { "INVALID COORDINATES" };
            }

            string row = ROWS[GetMin_Y(points)/10];

            int col = -1;
            if (IsTheTriangleInEvenNumberPosition(points,GetMax_X(points)))
            {
                col = GetMax_X(points) * 2 / 10;
            }
            else {
                col = (GetMin_X(points) * 2 / 10) + 1 ;
            }
            return new string[] {row+col};
        }
        private Point ParsePoint(string vertex)
        {

            string[] coords = vertex.Split(',');
            return new Point(int.Parse(coords[0]), int.Parse(coords[1]));
        }

        private int GetMax_X(List<Point> points)
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

        private int GetMin_X(List<Point> points)
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

        private int GetMin_Y(List<Point> points)
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

        private bool IsTheTriangleInEvenNumberPosition(List<Point> points,int maxX) {
            int doubleVertexRightSide = 0;
            foreach (Point point in points)
            {
                if (point.X == maxX)
                {
                    doubleVertexRightSide++;
                }
            }
            return doubleVertexRightSide == 2;
        }
    }
}



