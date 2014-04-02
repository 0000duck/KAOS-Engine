using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Game game = new Game(1280, 720, 3, 3)) { game.Run(60, 30); }
        }
    }
}
