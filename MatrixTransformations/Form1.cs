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

        // Object
        private readonly Cube cube;

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        //Animation
        private readonly Animation animation;

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

            // Create object
            cube = new Cube(Color.Blue);

            // Create animator
            animation = new Animation(cube);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Write keypress events
            e.Graphics.DrawString("KEYPRESS EVENTS \n\n" +
                "Scale             " + cube.scale + "        s/S\n" +
                "RotateX          " + cube.rotateX +  "           x/X\n" +
                "RotateY          " + cube.rotateY + "           y/Y\n" +
                "RotateZ          " + cube.rotateZ + "           z/Z\n" +
                "TranslateX       " + cube.translateX + "          Left/Right\n" +
                "TranslateY       " + cube.translateY + "          Up/Down\n" +
                "TranslateZ       " + cube.translateZ +"          PgUp/PgDn\n\n" +
                "Reset                          C\n" +
                "Animate                       A\n\n" +
                "Phi                " + cube.phi + "          p/P\n" +
                "Theta             " + cube.theta + "          t/T\n" +
                "d                   " + cube.d + "          d/D\n" +
                "r                    " + cube.r +"        r/R\n\n" +
                "Phase             " + animation.phase, new Font("Arial", 10), Brushes.Black, 0, 0); 


            // Draw axes
            x_axis.Draw(e.Graphics, ViewportTransformation(x_axis.vertexbuffer));
            y_axis.Draw(e.Graphics, ViewportTransformation(y_axis.vertexbuffer));
            z_axis.Draw(e.Graphics, ViewportTransformation(z_axis.vertexbuffer));

            cube.scaleMatrix = Matrix.ScaleMatrix(cube.scale);

            //Make sure transformations are done in the correct order
            Matrix transformation = cube.translationMatrix * cube.rotationMatrix * cube.scaleMatrix;
            // Draw cube 
            cube.Draw(e.Graphics, ViewportTransformation(Transformation(cube.vertexbuffer, transformation)));
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
            float delta_x = WIDTH / 2f;
            float delta_y = HEIGHT / 2f;

            foreach (Vector v in vb)
            {
                Vector vec = Matrix.InverseMatrix(cube.phi, cube.theta, cube.d) *
                    (Matrix.ProjectionMatrix(cube.r, 1000f) *
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
            int mod = e.Modifiers == Keys.Shift ? -1 : 1;
            switch (e.KeyCode)
            {
                case Keys.Escape: Application.Exit();
                    break;
                case Keys.S:
                    cube.scale += mod; //todo: change to 0.01 when scale = 1 is used. !!Also do this within the animation class!! 
                    cube.scaleMatrix *= Matrix.ScaleMatrix(cube.scale);
                    break;
                case Keys.X:
                    cube.rotateX += mod;
                    cube.rotationMatrix *= Matrix.RotateMatrixX(mod);
                    break;
                case Keys.Y:
                    cube.rotateY += mod;
                    cube.rotationMatrix *= Matrix.RotateMatrixY(mod);
                    break;
                case Keys.Z:
                    cube.rotateZ += mod;
                    cube.rotationMatrix *= Matrix.RotateMatrixZ(mod);
                    break;
                case Keys.Left:
                    cube.translateX -= 1;
                    cube.translationMatrix *= Matrix.TranslateMatrix(new Vector(-1, 0, 0));
                    break;
                case Keys.Right:
                    cube.translateX += 1;
                    cube.translationMatrix *= Matrix.TranslateMatrix(new Vector(1, 0, 0));
                    break;
                case Keys.Up:
                    cube.translateY += 1;
                    cube.translationMatrix *= Matrix.TranslateMatrix(new Vector(0, 1, 0));
                    break;
                case Keys.Down:
                    cube.translateY -= 1;
                    cube.translationMatrix *= Matrix.TranslateMatrix(new Vector(0, -1, 0));
                    break;
                case Keys.PageUp:
                    cube.translateZ += 1;
                    cube.translationMatrix *= Matrix.TranslateMatrix(new Vector(0, 0, 1));
                    break;
                case Keys.PageDown:
                    cube.translateZ -= 1;
                    cube.translationMatrix *= Matrix.TranslateMatrix(new Vector(0, 0, -1));
                    break;
                case Keys.R: cube.r += mod;
                    break;
                case Keys.D: cube.d += mod;
                    break;
                case Keys.P: cube.phi += mod;
                    break;
                case Keys.T: cube.theta += mod;
                    break;
                case Keys.C:
                    animation.Stop();
                    cube.Reset();
                    break;
                case Keys.A:
                    cube.Reset();
                    animation.Start();
                    break;

            }

            Invalidate();
        }
    }
}
