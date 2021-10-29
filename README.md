[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-f059dc9a6f8d3a56e377f745f24479a46679e63a5d9fe6f495e02850cd0d8118.svg)](https://classroom.github.com/online_ide?assignment_repo_id=445997&assignment_repo_type=GroupAssignmentRepo)


**The University of Melbourne**
# COMP30019 – Graphics and Interaction

Final Electronic Submission (project): **4pm, November 1**

Do not forget **One member** of your group must submit a text file to the LMS (Canvas) by the due date which includes the commit ID of your final submission.

You can add a link to your Gameplay Video here but you must have already submit it by **4pm, October 17**

# Project-2 README

You must modify this `README.md` that describes your application, specifically what it does, how to use it, and how you evaluated and improved it.

Remember that _"this document"_ should be `well written` and formatted **appropriately**. This is just an example of different formating tools available for you. For help with the format you can find a guide [here](https://docs.github.com/en/github/writing-on-github).


**Get ready to complete all the tasks:**

- [x] Read the handout for Project-2 carefully.

- [x] Brief explanation of the game.

- [x] How to use it (especially the user interface aspects).

- [x] How you designed objects and entities.

- [x] How you handled the graphics pipeline and camera motion.

- [x] The procedural generation technique and/or algorithm used, including a high level description of the implementation details.

- [x] Descriptions of how the custom shaders work (and which two should be marked).

- [x] A description of the particle system you wish to be marked and how to locate it in your Unity project.

- [x] Description of the querying and observational methods used, including a description of the participants (how many, demographics), description of the methodology (which techniques did you use, what did you have participants do, how did you record the data), and feedback gathered.

- [x] Document the changes made to your game based on the information collected during the evaluation.

- [x] References and external resources that you used.

- [x] A description of the contributions made by each member of the group.

## Table of contents
* [Team Members](#team-members)
* [Explanation of the game](#explanation-of-the-game)
* [Technologies](#technologies)
* [Using Images](#using-images)
* [Code Snipets ](#code-snippets)

## Team Members

| Name | Task | State |
| :---         |     :---:      |          ---: |
| Yibo Peng  | Inital project, finish player moving function (include normal, sprint, crouch style)     |  Done |
| Student Name 2    | Shader      |  Testing |
| Student Name 3    | README Format      |  Amazing! |

## Explanation of the game

### 1. Game Description
Our game is named Forest Runner, which is a first-person shooter game with 3 different difficulty levels. The player needs to kill goblins to get Health, Energy, Ammo Pickups and reach the target score to win.

### 2. Game Instruction
The user opens the game landscape and can see the main menu with three buttons: Start , Help and a close button.  To see the game instructions, just click the “Help” button and can see the instructions details. And there is a close button on the top right side for the player to exit the game. To start the game,  just click the “Start”. After clicking the “Start” button, the user can see the Level Choice menu and can choose “Easy”, “Normal” , “Difficult” level scenes. After choosing a level, the user needs to click the main green button under the level instruction part to start the game. During the game, the user can press “ESC” to mute the background music or choose “Again” or “Quit” the game. For the key operation, WASD keys for player move, if pressing at the same time, the player will sprint. Clicking the left mouse will start shooting. Space key for Jump. C key for Crouch or Stand up. R key for Reload. Killing the general monster, score increases 10, there is a chance to pick up Red or Blue pickups. Killing a special monster, score increases 30, there is a chance to pick up Green or Ammo pick up. Red pick up can make the player's health increase 30%.Blue pick up can make the player's energy increase 50%.Green pick up can make the player's health and health increase 50%.Ammo pick up can make the player's damage increase 25%. And the user needs to reach the target score on the menu within the limited time to win the game.


	
## Technologies
Project is created with:
* Unity 2021.1.13f1

## Using Images

You can use images/gif by adding them to a folder in your repo:

<p align="center">
  <img src="Gifs/Q1-1.gif"  width="300" >
</p>

To create a gif from a video you can follow this [link](https://ezgif.com/video-to-gif/ezgif-6-55f4b3b086d4.mov).

## Code Snippets 

You can include a code snippet here, but make sure to explain it! 
Do not just copy all your code, only explain the important parts.

```c#
public class firstPersonController : MonoBehaviour
{
    //This function run once when Unity is in Play
     void Start ()
    {
      standMotion();
    }
}
```




