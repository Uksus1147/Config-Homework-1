using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Xunit;

namespace Emulator
{
    [CollectionDefinition(nameof(VirtualFileSystemDefinition), DisableParallelization = true)]
    public class VirtualFileSystemDefinition { }

    [Collection(nameof(VirtualFileSystemDefinition))]
    public class VirtualFileSystemTests
    {
        private const string TestZipFilePath = "test.zip";

        [Fact]
        public void ListFiles_RootDirectory_ShouldReturnRootFiles()
        {
            // Arrange
            CreateTestZipArchive();
            using (var vfs = new VirtualFileSystem(TestZipFilePath))
            {
                vfs.ChangeDirectory("/");

                // Act
                string output = vfs.ListFiles();

                // Assert
                Assert.Contains("rootfile1.txt", output);
                Assert.Contains("rootfile2.txt", output);
                Assert.DoesNotContain("subdirfile.txt", output);
            };
            File.Delete(TestZipFilePath);
        }

        [Fact]
        public void ChangeDirectory_Subdirectory_ShouldChangeToSubdirectory()
        {
            // Arrange
            CreateTestZipArchive();
            using (var vfs = new VirtualFileSystem(TestZipFilePath)) {

                vfs.ChangeDirectory("/subdir");

                // Act
                string currentDirectory = vfs.ListFiles();

                // Assert
                Assert.Contains("subdirfile.txt", currentDirectory);
            };
            File.Delete(TestZipFilePath);
        }

        [Fact]
        public void GetCurrentDirectory_ShouldReturnCurrentDirectoryPath()
        {
            // Arrange
            CreateTestZipArchive();
            using (var vfs = new VirtualFileSystem(TestZipFilePath))
            {
                vfs.ChangeDirectory("/subdir");

                // Act
                string currentDirectory = vfs.GetCurrentDirectory();

                // Assert
                Assert.Equal("/", currentDirectory);
            };
            File.Delete(TestZipFilePath);
        }

        // Метод для создания тестового zip-архива
        private void CreateTestZipArchive()
        {
            using (var archive = ZipFile.Open(TestZipFilePath, ZipArchiveMode.Update))
            {
                // Создаем файлы в корневом каталоге
                archive.CreateEntry("rootfile1.txt");
                archive.CreateEntry("rootfile2.txt");

                // Создаем файлы в подкаталоге
                archive.CreateEntry("subdir/subdirfile.txt");
            }
        }
    }
}
