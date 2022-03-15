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
    internal class VectorTests
    {
        [TestCase(1, 1, 1)]
        [TestCase(1.2f, 2.2f, 7.3f)]
        [TestCase(0, 0f, 0.0f)]
        [TestCase(22, float.MaxValue, float.MinValue)]
        public void Constructor_Construction_FillsVariables(
            float x, float y, float z)
        {
            Vector vec = new Vector(x, y, z);

            Assert.AreEqual(x, vec.x);
            Assert.AreEqual(y, vec.y);
            Assert.AreEqual(z, vec.z);
            Assert.AreEqual(1f, vec.w);
        }

        [TestCase(1, 2, 3,
                  1, 2, 3,
                  2, 4, 6)]
        [TestCase(2.2f, 2.2f, 2.2f,
                  2.2f, 2.2f, 2.2f,
                  4.4f, 4.4f, 4.4f)]
        [TestCase(25, 71, 32,
                  3, 4, 5,
                  28, 75, 37)]
        public void Addition_AddNumbers_AddsNumbers(
            float leftX, float leftY, float leftZ,
            float rightX, float rightY, float rightZ,
            float expectedX, float expectedY, float expectedZ)
        {
            Vector result = new Vector(leftX, leftY, leftZ) +
                new Vector(rightX, rightY, rightZ);

            Assert.AreEqual(expectedX, result.x);
            Assert.AreEqual(expectedY, result.y);
            Assert.AreEqual(expectedZ, result.z);
            Assert.AreEqual(1f, result.w);
        }


        [TestCase(2, 3, 4,
                  1, 2, 3,
                  1, 1, 1)]
        [TestCase(2.2f, 2.2f, 2.2f,
                  1.1f, 1.1f, 1.1f,
                  1.1f, 1.1f, 1.1f)]
        [TestCase(25, 71, 32,
                  3, 4, 5,
                  22, 67, 27)]
        public void Subtraction_SubtractNumber_Subtracts(
            float leftX, float leftY, float leftZ,
            float rightX, float rightY, float rightZ,
            float expectedX, float expectedY, float expectedZ)
        {
            Vector result = new Vector(leftX, leftY, leftZ) -
                new Vector(rightX, rightY, rightZ);

            Assert.AreEqual(expectedX, result.x);
            Assert.AreEqual(expectedY, result.y);
            Assert.AreEqual(expectedZ, result.z);
            Assert.AreEqual(1f, result.w);
        }

        [TestCase(1, 2, 3,
                  2,
                  2, 4, 6)]
        [TestCase(2.2f, 2.2f, 2.2f,
                  2,
                  4.4f, 4.4f, 4.4f)]
        [TestCase(23.2f, 74, 81,
                  10,
                  232, 740, 810)]
        public void Multiplication_MultiplyNumber_MultipliesNumbers(
            float x, float y, float z,
            float multiplication,
            float expectedX, float expectedY, float expectedZ)
        {
            Vector result = new Vector(x, y, z) * multiplication;
            Vector resultLeft = multiplication * new Vector(x, y, z);

            Assert.AreEqual(expectedX, result.x);
            Assert.AreEqual(expectedY, result.y);
            Assert.AreEqual(expectedZ, result.z);
            Assert.AreEqual(1f, result.w);

            Assert.AreEqual(resultLeft.x, result.x);
            Assert.AreEqual(resultLeft.y, result.y);
            Assert.AreEqual(resultLeft.z, result.z);
        }

        [TestCase(1, 2, 3)]
        [TestCase(1.1f, 3.3f, 789f)]
        [TestCase(200, 3, 7894.2f)]
        public void ToString_OnVector_FollowsRegex(
            float x, float y, float z)
        {
            Vector result = new Vector(x, y, z);

            Assert.That(result.ToString(), 
                Does.Match(@"(\/(\d+(\.|,)\d+|\d+)\\\n" +
                           @"(\|(\d+(\.|,)\d+|\d+)\|\n){2}" +
                           @"\\(\d+(\.|,)\d+|\d+)\/)"));
        }
    }
}
