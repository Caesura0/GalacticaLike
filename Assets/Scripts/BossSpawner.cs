using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    bool bossDestroyed = false;
    [SerializeField] Enemy boss;

    [SerializeField] List<GameObject> pathList;

    public void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.Euler(0, 0, 180)) ;
    }



}
