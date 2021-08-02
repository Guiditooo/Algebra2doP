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
        public static QuaternionX Euler(float x, float y, float z) //Devuelve un Quat con las coordenadas ya rotadas una cantidad de angulos X, Y y Z.
        {
            
            float sin;
            float cos;
            QuaternionX qX, qY, qZ;
            QuaternionX ret = identity;

            sin = Mathf.Sin(Mathf.Deg2Rad * x * 0.5f); //Para la parte imaginaria, se usa el seno
            cos = Mathf.Cos(Mathf.Deg2Rad * x * 0.5f); //Para la parte real, siempre voy a usar el coseno
            qX = new QuaternionX(sin, 0, 0, cos);
            
            sin = Mathf.Sin(Mathf.Deg2Rad * y * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * y * 0.5f);
            qY = new QuaternionX(0, sin, 0, cos);
            
            sin = Mathf.Sin(Mathf.Deg2Rad * z * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * z * 0.5f);
            qZ = new QuaternionX(0, 0, sin, cos);

            ret = qY * qX * qZ; //Tengo entendido que esta es la forma en la que se debe trabajar.

            return ret;
        }
        public static QuaternionX Euler(Vector3 angle)
        {
            return Euler(angle.x, angle.y, angle.z);
        }//Idem Euler
        public static QuaternionX EulerAngles(float x, float y, float z)
        {
            return Euler(x, y, z);
        }//Idem Euler
        public static QuaternionX EulerAngles(Vector3 angle)
        {
            return Euler(angle.x, angle.y, angle.z);
        }//Idem Euler
        public static QuaternionX Normalize(QuaternionX a)
        { 
            float aux = a.magnitude; //Módulo o magnitud
            return new QuaternionX(a._x / aux, a._y / aux, a._z / aux, a._w / aux);
        }//Divide todas las componentes por la magnitud (raiz de los cuadrados de las componentes)
        public QuaternionX Normalize()// Devuelve el Quaternion con magnitud 1. Permite hacer cuentas más fácil.
        {
            float aux = magnitude; 
            return new QuaternionX(_x / aux, _y / aux,_z / aux, _w / aux);
        }
        public QuaternionX normalized {
            get
            {
                return Normalize(this);
            }
        } //Usa la función de arriba
        public float magnitude //Devuelve la magnitud del vector: raíz de la suma del cuadrado de cada componente.
        {
            get
            {
                return Mathf.Sqrt(Mathf.Pow(_x, 2) + Mathf.Pow(_y, 2) + Mathf.Pow(_z, 2) + Mathf.Pow(_w, 2));
            }
        }
        public static float Dot(QuaternionX a, QuaternionX b) //Término a término. Double son para hacerlo más exacto.
        {
            return (float)((double)a._x * (double)b._x + (double)a._y * (double)b._y + (double)a._z * (double)b._z + (double)a._w * (double)b._w);
        }
        public static float Angle(QuaternionX a, QuaternionX b) //Devuelve el ángulo entre un vector y otro.
        {
            if(a.magnitude==0 || b.magnitude == 0)
            {
                return 0; //Esto es porque si alguna magnitud es 0, tecnicamente no hay vector.
            }
            return (float)((double)Mathf.Acos(Mathf.Abs(Dot(a, b)) * Mathf.Rad2Deg / (a.magnitude * b.magnitude) ) );//si adotb es 0, el angulo es 90 grados.
        }
        public static QuaternionX AngleAxis(float angle, Vector3 axis) //Genera una rotacion en dicho vector usa radianes
        {
            QuaternionX ret = identity; 
            axis.Normalize();
            axis *= (float)System.Math.Sin((angle / 2) * Mathf.Deg2Rad); //Parte imaginaria
            ret._x = axis.x;
            ret._y = axis.y;
            ret._z = axis.z;
            ret._w = (float)System.Math.Cos((angle / 2) * Mathf.Deg2Rad); //Parte real
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
        public static QuaternionX FromToRotation(Vector3 from, Vector3 to)
        {

            Vector3.Normalize(to);
            Vector3.Normalize(from);

            QuaternionX ret;

            float dot = Vector3.Dot(from, to);

            if (dot > -1 + kEpsilon) // mas grande -0,99999
            {
                float s = Mathf.Sqrt((1 + dot) * 2); //Creo que es el arco coseno
                float inverse = 1 / s;               //Siguiendo el hilo de pensamiento, esto es el angulo
                Vector3 c = Vector3.Cross(from, to) * inverse; 
                ret = new QuaternionX(c.x, c.y, c.z, s * 0.5f);
            }
            else if (dot > 1 - kEpsilon) // si es mas grande que 0,999999
            {
                ret = new QuaternionX(0, 0, 0, 1);
            }
            else // - 0,99999 < dot < 0,99999
            {
                Vector3 axis = Vector3.Cross(Vector3.right, from);
                if (Vector3.SqrMagnitude(axis) < kEpsilon)
                {
                    axis = Vector3.Cross(Vector3.forward, from);
                }
                ret = new QuaternionX(axis.x, axis.y, axis.z, 0);
            }

            //Como obtengo un axis y un angulo, podria usar angleaxis para simplificar, pero asi tengo en cuenta los valores menores a -1 y mayores a 1

            return ret;

        }
        public static QuaternionX Inverse(QuaternionX q) => -q; //Invierte las partes imaginarias
        public static QuaternionX Lerp(QuaternionX a, QuaternionX b, float time)//Interpola linealmente desde A a B.
        {//Basicamente, time va a ir de 0 a 1. Siendo 0 la posición de A, y 1 la de B
            time = Mathf.Clamp(time, 0, 1);
            QuaternionX ret = LerpUnclamped(a, b, time);
            Normalize(ret);
            return ret;
        }
        public static QuaternionX LerpUnclamped(QuaternionX a, QuaternionX b, float time) //Va desde un punto que pasa por otro hasta el infinito, De manera lineal
        {   //Basicamente, time va a ir de 0 a 1. Siendo 0 la posición de A, y 1 la de B. 
            //En este caso, si time no está en el rango 0-1, va a pasarse, en la misma linea (formada por a y b).
            QuaternionX ret = identity;
            if (Dot(a, b) < 0) //Solamente para saber el sentido en el que debe lerpear
            {
                ret._x = a._x + time * (-b._x - a._x);
                ret._y = a._y + time * (-b._y - a._y);
                ret._z = a._z + time * (-b._z - a._z);
                ret._w = a._w + time * (-b._w - b._w);
            }
            else
            {
                ret._x = a._x + time * (b._x - a._x);
                ret._y = a._y + time * (b._y - a._y);
                ret._z = a._z + time * (b._z - a._z);
                ret._w = a._w + time * (b._w - b._w);
            }
            Normalize(ret);
            return ret;
        }
        public static QuaternionX Slerp(QuaternionX a, QuaternionX b, float time) //interpola esfericamente, desde A, a B. 
        {
            time = Mathf.Clamp(time, 0, 1);
            return SlerpUnclamped(a, b, time);
        }
        public static QuaternionX SlerpUnclamped(QuaternionX a, QuaternionX b, float time)//Va desde un punto que pasa por otro hasta el infinito, de manera esferica
        {//En este caso, si time no está en el rango 0-1, va a pasarse, en la misma linea (formada por a y b).
            QuaternionX ret = identity;
            float dot = Dot(a, b);

            if (dot < 0)//Verifica hacia donde tiene que ir
            {
                dot = -dot;
                b = -b;
                b._w = -b._w;
            }
            float t1, t2;
            if (dot < 0.95)
            {   
                float angle = Mathf.Acos(dot);
                float sinAgle = Mathf.Sin(angle);
                float inverse = 1 / sinAgle;
                t1 = Mathf.Sin((1 - time) * angle) * inverse;
                t2 = Mathf.Sin(time * angle) * inverse;
                ret = new QuaternionX(a._x * t1 + b._x * t2, a._y * t1 + b._y * t2, a._z * t1 + a._z * t2, a._w * t1 + b._w * t2);
            }
            else
            {
                ret = Lerp(a, b, time);
            }
            return ret;
        }
        public void Set(float new_x, float new_y, float new_z, float new_w) //Da nuevos valores a las componentes
        {
            _x = new_x;
            _y = new_y;
            _z = new_z;
            _w = new_w;
        }
        public static QuaternionX LookRotation(Vector3 forward, Vector3 upwards) //Forward es el ejez
        {   //Forma una rotación que parte desde la identidad, hasta forward, teniendo en cuenta, cuál es el vector que se utiliza como ortogonal a donde se está mirando.
            //generalmente, el vector ortogonal que se usa, es el up (0,1,0);

            QuaternionX ret = identity;
            int[] _next = { 1, 2, 0 };

            if (forward.magnitude < kEpsilon) //Si la magnitud es muy chica o es 0
            {
                return ret;
            }

            forward.Normalize();

            upwards = upwards != null ? upwards : Vector3.up; //Esto es para asemejar al LookRotation(vector3)
            Vector3 right = Vector3.Cross(upwards, forward); //Genera los vectores ortogonales a partir de los vectores ingresados. En este caso, es el eje X
            Vector3.Normalize(right);
            upwards = Vector3.Cross(forward, right); //Este es el eje Y //Pero para esto, es necesrio normalizar Y
            right = Vector3.Cross(upwards, forward); //Vuelvo a tomar el eje X, sin normalizar.

            float t = right.x + upwards.y + forward.z;  //Sumatoria de diagonales
            if (t > 0)
            {
                float x, y, z, w;

                t++; //suma +1

                float s = 0.5f / Mathf.Sqrt(t); // Inversa de raiz(diagonales + 1) * 2 Y esto me devuelve un ángulo
                
                //Supongamos que tengo una matriz de rotación formada por right, upwards y forwad. Similar a la del else.
                w = s * t;
                x = (upwards.z - forward.y) * s;
                y = (forward.x - right.z) * s;
                z = (right.y - upwards.x) * s;
                //Extraigo el quaternion de esa matriz de rotación formada por right, upwards y forward.
                ret = new QuaternionX(x, y, z, w);
            }
            else
            {
                float[,] rot =
                {
                    {right.x, upwards.x, forward.x},
                    {right.y, upwards.y, forward.y},
                    {right.z, upwards.z, forward.z}
                };
                float[] q = { 0, 0, 0 };
                int i = 0;
                if (upwards.y > right.x) //Si.
                {
                    i = 1;
                }
                if (forward.z > rot[i, i]) //También.
                {
                    i = 2;
                }
                int j = _next[i]; //Esta hecho para no pasarse del indice, a la vez que haya un valor diferente para i, j y k.
                int k = _next[j];
                t = rot[i, i] - rot[j, j] - rot[k, k] + 1; //suma +1 a la diagonal
                float s = 0.5f / Mathf.Sqrt(t); // Inversa de raiz(diagonales + 1) * 2 Y esto me devuelve un ángulo
                q[i] = s * t;
                float w = (rot[k, j] - rot[j, k]) * s;
                q[j] = (rot[j, i] + rot[i, j]) * s;
                q[k] = (rot[k, i] + rot[i, k]) * s;
                //Extraigo el quaternion de esa matriz de rotación formada por right, upwards y forward.
                ret = new QuaternionX(q[0], q[1], q[2], w);
            }
            Normalize(ret); //Solamente busca una dirección así que es necesario normalizar el resultado.
            return ret;
        }
        public static QuaternionX LookRotation(Vector3 forward) => LookRotation(forward, Vector3.up);
        public void SetLookRotation(Vector3 view)
        {
            Vector3 up = Vector3.up;
            this.SetLookRotation(view, up);
        }
        public void SetLookRotation(Vector3 view, Vector3 up)
        {
            this = LookRotation(view, up);
        }
        public static QuaternionX RotateTowards(QuaternionX from, QuaternionX to, float angle)
        {
            float num = Angle(from, to); //Es mover del uno al otro, la cantidad de grados especificada.
            if (num == 0)
            {
                return to;
            }
            float t = Mathf.Min(1f, angle / num); //Single es una estructura para mucha precision de datos 32 digitos.
            return SlerpUnclamped(from, to, t); 
        }
        public void SetFromToRotation(Vector3 from, Vector3 to)
        {
            this = FromToRotation(from, to);
        }
        public void ToAngleAxis(out float angle, out Vector3 axis)
        {
            angle = 2 * Mathf.Acos(_w); //w se arma con el coseno del angulo, porl o que a la inversa, se consigue el angulo
            if (Mathf.Abs(angle) < kEpsilon) //Si el angulo es muy chico
            {
                angle *= Mathf.Deg2Rad; //pasa de grado a radian
                axis = new Vector3(1, 0, 0);
            }
            float div = 1 / Mathf.Sqrt(1 - Mathf.Sqrt(_w)); 
            angle *= Mathf.Deg2Rad; //pasa de grado a radian
            axis = new Vector3(_x * div, _y * div, _z * div);
            
        }
        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Operators

        public static QuaternionX operator +(QuaternionX v1, QuaternionX v2)//suma componente a componente
        {
            return new QuaternionX(v1._x + v2._x, v1._y + v2._y, v1._z + v2._z, v1._w + v2._w);
        }
        public static QuaternionX operator -(QuaternionX v1, QuaternionX v2)//Resta componente a componente
        {
            return new QuaternionX(v1._x - v2._x, v1._y - v2._y, v1._z - v2._z, v1._w - v2._w);
        }
        public static QuaternionX operator -(QuaternionX v1)//Quaternion dando vuelta los signos de sus componentes
        {
            return new QuaternionX(-v1._x, -v1._y, -v1._z, v1._w);
        }
        public static QuaternionX operator *(QuaternionX v1, float v2)//Multiplicación componente a componente
        {
            return new QuaternionX(v1._x * v2, v1._y * v2, v1._z * v2, v1._w * v2);
        }
        public static bool operator ==(QuaternionX v1, QuaternionX v2)//Cada componente igual a la del otro quaternion
        {
            return (v1._x == v2._x && v1._y == v2._y && v1._z == v2._z && v1._w == v2._w);
        }
        public static bool operator !=(QuaternionX v1, QuaternionX v2)//Al menos una componente distinta respecto del otro quaternion
        {
            return (v1._x != v2._x || v1._y != v2._y || v1._z != v2._z || v1._w != v2._w);
        }
        public override int GetHashCode() => _x.GetHashCode() ^ _y.GetHashCode() << 2 ^ _z.GetHashCode() >> 2 ^ _w.GetHashCode() >> 1;

        public override bool Equals(object other) => other is QuaternionX other1 && Equals(other1);

        public static QuaternionX operator *(QuaternionX a, QuaternionX b) //Quaternion * Quaternion
        {
            return new QuaternionX( 
                a._w * b._x + a._x * b._w + a._y * b._z - a._z * b._y,  // i
                a._w * b._y - a._x * b._z + a._y * b._w + a._z * b._x,  // j
                a._w * b._z + a._x * b._y - a._y * b._x + a._z * b._w,  // k
                a._w * b._w - a._x * b._x - a._y * b._y - a._z * b._z   // 1
            );
        }

        public static Vector3 operator *(QuaternionX rot, Vector3 point) // Le aplica la rotación a un punto
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
       
    }
}