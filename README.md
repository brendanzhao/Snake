Snake
=========

This is the classic Snake game I've decided to recreate during my spare time for fun.<br />
The MSDN style commenting is unncessary however I felt like complying with all StyleCop formatting for this project.<br />

Refactor TODO:<br />
All major refactoring TODO's have been completed.

Features TODO:<br />
Pause functionality.<br />
End game Functionality.<br />
Add obstacles.<br />
Place food blocks in harder to reach places based on difficulty.<br />
Better graphics.<br />
Add power-ups.

Bugs TODO:<br />
The Snake is able to move backwards and thus through itself when moving very slowly. Bug is caused because the game tracks the last direction that has been pressed on the keyboard and not neccessarily the last direction the snake moved. That is, if within 1 timer tick, you press two directions, you can essentially move the snake backwards. For example: If the snake is moving up, then within a single timer tick, you press left and then down, the code will assume the last direction moved was left and will cause the snake to move from up to down in the next tick.<br />
There is a possibility of the food block being drawn ontop of the snake. However it's very unlikely.
