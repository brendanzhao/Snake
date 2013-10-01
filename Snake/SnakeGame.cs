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
        /// Represents whether or not the game can be restarted.
        /// </summary>
        private bool canRestart = false;

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
        /// Represents the object drawing to the form.
        /// </summary>
        private Graphics g;

        /// <summary>
        /// Represents the grid size of the snake game where each block represents one node.
        /// </summary>
        /// <remarks>THIS IS NOT PIXEL SIZE.</remarks>
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
            if (this.direction.X == 0 && this.direction.Y == 0)
            {
                return;
            }

            if (SnakeUtility.WillEatFood(this.snake, this.food, this.direction))
            {
                this.snake.Grow();
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
        /// Stops the game.
        /// </summary>
        private void GameOver()
        {
            // Displays a replay text when the game is over.
            Label replay;

            this.gameTimer.Enabled = false;
            this.canRestart = true;

            replay = new Label();
            replay.Text = "Press Space to Play Again";
            replay.AutoSize = true;
            replay.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            replay.Left = (this.ClientSize.Width - replay.PreferredWidth) / 2;
            replay.Top = (this.ClientSize.Height - replay.PreferredHeight) / 2;
            this.Controls.Add(replay);
        }

        /// <summary>
        /// Creates/Recreates the game and then starts it.
        /// </summary>
        private void NewGame()
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
            this.g = e.Graphics;

            switch (this.state)
            {
                case GameState.Menu:
                    this.g.DrawRectangle(new Pen(Color.Red), this.difficultyBorder);
                    break;
                case GameState.Playing:
                    this.snake.Draw(this.g);
                    this.food.Draw(this.g);
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
        /// Event which starts the game of Snake.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void NewGame_Click(object sender, EventArgs e)
        {
            this.NewGame();
        }

        /// <summary>
        /// Event which is used to control the game loop to keep moving the <see cref="Snake"/>.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            this.GameLoop();
            this.Invalidate();
        }

        /// <summary>
        /// Event which is used to determine the direction to move the <see cref="Snake"/>.
        /// </summary>
        /// <param name="sender">An <see cref="object"/> representing the source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        private void SnakeGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.canRestart && e.KeyData == Keys.Space)
            {
                this.canRestart = false;
                this.NewGame();
            }
            else
            {
                this.direction = SnakeUtility.ChangeDirection(this.direction, this.previousDirection, e.KeyData);
            }
        }
    }
}
