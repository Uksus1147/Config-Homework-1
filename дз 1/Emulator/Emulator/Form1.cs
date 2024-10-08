using System.Diagnostics;
using IniParser;
using IniParser.Model;

namespace Emulator
{
    public partial class Form1 : Form
    {
        private VirtualFileSystem vfs;
        private string zipFilePath;
        private string UserName;
        private string ZipFile;

        public Form1()
        {
            InitializeComponent();
            string iniFilePath = "config.ini";

            // Создание парсера
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(iniFilePath);

            // Считывание имени пользователя и имени zip-файла
            string username = data["User"]["username"];
            UserName = username;
            string zipFilename = data["File"]["zip_filename"];
            zipFilePath = zipFilename;
            ZipFile = zipFilename;
            vfs = new VirtualFileSystem(zipFilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string command = textBoxCommand.Text;
                string output = ProcessCommand(command);
                richTextBoxOutput.AppendText($">{UserName} \\ {ZipFile} \\ {command}\n{output}\n");

                textBoxCommand.Clear();
                e.SuppressKeyPress = true;
            }
        }
        private string ProcessCommand(string command)
        {
            var parts = command.Split(' ');
            var cmd = parts[0];

            switch (cmd)
            {
                case "ls":
                    return vfs.ListFiles();
                case "cd":
                    if (parts.Length > 1)
                    {
                        vfs.ChangeDirectory(parts[1]);
                        return "Directory changed successfully";
                    }
                    return "Usage: cd [directory]";
                case "echo":
                    return string.Join(" ", parts.Skip(1));
                case "exit":
                    Application.Exit();
                    return "";
                case "uptime":
                    return GetUptime();
                default:
                    return "Command not found";
            }
        }

        private string GetUptime()
        {
            TimeSpan uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;
            return $"Uptime: {uptime.Hours} hours {uptime.Minutes} minutes {uptime.Seconds} seconds";
        }

        private void richTextBoxOutput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
