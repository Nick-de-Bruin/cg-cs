using MatrixTransformations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformationsTests
{
    [TestFixture]
    internal class MatrixTests
    {
        [Test]
        public void Identity_New_CreatesIdentity()
        {
            Matrix matrix = Matrix.Identity();

            // 1, 0, 0, 0
            // 0, 1, 0, 0
            // 0, 0, 1, 0
            // 0, 0, 0, 1
            Assert.AreEqual(1, matrix.mat[0, 0]);
            Assert.AreEqual(0, matrix.mat[0, 1]);
            Assert.AreEqual(0, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(0, matrix.mat[1, 0]);
            Assert.AreEqual(1, matrix.mat[1, 1]);
            Assert.AreEqual(0, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(0, matrix.mat[2, 0]);
            Assert.AreEqual(0, matrix.mat[2, 1]);
            Assert.AreEqual(1, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [Test]
        public void Constructor_WithVariables_CreatesMatrix()
        {
            Matrix matrix = new Matrix(1, 2, 3,
                                       4, 5, 6,
                                       7, 8, 9);

            // 1, 2, 3, 0
            // 4, 5, 6, 0
            // 7, 8, 9, 0
            // 0, 0, 0, 1
            Assert.AreEqual(1, matrix.mat[0, 0]);
            Assert.AreEqual(2, matrix.mat[0, 1]);
            Assert.AreEqual(3, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(4, matrix.mat[1, 0]);
            Assert.AreEqual(5, matrix.mat[1, 1]);
            Assert.AreEqual(6, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(7, matrix.mat[2, 0]);
            Assert.AreEqual(8, matrix.mat[2, 1]);
            Assert.AreEqual(9, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [Test]
        public void Constructor_FromVector_InsertsVector()
        {
            Matrix matrix = new Matrix(new Vector(1, 2, 3));

            // 1, 0, 0, 0
            // 2, 0, 0, 0
            // 3, 0, 0, 0
            // 0, 0, 0, 1
            Assert.AreEqual(1, matrix.mat[0, 0]);
            Assert.AreEqual(0, matrix.mat[0, 1]);
            Assert.AreEqual(0, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(2, matrix.mat[1, 0]);
            Assert.AreEqual(0, matrix.mat[1, 1]);
            Assert.AreEqual(0, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(3, matrix.mat[2, 0]);
            Assert.AreEqual(0, matrix.mat[2, 1]);
            Assert.AreEqual(0, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(1, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [TestCase(1, 2, 3)]
        [TestCase(1.1f, 1.1f, 1.1f)]
        [TestCase(768, 454, 21)]
        [TestCase(21.3f, float.MaxValue, float.MinValue)]
        public void ToVector_CreateVectorFromMatrix_CreatesVector(
            float x, float y, float z)
        {
            Vector vec = new Matrix(x, 0, 0,
                                    y, 0, 0,
                                    z, 0, 0).ToVector();

            Assert.AreEqual(x, vec.x);
            Assert.AreEqual(y, vec.y);
            Assert.AreEqual(z, vec.z);
            Assert.AreEqual(1, vec.w);
        }

        [Test]
        public void Adition_AddMatrix_AddsNumberToEach()
        {
            Matrix matrix = new Matrix(1, 2, 3,
                                       4, 5, 6,
                                       7, 8, 9)
                            +
                            new Matrix(1, 2, 3,
                                       4, 5, 6,
                                       7, 8, 9);
            
            // 2,  4,  6,  0
            // 8,  10, 12, 0
            // 14, 16, 18, 0
            // 0,  0,  0,  1
            Assert.AreEqual(2, matrix.mat[0, 0]);
            Assert.AreEqual(4, matrix.mat[0, 1]);
            Assert.AreEqual(6, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(8, matrix.mat[1, 0]);
            Assert.AreEqual(10, matrix.mat[1, 1]);
            Assert.AreEqual(12, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(14, matrix.mat[2, 0]);
            Assert.AreEqual(16, matrix.mat[2, 1]);
            Assert.AreEqual(18, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [Test]
        public void Subtraction_SubtractMatrix_RemovesNumFromEach()
        {
            Matrix matrix = new Matrix(1, 2, 3,
                                       4, 5, 6,
                                       7, 8, 9)
                            -
                            new Matrix(1, 1, 1,
                                       1, 1, 1,
                                       1, 1, 1);

            // 0, 1, 2, 0
            // 3, 4, 5, 0
            // 6, 7, 8, 0
            // 0, 0, 0, 1
            Assert.AreEqual(0, matrix.mat[0, 0]);
            Assert.AreEqual(1, matrix.mat[0, 1]);
            Assert.AreEqual(2, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(3, matrix.mat[1, 0]);
            Assert.AreEqual(4, matrix.mat[1, 1]);
            Assert.AreEqual(5, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(6, matrix.mat[2, 0]);
            Assert.AreEqual(7, matrix.mat[2, 1]);
            Assert.AreEqual(8, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [Test]
        public void Multiplication_MultiplyFloat_MultipliesEach()
        {
            Matrix matrix = new Matrix(1, 2, 3,
                                       4, 5, 6,
                                       7, 8, 9) * 2;

            // 2,  4,  6,  0
            // 8,  10, 12, 0
            // 14, 16, 18, 0
            // 0,  0,  0,  1
            Assert.AreEqual(2, matrix.mat[0, 0]);
            Assert.AreEqual(4, matrix.mat[0, 1]);
            Assert.AreEqual(6, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(8, matrix.mat[1, 0]);
            Assert.AreEqual(10, matrix.mat[1, 1]);
            Assert.AreEqual(12, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(14, matrix.mat[2, 0]);
            Assert.AreEqual(16, matrix.mat[2, 1]);
            Assert.AreEqual(18, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [Test]
        public void Multiplication_MultiplyFloatByMatrix_MultipliesEach()
        {
            Matrix matrix = 2 * new Matrix(1, 2, 3,
                                           4, 5, 6,
                                           7, 8, 9);

            // 2,  4,  6,  0
            // 8,  10, 12, 0
            // 14, 16, 18, 0
            // 0,  0,  0,  1
            Assert.AreEqual(2, matrix.mat[0, 0]);
            Assert.AreEqual(4, matrix.mat[0, 1]);
            Assert.AreEqual(6, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(8, matrix.mat[1, 0]);
            Assert.AreEqual(10, matrix.mat[1, 1]);
            Assert.AreEqual(12, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(14, matrix.mat[2, 0]);
            Assert.AreEqual(16, matrix.mat[2, 1]);
            Assert.AreEqual(18, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [Test]
        public void Multiplication_MultiplyMatrix_MultipliesEach()
        {
            Matrix matrix = new Matrix(1, 2, 3,
                                       4, 5, 6,
                                       7, 8, 9)
                            *
                            new Matrix(9, 8, 7,
                                       6, 5, 4,
                                       3, 2, 1);

            // 30,  24,  18, 0
            // 84,  69,  54, 0
            // 138, 114, 90, 0
            // 0,   0,   0,  1
            Assert.AreEqual(30, matrix.mat[0, 0]);
            Assert.AreEqual(24, matrix.mat[0, 1]);
            Assert.AreEqual(18, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(84, matrix.mat[1, 0]);
            Assert.AreEqual(69, matrix.mat[1, 1]);
            Assert.AreEqual(54, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(138, matrix.mat[2, 0]);
            Assert.AreEqual(114, matrix.mat[2, 1]);
            Assert.AreEqual(90, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [Test]
        public void Multiplication_MultiplyMatrixByVector_MultipliesEach()
        {
            Vector vec = new Matrix(1, 2, 3,
                                    4, 5, 6,
                                    7, 8, 9)
                            *
                            new Vector(1, 2, 3);

            Assert.AreEqual(14, vec.x);
            Assert.AreEqual(32, vec.y);
            Assert.AreEqual(50, vec.z);
            Assert.AreEqual(1, vec.w);
        }

        [TestCase(3)]
        [TestCase(2.2f)]
        [TestCase(756.21f)]
        [TestCase(float.MinValue)]
        [TestCase(float.MaxValue)]
        public void ScaleMatrix_GivenFloat_CreatesScaleMatrix(
            float scale)
        { 
            Matrix matrix = Matrix.ScaleMatrix(scale);

            // In which s = scale
            // s, 0, 0, 0
            // 0, s, 0, 0
            // 0, 0, s, 0
            // 0, 0, 0, 1
            Assert.AreEqual(scale, matrix.mat[0, 0]);
            Assert.AreEqual(0, matrix.mat[0, 1]);
            Assert.AreEqual(0, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(0, matrix.mat[1, 0]);
            Assert.AreEqual(scale, matrix.mat[1, 1]);
            Assert.AreEqual(0, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(0, matrix.mat[2, 0]);
            Assert.AreEqual(0, matrix.mat[2, 1]);
            Assert.AreEqual(scale, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }


        [TestCase(1)]
        [TestCase(3.3f)]
        [TestCase(567.32f)]
        public void RotateMatrixZ_GivenFloat_CreatesRotateMatrix(
            float theta)
        {
            Matrix matrix = Matrix.RotateMatrixZ(theta);
            
            float rad = theta * ((float)Math.PI / 180);
            float radcos = (float)Math.Cos(rad);
            float radsin = (float)Math.Sin(rad);

            // radcos, -radsin, 0, 0
            // radsin, radcos,  0, 0
            // 0,      0,       1, 0
            // 0,      0,       0, 1
            Assert.AreEqual(radcos, matrix.mat[0, 0]);
            Assert.AreEqual(-radsin, matrix.mat[0, 1]);
            Assert.AreEqual(0, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(radsin, matrix.mat[1, 0]);
            Assert.AreEqual(radcos, matrix.mat[1, 1]);
            Assert.AreEqual(0, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(0, matrix.mat[2, 0]);
            Assert.AreEqual(0, matrix.mat[2, 1]);
            Assert.AreEqual(1, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [TestCase(1)]
        [TestCase(3.3f)]
        [TestCase(567.32f)]
        public void RotateMatrixX_GivenFloat_CreatesRotateMatrix(
            float theta)
        {
            Matrix matrix = Matrix.RotateMatrixX(theta);

            float rad = theta * ((float)Math.PI / 180);
            float radcos = (float)Math.Cos(rad);
            float radsin = (float)Math.Sin(rad);

            // 1, 0,      0,       0
            // 0, radcos, -radsin, 0
            // 0, radsin, radcos,  0
            // 0, 0,      0,       1
            Assert.AreEqual(1, matrix.mat[0, 0]);
            Assert.AreEqual(0, matrix.mat[0, 1]);
            Assert.AreEqual(0, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(0, matrix.mat[1, 0]);
            Assert.AreEqual(radcos, matrix.mat[1, 1]);
            Assert.AreEqual(-radsin, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(0, matrix.mat[2, 0]);
            Assert.AreEqual(radsin, matrix.mat[2, 1]);
            Assert.AreEqual(radcos, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [TestCase(1, 2, 3)]
        [TestCase(2.2f, 2.2f, 2.2f)]
        [TestCase(10.23f, 36.322f, 47)]
        public void InverseMatrix_GivenFloats_CreatesInverseMatrix(
            float phi, float theta, float distance)
        {
            Matrix matrix = Matrix.InverseMatrix(phi, theta, distance);

            float t = theta * ((float)Math.PI / 180);
            float p = phi * ((float)Math.PI / 180);

            float st = (float)Math.Sin(t);
            float ct = (float)Math.Cos(t);
            float sp = (float)Math.Sin(p);
            float cp = (float)Math.Cos(p);

            // -st,    ct,     0,  0
            // -ct*cp, -cp*st, sp, 0
            // ct*sp,  st*sp,  ct, -distance
            // 0,      0,      0,  1
            Assert.AreEqual(-st, matrix.mat[0, 0]);
            Assert.AreEqual(ct, matrix.mat[0, 1]);
            Assert.AreEqual(0, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(-(ct*cp), matrix.mat[1, 0]);
            Assert.AreEqual(-(cp*st), matrix.mat[1, 1]);
            Assert.AreEqual(sp, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(ct*sp, matrix.mat[2, 0]);
            Assert.AreEqual(st*sp, matrix.mat[2, 1]);
            Assert.AreEqual(cp, matrix.mat[2, 2]);
            Assert.AreEqual(-distance, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [TestCase(1, 2)]
        [TestCase(4.4f, 2.2f)]
        [TestCase(32.33f, 23556.32f)]
        public void ProjectionMatrix_GivenFloats_CreatesProjectionMatrix(
            float distance, float vecz)
        {
            Matrix matrix = Matrix.ProjectionMatrix(distance, vecz);

            float p = -(distance / vecz);

            // p, 0, 0, 0
            // 0, p, 0, 0
            // 0, 0, 1, 0
            // 0, 0, 0, 1
            Assert.AreEqual(p, matrix.mat[0, 0]);
            Assert.AreEqual(0, matrix.mat[0, 1]);
            Assert.AreEqual(0, matrix.mat[0, 2]);
            Assert.AreEqual(0, matrix.mat[0, 3]);

            Assert.AreEqual(0, matrix.mat[1, 0]);
            Assert.AreEqual(p, matrix.mat[1, 1]);
            Assert.AreEqual(0, matrix.mat[1, 2]);
            Assert.AreEqual(0, matrix.mat[1, 3]);

            Assert.AreEqual(0, matrix.mat[2, 0]);
            Assert.AreEqual(0, matrix.mat[2, 1]);
            Assert.AreEqual(1, matrix.mat[2, 2]);
            Assert.AreEqual(0, matrix.mat[2, 3]);

            Assert.AreEqual(0, matrix.mat[3, 0]);
            Assert.AreEqual(0, matrix.mat[3, 1]);
            Assert.AreEqual(0, matrix.mat[3, 2]);
            Assert.AreEqual(1, matrix.mat[3, 3]);
        }

        [Test]
        public void ToString_GiveMatrix_IsPattern()
        {
            Matrix matrix = new Matrix(2.2f, 678.3f, 4,
                                       5, 6, 7,
                                       324.4f, 323.434f, 57);

            Assert.That(matrix.ToString(),
                Does.Match(@"(\/((\d+(\.|,)\d+|\d+)\, ){3}(\d+(\.|,)\d+|\d+)\\\n" +
                           @"(\|((\d+(\.|,)\d+|\d+)\, ){3}(\d+(\.|,)\d+|\d+)\|\n){2}" +
                           @"\\((\d+(\.|,)\d+|\d+)\, ){3}(\d+(\.|,)\d+|\d+)\/)"));
        }
    }
}
