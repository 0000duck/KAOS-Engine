using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.Utilities
{
    public struct Shader
    {
        public int ID { get; set; }

        public Shader(int id)
            : this()
        {
            ID = id;
        }
    }
}
