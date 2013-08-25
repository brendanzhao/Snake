//-----------------------------------------------------------------------
// <copyright file="Snake.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// Represents the snake controlled by the user in the Snake game.
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

            this.Direction = Direction.Down;
        }

        /// <summary>
        /// Gets or sets the Direction of the <see cref="Snake"/>.
        /// </summary>
        public Direction Direction
        {
            get;
            set;
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
        /// <param name="snakeBlockLocation">A <see cref="Point"/> representing the location of the <see cref="SnakeBlock"/> to be added to the <see cref="Snake"/>.</param>
        public void Grow(Point snakeBlockLocation)
        {
            this.SnakeBlocks.AddFirst(new SnakeBlock(snakeBlockLocation));
        }

        /// <summary>
        /// Moves this snake in it's current direction.
        /// </summary>
        public void Move()
        {
            switch (this.Direction)
            {
                case Direction.Down:
                    this.SnakeBlocks.AddFirst(new SnakeBlock(new Point(this.SnakeBlocks.First.Value.Position.X, this.SnakeBlocks.First.Value.Position.Y + 1)));
                    break;
                case Direction.Left:
                    this.SnakeBlocks.AddFirst(new SnakeBlock(new Point(this.SnakeBlocks.First.Value.Position.X - 1, this.SnakeBlocks.First.Value.Position.Y)));
                    break;
                case Direction.Right:
                    this.SnakeBlocks.AddFirst(new SnakeBlock(new Point(this.SnakeBlocks.First.Value.Position.X + 1, this.SnakeBlocks.First.Value.Position.Y)));
                    break;
                case Direction.Up:
                    this.SnakeBlocks.AddFirst(new SnakeBlock(new Point(this.SnakeBlocks.First.Value.Position.X, this.SnakeBlocks.First.Value.Position.Y - 1)));
                    break;
                default:
                    break;
            }

            this.SnakeBlocks.RemoveLast();
        }

        /// <summary>
        /// Draws the Snake onto the Snake game.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object which will be drawing to the Snake game.</param>
        public void Draw(Graphics g)
        {
            foreach (SnakeBlock s in this.SnakeBlocks)
            {
                s.Draw(g);
            }
        }
    }
}
