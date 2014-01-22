using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AWGL
{
    public sealed class AWBufferManager
    {
        private static volatile AWBufferManager instance = new AWBufferManager();
        private static object syncRoot = new Object();

        private AWBufferManager() { }

        public static AWBufferManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new AWBufferManager();
                    }
                }

                return instance;
            }
        }

        internal static int SetupBuffer(
            Vector3[] data, BufferTarget bufferTarget, BufferUsageHint bufferUsageHint)
        {
            int handle;
            GL.GenBuffers(1, out handle);
            GL.BindBuffer(bufferTarget, handle);
            GL.BufferData<Vector3>(
                bufferTarget, new IntPtr(data.Length * Vector3.SizeInBytes),
                data, bufferUsageHint
                );
            return handle;
        }

        internal static int SetupBuffer(
            int[] data, BufferTarget bufferTarget, BufferUsageHint bufferUsageHint)
        {
            int handle;
            GL.GenBuffers(1, out handle);
            GL.BindBuffer(bufferTarget, handle);
            GL.BufferData(
                bufferTarget, new IntPtr(sizeof(uint) * data.Length),
                data, bufferUsageHint
                );
            return handle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bufferHandle"></param>
        /// <param name="ProgramHandle"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="attributeName"></param>
        /// <param name="bufferTarget"></param>
        /// <param name="vertexAttribPointerType"></param>
        internal static void SetupVaoBuffer(
            int bufferHandle, int ProgramHandle, int index, int size, string attributeName, 
            BufferTarget bufferTarget, VertexAttribPointerType vertexAttribPointerType)
        {
            GL.EnableVertexAttribArray(index);
            GL.BindBuffer(bufferTarget, bufferHandle);
            GL.VertexAttribPointer(
                index, size, vertexAttribPointerType, 
                true, Vector3.SizeInBytes, 0);
            GL.BindAttribLocation(ProgramHandle, 0, attributeName);
        }

        internal static int GenerateVaoBuffer()
        {
            int handle;
            GL.GenVertexArrays(1, out handle);
            GL.BindVertexArray(handle);
            return handle;
        }
    }
}
