using FileOp;
using System;
using System.IO;

namespace Final_Level
{
    class File_Manager
    {
        static void Main(string[] args)
        {
            //Отображает последний путь, если такой был
            if (File.Exists("path.txt"))
            {
                string[] path = File.ReadAllLines("path.txt");
                File_tree.ShFile(path[0], 1);
            }
            Console.Write("Введите 'help', для отображения списка команд: ");
            do
            {
                string input = Console.ReadLine();
                Analyse(input);

            } while (true);
        }
        static void Analyse(string n)
        {
            string[] cp = n.Split('/');
            //Основные команды
            switch (cp[0])
            {
                case "help":
                    if (cp.Length > 1)
                    {
                        Console.WriteLine("Ошибка аргументов (не более 1)");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Список доступных команд:\n" +
                            "|--------------------------------------------------|---------------------------------|\n" +
                            "1. help                                            |- Выводит спсок дотсупных команд,|\n" +
                            "2. dispc/<путь к каталогу>/<номер листа>           |- Отобразить каталог,            |\n" +
                            "3. inff/<путь к файлу>                             |- Выводит информацию о файле,    |\n" +
                            "4. cpf/<путь к файлу>/<путь куда копировать>       |- Копирование файла,             |\n" +
                            "5. cpc/<путь к каталогу>/<путь куда копировать>    |- Копирование каталога,          |\n" +
                            "6. delf/<путь к файлу>                             |- Удаление файла,                |\n" +
                            "7. delc/<путь к каталогу>                          |- Удаление каталога,             |\n" +
                            "8. exit                                            |- Выход из программы.            |\n" +
                            "|--------------------------------------------------|---------------------------------|");
                    }
                    break;
                case "dispc":
                    if (cp.Length > 3)
                    {
                        Console.WriteLine("Ошибка аргументов (не более 2)");
                    }
                    else
                    {
                        Console.Clear();
                        try
                        {
                            File_tree.ShFile(cp[1], Convert.ToInt32(cp[2]));
                        }
                        catch
                        {
                            Console.WriteLine("Ошибка аргументов команды");
                        }
                    }
                    Console.Write("Введите 'help', для отображения списка команд: ");
                    break;
                case "inff":
                    if (cp.Length > 2)
                    {
                        Console.WriteLine("Ошибка аргументов (не более 1)");
                    }
                    else
                    {
                        ICDO.SMIFC(cp[1]);
                    }
                    Console.Write("Введите 'help', для отображения списка команд: ");
                    break;
                case "cpf":
                    if (cp.Length > 3)
                    {
                        Console.WriteLine("Ошибка аргументов (не более 2)");
                    }
                    else
                    {
                        try
                        {
                            ICDO.Copy_file(cp[1], cp[2]);
                        }
                        catch
                        {
                            Console.WriteLine("Ошибка пути");
                        }
                    }
                    Console.Write("Введите 'help', для отображения списка команд: ");
                    break;
                case "cpc":
                    if (cp.Length > 3)
                    {
                        Console.WriteLine("Ошибка аргументов (не более 2)");
                    }
                    else
                    {
                        try
                        {
                            DirectoryInfo opath = new DirectoryInfo(cp[1]);
                            DirectoryInfo npath = new DirectoryInfo(cp[2]);
                            int error = ICDO.Copy_catalog(opath, npath.Parent, 0);
                            if (error == 0)
                            {
                                Console.WriteLine("Копирование успешно завершено!");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ошибка пути");
                        }
                    }
                    Console.Write("Введите 'help', для отображения списка команд: ");
                    break;
                case "delf":         
                    if (cp.Length > 2)
                    {
                        Console.WriteLine("Ошибка аргументов (не более 1)");
                    }
                    else
                    {
                        try
                        {
                           int error = ICDO.Del_file(cp[1], 0);
                            if (error == 0)
                            {
                                Console.WriteLine("Файл успешно удален!");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ошибка пути");
                        }
                    }
                    Console.Write("Введите 'help', для отображения списка команд: ");
                    break;
                case "delc":
                    if (cp.Length > 2)
                    {
                        Console.WriteLine("Ошибка аргументов (не более 1)");
                    }
                    else
                    {
                        try
                        {
                           int error = ICDO.Del_catalog(cp[1],0);
                            if (error == 1)
                            {
                                Console.WriteLine("Удаление не произошло или произошло частично");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ошибка пути");
                        }
                    }
                    Console.Write("Введите 'help', для отображения списка команд: ");
                    break;
                case "exit":
                    if (cp.Length > 1)
                    {
                        Console.WriteLine("Ошибка аргументов (не более 1)");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Завершение работы...");
                        Console.Write("Не надо вводить 'help', для отображения списка команд, программа отключилась.");
                        Environment.Exit(0);
                    }
                    break;
                default:
                    Console.WriteLine("Неверная команда");
                    Console.Write("Введите 'help', для отображения списка команд: ");
                    break;
            }
        }
    }
}
