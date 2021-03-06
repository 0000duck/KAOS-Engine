﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAOS.Utilities
{
    public struct Texture
    {
        public int Id { get; set; }
        public int Vao { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Texture(int id, int vao, int width, int height) :this()
        {
            Id = id;
            Vao = vao;
            Width = width;
            Height = Height;
        }
    }
}
