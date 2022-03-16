using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    public abstract class Shape
    {
        //Atributes
        public float scale = 1f;
        public float d = 800f;
        public float r = 10f;
        public float theta = -100f;
        public float phi = -10f;

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

        public abstract List<Vector> vertexbuffer { get; }

        public readonly Color color;

        public Shape(Color c)
        {
            color = c;
        }

        public abstract void Draw(Graphics g, List<Vector> vb);

        public void Reset()
        {
            //Attributes
            scale = 1f;
            d = 800f;
            r = 10f;
            theta = -100f;
            phi = -10f;

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
