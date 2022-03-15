﻿using System;
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
            x_axis = new AxisX(2);
            y_axis = new AxisY(2);
            z_axis = new AxisZ(2);

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
            cube.Draw(e.Graphics, ViewportTransformation(cube.vertexbuffer));

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

            foreach (Vector v in vb)
            {
                Vector inverse = Matrix.InverseMatrix(-10f, -100f, 10f) * v;
                Vector projection = Matrix.ProjectionMatrix(800f, inverse.z) * inverse;
                // result.Add(Matrix.TranslateMatrix(new Vector(delta_x, delta_y, 0)) * projection);
                result.Add(new Vector(projection.x + WIDTH / 2, -projection.y + HEIGHT / 2, 0));
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
