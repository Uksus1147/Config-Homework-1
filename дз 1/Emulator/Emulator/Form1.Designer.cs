namespace Emulator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxCommand = new TextBox();
            richTextBoxOutput = new RichTextBox();
            buttonLoadZip = new Button();
            SuspendLayout();
            // 
            // textBoxCommand
            // 
            textBoxCommand.Dock = DockStyle.Bottom;
            textBoxCommand.Location = new Point(0, 427);
            textBoxCommand.Name = "textBoxCommand";
            textBoxCommand.Size = new Size(800, 23);
            textBoxCommand.TabIndex = 0;
            textBoxCommand.TextChanged += textBox1_TextChanged;
            textBoxCommand.KeyDown += textBoxCommand_KeyDown;
            // 
            // richTextBoxOutput
            // 
            richTextBoxOutput.Dock = DockStyle.Top;
            richTextBoxOutput.Location = new Point(0, 0);
            richTextBoxOutput.Name = "richTextBoxOutput";
            richTextBoxOutput.Size = new Size(800, 348);
            richTextBoxOutput.TabIndex = 1;
            richTextBoxOutput.Text = "";
            richTextBoxOutput.TextChanged += richTextBoxOutput_TextChanged;
            // 
            // buttonLoadZip
            // 
            buttonLoadZip.Location = new Point(0, 354);
            buttonLoadZip.Name = "buttonLoadZip";
            buttonLoadZip.Size = new Size(107, 67);
            buttonLoadZip.TabIndex = 3;
            buttonLoadZip.Text = "Load to zip file";
            buttonLoadZip.UseVisualStyleBackColor = true;
            buttonLoadZip.Click += buttonLoadZip_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonLoadZip);
            Controls.Add(richTextBoxOutput);
            Controls.Add(textBoxCommand);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxCommand;
        private RichTextBox richTextBoxOutput;
        private Button buttonLoadZip;
    }
}
