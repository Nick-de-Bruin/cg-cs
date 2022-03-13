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
    }
}
