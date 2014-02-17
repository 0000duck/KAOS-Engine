using KAOS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAOS.Interfaces
{
    public interface IGroupNode : IEnumerable<ISceneNode>
    {
        void AddChild(ISceneNode child);
        void RemoveChild(ISceneNode child);
    }
}