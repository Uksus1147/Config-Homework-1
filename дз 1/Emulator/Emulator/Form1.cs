using System.Diagnostics;

namespace Emulator
{
    public partial class Form1 : Form
    {
        private VirtualFileSystem vfs;
        private string zipFilePath;
        public Form1()
        {
            InitializeComponent();
            //vfs = new VirtualFileSystem(zipFilePath);
        }
        // Метод для открытия диалога выбора файла
        private void LoadZipFile()
        {
            // Создаем экземпляр диалога выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Устанавливаем фильтр, чтобы показывались только zip-файлы
            openFileDialog.Filter = "Zip files (*.zip)|*.zip";
            openFileDialog.Title = "Select Virtual File System (zip)";

            // Если пользователь выбрал файл и нажал "ОК"
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                 zipFilePath = openFileDialog.FileName; // Получаем путь к выбранному файлу

                try
                {
                    // Передаем путь в нашу виртуальную файловую систему
                    vfs = new VirtualFileSystem(zipFilePath);

                    // Сообщаем пользователю, что файл успешно загружен
                    MessageBox.Show("Virtual File System loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // В случае ошибки при загрузке архива выводим сообщение
                    MessageBox.Show($"Error loading zip file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
                richTextBoxOutput.AppendText($"> {command}\n{output}\n");

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
                //case "cd":
                //    if (parts.Length > 1)
                //        return vfs.ChangeDirectory(parts[0]);
                //    return "Usage: cd [directory]";
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

        private void buttonLoadZip_Click(object sender, EventArgs e)
        {
            LoadZipFile(); // Вызываем метод загрузки zip-файла
        }

        private void richTextBoxOutput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
