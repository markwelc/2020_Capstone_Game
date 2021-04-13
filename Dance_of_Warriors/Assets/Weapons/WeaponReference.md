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
Parent class to:
<ul>
    <li><h4>Guns.cs</h3></li>
    <ul>
        <li>Handgun.cs</li>
    </ul>
    <li><h4>Melee.cs</h3></li>
    <ul>
        <li>Stick.cs</li>
    </ul>
</ul>
</ul>