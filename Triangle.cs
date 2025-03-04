using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_c_
{
    public struct Point2D
    {
        public double x { get; }
        public double y { get; }

        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double Distance(Point2D other)
        {
            return Math.Sqrt((x - other.x) * (x - other.x) + (y - other.y) * (y - other.y));
        }
        public void ShowInfo(string PointName)
        {
            Console.WriteLine($"Координаты точки {PointName}:\n x = {x}\n y = {y}");
        }
    }
    public interface GeometryObject
    {
        bool Contains(Point2D point);
        bool CorrectObject();
    }
    public class Triangle : GeometryObject
    {
        public Point2D A { get; }
        public Point2D B { get; }
        public Point2D C { get; }

        private readonly IChecker checker;

        public Triangle(Point2D a, Point2D b, Point2D c, IChecker checker)
        {
            A = a;
            B = b;
            C = c;
            this.checker = checker;

        }
        public bool Contains(Point2D point)
        {
            return checker.Check(point, this);
        }
        public bool CorrectObject()
        {
            try
            {
                checker.PointsCollide(this);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при проверке треугольника: {ex.Message} \n");
                return false;
            }
        }
    }
}
