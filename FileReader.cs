using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_c_
{
    public interface IFileReader
    {
        Point2D[] ReadPointsFromFile(ref string filePath);
    }

    enum InputChoice
    {
        FROM_KEYBOARD = 1,
        FROM_FILE = 2
    }
    public class FileReader : IFileReader
    {
        public int ApplyingInputChoice()
        {
            int choice;
            while (true)
            {
                choice = InputHandler.GetInput<int>(" - ");
                switch (choice)
                {
                    case (int)InputChoice.FROM_KEYBOARD:
                        return choice;
                    case (int)InputChoice.FROM_FILE:
                        return choice;
                    default:
                        Console.WriteLine("Введите «1», «2»");
                        break;
                }
            }
        }

        public Point2D[] ReadPointsFromFile(ref string filePath)
        {
            if (filePath == "RETURN") { return default; }
            while (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!");
                Console.Write(" Введите путь к файлу: ");
                filePath = Console.ReadLine();
                if (filePath == "RETURN") { return default; }
            }
            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);

                if (lines.Length != 4)
                {
                    throw new InvalidDataException($"Ожидалось 4 строки (3 точки треугольника + 1 искомая), а получено {lines.Length}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return default;
            }
            Point2D[] points = new Point2D[4];

            for (int i = 0; i < lines.Length; i++)
            {
                try { points[i] = ParsePoint(lines[i], i + 1); }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    return default;
                }
            }

            return points;
        }

        private Point2D ParsePoint(string line, int lineNumber)
        {
            string[] parts = line.Split(new[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                Console.WriteLine($"Ошибка в строке {lineNumber}: Ожидались две координаты, разделённые пробелом или точкой с запятой.");
            }

            if (!double.TryParse(parts[0], out double x) || !double.TryParse(parts[1], out double y))
            {
                throw new FormatException($"Ошибка в строке {lineNumber}: Координаты должны быть числами.");
            }
            return new Point2D(x, y);
        }
    }
}
