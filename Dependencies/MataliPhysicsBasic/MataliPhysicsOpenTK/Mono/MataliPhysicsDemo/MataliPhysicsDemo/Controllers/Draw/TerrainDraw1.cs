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
    public class TerrainDraw1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        Matrix4 world;
        Matrix4 view;
        Matrix4 projection;

        public TerrainDraw1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Terrain" + instanceIndexName);
            if (objectBase != null)
            {
                objectBase.UserControllers.EnableDraw = false;
                objectBase.UserControllers.DrawMethods += new DrawMethod(Draw);
            }
        }

        public void Draw(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if (physicsObjectWithActiveCamera == null) return;

            PhysicsCamera activeCamera = physicsObjectWithActiveCamera.Camera;

            if (activeCamera == null) return;

            DemoMesh mesh = demo.Meshes[objectBase.UserDataStr];

            if ((mesh == null) || (mesh.Vertices == null)) return;

            if (mesh.Dynamic && objectBase.Shape.ShapePrimitive.DynamicUpdate)
            {
                objectBase.Shape.GetMeshVertices(1.0f, 1.0f, false, true, mesh.Vertices);
                mesh.SetVertices(mesh.Vertices);
            }

            float time = args.Time;

            objectBase.MainWorldTransform.GetTransformMatrix(ref world);
            activeCamera.View.GetViewMatrix(ref view);
            activeCamera.Projection.GetProjectionMatrix(ref projection);

            PhysicsLight sceneLight = scene.Light;

            mesh.Draw(ref world, ref view, ref projection, sceneLight, objectBase.Material, activeCamera, false, demo.EnableWireframe);
        }
    }
}
