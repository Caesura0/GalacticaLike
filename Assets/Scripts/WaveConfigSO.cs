using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = .2f;
    [SerializeField] List<GameObject> enemyPrefabs;

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

        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public List<GameObject> GetEnemyPrefabs()
    {
        return enemyPrefabs;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }

    // New methods for spawning multiple waves
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] List<int> enemiesPerWave;

    public int GetWaveCount()
    {
        return enemiesPerWave.Count;
    }

    public int GetEnemiesInWave(int waveIndex)
    {
        if (waveIndex < enemiesPerWave.Count)
        {
            return enemiesPerWave[waveIndex];
        }
        return 0;
    }

    public float GetTimeBetweenWaves()
    {
        return timeBetweenWaves;
    }
}
