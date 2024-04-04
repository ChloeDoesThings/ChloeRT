using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unluau;
using static System.Windows.Forms.Design.AxImporter;

namespace ChloeRT.ChloeRT.LuauDecompiler
{
    public partial class LuauDecompilerWindow : Form
    {
        public LuauDecompilerWindow()
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

        private void LuauDecompilerWindow_Load(object sender, EventArgs e)
        {
            this.Text = randomStr(25);
        }

        private void LuauDecompilerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LuauDecompilerData.luauDecompiler_Window_DebugTextbox = this.richTextBox2;
            richTextBox2.Text = "";
            richTextBox1.Text = "";

            Stream stream = File.OpenRead(LuauDecompilerData.luauDecompiler_OpenedFile);

            if (!Directory.Exists("luau_decompiler"))
            {
                Directory.CreateDirectory("luau_decompiler");
            }
            if (!File.Exists("luau_decompiler\\decompile_output.txt"))
            {
                File.Create("luau_decompiler\\decompile_output.txt").Close(); // Close the file stream after creation
            }
            if (!File.Exists("luau_decompiler\\disassemble_output.txt"))
            {
                File.Create("luau_decompiler\\disassemble_output.txt").Close(); // Close the file stream after creation
            }

            // Create a StreamWriter with using statement to ensure it's properly disposed
            using (StreamWriter hmm = File.CreateText("luau_decompiler\\decompile_output.txt"))
            {
                DecompilerOptions decompilerOptions = new DecompilerOptions()
                {
                    Output = new Output(hmm),
                    DescriptiveComments = true,
                    HeaderEnabled = true,
                    InlineTableDefintions = true,
                    RenameUpvalues = true,
                    VariableNameGuessing = true,
                    Version = "v1.0.0",
                    PerferStringInterpolation = true,
                    Encoding = OpCodeEncoding.Client,
                };

                Decompiler decompiler = new Decompiler(stream, decompilerOptions);
                File.WriteAllText("luau_decompiler\\disassemble_output.txt", decompiler.Dissasemble());

                Thread thread = new Thread(() => decompiler.Decompile());
                thread.Start();

                // wait for it to finish
                thread.Join();
            }

            if (File.Exists("luau_decompiler\\disassemble_output.txt") && File.Exists("luau_decompiler\\decompile_output.txt"))
                {
                    // yeah this looks bad lmao
                    richTextBox1.Text = "-- Disassembled/Decompiled using ChloeRT Luau Decompiler (scuffed unluau fork)\n";
                    richTextBox1.Text += "-- Please note that there are prob A TON of bugs, make sure to report them using the ChloeRT GitHub repo's issues tab\n";
                    richTextBox1.Text += "\n\n\n-- DISASSEMBLED RESULTS: \n--[[\n";
                    richTextBox1.Text += File.ReadAllText("luau_decompiler\\disassemble_output.txt");
                    richTextBox1.Text += "]]\n\n-- DECOMPILED RESULTS: \n--[[\n";
                    richTextBox1.Text += File.ReadAllText("luau_decompiler\\decompile_output.txt");
                    richTextBox1.Text += "]]";
                }
                else
                {
                    richTextBox1.Text = "-- ChloeRT failed to read decompiled/disassembled results :(";
                }

        }
    }
}
