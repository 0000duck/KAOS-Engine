using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWGL.Utilities
{
    public struct Texture
    {
        public int ID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Texture(int id, int width, int height) :this()
        {
            ID = id;
            Width = width;
            Height = Height;
        }
    }
}
