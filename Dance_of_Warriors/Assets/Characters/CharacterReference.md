<h3>Character Reference</h3>
<ul>
    <li>CameraLook.cs</li>
        <ul>
            <li>Defines behavior of camera movement in game.</li>
            <li>private void Awake()</li>
                <ul>
                    <li>Initializes controls handlers and references to objects used in scripts here.</li>
                </ul>
            <li>void OnEnable()</li>
                <ul>
                    <li>Handles enabling the player controls.</li>
                </ul>
            <li>void OnDisable()</li>
                <ul>
                    <li>Handles disabling the player controls.</li>
                </ul>
            <li>void Update()</li>
                <ul>
                    <li>Handles camera rotation and zooming.</li>
                </ul>
            <li>public void setXLookScale(float xLookScale)</li>
                <ul>
                    <li>Sets the look scale for the x direction.</li>
                    <li>param xLookScale: scale taken from the settings menu to set x look scale.</li>
                </ul>
            <li>public void setYLookScale(float yLookScale)</li>
                <ul>
                    <li>Sets the look scale for the y direction.</li>
                    <li>param yLookScale: scale take from the settings menu to set y look scale.</li>
                </ul>
            <li>public void AddRecoil()</li>
                <ul>
                    <li>Changes camera look direction when weapon is fired to resemble recoil.</li>
                </ul>
            <li>void RotateCamera()</li>
                <ul>
                    <li>Handles camera rotation based on mouse or controller right stick input.</li>
                </ul>
            <li>private void zoomIn()</li>
                <ul>
                    <li>Handles smooth camera transition for zooming in when using a gun.</li>
                </ul>
            <li>private void zoomOut()</li>
                <ul>
                    <li>Handles smooth camera transition for zooming out when using a gun.</li>
                </ul>
            <li>private void fovTransition(float end)</li>
                <ul>
                    <li>Handles smooth FOV adjustment.</li>
                    <li>param end: The target FOV to stop the transition at.</li>
                </ul>
            <li>void enableTargetCross(Color32 color)</li>
                <ul>
                    <li>Changes the reticle color when aiming at an enemy.</li>
                    <li>param color: The color to change the reticle to.</li>
                </ul>
        </ul>
    <p><p>
    <li>Character.cs</li>
        <ul>
            <li>Defines basic behavior for all characters within game.</li>
            <li>protected virtual void Start()</li>
                <ul>
                    <li>Initializes character position, health, rigidbody values.</li>
                </ul>
            <li>void FixedUpdate()</li>
                <ul>
                    <li>Maintains default behavior of a character.</li>
                    <li>Tracks movement and health/stamina values.</li>
                </ul>
            <li>protected void moveCharacter(Vector3 direcAndDist)</li>
                <ul>
                    <li>Handles moving the character in a specific direction and for a specific distance.</li>
                    <li>param direcAndDist: vector3 determing direction and distance for character to move.</li>
                </ul>
            <li>protected virtual void handleMovement()</li>
                <ul>
                    <li>Meant to be overridden in child classes. Defines default behavior (does not try to move anywhere).</li>
                </ul>
            <li>protected virtual void handleJump()</li>
                <ul>
                    <li>Does nothing. Meant to be overridden.</li>
                </ul>
            <li>protected virtual void handleAngle()</li>
                <ul>
                    <li>Meant to be overridden in child classes. Defines default angle for character to face.</li>
                </ul>
            <li>protected virtual void handleWeapons()</li>
                <ul>
                    <li>Meant to be overridden in child classes. Does nothing.</li>
                </ul>
            <li>protected virtual void crouch()</li>
                <ul>
                    <li>Turns on crouching animation (Crouches player).</li>
                </ul>
            <li>protected virtual void endCrouch()</li>
                <ul>
                    <li>Turns off crouching animation (stands character up).</li>
                </ul>
            <li>protected void handleSprint()</li>
                <ul>
                    <li>Handles stamina and sprinting checks to determine if character can continue sprinting.</li>
                </ul>
            <li>protected void handleStamina()</li>
                <ul>
                    <li>Handles stamina regen when character is not sprinting.</li>
                </ul>
            <li>protected virtual void useWeapons(int attackType, float characterDamageModifier)</li>
                <ul>
                    <li>Handles weapon use for a character.</li>
                    <li>param attackType: values 1 or 2, corresponds to light or heavy attack.</li>
                    <li>param characterDamageModifier: modifies character's damage output based on damaged limbs.</li>
                </ul>
            <li>public void weaponDamageAllowed()</li>
                <ul>
                    <li>Determines when weapon damage is allowed.</li>
                </ul>
            <li>public void weaponNoDamageAllowed()</li>
                <ul>
                    <li>Determines when weapon damage is not allowed.</li>
                </ul>
            <li>protected void initiateTool(int toolNum)</li>
                <ul>
                    <li>Starts using a tool.</li>
                    <li>param toolNum: array index of tool to use.</li>
                </ul>
            <li>protected void toolUse()</li>
                <ul>
                    <li>Moves through all tool action states.</li>
                </ul>
            <li>protected void cycleWeapon()</li>
                <ul>
                    <li>Changes currently equipped weapon to next one in available weapons array.</li>
                </ul>
            <li>protected virtual void OnCollisionStay(Collision collision)</li>
                <ul>
                    <li>Sets jumpPossible to true.</li>
                </ul>
            <li>protected virtual void OnCollisionExit(Collision collision)</li>
                <ul>
                    <li>Sets jumpPossible to false.</li>
                </ul>
            <li>protected bool dashAllowed()</li>
                <ul>
                    <li>Returns a bool determining if dashing is allowed by character.</li>
                </ul>
            <li>protected bool toolAllowed()</li>
                <ul>
                    <li>Returns a bool determing if tool use is allowed.</li>
                </ul>
            <li>protected bool groundCheck()</li>
                <ul>
                    <li>Casts raycast from character's feet to ground. Character is grounded if raycast hits static environment.</li>
                </ul>
            <li>protected bool jumpAllowed()</li>
                <ul>
                    <li>Returns a bool determining if jumping is allowed by character.</li>
                </ul>
            <li>protected bool sprintAllowed(Vector2 move)</li>
                <ul>
                    <li>Determines whether sprinting is allowed.</li>
                    <li>param move: character's movement vector to ensure they are moving before allowing sprinting.</li>
                </ul>
            <li>protected virtual bool continueSprintAllowed()</li>
                <ul>
                    <li>Does nothing, returns false.</li>
                </ul>
            <li>protected bool crouchAllowed()</li>
                <ul>
                    <li>Returns whether the character is allowed to crouch.</li>
                </ul>
            <li>public float getHealth()</li>
                <ul>
                    <li>Returns the character's health</li>
                </ul>
            <li>protected virtual void breakBlock()</li>
                <ul>
                    <li>Overridden in child class</li>
                </ul>
            <li>protected virtual void endSprint()</li>
                <ul>
                    <li>Overridden in child class</li>
                </ul>
            <li>private void checkIsDead()</li>
                <ul>
                    <li>Check if the character is dead.</li>
                </ul>
            <li>protected char getCurrentWeaponType()</li>
                <ul>
                    <li>Returns the current type of weapon being used.</li>
                    <li>returns 'm' if the currently equipped weapon is a melee weapon</li>
                    <li>returns 'g' if it's a gun</li>
                    <li>returns '\0' otherwise</li>
                </ul>
            <li>protected Transform getCurrentWeapon()</li>
                <ul>
                    <li>Returns the current position of the currently equipped weapon.</li>
                </ul>
            <li>protected void toggleAnimRigging(bool turnOn, bool changeParent)</li>
                <ul>
                    <li>Toggles animation rigging for current weapon when weapon is a gun.</li>
                    <li>param turnOn: boolean determining whether to turn on or off animation rigging.</li>
                    <li>param changeParent: boolean determining whether to </li>
                </ul>
            <p><p>
            Parent class to:
            <ul>
                <li>NewPlayer.cs</li>
                    <ul>
                        <li>private void Awake()</li>
                            <ul>
                                <li>Initializes player controls.</li>
                            </ul>
                        <li>void OnEnable()</li>
                            <ul>
                                <li>Handles enabling player controls.</li>
                            </ul>
                        <li>void OnDisable()</li>
                            <ul>
                                <li>Handles disabling player controls.</li>
                            </ul>
                        <li>protected override void Start()</li>
                            <ul>
                                <li>Initializes toolstates, action states, movement speed, health, etc.</li>
                            </ul>
                        <li>void Update()</li>
                            <ul>
                                <li>Keeps track of player's position (in air or on ground), and determines player's movement speed.</li>
                            </ul>
                        <li>protected override void handleMovement()</li>
                            <ul>
                                <li>Determines if player is moving according to standard movement rules or dashing movement rules.</li>
                            </ul>
                        <li>private void standardMovement()</li>
                            <ul>
                                <li>Handles player movement in respect to the direction and camera</li>
                                <li>gets our current move location and transforms the players position each frame</li>
                            </ul>
                        <li>protected override void handleAngle()</li>
                            <ul>
                                <li>Overrides parent handleAngle() function.</li>
                                <li>Faces player character toward movement direction.</li>
                            </ul>
                        <li>private void Jump()</li>
                            <ul>
                                <li>Called when player presses jump button.</li>
                                <li>Calls jumping, then landing at appropriate times.</li>
                            </ul>
                        <li>private IEnumerator Landing()</li>
                            <ul>
                                <li>Handles player's landing on ground corouting.</li>
                            </ul>
                        <li>private IEnumerator Jumping()</li>
                            <ul>
                                <li>Handles player's jumping corouting.</li>
                            </ul>
                        <li>private void initiateDash()</li>
                            <ul>
                                <li>When player wants to dash, plays dash animation telegraph (animation signaling a dash is about to happen).</li>
                            </ul>
                        <li>private IEnumerator DashSound()</li>
                            <ul>
                                <li>Plays dash sound when player dashes.</li>
                            </ul>
                        <li>private void dashingMovement()</li>
                            <ul>
                                <li>Handles moving through action states for dashing.</li>
                            </ul>
                        <li>protected override void handleWeapons()</li>
                            <ul>
                                <li>Determines whether toolUse() is to be called.</li>
                            </ul>
                        <li>private void changeViewMode()</li>
                            <ul>
                                <li>Two view models. Handles switching between free look and character facing reticle.</li>
                            </ul>
                        <li>private void OnTriggerEnter(Collider other)</li>
                            <ul>
                                <li>Opens item pickup message when collider is entered.</li>
                            </ul>
                        <li>private void OnTriggerExit(Collider other)</li>
                            <ul>
                                <li>Disables item pickup message when collider is exited.</li>
                            </ul>
                        <li>void PickupMessage()</li>
                            <ul>
                                <li>When an item is picked up, adds item to inventory, disables item in environment, and disables pickup message.</li>
                            </ul>
                        <li>void startBlock()</li>
                            <ul>
                                <li>Starts blocking when player wants to block.</li>
                            </ul>
                        <li>void endBlock()</li>
                            <ul>
                                <li>Stops player's blocking animation if player does not want to block anymore.</li>
                            </ul>
                        <li>protected override void breakBlock()</li>
                            <ul>
                                <li>Stops player's blocking animation if block was broken.</li>
                            </ul>
                        <li>public void menuEnabled(bool siPapi)</li>
                            <ul>
                                <li>Enables controls if menu is not enabled, disables controls if menu is enabled.</li>
                                <li>param siPapi: bool determining if menu is currently enabled</li>
                            </ul>
                        <li>void Sprint()</li>
                            <ul>
                                <li>Enables sprinting animation and moves character forward faster than running.</li>
                            </ul>
                        <li>protected override bool continueSprintAllowed()</li>
                            <ul>
                                <li>Returns whether sprinting is still allowed for player.</li>
                            </ul>
                        <li>rotected override void crouch()</li>
                            <ul>
                                <li>Crouches the player.</li>
                            </ul>
                        <li>protected override void endCrouch()</li>
                            <ul>
                                <li>Uncrouches the player.</li>
                            </ul>
                        <li>public void playerWins()</li>
                            <ul>
                                <li>When player wins fight, slows timescale, plays winning sound, and displays winning message.</li>
                            </ul>
                        <li>private IEnumerator WaitToWin(float waitTime)</li>
                            <ul>
                                <li>Opens winning message panel for player after a wait time.</li>
                                <li>param waitTime: time to wait before message panel is displayed.</li>
                            </ul>
                    </ul>
                <p><p>
                <li>TrainingDummy.cs</li>
                    <ul>
                        <li>private void Awake()</li>
                            <ul>
                                <li>Initializes layermasks for player and ground</li>
                            </ul>
                        <li>protected override void Start()</li>
                            <ul>
                                <li>Initializes nav mesh agent, character's target, and character equipment.</li>
                            </ul>
                        <li>private void Update()</li>
                            <ul>
                                <li>Handles movement speed, checking beat of song, and game operation when defeated.</li>
                            </ul>
                        <li>private void LateUpdate()</li>
                            <ul>
                                <li>Controls movement animator.</li>
                            </ul>
                        <li>protected override void handleMovement()</li>
                            <ul>
                                <li>Handles moving the character around in the environment.</li>
                            </ul>
                        <li>protected override void handleWeapons()</li>
                            <ul>
                                <li>Handles whether character can attack player.</li>
                            </ul>
                        <li>void checkBeat()</li>
                            <ul>
                                <li>If all action conditions are satisfied, character can perform an action on a specific beat of the song.</li>
                            </ul>
                        <li>private void onBeatAction()</li>
                            <ul>
                                <li>Determines which state character is in and their possible actions on a beat.</li>
                            </ul>
                        <li>private void selectPatrollingAction()</li>
                            <ul>
                                <li>Does nothing.</li>
                            </ul>
                        <li>private void selectFollowingAction()</li>
                            <ul>
                                <li>dashes toward player when in following state.</li>
                            </ul>
                        <li>private void selectInRangeAction()</li>
                            <ul>
                                <li>Decides what character will do when in attack range.</li>
                            </ul>
                        <li>void shoot()</li>
                            <ul>
                                <li>Currently unused - handles character's gun use.</li>
                            </ul>
                        <li>oid initDashForward()</li>
                            <ul>
                                <li>Dashes character forward.</li>
                            </ul>
                        <li>private void Patrolling()</li>
                            <ul>
                                <li>Patrolling action state. Character chooses random spot on navmesh and moves to it.</li>
                            </ul>
                        <li>private void SearchWalkPoint()</li>
                            <ul>
                                <li>Finds a random spot on navmesh for character to move to.</li>
                            </ul>
                        <li>private void trackPlayerAttack()</li>
                            <ul>
                                <li>Determines if character will block based on player's actions.</li>
                            </ul>
                        <li>private void Block()</li>
                            <ul>
                                <li>Initiates block animation and sets character invincible for one hit.</li>
                            </ul>
                        <li>void endBlock()</li>
                            <ul>
                                <li>Ends the block animation and makes character able to be damaged again.</li>
                            </ul>
                        <li>protected override void breakBlock()</li>
                            <ul>
                                <li>Handles block broken behavior.</li>
                            </ul>
                        <li>private void AttackPlayer()</li>
                            <ul>
                                <li>Determines conditions for when useWeapon() is called.</li>
                            </ul>
                        <li>private void ResetAttack()</li>
                            <ul>
                                <li>Resets character to normal operation after an attack.</li>
                            </ul>
                        <li>private void ResetBlock()</li>
                            <ul>
                                <li>Resets character to normal operation after blocking.</li>
                            </ul>
                        <li>private void ResetCheck()</li>
                            <ul>
                                <li>Determines if a reset is needed.</li>
                            </ul>
                        <li>private void playerDefeated()</li>
                            <ul>
                                <li>If character wins fight, teabags player.</li>
                            </ul>
                        <li>private void CrouchDelay()</li>
                            <ul>
                                <li>Handles delaying between crouch operations.</li>
                            </ul>
                        <li>private void crouchAgain()</li>
                            <ul>
                                <li>Handles continuous teabagging when player is dead.</li>
                            </ul>
                        <li>private void enemyDefeated()</li>
                            <ul>
                                <li>Called when character dies and player wins fight. Disables character operation.</li>
                            </ul>
                        <li>protected override void handleAngle()</li>
                            <ul>
                                <li>Does nothing.</li>
                            </ul>
                        <li>void OnDrawGizmosSelected()</li>
                            <ul>
                                <li>Used for debugging to test detection distance.</li>
                            </ul>
                    </ul>
            </ul>
        </ul>
    <p><p>
    <li>EnemyController.cs</li>
        <ul>
            <li>Unused.</li>
        </ul>
    <p><p>
    <li>PlayerControls.cs</li>
        <ul>
            <li>Defines the control scheme for controlling the player character.</li>
        </ul>
    <p><p>
    <li>PlayerManager.cs</li>
        <ul>
            <li>Keeps track of where the player is in the game environment.</li>
            <li>void Awake()</li>
                <ul>
                    <li>Maintains a reference to the player character.</li>
                </ul>
        </ul>
</ul>