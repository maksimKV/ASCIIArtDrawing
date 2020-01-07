using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIArtDrawing
{
    public class Point
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string Colour { get; set; }

        public Point (int height, int width, string colour)
        {
            Height = height;
            Width = width;
            Colour = colour;
        }

        public bool Equals(Point toCompare)
        {
            return this.Width == toCompare.Width &&
                   this.Height == toCompare.Height;
        }
    }
}
