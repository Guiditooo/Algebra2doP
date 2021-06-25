using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EjerciciosAlgebra
{

    public class Ejercicio3 : MonoBehaviour
    {

        public const int Lenght = 10; //Tamaño del vector
        public float angle = 0; //Angulos a los que frame a frame va a rotar el vector

        private string s = "3"; //Selector de grupo 

        private void Start()
        {

            List<Vector3> vec = new List<Vector3>(); //Crea una lista para agregar los vectores a rotar

            vec.Add(Vector3.zero); //Agrega el (0,0,0) para iniciar en el
            vec.Add(new Vector3(Lenght, 0, 0)); //Agrega un punto a la lista
            vec.Add(new Vector3(Lenght, Lenght, 0)); //Agrega un nuevo punto a la lista vec
            vec.Add(new Vector3(Lenght * 2, Lenght, 0)); //Agrega otro nuevo punto a la lista 
            vec.Add(new Vector3(Lenght * 2, Lenght * 2, 0)); //Agrega otro nuevo punto a la lista 
            vec.Add(new Vector3(Lenght * 3, Lenght * 2, 0)); //Agrega otro nuevo punto a la lista 

            VectorDebugger.AddVectorsSecuence(vec, true, Color.yellow, s); //Crea una secuencia de vectores con la lista anterior
                                                                         //El booleano indica que quiero empezar el primer vector
                                                                         //como origen de coordenadas

            VectorDebugger.EnableEditorView(s); //Habilita la visualización en el editor, si no, solo en ejecución
            VectorDebugger.EnableCoordinates(s); //Muestra las coordenadas abajo de los puntos de los vectores

        }
        private void Update()
        {

            Vector3 rot = new Vector3(angle, angle, 0); //Setea la rotación en la cantidad elegida

            List<Vector3> aux = new List<Vector3>(); //Crea la lista en la que guardo la posición de los vectores en este momento
            List<Vector3> next = new List<Vector3>(); //Crea la lista en la que guardo la posición siguiente de los vectores
            
            next.Add(VectorDebugger.GetVectorsPositions(s)[0]);
            next.Add(Quaternion.Euler(rot) * VectorDebugger.GetVectorsPositions(s)[1]);
            next.Add(VectorDebugger.GetVectorsPositions(s)[2]);
            next.Add(Quaternion.Euler(-rot) * VectorDebugger.GetVectorsPositions(s)[3]);
            next.Add(VectorDebugger.GetVectorsPositions(s)[4]);

            VectorDebugger.UpdatePositionsSecuence(s, next); //Actualiza la secuencia de posiciones del selector

        }

    }

}