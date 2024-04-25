using UnityEngine;


public interface IMover 
{
    public void Init(WaveConfigSO waveConfigSO, EnemySpawner enemySpawner, Transform endPosition);
}
