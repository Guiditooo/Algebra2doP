using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EjerciciosAlgebra
{

    public class Ejercicio1 : MonoBehaviour
    {

        public int presettedLenght = 0;
        public float presettedAngle = 0;

        VectorArrow arrow;

        private void Start()
        {
            VectorDebugger.AddVector(Vector3.zero, Color.red, "0");
            VectorDebugger.AddVector(new Vector3(presettedLenght, 0, 0), Color.red, "1");

            arrow = new VectorArrow(1, 0.5f, 2);

            VectorDebugger.EnableCoordinates();
            VectorDebugger.EnableEditorView();
            VectorDebugger.SetVectorArrow(arrow);

            //target = new GameObject();
            //target.transform.position = new Vector3(presettedLenght, 0, 0);

        }
        float a = 0;
        private void Update()
        {

                

            float x = (presettedLenght * Mathf.Tan(a) * Mathf.Sqrt(1 + Mathf.Pow(Mathf.Tan(a), 2))) / (1 + Mathf.Pow(Mathf.Tan(a), 2));
            float z;

            if (a <= 180)
            {

                z = Mathf.Sqrt(Mathf.Pow(presettedLenght, 2) - Mathf.Pow(x, 2));

            }
            else
            {

                z = -Mathf.Sqrt(Mathf.Pow(presettedLenght, 2) - Mathf.Pow(x, 2));

                if (a == 360)
                {
                    a = 0;
                }

            }
            

            a += presettedAngle;

            

            VectorDebugger.UpdatePosition("1",new Vector3(x,0,z));

        }

    }

}