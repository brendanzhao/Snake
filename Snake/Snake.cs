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
            this.SnakeBlocks = new SnakeBlock[length];

            for (int i = 0; i < length; i++)
            {
                this.SnakeBlocks[length - 1 - i] = new SnakeBlock(new Point(1, 1 + i));
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
        /// Gets or sets the SnakeBlocks of the <see cref="Snake"/>.
        /// </summary>
        public SnakeBlock[] SnakeBlocks
        {
            get;
            set;
        }

        /// <summary>
        /// Increases the length of the <see cref="Snake"/> by one.
        /// </summary>
        /// <param name="snakeBlockLocation">A <see cref="Point"/> representing the location of the <see cref="SnakeBlock"/> to be added to the <see cref="Snake"/>.</param>
        public void Grow(Point snakeBlockLocation)
        {
            List<SnakeBlock> snakeBlockList;

            snakeBlockList = this.SnakeBlocks.ToList();
            snakeBlockList.Insert(0, new SnakeBlock(snakeBlockLocation));

            this.SnakeBlocks = snakeBlockList.ToArray();
        }

        /// <summary>
        /// Moves this snake in it's current direction.
        /// </summary>
        public void Move()
        {
            switch (this.Direction)
            {
                case Direction.Down:
                    for (int i = this.SnakeBlocks.Length - 1; i > 0; i--)
                    {
                        this.SnakeBlocks[i].Position = this.SnakeBlocks[i - 1].Position;
                    }

                    this.SnakeBlocks[0].Position = new Point(this.SnakeBlocks[0].Position.X, this.SnakeBlocks[0].Position.Y + 1);

                    break;
                case Direction.Left:
                    for (int i = this.SnakeBlocks.Length - 1; i > 0; i--)
                    {
                        this.SnakeBlocks[i].Position = this.SnakeBlocks[i - 1].Position;
                    }

                    this.SnakeBlocks[0].Position = new Point(this.SnakeBlocks[0].Position.X - 1, this.SnakeBlocks[0].Position.Y);
                    break;
                case Direction.Right:
                    for (int i = this.SnakeBlocks.Length - 1; i > 0; i--)
                    {
                        this.SnakeBlocks[i].Position = this.SnakeBlocks[i - 1].Position;
                    }

                    this.SnakeBlocks[0].Position = new Point(this.SnakeBlocks[0].Position.X + 1, this.SnakeBlocks[0].Position.Y);
                    break;
                case Direction.Up:
                    for (int i = this.SnakeBlocks.Length - 1; i > 0; i--)
                    {
                        this.SnakeBlocks[i].Position = this.SnakeBlocks[i - 1].Position;
                    }

                    this.SnakeBlocks[0].Position = new Point(this.SnakeBlocks[0].Position.X, this.SnakeBlocks[0].Position.Y + -1);
                    break;
                default:
                    break;
            }
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
