using OpenTK;
using System;
using System.Drawing;

namespace AWGL
{
    /// <summary>
    /// AWEngine Main Entry Piont
    /// </summary>
    public sealed class AWEngine
    {
        #region Singleton Pattern - Thread Safe
        private static volatile AWEngine instance = new AWEngine();
        private static object syncRoot = new Object();

        private AWEngine() { }

        public static AWEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new AWEngine();
                    }
                }

                return instance;
            }
        } 
        #endregion

        [STAThread]
        public static void Main()
        {
            using (AWScene game = new AWScene())
            {
                game.Run(30,0);
            }
        }

        public static string AppName
        {
            get
            {
                return "AWEngine";
            }
            
        }
    }
}
