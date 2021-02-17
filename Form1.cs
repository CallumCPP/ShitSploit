using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeAreDevs_API;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Shitsploit
{
    public partial class Form1 : Form
    {
        public static ExploitAPI Exploit = new ExploitAPI();
        int lastSelected = -2;
        public bool autoAttach = false;

        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);

        private void formDrag_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void formDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void formDrag_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        public void AddLineNumbers()
        {
            Point pt = new Point(0, 0); 
            int First_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox1.GetLineFromCharIndex(First_Index); 
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            int Last_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox1.GetLineFromCharIndex(Last_Index);
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth(); 
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        public int getWidth()
        {
            int w = 25;
            int line = richTextBox1.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)richTextBox1.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)richTextBox1.Font.Size;
            }
            else
            {
                w = 50 + (int)richTextBox1.Font.Size;
            }

            return w;
        }

        void refresh()
        {
            listBox1.Items.Clear();

            foreach (string fileName in Directory.GetFiles(Directory.GetCurrentDirectory() + "/Scripts"))
            {
                if (!listBox1.Items.Contains(Path.GetFileName(fileName)))
                {
                    listBox1.Invoke((Action)delegate
                    {
                        listBox1.Items.Add(Path.GetFileName(fileName));
                    });
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Scripts"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Scripts");
            }

            refresh();
            LineNumberTextBox.Font = richTextBox1.Font;
            richTextBox1.Select();
            AddLineNumbers();
            BGWorker.RunWorkerAsync();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            lastSelected = listBox1.SelectedIndex;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == lastSelected)
            {
                lastSelected = -2;

                if (MessageBox.Show(this, "Are you sure you want to execute this script?", "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (Exploit.isAPIAttached())
                    {
                        Exploit.SendLuaScript(File.ReadAllText(Directory.GetCurrentDirectory() + "/Scripts/" + listBox1.SelectedItem));
                    }
                    else
                    {
                        MessageBox.Show("Please attach to process!", "Error!");
                    }
                }
            }
            else
            {
                lastSelected = listBox1.SelectedIndex;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Exploit.LaunchExploit();
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (checkBox1.Checked && !Exploit.isAPIAttached())
            {
                Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
                if (pname.Length > 0)
                {
                    Exploit.LaunchExploit();
                }
            }

            label4.Invoke((Action)delegate
            {
                label4.Text = Exploit.isAPIAttached().ToString();
            });

            Thread.Sleep(50);
        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Exploit.isAPIAttached())
            {
                Exploit.SendLuaScript(File.ReadAllText(Directory.GetCurrentDirectory() + "/Scripts/" + listBox1.SelectedItem));
            }
            else
            {
                MessageBox.Show("Please attach to process!", "Error!");
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = File.ReadAllText(Directory.GetCurrentDirectory() + "/Scripts/" + listBox1.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to clear?", "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                richTextBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Exploit.isAPIAttached())
            {
                Exploit.SendLuaScript(richTextBox1.Text);
            }
            else
            {
                MessageBox.Show("Please attach to process!", "Error!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Directory.GetCurrentDirectory() + "/Scripts/";
            saveFile.Filter = "Lua File (*.lua) | .lua";
            saveFile.Title = "Save script to:";

            if (saveFile.ShowDialog() == DialogResult.OK && saveFile.FileName != string.Empty)
            {
                File.WriteAllText(saveFile.FileName, richTextBox1.Text);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Load script:";
            openFile.InitialDirectory = Directory.GetCurrentDirectory() + "/Scripts/";

            if (openFile.ShowDialog() == DialogResult.OK && openFile.FileName != string.Empty)
            {
                richTextBox1.Text = File.ReadAllText(openFile.FileName);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form extras = new Extras();
            extras.Show();
        }

        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = richTextBox1.Font;
            richTextBox1.Select();
            AddLineNumbers();
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            richTextBox1.Select();
            LineNumberTextBox.DeselectAll();
        }

        private void panel3_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }
    }
}
