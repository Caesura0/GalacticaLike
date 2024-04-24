using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPathLine : MonoBehaviour
{

    public List<Transform> pathPoints;
    

    private void Start()
    {
        pathPoints = GetComponentsInChildren<Transform>().ToList<Transform>();
    }
}
