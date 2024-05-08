using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] protected Transform pathPrefab;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float timeBetweenEnemySpawns = 1f;
    [SerializeField] protected float spawnTimeVariance = 0f;
    [SerializeField] protected float minimunSpawnTime = .2f;


    [SerializeField] protected List<Enemy> enemyPrefabs;


    

    [SerializeField] Transform endFormation;


    //build this location into enemy spawner

    [SerializeField] bool joinFormationAtEndOfPath;
    public bool JoinFormationAtEndOfPath { get { return joinFormationAtEndOfPath; } }




    public List<Transform> GetFormationPositionList()
    {
        if (joinFormationAtEndOfPath)
        {
            List<Transform> endFormationList = new List<Transform>();
            foreach (Transform child in endFormation.GetComponentInChildren<Transform>())
            {
                endFormationList.Add(child);
            }
            return endFormationList;
        }
        return null;
    }

    
    public void SetEnemyPrefabToFormationPoint()
    {

    }


    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
            

        }


        return waypoints;
    }


    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public List<Enemy> GetEnemyPrefabs()
    {

        return enemyPrefabs;
    }

    public Enemy GetEnemyPrefab(int index)
    {

        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimunSpawnTime, float.MaxValue);
    }
}
