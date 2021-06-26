using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algebra
{
    public class QuaternionX : MonoBehaviour
    {

        #region Variables

        public float _x, _y, _z, _w;

        #endregion

        #region Constructors
        public QuaternionX()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            _w = 0;
        } 
        public QuaternionX(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        #endregion

        #region Methods


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
        public static QuaternionX operator /(QuaternionX v1, float v2)//Value1, Value2
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

        #endregion

        private void Start()
        {
            Quaternion qa = new Quaternion();

            qa.Normalize();
        }

    }
}