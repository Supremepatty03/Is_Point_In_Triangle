using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_c_
{
    public interface IChecker
    {
        bool Check(Point2D other, GeometryObject figure);
        void PointsCollide(GeometryObject figure);
    }


    public class TriangleCheker : IChecker
    {
        public bool Check(Point2D point, GeometryObject figure)
        {
            if (figure is Triangle triangle)
            { 
                double Area(Point2D p1, Point2D p2, Point2D p3)
                {
                    double a = p1.Distance(p2);
                    double b = p2.Distance(p3);
                    double c = p1.Distance(p3);
                    double perimetr = (a + b + c) / 2;

                    return Math.Sqrt(perimetr * (perimetr - a) * (perimetr - b) * (perimetr - c));
                }
                double AreaNoP = Area(triangle.A, triangle.B, triangle.C);
                //Console.Write("defualt area:");
                //Console.WriteLine(AreaNoP);

                double AreaP1 = Area(point, triangle.B, triangle.C);
                double AreaP2 = Area(triangle.A, point, triangle.C);
                double AreaP3 = Area(triangle.A, triangle.B, point);
                //Console.Write("Dot area:");
                //Console.WriteLine(AreaP1 + AreaP2 + AreaP3);
                return Math.Abs(AreaNoP - (AreaP1 + AreaP2 + AreaP3)) < 1e-9;  //1 * 10^-9
            }
            else { throw new ArgumentException ("Объект не поддерживается для проверки."); }
        }
        public void PointsCollide(GeometryObject figure)
        {
            if (figure is Triangle triangle)
            {
                    if (triangle.A.Equals(triangle.B) || triangle.A.Equals(triangle.C) || triangle.B.Equals(triangle.C))
                {
                    throw new ArgumentException("Точки треугольника не должны совпадать");
                }
                if ((triangle.C.x - triangle.A.x) / (triangle.B.x - triangle.A.x) == (triangle.C.y - triangle.A.y) / (triangle.B.y - triangle.A.y))
                {
                    throw new ArgumentException("Точки треугольника не должны лежать на одной прямой");
                }
                return;
            }
            else { throw new ArgumentException("Объект не поддерживается для проверки."); }
        }
    }
}
