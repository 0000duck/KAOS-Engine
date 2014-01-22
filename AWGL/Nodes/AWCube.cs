using AWGL.Shapes;
using AWGL.Tutorial;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGL.Nodes
{
    /// <summary>
    /// Cube Node
    /// </summary>
    public class AWCube : AWNode, ISceneNode
    {
        #region Private Members
        
        private static Vector3[] CubeVertices = new Vector3[]{
            new Vector3(-1.0f, -1.0f,  1.0f),
            new Vector3( 1.0f, -1.0f,  1.0f),
            new Vector3( 1.0f,  1.0f,  1.0f),
            new Vector3(-1.0f,  1.0f,  1.0f),
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3( 1.0f, -1.0f, -1.0f), 
            new Vector3( 1.0f,  1.0f, -1.0f),
            new Vector3(-1.0f,  1.0f, -1.0f) 
        };

        private static int[] CubeElements = new int[]{
                // front face
                0, 1, 2, 2, 3, 0,
                // top face
                3, 2, 6, 6, 7, 3,
                // back face
                7, 6, 5, 5, 4, 7,
                // left face
                4, 0, 3, 3, 7, 4,
                // bottom face
                0, 1, 5, 5, 4, 0,
                // right face
                1, 5, 6, 6, 2, 1, 
        };

        #endregion

        public Vector3[] Vertices
        {
            get { return CubeVertices; }
        }

        public int[] Indices
        {
            get { return CubeElements; }
        }

        public AWCube()
        {
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

    }
}
