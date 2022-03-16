using System;
using System.Text;

namespace MatrixTransformations
{
    /// <summary>
    /// Represents a 4x4 matrix
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// A two-dimensional 4x4 float array
        /// </summary>
        public readonly float[,] mat = new float[4, 4];

        /// <summary>
        /// Creates an empty array
        /// </summary>
        public Matrix() => 
            mat[mat.GetLength(0) - 1, mat.GetLength(1) - 1] = 1f;

        /// <summary>
        /// Creates a matrix with the given variables
        /// </summary>
        /// <param name="m11">Matrix position 0, 0</param>
        /// <param name="m12">Matrix position 0, 1</param>
        /// <param name="m13">Matrix position 0, 2</param>
        /// <param name="m21">Matrix position 1, 0</param>
        /// <param name="m22">Matrix position 1, 1</param>
        /// <param name="m23">Matrix position 1, 2</param>
        /// <param name="m31">Matrix position 2, 0</param>
        /// <param name="m32">Matrix position 2, 1</param>
        /// <param name="m33">Matrix position 2, 2</param>
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

        /// <summary>
        /// Creates a matrix representation of a vector at left hand side of the matrix
        /// </summary>
        /// <param name="vec">The vector to be represented as a matrix</param>
        public Matrix(Vector vec) :
            this(vec.x, 0, 0, 0,
                 vec.y, 0, 0, 0,
                 vec.z, 0, 0, 0,
                 vec.w, 0, 0, 1) { }

        /// <summary>
        /// Converts the left hand side of a matrix to a vector
        /// </summary>
        /// <returns>A vector from a matrix</returns>
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

        /// <summary>
        /// Creates an identity matrix
        /// </summary>
        /// <returns>An identity matrix</returns>
        public static Matrix Identity() => 
            new Matrix(1, 0, 0,
                       0, 1, 0,
                       0, 0, 1);

        /// <summary>
        /// Creates a scale matrix
        /// </summary>
        /// <param name="s">The scale the matrix should hold</param>
        /// <returns>A scale matrix with the given scale</returns>
        public static Matrix ScaleMatrix(float s) =>
            new Matrix(s, 0, 0,
                       0, s, 0,
                       0, 0, s);

        /// <summary>
        /// Creates a rotation matrix over the z axis
        /// </summary>
        /// <param name="theta">The theta the matrix should represent</param>
        /// <returns>A rotation matrix given the theta</returns>
        public static Matrix RotateMatrixZ(float theta)
        {
            float rad = theta * ((float)Math.PI / 180);
            float cr = (float)Math.Cos(rad);
            float sr = (float)Math.Sin(rad);

            return new Matrix (cr, -sr, 0,
                               sr, cr,  0,
                               0,  0,   1);
        }

        /// <summary>
        /// Creates a rotation matrix over the z axis
        /// </summary>
        /// <param name="theta">The theta the matrix should represent</param>
        /// <returns>A rotation matrix given the theta</returns>
        public static Matrix RotateMatrixX(float theta)
        {
            float rad = theta * ((float)Math.PI / 180);
            float cr = (float)Math.Cos(rad);
            float sr = (float)Math.Sin(rad);

            return new Matrix(1, 0,  0,
                              0, cr, -sr,
                              0, sr, cr);
        }

        public static Matrix RotateMatrixY(float theta)
        {
            float rad = theta * ((float)Math.PI / 180);

            Matrix res = Identity();
            res.mat[0, 0] = (float)Math.Cos(rad);
            res.mat[0, 2] = (float)-Math.Sin(rad);
            res.mat[2, 0] = (float)Math.Sin(rad);
            res.mat[2, 2] = (float)Math.Cos(rad);

            return res;
        }

        public static Matrix TranslateMatrix(Vector vec)
            => new Matrix(1, 0, 0, vec.x,
                          0, 1, 0, vec.y,
                          0, 0, 1, vec.z,
                          0, 0, 0, 1);

        /// <summary>
        /// Creates an invese matrix given a phi, theta, and distance (r)
        /// </summary>
        /// <param name="phi">Represents phi in radians</param>
        /// <param name="theta">Represents the theta in radians</param>
        /// <param name="distance">Represents the distance (r)</param>
        /// <returns>An inverse matrix given phi, theta, and the distance (r)</returns>
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

        /// <summary>
        /// Creates a projection matrix given a distance and z vector
        /// </summary>
        /// <param name="distance">Represents the distance (d)</param>
        /// <param name="vecz">Represents the z vector</param>
        /// <returns>A matrix for projection transformation</returns>
        public static Matrix ProjectionMatrix(float distance, float vecz)
        {
            float p = -(distance / vecz);
            return new Matrix(p, 0, 0,
                              0, p, 0,
                              0, 0, 1);
        }

        /// <summary>
        /// Creates a projection matrix given a distance and a vector
        /// </summary>
        /// <param name="distance">Represents the distance (d)</param>
        /// <param name="vec">Represents the vector of which the z will be used</param>
        /// <returns>A matrix for projection transformation</returns>
        public static Matrix ProjectionMatrix(float distance, Vector vec) =>
            ProjectionMatrix(distance, vec.z);

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
