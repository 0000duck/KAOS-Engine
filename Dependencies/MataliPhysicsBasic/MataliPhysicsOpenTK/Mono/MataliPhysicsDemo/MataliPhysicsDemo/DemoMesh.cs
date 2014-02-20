/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Komires.MataliPhysics;
using Komires.MataliRender;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoMesh
    {
        Demo demo;
        int meshVertexBuffer;
        int meshIndexBuffer;
        DemoTexture meshTexture;
        CullFaceMode meshCullMode;
        int vertexCount;
        int indexCount;
        bool dynamic;
        bool scaleNormals;

        VertexPositionNormalTexture[] meshVertices;
        ushort[] meshIndices16Bit;
        int[] meshIndices32Bit;
        DrawElementsType meshIndicesType;

        public int VertexBuffer { get { return meshVertexBuffer; } }
        public int IndexBuffer { get { return meshIndexBuffer; } }
        public DemoTexture DemoTexture { get { return meshTexture; } }
        public CullFaceMode CullMode { get { return meshCullMode; } }

        public int VertexCount { get { return vertexCount; } }
        public int IndexCount { get { return indexCount; } }
        public bool Dynamic { get { return dynamic; } }
        public bool ScaleNormals { get { return scaleNormals; } set { scaleNormals = value; } }

        public VertexPositionNormalTexture[] Vertices { get { return meshVertices; } }
        public ushort[] Indices16Bit { get { return meshIndices16Bit; } }
        public int[] Indices32Bit { get { return meshIndices32Bit; } }

        public RenderMeshDeferredPNT Render;

        Vector3 ambient;
        Vector3 diffuse;
        Vector3 emission;
        Vector3 specular;

        Vector3 lightDirection;
        Vector3 lightDiffuse;
        Vector3 lightSpecular;

        public DemoMesh(Demo demo)
        {
            this.demo = demo;

            Render = new RenderMeshDeferredPNT();

            meshVertexBuffer = -1;
            meshIndexBuffer = -1;
            meshVertices = null;
            meshIndices16Bit = null;
            meshIndices32Bit = null;
            meshIndicesType = 0;
            meshCullMode = CullFaceMode.Back;
            dynamic = false;
            scaleNormals = false;

            SetTexture(null);
        }

        public DemoMesh(Demo demo, Shape shape, DemoTexture texture, Vector2 textureScale, bool indices, bool indices16Bit, bool flipTriangles, bool flipNormals, bool smoothNormals, CullFaceMode cullMode, bool dynamic, bool scaleNormals)
        {
            this.demo = demo;

            Render = new RenderMeshDeferredPNT();

            meshVertexBuffer = -1;
            meshIndexBuffer = -1;
            meshVertices = null;
            meshIndices16Bit = null;
            meshIndices32Bit = null;
            meshIndicesType = 0;
            this.dynamic = dynamic;
            this.scaleNormals = scaleNormals;

            SetCullMode(cullMode);
            SetTexture(texture);

            if (indices)
            {
                meshVertices = new VertexPositionNormalTexture[shape.VertexCount];
                shape.GetMeshVertices(textureScale.X, textureScale.Y, flipNormals, smoothNormals, meshVertices);
                CreateVertexBuffer(meshVertices, dynamic);

                if (indices16Bit)
                {
                    meshIndices16Bit = new ushort[shape.IndexCount];
                    shape.GetMesh16BitIndices(flipTriangles, meshIndices16Bit);
                    CreateIndexBuffer(meshIndices16Bit, false);
                }
                else
                {
                    meshIndices32Bit = new int[shape.IndexCount];
                    shape.GetMesh32BitIndices(flipTriangles, meshIndices32Bit);
                    CreateIndexBuffer(meshIndices32Bit, false);
                }
            }
            else
            {
                meshVertices = new VertexPositionNormalTexture[shape.TriangleVertexCount];
                shape.GetMesh(textureScale.X, textureScale.Y, flipTriangles, flipNormals, smoothNormals, meshVertices);
                CreateVertexBuffer(meshVertices, dynamic);
            }
        }

        public DemoMesh(Demo demo, TriangleMesh triangleMesh, DemoTexture texture, Vector2 textureScale, bool indices, bool indices16Bit, bool flipTriangles, bool flipNormals, bool smoothNormals, CullFaceMode cullMode, bool dynamic, bool scaleNormals)
        {
            this.demo = demo;

            Render = new RenderMeshDeferredPNT();

            meshVertexBuffer = -1;
            meshIndexBuffer = -1;
            meshVertices = null;
            meshIndices16Bit = null;
            meshIndices32Bit = null;
            meshIndicesType = 0;
            this.dynamic = dynamic;
            this.scaleNormals = scaleNormals;

            SetCullMode(cullMode);
            SetTexture(texture);

            if (indices)
            {
                triangleMesh.GetMeshVertices(flipNormals, smoothNormals, out meshVertices);
                CreateVertexBuffer(meshVertices, dynamic);

                if (indices16Bit)
                {
                    triangleMesh.GetMesh16BitIndices(flipTriangles, out meshIndices16Bit);
                    CreateIndexBuffer(meshIndices16Bit, false);
                }
                else
                {
                    triangleMesh.GetMesh32BitIndices(flipTriangles, out meshIndices32Bit);
                    CreateIndexBuffer(meshIndices32Bit, false);
                }
            }
            else
            {
                triangleMesh.GetMesh(textureScale, flipTriangles, flipNormals, smoothNormals, out meshVertices);
                CreateVertexBuffer(meshVertices, dynamic);
            }
        }

        public void CreateVertexBuffer(VertexPositionNormalTexture[] vertices, bool dynamic)
        {
            if (vertices.Length == 0) return;

            if (demo.EnableVertexBuffer)
            {
                GL.GenBuffers(1, out meshVertexBuffer);
                GL.BindBuffer(BufferTarget.ArrayBuffer, meshVertexBuffer);

                if (dynamic)
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexPositionNormalTexture.SizeInBytes * vertices.Length), vertices, BufferUsageHint.DynamicDraw);
                else
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexPositionNormalTexture.SizeInBytes * vertices.Length), vertices, BufferUsageHint.StaticDraw);
            }

            meshVertices = vertices;
            vertexCount = vertices.Length;
        }

        public void CreateIndexBuffer(ushort[] indices, bool dynamic)
        {
            if (indices.Length == 0) return;

            if (demo.EnableVertexBuffer)
            {
                GL.GenBuffers(1, out meshIndexBuffer);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshIndexBuffer);

                if (dynamic)
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(ushort) * indices.Length), indices, BufferUsageHint.DynamicDraw);
                else
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(ushort) * indices.Length), indices, BufferUsageHint.StaticDraw);
            }

            meshIndices16Bit = indices;
            indexCount = indices.Length;
            meshIndicesType = DrawElementsType.UnsignedShort;
        }

        public void CreateIndexBuffer(int[] indices, bool dynamic)
        {
            if (indices.Length == 0) return;

            if (demo.EnableVertexBuffer)
            {
                GL.GenBuffers(1, out meshIndexBuffer);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshIndexBuffer);

                if (dynamic)
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(int) * indices.Length), indices, BufferUsageHint.DynamicDraw);
                else
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(int) * indices.Length), indices, BufferUsageHint.StaticDraw);
            }

            meshIndices32Bit = indices;
            indexCount = indices.Length;
            meshIndicesType = DrawElementsType.UnsignedInt;
        }

        public void SetVertices(VertexPositionNormalTexture[] vertices)
        {
            if (demo.EnableVertexBuffer)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, meshVertexBuffer);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexPositionNormalTexture.SizeInBytes * vertices.Length), IntPtr.Zero, BufferUsageHint.DynamicDraw);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexPositionNormalTexture.SizeInBytes * vertices.Length), vertices, BufferUsageHint.DynamicDraw);
            }

            meshVertices = vertices;
            vertexCount = vertices.Length;
        }

        public void SetIndices(ushort[] indices)
        {
            if (demo.EnableVertexBuffer)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshIndexBuffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(ushort)), IntPtr.Zero, BufferUsageHint.DynamicDraw);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(ushort)), indices, BufferUsageHint.DynamicDraw);
            }

            meshIndices16Bit = indices;
            indexCount = indices.Length;
        }

        public void SetIndices(int[] indices)
        {
            if (demo.EnableVertexBuffer)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshIndexBuffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(int)), IntPtr.Zero, BufferUsageHint.DynamicDraw);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(int)), indices, BufferUsageHint.DynamicDraw);
            }

            meshIndices32Bit = indices;
            indexCount = indices.Length;
        }

        public void SetTexture(DemoTexture texture)
        {
            meshTexture = texture;
        }

        public void SetCullMode(CullFaceMode cullMode)
        {
            meshCullMode = cullMode;
        }

        public void Draw(IRender render)
        {
            render.Apply();

            if (demo.EnableVertexBuffer)
            {
                if (meshVertexBuffer != -1)
                {
                    if (meshIndexBuffer != -1)
                    {
                        GL.EnableClientState(ArrayCap.VertexArray);
                        GL.EnableClientState(ArrayCap.NormalArray);
                        GL.EnableClientState(ArrayCap.TextureCoordArray);

                        GL.BindBuffer(BufferTarget.ArrayBuffer, meshVertexBuffer);
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshIndexBuffer);

                        GL.VertexPointer(3, VertexPointerType.Float, VertexPositionNormalTexture.SizeInBytes, IntPtr.Zero);
                        GL.NormalPointer(NormalPointerType.Float, VertexPositionNormalTexture.SizeInBytes, (IntPtr)Vector3.SizeInBytes);
                        GL.TexCoordPointer(2, TexCoordPointerType.Float, VertexPositionNormalTexture.SizeInBytes, (IntPtr)(Vector3.SizeInBytes + Vector3.SizeInBytes));

                        GL.DrawElements(PrimitiveType.Triangles, indexCount, meshIndicesType, 0);

                        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                        GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
                        GL.NormalPointer(NormalPointerType.Float, 0, IntPtr.Zero);
                        GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);

                        GL.DisableClientState(ArrayCap.VertexArray);
                        GL.DisableClientState(ArrayCap.NormalArray);
                        GL.DisableClientState(ArrayCap.TextureCoordArray);
                    }
                    else
                    {
                        GL.EnableClientState(ArrayCap.VertexArray);
                        GL.EnableClientState(ArrayCap.NormalArray);
                        GL.EnableClientState(ArrayCap.TextureCoordArray);

                        GL.BindBuffer(BufferTarget.ArrayBuffer, meshVertexBuffer);

                        GL.VertexPointer(3, VertexPointerType.Float, VertexPositionNormalTexture.SizeInBytes, IntPtr.Zero);
                        GL.NormalPointer(NormalPointerType.Float, VertexPositionNormalTexture.SizeInBytes, (IntPtr)Vector3.SizeInBytes);
                        GL.TexCoordPointer(2, TexCoordPointerType.Float, VertexPositionNormalTexture.SizeInBytes, (IntPtr)(Vector3.SizeInBytes + Vector3.SizeInBytes));

                        GL.DrawArrays(PrimitiveType.Triangles, 0, vertexCount);

                        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                        GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
                        GL.NormalPointer(NormalPointerType.Float, 0, IntPtr.Zero);
                        GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);

                        GL.DisableClientState(ArrayCap.VertexArray);
                        GL.DisableClientState(ArrayCap.NormalArray);
                        GL.DisableClientState(ArrayCap.TextureCoordArray);
                    }
                }
            }
            else
            {
                VertexPositionNormalTexture v1, v2, v3;

                if (meshIndices16Bit != null)
                {
                    GL.Begin(PrimitiveType.Triangles);

                    for (int i = 0; i < indexCount; i += 3)
                    {
                        v1 = meshVertices[meshIndices16Bit[i]];
                        v2 = meshVertices[meshIndices16Bit[i + 1]];
                        v3 = meshVertices[meshIndices16Bit[i + 2]];

                        GL.TexCoord2(v1.TextureCoordinate);
                        GL.Normal3(v1.Normal);
                        GL.Vertex3(v1.Position);

                        GL.TexCoord2(v2.TextureCoordinate);
                        GL.Normal3(v2.Normal);
                        GL.Vertex3(v2.Position);

                        GL.TexCoord2(v3.TextureCoordinate);
                        GL.Normal3(v3.Normal);
                        GL.Vertex3(v3.Position);
                    }

                    GL.End();
                }
                else
                    if (meshIndices32Bit != null)
                    {
                        GL.Begin(PrimitiveType.Triangles);

                        for (int i = 0; i < indexCount; i += 3)
                        {
                            v1 = meshVertices[meshIndices32Bit[i]];
                            v2 = meshVertices[meshIndices32Bit[i + 1]];
                            v3 = meshVertices[meshIndices32Bit[i + 2]];

                            GL.TexCoord2(v1.TextureCoordinate);
                            GL.Normal3(v1.Normal);
                            GL.Vertex3(v1.Position);

                            GL.TexCoord2(v2.TextureCoordinate);
                            GL.Normal3(v2.Normal);
                            GL.Vertex3(v2.Position);

                            GL.TexCoord2(v3.TextureCoordinate);
                            GL.Normal3(v3.Normal);
                            GL.Vertex3(v3.Position);
                        }

                        GL.End();
                    }
                    else
                    {
                        GL.Begin(PrimitiveType.Triangles);

                        for (int i = 0; i < vertexCount; i += 3)
                        {
                            v1 = meshVertices[i];
                            v2 = meshVertices[i + 1];
                            v3 = meshVertices[i + 2];

                            GL.TexCoord2(v1.TextureCoordinate);
                            GL.Normal3(v1.Normal);
                            GL.Vertex3(v1.Position);

                            GL.TexCoord2(v2.TextureCoordinate);
                            GL.Normal3(v2.Normal);
                            GL.Vertex3(v2.Position);

                            GL.TexCoord2(v3.TextureCoordinate);
                            GL.Normal3(v3.Normal);
                            GL.Vertex3(v3.Position);
                        }

                        GL.End();
                    }
            }
        }

        public void Draw(ref Matrix4 world, ref Matrix4 view, ref Matrix4 projection, PhysicsLight light, PhysicsMaterial material, PhysicsCamera camera, bool sleep, bool wireframe)
        {
            Render.SetWorld(ref world);
            Render.SetView(ref view);
            Render.SetProjection(ref projection);
            Render.EnableScaleNormals = scaleNormals;

            light.GetDirection(ref lightDirection);
            light.GetDiffuse(ref lightDiffuse);
            light.GetSpecular(ref lightSpecular);

            if (meshCullMode == CullFaceMode.FrontAndBack)
                GL.Disable(EnableCap.CullFace);

            GL.CullFace(meshCullMode);

            DemoTexture currentTexture = meshTexture;
            if (material.UserDataStr != null)
                currentTexture = demo.Textures[material.UserDataStr];

            material.GetAmbient(ref ambient);
            material.GetDiffuse(ref diffuse);
            material.GetEmission(ref emission);
            material.GetSpecular(ref specular);

            if (sleep)
            {
                diffuse.X = 0.7f;
                diffuse.Y = 1.0f;
                diffuse.Z = 1.0f;
            }

            Render.SetAmbient(ref ambient);
            Render.SetSpecular(ref specular);
            Render.SetEmission(ref emission);
            Render.SpecularPower = material.SpecularPower;
            Render.Alpha = material.TransparencyFactor;
            Render.EnableTwoSidedNormals = material.TwoSidedNormals;
            
            if (!wireframe)
            {
                Render.SetDiffuse(ref diffuse);

                if (currentTexture != null)
                {
                    Render.EnableTexture = true;
                    Render.Texture = currentTexture.Handle;
                }
                else
                {
                    Render.EnableTexture = false;
                }
            }
            else
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

                diffuse.X *= 4.0f;
                diffuse.Y *= 4.0f;
                diffuse.Z *= 4.0f;

                Render.SetDiffuse(ref diffuse);

                Render.EnableTexture = false;
            }

            Render.Apply();

            if (demo.EnableVertexBuffer)
            {
                if (meshVertexBuffer != -1)
                {
                    if (meshIndexBuffer != -1)
                    {
                        GL.EnableClientState(ArrayCap.VertexArray);
                        GL.EnableClientState(ArrayCap.NormalArray);
                        GL.EnableClientState(ArrayCap.TextureCoordArray);

                        GL.BindBuffer(BufferTarget.ArrayBuffer, meshVertexBuffer);
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshIndexBuffer);

                        GL.VertexPointer(3, VertexPointerType.Float, VertexPositionNormalTexture.SizeInBytes, IntPtr.Zero);
                        GL.NormalPointer(NormalPointerType.Float, VertexPositionNormalTexture.SizeInBytes, (IntPtr)Vector3.SizeInBytes);
                        GL.TexCoordPointer(2, TexCoordPointerType.Float, VertexPositionNormalTexture.SizeInBytes, (IntPtr)(Vector3.SizeInBytes + Vector3.SizeInBytes));

                        GL.DrawElements(PrimitiveType.Triangles, indexCount, meshIndicesType, 0);

                        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                        GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
                        GL.NormalPointer(NormalPointerType.Float, 0, IntPtr.Zero);
                        GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);

                        GL.DisableClientState(ArrayCap.VertexArray);
                        GL.DisableClientState(ArrayCap.NormalArray);
                        GL.DisableClientState(ArrayCap.TextureCoordArray);
                    }
                    else
                    {
                        GL.EnableClientState(ArrayCap.VertexArray);
                        GL.EnableClientState(ArrayCap.NormalArray);
                        GL.EnableClientState(ArrayCap.TextureCoordArray);

                        GL.BindBuffer(BufferTarget.ArrayBuffer, meshVertexBuffer);

                        GL.VertexPointer(3, VertexPointerType.Float, VertexPositionNormalTexture.SizeInBytes, IntPtr.Zero);
                        GL.NormalPointer(NormalPointerType.Float, VertexPositionNormalTexture.SizeInBytes, (IntPtr)Vector3.SizeInBytes);
                        GL.TexCoordPointer(2, TexCoordPointerType.Float, VertexPositionNormalTexture.SizeInBytes, (IntPtr)(Vector3.SizeInBytes + Vector3.SizeInBytes));

                        GL.DrawArrays(PrimitiveType.Triangles, 0, vertexCount);

                        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                        GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
                        GL.NormalPointer(NormalPointerType.Float, 0, IntPtr.Zero);
                        GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);

                        GL.DisableClientState(ArrayCap.VertexArray);
                        GL.DisableClientState(ArrayCap.NormalArray);
                        GL.DisableClientState(ArrayCap.TextureCoordArray);
                    }
                }
            }
            else
            {
                VertexPositionNormalTexture v1, v2, v3;

                if (meshIndices16Bit != null)
                {
                    GL.Begin(PrimitiveType.Triangles);

                    for (int i = 0; i < indexCount; i += 3)
                    {
                        v1 = meshVertices[meshIndices16Bit[i]];
                        v2 = meshVertices[meshIndices16Bit[i + 1]];
                        v3 = meshVertices[meshIndices16Bit[i + 2]];

                        GL.TexCoord2(v1.TextureCoordinate);
                        GL.Normal3(v1.Normal);
                        GL.Vertex3(v1.Position);

                        GL.TexCoord2(v2.TextureCoordinate);
                        GL.Normal3(v2.Normal);
                        GL.Vertex3(v2.Position);

                        GL.TexCoord2(v3.TextureCoordinate);
                        GL.Normal3(v3.Normal);
                        GL.Vertex3(v3.Position);
                    }

                    GL.End();
                }
                else
                    if (meshIndices32Bit != null)
                    {
                        GL.Begin(PrimitiveType.Triangles);

                        for (int i = 0; i < indexCount; i += 3)
                        {
                            v1 = meshVertices[meshIndices32Bit[i]];
                            v2 = meshVertices[meshIndices32Bit[i + 1]];
                            v3 = meshVertices[meshIndices32Bit[i + 2]];

                            GL.TexCoord2(v1.TextureCoordinate);
                            GL.Normal3(v1.Normal);
                            GL.Vertex3(v1.Position);

                            GL.TexCoord2(v2.TextureCoordinate);
                            GL.Normal3(v2.Normal);
                            GL.Vertex3(v2.Position);

                            GL.TexCoord2(v3.TextureCoordinate);
                            GL.Normal3(v3.Normal);
                            GL.Vertex3(v3.Position);
                        }

                        GL.End();
                    }
                    else
                    {
                        GL.Begin(PrimitiveType.Triangles);

                        for (int i = 0; i < vertexCount; i += 3)
                        {
                            v1 = meshVertices[i];
                            v2 = meshVertices[i + 1];
                            v3 = meshVertices[i + 2];

                            GL.TexCoord2(v1.TextureCoordinate);
                            GL.Normal3(v1.Normal);
                            GL.Vertex3(v1.Position);

                            GL.TexCoord2(v2.TextureCoordinate);
                            GL.Normal3(v2.Normal);
                            GL.Vertex3(v2.Position);

                            GL.TexCoord2(v3.TextureCoordinate);
                            GL.Normal3(v3.Normal);
                            GL.Vertex3(v3.Position);
                        }

                        GL.End();
                    }
            }

            if (meshCullMode == CullFaceMode.FrontAndBack)
                GL.Enable(EnableCap.CullFace);

            if (wireframe)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }
    }
}
