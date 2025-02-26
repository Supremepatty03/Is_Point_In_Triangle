using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_c_
{
    public interface IFileWriter
    {
        void WriteDataToFile(ref string filePath, string[] data);
        void WriteResultToFile(ref string filePath, string result);
        int WhatToDoWithData(string filePath);
        bool ContainsInvalidPathChars(string filePath);

    }
    public class FileWriter : IFileWriter
    {
        enum FileChoise
        {
            REWRITE = 1,
            ADD = 2,
            CANCEL = 3
        }
        public bool ContainsInvalidPathChars(string filePath)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

            string fileName = Path.GetFileName(filePath); // только имя файла без пути

            return filePath.Any(ch => invalidPathChars.Contains(ch)) || fileName.Any(ch => invalidFileNameChars.Contains(ch));
        }
        private void EnsureFileNDirectoryExists(ref string filePath)
        {
            while (ContainsInvalidPathChars(filePath))
            {
                Console.WriteLine("Ошибка: путь к файлу содержит недопустимые символы.");
                Console.Write("Введите корректный путь к файлу: ");
                filePath = Console.ReadLine();
            }
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close(); // Создает файл и сразу закрывает поток, чтобы избежать блокировки
            }
        }
        public int WhatToDoWithData(string filePath)
        {

            AdditionalInfo.FileMenu(filePath);

            int choice = InputHandler.GetInput<int>("Введите ваш выбор: ");

            return choice;
        }

        public void ApplyingChoice(string filePath, int choice, string[] data)
        {
            if (choice == (int)FileChoise.REWRITE)
            {
                File.WriteAllLines(filePath, data);
            }
            else if (choice == (int)FileChoise.ADD)
            {
                File.AppendAllLines(filePath, data);
            }
            else if (choice == (int)FileChoise.CANCEL)
            {
                return;
            }

        }
        enum SaveOptions
        {
            ONLY_INPUT = 1,
            RESULT = 2,
            NO_SAVE = 3
        }
        public void SavingToFile(string[] data, string result)
        {
            AdditionalInfo.SaveToFile();
            bool flag = true;
            while (flag) { 
                int choice = InputHandler.GetInput<int>(" - ");
                string filepath;
                
                switch (choice)
                {
                    case (int)SaveOptions.ONLY_INPUT:
                        Console.Write(" Введите путь сохранения файла: ");
                        filepath = Console.ReadLine();
                        //ApplyingChoice(filepath, choice, data);
                        WriteDataToFile(ref filepath, data);
                        flag = false;
                        
                        break;
                    case (int)SaveOptions.RESULT:
                        Console.Write(" Введите путь сохранения файла: ");
                        filepath = Console.ReadLine();
                        //ApplyingChoice(filepath, choice, data);
                        WriteResultToFile(ref filepath, result);
                        flag = false;
                        break;
                    case (int)SaveOptions.NO_SAVE:
                        Console.WriteLine("Данные не сохранены.");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }
        public void WriteDataToFile(ref string filePath, string[] data)
        {
            bool flag = true;
            while (flag)
            {
                try { 
                    EnsureFileNDirectoryExists(ref filePath);
                    flag = false;
                }  
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.Write(" Введите путь еще раз: ");
                    filePath = Console.ReadLine();
                }
                
            }
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                int choice = WhatToDoWithData(filePath);
                ApplyingChoice(filePath, choice, data);
                return;
            }
            File.WriteAllLines(filePath, data);
            Console.WriteLine(" Данные успешно сохранены");
        }

        public void WriteResultToFile(ref string filePath, string result)
        {
            string[] converted = new string[] { result };
            bool flag = true;
            while (flag)
            {
                try
                {
                    EnsureFileNDirectoryExists(ref filePath);
                    flag = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.Write(" Введите путь еще раз: ");
                    filePath = Console.ReadLine();
                }

            }
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                int choice = WhatToDoWithData(filePath);
                ApplyingChoice(filePath, choice, converted);
            }
            File.WriteAllText(filePath, result);
            Console.WriteLine(" Данные успешно сохранены");
        }
    }
}
