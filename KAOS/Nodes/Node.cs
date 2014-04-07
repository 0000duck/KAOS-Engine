using KAOS.Interfaces;
using KAOS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.Nodes
{
    public abstract class Node : ISceneNode
    {
        public abstract void Render();
    }

}
