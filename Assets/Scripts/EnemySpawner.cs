using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    WaveConfigSO currentWave;
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;


    //check if enough of the enemies are in the pooler
    //if not then we add whatever we need for it
    //otherwise skip



    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }


    IEnumerator SpawnEnemyWaves()
    {
        foreach(WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;

            List<Transform> endFormationList = wave.GetFormationPositionList();

            
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Enemy enemy;
                enemy = Instantiate(currentWave.GetEnemyPrefab(i).gameObject,
                currentWave.GetStartingWaypoint().position,
                Quaternion.Euler(0,0,180), transform).GetComponent<Enemy>();
                Debug.Log(wave.JoinFormationAtEndOfPath + "  ");
                if (wave.JoinFormationAtEndOfPath && endFormationList != null && i < endFormationList.Count)
                {
                    enemy.Spawn(wave, this, endFormationList[i]);
                }
                else
                {
                    enemy.Spawn(wave, this);
                }
                
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }

    }
}
