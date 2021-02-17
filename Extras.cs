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
    public partial class Extras : Form
    {
        public Extras()
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
            Form LocalPlayer = new LocalPlayer();
            LocalPlayer.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form ESP = new ESPControls();
            ESP.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form More = new More();
            More.Show();
        }
    }
}
