Noko Games - Idle RPG / Arcade Idle Technical Case

A mobile-focused arcade idle RPG prototype developed in Unity for the Noko Games Developer Technical Case.

The project focuses on:

Clean and modular gameplay systems
Satisfying combat feedback
Progressive player growth
Idle/arcade gameplay loop
Mobile-friendly controls and UI
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

The game includes 3 unlockable bow skills.

Multishot

Unlocked at 10 kills.

Shoots 3 arrows simultaneously:

Left
Center
Right
Poison Arrow

Unlocked at 25 kills.

Applies poison damage over time to enemies.

Enemies receive a green visual effect while poisoned.

Rain of Arrows

Unlocked at 50 kills.

Every few seconds, arrows rain down from the sky onto nearby enemies.

Supports up to 5 simultaneous enemy targets.

Enemy System

The project includes:

3 enemy types
Different movement speeds
Different health values
Progressive spawn scaling

Enemy spawn rate increases over time to create increasing gameplay pressure.

Controls
PC
WASD → Movement
Mobile
Virtual Joystick → Movement

Combat is fully automatic.

Technical Details
Engine

Unity

Programming Language

C#

Architecture

The project uses a modular gameplay architecture with separated gameplay systems.

Main systems include:

Player Controller
Enemy AI
Combat System
Upgrade System
Skill System
Currency System
UI System
Project Structure
Scripts/
 ├── Player
 ├── Enemy
 ├── Skills
 ├── Managers
 ├── UI
 └── Systems
Visual Feedback

Implemented feedback systems:

Damage popups
Skill unlock popups
Arrow trails
Poison VFX
Combat animations
Skill effects
Build Target

Mobile Platform

Known Limitations
Prototype-focused project
No save/load system
Limited enemy variety
Placeholder UI visuals in some areas
Balancing can be further improved
Developer

Erhan Doğru