# ISLAND - Final Project for Programming Fundamentals

## How to Run the Game

1. Clone the repository with ```git clone https://github.com/kaseadoun/island.git```
2. Enter ```dotnet run``` in the terminal.
3. Have fun!

## Original Concept

The original concept of this game was a resource management game. The following resources were to be managed:
- Health Points
- Food
- Energy

### Original Gameplay Loop
The player has to survive 10 days and was given choices each day before rescue arrives. They must decide how much energy they want to expend during that day. In order to regain enery, the player must sleep, but at the expense of their Health Points. The lower the health points, the less energy the player can regain each day. To regain Health Points, the player has to eat food.

In order to find food, the player must hunt and explore the jungle of the island. If the player comes across a fight either with the island natives (cannibals) or dangerous creatures. The greater the threat, the greater the reward.

The combat system would be similar to a rock paper scissors mechanic (The chart below will go Player Decision -> Enemy Decision):

| PLAYER LOST | DRAW | ENEMY LOST|
|-------------|------|-----------|
| Attack -> Dodge | Attack -> Attack | Attack -> Taunt |
| Block / Dodge -> Taunt | Block / Dodge -> Dodge | Block / Dodge -> Attack |
| Throw Something on the Ground -> Attack | Throw Something on the Ground -> Taunt | Throw Something on the Ground -> Dodge |

The player will also have an additional choice, and that is to run. However, if the player decides to run, a good chunk of energy will be used.

If the player manages to survive with at least the HP remaining by the end of 10 days, the player will win.

## New, Final, and Submitted Concept

I decided to change the scope of the project as the previous idea was too large for what I was able to do currently. The game is partially a resource management game, but more so a puzzle adventure. The only resource that needs to be managed was the following:
- Time

### Final Game Loop

With the final game, the player is given a set amount of time to get their fortifications fully built in order to protect them from the onslaught of cannibals that will come after a certain time. The player must explore the island and get items, but exploring and building all takes time so they must use their time wisely.

The player will essentially have 3 main choices at base:
- Map (to explore other locations)
- Build (to build the fortifications)
- Inventory (to check what items they have)

With other locations, choices will be given to the player to decide what to do in that area. Some areas require an item in order to be accessed.

After exploring and building the fortification, when it reaches the time of 1900, the flare will be shot and the islanders will being the attack.

If the right fortifications have been built on the right spot, then the player will win.

If not, then there will be different dialogue depending on the player's decision.
