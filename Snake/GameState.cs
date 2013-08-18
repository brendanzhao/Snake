//-----------------------------------------------------------------------
// <copyright file="GameState.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    /// <summary>
    /// Specifies the state that the Snake game is currently at.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Specifies that the game is currently displaying the menu.
        /// </summary>
        Menu,

        /// <summary>
        /// Specifies that the game is currently being played.
        /// </summary>
        Playing,

        /// <summary>
        /// Specifies that the game is currently paused.
        /// </summary>
        Pause,

        /// <summary>
        /// Specifies that the game is currently at the end screen.
        /// </summary>
        End
    }
}
