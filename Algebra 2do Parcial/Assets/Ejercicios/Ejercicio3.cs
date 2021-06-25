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
            vec.Add(new Vector3(Lenght, 0, 0)); //Agrega un punto a la lista (10,0,0)
            vec.Add(new Vector3(Lenght, Lenght, 0)); //Agrega un nuevo punto a la lista vec (10,10,0)
            vec.Add(new Vector3(Lenght * 2, Lenght, 0)); //Agrega otro nuevo punto a la lista (20,10,0)
            vec.Add(new Vector3(Lenght * 2, Lenght * 2, 0)); //Agrega otro nuevo punto a la lista (20,20,0)

            VectorDebugger.AddVectorsSecuence(vec, true, Color.yellow, s); //Crea una secuencia de vectores con la lista anterior
                                                                         //El booleano indica que quiero empezar el primer vector
                                                                         //como origen de coordenadas

            VectorDebugger.EnableEditorView(s); //Habilita la visualización en el editor, si no, solo en ejecución
            VectorDebugger.EnableCoordinates(s); //Muestra las coordenadas abajo de los puntos de los vectores

        }
        private void Update()
        {

            Vector3 rot = new Vector3(angle, angle, 0); //Despues de ver varias (MUCHAS) veces el ejemplo en unity,
                                                        //Al verlo desde Z, los puntos hacían movimientos en forma de recta entre
                                                        //-10 y 10 tanto en 1, como en 3. Mientras que viéndolo desde X e Y, 
                                                        //el dibujo era en forma de circunferencia, entonces, probé con el angle
                                                        //sobre Z, ya que pensaba que era lo que estaba rotando. Error.
                                                        //Al hacerlo en x e y, y z en 0, conseguí que ambos giren de la misma
                                                        //forma

            List<Vector3> aux = new List<Vector3>(); //Crea la lista en la que guardo la posición de los vectores en este momento
            List<Vector3> next = new List<Vector3>(); //Crea la lista en la que guardo la posición siguiente de los vectores
            
            next.Add(VectorDebugger.GetVectorsPositions(s)[0]); //(0,0,0)La siguiente posición de los puntos 0,2,4 queda fija
            next.Add(Quaternion.Euler(rot) * VectorDebugger.GetVectorsPositions(s)[1]); //La siguiente posición del punto 1, rota
                                                                                        //en sentido antihorario.
            next.Add(VectorDebugger.GetVectorsPositions(s)[2]); //(10,10,0)
            next.Add(Quaternion.Euler(-rot) * VectorDebugger.GetVectorsPositions(s)[3]); //La siguiente posición del punto 3, rota
                                                                                         //en sentido horario. (el - hace que
                                                                                         //gire en el otro sentido)
            next.Add(VectorDebugger.GetVectorsPositions(s)[4]); //(20,20,0)

            VectorDebugger.UpdatePositionsSecuence(s, next); //Actualiza la secuencia de posiciones del selector

        }

    }

}