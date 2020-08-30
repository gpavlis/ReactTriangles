using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using Triangles.Validators;

namespace TrianglesTest
{
    [TestClass]
    public class TriangleValidatorTests
    {
        [TestMethod]
        public void TriangleVerticesAre10PixelsApartPerAxis()
        {
            TriangleValidator triangleValidator = new TriangleValidator(new Point(0, 0), new Point(0, 10), new Point(10, 10));
            Assert.IsTrue(triangleValidator.IsValid(), "Triangle vertices are not located on 10x10 grid");
        }

        [TestMethod]
        public void TriangleVerticesAreNotValidWhenTheyAreNot10PixelsApartPerAxis()
        {
            TriangleValidator triangleValidator = new TriangleValidator(new Point(1, 0), new Point(0, 0), new Point(10, 0));
            Assert.IsFalse(triangleValidator.IsValid(), "Triangle vertices are not located on 10x10 grid");
        }

        [TestMethod]
        public void TriangleRightSidesAreOfLength10()
        {
            TriangleValidator triangleValidator = new TriangleValidator(new Point(20, 20), new Point(20, 30), new Point(30, 30));
            Assert.IsTrue(triangleValidator.IsValid(), "Triangle vertices are not located on 10x10 grid");
        }

        [DataTestMethod]
        [DataRow(0, 0, 10, 10, 10, 1)]
        [DataRow(0, 0, 0, 0, 0, 0)]
        [DataRow(0, 0, 10, 10, 10, 1)]

        public void TriangleValidationDoesNotAllowRightSidesAreNotOfLength10(int point1_X, int point1_y, int point2_X, int point2_y, int point3_X, int point3_y)
        {
            TriangleValidator triangleValidator = new TriangleValidator(new Point(point1_X, point1_y), new Point(point2_X, point2_y), new Point(point3_X, point3_y));
            Assert.IsFalse(triangleValidator.IsValid(), "Triangle vertices do not have sides length 10");
        }

        [DataTestMethod]
        [DataRow(0, 0, 10, 0, 10, 0)]

        public void TriangleValidationDoesNotAllowHypotenusTopRightToBottomLeft(int point1_X, int point1_y, int point2_X, int point2_y, int point3_X, int point3_y)
        {
            TriangleValidator triangleValidator = new TriangleValidator(new Point(point1_X, point1_y), new Point(point2_X, point2_y), new Point(point3_X, point3_y));
            Assert.IsFalse(triangleValidator.IsValid(), "Invalid Triangle coordinates right angle is only allowed bottom left or top right");
        }

        [DataTestMethod]
        [DataRow(0, 0, 10, 0, 10, 10)]

        public void TriangleValidationDoesAllowHypotenusTopRightToBottomLeft(int point1_X, int point1_y, int point2_X, int point2_y, int point3_X, int point3_y)
        {
            TriangleValidator triangleValidator = new TriangleValidator(new Point(point1_X, point1_y), new Point(point2_X, point2_y), new Point(point3_X, point3_y));
            Assert.IsTrue(triangleValidator.IsValid(), "Invalid Triangle coordinates right angle is only allowed bottom left or top right");
        }


        [DataTestMethod]
        [DataRow(0, 0, -10, 0, -10, -10)]

        public void TriangleValidationDoesAllowTrianglesOutOfTheAllowedGridTopLeft(int point1_X, int point1_y, int point2_X, int point2_y, int point3_X, int point3_y)
        {
            TriangleValidator triangleValidator = new TriangleValidator(new Point(point1_X, point1_y), new Point(point2_X, point2_y), new Point(point3_X, point3_y));
            Assert.IsFalse(triangleValidator.IsValid(), "Triangle is out of bounds");
        }


        [DataTestMethod]
        [DataRow(60, 60, 70, 0, 70, 70)]

        public void TriangleValidationDoesAllowTrianglesOutOfTheAllowedGridBottomRight(int point1_X, int point1_y, int point2_X, int point2_y, int point3_X, int point3_y)
        {
            TriangleValidator triangleValidator = new TriangleValidator(new Point(point1_X, point1_y), new Point(point2_X, point2_y), new Point(point3_X, point3_y));
            Assert.IsFalse(triangleValidator.IsValid(), "Triangle is out of bounds");
        }

        [DataTestMethod]
        [DataRow(0, 0, 10, 0, 10, 10)]//A2
        [DataRow(0, 0, 0, 10, 10, 10)]//A1
        [DataRow(50, 50, 60, 50, 60, 60)]//f12
        [DataRow(50, 50, 50, 60, 60, 60)]//F11
        public void TriangleValidationDoesAllowTrianglesOnTheInsideEdgeOfTheGrid(int point1_X, int point1_y, int point2_X, int point2_y, int point3_X, int point3_y)
        {
            TriangleValidator triangleValidator = new TriangleValidator(new Point(point1_X, point1_y), new Point(point2_X, point2_y), new Point(point3_X, point3_y));
            Assert.IsTrue(triangleValidator.IsValid(), "Triangle is out of bounds");
        }
    }
}
