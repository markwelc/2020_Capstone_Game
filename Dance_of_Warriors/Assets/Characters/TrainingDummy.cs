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
    public bool dash;
    float distance;

    //patrolling variables
    public Vector3 walkPoint; //finds a point on the map to travel to
    bool walkPointSet; //keeps track of whether a walkpoint has been set
    public float walkPointRange; //range to the walkpoint

    //attacking variables
    public float timeBetweenAttacks; //sets attack frequency
    bool alreadyAttacked; //helps with attack frequency
    public GameObject projectile; //used for attacks
    private int numOfInRangeActions = 2;
    //states
    private NewPlayer targetPlayer;
    private int crouchAmount = 0;
    private bool playerDefeatedCalled;

    // Can grow if needed
    // 0 = patrolling
    // 1 = following
    // 2 = inRange
    private int enemyState;

    // Music stuff
    //[Header("Beat Settings")]
    //[Range(0, 3)]
    //public int onFullBeat;
    //[Range(0, 7)]
    //public int[] onBeatD8;
    //private int beatCountFull;
    [Header("Beat Settings")]
    [Range(1, 32)] public int[] active32ndNotes;
    private bool isOnBeat;
    private bool blockBroken;
    private bool alreadyCheckingAttack;



    private void Awake()
    {
        // player = GameObject.Find("Enemy Knight").transform; //find the position of the player
        whatIsGround = LayerMask.GetMask("staticEnvironment");
        whatIsPlayer = LayerMask.GetMask("player");
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        dashSpeed = new float[1];
        dashSpeed[0] = 11f;

        target = PlayerManager.instance.player.transform;
        targetPlayer = player.GetComponent<NewPlayer>();
        
        
        agent = GetComponent<NavMeshAgent>();

        equippedWeapon = 0; //this is the starting value
        numOfInRangeActions = 2;
        anim.SetFloat("turn", 0);


    }

    /**
     * Handle movement speed, getting relation to play, and checking beat
     * Also handles defeated operation
     */
    private void Update()
    {
        // Distance to player
        distance = Vector3.Distance(target.position, transform.position);

        // need this check because agent destroyed when dead
        if (!isDead)
        {
            // Get speed for agent ini conjunction with any limits from health debufs. either regular or dash speed
            if (dash)
                agent.speed = playerHealthManager.characterSpeed * dashSpeed[0];
            else 
                agent.speed = playerHealthManager.characterSpeed * 3.5f;

            // Check beat
            checkBeat();
            if (targetPlayer.isDead && !playerDefeatedCalled)
            {
                playerDefeatedCalled = true;
                playerDefeated();
            }
        }
        else
        {
            // Complete enemy specific death operation
            if(agent != null) // make sure only called once
                enemyDefeated();
        }


        
    }

    /**
     * Late update to get AI current speed for animator
     * No params just controls movement animator
     */
    private void LateUpdate()
    {
        if (!isDead)
        {
            float currentSpeed = this.agent.velocity.magnitude;
            anim.SetFloat("turn", agent.velocity.normalized.y);
        }
    }


    /**
     * Handles moving the player
     * and setting their current state
     * Overriden from charcter class fixedupdate
     */
    protected override void handleMovement()
    {

        if (!isDead)
        {
            if (distance <= lookRadius)
            {
                if (distance > attackRadius && !isBlocking) // In attack range dont want to move but do want to set to look at player
                {
                    enemyState = 1;
                    agent.SetDestination(target.position);
                }
                else if (distance < attackRadius &&  !dash)
                {
                    trackPlayerAttack();
                    agent.ResetPath();
                }
            }
            else
            {
                if(!isBlocking) // Just a check. likely this will never happen but just to be safe
                    Patrolling();
                enemyState = 0;
            }
        }
    }

    /**
     * Handle weapon access, called in character update
     * if the distance is less than attack radius the enemy can attack
     */
    protected override void handleWeapons()
    {
        if (distance <= attackRadius)
        {
            enemyState = 2;
        }
    }

    /**
     * This checks based on our music analyzer class
     * if all cases are satisified an on beat action is possible
     * can then call on beat action function to select one approprite
     */
    void checkBeat()
    {
        bool takeAction = false;

        for (int i = 0; i < active32ndNotes.Length && !takeAction; i++) //go through all the 32nd notes that we do something on
        {
            if (musicAnalyzer.count == active32ndNotes[i]) //if we're on a 32nd note that we can do something on
            {
                onBeatAction();//do something

                takeAction = true;//we can stop going through the array since only one positive is possible (or useful at least) and other negatives don't matter
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
        //Debug.Log("Select patrol action");
    }

    /**
     * This will be called on beat.
     * They are following the player so what can they do?
     */
    private void selectFollowingAction()
    {
        //Debug.Log("Select following action");
        //shoot();
        initDashForward();

    }

    /**
     * This will be called on beat.
     * They are in range so what can they do?
     */
    private void selectInRangeAction()
    {
        isOnBeat = true;
        //chase the player
        agent.SetDestination(transform.position);

        //look at the player so it doesn't look dumb
        transform.LookAt(target);

        if (!alreadyAttacked && distance < attackRadius) // Double check still in range 
        {
            int rand = Random.Range(1, numOfInRangeActions - 1); // just picking a random for now. can fine tune later
            alreadyAttacked = true;
            switch (rand)
            {
                case 1:
                    AttackPlayer();
                    break;
                default:
                    break;
            }


            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    /**
     * Shoot
     * Experimental not implemented currently
     */
    void shoot()
    {

        // Not working do we want to set up animation rigging for the enemy? when using gun point at the player?
        if (equippedWeapon != 0)
            cycleWeapon();
        
        // be sure they actually have access
        if (weaponAccess != null)
        {
            // Doing random range to select the type they want
            // since it uses else for standard attack
            // this is just to lower the probability of a heavy attack
            useWeapons(1, playerHealthManager.characterDamageModifier);
        }
    }

    /**
     * Make enemy dash
     * No params, sets dash true and invincible during dash
     */
    void initDashForward()
    {
        anim.SetTrigger("isDashing");
        dash = true;
        playerHealthManager.setInvincible(true);
        agent.speed = dashSpeed[0];

        Invoke(nameof(ResetAttack), 0.25f); // 0.25 seems to work well for dodge
        // want to dodge left right back etc?
        // could change target location for the nav mesh 
        // apply that to move vector
        // honestly possible just being able to get the move pos from the agent set destination may be best
        // than we can control everything manually and do what we want instead of just relying on nav mesh
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
     * Track player attack
     * Enemy AI knows when attack happens 
     * so decide if we let it hit or do something about it
     */
    private void trackPlayerAttack()
    {
        // If in attack range and we are not attacking and we are not blocking
        if(enemyState == 2 && !inAttackMotion && !isBlocking && !alreadyCheckingAttack)
        {
            // If player attacks you and is not on beat
            if (targetPlayer.inAttackMotion && !isOnBeat)
            {
                alreadyCheckingAttack = true;
                // Create a chance that enemy blocks
                // Exclusive so 25% chance they block attack if off beat
                int chanceBlock = Random.Range(1, 5);
                if (chanceBlock == 1)
                {
                    Block(); // We can block
                }
                Invoke(nameof(ResetCheck), 0.5f); // reset check
            }
        }
    }

    /**
     * Initialize block anim and set invincible for one hit
     */
    private void Block()
    {
        if (!isBlocking)
        {
            blockBroken = false;
            anim.SetTrigger("isBlocking");

            // Trigger wern't restting for some reason before reset now
            anim.ResetTrigger("doneBlocking");
            anim.ResetTrigger("breakBlock");
            // They are invincible at start
            isBlocking = true;
            playerHealthManager.setInvincible(true);
            Invoke(nameof(ResetBlock), 1f); // stay crouched for 0.5 seconds
        }
        
    }

    /**
     * They stopped blocking but was never broken
     */
    void endBlock()
    {
        //Debug.Log("End Block");
        // They released so end the block no longer invincible
        isBlocking = false;
        if (!blockBroken)
        {
            anim.SetTrigger("doneBlocking");
            playerHealthManager.setInvincible(false);
        }
    }


    /**
     * If the block has been broken
     */
    protected override void breakBlock()
    {
        if (isBlocking)
        {
            // only blocks one time reset back
            playerHealthManager.setOneTimeBlock(false);
            //   Debug.Log("Got Hit break block");
            anim.SetTrigger("breakBlock"); // break block animation then transition back to standard

            // no longer invincible or blocking
            //isBlocking = false;
            blockBroken = true;
            playerHealthManager.setInvincible(false);
            Invoke(nameof(ResetBlock), 0.5f); // stay crouched for 0.5 seconds
        }
    }


    /**
     * Attack the player
     * this will need to be changed later depending
     * on how attacks are handled
     * Maybe change weapon to be used for attack by feeding in variable
     */
    private void AttackPlayer()
    {

        // be sure they actually have access
        if (weaponAccess != null)
        {
            // Doing random range to select the type they want
            // since it uses else for standard attack
            // this is just to lower the probability of a heavy attack
            int weaponChoice = Random.Range(3, 5); //because this is the integer version, max is exclusive
                                                   //Debug.Log("weaponChoice = " + weaponChoice);
            useWeapons(weaponChoice, playerHealthManager.characterDamageModifier);

        }
    }

    /**
     * Handles resetting certain operation after attacking
     * called from invoke method
     * No Params
     */ 
    private void ResetAttack()
    {
        isOnBeat = false;
        alreadyAttacked = false;
        /*if(isBlocking)
        {
            endBlock();
        }*/
        if(dash)
        {
            anim.SetTrigger("doneDashing");
            anim.SetBool("isDashing", false);
            playerHealthManager.setInvincible(false);
            dash = false;
            //if (agent.speed > 5)
            //{
                agent.speed = speed;
            //}
        }
    }

    /**
     * Handles resetting block
     * called from invoke method
     * No Params
     */ 
    private void ResetBlock()
    {
        if(isBlocking)
        {
            endBlock();
        }
    }

    /**
     * Handles resetting check to see if player is attacking enemy
     * called from invoke method
     * No Params
     */
    private void ResetCheck()
    {
        alreadyCheckingAttack = false;
    }

    /**
     * The enemy successfully defeated the player
     * start crouching over player to assert dominance
     */
    private void playerDefeated()
    {
        // so what now? yep ya guessed it
        if (crouchAmount < 100)
        {
            crouch(); // start crouch
            Invoke(nameof(CrouchDelay), 0.5f); // stay crouched for 0.5 seconds
        }
    }

    /**
     * Used as invoke to delay
     */
    private void CrouchDelay()
    {
        endCrouch(); // endcrouch
        Invoke(nameof(crouchAgain), 0.5f); // stay upright for 0.5 seconds
    }

    /**
     * Used as invoke to crouch again
     */
    private void crouchAgain()
    {
        crouchAmount++; // ncrement teabag amount
        playerDefeated(); // get the next teabag
    }

    /**
     * Enemy specific death operation
     * remove agent to stop access attempts
     */
    private void enemyDefeated()
    {
        Destroy(agent);
        targetPlayer.playerWins();
    }

    protected override void handleAngle()
    {
        //don't set any angle, let the two transform.LookAt lines (in handleMovement and Patrolling) do it
    }

    /**
     * Debug to visualize radius
     */ 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
