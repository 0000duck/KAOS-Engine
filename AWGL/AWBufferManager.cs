using AWGL.Shapes;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGL
{
    /// <summary>
    /// 
    /// </summary>
    class AWBufferManager : IDisposable
    {
        #region
        // To create a VBO:
        // 1) Generate the buffer handles for the vertex and element buffers.
        // 2) Bind the vertex buffer handle and upload your vertex data. 
        //    Check that the buffer was uploaded correctly.
        // 3) Bind the element buffer handle and upload your element data. 
        //    Check that the buffer was uploaded correctly.
        #endregion

        private Vbo vboHandle;

        private Vbo GenerateVBO<TVertex>(TVertex[] vertices,
                                           short[] elements,
                                           int elementSize,
                                           int typeSize,
                                           BufferUsageHint bufferUsageTypeGL)
            where TVertex : struct
        {
            // Determine size of Buffer
            int vbo_Size = vertices.Length * BlittableValueType.StrideOf(vertices);
            int ebo_Size = elements.Length * sizeof(short);

            //Generate Buffer ID
            GL.GenBuffers(1, out vboHandle.VboID);

            // Binds the buffer that is used next
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandle.VboID);

            // Copy data to the VBO on the GPU.
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)vbo_Size, vertices, bufferUsageTypeGL);

            CheckForErrors(vbo_Size);

            //Generate Buffer ID
            GL.GenBuffers(1, out vboHandle.EboID);

            // Binds the buffer that is used next
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboHandle.EboID);

            // Copy data to the VBO on the GPU.
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)ebo_Size, elements, bufferUsageTypeGL);

            CheckForErrors(ebo_Size);

            return this.vboHandle;
        }

        private static void CheckForErrors(int size)
        {
            int getBufferSize;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out getBufferSize);
            if (getBufferSize != size)
                throw new Exception("Data not uploaded correctly");
        }

        public Vbo getBufferObjects(DrawableShape shape)
        {
            return new Vbo();//GenerateVBO(
        }


        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
