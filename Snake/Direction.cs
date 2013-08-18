//-----------------------------------------------------------------------
// <copyright file="Direction.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    /// <summary>
    /// Specifies the direction to move the <see cref="Snake"/> after the next specified time interval.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Specifies the <see cref="Direction"/> as down.
        /// </summary>
        Down,

        /// <summary>
        /// Specifies the <see cref="Direction"/> as left.
        /// </summary>
        Left,

        /// <summary>
        /// Specifies the <see cref="Direction"/> as right.
        /// </summary>
        Right,

        /// <summary>
        /// Specifies the <see cref="Direction"/> as up.
        /// </summary>
        Up
    }
}
