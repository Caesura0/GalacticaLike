using UnityEngine;

public class AIRandomMovement : MonoBehaviour
{
    public float minMoveSpeed = 3f;  // Minimum movement speed
    public float maxMoveSpeed = 6f;  // Maximum movement speed
    public float minWaitTime = 1f;   // Minimum wait time
    public float maxWaitTime = 3f;   // Maximum wait time

    private Vector3 targetPosition;  // Current target position
    private bool isMoving = false;   // Flag to track if the AI is currently moving
    private float waitTimer = 0f;    // Timer to track waiting time

    float currentWaitTime;
    void Start()
    {
        // Start moving to a new random position
        MoveToRandomPosition();
    }

    void Update()
    {
        // If AI is moving
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Random.Range(minMoveSpeed, maxMoveSpeed) * Time.deltaTime);

            // If AI reaches the target position
            if (transform.position == targetPosition)
            {
                // Start waiting
                waitTimer += Time.deltaTime;
                if (waitTimer >= currentWaitTime)
                {
                    // Reset the timer and move to a new random position
                    waitTimer = 0f;
                    MoveToRandomPosition();
                }
            }
        }
    }

    // Move to a new random position on the screen
    void MoveToRandomPosition()
    {
        // Generate random position within the screen bounds
        targetPosition = new Vector3(Random.Range(0f, Screen.width), Random.Range(0f, Screen.height), 0f);
        targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
        targetPosition.z = 0f;  // Ensure z position is 0

        // Start moving towards the new position
        isMoving = true;
        Random.Range(minWaitTime, maxWaitTime);
    }
}
