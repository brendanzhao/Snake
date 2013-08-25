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
            this.DoubleBuffered = true;
            this.gameTimer.Interval = 75;
            this.state = GameState.Menu;
            this.Invalidate();
        }

        /// <summary>
        /// Moves the snake depending on what direction is currently pressed.
        /// </summary>
        private void GameLoop()
        {
            if (SnakeUtility.WillEatFood(this.snake, this.food))
            {
                this.snake.Grow(this.food.Position);
                this.food = new FoodBlock(this.ClientSize.Width / BaseBlock.StandardBlockSize.Width, this.ClientSize.Height / BaseBlock.StandardBlockSize.Height);
            }
            else
            {
                this.snake.Move();
            }

            if (SnakeUtility.HasCollidedWithSelf(this.snake) || SnakeUtility.HasHitBounds(this.snake, this.ClientSize.Width, this.ClientSize.Height))
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
        /// Event which sets the difficulty of the Snake game to easy.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void Easy_Click(object sender, EventArgs e)
        {
            this.gameTimer.Interval = 100;
            this.Refresh();
        }

        /// <summary>
        /// Event which sets the difficulty of the Snake game to medium.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void Medium_Click(object sender, EventArgs e)
        {
            this.gameTimer.Interval = 75;
            this.Refresh();
        }

        /// <summary>
        /// Event which sets the difficulty of the Snake game to hard.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void Hard_Click(object sender, EventArgs e)
        {
            this.gameTimer.Interval = 50;
            this.Refresh();
        }

        /// <summary>
        /// Event which sets the difficulty of the Snake game to insane.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void Insane_Click(object sender, EventArgs e)
        {
            this.gameTimer.Interval = 25;
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

            g = e.Graphics;

            switch (this.state)
            {
                case GameState.Menu:
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
            this.Controls.Clear();
            this.snake = new Snake(SnakeGame.DefaultSnakeLength);
            this.food = new FoodBlock(this.ClientSize.Width / BaseBlock.StandardBlockSize.Width, this.ClientSize.Height / BaseBlock.StandardBlockSize.Height);
            this.state = GameState.Playing;
            this.Invalidate();
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
            this.Invalidate();
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
