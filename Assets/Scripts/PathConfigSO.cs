using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Path Config", fileName = "New Path Config")]
public class PathConfigSO : ScriptableObject
{
    [SerializeField] private List<Transform> controlPoints;

    public List<Transform> GetControlPoints()
    {
        return controlPoints;
    }





    public Vector3 GetPointOnPath(float t)
    {
        if (controlPoints.Count < 2)
        {
            Debug.LogWarning("Not enough control points to create a path.");
            return Vector3.zero;
        }

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * controlPoints[0].position;
        p += 2 * u * t * controlPoints[1].position;
        p += tt * controlPoints[2].position;

        return p;
    }
}