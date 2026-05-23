Noko Games - Idle RPG / Arcade Idle
Technical Prototype (Publisher-Ready Build)

A mobile-first arcade idle RPG prototype developed in Unity.
Designed as a scalable foundation for long-term progression systems, live-ops potential, and monetization-ready idle gameplay loops.

🎮 High-Level Overview

The player survives inside an arena where combat is fully automated using a bow-based attack system.

The core loop is built around:

Continuous enemy pressure
Automated combat progression
Permanent stat-based upgrades
Skill-based power spikes tied to progression milestones

The design focuses on satisfying idle progression + arcade feedback loop clarity, optimized for mobile engagement patterns.

🔁 Core Gameplay Loop
Spawn into arena
Auto-combat enemies with bow
Earn gold from kills
Upgrade permanent stats
Unlock new skills based on kill milestones
Survive increasing enemy intensity

This loop is designed for:

Short-session engagement (1–3 min loops)
Long-term progression retention
Idle growth satisfaction
⚔️ Combat System

The combat system is fully automated and optimized for mobile readability and feedback clarity.

Key Features:
Auto-target nearest enemies
Projectile-based bow combat
Frame-safe attack logic (no input dependency)
Scalable damage system
Visual feedback per hit (damage popups + VFX)
Lightweight performance-friendly execution

Designed to support future expansion into:

Weapon variations
Critical hit systems
Elemental damage types
🧠 Skill System (Progression-Based)

Skills are automatically unlocked based on total kill milestones, reinforcing long-term engagement.

Skills:

Multishot (10 kills)

Fires 3 directional arrows
Increases early-game power spike

Poison Arrow (25 kills)

Damage-over-time system
Visual poison effect applied to enemies

Rain of Arrows (50 kills)

Area-based periodic strike system
Targets up to 5 enemies simultaneously
Designed for mid/late-game power scaling
💰 Upgrade System

A permanent progression system designed for retention and scaling difficulty.

Upgrades:
Damage
HP
Attack Speed
Movement Speed
System Behavior:
Real-time stat application
Scales directly with combat feel
Supports exponential difficulty scaling
Design Intent:
Idle RPG retention loop
Long-term progression sink
Future monetization hook (upgrade economy expansion ready)
👾 Enemy System

Designed for progressive difficulty scaling and controlled pressure increase.

Features:
3 enemy archetypes
Distinct movement speeds
Different health pools
Dynamic spawn scaling system
Increasing spawn rate over time
Design Focus:
Controlled difficulty curve
Readable enemy behavior
Performance-safe scaling approach
🧭 Controls
Mobile:
Virtual Joystick → Movement
Combat → Fully automatic

No manual attack input required by design (idle-arcade hybrid structure).

🏗 Architecture Overview

The project is built with a modular Unity architecture to support scalability and feature expansion.

Core Systems:
Player Controller
Enemy AI System
Combat System
Skill System
Upgrade System
Currency System
UI System
Feedback/VFX System
Design Principles:
Decoupled gameplay systems
Data-driven progression logic
Expandable skill & upgrade architecture
Mobile performance-first design
📁 Project Structure
Scripts/
 ├── Animations
 ├── Camera
 ├── Data
 ├── Enemy
 ├── Feedback
 ├── Player
 ├── UI
 └── Systems
✨ Visual & Feedback Design

Strong emphasis on “feel” and readability for mobile gameplay.

Implemented Feedback:
Damage popups (hit confirmation)
Skill unlock notifications
Arrow trail effects
Poison VFX system
Combat animation feedback
Skill impact effects
📱 Platform Target
Primary: Mobile (Android / iOS)
Input: Touch optimized
Performance: Lightweight real-time combat loop
🚀 Build & Run Instructions
Requirements:
Unity 2022.3 LTS (recommended)
Steps:
Open project in Unity Hub

Load main scene:

Assets/Scenes/Main.unity
Press Play (Editor)
Build:
Target platform: Android / iOS
Recommended: IL2CPP + ARM64
🔧 Design Limitations (Transparent for Evaluation)

This is a technical prototype, not a production release.

Known Limitations:
No save/load system implemented yet
UI is placeholder-level (function-first design)
Limited enemy variety (3 archetypes only)
No monetization layer implemented yet
Balancing is in early iteration stage
📈 Expansion Potential (Publisher View)

This prototype is intentionally structured for scalability:

Future Ready Systems:
Weapon system expansion (melee/ranged/magic)
Skill tree system
Idle offline progression
Meta progression layer
Monetization integration (IAP / ads)
Live-ops event system support
👤 Developer

Erhan Doğru
GitHub: https://github.com/ErhanDgr23
