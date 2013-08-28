//-----------------------------------------------------------------------
// <copyright file="GameState.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    /// <summary>
    /// Specifies the state that the game is currently in.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Specifies that the menu is being displayed.
        /// </summary>
        Menu,

        /// <summary>
        /// Specifies that the main game is being played.
        /// </summary>
        Playing,

        /// <summary>
        /// Specifies that the game is paused.
        /// </summary>
        Pause,

        /// <summary>
        /// Specifies that the game is over.
        /// </summary>
        End
    }
}
