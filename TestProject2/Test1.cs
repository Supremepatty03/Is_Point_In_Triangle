using Lab1_c_;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForTriangle
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void TestPointInsideTriangle()
        {
            Point2D A = new Point2D(0, 0);
            Point2D B = new Point2D(3, 0);
            Point2D C = new Point2D(0, 3);
            Point2D PointInside = new Point2D(1, 1);  // внутри 
            Point2D PointOutside = new Point2D(4, 4); // снаружи 
            Point2D PointOnLine = new Point2D(2, 0); 

            Triangle triangle = new Triangle(A, B, C, new TriangleCheker());

            bool ResultInside = triangle.Contains(PointInside);
            bool ResultOutside = triangle.Contains(PointOutside);
            bool ResultOnLine = triangle.Contains(PointOnLine);

            Assert.IsTrue(ResultInside);  // Точка должна быть внутри
            Assert.IsFalse(ResultOutside);  // Точка не должна быть внутри
            Assert.IsTrue(ResultOnLine);
        }
    }
}
