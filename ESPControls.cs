using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shitsploit
{
    public partial class ESPControls : Form
    {
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

        public ESPControls()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.Exploit.isAPIAttached())
            {
                try
                {
                    Form1.Exploit.SendCommand("boxesp");
                }
                catch { MessageBox.Show("Please ensure valid values!", "Error!"); }
            }
            else
            {
                MessageBox.Show("Please attach to process!", "Error!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Form1.Exploit.isAPIAttached())
            {
                try
                {
                    Form1.Exploit.SendCommand("boxespteamcheck");
                }
                catch { MessageBox.Show("Please ensure valid values!", "Error!"); }
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
                    Form1.Exploit.SendCommand("boxesplines");
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
                    Form1.Exploit.SendCommand("boxespnames");
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
                    Form1.Exploit.SendCommand("boxespdistance");
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
