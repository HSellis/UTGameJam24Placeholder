using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elf : MonoBehaviour
{
    private Transform playerTransform;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;

    // state 1: passive, state 2: active
    public int state;

    public Transform centerPoint;   // Center of the circle (set a reference point for the circle's center)
    public float radius = 5.0f;     // Radius of the circle
    public float speed = 2.0f;      // Speed at which the agent moves along the circle
    public float angularSpeed = 1.0f; // How fast the agent moves around the circle (in radians)

    private float currentAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

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
            MoveToPointOnCircle();
        }
        else
        {
            navMeshAgent.SetDestination(playerTransform.position);
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
        navMeshAgent.SetDestination(nextPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            audioSource.Play();
        }
    }
}
