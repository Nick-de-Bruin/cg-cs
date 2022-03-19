using System.Collections.Generic;
using System.Drawing;

namespace MatrixTransformations
{
    /// <summary>
    /// Represents a three-dimensional axis
    /// </summary>
    public abstract class Axis
    {
        public List<Vector> vertexbuffer;
        public Brush brush;
        public Color color;
        public string axis;

        private protected Axis()
        {
            vertexbuffer = new List<Vector>()
            {
                new Vector(0, 0, 0)
            };
        }

        /// <summary>
        /// Draws the axis
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        /// <param name="vb">The vertexbuffer to draw</param>
        public void Draw(Graphics g, List<Vector> vb)
        {
            Pen pen = new Pen(color, 2f);
            g.DrawLine(pen, vb[0].x, vb[0].y, vb[1].x, vb[1].y);
            Font font = new Font("Arial", 10);
            PointF p = new PointF(vb[1].x, vb[1].y);
            g.DrawString(axis, font, brush, p);
        }
    }

    /// <summary>
    /// Represents the x axis
    /// </summary>
    public class AxisX : Axis
    {
        /// <summary>
        /// Creates an axis on the x with size as its length
        /// </summary>
        /// <param name="size">The length of the axis</param>
        public AxisX(int size=100) : base()
        {
            vertexbuffer.Add(new Vector(size, 0, 0));
            brush = Brushes.Red;
            color = Color.Red;
            axis = "x";
        }
    }

    /// <summary>
    /// Represents the y axis
    /// </summary>
    public class AxisY : Axis
    {
        /// <summary>
        /// Creates an axis on the y with size as its length
        /// </summary>
        /// <param name="size">The length of the axis</param>
        public AxisY(int size = 100) : base()
        {
            vertexbuffer.Add(new Vector(0, size, 0));
            brush = Brushes.Green;
            color = Color.Green;
            axis = "y";
        }
    }

    /// <summary>
    /// Represents the z axis
    /// </summary>
    public class AxisZ : Axis
    {
        /// <summary>
        /// Creates an axis on the z with size as its length
        /// </summary>
        /// <param name="size">The length of the axis</param>
        public AxisZ(int size = 100) : base()
        {
            vertexbuffer.Add(new Vector(0, 0, size));
            brush = Brushes.Blue;
            color = Color.Blue;
            axis = "z";
        }
    }
}
