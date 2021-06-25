using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EjerciciosAlgebra
{

    public class Ejercicio1 : MonoBehaviour
    {

        public const int Lenght = 10;
        public float angle = 0;

        private void Start()
        {

            VectorDebugger.AddVector(new Vector3(Lenght, 0, 0), Color.red, "1");

            VectorDebugger.EnableEditorView();
            VectorDebugger.EnableCoordinates(); 

        }
        private void Update()
        {

            Vector3 aux = new Vector3(0, angle, 0);
            Vector3 act = VectorDebugger.GetVectorsPositions("1")[1];
            Vector3 niu = Quaternion.Euler(aux)*act;

            VectorDebugger.UpdatePosition("1", niu);

        }

    }

}