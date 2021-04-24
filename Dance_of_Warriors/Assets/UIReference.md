<h3>UI Reference</h3>
<ul>
    <li>HealthBarManager.cs</li>
        <ul>
            <li>void Start()</li>
                <ul>
                    <li>Get healthbar image and the character that this health bar represents</li>
                </ul>
            <li>void Update()</li>
                <ul>
                    <li>Updates health bar based on character's current health and fills the bar accordingly</li>
                </ul>
        </ul>
    <p><p>
    <li>LimbUIManager.cs</li>
        <ul>
            <li>Handles display of damages limbs to player</li>
            <li>private void Awake()</li>
                <ul>
                    <li>Initialize instance of this Limb UI manager</li>
                </ul>
            <li>void Start()</li>
                <ul>
                    <li>Initialize strings for different limbs, to be used later in script</li>
                </ul>
            <li>public void setUIMember(string badLimb)</li>
                <ul>
                    <li>Displays damaged limb to player</li>
                    <li>Called from PlayerHealthController.cs</li>
                    <li>param badLimb: string for which limb is damaged</li>
                </ul>
            <li>private void setSelectedUI(string selectUI, string name)</li>
                <ul>
                    <li>Assigns the selected limb info and enables UI</li>
                    <li>param selectUI: string holding text to be displayed to player</li>
                    <li>param name: string holding the name of the limb that has been damaged</li>
                </ul>
            <li>public void resetUnusableUI(string name)</li>
                <ul>
                    <li>Called when limb is healed and set to usable again. Displays this info to player</li>
                    <li>param name: string holding the name of the limb that has been healed</li>
                </ul>
        </ul>
    <p><p>
    <li>MainMenu.cs</li>
        <ul>
            <li>Handles main game menu functionality</li>
            <li>private void Start()</li>
                <ul>
                    <li>Plays main menu music.</li>
                </ul>
            <li>public void PlayGame()</li>
                <ul>
                    <li>Play button press sound and call loadLevel.cs to load game.</li>
                </ul>
            <li>public void viewInfo()</li>
                <ul>
                    <li>Opens info panel to allow player to set options and view controls.</li>
                </ul>
            <li>public void disableInfoScreen()</li>
                <ul>
                    <li>Called when player presses "back" button. Returns to previous menu/screen.</li>
                </ul>
            <li>public void quitApplication()</li>
                <ul>
                    <li>Quits the game.</li>
                </ul>
            <li>public void playOnSelectSound()</li>
                <ul>
                    <li>Plays a sound when player selects a button on menu.</li>
                </ul>
            <li>public void setInfoScreenActive(int idx)</li>
                <ul>
                    <li>Shows info menu when player selects info from main menu.</li>
                    <li>param idx: holds reference to which button player selected</li>
                </ul>
        </ul>
    <p><p>
    <li>SettingsMenu.cs</li>
        <ul>
            <li>Handles the settings menu from both the pause menu and the main menu</li>
            <li>private void Start()</li>
                <ul>
                    <li>Sets default options values and stores player preferences.</li>
                    <li>Player preferences are persistent between game launches.</li>
                </ul>
            <li>public void setBackgroundVolume(float volume)</li>
                <ul>
                    <li>Allows player to set the volume of the game's background music</li>
                    <li>param volume: float value converted from menu bar</li>
                </ul>
            <li>public void setSoundFXVolume(float volume)</li>
                <ul>
                    <li>Allows player to set the volume of the game's sound effects</li>
                    <li>param volume: float value converted from menu bar</li>
                </ul>
            <li>IEnumerator playSampleSound(float inVolume, string key)</li>
                <ul>
                    <li>plays a sound while the character is changing the sound settings so that they know how loud they're making it</li>
                    <li>param inVolume: helps keep track of the current volume setting and how long it's beend since it's been changed</li>
                    <li>param key: used to keep track of whether we're changing the sound effects volume or the background volume</li>
                </ul>
            <li>private float convertToLogarithmic(float val)</li>
                <ul>
                    <li>Converts volume value into a logarithmic scale</li>
                    <li>param val: value to be converted to logarithmic scale</li>
                </ul>
            <li>public void setLookSensitivityHorizontal(float sensitivityX)</li>
                <ul>
                    <li>Handles player look sensitivity in the X direction</li>
                    <li>param sensitivityX: converts value from menu bar into float value for game engine to handle</li>
                </ul>
            <li>public void setLookSensitivityVertical(float sensitivityY)</li>
                <ul>
                    <li>Handles player look sensitivity in the Y direction</li>
                    <li>param sensitivityY: converts value from menu bar into a float value for game engine to handle</li>
                </ul>
        </ul>
    <p><p>
    <li>PauseMenu.cs</li>
        <ul>
            <li>Handles Pause Menu functionality</li>
            <li>private void Awake()</li>
                <ul>
                    <li></li>
                </ul>
            <li>void OnEnable()</li>
                <ul>
                    <li>Handles enabling the player's controls when game is unpaused</li>
                </ul>
            <li>void OnDisable()</li>
                <ul>
                    <li>Handles disabling the player's controls when the game is paused</li>
                </ul>
            <li>void GamePause()</li>
                <ul>
                    <li>Handles game behavior with pausing</li>
                    <li>If pause menu is active, call Pause()</li>
                    <li>If pause menu is not active, call Resume()</li>
                </ul>
            <li>public void Resume()</li>
                <ul>
                    <li>Sets timescale to normal, allows player to control character again, disables pause menu, and hides cursor.</li>
                </ul>
            <li>void Pause()</li>
                <ul>
                    <li>Sets timescale to 0, disables player control of their character, enables pause menu, and shows cursor.</li>
                </ul>
            <li>public void ResetGame()</li>
                <ul>
                    <li>Resets game when reset button is pressed</li>
                </ul>
            <li>public void QuitGame()</li>
                <ul>
                    <li>Quits the game when quit button is pressed</li>
                </ul>
            <li>public void viewInfo()</li>
                <ul>
                    <li>Shows the info screen when the info button is pressed</li>
                </ul>
            <li>public void disableInfoScreen()</li>
                <ul>
                    <li>Returns to pause menu when "back" button is pressed on info menu.</li>
                </ul>
            <li>public void setInfoScreenActive(int idx)</li>
                <ul>
                    <li>Shows info menu when player selects info from main menu.</li>
                    <li>param idx: holds reference to which button player selected</li>
                </ul>
        </ul>
    <p><p>
    <li>reticleController.cs</li>
        <ul>
            <li>Handles the player's aiming reticle in game</li>
            <li>void Start()</li>
                <ul>
                    <li>Gets reference to player aiming reticle from screen</li>
                </ul>
            <li>void Update()</li>
                <ul>
                    <li>Handles smooth reticle movement with player, and zooming when zoom button is pressed</li>
                </ul>
            <li>public void setZoomReticle(bool val)</li>
                <ul>
                    <li>Zooms in on the reticle for easier aiming of projectile weapons</li>
                    <li>param val: boolean determining whether zoom button is pressed by player</li>
                </ul>
            <li>public void setMoving(bool val)</li>
                <ul>
                    <li>Changes reticle spread when player is moving</li>
                    <li>param val: boolean determing whether player is moving around or not</li>
                </ul>
            <li>public void setShot()</li>
                <ul>
                    <li>Handles reticle bloom when shooting.</li>
                </ul>
        </ul>
    <p><p>
    <li>PlayerHealthController.cs</li>
        <ul>
            <li>Handles character health within game</li>
            <li>private void Awake()</li>
                <ul>
                    <li>Initializes character's movement speed and calls InItHealth().</li>
                </ul>
            <li>void Update()</li>
                <ul>
                    <li>Maintains current health values for character.</li>
                    <li>If character's health reaches 0, calls Die().</li>
                </ul>
            <li>public void InItHealth()</li>
                <ul>
                    <li>Sets initial health values for character's general health and their limb health.</li>
                </ul>
            <li>public void setInvincible(bool invincible)</li>
                <ul>
                    <li>Determines if character is invincible.</li>
                    <li>param invincible: boolean, 1 = yes, 0 = no.</li>
                </ul>
            <li>public void setOneTimeBlock(bool val)</li>
                <ul>
                    <li>Sets whether the character can block more than once</li>
                    <li>param val: boolean, 1 = yes, 0 = no.</li>
                </ul>
            <li>public bool getOneTimeBlock()</li>
                <ul>
                    <li>Determines if character has already blocked one attack.</li>
                </ul>
            <li>public void TakeDamage(string collisionTag, float attackDamage)</li>
                <ul>
                    <li>Damages the character's health.</li>
                    <li>param collisionTag: String holding which part of character's body to damage.</li>
                    <li>param attackDamage: amount to damage health by.</li>
                </ul>
            <li>public void healGeneralHealth(float healAmount)</li>
                <ul>
                    <li>Heals the character's health, not their limbs.</li>
                    <li>param healAmount: amount to increase character's health by.</li>
                </ul>
            <li>public float getAllLimbHealth()</li>
                <ul>
                    <li>Returns the health values of every limb on the character.</li>
                </ul>
            <li>public float getLimbHealth(string limb)</li>
                <ul>
                    <li>Returns the health value of a particular limb.</li>
                    <li>param limb: string holding the name of the limb in question.</li>
                </ul>
            <li>public void healAllLimbs(float healAmount)</li>
                <ul>
                    <li>Heals every limb on the character.</li>
                    <li>param healAmount: amount of health to apply to character's limbs.</li>
                </ul>
            <li>public void healLimb(string limbToBeHealed, float healAmount)</li>
                <ul>
                    <li>Heals a particular limb.</li>
                    <li>param limbToBeHealed: string holding the name of the limb to be healed.</li>
                    <li>param healAmount: amount of health to apply to the limb.</li>
                </ul>
            <li>private void KeepBelowMax()</li>
                <ul>
                    <li>Keeps all health values below their max to prevent weirdness.</li>
                </ul>
            <li>public List<string> getUnusableLimb()</li>
                <ul>
                    <li>Returns an array or the character's currently damaged limbs</li>
                </ul>
            <li>public float getHealth()</li>
                <ul>
                    <li>Returns the character's current health</li>
                </ul>
            <li>void RagDoll(bool value)</li>
                <ul>
                    <li>Determines whether to ragdoll the character's body</li>
                    <li>param value: boolean, 1 = ragdoll, 0 = no ragdoll
                </ul>
            <li>private void die()</li>
                <ul>
                    <li>Ragdolls the player and keeps the body visible in game.</li>
                </ul>
        </ul>
</ul>
