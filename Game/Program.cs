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
            using (Window game = new Window(1024, 600)) { game.Run(); }
        }
    }
}
