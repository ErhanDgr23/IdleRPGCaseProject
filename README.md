# Noko Games - Idle RPG / Arcade Idle Technical Case

A mobile-focused arcade idle RPG prototype developed in Unity for the Noko Games Developer Technical Case. The core focus of this project is on creating clean and modular gameplay systems, satisfying combat feedback, progressive player growth, and a seamless idle/arcade gameplay loop.

## 📋 Case Requirements Overview
As per the technical case instructions, here are the required details:

*   **Game Engine:** Unity 3D
*   **Programming Language:** C#
*   **Weapon Choice:** Bow (Projectile-based with automatic targeting and shooting)
*   **Build Target:** Mobile Platform (Android/iOS)

### 🚀 How to Run the Project
1. Clone this repository to your local machine.
2. Open the project using **Unity** (Ensure you are using a compatible version, preferably 2022.3 LTS or newer).
3. In the Project window, navigate to `Assets/Scenes/` and open the **Main Scene**.
4. Set the Game window aspect ratio to a mobile portrait resolution (e.g., 1080x1920 or Simulator view) for the intended UI experience.
5. Press the **Play** button in the Unity Editor.

---

## 🎮 Gameplay Overview
The player survives inside an arena while automatically attacking nearby enemies using a bow. Enemies continuously spawn with increasing intensity over time. By defeating enemies, the player earns gold that can be used to purchase permanent stat upgrades. As the player progresses, new combat skills unlock automatically based on total kills.

### ⚔️ Combat System
*   **Automatic Enemy Targeting & Shooting:** The player automatically finds the closest enemy and shoots.
*   **Projectile-Based Combat:** Real physics and trajectory for arrows.
*   **Progressive Difficulty:** Enemy spawn rate and intensity dynamically increase over time to create gameplay pressure.

### 🆙 Upgrade System
Players can open the upgrade panel at any time using the UI.
*   *Note:* The game properly **pauses** while the upgrade menu is open.
*   **Available Upgrades:** Damage, Max HP, Attack Speed, and Movement Speed.
*   Each upgrade directly affects gameplay and improves combat efficiency instantly.

### ✨ Skill System (Kill-Count Unlocks)
The game features 3 unlockable bow skills that trigger automatically as the player reaches specific kill milestones:

1.  **Multishot (Unlocked at 10 Kills):** Shoots 3 arrows simultaneously (Left, Center, Right).
2.  **Poison Arrow (Unlocked at 25 Kills):** Applies poison damage over time (DoT) to enemies. Enemies receive a green material/color visual effect while poisoned.
3.  **Rain of Arrows (Unlocked at 50 Kills):** Every few seconds, deadly arrows rain down from the sky onto up to 5 nearby enemies (AoE).

### 👾 Enemy System
*   **3 Enemy Types:** Featuring different movement speeds and health values.
*   **Weighted Spawning:** Progressive spawn scaling with safe-zone constraints (enemies spawn outside the camera view/safe area).

### 📱 Controls
*   **Movement:** Virtual Joystick (Mobile-friendly).
*   **Combat:** Fully automatic (Idle mechanics).

---

## 🛠️ Technical Architecture
The project uses a modular gameplay architecture with separated gameplay systems to ensure scalability and clean code.

**Main Systems:**
*   Player Controller & Combat System
*   Enemy AI & Dynamic Spawner
*   Upgrade & Currency System
*   Skill Manager
*   UI & Popup Feedback System

**Project Structure:**
    Scripts/
     ├── Animations
     ├── Camera
     ├── Data
     ├── Enemy
     ├── Feedback
     ├── Other
     ├── Player
     └── UI

---

## 🎨 Visual Feedback & "Juice"
To make the combat feel satisfying, the following feedback systems were implemented:
*   Damage popups (Floating text)
*   Skill unlock UI popups
*   Arrow trails
*   Poison VFX (Material swapping)
*   Combat animations and skill effects

---

## ⚠️ Known Limitations & Missing Features
As requested in the case guidelines, the focus was kept strictly on mechanical functionality within the 72-hour limit. The following features are intentionally limited or missing:

*   **Save/Load System:** Not implemented. Gold, upgrades, and kill counts reset upon restarting the game.
*   **Placeholder Visuals:** Some UI elements and environments use placeholder assets. The focus was heavily shifted towards clean code and mechanics rather than polished art.
*   **Balancing:** While the difficulty scales dynamically, the exact gold costs, enemy health, and damage values may need further mathematical balancing for a long-term production release.
*   **Limited Enemy Variety:** Currently restricted to 3 archetypes to prove the concept of the weighted spawner system.

---

## 👨‍💻 Developer
**Erhan Doğru**
*   **GitHub:** [https://github.com/ErhanDgr23](https://github.com/ErhanDgr23)
