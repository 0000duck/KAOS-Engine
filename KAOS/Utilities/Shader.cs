using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.Utilities
{
    public struct Shader
    {
        public int Id { get; set; }

        public Shader(int id)
            : this()
        {
            Id = id;
        }
    }
}
