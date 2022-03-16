using System.Collections.Generic;
using System.Drawing;

namespace MatrixTransformations
{
    public class Cube
    {
        //Attributes
        public float scale = 100f; //1f
        public float d = 10f; //800f
        public float r = 800f; //10f
        public float theta = 10f; //-100f
        public float phi = 10f; //-10f

        //Rotations and translations
        public int rotateX = 0;
        public int rotateY = 0;
        public int rotateZ = 0;

        public int translateX = 0;
        public int translateY = 0;
        public int translateZ = 0;

        //Matrices
        public Matrix rotationMatrix = Matrix.Identity();
        public Matrix translationMatrix = Matrix.Identity();
        public Matrix scaleMatrix = Matrix.Identity();

        //          7----------4
        //         /|         /|
        //        / |        / |                y
        //       /  6-------/--5                |
        //      3----------0  /                 ----x
        //      | /        | /                 /
        //      |/         |/                  z
        //      2----------1

        public List<Vector> vertexbuffer = new List<Vector>
        {
            new Vector( 1.0f,  1.0f, 1.0f),     //0
            new Vector( 1.0f, -1.0f, 1.0f),     //1
            new Vector(-1.0f, -1.0f, 1.0f),     //2
            new Vector(-1.0f,  1.0f, 1.0f),     //3

            new Vector( 1.0f,  1.0f, -1.0f),    //4
            new Vector( 1.0f, -1.0f, -1.0f),    //5
            new Vector(-1.0f, -1.0f, -1.0f),    //6
            new Vector(-1.0f,  1.0f, -1.0f),    //7

            new Vector( 1.2f,  1.2f, 1.2f),     //0
            new Vector( 1.2f, -1.2f, 1.2f),     //1
            new Vector(-1.2f, -1.2f, 1.2f),     //2
            new Vector(-1.2f,  1.2f, 1.2f),     //3

            new Vector( 1.2f,  1.2f, -1.2f),    //4
            new Vector( 1.2f, -1.2f, -1.2f),    //5
            new Vector(-1.2f, -1.2f, -1.2f),    //6
            new Vector(-1.2f,  1.2f, -1.2f)     //7
        };

        private readonly Color color;

        public Cube(Color c) => color = c;

        public void Draw(Graphics g, List<Vector> vb)
        {
            Pen pen = new Pen(color, 2f);
            Brush brush = new SolidBrush(Color.Black);

            g.DrawLine(pen, vb[0].x, vb[0].y, vb[1].x, vb[1].y);    //0 -> 1
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[2].x, vb[2].y);    //1 -> 2
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[3].x, vb[3].y);    //2 -> 3
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[0].x, vb[0].y);    //3 -> 0

            g.DrawLine(pen, vb[4].x, vb[4].y, vb[5].x, vb[5].y);    //4 -> 5
            g.DrawLine(pen, vb[5].x, vb[5].y, vb[6].x, vb[6].y);    //5 -> 6
            g.DrawLine(pen, vb[6].x, vb[6].y, vb[7].x, vb[7].y);    //6 -> 7
            g.DrawLine(pen, vb[7].x, vb[7].y, vb[4].x, vb[4].y);    //7 -> 4

            g.DrawLine(pen, vb[0].x, vb[0].y, vb[4].x, vb[4].y);    //0 -> 4
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[5].x, vb[5].y);    //1 -> 5
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[6].x, vb[6].y);    //2 -> 6
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[7].x, vb[7].y);    //3 -> 7

            Font font = new Font("Arial", 12, FontStyle.Bold);
            for (int i = 0; i < 8; i++)
            {
                PointF p = new PointF(vb[i + 8].x, vb[i + 8].y);
                g.DrawString(i.ToString(), font, brush, p);
            }
        }

        public void Reset()
        {
            //Attributes
            scale = 100f; //1f
            d = 10f; //800f
            r = 800f; //10f
            theta = 10f; //-100f
            phi = 10f; //-10f

            //Rotations and translations
            rotateX = 0;
            rotateY = 0;
            rotateZ = 0;

            translateX = 0;
            translateY = 0;
            translateZ = 0;

            //Matrices
            rotationMatrix = Matrix.Identity();
            translationMatrix = Matrix.Identity();
            scaleMatrix = Matrix.Identity();
        }

    } 
}
