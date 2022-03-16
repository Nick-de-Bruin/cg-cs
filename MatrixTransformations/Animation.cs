using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public class Animation
    {
        public double phase;

        private readonly Timer timer;
        private readonly Cube cube;

        public Animation(Cube cube)
        {
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Animate;
            this.cube = cube;
        }

        public void Start()
        {
            phase = 1;
            timer.Start();
        }

        public void Stop() => timer.Stop();

        private void Animate(Object myObject, EventArgs myEventArgs)
        {
            switch (phase)
            {
                case 1:
                    cube.scale += 1f;
                    cube.scaleMatrix *= Matrix.ScaleMatrix(cube.scale);
                    cube.theta -= 1;
                    if (cube.scale == 150f) phase = 1.5;
                    break;
                case 1.5:
                    cube.scale -= 1f;
                    cube.theta -= 1;
                    cube.scaleMatrix *= Matrix.ScaleMatrix(cube.scale);
                    if (cube.scale == 100f) phase = 2;
                    break;
                case 2:
                    cube.rotateX += 1;
                    cube.theta -= 1;
                    cube.rotationMatrix *= Matrix.RotateMatrixX(cube.rotateX);
                    if(cube.rotateX == 45) phase = 2.5;
                    break;
                case 2.5:
                    cube.rotateX -= 1;
                    cube.theta -= 1;
                    cube.rotationMatrix *= Matrix.RotateMatrixX(cube.rotateX);
                    if( cube.rotateX == 0) { phase = 3; }
                    break;
                case 3:
                    cube.rotateY += 1;
                    cube.phi += 1;
                    cube.rotationMatrix *= Matrix.RotateMatrixX(cube.rotateY);
                    if (cube.rotateY == 45) phase = 3.5;
                    break;
                case 3.5:
                    cube.rotateY -= 1;
                    cube.phi += 1;
                    cube.rotationMatrix *= Matrix.RotateMatrixX(cube.rotateY);
                    if (cube.rotateY == 0) phase = 4;
                    break;
                case 4:
                    if(cube.phi > 10f) { cube.phi -= 1; }
                    if(cube.theta < 10f) { cube.theta += 1; }
                    if (cube.phi == 10f && cube.theta == 10f) { phase = 1; }
                    break;

            }

            if(Form.ActiveForm != null) { Form.ActiveForm.Invalidate(); }
        }
    }
}
