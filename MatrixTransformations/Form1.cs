using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        // Axes
        private readonly Axis x_axis;
        private readonly Axis y_axis;
        private readonly Axis z_axis;

        // Object
        private Shape shape;

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        //Animation
        private Animation animation;

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

            // Create object
            shape = new Cube(Color.Blue);

            // Create animator
            animation = new Animation(shape);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Write keypress events
            e.Graphics.DrawString("KEYPRESS EVENTS \n\n" +
                "Scale             " + shape.scale + "           s/S\n" +
                "RotateX          " + shape.rotateX +  "           x/X\n" +
                "RotateY          " + shape.rotateY + "           y/Y\n" +
                "RotateZ          " + shape.rotateZ + "           z/Z\n" +
                "TranslateX       " + shape.translateX + "          Left/Right\n" +
                "TranslateY       " + shape.translateY + "          Up/Down\n" +
                "TranslateZ       " + shape.translateZ +"          PgUp/PgDn\n" +
                "Change Shape                 w\n\n" +
                "Reset                          C\n" +
                "Animate                       A\n" +
                "Pause/Play                  Space\n\n" +
                "Phi                " + shape.phi + "          p/P\n" +
                "Theta             " + shape.theta + "        t/T\n" +
                "d                   " + shape.d + "         d/D\n" +
                "r                    " + shape.r +"          r/R\n\n" +
                "Phase             " + animation.phase, new Font("Arial", 10), Brushes.Black, 0, 0); 


            // Draw axes
            x_axis.Draw(e.Graphics, ViewportTransformation(x_axis.vertexbuffer));
            y_axis.Draw(e.Graphics, ViewportTransformation(y_axis.vertexbuffer));
            z_axis.Draw(e.Graphics, ViewportTransformation(z_axis.vertexbuffer));

            shape.scaleMatrix = Matrix.ScaleMatrix(shape.scale);

            //Make sure transformations are done in the correct order
            Matrix transformation = shape.translationMatrix * shape.rotationMatrix * shape.scaleMatrix;
            // Draw cube 
            shape.Draw(e.Graphics, ViewportTransformation(Transformation(shape.vertexbuffer, transformation)));
        }

        public static List<Vector> Transformation(List<Vector> vb, Matrix matrix)
        {
            List<Vector> result = new List<Vector>();

            foreach (Vector v in vb)
                result.Add(matrix * v);

            return result;
        }

        public List<Vector> ViewportTransformation(List<Vector> vb)
        {
            List<Vector> result = new List<Vector>();

            foreach (Vector v in vb)
            {
                Vector inverse = Matrix.InverseMatrix(shape.phi, shape.theta, shape.r) * v;
                Vector projection = Matrix.ProjectionMatrix(shape.d, inverse.z) * inverse;
                result.Add(new Vector(projection.x + WIDTH / 2, -projection.y + HEIGHT / 2, 0));
            }

            return result;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int mod = e.Modifiers == Keys.Shift ? -1 : 1;
            switch (e.KeyCode)
            {
                case Keys.Escape: Application.Exit();
                    break;
                case Keys.S:
                    shape.scale += mod * .01f;
                    shape.scaleMatrix *= Matrix.ScaleMatrix(mod);
                    break;
                case Keys.X:
                    shape.rotateX += mod;
                    shape.rotationMatrix *= Matrix.RotateMatrixX(mod);
                    break;
                case Keys.Y:
                    shape.rotateY += mod;
                    shape.rotationMatrix *= Matrix.RotateMatrixY(mod);
                    break;
                case Keys.Z:
                    shape.rotateZ += mod;
                    shape.rotationMatrix *= Matrix.RotateMatrixZ(mod);
                    break;
                case Keys.Left:
                    shape.translateX -= 1;
                    shape.translationMatrix *= Matrix.TranslateMatrix(new Vector(-1, 0, 0));
                    break;
                case Keys.Right:
                    shape.translateX += 1;
                    shape.translationMatrix *= Matrix.TranslateMatrix(new Vector(1, 0, 0));
                    break;
                case Keys.Up:
                    shape.translateY += 1;
                    shape.translationMatrix *= Matrix.TranslateMatrix(new Vector(0, 1, 0));
                    break;
                case Keys.Down:
                    shape.translateY -= 1;
                    shape.translationMatrix *= Matrix.TranslateMatrix(new Vector(0, -1, 0));
                    break;
                case Keys.PageUp:
                    shape.translateZ += 1;
                    shape.translationMatrix *= Matrix.TranslateMatrix(new Vector(0, 0, 1));
                    break;
                case Keys.PageDown:
                    shape.translateZ -= 1;
                    shape.translationMatrix *= Matrix.TranslateMatrix(new Vector(0, 0, -1));
                    break;
                case Keys.R: 
                    shape.r += mod;
                    break;
                case Keys.D: 
                    shape.d += mod;
                    break;
                case Keys.P: 
                    shape.phi += mod;
                    break;
                case Keys.T: 
                    shape.theta += mod;
                    break;
                case Keys.W:
                    if (shape is Star)
                        shape = new Cube(Color.Blue);
                    else 
                        shape = new Star(Color.Blue);
                    animation = new Animation(shape);
                    break;
                case Keys.C: 
                    animation.Stop();
                    break;
                case Keys.A: 
                    animation.Start();
                    break;
                case Keys.Space: 
                    animation.PausePlay();
                    break;
            }

            Invalidate();
        }
    }
}
