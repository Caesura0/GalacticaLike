using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    private int currentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        while (currentWaveIndex < waveConfigs.Count)
        {
            WaveConfigSO currentWave = waveConfigs[currentWaveIndex];
            for (int waveIndex = 0; waveIndex < currentWave.GetWaveCount(); waveIndex++)
            {
                for (int enemyIndex = 0; enemyIndex < currentWave.GetEnemiesInWave(waveIndex); enemyIndex++)
                {
                    Instantiate(
                        currentWave.GetEnemyPrefab(enemyIndex),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180),
                        transform
                    );
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(currentWave.GetTimeBetweenWaves());
            }
            currentWaveIndex++; // Move to the next wave
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        if (currentWaveIndex < waveConfigs.Count)
        {
            return waveConfigs[currentWaveIndex];
        }
        else
        {
            return null; // No more waves available
        }
    }
}
