using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    [SerializeField] float sphereSize = 0.2f;


    private void OnDrawGizmos()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length - 1; i++)
        {
            Handles.Label(children[i].position, children[i].gameObject.name);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(children[i].position, sphereSize);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(children[i].position, children[i + 1].position);

            // Draw Bezier Curve
            //Handles.DrawBezier(
            //    children[i].position,
            //    children[i + 1].position,
            //    children[i].position + Vector3.up * 2f,
            //    children[i + 1].position - Vector3.up * 2f,
            //    Color.blue,
            //    null,
            //    2f
            //);
        }
    }




    /* private void OnDrawGizmos()
     {
         Transform lastChild = null;
         foreach(Transform child in transform)
         {

             Handles.Label(child.transform.position, child.transform.gameObject.name);
             Gizmos.color = Color.green;
             Gizmos.DrawSphere(child.transform.position, sphereSize);
             if (lastChild != null)
             {
                 Gizmos.color = Color.red;
                 Gizmos.DrawLine(child.position, lastChild.position);

             }
             lastChild = child;
         }
     }
    */
}
