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
        private readonly Shape shape;

        public Animation(Shape shape)
        {
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Animate;
            this.shape = shape;
        }

        public void Start()
        {
            shape.Reset();
            phase = 1;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            phase = 0;
            shape.Reset();
        }

        public void PausePlay()
        {
            if (timer.Enabled) { timer.Stop(); }
            else if(!timer.Enabled && phase == 0) { Start(); }
            else { timer.Start(); }
        }

        private void Animate(Object myObject, EventArgs myEventArgs)
        {
            switch (phase)
            {
                //Phase 1: Scale until 1.5x and shrink with a stepsize of 0.01.
                //Also decrease theta
                case 1:
                    shape.scale += .01f;
                    shape.theta -= 1;
                    shape.scaleMatrix *= Matrix.ScaleMatrix(shape.scale);
                    if (shape.scale > 1.49f) phase = 1.5;
                    break;
                case 1.5:
                    shape.scale -= .01f;
                    shape.theta -= 1;
                    shape.scaleMatrix *= Matrix.ScaleMatrix(shape.scale);
                    if (shape.scale == 1f) phase = 2;
                    break;
                //Phase 2: Rotate 45 degrees over X-axis and back
                //Also decrease theta
                case 2:
                    shape.rotateX += 1;
                    shape.theta -= 1;
                    shape.rotationMatrix *= Matrix.RotateMatrixX(shape.rotateX);
                    if(shape.rotateX == 45) phase = 2.5;
                    break;
                case 2.5:
                    shape.rotateX -= 1;
                    shape.theta -= 1;
                    shape.rotationMatrix *= Matrix.RotateMatrixX(shape.rotateX);
                    if( shape.rotateX == 0) { phase = 3; }
                    break;
                //Phase 3: Rotate 45 degrees over Y-axis and back
                //Also increase Phi
                case 3:
                    shape.rotateY += 1;
                    shape.phi += 1;
                    shape.rotationMatrix *= Matrix.RotateMatrixX(shape.rotateY);
                    if (shape.rotateY == 45) phase = 3.5;
                    break;
                case 3.5:
                    shape.rotateY -= 1;
                    shape.phi += 1;
                    shape.rotationMatrix *= Matrix.RotateMatrixX(shape.rotateY);
                    if (shape.rotateY == 0) phase = 4;
                    break;
                //Phase 4: Increase theta and decrease phi until starting values
                //Also start again with phase 1
                case 4:
                    if(shape.phi > -10f) { shape.phi -= 1; }
                    if(shape.theta < -100f) { shape.theta += 1; }
                    if (shape.phi == -10f && shape.theta == -100f) { phase = 1; }
                    break;

            }

            if(Form.ActiveForm != null) { Form.ActiveForm.Invalidate(); }
        }
    }
}
