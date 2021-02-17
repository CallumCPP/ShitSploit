using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shitsploit
{
    public partial class LocalPlayer : Form
    {
        public LocalPlayer()
        {
            InitializeComponent();
        }

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

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.Exploit.isAPIAttached())
            {
                Form1.Exploit.DoBTools();
            }
            else
            {
                MessageBox.Show("Please attach to process!", "Error!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Form1.Exploit.isAPIAttached())
            {
                try
                {
                    Form1.Exploit.SetWalkSpeed("me", Int32.Parse(textBox1.Text));
                }
                catch { MessageBox.Show("Please ensure valid values!", "Error!"); }
            }
            else
            {
                MessageBox.Show("Please attach to process!", "Error!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Form1.Exploit.isAPIAttached())
            {
                try
                {
                    Form1.Exploit.SendCommand("hipheight me " + Int32.Parse(textBox3.Text).ToString());
                }
                catch { MessageBox.Show("Please ensure valid values!", "Error!"); }
            }
            else
            {
                MessageBox.Show("Please attach to process!", "Error!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Form1.Exploit.isAPIAttached())
            {
                try
                {
                    Form1.Exploit.SendCommand("kill me");
                }
                catch { MessageBox.Show("Please ensure valid values!", "Error!"); }
            }
            else
            {
                MessageBox.Show("Please attach to process!", "Error!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Form1.Exploit.isAPIAttached())
            {
                try
                {
                    Form1.Exploit.SendCommand("vectorteleport " + Int32.Parse(textBox4.Text) + " " + Int32.Parse(textBox6.Text) + " " + Int32.Parse(textBox5.Text));
                }
                catch { MessageBox.Show("Please ensure valid values!", "Error!"); }
            }
            else
            {
                MessageBox.Show("Please attach to process!", "Error!");
            }
        }
    }
}
