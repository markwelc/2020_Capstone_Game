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

    float distance;

    //patrolling variables
    public Vector3 walkPoint; //finds a point on the map to travel to
    bool walkPointSet; //keeps track of whether a walkpoint has been set
    public float walkPointRange; //range to the walkpoint

    //attacking variables
    public float timeBetweenAttacks; //sets attack frequency
    bool alreadyAttacked; //helps with attack frequency
    public GameObject projectile; //used for attacks

    //states

    // Can grow if needed
    // 0 = patrolling
    // 1 = following
    // 2 = inRange
    private int enemyState;

    // Music stuff
    [Header("Beat Settings")]
    [Range(0, 3)]
    public int onFullBeat;
    [Range(0, 7)]
    public int[] onBeatD8;
    private int beatCountFull;


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
        
        distance = Vector3.Distance(target.position, transform.position);
        if (!isDead)
        {
            checkBeat();
            
        }
        else
        {
            // Complete enemy specific death operation
            enemyDefeated();
        }
    }


    /**
     * Handles moving the player
     * and setting their current state
     */
    protected override void handleMovement()
    {
        if (!isDead)
        {
            if (distance <= lookRadius)
            {
                if (distance > attackRadius) // In attack range dont want to move but do want to set to look at player
                {
                    enemyState = 1;
                    agent.SetDestination(target.position);
                }
                transform.LookAt(target.position);
            }
            else
            {
                Patrolling();
                enemyState = 0;
            }
        }
    }

    /**
     * This checks based on our music analyzer class
     * if all cases are satisified an on beat action is possible
     * can then call on beat action function to select one approprite
     */
    void checkBeat()
    {
        // Loop in 4 steps
        beatCountFull = musicAnalyzer.beatCountFull % 4;

        // on beat divded by 8 to get our 4 steps
        for (int i = 0; i < onBeatD8.Length; i++)
        {
            // 3 cases
            // is timer greater than interval
            // is it a full beat
            // is the ccount equal to that in question 
            if (musicAnalyzer.beatD8 && beatCountFull == onFullBeat && musicAnalyzer.beatCountD8 % 8 == onBeatD8[i])
            {
                //Debug.Log("Do move");
                // can do something
                onBeatAction();
            }
        }

    }

    /**
     * This happens on the third beat
     * Where are they in relation to player?
     * Slect action based on where they are
     * NOTE: will need to handle to do something in between beats 
     * otherwise the player will be in range but just not be doing anything until 3rd beat
     */
    private void onBeatAction()
    {
        // Overall idea
        // Enemy has a list of available actions for each case
        // randomly select one depending on case
        // maybe less randomly if we can check what the player is doing
        switch(enemyState)
        {
            case 0: // Patrolling
                selectPatrollingAction();
                break;
            case 1: // Following
                selectFollowingAction();
                break;
            case 2: // In player range
                selectInRangeAction();
                break;
            default:
                Debug.Log("Unrecognizable enemy state");
                break;
        }
    }

    /**
     * This will be called on beat.
     * They are patrolling so what can the do for an action?
     */
    private void selectPatrollingAction()
    {
        Debug.Log("Select patrol action");
    }

    /**
     * This will be called on beat.
     * They are following the player so what can they do?
     */
    private void selectFollowingAction()
    {
        Debug.Log("Select following action");
    }

    /**
     * This will be called on beat.
     * They are in range so what can they do?
     */
    private void selectInRangeAction()
    {
        Debug.Log("Select in range action");
        AttackPlayer();
    }

    /**
     * They are in range
     * set state to notify
     */
    protected override void handleWeapons()
    {
        if (distance <= attackRadius)
        {
            enemyState = 2;
        }
    }

    /**
     * Enemy is randomly patrolling
     */
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

    /**
     * They are patrolling
     * now search for a random patrol point
     */
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

    
    /**
     * Attack the player
     * this will need to be changed later depending
     * on how attacks are handled
     * Maybe change weapon to be used for attack by feeding in variable
     */
    private void AttackPlayer()
    {
        //Debug.Log("Attacking Player");
        //chase the player
        agent.SetDestination(transform.position);

        //look at the player so it doesn't look dumb
        transform.LookAt(target);

        // if character has not already attacked, throw a projectile at them
        if (!alreadyAttacked)
        {
            if (weaponAccess != null)
                useWeapons(1);
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

    private void enemyDefeated()
    {
        Destroy(agent);
    }

    protected override void handleAngle()
    {
        //don't set any angle, let the two transform.LookAt lines (in handleMovement and Patrolling) do it
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}