using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject astroidPrefab;

    [SerializeField] float minTime = 2f;
    [SerializeField] float maxTime = 10f;


    //spawn position
    [SerializeField] protected float startYPosition = 12;


    public bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnObjectsContinuously());
    }

    public void StartMeteorSpawning()
    {
        StartCoroutine(SpawnObjectsContinuously());
    }

    public void StopMeteorSpawning()
    {
        StopCoroutine(SpawnObjectsContinuously());
    }

    IEnumerator SpawnObjectsContinuously()
    {
        while (isSpawning)
        {
            float minX = 0f; // Minimum X position
            float maxX = Screen.width; // Maximum X position
            Vector2 minWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(minX, 0f));
            Vector2 maxWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(maxX, 0f));

            float xPosition = Random.Range(minWorldPos.x, maxWorldPos.x);
            Vector2 startPostion = new Vector2(xPosition, startYPosition);

            transform.position = startPostion;
            // Generate a random delay between 10 and 25 seconds
            float spawnDelay = Random.Range(minTime, maxTime);
            Debug.LogWarning(" wait for spawning");
            // Wait for the random delay
            yield return new WaitForSeconds(spawnDelay);
            Debug.LogWarning("spawning");
            GameObject astroidGO;
            // Spawn the object
            astroidGO = Instantiate(astroidPrefab, startPostion, Quaternion.identity);

        }
 

    }
}
