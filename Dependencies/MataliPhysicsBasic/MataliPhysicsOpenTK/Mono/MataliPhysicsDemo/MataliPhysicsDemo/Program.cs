using System;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Demo demo = new Demo())
            {
                demo.Title = "Matali Physics Demo " + PhysicsEngine.Version + " (OpenTK)";
                demo.Run();
            }
        }
    }
}
