/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Lake
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        int cellCountX;
        int cellCountZ;

        int partCountX;
        int partCountZ;

        public Lake(Demo demo, int instanceIndex, int partCountX, int partCountZ)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();
            this.partCountX = partCountX;
            this.partCountZ = partCountZ;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void CreateShapes(Demo demo, PhysicsScene scene, int cellCountX, int cellCountZ, float depth, float density, float linearDamping, float angularDamping, float surfaceDamping, float surfaceVelocityFactor, float surfaceAmplitudeFactor, float minSurfaceAmplitude, float maxSurfaceAmplitude, float margin, bool dynamic)
        {
            string partIndexName;
            float halfCellCountX, halfCellCountZ;
            ShapePrimitive shapePrimitive;
            Shape shape;

            this.cellCountX = cellCountX;
            this.cellCountZ = cellCountZ;

            halfCellCountX = 0.5f * cellCountX;
            halfCellCountZ = 0.5f * cellCountZ;

            float[] positionsX = { -halfCellCountX, -halfCellCountX, halfCellCountX, halfCellCountX };
            float[] positionsY = { -halfCellCountZ, halfCellCountZ, halfCellCountZ, -halfCellCountZ };

            TriangleMesh mesh = scene.Factory.TriangleMeshManager.Create("Lake" + instanceIndexName);
            mesh.CreateWall(Vector3.Zero, Vector3.UnitY, positionsX, positionsY);

            if (!demo.Meshes.ContainsKey("Lake" + instanceIndexName))
                demo.Meshes.Add("Lake" + instanceIndexName, new DemoMesh(demo, mesh, null, Vector2.One, true, true, false, false, true, CullFaceMode.FrontAndBack, false, false));

            for (int i = 0; i < partCountX; i++)
                for (int j = 0; j < partCountZ; j++)
                {
                    partIndexName = " " + i.ToString() + " " + j.ToString();

                    shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Lake" + instanceIndexName + partIndexName);
                    shapePrimitive.CreateFluid(cellCountX, cellCountZ, depth, density, linearDamping, angularDamping, surfaceDamping, surfaceVelocityFactor, surfaceAmplitudeFactor, minSurfaceAmplitude, maxSurfaceAmplitude, dynamic);

                    shape = scene.Factory.ShapeManager.Create("Lake" + instanceIndexName + partIndexName);
                    shape.Set(shapePrimitive, Matrix4.Identity, margin);
                    shape.CreateMesh(0.0f);

                    if (!demo.Meshes.ContainsKey("Lake" + instanceIndexName + partIndexName))
                        demo.Meshes.Add("Lake" + instanceIndexName + partIndexName, new DemoMesh(demo, shape, null, Vector2.One, true, true, false, false, true, CullFaceMode.FrontAndBack, dynamic, false));
                }
        }

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, float density)
        {
            string partIndexName;
            float totalHalfScaleX, totalHalfScaleZ;
            Vector3 minPosition, position, axisX, axisZ, deltaX, deltaZ;
            Matrix4 rotation;
            Shape shape;
            PhysicsObject objectRoot;
            minPosition = objectPosition;
            rotation = Matrix4.Transpose(Matrix4.CreateFromQuaternion(objectOrientation));

            axisX.X = rotation.Row0.X;
            axisX.Y = rotation.Row0.Y;
            axisX.Z = rotation.Row0.Z;

            axisZ.X = rotation.Row2.X;
            axisZ.Y = rotation.Row2.Y;
            axisZ.Z = rotation.Row2.Z;

            totalHalfScaleX = 0.5f * (partCountX - 1) * cellCountX * objectScale.X;
            totalHalfScaleZ = 0.5f * (partCountZ - 1) * cellCountZ * objectScale.Z;

            Vector3.Multiply(ref axisX, -totalHalfScaleX, out deltaX);
            Vector3.Multiply(ref axisZ, -totalHalfScaleZ, out deltaZ);

            Vector3.Add(ref minPosition, ref deltaX, out minPosition);
            Vector3.Add(ref minPosition, ref deltaZ, out minPosition);

            for (int i = 0; i < partCountX; i++)
            {
                Vector3.Multiply(ref axisX, objectScale.X * cellCountX * i, out deltaX);

                for (int j = 0; j < partCountZ; j++)
                {
                    partIndexName = " " + i.ToString() + " " + j.ToString();

                    Vector3.Multiply(ref axisZ, objectScale.Z * cellCountZ * j, out deltaZ);

                    Vector3.Add(ref minPosition, ref deltaX, out position);
                    Vector3.Add(ref position, ref deltaZ, out position);

                    shape = scene.Factory.ShapeManager.Find("Lake" + instanceIndexName + partIndexName);

                    objectRoot = scene.Factory.PhysicsObjectManager.Create("Lake" + instanceIndexName + partIndexName);
                    objectRoot.EnableCursorInteraction = false;
                    objectRoot.DrawPriority = 4;
                    objectRoot.Material.SetAmbient(0.55f, 0.55f, 0.55f);
                    objectRoot.Material.SetSpecular(0.25f, 0.25f, 0.55f);
                    objectRoot.Material.SetDiffuse(0.0f, 0.45f, 0.15f);
                    objectRoot.Material.SetEmission(0.0f, 0.2f, 0.5f);
                    objectRoot.Material.TransparencyFactor = 0.4f;
                    objectRoot.Material.TwoSidedNormals = true;
                    objectRoot.Shape = shape;
                    objectRoot.UserDataStr = "Lake" + instanceIndexName + partIndexName;
                    objectRoot.InitLocalTransform.SetPosition(ref position);
                    objectRoot.InitLocalTransform.SetScale(ref objectScale);
                    objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
                    objectRoot.Integral.SetDensity(density);
                    objectRoot.EnableLocalGravity = true;
                    objectRoot.InternalControllers.CreateFluidController(true);

                    scene.UpdateFromInitLocalTransform(objectRoot);
                }
            }
        }
    }
}
