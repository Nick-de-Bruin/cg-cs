﻿using System;
using System.Text;

namespace MatrixTransformations
{
    /// <summary>
    /// Represents a three dimensional vector
    /// </summary>
    public class Vector
    {
        public float x, y, z;
        public readonly float w = 1f;

        /// <summary>
        /// Represents an empty vector
        /// </summary>
        public Vector() { }

        /// <summary>
        /// Represents a vector with x, y, z
        /// </summary>
        /// <param name="x">Sets the x value</param>
        /// <param name="y">Sets the y value</param>
        /// <param name="z">Sets the z value</param>
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector operator +(Vector vec1, Vector vec2) =>
            new Vector(vec1.x + vec2.x, vec1.y + vec2.y, vec1.z + vec2.z);

        public static Vector operator -(Vector vec1, Vector vec2) =>
            new Vector(vec1.x - vec2.x, vec1.y - vec2.y, vec1.z - vec2.z);

        public static Vector operator *(float num, Vector vec) =>
            new Vector(vec.x * num, vec.y * num, vec.z * num);
        public static Vector operator *(Vector vec, float num) =>
            num * vec;

        public override string ToString()
        {
            return $"/{x}\\\n" +
                   $"|{y}|\n" +
                   $"|{z}|\n" +
                   $"\\{w}/\n";
        }
    }
}
