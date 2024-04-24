using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] bool LoopAtEnd;

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

        //waveConfig = enemySpawner.GetCurrentWave();
        //waypoints = waveConfig.GetWaypoints();
        //transform.position = waypoints[waypointIndex].position;
        
    }

    /// <summary>
    /// Initiate the unit pathfinding
    /// </summary>
    /// <param name="waveConfigSO"></param>
    /// <param name="enemySpawner"></param>
    public void Init(WaveConfigSO waveConfigSO, EnemySpawner enemySpawner, Transform endPosition)
    {
        this.enemySpawner = enemySpawner;
        waveConfig = waveConfigSO;
        waypoints = waveConfig.GetWaypoints();
        if(endPosition != null )
        {
            waypoints.Add(endPosition);
        }
        else
        {
            Debug.LogWarning("End position is null. Ensure that the end position is provided for proper initialization.");
        }
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
    {
        FollowPath();
    }




     private void FollowPath()
     {
        if (LoopAtEnd)
        {
            if (waypointIndex < waypoints.Count)
            {
                Vector3 targetPosition = waypoints[waypointIndex].position;
                float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else
            {
                waypointIndex = 0;
            }
        }
        else
        {
            if (waypointIndex < waypoints.Count)
            {
                Vector3 targetPosition = waypoints[waypointIndex].position;
                float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else
            {
                if (!waveConfig.JoinFormationAtEndOfPath)
                {
                    Destroy(gameObject);
                }
                
            }
        }


     }
    
}
