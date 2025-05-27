# 🎮 Project Overview

This project is a **top-down shooter** game that blends **action-packed gameplay** with **rogue-like elements**. Set in a post-apocalyptic world, players take on the role of a survivor who must venture out from a secure base to **locate and rescue scattered companions**.

## 🧠 Core Concept

The game begins at a safe base area. The player's mission is to explore dangerous zones, fight off enemies, and **rescue lost allies**. Along this journey, you're accompanied by an **AI-powered intelligent robot companion** that can switch between multiple functional modes, such as combat, support, and scouting, depending on the situation.

---

## ⚙️ Technical Features

- Developed with **Unity Engine (C#)**
- Uses **Scriptable Objects** for data management (e.g., level data, character stats, robot modes)
- **Component-based architecture** for modular and extensible design
- Game interactions handled via **event-driven system** (Action/Delegate based)
- **Procedurally generated levels** (planned/in development)
- **AI Companion System**:
  - Multiple modes (attack, defense, scout, etc.)
  - Dynamic mode switching based on in-game context
  - Behavior changes depending on player distance, enemy density, and other environmental factors
- **Companion Rescue System**:
  - Each rescued ally unlocks new active abilities or gameplay advantages
  - Companions contribute both narratively and mechanically

---

## 🕹️ Gameplay Mechanics

- **Top-Down Controls**: Character movement via twin-stick (WASD + Mouse)
- **Rogue-like Elements**: Each run provides unique challenges and randomness
- **Resource Management**: Collect materials (e.g., scrap) for upgrades and progression
- **Companion Robot**: AI robot supports the player in combat and navigation with modular behavior
- **Upgrade System**: Both the player and companion can be upgraded using resources
- **Interactive Objects**: Players can interact with environmental elements to complete objectives or gain advantages

---

## 🗺️ Development Status

> The project is currently in early development. Core gameplay systems (character movement, robot companion behavior, basic combat) are prototyped or functional.

---

# Project - DevLog 📝

⚠️Because I started the devlog late, I will write what I have done so far superficially. The rest will be more detailed.
### Current Features 📈

- The player; 
   - Has 8 dimension animations
   - Has health system
   - Has health bar
   - Has 3D Crosshair
   - Has red line gun barrel to crosshair
   - Can aims to crosshair without any animation corruption
   - Can smoothly aim toward the position of the crosshair.
   - Can kill the enemies
   - Can collect scrap dropped by enemies.
   - Can get damage by enemies

 - The Enemy; 
   - Has die, idle, run and attack animation
   - Enemy can drop scarp to collect

 -The Companion;
   -Has 3 Modes: Healer, Attacer, Base
   -Follows the player
   -Can attack nearest enemy

## Core Assets - 🎨
<p align="Left">
  <img src="ReadmeFiles/Images/Gun.png" alt="gun_png" width="200" style="margin: 20px;"/>
  <img src="ReadmeFiles/Images/Enemy.png" alt="enemy_png" width="200" style="margin: 20px;"/>
  <img src="ReadmeFiles/Images/Player.png" alt="player_png" width="200" style="margin: 20px;"/>
  <img src="ReadmeFiles/Images/CmpBot.png" alt="cmpbot_png" width="200" style="margin: 20px;"/>
</p>

## Player Actions - 🔄
<p align="Left">
  <img src="ReadmeFiles/Gifs/8DMove.gif" alt="Player 8D Movement" width="250" height="200" style="margin: 20px;"/>
  <img src="ReadmeFiles/Gifs/Turning.gif" alt="Turning" width="250" height = "200"/>
  <img src="ReadmeFiles/Gifs/Die.gif" alt="Die" width="250" height = "200"/>
  <img src="ReadmeFiles/Gifs/Kill_Collect.gif" alt="Kill_Collect" width="250" height = "200"/>
  <img src="ReadmeFiles/Gifs/Firing.gif" alt="Firing" width="250" height = "200"/>
</p>

## Companion Modes - 🟢🔵🔴
<p align="Center">
  <img src="ReadmeFiles/Gifs/CompanionModes.gif" alt="Player 8D Movement" width="500" height="350" style="margin: 20px;"/>
</p>

---

## 05.21.2025 Updates 🔄

- Character, companion and gun datas edited. 📝
- The number of upgrade areas has been increased. Separate upgrade areas were created for player, weapons and Companion 🔼🔼🔼
  
<table>
  <tr>
    <td align="center" valign="middle">
      <img src="ReadmeFiles/Images/CmpUpgradeArea.png" alt="CmpUpgradeArea" width="250"/>
    </td>
    <td align="center" valign="middle">
      <img src="ReadmeFiles/Images/WeaponUpgradeArea.png" alt="WeaponUpgradeArea" width="250"/>
    </td>
    <td align="center" valign="middle">
      <img src="ReadmeFiles/Images/PlayerUpgradeArea.png" alt="PlayerUpgradeArea" width="250"/>
    </td>
  </tr>
</table>

 - Upgrade System Added 🛠️
 - Save System Added 📝

<table>
  <tr>
    <td align="center" valign="middle">
      <img src="ReadmeFiles/Images/BotData.png" alt="BotData" width="250"/>
    </td>
    <td align="center" valign="middle">
      <img src="ReadmeFiles/Images/PlayerData.png" alt="PlayerData" width="250"/>
    </td>
    <td align="center" valign="middle">
      <img src="ReadmeFiles/Images/WeaponData.png" alt="WeaponData" width="250"/>
    </td>
  </tr>
</table>

---

## 05.22.2025 Updates 🔄

- I installed Odin Inspector to speed up the development process and testing.
- I changed the companion's mod switching feature. Now, mod switching is locked while inside the base.
- During action, we can switch between healer and attacker modes.
- Inside the base, the companion automatically switches to base mode and it cannot be changed.

<table>
  <tr>
    <td align="center" valign="middle">
      <img src="ReadmeFiles/Gifs/CmpBaseMode.gif" alt="CmpBaseMode" width="250"/>
    </td>
    <td align="center" valign="middle">
      <img src="ReadmeFiles/Gifs/Cmp_Attacker_Healer.gif" alt="Cmp_Attacker_Healer" width="250"/>
    </td>
  </tr>
</table>

## 05.27.2025 Updates 🔄

- The moderation system of the Companion Bot was made more modular.
- The required models for the new levels were added to the Companion Bot.
- Upgrades were enabled.

<table>
  <tr>
    <td align="center" valign="middle">
      <div><strong>Level 2</strong></div>
      <img src="ReadmeFiles/Images/cmp_LvL_2.png" alt="Cmp2" width="250"/>
    </td>
    <td align="center" valign="middle">
      <div><strong>Level 3</strong></div>
      <img src="ReadmeFiles/Images/cmp_LvL_3.png" alt="Cmp3" width="250"/>
    </td>
  </tr>
</table>

