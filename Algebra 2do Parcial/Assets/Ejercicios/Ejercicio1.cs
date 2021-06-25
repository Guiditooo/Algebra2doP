using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EjerciciosAlgebra
{

    public class Ejercicio1 : MonoBehaviour
    {

        public const int Lenght = 10; //Tamaño del vector
        public float angle = 0; //Angulos a los que frame a frame va a rotar el vector

        private void Start()
        {

            VectorDebugger.AddVector(new Vector3(Lenght, 0, 0), Color.red, "1"); //Agrega un vector al grupo "1"

            VectorDebugger.EnableEditorView(); //Habilita la visualización en el editor, si no, solo en ejecución
            VectorDebugger.EnableCoordinates(); //Muestra las coordenadas abajo de los puntos de los vectores.

        }
        private void Update()
        {

            Vector3 aux = new Vector3(0, angle, 0); //Setea la rotación en la cantidad elegida.

            Vector3 act = VectorDebugger.GetVectorsPositions("1")[1];   //Abre el grupo de vectores "1" u usa la posición
                                                                        //elegida, en este caso es 1, porque el 0 está reservado
                                                                        //al (0,0,0).

            Vector3 niu = Quaternion.Euler(aux)*act;    //Un quaternion multiplicado con un vector, devuelve un vector.
                                                        //Quaternion.euler genera una rotación sobre los ejes distintos en donde
                                                        //su componente es distinta de 0. Se lo multiplica por la posición actual
                                                        //del vector seleccionado arriba y con eso se genera la rotación.

            VectorDebugger.UpdatePosition("1", niu); //Refresca la posición del grupo "1". En este caso sirve porque hay un solo
                                                     //punto (sin contal el origen de coordenadas).
            
        }

    }

}