using ChloeRT.ChloeRT.LuauDecompiler;
using System;

namespace ChloeRT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string randomStr(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = randomStr(25);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            LuauDecompilerData.luauDecompiler_OpenedFile = openFileDialog.FileName;

            LuauDecompilerWindow window = new LuauDecompilerWindow();
            window.Show();
        }
    }
}
