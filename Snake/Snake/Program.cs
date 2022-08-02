using System;

namespace Snake
{
    class Program 
    {
        private const int WIDTH = 16;
        private const int HEIGHT = 12;//visota
        static Pixel apple = new Pixel(7 * Pixel.PIXEL_WIDTH, 7 * Pixel.PIXEL_HEIGHT, ConsoleColor.Red);
        static ConsoleKey OldDirection = ConsoleKey.D;
        static bool inGame = false;
        static ConsoleColor colorWall = ConsoleColor.Yellow;
        static int apples = 0;

        public static void Main()
        {
              Program app = new Program();
              Console.SetWindowSize(WIDTH*Pixel.PIXEL_WIDTH , HEIGHT*Pixel.PIXEL_HEIGHT);
              Console.BufferHeight = HEIGHT*Pixel.PIXEL_HEIGHT+1;
              Console.BufferWidth = WIDTH*Pixel.PIXEL_WIDTH+1;     
              Console.CursorVisible = false;
              while (true)
              {
                  snake snak = new snake(5,5);
              snak.Paint();
              Pixel[] wall = new Pixel[(WIDTH+HEIGHT)*2];           
              app.WallDraw(wall, colorWall);
              foreach (var pix in snak.Body)
                  pix.Paint();
              inGame = true;
              OldDirection = ConsoleKey.D;
              Console.BackgroundColor = colorWall;
              Console.ForegroundColor = ConsoleColor.Black;
              Console.SetCursorPosition(4, 0);
              Console.Write($"Apples : {apples}");
                apples = 0;
              int recordApples = 0;
              try
              {
                  StreamReader streamReader = new StreamReader("snake_record.txt");
                  string record = streamReader.ReadLine();
                    recordApples = Int32.Parse(record);
                  streamReader.Close();
              }
              catch (Exception e)
              {
                  Console.SetCursorPosition(4, 0);
                  Console.WriteLine("Exception: " + e.Message);
              }
              Console.SetCursorPosition(20, 0);
              Console.Write($"Record : {recordApples}");
            
                while (inGame)
                {
                    app.Update(snak);
                }
                Console.Clear();
                Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2);
                Console.WriteLine($"The End, Result : {apples}");
                if (apples > recordApples)
                {
                    try
                    {
                        StreamWriter sw = new StreamWriter("snake_record.txt");
                        sw.Write(apples);
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        Console.SetCursorPosition(4, 0);
                        Console.WriteLine("Exception: " + e.Message);
                    }
                }
                
                Thread.Sleep(1300);
                Console.Clear();

            }
            Console.ReadLine();
        }
       
        void Update(snake Snake)
        {           
            apple.Paint();
            Snake.Paint();
            Move(Snake, apple);           
        }
        ConsoleKey ScanKey()
        {
            Console.BackgroundColor = colorWall;
            Console.ForegroundColor = colorWall;
            Console.SetCursorPosition(WIDTH * Pixel.PIXEL_WIDTH-1, HEIGHT * Pixel.PIXEL_HEIGHT-1);
            Thread.Sleep(250);
            if (Console.KeyAvailable)
                return Console.ReadKey().Key;
            else return OldDirection;
        }
        void SpawnApple(snake Snake)// 
        {          
            Random random = new Random();
            int it;
            do
            {  it = 0;
                apple = new Pixel(random.Next(1,WIDTH-1) * Pixel.PIXEL_WIDTH, random.Next(1,HEIGHT-1) * Pixel.PIXEL_HEIGHT, ConsoleColor.Red);
                foreach(Pixel pixel in Snake.Body)
                    if (apple == pixel)
                        it++;
            }
            while (apple == Snake.Head || it > 0);
            apples++;
            //Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(4, 0);
            Console.Write($"Apples : {apples}");
        }
        void CheckContact(snake Snake)
        {
            if (Snake.Head._x == 0 || Snake.Head._y == 0 || Snake.Head._x/Pixel.PIXEL_WIDTH == WIDTH-1 || Snake.Head._y/Pixel.PIXEL_HEIGHT == HEIGHT-1)
                inGame = false;
            foreach (Pixel pixel in Snake.Body)
                if (pixel == Snake.Head)
                    inGame = false;
            if (Snake.Head == apple)
            {              
                SpawnApple(Snake);
                Snake.Enqueue();               
            }
            else
            {              
                Snake.Enqueue();
                Snake.Dequeue();
            }
            
               
        }
        void Move(snake Snake, Pixel apple)
        {
            ConsoleKey Direction = ScanKey();
            if (OldDirection == ConsoleKey.D && Direction == ConsoleKey.A)
                Direction = OldDirection;
            else if(OldDirection == ConsoleKey.A && Direction == ConsoleKey.D)
                Direction = OldDirection;
            if (OldDirection == ConsoleKey.S && Direction == ConsoleKey.W)
                Direction = OldDirection;
            else if (OldDirection == ConsoleKey.W && Direction == ConsoleKey.S)
                Direction = OldDirection;
            CheckContact(Snake);
            switch (Direction)
            {
                case ConsoleKey.W:                   
                    Snake.Head = new Pixel(Snake.Head._x, Snake.Head._y - (1*Pixel.PIXEL_HEIGHT), ConsoleColor.White);                    
                    break;
                case ConsoleKey.S:
                    Snake.Head = new Pixel(Snake.Head._x, Snake.Head._y + (1 * Pixel.PIXEL_HEIGHT), ConsoleColor.White);                    
                    break;
                case ConsoleKey.A:
                    Snake.Head = new Pixel(Snake.Head._x - (1*Pixel.PIXEL_WIDTH), Snake.Head._y, ConsoleColor.White);                   
                    break;
                case ConsoleKey.D:
                    Snake.Head = new Pixel(Snake.Head._x + (1 * Pixel.PIXEL_WIDTH), Snake.Head._y , ConsoleColor.White);                   
                    break;
                default:
                    break;
            }
            OldDirection = Direction;
        }    
        void WallDraw(Pixel[] wall, ConsoleColor _color)
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                wall[i] = new Pixel(0, i * Pixel.PIXEL_HEIGHT, _color);
                wall[i + HEIGHT] = new Pixel(WIDTH * Pixel.PIXEL_WIDTH - Pixel.PIXEL_WIDTH, i * Pixel.PIXEL_HEIGHT, _color);
            }
            for (int i = 0; i < WIDTH; i++)
            {
                wall[i + HEIGHT * 2] = new Pixel(i * Pixel.PIXEL_WIDTH, 0, _color);
                wall[i + HEIGHT * 2 + WIDTH] = new Pixel(i * Pixel.PIXEL_WIDTH, HEIGHT * Pixel.PIXEL_HEIGHT - Pixel.PIXEL_HEIGHT, _color);
            }
            for (int i = 0; i < (WIDTH + HEIGHT) * 2; i++)
            {
                wall[i].Paint();
            }
        }
    }
}