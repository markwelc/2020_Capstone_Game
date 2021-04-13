Lists used here to show inheritance
<ul>
<li><h3>WeaponController.cs</h3></li>
<ul>
<li>protected virtual void Start()</li>
    <ul>
        <li>Initialize references to Handgun and Stick classes</li>
        <li>Initialize animationName</li>
    </ul>
<li>public virtual void useWeapon(string weaponName, out string animation, out int[] states, int attackType, float characterDamageModifier)</li>
    <ul>
        <li>param weaponName: stores the name of the weapon to be used</li>
        <li>param animation: stores the name of the animation to be played when weapon is used</li>
        <li>param states: stores the length of each animation state for tuning of animation speed</li>
        <li>param attackType: stores the type of attack being performed (melee, gun)</li>
        <li>param characterDamageModifier: used to modify the damage values applied by the character when limbs are damaged</li>
    </ul>
<li>protected virtual void useWeapon()</li>
    <ul>
        <li>overridden by child classes</li>
    </ul>
<li>public virtual void canDealDamage(string weaponName, bool canDamage)</li>
    <ul>
        <li>param weaponName: name of weapon that is being referenced</li>
        <li>param canDamage: determines whether the weapon can deal damage at a specific point in time</li>
    </ul>
</ul>
<p>
Parent class to:
<ul>
    <li><h4>Guns.cs</h3></li>
    <ul>
        <li>public override void useWeapon(string weaponName, out string animation, out int[] states, int attackType, float characterDamageModifier)</li>
            <ul>
                <li>param weaponName: See WeaponController.cs definition.</li>
                <li>param animation: See WeaponController.cs definition.</li>
                <li>param states: See WeaponController.cs definition.</li>
                <li>param attackType: See WeaponController.cs definition.</li>
                <li>param characterDamageModifier: See WeaponController.cs definition.</li>
            </ul>
        <li>IEnumerator Reload()</li>
            <ul>
                <li>Reloads the gun. Stops activity with gun for duration of reload, plays reload sound</li>
            </ul>
        <li>void shoot()</li>
            <ul>
                <li>Spawns a bullet at the tip of the gun model, plays gun sound</li>
            </ul>
    </ul>
    <p>
    Parent class to:
        <ul>
            <li>Handgun.cs</li>
                <ul>
                    <li>protected override void Start()</li>
                        <ul>
                            <li>Initializes weapon ammo capacity, animation names and animation timing</li>
                        </ul>
                </ul>
        </ul>
    <li><h4>Melee.cs</h3></li>
        <ul>
            <li>public override void useWeapon(string weaponName, out string animation, out int[] states, int attackType, float characterDamageModifier)</li>
                <ul>
                    <li>param weaponName: See WeaponController.cs definition.</li>
                    <li>param animation: See WeaponController.cs definition.</li>
                    <li>param states: See WeaponController.cs definition.</li>
                    <li>param attackType: See WeaponController.cs definition.</li>
                    <li>param characterDamageModifier: See WeaponController.cs definition.</li>
                </ul>
            <li>protected void OnTriggerEnter(Collider other)</li>
                <ul>
                    <li>Deals with playing impact sounds and when damage is dealt</li>
                    <li>param other: references the collider of the object the weapon is hitting</li>
                </ul>
            <li>private IEnumerator ImpactAudio()</li>
                <ul>
                    <li>Plays a melee impact sound based on current conditions in game</li>
                </ul>
            <li>private IEnumerator Swing()</li>
                <ul>
                    <li>Plays the regular Swing animation</li>
                </ul>
            <li>private IEnumerator FastSwing()</li>
                <ul>
                    <li>Plays the faster Swing animation</li>
                </ul>
            <li>private Character getCharacterInParents(GameObject start)</li>
                <ul>
                    <li>Gets a reference to the character holding the current melee weapon</li>
                    <li>param start: references the weapon being held by a character, used to find the character holding it</li>
                </ul>
            <li>public override void canDealDamage(string weaponN, bool canDamage)</li>
                <ul>
                    <li>keeps track of whether a weapon can deal damage</li>
                    <li>param weaponN: See WeaponContriller.cs definition.</li>
                    <li>param canDamage: See WeaponController.cs definition</li>
                </ul>
        </ul>
    <p>
    Parent class to:
    <ul>
        <li>Stick.cs</li>
            <ul>
                <li>protected override void Start()</li>
                    <ul>
                        <li>Initializes length of action states and animation names</li>
                    </ul>
            </ul>
    </ul>
</ul>
</ul>