﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization.Internal;
using Microsoft.Extensions.Logging;

namespace TrianglesInReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RendererController : ControllerBase
    {

        private readonly string[] ROWS = { "A", "B", "C", "D", "E", "F" };
        const int HEIGHT_OF_TRIANGLE = 10;
        private readonly ILogger<RendererController> _logger;

        public RendererController(ILogger<RendererController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{row}/{column}")]
        public Point[] Get(string Row, int Column)
        {
            List<Point> points = new List<Point>();
            int y_start = CalculateStartingHeight(Row);

            if (IsAnEvenPositionTriangle(Column))
            {
                for (int y = 0; y <= 10; y++)
                {
                    for (int x = 10 - y; x >= 0; x--)
                    {
                        points.Add(new Point(((Column) / 2 * 10) - x, y_start + y));
                    }
                }
            }
            else
            {
                for (int y = 0; y <= 10; y++)
                {
                    for (int x = 0; x <= y; x++)
                    {
                        points.Add(new Point(((Column - 1) / 2 * 10) + x, y_start + y));
                    }
                }
            }

            return points.ToArray();
        }

        private static bool IsAnEvenPositionTriangle(int Column)
        {
            return Column % 2 == 0;
        }

        private int CalculateStartingHeight(string Row)
        {
            int y_start = 0;

            for (int i = 0; i < ROWS.Length; i++)
            {
                if (ROWS[i] == Row)
                {
                    y_start = i * HEIGHT_OF_TRIANGLE;
                }
            }

            return y_start;
        }
    }
}
