using System;
using System.Collections.Generic;
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
}
