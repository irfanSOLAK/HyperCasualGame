# HyperCasualGame
This repository includes Unity project files for the game development portion and Windows build files, including the executable (exe) file for running the game on Windows.

## Overview
This project is a classic Hypercasual game featuring Swerve mechanics. The game aims to provide players with a dynamic and entertaining experience. Swerve stands out with its simple controls requiring quick reactions and engaging gameplay mechanics. The game comprises three different sections.

## Game Sections
![1](https://github.com/irfanSOLAK/HyperCasualGame/assets/108720676/dcbdff0d-98b0-4c62-91d0-cb04920a1140)
&nbsp;

### First and Third Games:
•	For both games players attempt to reach the finish line without colliding with obstacles.
![2](https://github.com/irfanSOLAK/HyperCasualGame/assets/108720676/e694b01b-681f-41f5-bc6c-b87ae78bd87b)
&nbsp;

•	In the third section, players compete with an additional 10 characters controlled by AI. The character ranking can be displayed on the left side.
![5](https://github.com/irfanSOLAK/HyperCasualGame/assets/108720676/8990c99f-2c8f-4e63-9bfe-9f6b7a1b8e26)
&nbsp;

### Second Section:
•	Players can paint the object appearing on the screen.

•	The painted portion is displayed as a percentage on top of the object.
![3](https://github.com/irfanSOLAK/HyperCasualGame/assets/108720676/0e3411fc-bc6c-4401-aad0-3782af24c80f)
&nbsp;

## Game Mechanics

### Swerve Mechanic:
With simple controls demanding quick reactions and skills, players maneuver between obstacles.

### Race and Competition:
In the third section, compete with AI-controlled characters to maintain a high ranking.

### Object Painting Mechanic:
In the second section, achieve visual satisfaction by coloring the object displayed on the screen.

## Game Controls
### Swerve Controls (Games 1 and 3):
**A/D or Left/Right Arrow Keys:** Move the character left or right.

**Mouse:** Hold down the left mouse button anywhere on the screen and move the mouse left or right to maneuver the character.

### Paint Controls (Game 2):
**Mouse:** Click on the object to paint it.

## Key Features
•	**Unique Swerve Mechanics:** Distinctive swerve mechanics for smooth and responsive character movement.

•	**Striking Visuals:** Stunning visuals enhanced by advanced "Post Process" settings and captivating design.

•	**Modular Design:** Built with a modular structure for easy updates and scalable features.

•	**Diverse Game Sections:** Three unique game sections with different objectives and challenges.

•	**Competitive AI Racing:** Race against AI-controlled characters in the third section for a challenging competition.

•	**Cinematic Camera Transitions:** Utilizes Cinemachine for smooth and cinematic camera movements, enhancing the overall gaming experience.

# Technical Highlights
## Design Patterns Implementation:
•	**Singleton:** Utilizing a generic Singleton structure, objects can easily acquire this feature when needed. In this project, a GameManager named GameBehaviour has been created to implement this pattern efficiently.

•	**Observer:** Implemented an Observer design pattern using the subscription model. This pattern, facilitated by the Notification Manager script, enables objects to register and unregister themselves for events named with enums. Some objects, inheriting from a listener base class, listen to these events.

•	**State Pattern:** Various states are defined using enums. Additionally, the GameBehaviour has been optimized for understanding the three different games and ensuring the game operates accordingly based on these states.

## Scriptable Objects
Utilizing Scriptable Objects, we've established a streamlined system that enables the centralized control of diverse game parameters. This approach not only enhances the ease of managing aspects within the game but also ensures a more flexible and modular structure. By centralizing parameters such as character attributes and game settings, Scriptable Objects provide a unified hub for adjustments, promoting efficiency in development and facilitating quick iterations. This design choice contributes to a cleaner architecture, separating data from code and promoting maintainability throughout the project.

## Extension Methods
Various extension methods have been employed to condense and optimize reusable code snippets, enhancing code readability and promoting efficient development practices.

## AI
The artificial intelligence (AI) within the system has undergone significant development to improve its capabilities and performance. Through rigorous optimization and fine-tuning, the AI now exhibits enhanced decision-making, increased adaptability to various in-game scenarios, and improved overall intelligence. These advancements contribute to a more sophisticated and responsive AI, providing a richer and more immersive experience for players.

## GameManager
Using the GameManager, communication between objects has been facilitated, and the architecture has been optimized for easy understanding and maintenance.

## Untiy version
2020.3.35f1

