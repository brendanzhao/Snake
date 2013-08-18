//-----------------------------------------------------------------------
// <copyright file="BaseBlock.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents a basic block in the Snake game.
    /// </summary>
    public class BaseBlock
    {
        /// <summary>
        /// Defines the default size for all blocks created.
        /// </summary>
        public static readonly Size StandardBlockSize = new Size(10, 10);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBlock"/> class.
        /// </summary>
        public BaseBlock()
        {
            this.PixelSize = BaseBlock.StandardBlockSize;
        }

        /// <summary>
        /// Gets or sets the PixelSize of the <see cref="BaseBlock"/>.
        /// </summary>
        /// <returns>A <see cref="Size"/> representing the pixel width and height of the <see cref="BaseBlock" /> object.</returns>
        public Size PixelSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Position of the <see cref="BaseBlock"/>.
        /// </summary>
        /// <returns>A <see cref="Point"/> representing the grid position of the <see cref="BaseBlock"/> object.</returns>
        public Point Position
        {
            get;
            set;
        }

        /// <summary>
        /// Draws the <see cref="BaseBlock"/> onto the Snake game.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object which will be drawing to the Snake game.</param>
        public virtual void Draw(Graphics g)
        {
            throw new NotImplementedException("This method is never intended to be directly ran.");
        }
    }
}
