using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using TrianglesInReact.Controllers;

namespace TrianglesTest1
{
    [TestClass]
    public class LocatorControllerTests
    {

        [TestMethod]
        public void GettingAnEvenPositionTriangle()
        {
            LocatorController locator = new LocatorController(null);
            string[] locationOfTriangle = locator.Get("0,0", "10,0", "10,10");
            Assert.AreEqual("A2", locationOfTriangle[0]);
        }

        [TestMethod]
        public void GettingAnOddPositionTriangle()
        {
            LocatorController locator = new LocatorController(null);
            string[] locationOfTriangle = locator.Get("0,0", "0,10", "10,10");
            Assert.AreEqual("A1", locationOfTriangle[0]);
        }
        [TestMethod]
        public void GettingAnInvalidTriangle()
        {
            LocatorController locator = new LocatorController(null);
            string[] locationOfTriangle = locator.Get("0,0", "10,1", "10,10");
            Assert.AreEqual("INVALID COORDINATES", locationOfTriangle[0]);
        }
    }
}
