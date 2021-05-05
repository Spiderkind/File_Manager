using System;
using System.IO;
using System.Linq;

namespace FileOp
{
    public class File_tree
    {
        static string[,] Aarray;
        static int cfi = 0;
        static int cfo = 0;
        static int d = 1;
        public static void ShFile(string path, int page)
        {
            try
            {
                CountFolders(path, 0);
                CountFiles(path, 0);
                Aarray = new string[2, cfo + cfi];
                GFolders(path, 0);
                GFiles(path, 0);
                ShowMeFiles(path, page);
                SaveMyPath(path);
                cfi = 0;
                cfo = 0;
                d = 1;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Путь не существует");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Ошибка пути");
            }
            catch
            {
                Console.WriteLine("Неожиданная ошибка...");
            }
        }
        //Подсчет количества папок и файлов
        public static void CountFolders(string path, int level)
        {
            string[] allfolders = Directory.GetDirectories(path);
            foreach (string folder in allfolders)
            {
                cfo++;
                if (level <= 2)
                {
                    CountFolders(folder, level + 1);
                    CountFiles(folder, level + 1);
                }
            }
        }
         //Подсчет количества файлов
        public static void CountFiles(string path, int level)
        {
            string[] allfiles = Directory.GetFiles(path);
            foreach (string file in allfiles)
            {
                cfi++;
            }
        }
        //Получение папок и файлов
        public static void GFolders(string path, int level)
        {
            string[] allfolders = Directory.GetDirectories(path);
            foreach (string folder in allfolders)
            {
                Aarray[0, d - 1] = DeleteUsless(folder, path);
                Aarray[1, d - 1] = level.ToString();
                d++;
                if (level <= 2)
                {
                    GFolders(folder, level + 1);
                    GFiles(folder, level + 1);
                }
            }
        }
        //Получение файлов
        public static void GFiles(string path, int level)
        {
            string[] allfiles = Directory.GetFiles(path);
            foreach (string file in allfiles)
            {
                Aarray[0, d - 1] = DeleteUsless(file, path);
                Aarray[1, d - 1] = level.ToString();
                d++;
            }
        }
        //Удаление части пути для корректного отображения
        public static string DeleteUsless(string name, string path)
        {
            string new_name = name.Remove(0, path.Length + 1);
            return new_name;
        }
        //Подготовка к выводу файлов
        public static void ShowMeFiles(string path, int page)
        {
            int n = new int();
            if (!File.Exists("configuration.txt"))
            {
                string[] createText = { "70" };
                File.WriteAllLines("configuration.txt", createText);
            }
            try
            {
                n = Convert.ToInt32(File.ReadLines("configuration.txt").First());
            }
            catch
            {
                Console.WriteLine("Ошибка файла конфигурации");
                Environment.Exit(0);
            }

            ShowMeFilesRight(path, page, n);
        }
        //Вывод файлов
        public static void ShowMeFilesRight(string path, int page, int n)
        {
            Console.WriteLine(path);
            for (int i = n * (page - 1); i < n * page; i++)
            {
                if (i < cfi + cfo)
                {
                    switch (Aarray[1, i])
                    {
                        case "0":
                            for (int j = 0; j < path.Length - 1; j++)
                            {
                                Console.Write(" ");
                            }
                            Console.WriteLine("|--" + Aarray[0, i]);
                            break;
                        case "1":
                            for (int j = 0; j < path.Length - 1; j++)
                            {
                                Console.Write(" ");
                            }
                            Console.WriteLine("|  |--" + Aarray[0, i]);
                            break;
                        case "2":
                            for (int j = 0; j < path.Length - 1; j++)
                            {
                                Console.Write(" ");
                            }
                            Console.WriteLine("|  |  |--" + Aarray[0, i]);
                            break;
                    }
                }
            }
        }
        //Сохраняет последний путь
        public static void SaveMyPath(string path)
        {
            if (!File.Exists("path.txt"))
            {
                string[] createText = { path };
                File.WriteAllLines("path.txt", createText);
            }
        }
    }
}
