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
            using (Game game = new Game(1024, 600, 3, 2)) { game.Run(60, 0); }
        }
    }
}
