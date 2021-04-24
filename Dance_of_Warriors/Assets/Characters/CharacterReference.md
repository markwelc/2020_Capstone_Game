<h3>Character Reference</h3>
<ul>
    <li>CameraLook.cs</li>
        <ul>
            <li>Defines behavior of camera movement in game.</li>
            <li>private void Awake()</li>
            <li>void OnEnable()</li>
            <li>void OnDisable()</li>
            <li>void Update()</li>
            <li>public void setXLookScale(float xLookScale)</li>
            <li>public void setYLookScale(float yLookScale)</li>
            <li>public void AddRecoil()</li>
            <li>void RotateCamera()</li>
            <li>private void zoomIn()</li>
            <li>private void zoomOut()</li>
            <li>private void fovTransition(float end)</li>
            <li>void enableTargetCross(Color32 color)</li>
        </ul>
    <li>Character.cs</li>
        <ul>
            <li>Defines basic behavior for all characters within game.</li>
            <li>protected virtual void Start()</li>
            <li>void FixedUpdate()</li>
            <li>protected void moveCharacter(Vector3 direcAndDist)</li>
            <li>protected virtual void handleMovement()</li>
            <li>protected virtual void handleJump()</li>
            <li>protected virtual void handleAngle()</li>
            <li>protected virtual void handleWeapons()</li>
            <li>protected virtual void crouch()</li>
            <li>protected virtual void endCrouch()</li>
            <li>protected void handleSprint()</li>
            <li>protected void handleStamina()</li>
            <li>protected virtual void useWeapons(int attackType, float characterDamageModifier)</li>
            <li>public void weaponDamageAllowed()</li>
            <li>public void weaponNoDamageAllowed()</li>
            <li>protected void initiateTool(int toolNum)</li>
            <li>protected void toolUse()</li>
            <li>protected void cycleWeapon()</li>
            <li>protected virtual void OnCollisionStay(Collision collision)</li>
            <li>protected virtual void OnCollisionExit(Collision collision)</li>
            <li>protected bool dashAllowed()</li>
            <li>protected bool toolAllowed()</li>
            <li>protected bool groundCheck()</li>
            <li>protected bool jumpAllowed()</li>
            <li>protected bool sprintAllowed(Vector2 move)</li>
            <li>protected virtual bool continueSprintAllowed()</li>
            <li>protected bool crouchAllowed()</li>
            <li>public float getHealth()</li>
            <li>protected virtual void breakBlock()</li>
            <li>protected virtual void endSprint()</li>
            <li>private void checkIsDead()</li>
            <li>protected char getCurrentWeaponType()</li>
            <li>protected Transform getCurrentWeapon()</li>
            <li>protected void toggleAnimRigging(bool turnOn, bool changeParent)</li>
            <p><p>
            Parent class to:
            <ul>
                <li>NewPlayer.cs</li>
                    <ul>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                    </ul>
                <li>TrainingDummy.cs</li>
                    <ul>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
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