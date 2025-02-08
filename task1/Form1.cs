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
        Font axisLabelFont = new Font("Arial", 7);

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGraph();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            DrawGraph();
        }

        private void DrawGraph()
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(Color.White);

                Pen axisPen = new Pen(Color.Black, 1);
                Pen graphPen = new Pen(Color.Blue, 2);
                Font labelFont = new Font("Arial", 12);
                SolidBrush labelBrush = new SolidBrush(Color.Blue);
                StringFormat labelFormat = new StringFormat { FormatFlags = StringFormatFlags.DirectionRightToLeft };

                int width = this.ClientSize.Width;
                int height = this.ClientSize.Height;
                Point center = new Point(width / 2, height / 2);

                DrawAxes(g, axisPen, center, width, height);
                DrawGrid(g, axisPen, center, width, height);
                PlotGraph(g, graphPen, center);
                DrawLabels(g, labelFont, axisLabelFont, labelBrush, labelFormat, center);
            }
        }

        private void DrawAxes(Graphics g, Pen axisPen, Point center, int width, int height)
        {
            g.DrawLine(axisPen, 5, center.Y, width - 5, center.Y);
            g.DrawLine(axisPen, center.X, 10, center.X, height - 5);
        }

        private void DrawGrid(Graphics g, Pen axisPen, Point center, int width, int height)
        {
            int stepForAxes = 25;
            int lengthShtrih = 3;
            float oneDelenieX = (float)(10) / (center.X / (float)stepForAxes);
            float oneDelenieY = (float)(80) / (center.Y / (float)stepForAxes);


            for (int i = center.X, j = center.X, k = 1; i < 2 * center.X - 30;
                j -= stepForAxes, i += stepForAxes, k++)
            {
                g.DrawLine(axisPen, i, center.Y - lengthShtrih, i, center.Y + lengthShtrih);
                g.DrawLine(axisPen, j, center.Y - lengthShtrih, j, center.Y + lengthShtrih);
                if (i < 2 * center.X - 55)
                {
                    g.DrawString((k * oneDelenieX).ToString("0.0"), axisLabelFont, new SolidBrush(Color.Blue), new PointF(i + stepForAxes + 9, center.Y + 6));
                    g.DrawString((k * oneDelenieX).ToString("0.0") + "-", axisLabelFont, new SolidBrush(Color.Blue), new PointF(j - stepForAxes + 9, center.Y + 6));
                }
            }

            // Y axis grid
            for (int i = center.Y, j = center.Y, k = 1; i < 2 * center.Y - 30;
                j -= stepForAxes, i += stepForAxes, k++)
            {
                g.DrawLine(axisPen, center.X - lengthShtrih, i, center.X + lengthShtrih, i);
                g.DrawLine(axisPen, center.X - lengthShtrih, j, center.X + lengthShtrih, j);

                if (i < 2 * center.Y - 55)
                {
                    g.DrawString((k * oneDelenieY).ToString("0.0") + "-", axisLabelFont, new SolidBrush(Color.Blue), new PointF(center.X, i - 5));
                    g.DrawString((k * oneDelenieY).ToString("0.0"), axisLabelFont, new SolidBrush(Color.Blue), new PointF(center.X, j - 5));
                }
            }
        }

        private void PlotGraph(Graphics g, Pen graphPen, Point center)
        {
            int numberOfPoints = 100;
            int minX = -6;
            int maxX = 10;
            int maxY = 80;
            float step = (float)(maxX - minX) / (numberOfPoints - 1);
            float[] mathX = new float[numberOfPoints];
            float[] mathY = new float[numberOfPoints];

            for (int i = 0; i < numberOfPoints; i++)
            {
                mathX[i] = minX + step * i;
                mathY[i] = (float)(Math.Pow(Math.E, mathX[i] / 2) * Math.Sin(2 * mathX[i]));
            }

            Point[] points = new Point[numberOfPoints];
            float scaleX = center.X / maxX;
            float scaleY = center.Y / maxY;

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].X = (int)(center.X + mathX[i] * scaleX);
                points[i].Y = (int)(center.Y - mathY[i] * scaleY);
            }

            g.DrawCurve(graphPen, points);
        }

        private void DrawLabels(Graphics g, Font labelFont, Font axisLabelFont, SolidBrush labelBrush, StringFormat labelFormat, Point center)
        {
            g.DrawString("X", labelFont, labelBrush, new PointF(2 * center.X - 5, center.Y + 10), labelFormat);
            g.DrawString("Y", labelFont, labelBrush, new PointF(center.X + 30, 5), labelFormat);
        }
    }
}
