using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : Character
{
    public float lookRadius = 10f;

    public Transform target;
    public NavMeshAgent agent;

    // public Vector3 walkPoint; //finds a point on the map to travel to
    // bool walkPointSet; //keeps track of whether a walkpoint has been set
    // public float walkPointRange; //range to the walkpoint

    // public LayerMask whatIsGround, whatIsPlayer;


    // Start is called before the first frame update
    protected override void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        // whatIsGround = LayerMask.GetMask("staticEnvironment");
        // whatIsPlayer = LayerMask.GetMask("player");

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            transform.LookAt(target.position);
        }
        // else
        // {
        //     Patrolling();
        // }
    }

    // private void Patrolling()
    // {
    //     //finds a walk point for the patrolling behavior
    //     if (!walkPointSet) SearchWalkPoint();

    //     //moves the character toward the walkpoint
    //     if (walkPointSet)
    //         agent.SetDestination(walkPoint);

    //     //keeps track of distance to walkpoint
    //     Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //     //determines if the character has reached the walkpoint
    //     if (distanceToWalkPoint.magnitude < 1f)
    //         Debug.Log("Walkpoint is unset");
    //         walkPointSet = false;
    // }

    // private void SearchWalkPoint()
    // {
    //     //Z and X coordinate range for random walkpoint setting
    //     // float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //     // float randomX = Random.Range(-walkPointRange, walkPointRange);

    //     float randomZ = Random.Range(-20, 20);
    //     float randomX = Random.Range(-20, 20);

    //     //creates new walkpoint with random X and Z positions, but keeps Y coordinate the same to keep character on the ground
    //     walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //     //keeps walkpoint within bounds of the map
    //     if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
    //         Debug.Log("Walkpoint is set to " + walkPoint);
    //         walkPointSet = true;
    // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
