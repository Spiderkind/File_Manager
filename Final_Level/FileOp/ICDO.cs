using System;
using System.IO;

namespace FileOp
{
    public class ICDO
    {
        //Показывает информацию о файле или папке
        public static void SMIFC(string path)
        {
            if (File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                Console.WriteLine($"Имя файла {file.Name}\n" +
                    $"Расположение файла {file.Directory}\n" +
                    $"Дата создания файла {file.CreationTime}\n" +
                    $"Дата изменения файла {file.LastWriteTime}\n" +
                    $"Размер файла {file.Length}");
            }
            else if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                Console.WriteLine($"Имя папки {dir.Name}\n" +
                    $"Полное имя папки {dir.FullName}\n" +
                    $"Расположение папки {dir.Parent}\n" +
                    $"Дата создания папки {dir.CreationTime}\n" +
                    $"Дата доступа папки {dir.LastAccessTime}\n" +
                    $"Дата изменения папки {dir.LastWriteTime}");
            }
            else
            {
                Console.WriteLine("Ошибка пути");
            }

        }
        //Копирует файл
        public static void Copy_file(string opath, string npath)
        {
            try
            {
                File.Copy(opath, npath);
                Console.WriteLine("Копирование успешно завершено!");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Ошибка пути");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Папка не существует");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не существует");
            }
            catch (IOException)
            {
                Console.WriteLine("Указано неверное имя файла или раположение");
            }
            catch
            {
                Console.WriteLine("Неожиданная ошибка...");
            }
        }
        //Копирует каталог
        public static int Copy_catalog(DirectoryInfo opath, DirectoryInfo npath, int error)
        {
            if (!npath.Exists)
            {
                npath.Create();
            }
            try
            {
                FileInfo[] files = opath.GetFiles();
                foreach (FileInfo file in files)
                {
                    file.CopyTo(Path.Combine(npath.FullName, file.Name));
                }
                DirectoryInfo[] dirs = opath.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    string destinationDir = Path.Combine(npath.FullName, dir.Name);
                    Copy_catalog(dir, new DirectoryInfo(destinationDir), error);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Ошибка начального пути");
                error = 1;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа");
                error = 1;
            }
            return error;
        }
        //Удаляет файл
        public static int Del_file(string path, int error)
        {
            try
            {
                File.Delete(path);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа");
                error = 1;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Ошибка пути");
                error = 1;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Папка не существует");
                error = 1;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не существует");
                error = 1;
            }
            catch (IOException)
            {
                Console.WriteLine("Указано неверное имя файла или раположение");
                error = 1;
            }
            return error;
        }
        //Удаляет каталог
        public static int Del_catalog(string path,int error)
        {
            try
            {
                string[] allfiles = Directory.GetFiles(path);
                foreach (string file in allfiles)
                {
                    Del_file(file, 0);
                }
                string[] allfolders = Directory.GetDirectories(path);
                foreach (string folder in allfolders)
                {
                    Del_catalog(folder, error);
                }
                Directory.Delete(path);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Ошибка дотсупа");
                error = 1;
            }
            return error;
        }
    }
}