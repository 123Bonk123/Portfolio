# ▎Description of the Test Assignment

▎1. Introduction

This document describes a project completed as part of a test assignment in Unity. The main goal is to create a buff system for the character, accessible through interaction with a capsule.

▎2. General Concept

The project includes a capsule that offers the player a choice of three different buffs. The buffs affect the character's attributes and can be stacked multiple times. The player can interact with the capsule by entering its area of effect and pressing the E key, then clicking on the desired buff with the mouse.

![image](https://github.com/user-attachments/assets/7ddaad55-4679-4bd4-bf69-68130d886607)


▎3. Main Components

▎3.1 Capsule

  • PlayerDetector: responsible for interacting with the player and tracking their position.

  • BonusCapsuleView: responsible for the visual effects of the capsule and signals for granting bonuses.

▎3.2 Character

  • CharacterStats: a structure containing the character's attributes.

  • Character: manages the character's attributes and applies buffs.

  • PlayerView: responsible for the character's interaction with the game world, including picking up buffs.

▎3.3 Buffs

  • Implemented using interfaces.

  • Buff 1: health +50

  • Buff 2: damage +10

  • Buff 3: movement speed +10

  • Buff selection: the player can choose one of the buffs by hovering the cursor over the card and clicking with the left mouse button.

▎3.4 HUD

  • Displays the current attributes of the character (health, damage, movement speed).

▎4. Architecture

  • A flexible architecture has been used for the buff system, allowing for easy addition of new types of buffs.

  • Interaction between components is implemented using C# events.

  • Dependencies between classes are passed through interfaces using Zenject, minimizing coupling between entities.

▎5. Plans

  • Implement buffs using generics: IBuff<T> where T : IStats for easier addition of new buffs.

  • Add temporary buffs by implementing the Decorator pattern.

▎6. Conclusion

This project demonstrates my skills in game development with Unity, including creating a flexible architecture, managing character states, and implementing user interfaces.
