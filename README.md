# Block Zombie Project for BizAR Reality

## Instructions

To play the game, extract the ZIP file `Joshua van Breda BizAr Reality BUILD.zip`. It contains the `BlockZombie.exe` inside its folder.

Included in the Git repo are my project files, feel free to take a look.

## Project Requirements
- Keybord/Mouse input
- UI Feedback that's scalable between different screen size
- IEnumerators
- Rigidbody.addforce
- Raycast
- Changing textures on a material
- A Sun that is angled based on the system's current time

## How I achieved these requirements
- Keyboard mouse input is handled in the `NewMovement.cs`, `PlayerHead.cs`, and `ThirdPersonOrbitCamBasic.cs`.
- I use IEnumerators for the Clock and Sun Timer inside `LightingManager.cs` as well as to add a slight delay for my sound effects inside of `NewMovement.cs`.
- I use Rigidbody.Addforce in `NewMovement.cs` on `MouseButtonDown(0)` to launch the "enemy" transform away from the player.
- I use a Raycast in `NewMovement.cs` to detect whether the player is looking at an enemy object, if so then it is able to hit the enemy.
- I change the texture of the enemy material in `NewMovement.cs` inside the `MouseButtonDown(0)` inside the Raycast if statement (when an enemy is hit).
- The Sun is handled inside `LightingManager.cs`, it gets a scriptable object `LightingPreset` and sets the color values and sun position. In `LightingManager.cs` we convert the System time into the correct time format, and apply the current time to the rotational value slider which we use to change the angle of the Directional light and the sun position.
