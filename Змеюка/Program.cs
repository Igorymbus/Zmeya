enum BorderSize
{
    MaxRightBorder = 10,
    MaxBottomBorder = 10
}

class SnakeGame
{
    private Thread drawingThread;
    private bool isRunning;

    public void Start()
    {
        isRunning = true;
        drawingThread = new Thread(DrawSnake);
        drawingThread.Start();
    }

    public void Stop()
    {
        isRunning = false;
        drawingThread.Join();
    }

    private void DrawSnake()
    {
        while (isRunning)
        {
            Console.Clear();
            
            DrawBorder();
            
            Thread.Sleep(500);
        }
    }

    private void DrawBorder()
    {
        Console.SetCursorPosition((int)BorderSize.MaxRightBorder, 0);
        for (int i = 0; i <= (int)BorderSize.MaxBottomBorder; i++)
        {
            Console.SetCursorPosition((int)BorderSize.MaxRightBorder, i);
            Console.Write('|');
        }

        for (int i = 0; i <= (int)BorderSize.MaxRightBorder; i++)
        {
            Console.SetCursorPosition(i, (int)BorderSize.MaxBottomBorder);
            Console.Write('-');
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        SnakeGame game = new SnakeGame();
        game.Start();

        Console.WriteLine("Press any key to stop the game...");
        Console.ReadKey(true);

        game.Stop();
    }
    
}



class Program2
{
    static int mapWidth = 20;
    static int mapHeight = 10;
    static int snakeHeadX;
    static int snakeHeadY;
    static int foodX;
    static int foodY;
    static int score;
    static bool gameOver;




    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    static Direction snakeDirection;


    static List<Segment> snake = new List<Segment>();

    class Segment
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    static void Main(string[] args)
    {
        Console.Title = "Змейка";
        Console.CursorVisible = false;


        snakeHeadX = mapWidth / 2;
        snakeHeadY = mapHeight / 2;


        snakeDirection = Direction.Right;


        GenerateFood();


        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {

                var key = Console.ReadKey(true).Key;


                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (snakeDirection != Direction.Down)
                            snakeDirection = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        if (snakeDirection != Direction.Up)
                            snakeDirection = Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (snakeDirection != Direction.Right)
                            snakeDirection = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        if (snakeDirection != Direction.Left)
                            snakeDirection = Direction.Right;
                        break;
                }
            }

            MoveSnake();
            CheckCollision();
            Draw();

            Thread.Sleep(100);

            Console.SetCursorPosition(0, mapHeight + 2);
            Console.WriteLine("Игра окончена. Ваш счет: " + score);
            Console.ReadLine();
        }

        static void MoveSnake()
        {

            var newHead = new Segment();


            switch (snakeDirection)
            {
                case Direction.Up:
                    newHead.X = snakeHeadX;
                    newHead.Y = snakeHeadY - 1;
                    break;
                case Direction.Down:
                    newHead.X = snakeHeadX;
                    newHead.Y = snakeHeadY + 1;
                    break;
                case Direction.Left:
                    newHead.X = snakeHeadX - 1;
                    newHead.Y = snakeHeadY;
                    break;
                case Direction.Right:
                    newHead.X = snakeHeadX + 1;
                    newHead.Y = snakeHeadY;
                    break;
            }

            snake.Insert(0, newHead);


            snakeHeadX = newHead.X;
            snakeHeadY = newHead.Y;


            if (snakeHeadX == foodX && snakeHeadY == foodY)
            {
                score++;
                GenerateFood();
            }
            else
            {

                snake.RemoveAt(snake.Count - 1);
            }
        }

        static void CheckCollision()
        {
            if (snakeHeadX < 0 || snakeHeadX >= mapWidth || snakeHeadY < 0 || snakeHeadY >= mapHeight)
            {
                gameOver = true;
                return;
            }


            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[i].X == snakeHeadX && snake[i].Y == snakeHeadY)
                {
                    gameOver = true;
                    return;
                }
            }
        }

        static void GenerateFood()
        {

            var random = new Random();
            foodX = random.Next(0, mapWidth);
            foodY = random.Next(0, mapHeight);
        }

        static void Draw()
        {
            Console.Clear();


            for (int i = 0; i < mapWidth + 2; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
                Console.SetCursorPosition(i, mapHeight + 1);
                Console.Write("#");
            }
            for (int i = 0; i < mapHeight + 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
                Console.SetCursorPosition(mapWidth + 1, i);
                Console.Write("#");
            }


            foreach (var segment in snake)
            {
                Console.SetCursorPosition(segment.X + 1, segment.Y + 1);
                Console.Write("*");
            }


            Console.SetCursorPosition(foodX + 1, foodY + 1);
            Console.Write("@");


            Console.SetCursorPosition(mapWidth + 4, 0);
            Console.Write("Счет: " + score);
        }
    }
}

// (*_*)

