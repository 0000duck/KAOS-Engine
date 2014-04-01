using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    class ModernGLControl : GLControl
    {
        // 32bpp color, 24bpp z-depth, 8bpp stencil and 4x antialiasing
        // OpenGL version is major=3, minor=3
        public ModernGLControl()
            : base(new GraphicsMode(32, 24, 8, 4), 3, 3, GraphicsContextFlags.ForwardCompatible)
        { }
    }
}
