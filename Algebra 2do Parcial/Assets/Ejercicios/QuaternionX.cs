using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algebra
{
    public struct QuaternionX : IEquatable<QuaternionX>, IFormattable
    {

        #region Variables

        public const float kEpsilon = 1E-06F;
        public float _x, _y, _z, _w;

        #endregion

        #region Constructor
        public QuaternionX(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        #endregion

        #region Methods

        public float this[int index] //Entre 0 y 3. 
        {
            get //Devuelve esa componente.
            {
                switch (index)
                {
                    case 0:
                        return _x;
                    case 1:
                        return _y;
                    case 2:
                        return _z;
                    case 3:
                        return _w;
                    default:
                        throw new IndexOutOfRangeException("Index out of Range!");
                }
            }
            set
            {
                switch (index) //Setea esa componente.
                {
                    case 0:
                        _x = value;
                        break;
                    case 1:
                        _y = value;
                        break;
                    case 2:
                        _z = value;
                        break;
                    case 3:
                        _w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Index out of Range!");                }
            }
        }

        #endregion

        #region Operators

        public static QuaternionX operator +(QuaternionX v1, QuaternionX v2)//Value1, Value2
        {
            return new QuaternionX(v1._x + v2._x, v1._y + v2._y, v1._z + v2._z, v1._w + v2._w);
        }
        public static QuaternionX operator -(QuaternionX v1, QuaternionX v2)//Value1, Value2
        {
            return new QuaternionX(v1._x - v2._x, v1._y - v2._y, v1._z - v2._z, v1._w - v2._w);
        }
        public static QuaternionX operator -(QuaternionX v1)//Value1
        {
            return new QuaternionX(-v1._x, -v1._y, -v1._z, -v1._w);
        }
        public static QuaternionX operator *(QuaternionX v1, float v2)//Value1, Value2
        {
            return new QuaternionX(v1._x * v2, v1._y * v2, v1._z * v2, v1._w * v2);
        }
        public static bool operator ==(QuaternionX v1, QuaternionX v2)//Value1, Value2
        {
            return (v1._x == v2._x && v1._y == v2._y && v1._z == v2._z && v1._w == v2._w);
        }
        public static bool operator !=(QuaternionX v1, QuaternionX v2)//Value1, Value2
        {
            return (v1._x != v2._x || v1._y != v2._y || v1._z != v2._z || v1._w != v2._w);
        }

        public static QuaternionX operator *(QuaternionX a, QuaternionX b) //Quaternion - Quaternion
        {
            return new QuaternionX( 
                a._w * b._x + a._x * b._w + a._y * b._z - a._z * b._y,  // i
                a._w * b._y - a._x * b._z + a._y * b._w + a._z * b._x,  // j
                a._w * b._z + a._x * b._y - a._y * b._x + a._z * b._w,  // k
                a._w * b._w - a._x * b._x - a._y * b._y - a._z * b._z   // 1
            );
        }

        public static Vector3 operator *(QuaternionX rot, Vector3 point) // Quaternion - vector
        {
            float num1 = rot._x * 2f;
            float num2 = rot._y * 2f;
            float num3 = rot._z * 2f;
            float num4 = rot._x * num1;
            float num5 = rot._y * num2;
            float num6 = rot._z * num3;
            float num7 = rot._x * num2;
            float num8 = rot._x * num3;
            float num9 = rot._y * num3;
            float num10 = rot._w * num1;
            float num11 = rot._w * num2;
            float num12 = rot._w * num3;
            Vector3 vector3;
            vector3.x = (float)((1.0 - ((double)num5 + (double)num6)) * (double)point.x + ((double)num7 - (double)num12) * (double)point.y + ((double)num8 + (double)num11) * (double)point.z);
            vector3.y = (float)(((double)num7 + (double)num12) * (double)point.x + (1.0 - ((double)num4 + (double)num6)) * (double)point.y + ((double)num9 - (double)num10) * (double)point.z);
            vector3.z = (float)(((double)num8 - (double)num11) * (double)point.x + ((double)num9 + (double)num10) * (double)point.y + (1.0 - ((double)num4 + (double)num5)) * (double)point.z);
            //El double es para perder la menor cantidad de decimales en el camino. 
            //Despues se lo trunca, pero es más exacto el proceso.
            return vector3;
        }

        #endregion


        private void Start()
        {
            Quaternion qa = new Quaternion();

            qa.Normalize();
        }

        public bool Equals(QuaternionX other)
        {
            throw new NotImplementedException();
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}