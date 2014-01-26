using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AWGL.Utilities
{
    public struct AWVertex
    {
        /// <summary>
        /// Defines the size of the AWVertex struct in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Marshal.SizeOf(new AWVertex());
        private Vector3[] vector31;
        private Vector3[] vector32;
        private int[] p;

        public Vector3[] Postions { get; set; }

        public Vector3[] Normals {get; set;}

        public int[] Colors {get; set;}

        public AWVertex(Vector3[] positions, Vector3[] nomarls, int[] colors) :this()
        {
            Postions = positions;
            Normals = nomarls;
            Colors = colors;
        }

            
    }
}
