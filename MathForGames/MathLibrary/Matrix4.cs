using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public class Matrix4
    {
        public float m11, m12, m13, m21, m22, m23, m31, m32, m33, m41, m42, m43;


        public Matrix4()
        {
            m11 = 1; m12 = 0; m13 = 0;
            m21 = 0; m22 = 1; m23 = 0;
            m31 = 0; m32 = 0; m33 = 0;
            m41 = 0; m32 = 0; m43 = 0;
        }

        public Matrix4(float m11, float m12, float m13,
                       float m21, float m22, float m23,
                       float m31, float m32, float m33,
                       float m41, float m42, float m43)
        {
            this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m31 = m31; this.m32 = m32; this.m33 = m33;
            this.m41 = m31; this.m42 = m42; this.m43 = m43;
        }

        public static Matrix4 operator +(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    lhs.m11 + rhs.m11, lhs.m12 + rhs.m12, lhs.m13 + rhs.m13,
                    lhs.m21 + rhs.m21, lhs.m22 + rhs.m22, lhs.m23 + rhs.m23,
                    lhs.m31 + rhs.m31, lhs.m32 + rhs.m32, lhs.m33 + rhs.m33,
                    lhs.m41 + rhs.m41, lhs.m42 + rhs.m42, lhs.m43 + rhs.m43
                );

        }

        public static Matrix4 CreateRotation(float radians)
        {
            return new Matrix4
                (
                    (float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                    -(float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                    0, 0, 1
                );
        }

        public static Matrix4 CreateTranslation(Vector2 position)
        {
            return new Matrix4
                (
                    0, 0, 0, 0, position.X,
                    0, 0, 0, position.Y,
                    0, 0, 1
                );
        }

        public static Matrix4 CreateScale(Vector2 scale)
        {
            return new Matrix4
                (
                    scale.X, 0, 0, 0,
                    0, scale.Y, 0, 0,
                    0, 0, 1, 0
                );
        }

        public static Matrix4 operator -(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    lhs.m11 - rhs.m11, lhs.m12 - rhs.m12, lhs.m13 - rhs.m13,
                    lhs.m21 - rhs.m21, lhs.m22 - rhs.m22, lhs.m23 - rhs.m23,
                    lhs.m31 - rhs.m31, lhs.m32 - rhs.m32, lhs.m32 - rhs.m33,
                    lhs.m41 - rhs.m41, lhs.m42 - rhs.m42, lhs.m42 - rhs.m43
                );

        }

        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    //Row1, Column1
                    lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31,
                    //Row2, Column2
                    lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32,
                    //Row3, Column3
                    lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33,

                    lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33,

                    //Row2, Column1
                    lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31,
                    //Row2, Column2
                    lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32,
                    //Row2, Colume3
                    lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33,

                    lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33,

                    //Row3, Column1
                    lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31,
                    //Row3, Column2
                    lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32,
                    //Row3, Column3
                    lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33,

                    lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33
                );
        }
    }
}
