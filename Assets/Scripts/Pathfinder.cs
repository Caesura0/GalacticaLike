using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    WaveConfigSO waveConfig;
    EnemySpawner enemySpawner;
  
    List<Transform> waypoints = new List<Transform>();
    int waypointIndex = 0;

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
        
    }

    private void Update()
    {
        FollowPath();
    }



    //private void FollowPath()
    //{
    //    if (waypointIndex < waypoints.Count)
    //    {
    //        Vector3 targetPosition = waypoints[waypointIndex].position;
    //        float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;

    //        // Use Bezier curve for smooth movement
    //        transform.position = BezierCurve(transform.position, targetPosition, waypoints[waypointIndex].position + Vector3.up * 2f, delta);

    //        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
    //        {
    //            waypointIndex++;
    //        }
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}


    private Vector3 BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t, float curveIntensity = 0.0f, Vector3 curveDirection = default)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * (p1 + curveIntensity * curveDirection);
        p += 3 * u * tt * (p2 + curveIntensity * curveDirection);
        p += ttt * p2;

        return p;
    }





     private void FollowPath()
     {
         if(waypointIndex < waypoints.Count)
         {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            
            if(transform.position == targetPosition)
            {
                waypointIndex++;
            }
         }
         else
         {
            Destroy(gameObject);
         }

     }
    
}
