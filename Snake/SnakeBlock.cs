//-----------------------------------------------------------------------
// <copyright file="SnakeBlock.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents a single block of the Snake controlled by the user.
    /// </summary>
    public class SnakeBlock : BaseBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnakeBlock"/> class.
        /// </summary>
        /// /// <param name="position">A <see cref="Point"/> representing the position of the <see cref="SnakeBlock"/> on the screen.</param>
        public SnakeBlock(Point position)
            : base()
        {
            this.Position = position;
        }

        /// <summary>
        /// Draws the <see cref="SnakeBlock"/> onto the Snake game.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object which will be drawing to the Snake game.</param>
        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), this.Position.X * this.PixelSize.Width, this.Position.Y * this.PixelSize.Height, this.PixelSize.Width, this.PixelSize.Height);
        }
    }
}
