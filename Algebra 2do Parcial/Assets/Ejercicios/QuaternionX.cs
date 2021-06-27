using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algebra
{
    public struct QuaternionX : IEquatable<object>, IFormattable
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
        public static QuaternionX identity { get; } = new QuaternionX(0, 0, 0, 1); //Devuelve la identidad del quaternion (0,0,0,1).
        public static QuaternionX Euler(float x, float y, float z)
        {

            float sin;
            float cos;
            QuaternionX qX, qY, qZ, ret = identity;

            sin = Mathf.Sin(Mathf.Deg2Rad * x * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * x * 0.5f);
            qX = new QuaternionX(sin, 0, 0, cos);
            
            sin = Mathf.Sin(Mathf.Deg2Rad * y * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * y * 0.5f);
            qY = new QuaternionX(0, sin, 0, cos);
            
            sin = Mathf.Sin(Mathf.Deg2Rad * z * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * z * 0.5f);
            qZ = new QuaternionX(0, 0, sin, cos);

            ret = qY * qX * qZ;

            return ret;
        }
        public static QuaternionX Euler(Vector3 angle)
        {
            return Euler(angle.x, angle.y, angle.z);
        }
        public static QuaternionX EulerAngles(float x, float y, float z)
        {
            return Euler(x, y, z);
        }
        public static QuaternionX EulerAngles(Vector3 angle)
        {
            return Euler(angle.x, angle.y, angle.z);
        }
        public static QuaternionX Normalize(QuaternionX a)
        { 
            float aux = Mathf.Sqrt(Mathf.Pow(a._x, 2) + Mathf.Pow(a._y, 2) + Mathf.Pow(a._z, 2) + Mathf.Pow(a._w, 2));
            return new QuaternionX(a._x / aux, a._y / aux, a._z / aux, a._w / aux);
        }
        public QuaternionX Normalize()
        {
            float aux = Mathf.Sqrt(Mathf.Pow(_x, 2) + Mathf.Pow(_y, 2) + Mathf.Pow(_z, 2) + Mathf.Pow(_w, 2));
            return new QuaternionX(_x / aux, _y / aux,_z / aux, _w / aux);
        }
        public QuaternionX normalized {
            get
            {
                return Normalize(this);
            }
        }
        public static float Dot(QuaternionX a, QuaternionX b) //Término a término. Double son par hacerlo más exacto.
        {
            return (float)((double)a._x * (double)b._x + (double)a._y * (double)b._y + (double)a._z * (double)b._z + (double)a._w * (double)b._w);
        }
        public static float Angle(QuaternionX a, QuaternionX b) //Investigar
        {
            return (float)((double)Mathf.Acos(Mathf.Min(Mathf.Abs(Dot(a, b)), 1f)) * 2.0f * Mathf.Rad2Deg);
        }
        public static QuaternionX AngleAxis(float angle, Vector3 axis) //Genera una rotacion en dicho vector usa radianes
        {
            QuaternionX ret = identity; 
            axis.Normalize();
            axis *= (float)System.Math.Sin((angle / 2) * Mathf.Deg2Rad);
            ret._x = axis.x;
            ret._y = axis.y;
            ret._z = axis.z;
            ret._w = (float)System.Math.Cos((angle / 2) * Mathf.Deg2Rad);
            return Normalize(ret);
        }
        public static QuaternionX AxisAngle(Vector3 axis, float angle) //Genera una rotacion en dicho vector usa grados
        {
            QuaternionX ret = identity;
            axis.Normalize();
            axis *= (float)System.Math.Sin((angle / 2));
            ret._x = axis.x;
            ret._y = axis.y;
            ret._z = axis.z;
            ret._w = (float)System.Math.Cos((angle / 2));
            return Normalize(ret);
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
        public override int GetHashCode() => _x.GetHashCode() ^ _y.GetHashCode() << 2 ^ _z.GetHashCode() >> 2 ^ _w.GetHashCode() >> 1;

        public override bool Equals(object other) => other is QuaternionX other1 && Equals(other1);

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
            QuaternionX qa = new QuaternionX(1, 2, 3, 1); 
            qa.Normalize();

        }
        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

       
    }
}