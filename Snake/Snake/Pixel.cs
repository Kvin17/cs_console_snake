using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
   
    public struct Pixel : IPaint
    {
        public static int PIXEL_HEIGHT = 3;
        public static int PIXEL_WIDTH = 6;
       
        private ConsoleColor _color;
        private static char symbol = ' ';
        public int _x { get; set; }
        public int _y { get; set; }
        public Pixel(int x, int y, ConsoleColor color)
        {
            _x = x;
            _y = y;
            _color = color;
        }
        public void Paint()
        {
            Console.BackgroundColor = _color;
            for (int i = 0; i < PIXEL_HEIGHT; i++)
            {
                Console.SetCursorPosition(_x, _y + i);
                for (int j = 0; j < PIXEL_WIDTH; j++)
                    Console.Write(symbol);
            }
            Console.BackgroundColor = default;
        }

        public void Clear()
        {
            Console.BackgroundColor = default;
            for (int i = 0; i < PIXEL_HEIGHT; i++)
            {
                Console.SetCursorPosition(_x, _y + i);
                for (int j = 0; j < PIXEL_WIDTH; j++)
                    Console.Write(symbol);
            }            
        }

        public static bool operator ==(Pixel left, Pixel right) => (left._x == right._x && left._y == right._y);    
        public static bool operator !=(Pixel left, Pixel right) => (left._x != right._x || left._y != right._y);
       
    }
}
