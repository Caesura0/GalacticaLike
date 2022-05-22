using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    [SerializeField] float sphereSize = 0.2f;

    private void OnDrawGizmos()
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
}
