//-----------------------------------------------------------------------
// <copyright file="SnakeUtility.cs" company="N/A">
//     Personal Project. Open Source.
// </copyright>
//-----------------------------------------------------------------------
namespace Snake
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Contains methods used for calculating the logic in the Snake game.
    /// </summary>
    public static class SnakeUtility
    {
        /// <summary>
        /// Checks if snake has collided with itself.
        /// </summary>
        /// <param name="snake">A <see cref="Snake"/> being checked to for self collision.</param>
        /// <returns>true if the snake has collided with itself; otherwise false.</returns>
        public static bool HasCollidedWithSelf(Snake snake)
        {
            // The snake cannot collide with itself if there are only 4 snake blocks.
            if (snake.SnakeBlocks.Count <= 4)
            {
                return false;
            }

            // The position of the head of the snake.
            Point snakeHead = snake.SnakeBlocks.First.Value.Position;

            // The current block of the snake being tested for collision.
            LinkedListNode<SnakeBlock> current = snake.SnakeBlocks.First.Next.Next.Next;

            while (current.Next != null)
            {
                current = current.Next;

                if (snakeHead.X == current.Value.Position.X && snakeHead.Y == current.Value.Position.Y)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the snake has hit the boundaries of the game.
        /// </summary>
        /// <param name="snake">A <see cref="Snake"/> being checked if it has past the bounds.</param>
        /// <param name="horizontalBound">A <see cref="int"/> representing the maximum x-bound that the snake can be at.</param>
        /// <param name="verticalBound">A <see cref="int"/> representing the maximum y-bound that the snake can be at.</param>
        /// <returns>true if the snake has hit the boundary of the game; otherwise false.</returns>
        public static bool HasHitBounds(Snake snake, int horizontalBound, int verticalBound)
        {
            // The position of the head of the snake.
            Point snakeHead = snake.SnakeBlocks.First.Value.Position;

            if (snakeHead.X < 0 || snakeHead.Y < 0 || (snakeHead.X * BaseBlock.StandardBlockSize.Width) >= horizontalBound || (snakeHead.Y * BaseBlock.StandardBlockSize.Height) >= verticalBound)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the snake will eat a food block the next time it moves.
        /// </summary>
        /// <param name="snake">A <see cref="Snake"/> being checked if it will eat a food block.</param>
        /// <param name="food">A <see cref="FoodBlock"/> being checked if it will be eaten.</param>
        /// <returns>true if the snake will hit a food block on it's next move; otherwise false.</returns>
        public static bool WillEatFood(Snake snake, FoodBlock food)
        {
            // The position of the head of the snake.
            Point snakeHead = snake.SnakeBlocks.First.Value.Position;

            switch (snake.Direction)
            {
                case Direction.Down:
                    if (snakeHead.X == food.Position.X && (snakeHead.Y + 1) == food.Position.Y)
                    {
                        return true;
                    }

                    break;
                case Direction.Left:
                    if (snakeHead.X - 1 == food.Position.X && snakeHead.Y == food.Position.Y)
                    {
                        return true;
                    }

                    break;
                case Direction.Right:
                    if (snakeHead.X + 1 == food.Position.X && snakeHead.Y == food.Position.Y)
                    {
                        return true;
                    }

                    break;
                case Direction.Up:
                    if (snakeHead.X == food.Position.X && (snakeHead.Y - 1) == food.Position.Y)
                    {
                        return true;
                    }

                    break;
                default:
                    return false;
            }

            return false;
        }
    }
}
