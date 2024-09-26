using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Text;

public class VirtualFileSystem
{
    //private Dictionary<string, List<string>> fileStructure;
    private string currentDirectory;
    ZipArchive archive;
    string[] currentPath = new string[] { };
    //public VirtualFileSystem()
    //{
    //    fileStructure = new Dictionary<string, List<string>>();
    //    currentDirectory = "/";
    //}
    public VirtualFileSystem(string zipFilePath)
    {
        //fileStructure = new Dictionary<string, List<string>>();
        LoadFileSystemFromZip(zipFilePath);
        currentDirectory = "/";
    }
    // Чтение zip-архива и построение файловой системы
    private void LoadFileSystemFromZip(string zipFilePath)
    {
        archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update);
        
            //foreach (ZipArchiveEntry entry in archive.Entries)
            //{

                //string directory = Path.GetDirectoryName(entry.FullName).Replace('\\', '/');
                //string fileName = Path.GetFileName(entry.FullName);

                //// Для отладки: выводим директории и файлы
                //Console.WriteLine($"Directory: {directory}, File: {fileName}");


                //if (!fileStructure.ContainsKey(directory))
                //{
                //    fileStructure[directory] = new List<string>();
                //}

                //if (!string.IsNullOrEmpty(fileName))
                //{
                //    fileStructure[directory].Add(fileName);
                //}
            
        
        //// Выводим для диагностики с помощью MessageBox
        //string diagnosticInfo = string.Join("\n", fileStructure.Select(kvp => $"Directory: {kvp.Key}, Files: {string.Join(", ", kvp.Value)}"));
        //MessageBox.Show(diagnosticInfo, "Diagnostic Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // Команда ls
    public string ListFiles()
    {

        var output = new StringBuilder();
        foreach (ZipArchiveEntry entry in archive.Entries)
        {
            string entryPath = entry.FullName.Trim('/');
             string[] pathParts = entryPath.Split('/');
            string last = pathParts.Last();
            var a = pathParts[0..(pathParts.Length - 1)];
            if (currentPath.SequenceEqual(a))
                {
                output.AppendLine(last);
                
                //Console.Write(last + " ");
               }
            }
        if (output.Length == 0)
        {
             return "Directory not found";
        }
        return output.ToString();
    }
    //MessageBox.Show($"Current directory: {currentDirectory}"); // Для отладки

    //if (fileStructure.ContainsKey(currentDirectory))
    //{
    //    return string.Join("\n", fileStructure[currentDirectory]);
    //}
    //else
    //{
    ////    Console.WriteLine("Directory not found in file structure!"); // Для отладки
    ////    return "Directory not found";
    ////}


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
}
