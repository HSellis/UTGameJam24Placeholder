using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elf : MonoBehaviour
{
    private Transform playerTransform;
    public LayerMask obstacleMask;   // Layer mask for obstacles (like walls)

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    // state 1: passive, state 2: active
    public int state;

    public Transform centerPoint;   // Center of the circle (set a reference point for the circle's center)
    public float radius = 5.0f;     // Radius of the circle
    public float speed = 2.0f;      // Speed at which the agent moves along the circle
    public float angularSpeed = 1.0f; // How fast the agent moves around the circle (in radians)

    public float detectionRange = 10f;  // How far the NPC can see
    public float fieldOfViewAngle = 60f; // Field of view angle in degrees (half-angle, i.e., 60 means 120 degrees total)

    private float currentAngle = 0f;

    public float explosionForce = 250;

    private RandomAudioPlayer randomAudioPlayer;
    private Rigidbody rb;
    public AudioClip[] deathAudioClips;
    public AudioClip[] angryAudioClips;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        randomAudioPlayer = GetComponent<RandomAudioPlayer>();
        rb = GetComponent<Rigidbody>();

        playerTransform = Player.Instance.transform;
        state = 1;
        animator.SetFloat("Speed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            // Update the angle over time to make the agent move along the circle
            currentAngle += angularSpeed * Time.deltaTime;

            // Keep the angle within 0 to 2 * Pi (360 degrees) to avoid overflow
            if (currentAngle > Mathf.PI * 2)
            {
                currentAngle -= Mathf.PI * 2;
            }

            // Move the agent to the next position along the circle
            if (centerPoint != null)
            {
                MoveToPointOnCircle();

            }

            if (CanSeePlayer())
            {
                randomAudioPlayer.PlayRandomClip(angryAudioClips);
                state = 2;
            }
        }
        else
        {
            if (navMeshAgent.enabled)
            {
                navMeshAgent.SetDestination(playerTransform.position);
            }
        }
    }

    void MoveToPointOnCircle()
    {
        // Calculate the position of the next point on the circle
        Vector3 nextPoint = new Vector3(
            centerPoint.position.x + Mathf.Cos(currentAngle) * radius,
            centerPoint.position.y,
            centerPoint.position.z + Mathf.Sin(currentAngle) * radius
        );

        // Move the agent towards the next point
        if (navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(nextPoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            navMeshAgent.enabled = false;
            rb.AddExplosionForce(explosionForce, player.transform.position - Vector3.up * 1f, 5);
            randomAudioPlayer.PlayRandomClip(deathAudioClips);
            Destroy(gameObject, 0.75f);
        }
    }

    // Method to check if there is an obstacle between the NPC and the player
    bool IsPlayerBehindObstacle()
    {
        // Direction from NPC to player
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Perform the raycast, using the obstacle mask to detect walls and obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange, obstacleMask))
        {
            // If the ray hits something before reaching the player, it means there's an obstacle in the way
            if (hit.transform != playerTransform)
            {
                Debug.Log("Player is blocked by " + hit.transform.name);
                return true;
            }
        }

        // No obstacle detected
        return false;
    }

    // Method to check if the NPC can see the player
    bool CanSeePlayer()
    {
        // Calculate direction from NPC to player
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0; // Ignore Y axis to only check in 2D (horizontal plane)

        // Calculate distance to the player
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if the player is within detection range
        if (distanceToPlayer > detectionRange)
        {
            return false;
        }

        // Normalize the direction vector
        directionToPlayer.Normalize();

        // Calculate the angle between the NPC's forward direction and the direction to the player
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // Check if the player is within the NPC's field of view
        if (angleToPlayer < fieldOfViewAngle)
        {
            // Perform a raycast to see if there are any obstacles between the NPC and the player
            if (!IsPlayerBehindObstacle())
            {
                // Player is within field of view and no obstacles are blocking sight
                return true;
            }
        }

        // Player is either outside the field of view or behind an obstacle
        return false;
    }
}
