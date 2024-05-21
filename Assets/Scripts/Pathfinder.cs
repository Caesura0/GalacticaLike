using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour, IMover
{
    [SerializeField] bool LoopAtEnd;

    WaveConfigSO waveConfig;
    EnemySpawner enemySpawner;
  
    public List<Transform> waypoints = new List<Transform>();
    int waypointIndex = 0;

    [SerializeField] bool isBossEnemy;

    public event EventHandler OnPathEnd;


    float moveSpeed;


    private void OnValidate()
    {
#if UNITY_EDITOR
        if (!isBossEnemy)
        {
            
        }
#endif
    }



    private void Awake()
    {
        //enemySpawner = FindObjectOfType<EnemySpawner>();
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
        moveSpeed = waveConfigSO.GetMoveSpeed();
        if(endPosition != null )
        {
            waypoints.Add(endPosition);
        }
        else
        {
            //Debug.LogWarning("End position is null. Ensure that the end position is provided for proper initialization.");
        }
        transform.position = waypoints[waypointIndex].position;
    }

    public void SetPathDirectly(List<Transform> waypoints, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.waypoints = waypoints;
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
                float delta = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else
            {
                waypointIndex = 0;
                OnPathEnd?.Invoke(this,EventArgs.Empty);
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
                if (!waveConfig.JoinFormationAtEndOfPath && !isBossEnemy)
                {      
                    Destroy(gameObject);
                }
                
            }
        }


     }
    
}
