using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeAreDevs_API;
using ScintillaNET;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
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
            scintilla1.Styles[0].BackColor = Color.FromArgb(48, 48, 48);
            scintilla1.Styles[0].ForeColor = Color.White;
            scintilla1.Styles[Style.Default].BackColor = Color.FromArgb(48, 48, 48);

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Scripts"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Scripts");
            }

            refresh();
            BGWorker.RunWorkerAsync();
        }

        private int maxLineNumberCharLength;
        private void scintilla1_TextChanged(object sender, EventArgs e)
        {
            var maxLineNumberCharLength = scintilla1.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            const int padding = 2;
            scintilla1.Margins[0].Width = scintilla1.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
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
            scintilla1.Text = File.ReadAllText(Directory.GetCurrentDirectory() + "/Scripts/" + listBox1.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to clear?", "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                scintilla1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Exploit.isAPIAttached())
            {
                Exploit.SendLuaScript(scintilla1.Text);
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
                File.WriteAllText(saveFile.FileName, scintilla1.Text);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Load script:";
            openFile.InitialDirectory = Directory.GetCurrentDirectory() + "/Scripts/";

            if (openFile.ShowDialog() == DialogResult.OK && openFile.FileName != string.Empty)
            {
                scintilla1.Text = File.ReadAllText(openFile.FileName);
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
    }
}
