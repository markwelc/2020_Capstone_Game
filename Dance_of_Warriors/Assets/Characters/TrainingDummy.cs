using System.Collections;
using System.Collections.Generic;
//used for navmesh agents
using UnityEngine.AI;
using UnityEngine;

public class TrainingDummy : Character
{
    public float lookRadius = 10f;
    public float attackRadius = 4f;

    public NavMeshAgent agent;
    public Transform player;
    public Transform target;
    public LayerMask whatIsGround, whatIsPlayer;

    //patrolling variables
    public Vector3 walkPoint; //finds a point on the map to travel to
    bool walkPointSet; //keeps track of whether a walkpoint has been set
    public float walkPointRange; //range to the walkpoint

    //attacking variables
    public float timeBetweenAttacks; //sets attack frequency
    bool alreadyAttacked; //helps with attack frequency
    public GameObject projectile; //used for attacks

    //states
    public float sightRange, attackRange; //self explanatory
    public bool playerInSightRange, playerInAttackRange; //self explanatory

    private void Awake()
    {
        // player = GameObject.Find("Enemy Knight").transform; //find the position of the player
        whatIsGround = LayerMask.GetMask("staticEnvironment");
        whatIsPlayer = LayerMask.GetMask("player");
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        // healthMax = 5;
        // speed = 5;
        // jumpForce = 300;

        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            transform.LookAt(target.position);
            if (distance <= attackRadius)
            {
                AttackPlayer();
            }
        }
        else
        {
            Patrolling();
        }
    }

    private void Patrolling()
    {
        //finds a walk point for the patrolling behavior
        if (!walkPointSet) SearchWalkPoint();

        //moves the character toward the walkpoint
        if (walkPointSet)
            agent.SetDestination(walkPoint);
            transform.LookAt(walkPoint);

        //keeps track of distance to walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //determines if the character has reached the walkpoint
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Z and X coordinate range for random walkpoint setting
        // float randomZ = Random.Range(-walkPointRange, walkPointRange);
        // float randomX = Random.Range(-walkPointRange, walkPointRange);

        float randomZ = Random.Range(-10, 10);
        float randomX = Random.Range(-10, 10);

        //creates new walkpoint with random X and Z positions, but keeps Y coordinate the same to keep character on the ground
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //keeps walkpoint within bounds of the map
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    //attack player code
    private void AttackPlayer()
    {
        //Debug.Log("Attacking Player");
        //chase the player
        agent.SetDestination(transform.position);

        //look at the player so it doesn't look dumb
        transform.LookAt(target);

        // if character has not already attacked, throw a projectile at them
        if(!alreadyAttacked)
        {
            if(weaponAccess != null)
                useWeapons();
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    // resets the alreadyAttacked variable used in AttackPlayer()
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // //handles taking damage from player
    // public void TakeDamage(int damage)
    // {
    //     //damage health
    //     health -= damage;

    //     //kill character if health == 0
    //     if (health <= 0)
    //         die();
    //     else if (healthMax < health) //if the character has too much health for some reason
    //         health = healthMax; //reduce their health to the max possible
    // }
    //     // if (health <= 0)
    //     //     die();
    //     // else if (healthMax < health) //if the character has too much health for some reason
    //     //     health = healthMax; //reduce their health to the max possible

    //     // if(jumpPossible)
    //     // {
    //     //     if(isJumping)
    //     //     {
    //     //         anim.SetBool("isJumping", false);
    //     //         anim.SetBool("doneJumping", true);
    //     //         isJumping = false;
    //     //     }
    //     // }
    // // }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}