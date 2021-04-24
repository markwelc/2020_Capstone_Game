<h3>Character Reference</h3>
<ul>
    <li>CameraLook.cs</li>
        <ul>
            <li>Defines behavior of camera movement in game.</li>
            <li>private void Awake()</li>
                <ul>
                    <li></li>
                </ul>
            <li>void OnEnable()</li>
                <ul>
                    <li></li>
                </ul>
            <li>void OnDisable()</li>
                <ul>
                    <li></li>
                </ul>
            <li>void Update()</li>
                <ul>
                    <li></li>
                </ul>
            <li>public void setXLookScale(float xLookScale)</li>
                <ul>
                    <li></li>
                </ul>
            <li>public void setYLookScale(float yLookScale)</li>
                <ul>
                    <li></li>
                </ul>
            <li>public void AddRecoil()</li>
                <ul>
                    <li></li>
                </ul>
            <li>void RotateCamera()</li>
                <ul>
                    <li></li>
                </ul>
            <li>private void zoomIn()</li>
                <ul>
                    <li></li>
                </ul>
            <li>private void zoomOut()</li>
                <ul>
                    <li></li>
                </ul>
            <li>private void fovTransition(float end)</li>
                <ul>
                    <li></li>
                </ul>
            <li>void enableTargetCross(Color32 color)</li>
                <ul>
                    <li></li>
                </ul>
        </ul>
    <li>Character.cs</li>
        <ul>
            <li>Defines basic behavior for all characters within game.</li>
            <li>protected virtual void Start()</li>
                <ul>
                    <li></li>
                </ul>
            <li>void FixedUpdate()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected void moveCharacter(Vector3 direcAndDist)</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void handleMovement()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void handleJump()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void handleAngle()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void handleWeapons()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void crouch()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void endCrouch()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected void handleSprint()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected void handleStamina()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void useWeapons(int attackType, float characterDamageModifier)</li>
                <ul>
                    <li></li>
                </ul>
            <li>public void weaponDamageAllowed()</li>
                <ul>
                    <li></li>
                </ul>
            <li>public void weaponNoDamageAllowed()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected void initiateTool(int toolNum)</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected void toolUse()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected void cycleWeapon()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void OnCollisionStay(Collision collision)</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void OnCollisionExit(Collision collision)</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected bool dashAllowed()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected bool toolAllowed()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected bool groundCheck()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected bool jumpAllowed()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected bool sprintAllowed(Vector2 move)</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual bool continueSprintAllowed()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected bool crouchAllowed()</li>
                <ul>
                    <li></li>
                </ul>
            <li>public float getHealth()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void breakBlock()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected virtual void endSprint()</li>
                <ul>
                    <li></li>
                </ul>
            <li>private void checkIsDead()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected char getCurrentWeaponType()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected Transform getCurrentWeapon()</li>
                <ul>
                    <li></li>
                </ul>
            <li>protected void toggleAnimRigging(bool turnOn, bool changeParent)</li>
                <ul>
                    <li></li>
                </ul>
            <p><p>
            Parent class to:
            <ul>
                <li>NewPlayer.cs</li>
                    <ul>
                        <li>private void Awake()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void OnEnable()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void OnDisable()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void Start()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void Update()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void handleMovement()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void standardMovement()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void handleAngle()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void Jump()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private IEnumerator Landing()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private IEnumerator Jumping()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void initiateDash()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private IEnumerator DashSound()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void dashingMovement()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void handleWeapons()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void changeViewMode()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void OnTriggerEnter(Collider other)</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void OnTriggerExit(Collider other)</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void PickupMessage()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void startBlock()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void endBlock()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void breakBlock()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>public void menuEnabled(bool siPapi)</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void Sprint()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override bool continueSprintAllowed()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>rotected override void crouch()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void endCrouch()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>public void playerWins()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private IEnumerator WaitToWin(float waitTime)</li>
                            <ul>
                                <li></li>
                            </ul>
                    </ul>
                <li>TrainingDummy.cs</li>
                    <ul>
                        <li>private void Awake()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void Start()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void Update()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void LateUpdate()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void handleMovement()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void handleWeapons()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void checkBeat()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void onBeatAction()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void selectPatrollingAction()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void selectFollowingAction()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void selectInRangeAction()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void shoot()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>oid initDashForward()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void Patrolling()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void SearchWalkPoint()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void trackPlayerAttack()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void Block()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void endBlock()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void breakBlock()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void AttackPlayer()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void ResetAttack()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void ResetBlock()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void ResetCheck()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void playerDefeated()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void CrouchDelay()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void crouchAgain()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>private void enemyDefeated()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>protected override void handleAngle()</li>
                            <ul>
                                <li></li>
                            </ul>
                        <li>void OnDrawGizmosSelected()</li>
                            <ul>
                                <li>Used for debugging to test detection distance.</li>
                            </ul>
                    </ul>
            </ul>
        </ul>
    <li>EnemyController.cs</li>
        <ul>
            <li>Unused.</li>
        </ul>
    <li>PlayerControls.cs</li>
        <ul>
            <li>Defines the control scheme for controlling the player character.</li>
        </ul>
    <li>PlayerManager.cs</li>
        <ul>
            <li>Keeps track of where the player is in the game environment.</li>
            <li>void Awake()</li>
                <ul>
                    <li>Maintains a reference to the player character.</li>
                </ul>
        </ul>
</ul>