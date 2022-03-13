using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        // Axes
        private readonly AxisX x_axis;
        private readonly AxisY y_axis;
        private readonly AxisZ z_axis;

        // Objects
        private readonly Cube cube;
        // private readonly Square square;
        // private readonly Square orangeSquare;
        // private readonly Square cyanSquare;
        // private readonly Square darkBlueSquare;

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        public Form1()
        {
            InitializeComponent();

            Width = WIDTH;
            Height = HEIGHT;
            DoubleBuffered = true;

            // Define axes
            x_axis = new AxisX(200);
            y_axis = new AxisY(200);
            z_axis = new AxisZ(200);

            // Create objects
            cube = new Cube(Color.Blue);
            // square = new Square(Color.Purple, 100);
            // cyanSquare = new Square(Color.Cyan, 100);
            // orangeSquare = new Square(Color.Orange, 100);
            // darkBlueSquare = new Square(Color.DarkBlue, 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            x_axis.Draw(e.Graphics, ViewportTransformation(x_axis.vertexbuffer));
            y_axis.Draw(e.Graphics, ViewportTransformation(y_axis.vertexbuffer));
            z_axis.Draw(e.Graphics, ViewportTransformation(z_axis.vertexbuffer));

            // Draw cube
            cube.Draw(e.Graphics, ViewportTransformation(
                Transformation(cube.vertexbuffer,
                Matrix.ScaleMatrix(100f))
            ));

            // Draw square
            // square.Draw(e.Graphics, ViewportTransformation(square.vertexbuffer));

            // cyanSquare.Draw(e.Graphics, ViewportTransformation(
            //     Transformation(cyanSquare.vertexbuffer, 
            //     Matrix.ScaleMatrix(1.5f))));

            // orangeSquare.Draw(e.Graphics, ViewportTransformation(
            //     Transformation(orangeSquare.vertexbuffer,
            //     Matrix.RotateMatrixZ(20))));

            // darkBlueSquare.Draw(e.Graphics, ViewportTransformation(
            //     Transformation(darkBlueSquare.vertexbuffer,
            //     Matrix.TranslateMatrix(new Vector(75, -25, 0)))));
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
            float delta_x = WIDTH / 2f;
            float delta_y = HEIGHT / 2f;

            foreach (Vector v in vb)
            {
                Vector vec = Matrix.InverseMatrix(10f, 10f, 10f) *
                    (Matrix.ProjectionMatrix(800f, 1000f) *
                    new Vector(v.x, v.y, v.z));

                result.Add(
                    Matrix.TranslateMatrix(
                        new Vector(delta_x, delta_y, 0)
                    ) * vec);
            }
                

            return result;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}
