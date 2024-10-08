using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Text;

namespace Emulator
{
    public class VirtualFileSystem : IDisposable
    {

        private string currentDirectory;
        ZipArchive archive;
        string[] currentPath = new string[] { };

        public VirtualFileSystem(string zipFilePath)
        {

            LoadFileSystemFromZip(zipFilePath);
            currentDirectory = "/";
        }
        // Чтение zip-архива и построение файловой системы
        private void LoadFileSystemFromZip(string zipFilePath)
        {
            archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update);
        }


        public string ListFiles()
        {
            var output = new StringBuilder();

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                string entryPath = entry.FullName.Trim('/');
                string[] pathParts = entryPath.Split('/');
                string fileName = pathParts.Last(); // Имя файла или папки
                var entryParentPath = pathParts[0..^1]; // Путь к родительской папке

                // Проверяем, соответствует ли текущий путь выбранной директории
                if (currentPath.SequenceEqual(entryParentPath))
                {
                    // Собираем полный путь к элементу относительно текущей директории
                    string fullPath = "/" + string.Join("/", entryParentPath) + "/" + fileName;
                    output.AppendLine(fullPath);
                }
            }

            if (output.Length == 0)
            {
                return "Directory not found";
            }

            return output.ToString();
        }



        //Команда cd
        public void ChangeDirectory(string directory)
        {
            string[] parts = directory.Split(' ');
            currentPath = parts[0].Split("/").Where(i => !string.IsNullOrEmpty(i)).ToArray();
        }

        // Получить текущую директорию
        public string GetCurrentDirectory()
        {
            return currentDirectory;
        }

        public void Dispose()
        {
            archive.Dispose();
        }
    }
}