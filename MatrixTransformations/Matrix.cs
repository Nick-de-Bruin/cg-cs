using System;
using System.Text;

namespace MatrixTransformations
{
    public class Matrix
    {
        private readonly float[,] mat = new float[4, 4];

        public Matrix() => 
            mat[mat.GetLength(0) - 1, mat.GetLength(1) - 1] = 1f;

        public Matrix(float m11, float m12, float m13,
                      float m21, float m22, float m23,
                      float m31, float m32, float m33) 
            : this()
        {
            mat[0, 0] = m11; mat[0, 1] = m12; mat[0, 2] = m13;
            mat[1, 0] = m21; mat[1, 1] = m22; mat[1, 2] = m23;
            mat[2, 0] = m31; mat[2, 1] = m32; mat[2, 2] = m33;
        }

        public Matrix(Vector vec) : 
            this(vec.x, 0, 0,
                 vec.y, 0, 0,
                 vec.z, 0, 0) => 
            mat[mat.GetLength(1) - 1, 0] = vec.w; 

        public Vector ToVector() => 
            new Vector(mat[0, 0], mat[1, 0], mat[2, 0]);

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            Matrix m = new Matrix();

            for (int r = 0; r < m1.mat.GetLength(0) - 1; r++)
                for (int c = 0; c < m1.mat.GetLength(1) - 1; c++)
                    m.mat[r, c] = m1.mat[r, c] + m2.mat[r, c];

            return m;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            Matrix m = new Matrix();

            for (int r = 0; r < m1.mat.GetLength(0) - 1; r++)
                for (int c = 0; c < m1.mat.GetLength(1) - 1; c++)
                    m.mat[r, c] = m1.mat[r, c] - m2.mat[r, c];

            return m;
        }

        public static Matrix operator *(float f, Matrix m1) => m1 * f;
        public static Matrix operator *(Matrix m1, float f)
        {
            Matrix m = new Matrix();

            for (int r = 0; r < m1.mat.GetLength(0); r++)
                for (int c = 0; c < m1.mat.GetLength(1); c++)
                    m.mat[r, c] = m1.mat[r, c] * f;

            return m;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix m = new Matrix();

            for (int r = 0; r < m1.mat.GetLength(1); r++)
                for (int c = 0; c < m2.mat.GetLength(0); c++)
                {
                    float total = 0;

                    for (int i = 0; i < m1.mat.GetLength(1); i++)
                        total += m1.mat[r, i] * m2.mat[i, c];

                    m.mat[r, c] = total;
                }

            return m;
        }

        public static Vector operator *(Matrix m1, Vector v) =>
            (m1 * new Matrix(v)).ToVector();

        public static Matrix Identity() => 
            new Matrix(1, 0, 0,
                       0, 1, 0,
                       0, 0, 1);

        public static Matrix ScaleMatrix(float s) =>
            new Matrix(s, 0, 0, 
                       0, s, 0,
                       0, 0, s);

        public static Matrix RotateMatrix(float deg)
        {
            float rad = deg * ((float)Math.PI / 180);

            Matrix res = Identity();
            res.mat[0, 0] = (float)Math.Cos(rad);
            res.mat[0, 1] = (float)-Math.Sin(rad);
            res.mat[1, 0] = (float)Math.Sin(rad);
            res.mat[1, 1] = (float)Math.Cos(rad);

            return res;
        }

        public static Matrix TranslateMatrix(Vector vec)
        {
            Matrix m = Identity();
            m.mat[0, m.mat.GetLength(1) - 1] = vec.x;
            m.mat[1, m.mat.GetLength(1) - 1] = vec.y;
            m.mat[2, m.mat.GetLength(1) - 1] = vec.z;
            return m;
        }

        public static Matrix InverseMatrix(float phi, float theta, float distance)
        {
            Matrix m = new Matrix(
                (float)-Math.Sin(theta), (float)Math.Cos(theta), 0,
                (float)-(Math.Cos(theta) * Math.Cos(phi)), (float)-(Math.Cos(phi) * Math.Sin(theta)), (float)Math.Sin(phi),
                (float)(Math.Cos(theta) * Math.Sin(phi)), (float)(Math.Sin(theta) * Math.Sin(phi)), (float)Math.Cos(phi)
            );

            m.mat[2, 3] = -distance;

            return m;
        }

        public static Matrix ProjectionMatrix(float distance, float vecz)
        {
            float p = -(distance / vecz);
            return new Matrix(p, 0, 0,
                              0, p, 0,
                              0, 0, 1);
        }
            

        public override string ToString()
        {
            string s = "";
            for (int r = 0; r < mat.GetLength(0); r++)
            {
                if (r == 0)
                    s += "/";
                else if (r == mat.GetLength(0) - 1)
                    s += "\\";
                else
                    s += "|";

                for (int c = 0; c < mat.GetLength(1); c++)
                    s += mat[r, c] + ", ";

                if (r == 0)
                    s += "\\";
                else if (r == mat.GetLength(0) - 1)
                    s += "/";
                else
                    s += "|";
                s += "\n";
            }

            return s;
        }
    }
}
