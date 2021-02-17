using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shitsploit
{
    public partial class More : Form
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

        public More()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.Exploit.isAPIAttached())
            {
                try
                {
                    Form1.Exploit.SendLuaScript("loadstring(game:HttpGet('https://raw.githubusercontent.com/EdgeIY/infiniteyield/master/source'))()");
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
                    Form1.Exploit.SendLuaScript("loadstring(game:HttpGet('https://gist.githubusercontent.com/DinosaurXxX/b757fe011e7e600c0873f967fe427dc2/raw/ee5324771f017073fc30e640323ac2a9b3bfc550/dark%2520dex%2520v4'))()");
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
    }
}
