<h3>Basic movement</h3>
<ol>
<li>Slow walk</li>
<ul>
<li>Forward, backward, left, right</li>
<li>Standard (the player has this from the start of the game)
<li>No stamina cost
<li>Wasd + caps lock
</ul>
<li>Bow</li>
<ul>
<li>Allows the player to bow to their opponent before the fight starts
<li>The bow should last for as long as the player is hitting the button
<li>After releasing the button, there should be a short period where the player is vulnerable
<ul>
<li>This means that, if the player bows, there’s real trust because there’s real risk
<li>Additionally, there could be a few bosses that attempt to take advantage of this
</ul>
<li>Standard
<li>No stamina cost
<li>B
</ul>
<li>Standard movement</li>
<ul>
<li>Forward, backward, left, right
<li>Standard 
<li>Wasd
<li>No stamina cost
</ul>
<li>Jumping</li>
<ul>
<li>Single jump
<li>Direction cannot be changed mid-air using standard movement (a type of dash or double jump is needed)
<li>Standard
<li>Shift
<ul>
<li>I figured that dashing might be more important, though this could be totally wrong
</ul>
<li>No stamina cost
</ul>
<li>Sprint</li>
<ul>
<li>Faster, though not significantly, than standard movement
<li>Slightly harder to change direction
<li>Jumping while sprinting will result in a longer and higher jump
<li>Cannot be started mid-air
<li>Standard 
<li>Double tap wasd
<li>Stamina drain
</ul>
<li>Short dash</li>
<ul>
<li>Quick way to slightly change position
<li>Similar to the backstep in Dark Souls 3, though operates in any direction
<li>Works in mid-air (This could be its own move, though it doesn’t need to be)
<ul>
<li>Will only dash horizontally
<li>While dashing in air, the player’s altitude does not change (or at least, doesn’t change much)
<li>After dashing, the character’s trajectory resumes as though the player had originally jumped in the direction of the dash and had just reached its height (so the player won’t continue going up after dashing mid-air)
<li>Use while in mid-air comes at a significantly greater stamina cost
</ul>
<li>Standard
<li>Wasd + alt 
<li>small stamina drain
</ul>
<li>Dodge</li>
<ul>
<li>Easy way to change position while providing I-frames
<li>It’s possible that the I-frames don’t apply to AOEs, forcing the player to rely on other dodging mechanisms. Though this is debatable, I don’t even know if it’s just AOEs that shouldn’t be dodge-able, I’d like to encourage jumping over a sword sweep.
<li>Takes longer than short dash (?)
<li>Works in mid-air
<ul>
<li>I’m not sure at all on the specifics on this, but not allowing it might disincentivize jumping too much
<li>I do know, however, that if it’s allowed it will cost a significantly greater stamina drain
</ul>
<li>Acquired 
<li>Wasd + space
<li>Small stamina drain
</ul>
<li>Long dash</li>
<ul>
<li>Very quickly moves the player a long way
<li>Should be used as a dodge, but doesn’t provide I-frames (perhaps this is what is used to dodge AOEs)
<li>Stopping mid dash should be possible, but not changing direction (it shouldn’t last a long time, so hopefully you won’t need to)
<li>Can be used mid-air
<ul>
<li>Will only dash horizontally
<li>This means that, while dashing, the player’s altitude won’t change
<li>After finishing the dash, the player’s trajectory continues in the direction of the dash as though the player had just reached his maximum height (so he won’t continue going up)
<li>Will incur a significantly heavier stamina cost
</ul>
<li>Acquired (I already dread fighting the boss that makes use of this, that seems bad)
<li>Wasd + ctrl (?)
<li>Standard stamina cost
</ul>
<li>Jet Run</li>
<ul>
<li>Replacement for sprinting, much faster, but harder to change direction and more stamina drain
<li>Jumping while using this has the same effect on the jump as jumping while sprinting, though to a greater degree
<ul>
<li>i.e. the jump is even longer and higher
</ul>
<li>cannot be started mid-air
<li>The player should be able to stop it at any time
<li>This means that it should be on its own cooldown 
<ul>
<li>This will prevent the player from stopping, looking in a new direction, and starting again
<li>of course, stopping could also incur a heavy stamina cost (though this limits the player’s ability to stop right next to the boss and strike him)
<li>It’s also worth noting that we might want to allow this tactic
</ul>
<li>Acquired
<li>Decent stamina drain
<li>triple tap (?) wasd to start, release all wasd keys while jet running to stop
</ul>
<li>Vertical short dash</li>
<ul>
<li>Must be used while in the air
<ul>
<li>It would be nice if this wasn’t the case, and a quick way to gain height was possible, but I don’t know how the key bindings would work (as it is, they might end up being clunky)
</ul>
<li>Provides a way to either gain extra height and air-time, or as a method of cancelling a jump
<li>This move counts as a mid-air short dash, and so follows the same rules
<ul>
<li>Notably, the player must either touch the ground or pogo off something in order to use either it or the horizontal short dash again
</ul>
<li>Acquired
<li>Similar stamina drain to short dash
<li>While in air, alt (without pressing any key in wasd) for down, and alt + jump (shift) (without pressing any key in wasd) for up
<ul>
<li>It’s possible that, with different key bindings, the player can short dash 45 degrees up and forward
</ul>
<li>Double jump</li>
<ul>
<li>Provides a way to either get more height, or to change direction in mid-air
<li>This is simulated as though there were ground under the player when jumping
<ul>
<li>This means that direction can only be changed to one new direction
<li>This is because the character’s trajectory acts as it normally does for a jump
<li>If the player is in a running jump, the double jump also acts as a running jump, though the direction can’t be changed
</ul>
<li>Can only be used once per jump (unless reset by pogoing off an enemy if that’s possible)
<li>Acquired
<li>Similar stamina cost to jump (maybe more?)
<li>Jump while in the air (+ wasd to determine the direction of the new jump)
</ul>
</ol>
<p>
<h3>Weapon system</h3>
<ol>
<li>There are four basic attack types (all standard)
<ul>
<li>Standard
<ul>
<li>This is the standard attack with the player’s current weapon
<li>Left-click
<li>Stamina cost determined by weapon (usually standard cost)
</ul>
<li>Special
<ul>
<li>The special attack provided by the weapon
<li>May or may not be simply a heavy attack
<li>Boring specials, such as a block, are possible (though I don’t know if we should do this)
<li>Either this is how you zoom in with ranged weapons, or you zoom in by pressing Z (I don’t know which we should do)
<li>Right click
<li>Stamina cost determined by weapon (usually heavy cost)
</ul>
<li>Item
<ul>
<li>This is where grenades, powerups, health regen (? This might be important enough to dedicate its own button to), and any other item we can think of is used
<li>Q
<li>Stamina cost determined by item used (usually tiny cost)
</ul>
<li>Quick melee 
<ul>
<li>Quick and cheap way to deal small amount of damage
<li>Similar to Master Chief’s bash
<li>Can be used regardless of weapon equipped
<li>F
<li>Tiny stamina cost
</ul>
</ul>
<li>Main weapon can be cycled by pressing E
<ul>
<li>If the player has a lot of weapons to choose from (hopefully they do) then before the fight they can specify which ones they want to use for the fight
</ul>
<li>Item can be cycled by pressing R
<ul>
<li>Again, if there are a lot of options, the player can choose which ones he wants before the fight
<li>We might want to reserve this key for reloading, not sure
</ul>
<li>Any attack should work at any time unless doing a dash or a dodge
<ul>
<li>Specifically, in mid-air and while running
</ul>
<li>Certain weapons could have ‘hidden specials’ if the weapon is used after a particular movement
<ul>
<li>For instance, using a weapon’s special immediately after stopping a jet run could cause the weapon’s special to be amplified, or completely different
<li>I call them hidden specials, but they could be told to the player, I just didn’t think of a better name for them
</ul>
</ol>