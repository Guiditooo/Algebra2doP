using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EjerciciosAlgebra
{

    public class Ejercicio1 : MonoBehaviour
    {

        public const int Lenght = 10;
        public float angle = 0;

        Vector3 vec;

        private void Start()
        {

            vec = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));

            VectorDebugger.AddVector(Vector3.zero, Color.red, "0");
            VectorDebugger.AddVector(new Vector3(Lenght, 0, 0), Color.red, "1");

            VectorDebugger.EnableCoordinates();
            VectorDebugger.EnableEditorView();

        }
        private void Update()
        {

            Vector3 act = transform.position;

            VectorDebugger.UpdatePosition("1", new Vector3(act.x * vec.x - act.y * vec.y, act.y * vec.x - act.x * vec.y, 0));

            //transform.position = new Vector3(act.x * vec.x - act.y * vec.y, act.x * vec.y - act.y * vec.x, 0);

        }

    }

}