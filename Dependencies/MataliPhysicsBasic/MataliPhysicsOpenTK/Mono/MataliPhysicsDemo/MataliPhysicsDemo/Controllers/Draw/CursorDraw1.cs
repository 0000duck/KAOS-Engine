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
    public class CursorDraw1
    {
        Demo demo;
        PhysicsScene scene;

        RenderPC render;

        Matrix4 projection;
        Matrix4 view;
        Matrix4 world;

        Vector3 localLightDirection;
        Vector3 lightDirection;

        Vector3 startPosition;
        Vector3 endPosition;

        Matrix4 matrixIdentity;

        public CursorDraw1(Demo demo)
        {
            this.demo = demo;

            matrixIdentity = Matrix4.Identity;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;

            render = new RenderPC();
        }

        public void SetControllers()
        {
            PhysicsObject objectBase = null;

            objectBase = scene.Factory.PhysicsObjectManager.Find("Cursor A");
            if (objectBase != null)
                objectBase.UserControllers.DrawMethods += new DrawMethod(Draw);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Cursor B");
            if (objectBase != null)
                objectBase.UserControllers.DrawMethods += new DrawMethod(Draw);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Cursor");
            if (objectBase != null)
                objectBase.UserControllers.DrawMethods += new DrawMethod(DrawLine);
        }

        public void Draw(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.EnableDrawing) return;

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if (physicsObjectWithActiveCamera == null) return;

            PhysicsCamera activeCamera = physicsObjectWithActiveCamera.Camera;

            if (activeCamera == null) return;

            float time = args.Time;

            activeCamera.GetTransposeRotation(ref world);
            Vector3.TransformVector(ref demo.CursorLightDirection, ref world, out localLightDirection);

            activeCamera.View.GetViewMatrix(ref view);
            activeCamera.Projection.GetProjectionMatrix(ref projection);
            objectBase.MainWorldTransform.GetTransformMatrix(ref world);

            if (objectBase.UserDataStr != null)
            {
                GL.Disable(EnableCap.DepthTest);
                GL.DepthMask(false);

                DemoMesh mesh = demo.Meshes[objectBase.UserDataStr];
                PhysicsLight sceneLight = scene.Light;

                sceneLight.GetDirection(ref lightDirection);
                sceneLight.SetDirection(ref localLightDirection);

                mesh.Draw(ref world, ref view, ref projection, sceneLight, objectBase.Material, activeCamera, false, demo.EnableWireframe);

                sceneLight.SetDirection(ref lightDirection);

                GL.DepthMask(true);
                GL.Enable(EnableCap.DepthTest);
            }
        }

        public void DrawLine(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.EnableDrawing) return;

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if (physicsObjectWithActiveCamera == null) return;

            PhysicsCamera activeCamera = physicsObjectWithActiveCamera.Camera;

            if (activeCamera == null) return;

            GL.Disable(EnableCap.DepthTest);
            GL.DepthMask(false);

            float time = args.Time;

            CursorController cursorController = physicsObjectWithActiveCamera.InternalControllers.CursorController;

            if ((cursorController != null) && cursorController.IsDragging)
            {
                activeCamera.View.GetViewMatrix(ref view);
                activeCamera.Projection.GetProjectionMatrix(ref projection);
                world = matrixIdentity;

                cursorController.GetAnchor1(ref startPosition);
                cursorController.GetAnchor2(ref endPosition);

                render.SetWorld(ref world);
                render.SetView(ref view);
                render.SetProjection(ref projection);

                render.Apply();

                GL.Begin(PrimitiveType.Lines);

                GL.Color3(0.0f, 1.0f, 0.6f);
                GL.Vertex3(startPosition);
                GL.Color3(1.0f, 1.0f, 1.0f);
                GL.Vertex3(endPosition);

                GL.End();
            }

            GL.DepthMask(true);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
