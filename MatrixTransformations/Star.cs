using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    internal class Star : Shape
    {
        private List<Vector> _vertexbuffer = new List<Vector>() 
        { 
            new Vector(1, 1, 0), // 0
            new Vector(1, -1, 0), // 1
            new Vector(-1, -1, 0), // 2
            new Vector(-1, 1, 0), // 3

            new Vector(0.5f, 0, 0), // 4
            new Vector(0, -0.5f, 0), // 5
            new Vector(-0.5f, 0, 0), // 6
            new Vector(0, 0.5f, 0), // 7

            new Vector(0, 0, -0.5f), // 8
            new Vector(0, 0, 0.5f), // 9
        };

        public override List<Vector> vertexbuffer => _vertexbuffer;

        public Star(Color c) : base(c) { }

        public override void Draw(Graphics g, List<Vector> vb)
        {
            Pen pen = new Pen(color, 2f);
            Brush brush = new SolidBrush(Color.Black);
            Font font = new Font("Arial", 12, FontStyle.Bold);


            // All outer to inner righthand
            g.DrawLine(pen, vb[0].x, vb[0].y, vb[4].x, vb[4].y);
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[5].x, vb[5].y);
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[6].x, vb[6].y);
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[7].x, vb[7].y);

            // All outer to inner lefthand
            g.DrawLine(pen, vb[0].x, vb[0].y, vb[7].x, vb[7].y);
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[4].x, vb[4].y);
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[5].x, vb[5].y);
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[6].x, vb[6].y);

            // All outer to front
            g.DrawLine(pen, vb[0].x, vb[0].y, vb[8].x, vb[8].y);
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[8].x, vb[8].y);
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[8].x, vb[8].y);
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[8].x, vb[8].y);

            // All outer to back
            g.DrawLine(pen, vb[0].x, vb[0].y, vb[9].x, vb[9].y);
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[9].x, vb[9].y);
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[9].x, vb[9].y);
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[9].x, vb[9].y);

            // All inner to front
            g.DrawLine(pen, vb[4].x, vb[4].y, vb[8].x, vb[8].y);
            g.DrawLine(pen, vb[5].x, vb[5].y, vb[8].x, vb[8].y);
            g.DrawLine(pen, vb[6].x, vb[6].y, vb[8].x, vb[8].y);
            g.DrawLine(pen, vb[7].x, vb[7].y, vb[8].x, vb[8].y);

            // All inner to back
            g.DrawLine(pen, vb[4].x, vb[4].y, vb[9].x, vb[9].y);
            g.DrawLine(pen, vb[5].x, vb[5].y, vb[9].x, vb[9].y);
            g.DrawLine(pen, vb[6].x, vb[6].y, vb[9].x, vb[9].y);
            g.DrawLine(pen, vb[7].x, vb[7].y, vb[9].x, vb[9].y);

            for (int i = 0; i < vb.Count; i++)
            {
                PointF p = new PointF(vb[i].x, vb[i].y);
                g.DrawString(i.ToString(), font, brush, p);
            }
        }
    }
}
