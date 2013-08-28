//-----------------------------------------------------------------------
// <copyright file="FoodBlock.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Represents a piece of food on the Snake game to be eaten.
    /// </summary>
    public class FoodBlock : BaseBlock
    {
        /// <summary>
        /// Represents an array of all the colors the <see cref="FoodBlock"/> can be.
        /// </summary>
        private static Color[] colors = { Color.Red, Color.Orange, Color.Green, Color.Blue, Color.Purple };

        /// <summary>
        /// Represents a random number generator to randomize the properties of the <see cref="FoodBlock"/>.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodBlock"/> class with a random color and position.
        /// </summary>
        /// <param name="availablePositions">A <see cref="List"/> of Points containing all possible positions.</param>
        public FoodBlock(List<Point> availablePositions)
            : base()
        {
            this.FoodColor = colors[random.Next(0, colors.Length)];
            this.Position = availablePositions[random.Next(0, availablePositions.Count)];
        }

        /// <summary>
        /// Gets or sets the FoodType of the <see cref="FoodBlock"/>.
        /// </summary>
        private Color FoodColor
        {
            get;
            set;
        }

        /// <summary>
        /// Draws the <see cref="FoodBlock"/> onto the Snake game.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object which will be drawing to the Snake game.</param>
        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(this.FoodColor), this.Position.X * this.PixelSize.Width, this.Position.Y * this.PixelSize.Height, this.PixelSize.Width, this.PixelSize.Height);
        }
    }
}
