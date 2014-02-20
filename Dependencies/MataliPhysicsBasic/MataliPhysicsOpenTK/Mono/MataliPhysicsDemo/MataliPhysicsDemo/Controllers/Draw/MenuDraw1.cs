/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Komires.MataliPhysics;
using Komires.MataliRender;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MenuDraw1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        PhysicsObject infoScreen;
        PhysicsObject infoDescription;

        Color whiteColor;

        Vector3 position;

        Matrix4 world;
        Matrix4 view;
        Matrix4 projection;

        public MenuDraw1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            whiteColor = Color.White;

            world = Matrix4.Identity;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            infoScreen = scene.Factory.PhysicsObjectManager.Find("Info Screen" + instanceIndexName);
            infoDescription = scene.Factory.PhysicsObjectManager.Find("Info Description" + instanceIndexName);

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Info" + instanceIndexName);

            if (objectBase != null)
                objectBase.UserControllers.DrawMethods += new DrawMethod(Draw);
        }

        void Draw(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            PhysicsObject physicsObjectWithCamera = scene.GetPhysicsObjectWithCamera(0);

            if (physicsObjectWithCamera == null) return;

            PhysicsCamera activeCamera = physicsObjectWithCamera.Camera;

            if (activeCamera == null) return;

            float time = args.Time;

            if ((infoScreen == null) || (infoDescription == null)) return;
            if (!infoDescription.EnableDrawing) return;

            string sceneScreenName = infoScreen.Material.UserDataStr;

            if (sceneScreenName == null) return;
            if (!demo.Descriptions.ContainsKey(sceneScreenName)) return;

            List<string> Descriptions = demo.Descriptions[sceneScreenName];

            string info = null;

            infoDescription.MainWorldTransform.GetPosition(ref position);
            activeCamera.View.GetViewMatrix(ref view);
            activeCamera.Projection.GetProjectionMatrix(ref projection);

            RenderPCT render = demo.DemoFont3D.Render;

            render.SetWorld(ref world);
            render.SetView(ref view);
            render.SetProjection(ref projection);

            GL.CullFace(CullFaceMode.Back);

            demo.DemoFont3D.Begin();

            for (int i = 0; i < Descriptions.Count; i++)
            {
                info = Descriptions[i];

                if (info != null)
                    demo.DemoFont3D.Draw(position.X - 25.0f, position.Y + 12.0f - 1.4f * i, position.Z - 0.5f, 0.08125f, 0.12125f, info, whiteColor);
            }

            demo.DemoFont3D.End();
        }
    }
}
