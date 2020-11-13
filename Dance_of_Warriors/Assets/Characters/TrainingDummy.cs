using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class TrainingDummy : Character
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Enemy Knight").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        healthMax = 5;
        speed = 5;
        jumpForce = 300;

        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // float randomZ = Random.Range(-walkPointRange, walkPointRange);
        // float randomX = Random.Range(-walkPointRange, walkPointRange);

        float randomZ = Random.Range(-10, 10);
        float randomX = Random.Range(-10, 10);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            die();
        else if (healthMax < health) //if the character has too much health for some reason
            health = healthMax; //reduce their health to the max possible
    }
        // if (health <= 0)
        //     die();
        // else if (healthMax < health) //if the character has too much health for some reason
        //     health = healthMax; //reduce their health to the max possible

        // if(jumpPossible)
        // {
        //     if(isJumping)
        //     {
        //         anim.SetBool("isJumping", false);
        //         anim.SetBool("doneJumping", true);
        //         isJumping = false;
        //     }
        // }
    // }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class TrainingDummy : Character
// {
//     Transform playerTransform;
//     UnityEngine.AI.NavMeshAgent myNav;
//     public float checkRate = 0.001f;
//     float nextCheck;

//     protected override void Start()
//     {
//         if (GameObject.FindGameObjectWithTag("Enemy").activeInHierarchy)
//         {
//             playerTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
//         }

//         myNav = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
//     }

//     private void Update()
//     {
//         if (Time.time > nextCheck)
//         {
//             nextCheck = Time.time + checkRate;
//             FollowPlayer();
//         }
//     }

//     private void FollowPlayer()
//     {
//         myNav.transform.LookAt(playerTransform);
//         myNav.SetDestination(playerTransform.position);
//     }
// }