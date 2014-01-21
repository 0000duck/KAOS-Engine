using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AWGL
{
    class AWBufferManager
    {
        internal void SetupBuffer(
            out int handle, Vector3[] data, 
            BufferTarget bufferTarget, BufferUsageHint bufferUsageHint)
        {
            GL.GenBuffers(1, out handle);
            GL.BindBuffer(bufferTarget, handle);
            GL.BufferData<Vector3>(
                bufferTarget, new IntPtr(data.Length * Vector3.SizeInBytes),
                data, bufferUsageHint
                );
        }

        internal void SetupBuffer(
            out int handle, int[] data, 
            BufferTarget bufferTarget, BufferUsageHint bufferUsageHint)
        {
            GL.GenBuffers(1, out handle);
            GL.BindBuffer(bufferTarget, handle);
            GL.BufferData(
                bufferTarget, new IntPtr(sizeof(uint) * data.Length),
                data, bufferUsageHint
                );
        }

        internal void SetupVaoBuffer(BufferTarget bufferTarget, int positionVboHandle, int ProgramHandle, string attributeName, VertexAttribPointerType vertexAttribPointerType, int index, int size)
        {
            GL.EnableVertexAttribArray(index);
            GL.BindBuffer(bufferTarget, positionVboHandle);
            GL.VertexAttribPointer(
                index, size, vertexAttribPointerType, 
                true, Vector3.SizeInBytes, 0);
            GL.BindAttribLocation(ProgramHandle, 0, attributeName);
        }

        internal void GenerateVaoBuffer(out int handle)
        {
            GL.GenVertexArrays(1, out handle);
            GL.BindVertexArray(handle);
        }
    }
}
