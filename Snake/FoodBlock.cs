//-----------------------------------------------------------------------
// <copyright file="FoodBlock.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents a piece of food on the Snake game which can be eaten.
    /// </summary>
    public class FoodBlock : BaseBlock
    {
        /// <summary>
        /// Represents a random number generator to position the <see cref="FoodBlock"/> randomly and be of a random <see cref="FoodType"/>.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Represents an array of all the <see cref="FoodType"/> enumeration values.
        /// </summary>
        private static FoodType[] foodTypes = (FoodType[])Enum.GetValues(typeof(FoodType));

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodBlock"/> class with a specified Position and random <see cref="FoodType"/>.
        /// </summary>
        /// <param name="horizontalBound">An <see cref="int"/> representing the maximum X position to place the <see cref="FoodBlock"/>.</param>
        /// <param name="verticalBound">An <see cref="int"/> representing the maximum Y position to place the <see cref="FoodBlock"/>.</param>
        public FoodBlock(int horizontalBound, int verticalBound)
            : base()
        {
            this.FoodType = (FoodType)foodTypes.GetValue(random.Next(0, foodTypes.Length));
            this.Position = new Point(FoodBlock.random.Next(0, horizontalBound), FoodBlock.random.Next(0, verticalBound));
        }

        /// <summary>
        /// Gets or sets the FoodType of the <see cref="FoodBlock"/>.
        /// </summary>
        private FoodType FoodType
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
            // The color of the food block to be drawn.
            Color food;

            switch (this.FoodType)
            {
                case FoodType.Apple:
                    food = Color.Red;
                    break;
                case FoodType.Blueberry:
                    food = Color.Blue;
                    break;
                case FoodType.Grape:
                    food = Color.Purple;
                    break;
                case FoodType.Lime:
                    food = Color.Green;
                    break;
                case FoodType.Orange:
                    food = Color.Orange;
                    break;
                default:
                    throw new ArgumentException("FoodBlocks should always have a type.");
            }

            g.FillRectangle(new SolidBrush(food), this.Position.X * this.PixelSize.Width, this.Position.Y * this.PixelSize.Height, this.PixelSize.Width, this.PixelSize.Height);
        }
    }
}
