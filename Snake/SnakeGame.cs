//-----------------------------------------------------------------------
// <copyright file="SnakeGame.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    using System;
    using System.Collections.Generic;
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
        /// Represents the border drawn around the currently selected difficulty.
        /// </summary>
        private Rectangle difficultyBorder;

        /// <summary>
        /// Represents the direction to move the <see cref="Snake"/>.
        /// </summary>
        /// <remarks>Should only ever hold values from -1 to 1.</remarks>
        private Point direction;

        /// <summary>
        /// Represents the empty positions on the grid where a new food block can be placed.
        /// </summary>
        private List<Point> emptyPositions;

        /// <summary>
        /// Represents the food block that the <see cref="Snake"/> needs to eat in order to grow.
        /// </summary>
        private FoodBlock food;

        /// <summary>
        /// Represents the grid size of the snake game where each block represents one node. THIS IS NOT PIXEL SIZE.
        /// </summary>
        private Size gridSize;

        /// <summary>
        /// Represents the last direction the <see cref="Snake"/> moved in the previous tick in order to prevent moving backwards.
        /// </summary>
        private Point previousDirection;

        /// <summary>
        /// Represents the user controlled <see cref="Snake"/> in the game.
        /// </summary>
        private Snake snake;

        /// <summary>
        /// Represents the state the game is currently at to determine what to draw on the form.
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
            this.difficultyBorder = new Rectangle(this.medium.Location.X, this.medium.Location.Y, this.medium.Size.Width, this.medium.Size.Height);
            this.state = GameState.Menu;
            this.Invalidate();
        }

        /// <summary>
        /// Performs the main loop of game which is ran on every tick of the game timer.
        /// </summary>
        private void GameLoop()
        {
            if (SnakeUtility.WillEatFood(this.snake, this.food, this.direction))
            {
                this.snake.Grow(this.snake.SnakeBlocks.Last.Value);
                this.emptyPositions = SnakeUtility.GetEmptyBlockPositions(this.snake, this.gridSize);
                this.food = new FoodBlock(this.emptyPositions);
            }

            this.snake.Move(this.direction);
            this.previousDirection = this.direction;

            if (SnakeUtility.HasCollidedWithSelf(this.snake) || SnakeUtility.HasHitBounds(this.snake, this.gridSize.Width, this.gridSize.Height) || this.emptyPositions.Count == 0)
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
            this.difficultyBorder = new Rectangle(this.easy.Location.X, this.easy.Location.Y, this.easy.Size.Width, this.easy.Size.Height);
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
            this.difficultyBorder = new Rectangle(this.medium.Location.X, this.medium.Location.Y, this.medium.Size.Width, this.medium.Size.Height);
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
            this.difficultyBorder = new Rectangle(this.hard.Location.X, this.hard.Location.Y, this.hard.Size.Width, this.hard.Size.Height);
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
            this.difficultyBorder = new Rectangle(this.insane.Location.X, this.insane.Location.Y, this.insane.Size.Width, this.insane.Size.Height);
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

            // Used to determine the colour being drawn to the form.
            Pen pen;

            g = e.Graphics;
            pen = new Pen(Color.Red);

            switch (this.state)
            {
                case GameState.Menu:
                    g.DrawRectangle(pen, this.difficultyBorder);
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
            this.gridSize = new Size(this.ClientSize.Width / BaseBlock.StandardBlockSize.Width, this.ClientSize.Height / BaseBlock.StandardBlockSize.Height);
            this.snake = new Snake(SnakeGame.DefaultSnakeLength);
            this.emptyPositions = SnakeUtility.GetEmptyBlockPositions(this.snake, this.gridSize);
            this.food = new FoodBlock(this.emptyPositions);
            this.direction = new Point(0, 1);
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
            this.direction = SnakeUtility.ChangeDirection(this.previousDirection, e.KeyData);
        }
    }
}
