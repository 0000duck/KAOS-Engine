/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Komires.MataliPhysics;
using Komires.MataliRender;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SkyDraw1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        DrawBuffersEnum[] targets;

        //RenderCubeP render;
        RenderSkyPNT render;

        Vector3 position;
        Vector3 direction;
        Vector3 diffuse;
        Vector3 specular;

        Matrix4 translation;
        Matrix4 transform;

        Matrix4 world;
        Matrix4 view;
        Matrix4 projection;

        int curTime;
        int oldTime;

        public SkyDraw1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;

            targets = new DrawBuffersEnum[4];

            //render = new RenderCubeP();
            render = new RenderSkyPNT();

            // Sun position update based on the current time
            //oldTime = curTime = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
            //scene.Light.RotationDegAngleX = curTime * 0.25f - 90.0f;

            scene.Light.RotationDegAngleX = 138.0f;
            scene.Light.RotationDegAngleZ = 10.0f;

            render.SunRotationDegAngleX = scene.Light.RotationDegAngleX;
            render.SunRotationDegAngleZ = scene.Light.RotationDegAngleZ;
            render.UpdateSunParameters();

            render.GetLightDirection(ref direction);
            scene.Light.SetDirection(ref direction);
        }

        public void SetControllers()
        {
            render.SunRotationDegAngleX = scene.Light.RotationDegAngleX;
            render.SunRotationDegAngleZ = scene.Light.RotationDegAngleZ;
            render.UpdateSunParameters();

            render.GetLightDirection(ref direction);
            scene.Light.SetDirection(ref direction);

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Sky" + instanceIndexName);
            if (objectBase != null)
            {
                objectBase.UserControllers.EnableDraw = false;
                objectBase.UserControllers.DrawMethods += new DrawMethod(DrawSky);
            }
        }

        public void DrawSky(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            DemoKeyboardState keyboardState = demo.GetKeyboardState();

            if (keyboardState[Key.J])
            {
                scene.Light.RotationDegAngleX += 0.5f;

                render.SunRotationDegAngleX = scene.Light.RotationDegAngleX;
                render.UpdateSunParameters();
            }

            if (keyboardState[Key.K])
            {
                scene.Light.RotationDegAngleX -= 0.5f;

                render.SunRotationDegAngleX = scene.Light.RotationDegAngleX;
                render.UpdateSunParameters();
            }

            curTime = DateTime.Now.Hour * 60 + DateTime.Now.Minute;

            if (curTime != oldTime)
            {
                oldTime = curTime;

                // Sun position update based on the current time
                //scene.Light.RotationDegAngleX = curTime * 0.25f - 90.0f;

                //render.SunRotationDegAngleX = scene.Light.RotationDegAngleX;
                //render.UpdateSunParameters();
            }

            render.GetLightDirection(ref direction);
            render.GetSunColor(ref diffuse);

            Vector3.Multiply(ref diffuse, 0.5f, out diffuse);
            Vector3.Multiply(ref diffuse, 0.4f, out specular);

            scene.Light.SetDirection(ref direction);
            scene.Light.SetDiffuse(ref diffuse);
            scene.Light.SetSpecular(ref specular);

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if (physicsObjectWithActiveCamera == null) return;

            PhysicsCamera activeCamera = physicsObjectWithActiveCamera.Camera;

            if (activeCamera == null) return;

            if (demo.EnableWireframe) return;

            float time = args.Time;

            activeCamera.View.GetViewMatrix(ref view);
            activeCamera.Projection.GetProjectionMatrix(ref projection);

            if (objectBase.UserDataStr != null)
            {
                DemoMesh mesh = demo.Meshes[objectBase.UserDataStr];

                targets[0] = DrawBuffersEnum.ColorAttachment0;
                targets[1] = DrawBuffersEnum.None;
                targets[2] = DrawBuffersEnum.None;
                targets[3] = DrawBuffersEnum.None;

                GL.DrawBuffers(4, targets);

                GL.CullFace(mesh.CullMode);
                GL.Disable(EnableCap.Texture2D);
                GL.DepthMask(false);

                physicsObjectWithActiveCamera.MainWorldTransform.GetPosition(ref position);
                Matrix4.CreateTranslation(ref position, out translation);
                objectBase.MainWorldTransform.GetTransformMatrix(ref transform);
                Matrix4.Mult(ref transform, ref translation, out world);
                //Matrix4.Mult(ref world, ref view, out transform);
                //Matrix4.Mult(ref transform, ref projection, out world);

                //render.SetWorldViewProjection(ref world);
                //render.Texture = mesh.DemoTexture.Handle;
                render.SetWorld(ref world);
                render.SetView(ref view);
                render.SetProjection(ref projection);

                mesh.Draw(render);

                GL.DepthMask(true);
                GL.Enable(EnableCap.Texture2D);

                targets[0] = DrawBuffersEnum.ColorAttachment0;
                targets[1] = DrawBuffersEnum.ColorAttachment1;
                targets[2] = DrawBuffersEnum.ColorAttachment2;
                targets[3] = DrawBuffersEnum.ColorAttachment3;

                GL.DrawBuffers(4, targets);
            }
        }
    }
}
