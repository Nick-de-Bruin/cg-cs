using System;
using System.Text;

namespace MatrixTransformations
{
    public class Matrix
    {
        public readonly float[,] mat = new float[4, 4];

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

        private Matrix(float m11, float m12, float m13, float m14,
                       float m21, float m22, float m23, float m24,
                       float m31, float m32, float m33, float m34,
                       float m41, float m42, float m43, float m44)
            : this (m11, m12, m13,
                    m21, m22, m23,
                    m31, m32, m33)
        {
            mat[0, 3] = m14;
            mat[1, 3] = m24;
            mat[2, 3] = m34;

            mat[3, 0] = m41;
            mat[3, 1] = m42;
            mat[3, 2] = m43;
            mat[3, 3] = m44;
        }

        public Matrix(Vector vec) :
            this(vec.x, 0, 0, 0,
                 vec.y, 0, 0, 0,
                 vec.z, 0, 0, 0,
                 vec.w, 0, 0, 1) { }

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

            for (int r = 0; r < m1.mat.GetLength(0) - 1; r++)
                for (int c = 0; c < m1.mat.GetLength(1) - 1; c++)
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

        public static Matrix RotateMatrixZ(float theta)
        {
            float rad = theta * ((float)Math.PI / 180);
            float cr = (float)Math.Cos(rad);
            float sr = (float)Math.Sin(rad);

            return new Matrix (cr, -sr, 0,
                               sr, cr,  0,
                               0,  0,   1);
        }

        public static Matrix RotateMatrixX(float theta)
        {
            float rad = theta * ((float)Math.PI / 180);
            float cr = (float)Math.Cos(rad);
            float sr = (float)Math.Sin(rad);

            return new Matrix(1, 0,  0,
                              0, cr, -sr,
                              0, sr, cr);
        }

        public static Matrix TranslateMatrix(Vector vec)
            => new Matrix(1, 0, 0, vec.x,
                          0, 1, 0, vec.y,
                          0, 0, 1, vec.z,
                          0, 0, 0, 1);

        public static Matrix InverseMatrix(float phi, float theta, float distance)
        {
            float t = theta * ((float)Math.PI / 180);
            float p = phi * ((float)Math.PI / 180);

            float st = (float)Math.Sin(t);
            float ct = (float)Math.Cos(t);
            float sp = (float)Math.Sin(p);
            float cp = (float)Math.Cos(p);

            return new Matrix(
                -st,        ct,         0,  0,
                -(ct * cp), -(cp * st), sp, 0,
                (ct * sp),  (st * sp),  cp, -distance,
                0,          0,          0,  1
            );
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
            StringBuilder s = new StringBuilder();
            for (int r = 0; r < mat.GetLength(0); r++)
            {
                if (r == 0)
                    s.Append("/");
                else if (r == mat.GetLength(0) - 1)
                    s.Append("\\");
                else
                    s.Append("|");

                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    if (c != mat.GetLength(1) - 1)
                        s.Append(mat[r, c] + ", ");
                    else
                        s.Append(mat[r, c]);
                }

                if (r == 0)
                    s.Append("\\");
                else if (r == mat.GetLength(0) - 1)
                    s.Append("/");
                else
                    s.Append("|");
                s.Append("\n");
            }

            return s.ToString();
        }
    }
}
