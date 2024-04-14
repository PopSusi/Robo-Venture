=== Basic Requirements ===
|-> Game is 3D person, and made in Unity 3D.
|-> Xbox Controller and Keyboard supported for gameplay. Keyboard only for UI as of now.
|-> Runs at 16:9 Aspect Ratios, tested for 1920 x 1080 and 2560 x 1440.
|-> HUD System with Healthbar, tutorial and objective based text, and Cooldowns.
|-> Overall Narrative: Robo discovers more about the mysterious inhabitants of the planet, finding out they are an advanced species, supported primarily by environmental models and lore hints.
|-> Multiple Audio Sound Effects for the player and enemies. Music present on each level
	|-> Hit, Hit Variant, Death Sound for Players and Enemies.
	|-> Punch SFX for Player.
|-> Unsure of length as of now.
|-> Hub level with Tutorial area, and 3 levels which are connected by the Hub level.
|-> Picking up a Fuel Cell will notify the player of their win, and return them to the Hub Level.
|-> Dying constitutes a loss. The Player can respawn at Checkpoints.
|-> On the 'How To Play' Menu, the player can track Fuel Cells inserted and Coins collected.

=== Level Requirements ===
|-> Each level is physically connected to the Hub. The Hub also has a tutorial area for the player.
|-> Grabbing the Fuel Cell wins the level, and prompts the player to play the level or teleport back. An option to teleport back is also in the 'Pause' menu.
|-> We have 10 models per level, which are all textured
	|-> Every level - 4 Models
		|-> Enemy Model
		|-> Mod Chip
		|-> Fuel Cell
		|-> Big Coin
	|-> Dash - 6 Unique
		|-> Rock Platforms (3 Variants)
		|-> Rock Variants (3 Variants)
	|-> Grapple - 5 Unique
		|-> Grapple Point
		|-> Spire
		|-> Spire Variant
		|-> Dead Tree
		|-> Dead roots
		|-> Dead branches
	|-> Charged Punch - 7 Unique
		|-> Wall
		|-> Breakable Wall
		|-> Cieling
		|-> Torch
		|-> Gems (3 Variants)
|-> We have primary animations for each level
	|-> Dash - Player Dash
	|-> Grapple - Player Grapple (3 different animations)
	|-> Charge Punch - Player Charge Punch (4 different animations)
|-> We have primary particle effects for each level
	|-> Every level - Player Hit (2 Variants), Enemy Trail
	|-> Dash - Sandstorm
	|-> Grapple - Grapple Hook
	|-> Charge Punch - Torch Fire
|-> Each level has their own method of acquiring the 'Win State' (Fuel Cell)
	|-> Dash - Dash Ability
	|-> Grapple - Grapple Ability
	|-> Charge Punch - Charge Punch Ability
|-> Each level is designed uniquely, with one shared aspect
	|-> Each level - Ability is required to complete the level, and is unlocked in the beginning of each level
	|-> Dash - Open level encouraging freedom and platforming
	|-> Grapple - Scaling a large tower with enemies on each floor
	|-> Charge Punch - Maze with destroyable walls

=== Art Requirements ===
|-> All models, textures and SFX of our own creation.
|-> Menus are stylistically consistent.

== Code Requirements ===
|-> Our significant and unique mechanics are the 3 previously mentioned abilities and 2 more that are unlocked from the beginning.
	|-> 3-Punch Combo - A short punching combo that deals damage
	|-> Stun Grenade - Hold to aim, let go to throw. Spawns a Sphere of damage that also locks enemy movement
	|-> Dash - Moves player in the direction they are walking very fast
	|-> Grapple - Looking at a Grapple Point, the player will travel to the Grapple Point's respective landing point
|-> Pausable using the 'P' Key.
|-> No gamebreaking crashes or bugs documented.

=== Tech Requirements ===
|-> All levels and menus designed by team.
|-> Win and Loss states implemented.
|-> Main Menu has 'Start Game' and 'Quit' buttons, and 'How To Play' and 'Options' menus.
|-> Pause menu with the same 'How To Play' and 'Options' menus, as well as a 'Continue', 'Return to Hub', and 'Quit' buttons.
|-> Options menu with 3 Options.
	|-> Infinite Health (Invulnerable to damage)
	|-> All Mod Chips (Unlocks all abilities)
	|-> Hard Mode (Take 2 damage instead of 1)
|-> Options Icons on HUD and checkmarks next to active mods are consistent across levels
|-> No dead ends in levels or menus.
|-> All menus and levels have finalized design.

=== Known Issues ===
|-> Hub level start has very harsh lighting in build, not in editor.