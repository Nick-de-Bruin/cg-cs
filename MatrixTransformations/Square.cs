﻿using System.Collections.Generic;
using System.Drawing;

namespace MatrixTransformations
{
    public class Square
    {
        private readonly Color color;
        private readonly float weight;

        public List<Vector> vertexbuffer;

        public Square(Color color, int size = 100, float weight = 3)
        {
            this.color = color;
            this.weight = weight;

            vertexbuffer = new List<Vector>
            {
                new Vector(-size, -size, 0),
                new Vector(size, -size, 0),
                new Vector(size, size, 0),
                new Vector(-size, size, 0)
            };
        }

        public void Draw(Graphics g, List<Vector> vb)
        {
            Pen pen = new Pen(color, weight);
            g.DrawLine(pen, vb[0].x, vb[0].y, vb[1].x, vb[1].y);
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[2].x, vb[2].y);
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[3].x, vb[3].y);
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[0].x, vb[0].y);
        }
    }
}
