# Plantee

Plantee is a card and map based game. The goal is to fight against climate change by removing all trash from the map and plant a seed on every field.
Just like preventing climate change with the current policy is a matter of luck the game is almost exclusively random. Nevertheless it comes with two map sizes.
At least the smaller one is in principle solvable, but not always for lucks sake. 


## Game Play

### Mouse Management

The game is complete mouse based. Almost all functionality is done by left click. Some are using the right click.

By clickling on the robot and then clicking on a target field the robot moves to the selected field. Fields reachable by the robot are highlighted.
Clicking on a card (if playable) enables the player to play it onto a field. A field where a card can be played is highlighted. 
For tool cards and some action cards it doesen't matter, where they are played at.
Clicking on the field on which the robot is, enables the player to select trash and seeds of grown plants. Grown plants are highlighted in blue.
A tool which is equiped, can be removed by using right click. This makes tool positions usable again.

### Functionality

To reach the goal the robot has to plant seeds. Seed cards are green and have the attributes water, nutrient, toxicity and trash. 
A seed can only be planted onto a field with the same or higher (water and nutrient)/ lower (toxicity and trash) value. 
The field attributes can be seen in the bars for water and nutrient, the field color for toxicty and the trash tower for trash. 
Plants develope after up to 4 rounds. If they have reached the last developement stage, there is a chance of spreading to adjacent fields.
This only happens if the fields attributes are compatible with the the plants attribtes. Also the plant starts to take action. 
The action is reqeapeted in the same round intervall as the developement. If a field does not fulfill the plants needs anymore, the plant will not grow or take action until it is fulfiled again.

To get better field attribute values, cards like water cards or action cards can be used. Action and tool cards can be earned by collecting trash.
Trash collection also puts trash cards in the card deck, which can't do anything, but the trash can be recycled at the dump. The dumo is the filed without any bars.

A round is completed if the mana is empty. Every movement costs mana. Also playing cards cost mana according to the card costs. 
If the cards are empty before the mana is, there will be new handcards and the round is not finished. After each round bad things may be happening, causing some plants to spoil (brown highlight).

In the right lower corner of a card can be seen in which range from the field where a card was played at an action takes place (in case of seeds how many adjacent fields a plant may be spreading at). 
The left lower shows in which distance of the current position a card can be played. 0 means only the current position, otherwise the number means the distance to the cureent position.

The goal is reached, if on every field (except the dump) is a plant regardless of the developement and no field has anymore trash.

Using the menu button one can return to the menu and start a new game.

For a game play see https://youtu.be/jYiehsTQoW4


Good Luck!


## Developement Information

Since the level structure is completely dynamically build, new elements can be added very easly. Most of the functionality can be reached using the manager instances.
New cards can be build by using the scriptable objects. Nevertheless, the complexity is one point for further improvement.

For bugs see issues in GitHub.
