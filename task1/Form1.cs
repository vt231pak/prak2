using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            one();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            one();
        }

        void one()
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            Pen p1 = new Pen(Color.Black, 1);
            Pen p2 = new Pen(Color.Blue, 2);
            int sizeWidth = this.ClientSize.Width;
            int sizeHeight = this.ClientSize.Height;
            Point center = new Point(sizeWidth / 2, sizeHeight / 2);
            g.DrawLine(p1, 5, center.Y, sizeWidth-5, center.Y);
            g.DrawLine(p1, center.X, 10, center.X, sizeHeight-5);

            int numberOfPoints = 100;

            int minX = -6;
            int maxX = 10;
            int maxY = 80;
            float step = (float)(maxX - minX) / (numberOfPoints - 1);
            
            float[] mathX = new float[numberOfPoints];
            float[] mathY = new float[numberOfPoints];

            for ( int i = 0; i < numberOfPoints; i++ )
            {
                mathX[i] = minX + step * i;
                mathY[i] = (float)(Math.Pow(Math.E, mathX[i] / 2) * Math.Sin(2 * mathX[i]));
            }
            Point[] points = new Point[numberOfPoints];

            float scaleX = center.X / maxX;
            float scaleY = center.Y / maxY;

            for (int i = 0; i < numberOfPoints; i++)
            {
                chart1.Series[0].Points.AddXY(mathX[i], mathY[i]);
                points[i].X= (int)(center.X + mathX[i] * scaleX);
                points[i].Y= (int)(center.Y - mathY[i] * scaleY);
            }

            g.DrawCurve(p2, points);
            

                Font drawFont = new Font("Arial", 12);
                Font signatureFont = new Font("Arial", 7);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                StringFormat drawFormat = new StringFormat();
                drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                g.DrawLine(p1, 10, center.Y, center.X, center.Y);
                g.DrawLine(p1, center.X, center.Y, 2 * center.X - 10, center.Y);
                g.DrawLine(p1, center.X, 10, center.X, center.Y);
                g.DrawLine(p1, center.X, center.Y, center.X, 2 * center.Y - 10);
                g.DrawString("X", drawFont, drawBrush, new PointF(2 * center.X - 5, center.Y + 10), drawFormat);
                g.DrawString("Y", drawFont, drawBrush, new PointF(center.X + 30, 5), drawFormat);
            int stepForAxes = 25;
            int lengthShtrih = 3;
            float oneDelenieX = (float)maxX / ((float)center.X / (float)stepForAxes);
            float oneDelenieY = (float)maxY / ((float)center.Y / (float)stepForAxes);
            for (int i = center.X, j = center.X, k = 1; i < 2 * center.X - 30;
                j -= stepForAxes, i += stepForAxes, k++)
            {
                g.DrawLine(p1, i, center.Y - lengthShtrih, i, center.Y + lengthShtrih);
                g.DrawLine(p1, j, center.Y - lengthShtrih, j, center.Y + lengthShtrih);
                if (i < 2 * center.X - 55)
                {
                    g.DrawString((k * oneDelenieX).ToString("0.0"), signatureFont, drawBrush, new PointF(i + stepForAxes + 9, center.Y + 6), drawFormat);
                    g.DrawString((k * oneDelenieX).ToString("0.0") + "-", signatureFont, drawBrush, new PointF(j - stepForAxes + 9, center.Y + 6), drawFormat);
                }
            }
            for (int i = center.Y, j = center.Y, k = 1; i < 2 * center.Y - 30;
     j -= stepForAxes, i += stepForAxes, k++)
            {
                g.DrawLine(p1, center.X - lengthShtrih, i, center.X + lengthShtrih, i);
                g.DrawLine(p1, center.X - lengthShtrih, j, center.X + lengthShtrih, j);

                if (i < 2 * center.Y - 55)
                {
                    g.DrawString((k * oneDelenieY).ToString("0.0")+"-", signatureFont, drawBrush, new PointF(center.X , i - 5), drawFormat);
                    g.DrawString((k * oneDelenieY).ToString("0.0"), signatureFont, drawBrush, new PointF(center.X , j - 5), drawFormat);
                }
            }
        }
    }
}
