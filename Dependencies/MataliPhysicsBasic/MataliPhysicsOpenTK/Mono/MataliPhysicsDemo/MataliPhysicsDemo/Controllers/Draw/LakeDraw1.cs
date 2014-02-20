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
    public class LakeDraw1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;
        string lakeInstanceName;

        int partCountX;
        int partCountZ;

        Matrix4 world;
        Matrix4 view;
        Matrix4 projection;

        public LakeDraw1(Demo demo, int instanceIndex, int partCountX, int partCountZ)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();
            lakeInstanceName = "Lake" + instanceIndexName;
            this.partCountX = partCountX;
            this.partCountZ = partCountZ;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            string partIndexName;
            PhysicsObject objectBase;

            for (int i = 0; i < partCountX; i++)
                for (int j = 0; j < partCountZ; j++)
                {
                    partIndexName = " " + i.ToString() + " " + j.ToString();

                    objectBase = scene.Factory.PhysicsObjectManager.Find("Lake" + instanceIndexName + partIndexName);
                    if (objectBase != null)
                    {
                        objectBase.UserControllers.EnableDraw = false;
                        objectBase.UserControllers.DrawMethods += new DrawMethod(Draw);
                    }
                }
        }

        void Draw(DrawMethodArgs args)
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

            if (objectBase.Shape.ShapePrimitive.DynamicUpdateState)
            {
                mesh.Draw(ref world, ref view, ref projection, sceneLight, objectBase.Material, activeCamera, false, demo.EnableWireframe);
            }
            else
            {
                mesh = demo.Meshes[lakeInstanceName];

                mesh.Draw(ref world, ref view, ref projection, sceneLight, objectBase.Material, activeCamera, false, demo.EnableWireframe);
            }
        }
    }
}
