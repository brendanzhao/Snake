//-----------------------------------------------------------------------
// <copyright file="SnakeGame.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Represents the user control of the Snake game.
    /// </summary>
    public partial class SnakeGame : Form
    {
        /// <summary>
        /// Represents the starting length of the <see cref="Snake"/> when the game begins.
        /// </summary>
        private const int DefaultSnakeLength = 5;

        /// <summary>
        ///  Represents the difficulty of the game to determine how fast the snake will move.
        /// </summary>
        private GameDifficulty difficulty;

        /// <summary>
        /// Represents the food block that the snake needs to eat in order to grow.
        /// </summary>
        private FoodBlock food;

        /// <summary>
        /// Represents the user controlled snake in the game.
        /// </summary>
        private Snake snake;

        /// <summary>
        ///  Represents the state the game is currently at to determine what to draw on the form.
        /// </summary>
        private GameState state;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnakeGame"/> class.
        /// </summary>
        public SnakeGame()
        {
            this.InitializeComponent();
            this.difficulty = GameDifficulty.Medium;
            this.gameTimer.Interval = 1000;
            this.state = GameState.Menu;
            this.gameBoard.Invalidate();
        }

        /// <summary>
        /// Sets the timer interval representing how fast the snake will move depending on the selected difficulty.
        /// </summary>
        private void ApplyDifficulty()
        {
            switch (this.difficulty)
            {
                case GameDifficulty.Easy:
                    this.gameTimer.Interval = 500;
                    break;
                case GameDifficulty.Medium:
                    this.gameTimer.Interval = 250;
                    break;
                case GameDifficulty.Hard:
                    this.gameTimer.Interval = 100;
                    break;
                case GameDifficulty.Insane:
                    this.gameTimer.Interval = 25;
                    break;
                default:
                    throw new ArgumentException("The Interval of the timer should have been set");
            }
        }

        /// <summary>
        /// Moves the snake depending on what direction is currently pressed.
        /// </summary>
        private void GameLoop()
        {
            if (SnakeUtility.WillEatFood(this.snake, this.food))
            {
                this.snake.Grow(this.food.Position);
                this.food = new FoodBlock(this.gameBoard.Width / BaseBlock.StandardBlockSize.Width, this.gameBoard.Height / BaseBlock.StandardBlockSize.Height);
            }
            else
            {
                this.snake.Move();
            }

            if (SnakeUtility.HasCollidedWithSelf(this.snake) || SnakeUtility.HasHitBounds(this.snake, this.gameBoard.Width, this.gameBoard.Height))
            {
                this.GameOver();
            } 
        }

        /// <summary>
        /// Stops the game of the Snake.
        /// </summary>
        private void GameOver()
        {
            this.gameTimer.Enabled = false;
        }

        /// <summary>
        /// Outlines the currently selected difficulty on the menu.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object which will be drawing to the Snake game.</param>
        private void OutlineCurrentDifficulty(Graphics g)
        {
            // Used to define the color that will outline the currently selected difficulty.
            Pen colorPen;

            colorPen = new Pen(Color.Red);

            switch (this.difficulty)
            {
                case GameDifficulty.Easy:
                    g.DrawRectangle(colorPen, this.easy.Location.X, this.easy.Location.Y, this.easy.Size.Width, this.easy.Size.Height);
                    break;
                case GameDifficulty.Medium:
                    g.DrawRectangle(colorPen, this.medium.Location.X, this.medium.Location.Y, this.medium.Size.Width, this.medium.Size.Height);
                    break;
                case GameDifficulty.Hard:
                    g.DrawRectangle(colorPen, this.hard.Location.X, this.hard.Location.Y, this.hard.Size.Width, this.hard.Size.Height);
                    break;
                case GameDifficulty.Insane:
                    g.DrawRectangle(colorPen, this.insane.Location.X, this.insane.Location.Y, this.insane.Size.Width, this.insane.Size.Height);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Event which sets the difficulty of the Snake game to easy.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void Easy_Click(object sender, EventArgs e)
        {
            this.difficulty = GameDifficulty.Easy;
            this.Refresh();
        }

        /// <summary>
        /// Event which sets the difficulty of the Snake game to medium.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void Medium_Click(object sender, EventArgs e)
        {
            this.difficulty = GameDifficulty.Medium;
            this.Refresh();
        }

        /// <summary>
        /// Event which sets the difficulty of the Snake game to hard.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void Hard_Click(object sender, EventArgs e)
        {
            this.difficulty = GameDifficulty.Hard;
            this.Refresh();
        }

        /// <summary>
        /// Event which sets the difficulty of the Snake game to insane.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void Insane_Click(object sender, EventArgs e)
        {
            this.difficulty = GameDifficulty.Insane;
            this.Refresh();
        }

        /// <summary>
        /// Event which performs all the drawing on the Snake game.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="PaintEventArgs"/> containing the event data.</param>
        private void GameBoard_Paint(object sender, PaintEventArgs e)
        {
            // Draws to the form.
            Graphics g;

            // Defines the color to draw with.
            Pen pen;

            g = e.Graphics;

            pen = new Pen(Color.Red);

            switch (this.state)
            {
                case GameState.Menu:
                    this.OutlineCurrentDifficulty(g);
                    break;
                case GameState.Playing:
                    this.snake.Draw(g);
                    this.food.Draw(g);
                    break;
                case GameState.Pause:
                    break;
                case GameState.End:
                    break;
                default:
                    throw new ArgumentException("The game should always have a state.");
            }
        }

        /// <summary>
        /// Event which clears the menu and starts the game of Snake.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void NewGame_Click(object sender, EventArgs e)
        {
            this.gameBoard.Controls.Clear();
            this.snake = new Snake(SnakeGame.DefaultSnakeLength);
            this.food = new FoodBlock(this.gameBoard.Width / BaseBlock.StandardBlockSize.Width, this.gameBoard.Height / BaseBlock.StandardBlockSize.Height);
            this.state = GameState.Playing;
            this.ApplyDifficulty();
            this.gameBoard.Invalidate();
            this.gameTimer.Enabled = true;
        }

        /// <summary>
        /// Event which is used to control the game loop to keep moving the snake.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            this.GameLoop();
            this.gameBoard.Invalidate();
        }

        /// <summary>
        /// Event which is used to determine the direction to move the snake.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void SnakeGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Down:
                    if (this.snake.Direction != Direction.Up)
                    {
                        this.snake.Direction = Direction.Down;
                    }

                    break;
                case Keys.Left:
                    if (this.snake.Direction != Direction.Right)
                    {
                        this.snake.Direction = Direction.Left;
                    }

                    break;
                case Keys.Right:
                    if (this.snake.Direction != Direction.Left)
                    {
                        this.snake.Direction = Direction.Right;
                    }

                    break;
                case Keys.Up:
                    if (this.snake.Direction != Direction.Down)
                    {
                        this.snake.Direction = Direction.Up;
                    }

                    break;
                case Keys.Space:
                    // TODO: Implement pause function.
                    break;
                default:
                    break;
            }
        }
    }
}
