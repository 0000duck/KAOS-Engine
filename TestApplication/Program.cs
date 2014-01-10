using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGL;
using AWGL.Scene;

using OpenTK;

namespace TestApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (Display mainDisplay = new Display())
            {
                mainDisplay.Run(30.0);
            }
        }
    }
}
