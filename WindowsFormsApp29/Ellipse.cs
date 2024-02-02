using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp29
{
    class Ellipse
    {
        private int startX, startY, radius, znach;
        private Graphics graf;
        private StringFormat format1;
        private Rectangle rect1;

        public Ellipse(int x, int y, int r, int znach)
        {
            this.startX = x;
            this.startY = y;
            this.radius = r;
            this.znach = znach;
            rect1 = new Rectangle(x, y, r, r);
            format1 = new StringFormat();
            format1.LineAlignment = StringAlignment.Center;
            format1.Alignment = StringAlignment.Center;
        }
        public Graphics Draw(Panel p1)
        {
            graf = p1.CreateGraphics();
            graf.DrawEllipse(new Pen(new SolidBrush(Color.Black)), rect1);
            graf.DrawString(Convert.ToString(znach), new Font("Arial", 10), new SolidBrush(Color.Black), rect1, format1);
            return graf;
        }
        public void Rerect()
        {
            rect1 = new Rectangle(startX, startY, radius, radius);
        }
        public int StartX { get { return startX; } set { startX = value; } }
        public int StartY { get { return startY; } set { startY = value; } }
        public int Radius { get { return radius; } set { Radius = value; } }
        public int Znach { get { return znach; } }
    }
}
