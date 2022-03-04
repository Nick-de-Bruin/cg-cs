using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        // Axes
        AxisX x_axis;
        AxisY y_axis;

        // Objects
        Square square;
        Square orangeSquare;
        Square cyanSquare;
        Square darkBlueSquare;

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        public Form1()
        {
            InitializeComponent();

            this.Width = WIDTH;
            this.Height = HEIGHT;
            this.DoubleBuffered = true;

            Vector v1 = new Vector();
            Console.WriteLine(v1);
            Vector v2 = new Vector(1, 2, 0);
            Console.WriteLine(v2);
            Vector v3 = new Vector(2, 6, 0);
            Console.WriteLine(v3);
            Vector v4 = v2 + v3;
            Console.WriteLine(v4); // 3, 8

            Matrix m1 = Matrix.Identity();
            Console.WriteLine(m1); // 1, 0, 0, 1
            Matrix m2 = new Matrix(2, 4, 0,
                                   -1, 3, 0,
                                   0, 0, 0);
            Console.WriteLine(m2);

            Console.WriteLine("Plus");
            Console.WriteLine(m1 + m2); // 3, 4, -1, 4
            Console.WriteLine("Minus");
            Console.WriteLine(m1 - m2); // -1, -4, 1, -2
            Console.WriteLine("Matrix x matrix");
            Console.WriteLine(m2 * m2); // 0, 20, -5, 5

            Console.WriteLine("Matrix x vector");
            Console.WriteLine(m2 * v3); // 28, 16

            // Define axes
            x_axis = new AxisX(200);
            y_axis = new AxisY(200);

            // Create objects
            square = new Square(Color.Purple, 100);
            cyanSquare = new Square(Color.Cyan, 100);
            orangeSquare = new Square(Color.Orange, 100);
            darkBlueSquare = new Square(Color.DarkBlue, 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            x_axis.Draw(e.Graphics, ViewportTransformation(x_axis.vertexbuffer));
            y_axis.Draw(e.Graphics, ViewportTransformation(y_axis.vertexbuffer));

            // Draw square
            square.Draw(e.Graphics, ViewportTransformation(square.vertexbuffer));

            cyanSquare.Draw(e.Graphics, ViewportTransformation(
                Transformation(cyanSquare.vertexbuffer, 
                Matrix.ScaleMatrix(1.5f))));

            orangeSquare.Draw(e.Graphics, ViewportTransformation(
                Transformation(orangeSquare.vertexbuffer,
                Matrix.RotateMatrix(20))));

            darkBlueSquare.Draw(e.Graphics, ViewportTransformation(
                Transformation(darkBlueSquare.vertexbuffer,
                Matrix.TranslateMatrix(new Vector(75, -25, 0)))));
        }

        public static List<Vector> Transformation(List<Vector> vb, Matrix matrix)
        {
            List<Vector> result = new List<Vector>();

            foreach (Vector v in vb)
                result.Add(matrix * v);

            return result;
        }

        public static List<Vector> ViewportTransformation(List<Vector> vb)
        {
            List<Vector> result = new List<Vector>();
            float delta_x = WIDTH / 2;
            float delta_y = HEIGHT / 2;

            foreach (Vector v in vb)
                result.Add(new Vector(v.x + delta_x, -v.y + delta_y, 0));

            return result;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}
