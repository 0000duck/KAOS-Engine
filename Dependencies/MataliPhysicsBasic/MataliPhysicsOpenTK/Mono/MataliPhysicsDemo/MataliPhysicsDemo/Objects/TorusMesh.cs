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
    public class TorusMesh
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public TorusMesh(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public static void CreateShapes(Demo demo, PhysicsScene scene)
        {
            TriangleMesh triangleMesh = null;
            ShapePrimitive shapePrimitive = null;
            Shape shape = null;

            triangleMesh = scene.Factory.TriangleMeshManager.Create("TorusMesh2");
            triangleMesh.CreateTorusY(10, 15, 5.0f, 1.5f);
            if (!demo.Meshes.ContainsKey("TorusMesh2"))
                demo.Meshes.Add("TorusMesh2", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            int triangleCount = triangleMesh.GetTriangleCount();
            float[] frictions = new float[triangleCount];
            float[] restitutions = new float[triangleCount];

            for (int i = 0; i < triangleCount; i++)
            {
                frictions[i] = 1.0f;
                restitutions[i] = 0.0f;
            }

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("TorusMesh");
            shapePrimitive.CreateTriangleMesh(triangleMesh, false, 2, frictions, restitutions, 1.0f, 0.0f);

            shape = scene.Factory.ShapeManager.Create("TorusMesh");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
        }

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, float density)
        {
            PhysicsObject objectBase = null;

            Shape torusShape = scene.Factory.ShapeManager.Find("TorusMesh");

            objectBase = scene.Factory.PhysicsObjectManager.Create("TorusMesh" + instanceIndexName);
            objectBase.Shape = torusShape;
            objectBase.UserDataStr = "TorusMesh2";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(ref objectPosition);
            objectBase.InitLocalTransform.SetScale(ref objectScale);
            objectBase.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectBase.Integral.SetDensity(density);
            objectBase.InternalControllers.CreateTriangleMeshController(true);

            scene.UpdateFromInitLocalTransform(objectBase);
        }
    }
}
