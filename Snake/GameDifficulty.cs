//-----------------------------------------------------------------------
// <copyright file="GameDifficulty.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    /// <summary>
    /// Specifies the difficulty of the Snake game.
    /// </summary>
    public enum GameDifficulty
    {
        /// <summary>
        /// Specifies the <see cref="Snake"/> to move at the slowest pace.
        /// </summary>
        Easy,

        /// <summary>
        /// Specifies the <see cref="Snake"/> to move at a slow pace.
        /// </summary>
        Medium,

        /// <summary>
        /// Specifies the <see cref="Snake"/> to move at a fast pace.
        /// </summary>
        Hard,

        /// <summary>
        /// Specifies the <see cref="Snake"/> to move at the fastest pace.
        /// </summary>
        Insane
    }
}
