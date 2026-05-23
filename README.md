Noko Games - Idle RPG / Arcade Idle Technical Case

A mobile-focused arcade idle RPG prototype developed in Unity for the Noko Games Developer Technical Case.

Gameplay Overview

The player survives inside an arena while automatically attacking nearby enemies using a bow.

Enemies continuously spawn with increasing intensity over time.
By defeating enemies, the player earns gold that can be used to purchase permanent stat upgrades.

As the player progresses, new combat skills unlock automatically based on total kills.

Core Features
Combat System
Automatic enemy targeting
Automatic bow shooting
Projectile-based combat
Damage popup feedback
Arrow trail effects
Enemy scaling and progression
Upgrade System

Players can open the upgrade panel at any time.
The game pauses while the upgrade menu is open.

Available upgrades:

Damage
HP
Attack Speed
Movement Speed

Each upgrade directly affects gameplay and improves combat efficiency.

Skill System

The game includes 3 unlockable bow skills:

Multishot (10 kills)

Shoots 3 arrows (left, center, right)

Poison Arrow (25 kills)

Applies damage over time
Enemies receive green poison VFX

Rain of Arrows (50 kills)

Periodic arrow rain attack
Targets up to 5 enemies simultaneously
Weapon System

The player currently uses a bow as the primary and only weapon in this prototype.

Combat is fully automatic
No weapon switching system is implemented
Designed as a base system for future weapon expansion
Enemy System
3 enemy types
Different movement speeds
Different health values
Progressive spawn scaling
Increasing spawn rate over time for difficulty scaling
Controls
Mobile
Virtual Joystick → Movement

Combat is fully automatic.

Technical Details
Engine

Unity

Programming Language

C#

Architecture

The project uses a modular gameplay architecture with separated systems:

Player Controller
Enemy AI
Combat System
Upgrade System
Skill System
Currency System
UI System
Project Structure
Scripts/
 ├── Animations
 ├── Camera
 ├── Data
 ├── Enemy
 ├── Feedback
 ├── Other
 ├── Player
 └── UI
Visual Feedback Systems
Damage popups
Skill unlock popups
Arrow trail effects
Poison VFX
Combat animations
Skill effects
Build Target

Mobile Platform

How to Run
Open the project in Unity
Recommended Version: Unity 2022.3 LTS

Load the main scene from:

Assets/Scenes/Main.unity
Press Play to start the game
Assets

All assets used in this project are self-created or free-to-use placeholder assets.
No external paid asset packs were used.

Known Limitations
Prototype-focused project
No save/load system implemented
Limited enemy variety
Placeholder UI visuals in some areas
Balancing can be improved
Developer

Erhan Doğru
GitHub: https://github.com/ErhanDgr23
