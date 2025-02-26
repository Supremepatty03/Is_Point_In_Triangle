using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1_c_
{
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
}



