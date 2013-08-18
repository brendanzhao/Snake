Snake
=========

This is the classic Snake game I've decided to recreate during my spare time.<br />
This game is very basic and was purposefully written in an extremely simple manner.<br />

Refactor TODO:<br />
Subclass Panel to enable DoubleBuffered to stop the flickering.<br />
Remove logic from event handlers and create separate methods for them.<br />
Have Snake inherit from Collection instead of having a Property representing an Array of SnakeBlocks.<br />
Everytime the Snake grows, the array of SnakeBlocks is converted to a Collection, a block is added to it and then it's converted back into an array. BAD.<br />
Re-order methods to be in alphabetical order.

Features TODO:<br />
Pause Functionality.<br />
End Game Functionality.<br />
Add Obstacles.<br />
Place food blocks in harder to reach places based on difficulty.<br />
Better graphics.
