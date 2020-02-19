using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompGraph_lab1_Forms
{
    public partial class Form1 : Form
    {
        //public int[] xs = new int[7] { 76, 225, 483, 598, 677, 753, 932 };
        //public int[] ys = new int[7] { 177, 246, 367, 402, 420, 569, 784 };
        public int[] xs = new int[] { 76, 225, 483 };
        public int[] ys = new int[] { 177, 246, 367 };
        public double step;
        public LinkedList<Point> points;
        private Bitmap bit;
        public Form1()
        {
            InitializeComponent();
            panel1.AutoScroll = true;
            panel1.HorizontalScroll.Visible = Visible;
            panel1.VerticalScroll.Visible = Visible;
            bit=new Bitmap(1000, 1000);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Image = bit;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graph=Graphics.FromImage(bit);
            Pen pen1 = new Pen(color: Color.Red, 3);
            Pen pen2=new Pen(Color.Aquamarine, 3);
            BuildPoints();
            for (int i = 0; i < xs.Length; i++)
            {
                graph.DrawEllipse(pen1, xs[i]-5, ys[i]-5, 10, 10);
            }

            LinkedListNode<Point> fp = points.First;
            while (fp.Next!=null)
            {
                graph.DrawLine(pen2, fp.Value, fp.Next.Value);
                fp = fp.Next;
            }

        }
        private Point Lagrange(double x)
        {
            //double y = (x - xs[1]) * (x - xs[2]) * (x - xs[3]) * (x - xs[4]) / (xs[0] - xs[1]) / (xs[0] - xs[2]) / (xs[0] - xs[3]) / (xs[0] - xs[4]) * ys[0];
            //y += (x - xs[0]) * (x - xs[2]) * (x - xs[3]) * (x - xs[4]) / (xs[1] - xs[0]) / (xs[1] - xs[2]) / (xs[1] - xs[3]) / (xs[1] - xs[4]) * ys[1];
            //y += (x - xs[0]) * (x - xs[1]) * (x - xs[3]) * (x - xs[4]) / (xs[2] - xs[0]) / (xs[2] - xs[1]) / (xs[2] - xs[3]) / (xs[2] - xs[4]) * ys[2];
            //y += (x - xs[0]) * (x - xs[1]) * (x - xs[2]) * (x - xs[4]) / (xs[3] - xs[0]) / (xs[3] - xs[1]) / (xs[3] - xs[2]) / (xs[3] - xs[4]) * ys[3];
            //y += (x - xs[0]) * (x - xs[1]) * (x - xs[2]) * (x - xs[3]) / (xs[4] - xs[0]) / (xs[4] - xs[1]) / (xs[4] - xs[2]) / (xs[4] - xs[3]) * ys[4];
            //return new Point((int)x, (int)y);

            double y = 0;

            for (int i = 0; i < xs.Length; i++)
            {
                double res = 1;
                for (int j = 0; j < xs.Length; j++)
                {
                    if (i != j)
                    {
                        res *= x - xs[j];
                        res /= xs[i] - xs[j];
                    }
                }

                res *= ys[i];
                y += res;
            }
            return new Point((int)x, (int)y);
        }

        private void BuildPoints()
        {
            points=new LinkedList<Point>();
            for (int i = xs[0]; i < xs[xs.Length-1]; i++)
            {
                points.AddLast(Lagrange(i));
            }
        }
    }
}
