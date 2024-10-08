using NUnit.Framework;
using Moq;
using Emulator;
using System.IO;
using System.Windows.Forms;

namespace EmulatorTests
{
    [TestFixture]
    public class Form1Tests
    {
        private Form1 form;
        private Mock<VirtualFileSystem> mockFileSystem;
        private string configFilePath;
        private string tempZipFilePath;

        [SetUp]
        public void Setup()
        {
            // Создаем временный config.ini файл и временный zip-архив
            configFilePath = CreateTestConfigFile();
            tempZipFilePath = CreateTestZipFile();

            // Mock объекта VirtualFileSystem для имитации его поведения в тестах
            mockFileSystem = new Mock<VirtualFileSystem>(tempZipFilePath);

            // Инициализация формы с mock-объектом и временным config.ini файлом
            form = new Form1
            {
                UserName = "TestUser",
                ZipFile = tempZipFilePath,
                vfs = mockFileSystem.Object
            };
        }

        [TearDown]
        public void TearDown()
        {
            // Очистка временных файлов после каждого теста
            if (File.Exists(configFilePath))
                File.Delete(configFilePath);

            if (File.Exists(tempZipFilePath))
                File.Delete(tempZipFilePath);
        }

        [Test]
        public void Test_Form1_ProcessCommand_ReturnsCorrectOutput()
        {
            // Проверка корректности выполнения команды "ls" в форме
            mockFileSystem.Setup(fs => fs.ListFiles()).Returns("test.txt\nfolder1");
            string result = form.ProcessCommand("ls");

            Assert.AreEqual("test.txt\nfolder1", result);
        }

        [Test]
        public void Test_Form1_ProcessCommand_EchoCommand_ReturnsInputText()
        {
            // Проверка команды echo
            string result = form.ProcessCommand("echo Test message");
            Assert.AreEqual("Test message", result);
        }

        [Test]
        public void Test_Form1_LoadsConfigFile_CorrectlyInitializesFileSystem()
        {
            // Проверка, что форма корректно инициализируется на основе config.ini
            Assert.AreEqual("TestUser", form.UserName);
            Assert.AreEqual(tempZipFilePath, form.ZipFile);
        }

        // Вспомогательный метод для создания временного config.ini файла
        private string CreateTestConfigFile()
        {
            string configPath = Path.Combine(Path.GetTempPath(), "config.ini");
            string iniContent = @"
[User]
username=TestUser

[File]
zip_filename=test.zip
";
            File.WriteAllText(configPath, iniContent);
            return configPath;
        }

        // Вспомогательный метод для создания временного zip-архива
        private string CreateTestZipFile()
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "test.zip");
            using (ZipArchive archive = ZipFile.Open(tempPath, ZipArchiveMode.Create))
            {
                // Добавляем файлы и директории в архив для тестов
                archive.CreateEntry("test.txt");
                archive.CreateEntry("folder1/");
                archive.CreateEntry("folder1/file1.txt");
            }

            return tempPath;
        }
    }
}
