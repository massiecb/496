Force of Nature Project Read Me

The purpose of the 496 project was to try and implement the game as outlined in a document given to my by Brian for a game called force of nature. Force of nature is a card game and the goal is to reduce the HP of your opponent to zero. Currently, only minions can deal damage to the player and only if the opposing player does not have any other minions.

All cards are played into a shared stack. Spell cards are assumed to happen all at once. Creatures attack in order of speed.

Each player has a hand zone, a tactics zone, and a creature zone. The opposing player can see how many cards are in each zone, but not the actual card. Once the card is in the stack, everyone can see it.

What is Implemented:

Deck creation, some game play, all data types required
prototype XML documents which exemplify the kind of XML documents which could be used

Card Prefabs: Each card type has its own prefab since it displays different information
There is also a cardback prefab to display that to the opponent when the card is played into the appropriate zone

What Isn’t Implemented:

Creatures can’t attack each other: really important. each creature should be able to attack the opposing player’s creatures or else the game can’t end as creatures act as blockers. It was not clear in the document if the attacker gets to choose which creatures to hit or if the defender does. I would assume it would be the attacker because of the order of the stack.

Spell cards don’t work: Spell cards and arms cards can’t target or do anything when placed on the stack. Spell and arms cards effects are
created and accessible by using their name for their respective dicitonary. YOu would then apply these effects onto the target card, however
targetting is decided to work

Can only use one deck: Changes required to deck building and the selection deck, but the framework is there to add the ability.


Some Display Bugs: cards will sometimes not display, sometimes will jump to another part of the player’s field. The parenting is correct however. I assume some bugs are due to lag my laptop causes. Requires further testing

Needs more cards: Actual cards would be good. I just made up a bunch with what I thought would be standard. No cards have art either.

Limit playable cards: currently the player can play his whole hand, needs to be limited.

Card Text spills over card: on longer texts, it goes beyond the bounds of the card

You will find in the Prototype folder a bunch of scripts and scenes that I have tinkered with to try and get where I got to. 

Scripts:

AccessPreFabTest was figuring out how to access the text compoenents as I changed the prefabs over time. I went through about three or four
of them.

Card LoaderAlpha is the second attempt at the main data type this game uses. It loads the original cardinfo Xml document, also labled alpha
GameLoop1 is the second attempt at the main game. I tried to use a canvas to display the game on and ran into some difficulties displaying
the cards

GamePlay1 was my first attempt at programming the main game. I stopped part way through because the display wasn not working very well,
so I switched over to GameLoop1 to try and use a canvas

HandCard Handler was my first attempt at displaying the hand cards. I also tried to combine it with the mouse click dector which was not
very successful. I stopped this script because I switched prefabs.

Scenes:
Try1 was my first scene attempt at the game, gameplay1 and gameplay2 are scenes which I build upon my learning. The final GamePlay scene
is built upon my knowledge in working on these scenes.

PreFabDemos was me changing a few of my prefabs and using AccessPrefabTest to access their text components. This is how I finalized my 
prefabs for the cards.


How to Play:
A deck is prebuilt, though you can play our own. navigate through the menu and play the game. 
Player1 goes first. Cards from the hand are clicked and placed in correct zone, then when the card in the zone is clicked it is added to the stack. When Player1’s turn is done, that player should click end turn and then the camera will be focused on player2’s board. Player2 can then do the same things and click end turn.

After each player has had a chance to play, the round is ended and the stack is resolved automatically.

Was this successful?

Maybe. The game can be made. It would take a lot more people and time than just me. There are also some unanswered questions from the game design document, so more discussion with the originators would be needed to continue.

A way nicer UI would make the game a lot better.

A lot was left unfinished, but I did what I could.

Thanks for reading!
