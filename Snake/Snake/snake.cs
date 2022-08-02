using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class snake : IPaint
    {
        public Pixel Head  { get; set; }
        public Queue<Pixel> Body { get; }
        public snake(int x, int y)
        {
            Head = new Pixel(x * Pixel.PIXEL_WIDTH,y * Pixel.PIXEL_HEIGHT, ConsoleColor.White);
            Body = new Queue<Pixel>();
            Body.Enqueue(new Pixel((x - 3) * Pixel.PIXEL_WIDTH, y * Pixel.PIXEL_HEIGHT, ConsoleColor.DarkGray));
            Body.Enqueue(new Pixel((x - 2) * Pixel.PIXEL_WIDTH, y * Pixel.PIXEL_HEIGHT, ConsoleColor.DarkGray));
            Body.Enqueue(new Pixel((x - 1) * Pixel.PIXEL_WIDTH, y * Pixel.PIXEL_HEIGHT, ConsoleColor.DarkGray));            
        }       
        public void Paint()
        {
            Body.Peek().Paint();
            Head.Paint();          
        }        
        public void Enqueue()
        {           
            Body.Enqueue(new Pixel(Head._x, Head._y, ConsoleColor.DarkGray));
            new Pixel(Head._x, Head._y, ConsoleColor.DarkGray).Paint();
        }
        public void Dequeue() => Body.Dequeue().Clear();

        public void Clear()
        {
            
        }
    }
    
}
