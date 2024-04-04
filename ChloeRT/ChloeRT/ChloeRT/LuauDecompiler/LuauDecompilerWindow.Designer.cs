namespace ChloeRT.ChloeRT.LuauDecompiler
{
    partial class LuauDecompilerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            richTextBox2 = new RichTextBox();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.FromArgb(35, 35, 35);
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Font = new Font("Product Sans", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextBox1.ForeColor = Color.White;
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(677, 481);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "Decompilation Output";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(45, 45, 45);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Product Sans", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(12, 499);
            button1.Name = "button1";
            button1.Size = new Size(279, 51);
            button1.TabIndex = 2;
            button1.Text = "Decompile";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // richTextBox2
            // 
            richTextBox2.BackColor = Color.FromArgb(35, 35, 35);
            richTextBox2.BorderStyle = BorderStyle.None;
            richTextBox2.Font = new Font("Product Sans", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextBox2.ForeColor = Color.White;
            richTextBox2.Location = new Point(695, 12);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(279, 481);
            richTextBox2.TabIndex = 3;
            richTextBox2.Text = "Debug Output";
            // 
            // LuauDecompilerWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(987, 562);
            Controls.Add(richTextBox2);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LuauDecompilerWindow";
            FormClosed += LuauDecompilerWindow_FormClosed;
            Load += LuauDecompilerWindow_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button button1;
        private RichTextBox richTextBox2;
    }
}