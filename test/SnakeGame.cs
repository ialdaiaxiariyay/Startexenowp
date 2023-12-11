using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{
    public class Snake
    {
        private int width;
        private int height;
        private int score;
        private bool gameOver;
        private List<(int, int)> snake;
        private (int, int) food;
        private (int, int) direction;
        private Random random;

        public Snake(int width = 20, int height = 20)
        {
            this.width = width;
            this.height = height;
            score = 0;
            gameOver = false;
            snake = new List<(int, int)>();
            snake.Add((width / 2, height / 2));
            direction = (0, 1);
            random = new Random();
            SpawnFood();
        }

        public void Start()
        {
            while (!gameOver)
            {
                Draw();
                Input();
                Logic();
                Thread.Sleep(100);
            }
            Console.WriteLine("Game Over! Your score: " + score);
        }

        private void Draw()
        {
            Console.Clear();
            for (int i = 0; i < width + 2; i++)
                Console.Write("#");
            Console.WriteLine();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j == 0)
                        Console.Write("#");
                    if (snake.Contains((j, i)))
                        Console.Write("O");
                    else if (food == (j, i))
                        Console.Write("@");
                    else
                        Console.Write(" ");
                    if (j == width - 1)
                        Console.Write("#");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < width + 2; i++)
                Console.Write("#");
            Console.WriteLine();
        }

        private void Input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (direction != (0, 1))
                            direction = (0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        if (direction != (0, -1))
                            direction = (0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (direction != (1, 0))
                            direction = (-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        if (direction != (-1, 0))
                            direction = (1, 0);
                        break;
                }
            }
        }

        private void Logic()
        {
            (int, int) newHead = (snake[0].Item1 + direction.Item1, snake[0].Item2 + direction.Item2);
            if (newHead.Item1 < 0 || newHead.Item1 >= width || newHead.Item2 < 0 || newHead.Item2 >= height || snake.Contains(newHead))
            {
                gameOver = true;
                return;
            }
            snake.Insert(0, newHead);
            if (newHead == food)
            {
                score++;
                SpawnFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void SpawnFood()
        {
            food = (random.Next(width), random.Next(height));
            while (snake.Contains(food))
            {
                food = (random.Next(width), random.Next(height));
            }
        }
    }
}