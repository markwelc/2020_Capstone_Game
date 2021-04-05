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
    private int teabagAmount = 0;
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
        // speed = enemyHealthController.characterSpeed * 3.5f;

        target = PlayerManager.instance.player.transform;
        targetPlayer = player.GetComponent<NewPlayer>();
        
        
        agent = GetComponent<NavMeshAgent>();
        // healthMax = 5;
        // speed = 5;
        // jumpForce = 300;
        /*
        // Tool Added stuff
        toolActionState = actionState.inactive;
        usingTool = 0;
        toolStates = new int[4];
        toolStates[0] = 0;  //length of telegraph
        toolStates[1] = 0;  //length of action
        toolStates[2] = 0;  //length of recovery
        toolStates[3] = 0;  //length of tool cooldown
        toolUsed = 0;
        // End tools
        */

        equippedWeapon = 0; //this is the starting value
        numOfInRangeActions = 2;


    }

    // Update is called once per frame
    private void Update()
    {
        // need this check because agent destroyed when dead
        if (!isDead)
        {
            if (dash)
                agent.speed = playerHealthManager.characterSpeed * dashSpeed[0];
            else 
                agent.speed = playerHealthManager.characterSpeed * 3.5f;
        }

        distance = Vector3.Distance(target.position, transform.position);
        if (!isDead)
        {
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
                if (distance > attackRadius && !isBlocking) // In attack range dont want to move but do want to set to look at player
                {
                    enemyState = 1;
                    agent.SetDestination(target.position);
                }
                else if (distance < attackRadius && !dash)
                {
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
     * This checks based on our music analyzer class
     * if all cases are satisified an on beat action is possible
     * can then call on beat action function to select one approprite
     */
    void checkBeat()
    {
        //// Loop in 4 steps
        //beatCountFull = musicAnalyzer.beatCountFull % 4;

        //// on beat divded by 8 to get our 4 steps
        //for (int i = 0; i < onBeatD8.Length; i++)
        //{
        //    // 3 cases
        //    // is timer greater than interval
        //    // is it a full beat
        //    // is the ccount equal to that in question
        //    if (musicAnalyzer.beatD8 && beatCountFull == onFullBeat && musicAnalyzer.beatCountD8 % 8 == onBeatD8[i])
        //    {
        //        //Debug.Log("Do move");
        //        // can do something
        //        onBeatAction();
        //    }
        //}

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
     * This will be called on beat.
     * They are in range so what can they do?
     */
    private void selectInRangeAction()
    {
        //chase the player
        agent.SetDestination(transform.position);

        //look at the player so it doesn't look dumb
        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            int rand = Random.Range(1, numOfInRangeActions + 1); // just picking a random for now. can fine tune later
            alreadyAttacked = true;
            switch (rand)
            {
                case 1:
                    AttackPlayer();
                    break;
                case 2:
                    Block();
                    break;
                default:
                    break;
            }

            
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
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

    private void Block()
    {
            anim.SetTrigger("isBlocking");

            // Trigger wern't restting for some reason before reset now
            anim.ResetTrigger("doneBlocking");
            anim.ResetTrigger("breakBlock");
            // They are invincible at start
            isBlocking = true;
            playerHealthManager.setInvincible(true);
        
    }

    /**
     * They stopped blocking but was never broken
     */
    void endBlock()
    {
        //Debug.Log("End Block");
        // They released so end the block no longer invincible
        anim.SetTrigger("doneBlocking");
        isBlocking = false;
        playerHealthManager.setInvincible(false);
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
            isBlocking = false;
            playerHealthManager.setInvincible(false);
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
        //Debug.Log("Attacking Player");
        //if (equippedWeapon != 1)
            //cycleWeapon();

        // if character has not already attacked, throw a projectile at them
        
       
           // be sure they actually have access
            if (weaponAccess != null)
            {
            // Doing random range to select the type they want
            // since it uses else for standard attack
            // this is just to lower the probability of a heavy attack
                int weaponChoice = Random.Range(1, 3); //because this is the integer version, max is exclusive
                //Debug.Log("weaponChoice = " + weaponChoice);
                useWeapons(weaponChoice, playerHealthManager.characterDamageModifier);

            }
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //alreadyAttacked = true;
          //  Invoke(nameof(ResetAttack), timeBetweenAttacks);
        //}
    }

    // resets the alreadyAttacked variable used in AttackPlayer()
    private void ResetAttack()
    {
        alreadyAttacked = false;
        if(isBlocking)
        {
            endBlock();
        }
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
     * The enemy successfully defeated the player
     */
    private void playerDefeated()
    {
        // so what now? yep ya guessed it
        if (teabagAmount < 100)
        {
            crouch(); // start crouch
            Invoke(nameof(CrouchDelay), 0.5f); // stay crouched for 0.5 seconds
        }
    }

    private void CrouchDelay()
    {
        endCrouch(); // endcrouch
        Invoke(nameof(crouchAgain), 0.5f); // stay upright for 0.5 seconds
    }

    private void crouchAgain()
    {
        teabagAmount++; // ncrement teabag amount
        playerDefeated(); // get the next teabag
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
