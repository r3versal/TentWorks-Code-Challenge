# TentWorks-Code-Challenge
 Code Challenge, part of the application process for employment with TentWorks.
 This is a cooking master (salad chef) clone made in Unity3D.

## Getting Started
Be sure the Unity.dll file is present in the same folder/location as the Demo!

## Gameplay Controls
Move Player 1: WASD
Move Player 2: IJKL
To pickup vegetables walk towards them until you see it has been picked up in your inventory
To use the chopping board and plate walk toward until the vegetable(s) have been removed from your inventory and placed on either plate or chopping board
To use the garbage can walk towards the garbage can until the vegetable(s) have been removed from your inventory
To pickup vegetables from the chopping board walk towards it until you see the UI - "Deliver to Customer!"
To deliver vegetables to the customer walk towards the customer whose recip matches the salad you are delivering until you recieve positive or negative UI feedback regarding customer satisifaction
To pickup bonus simply walk over it
Other Notes: Player can only use plate if they are only holding 1 vegetable, players may only use the chopping board on their side of the screen, players with a score less than zero will have a score of zero at the end of the game, only the winning player's score will add to high scores list.

## Additonal Features
Opimize for multiple screen aspect ratios
camera that pans in/out depending on the distance between players in the level. 
Add unit tests for all the use cases, some considerations include: all possible combinations of recipe interactions between player and customer
Build AI player 2(ie play against a computer)
Build AI obstacle (like a rat running across the kitchen floor in a random pattern every 25 seconds)
Utilize object pooling wherever possible to improve performance - customers are a possible implementation point
Build more levels with different customer requirements
Build Load screen/Main menu
Redesign on the PlayerActivity script (and a few other scripts) how the logic is being checked against gameObject.name strings - use better practice
Review opportunities to implement inheritence in scripts, specifically in the Recipe, Customer classes
Design a more robust bonus pickup system
Add difficulty level selection as well as player(user) profiles
Add remove observer method to relevant scripts for optimization

## Installing
Use Unity 2019.4.3

## Project Documentation
[Trello - Basic Task Breakdown](https://trello.com/b/Yc6ccq2R)
[LucidChart - Early Design of Notification Center](https://app.lucidchart.com/invitations/accept/3e691b57-5d39-42b3-893b-48f62bc1b66e)

## Additional Notes

Design Approach decisions:
Unity: I decided to use the traditional Unity GameObjects as opposed to ECS, mainly because I’m more familiar with it and therefore it would be quicker and run into fewer gotchas. However if given a new project I would assess if the Entity Framework made more sense because of the potential optimization gains associated.
Trello: Actually used this for the build, so should give you an idea of how I think through a full process, however the cards are less detailed than normal and usually I would have due dates for each card. And a few rules for managing the state of the cards based on current column.
LucidChart: Basic implementation of notification system design. I didn’t continue to update throughout the process, once it was up and running I knew where I wanted to put things but in a real life scenario I would have used the design for each notification scenario and kept the documentation/chart in sync. 

## Authors

Carmen Shaw - Initial work- [r3versal](https://github.com/r3versal)