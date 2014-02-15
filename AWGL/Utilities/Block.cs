using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.Utilities
{
    public class Block
    {
        enum BlockType
        {
            BlockType_Default = 0,

            BlockType_Grass,
            BlockType_Dirt,
            BlockType_Water,
            BlockType_Stone,
            BlockType_Wood,
            BlockType_Sand,

            BlockType_NumTypes,
        };

        public bool IsActive { get; set; }
        private bool m_active;
        private BlockType type;
    }
}
