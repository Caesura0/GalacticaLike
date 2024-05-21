using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : MonoBehaviour, IDamagable
{
    [SerializeField] List<Transform> pathList;
    //might want this to be a list of pathobjects
    // then we can add parameters like 

    [SerializeField] float moveSpeed = 4f;

    [SerializeField] Transform mainPath;
    [SerializeField] Transform leftPath;
    [SerializeField] Transform rightPath;


    [SerializeField] Health leftArm;
    [SerializeField] Health rightArm;
    [SerializeField] Health mainBody;

    public bool rightArmDestroyed;
    public bool leftArmDestroyed;
    public bool mainBodyDestroyed;

    Transform currentPath;

    int currentPathCounter;

    Pathfinder pathfinder;
    Health health;

    

    private void Start()
    {
        health = GetComponent<Health>();
        pathfinder = GetComponent<Pathfinder>();
        currentPath = mainPath;
        pathfinder.SetPathDirectly(GetWaypoints(currentPath), moveSpeed);
        pathfinder.OnPathEnd += Pathfinder_OnPathEnd;

        leftArm.onDeath += LeftArm_onDeath;
        rightArm.onDeath += RightArm_onDeath;
        mainBody.onDeath += MainBody_onDeath;
    }



    private void Update()
    {
        if(rightArmDestroyed && leftArmDestroyed && mainBodyDestroyed)
        {
            Destroy(gameObject);
        }
    }

    private void MainBody_onDeath(object sender, System.EventArgs e)
    {
        Debug.Log("main ");
        mainBodyDestroyed = true;
        mainBody.GetComponent<ShooterEx>().enabled = false;
        mainBody.onDeath -= MainBody_onDeath;
        
    }

    private void RightArm_onDeath(object sender, System.EventArgs e)
    {
        Debug.Log("right ");
        rightArmDestroyed = true;
        rightArm.GetComponent<ShooterEx>().enabled = false;
        rightArm.onDeath -= RightArm_onDeath;
        Destroy(rightArm.gameObject);
    }

    private void LeftArm_onDeath(object sender, System.EventArgs e)
    {
        Debug.Log("left ");
        leftArmDestroyed = true;
        leftArm.GetComponent<ShooterEx>().enabled = false;
        leftArm.onDeath -= LeftArm_onDeath;
        Destroy(leftArm.gameObject);

    }

    private void Pathfinder_OnPathEnd(object sender, System.EventArgs e)
    {
        SwitchMovement();
    }


    public void TakeDamage(int damage)
    {
        if (health != null)
        {
            health.TakeDamage(damage); // Call the Damage method of the Health component attached to the enemy
        }
        else
        {
            Debug.LogWarning("Health component not found on enemy!");
        }
    }


    void SwitchMovement()
    {

        int randomIndex = Random.Range(0, pathList.Count);
        // Set the current path to a random path from the pathList
        currentPath = pathList[randomIndex];
        pathfinder.SetPathDirectly(GetWaypoints(currentPath), moveSpeed);
    }


    public List<Transform> GetWaypoints(Transform pathPrefab)
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in pathPrefab)
        {
            if (child != pathPrefab)
            {
                waypoints.Add(child);
            }
        }

        return waypoints;
    }

}
