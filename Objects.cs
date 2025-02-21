using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1_c_
{
    public static class AdditionalInfo
    {
        public static void Greetings()
            {
            Console.WriteLine("Практическая работа №1");
            Console.WriteLine("Вариант №6. Для заданной точки и треугольника на плоскости определить, принадлежит ли точка треугольнику.");
            Console.WriteLine("Выполнил Тарасов Артем, 433 \n\n");
            }
        public static void ChooseInputMethod()
        {
            Console.WriteLine("Как вы хотите ввести данные?");
            Console.WriteLine(" «1» - Ручной ввод ");
            Console.WriteLine(" «2» - Ввод данных из файла");
        }
        public static void SaveToFile()
        {
            Console.WriteLine("Вы хотите сохранить данные в файл? ");
            Console.WriteLine(" «1» - Сохранить начальные данные в файл ");
            Console.WriteLine(" «2» - Сохранить результат в файл ");
            Console.WriteLine(" «3» - Не сохранять ");
        }

        public static void LoopMenu()
        {
            Console.WriteLine("Вы хотите продолжить взаимодейситвие с програмой?");
            Console.WriteLine(" «1» - Ввести данные еще раз ");
            Console.WriteLine(" «2» - Выключить програму ");
        }
        public static void FileMenu(string filePath)
        {
            Console.WriteLine($"Файл {filePath} уже содержит данные. Что вы хотите сделать?");
            Console.WriteLine(" «1»  - Перезаписать файл");
            Console.WriteLine(" «2» - Добавить данные в конец файла");
            Console.WriteLine(" «3» - Отмена");
        }
        public static void Farewell()
        {
            Console.WriteLine(" Завершение работы");
            Thread.Sleep(400);
            Console.WriteLine(" Завершение работы.");
            Thread.Sleep(400);
            Console.WriteLine(" Завершение работы..");
            Thread.Sleep(400);
            Console.WriteLine(" Завершение работы...");
        }
    }
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
    public static class InputHandler
    {
        public static Point2D GetPoint(string Text4User)
        {
            double x, y;
            Console.WriteLine(Text4User);

            x = GetInput<double>(" Введите х: ");
            y = GetInput<double>(" Введите y: ");

            return new Point2D(x, y);
        }
        public static T GetInput<T>(string Text4User) where T : struct
        {
            while (true)
            {
                Console.Write(Text4User);
                string input = Console.ReadLine();
                if (TryParse<T>(input, out T res))
                { return res; }
                Console.WriteLine("Ошибка ввода! Пробуйте снова");
            }

        }
        public static bool TryParse<T>(string input, out T result) where T : struct
        {

            var type = typeof(T);
            var ParsingMethod = type.GetMethod("TryParse", new[] { typeof(string), type.MakeByRefType() }); // создается массив типа Type, внутри которого параметры искомой функции
            if (ParsingMethod != null)
            {
                object[] parameters = { input, null }; // в null будет записан ответ (заранее вид результата не известен)
                bool outcome = (bool)ParsingMethod.Invoke(null, parameters); //null - объект, на котором вызывается метод
                result = outcome ? (T)parameters[1] : default; //явно приводим результат к типу Т в случае успеха
                return outcome;
            }
            result = default;
            return false;
        }
    }
        public interface IChecker
        {
            bool Check(Point2D other, Triangle triangle);
        }
        public class Triangle
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

        }
        public class TriangleCheker : IChecker
        {
            public bool Check(Point2D point, Triangle triangle)
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

                double AreaP1 = Area(point, triangle.B, triangle.C);
                double AreaP2 = Area(triangle.A, point, triangle.C);
                double AreaP3 = Area(triangle.A, triangle.B, point);

                return Math.Abs(AreaNoP - (AreaP1 + AreaP2 + AreaP3)) < 1e-9;  //1 * 10^-9

            }
        }
        public static class Functionality
        {
            enum literals
            {
                CONTINUE = 1,
                EXIT = 2
            };
            public static bool Looping()
            {
                int response;
                while (true)
                {
                    AdditionalInfo.LoopMenu();
                    response = InputHandler.GetInput<int>(" - ");
                    if (response == (int)literals.CONTINUE) { return true; }
                    else if (response == (int)literals.EXIT) { return false; }
                    Console.WriteLine(" Ошибка ввода! ");
                }

            }
        }

}



