using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawFormation : MonoBehaviour
{
    [SerializeField] float sphereSize = 0.2f;
    private void OnDrawGizmos()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        for (int i = 1; i < children.Length ; i++)
        {
            Handles.Label(children[i].position, children[i].gameObject.name);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(children[i].position, sphereSize);
        }
    }
}

