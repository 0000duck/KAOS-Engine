using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWGL.Interfaces
{
    public interface IGameObject
    {
        void Update(float elapsedTime);
        void Render();
    }
}
