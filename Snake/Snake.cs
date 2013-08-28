//-----------------------------------------------------------------------
// <copyright file="Snake.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Represents the snake controlled by the user in the game.
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Snake"/> class.
        /// </summary>
        /// <param name="length">An <see cref="int"/> specifying the default length of the Snake.</param>
        public Snake(int length)
        {
            this.SnakeBlocks = new LinkedList<SnakeBlock>();

            for (int i = 0; i < length; i++)
            {
                this.SnakeBlocks.AddFirst(new SnakeBlock(new Point(1, 1 + i)));
            }
        }

        /// <summary>
        /// Gets the SnakeBlocks of the <see cref="Snake"/>.
        /// </summary>
        public LinkedList<SnakeBlock> SnakeBlocks
        {
            get;
            private set;
        }

        /// <summary>
        /// Increases the length of the <see cref="Snake"/> by one.
        /// </summary>
        /// <param name="snakeBlock">A <see cref="SnakeBlock"/> to be added to the end of the <see cref="Snake"/>.</param>
        public void Grow(SnakeBlock snakeBlock)
        {
            this.SnakeBlocks.AddLast(snakeBlock);
        }

        /// <summary>
        /// Moves the <see cref="Snake"/> in a specified direction.
        /// </summary>
        /// <param name="direction">A <see cref="Point"/> representing the direction to move.</param>
        public void Move(Point direction)
        {
            this.SnakeBlocks.AddFirst(new SnakeBlock(new Point(this.SnakeBlocks.First.Value.Position.X + direction.X, this.SnakeBlocks.First.Value.Position.Y + direction.Y)));
            this.SnakeBlocks.RemoveLast();
        }

        /// <summary>
        /// Draws the <see cref="Snake"/> onto the Snake game.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object which will be drawing to the game.</param>
        public void Draw(Graphics g)
        {
            foreach (SnakeBlock s in this.SnakeBlocks)
            {
                s.Draw(g);
            }
        }
    }
}
