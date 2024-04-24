using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveConfigListSetup 
{
    [SerializeField] List<EnemySpawner> EnemySpawners;
    [SerializeField] float preWaitTime = 1f;
    [SerializeField] float PostWaitTime = 1f;
}
