using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAOS.Utilities
{
    public struct Texture
    {
        public int ID { get; set; }
        public int VAO { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Texture(int id, int vao, int width, int height) :this()
        {
            ID = id;
            VAO = vao;
            Width = width;
            Height = Height;
        }
    }
}
