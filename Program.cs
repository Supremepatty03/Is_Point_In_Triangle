using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1_c_;

namespace Lab1_c_
{
    internal class Program
    {
        static void Main()
        {
            AdditionalInfo.Greetings();
            FileReader Reader = new FileReader();
            while (true)
            {    
                AdditionalInfo.ChooseInputMethod();
                int UserInputChoice = Reader.ApplyingInputChoice();
                Point2D A = default, B = default, C = default, Point = default  ;

                if (UserInputChoice == (int)InputChoice.FROM_KEYBOARD)
                {
                    A = InputHandler.GetPoint("Введите координаты точки А");
                    B = InputHandler.GetPoint("Введите координаты точки В");
                    C = InputHandler.GetPoint("Введите координаты точки С");
                    Point = InputHandler.GetPoint("Введите координаты искомой точки");
                }
                else if (UserInputChoice == (int)InputChoice.FROM_FILE) 
                {
                    Console.WriteLine("Для отмены введите «RETURN»");
                    Console.Write(" Введите путь к файлу: ");
                    string filepath = Console.ReadLine();
                    Point2D[] points = Reader.ReadPointsFromFile(ref filepath);
                    if (points == default(Point2D[]) || points == null) { continue; }

                    A = points[0];
                    A.ShowInfo("A");
                    B = points[1];
                    B.ShowInfo("B");
                    C = points[2];
                    C.ShowInfo("C");
                    Point = points[3];
                    Point.ShowInfo("исследования");
                }

                Triangle triangle = new Triangle(A, B, C, new TriangleCheker());
                if (!triangle.CorrectObject()) { continue; }

                bool IsInside = triangle.Contains(Point);

                Console.WriteLine($"Точка принадлежит треугольнику:{(IsInside ? " Да " : " Нет ")}\n");

                FileWriter fileWriter = new FileWriter();
                fileWriter.SavingToFile(new string[] { A.x.ToString() + " " + A.y.ToString(),
                                                       B.x.ToString() + " " + B.y.ToString(),
                                                       C.x.ToString() + " " + C.y.ToString(),
                                                       Point.x.ToString() + " " + Point.y.ToString(), },        // массив данных + результат
                                                       $"{(IsInside ? "Точка принадлежит треугольнику" : "Точка не принадлежит треугольнику")}");
                
                if (!Functionality.Looping()) { break; }
            }
            AdditionalInfo.Farewell();
        }
    }

}
