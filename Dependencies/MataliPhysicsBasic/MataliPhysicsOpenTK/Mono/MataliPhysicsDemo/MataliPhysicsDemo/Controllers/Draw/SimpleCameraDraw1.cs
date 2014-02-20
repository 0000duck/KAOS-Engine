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
    public class SimpleCameraDraw1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string quadName;

        DrawBuffersEnum[] targets;

        RenderClearDeferredQuadP renderClear;
        RenderLightDirectionalDeferredQuadP renderLightDirectional;
        RenderLightPointDeferredP renderLightPoint;
        RenderLightSpotDeferredP renderLightSpot;
        RenderScreenDeferredQuadP renderScreen;

        Vector3 position;
        Vector3 direction;
        Vector3 lightDiffuse;
        Vector3 lightSpecular;

        Matrix4 world;
        Matrix4 view;
        Matrix4 projection;

        public SimpleCameraDraw1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            quadName = "Quad";
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;

            targets = new DrawBuffersEnum[4];

            renderClear = new RenderClearDeferredQuadP();
            renderLightDirectional = new RenderLightDirectionalDeferredQuadP();
            renderLightPoint = new RenderLightPointDeferredP();
            renderLightSpot = new RenderLightSpotDeferredP();
            renderScreen = new RenderScreenDeferredQuadP();
        }

        public void SetControllers()
        {
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Simple Camera" + instanceIndexName);
            if (objectBase != null)
            {
                objectBase.UserControllers.DrawMethods += new DrawMethod(Draw);
            }
        }

        public void Draw(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.Camera.Enabled) return;
            if (!objectBase.Camera.Active) return;

            PhysicsObject menuPhysicsObjectWithCamera = demo.MenuScene.PhysicsScene.GetPhysicsObjectWithCamera(0);

            if (menuPhysicsObjectWithCamera != null)
            {
                if (menuPhysicsObjectWithCamera.Camera.UserDataObj != null)
                {
                    Camera3Draw1 menuCamera = menuPhysicsObjectWithCamera.Camera.UserDataObj as Camera3Draw1;

                    if (menuCamera != null)
                    {
                        menuCamera.EnableDrawBoundingBoxes = false;
                        menuCamera.EnableDrawContactPoints = false;
                        menuCamera.EnableDrawSlipingObjects = false;
                        menuCamera.EnableDrawLights = false;
                        menuCamera.EnableWireframe = false;
                    }
                }
            }

            float time = args.Time;

            PhysicsObject drawPhysicsObject, transparentPhysicsObject, lightPhysicsObject;
            PhysicsLight sceneLight, drawLight;
            DemoMesh mesh, quad;

            objectBase.Camera.View.GetViewMatrix(ref view);
            objectBase.Camera.Projection.GetProjectionMatrix(ref projection);

            sceneLight = scene.Light;

            quad = demo.Meshes[quadName];

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, demo.SceneFrameBuffer);

            targets[0] = DrawBuffersEnum.ColorAttachment0;
            targets[1] = DrawBuffersEnum.ColorAttachment1;
            targets[2] = DrawBuffersEnum.ColorAttachment2;
            targets[3] = DrawBuffersEnum.ColorAttachment3;

            GL.DrawBuffers(4, targets);

            GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.Zero);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);
            GL.DepthMask(false);
            GL.Disable(EnableCap.CullFace);

            renderClear.SetClearScreenColor(ref demo.ClearScreenColor);

            quad.Draw(renderClear);

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);

            for (int i = 0; i < objectBase.Camera.DrawPhysicsObjectCount; i++)
            {
                drawPhysicsObject = objectBase.Camera.GetDrawPhysicsObject(i);

                if ((drawPhysicsObject.UserControllers.DrawMethods == null) || (drawPhysicsObject == objectBase))
                {
                    if (drawPhysicsObject.UserDataStr == null)
                        continue;

                    if ((drawPhysicsObject.Shape == null) && drawPhysicsObject.IsBrokenRigidGroup)
                        continue;

                    if ((drawPhysicsObject.RigidGroupOwner != drawPhysicsObject) && (drawPhysicsObject.RigidGroupOwner.UserDataStr != null))
                        continue;

                    drawPhysicsObject.MainWorldTransform.GetTransformMatrix(ref world);

                    mesh = demo.Meshes[drawPhysicsObject.UserDataStr];

                    mesh.Draw(ref world, ref view, ref projection, sceneLight, drawPhysicsObject.Material, objectBase.Camera, false, false);
                }
                else
                {
                    if (drawPhysicsObject.UserControllers.EnableDraw)
                        continue;

                    if ((drawPhysicsObject.Shape == null) && drawPhysicsObject.IsBrokenRigidGroup)
                        continue;

                    if ((drawPhysicsObject.RigidGroupOwner != drawPhysicsObject) && (drawPhysicsObject.RigidGroupOwner.UserDataStr != null))
                        continue;

                    drawPhysicsObject.UserControllers.DrawMethodArgs.Time = time;
                    drawPhysicsObject.UserControllers.DrawMethodArgs.OwnerIndex = drawPhysicsObject.Index;
                    drawPhysicsObject.UserControllers.DrawMethodArgs.OwnerSceneIndex = scene.Index;
                    drawPhysicsObject.UserControllers.DrawMethods(drawPhysicsObject.UserControllers.DrawMethodArgs);
                }
            }

            if (objectBase.Camera.TransparentPhysicsObjectCount != 0)
            {
                targets[0] = DrawBuffersEnum.ColorAttachment0;
                targets[1] = DrawBuffersEnum.ColorAttachment1;
                targets[2] = DrawBuffersEnum.None;
                targets[3] = DrawBuffersEnum.None;

                GL.DrawBuffers(4, targets);

                GL.DepthMask(false);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.OneMinusSrcAlpha);
                GL.BlendEquation(BlendEquationMode.FuncAdd);

                for (int i = 0; i < objectBase.Camera.TransparentPhysicsObjectCount; i++)
                {
                    transparentPhysicsObject = objectBase.Camera.GetTransparentPhysicsObject(i);

                    if ((transparentPhysicsObject.UserControllers.DrawMethods == null) || (transparentPhysicsObject == objectBase))
                    {
                        if (transparentPhysicsObject.UserDataStr == null)
                            continue;

                        if ((transparentPhysicsObject.Shape == null) && transparentPhysicsObject.IsBrokenRigidGroup)
                            continue;

                        if ((transparentPhysicsObject.RigidGroupOwner != transparentPhysicsObject) && (transparentPhysicsObject.RigidGroupOwner.UserDataStr != null))
                            continue;

                        transparentPhysicsObject.MainWorldTransform.GetTransformMatrix(ref world);

                        mesh = demo.Meshes[transparentPhysicsObject.UserDataStr];

                        mesh.Draw(ref world, ref view, ref projection, sceneLight, transparentPhysicsObject.Material, objectBase.Camera, false, false);
                    }
                    else
                    {
                        if (transparentPhysicsObject.UserControllers.EnableDraw)
                            continue;

                        if ((transparentPhysicsObject.Shape == null) && transparentPhysicsObject.IsBrokenRigidGroup)
                            continue;

                        if ((transparentPhysicsObject.RigidGroupOwner != transparentPhysicsObject) && (transparentPhysicsObject.RigidGroupOwner.UserDataStr != null))
                            continue;

                        transparentPhysicsObject.UserControllers.DrawMethodArgs.Time = time;
                        transparentPhysicsObject.UserControllers.DrawMethodArgs.OwnerIndex = transparentPhysicsObject.Index;
                        transparentPhysicsObject.UserControllers.DrawMethodArgs.OwnerSceneIndex = scene.Index;
                        transparentPhysicsObject.UserControllers.DrawMethods(transparentPhysicsObject.UserControllers.DrawMethodArgs);
                    }
                }

                targets[0] = DrawBuffersEnum.None;
                targets[1] = DrawBuffersEnum.None;
                targets[2] = DrawBuffersEnum.ColorAttachment2;
                targets[3] = DrawBuffersEnum.ColorAttachment3;

                GL.DrawBuffers(4, targets);

                GL.DepthMask(true);
                GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.Zero);
                GL.BlendEquation(BlendEquationMode.FuncAdd);
                GL.Disable(EnableCap.Blend);

                for (int i = 0; i < objectBase.Camera.TransparentPhysicsObjectCount; i++)
                {
                    transparentPhysicsObject = objectBase.Camera.GetTransparentPhysicsObject(i);

                    if (!transparentPhysicsObject.Material.TransparencySecondPass)
                        continue;

                    if ((transparentPhysicsObject.UserControllers.DrawMethods == null) || (transparentPhysicsObject == objectBase))
                    {
                        if (transparentPhysicsObject.UserDataStr == null)
                            continue;

                        if ((transparentPhysicsObject.Shape == null) && transparentPhysicsObject.IsBrokenRigidGroup)
                            continue;

                        if ((transparentPhysicsObject.RigidGroupOwner != transparentPhysicsObject) && (transparentPhysicsObject.RigidGroupOwner.UserDataStr != null))
                            continue;

                        transparentPhysicsObject.MainWorldTransform.GetTransformMatrix(ref world);

                        mesh = demo.Meshes[transparentPhysicsObject.UserDataStr];

                        mesh.Draw(ref world, ref view, ref projection, sceneLight, transparentPhysicsObject.Material, objectBase.Camera, false, false);
                    }
                    else
                    {
                        if (transparentPhysicsObject.UserControllers.EnableDraw)
                            continue;

                        if ((transparentPhysicsObject.Shape == null) && transparentPhysicsObject.IsBrokenRigidGroup)
                            continue;

                        if ((transparentPhysicsObject.RigidGroupOwner != transparentPhysicsObject) && (transparentPhysicsObject.RigidGroupOwner.UserDataStr != null))
                            continue;

                        transparentPhysicsObject.UserControllers.DrawMethodArgs.Time = time;
                        transparentPhysicsObject.UserControllers.DrawMethodArgs.OwnerIndex = transparentPhysicsObject.Index;
                        transparentPhysicsObject.UserControllers.DrawMethodArgs.OwnerSceneIndex = scene.Index;
                        transparentPhysicsObject.UserControllers.DrawMethods(transparentPhysicsObject.UserControllers.DrawMethodArgs);
                    }
                }
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, demo.LightFrameBuffer);

            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);

            GL.ClearColor(demo.ClearLightColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Disable(EnableCap.DepthTest);
            GL.DepthMask(false);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.SrcAlpha);
            GL.BlendEquation(BlendEquationMode.FuncAdd);

            for (int i = 0; i < objectBase.Camera.LightPhysicsObjectCount; i++)
            {
                lightPhysicsObject = objectBase.Camera.GetLightPhysicsObject(i);

                drawLight = lightPhysicsObject.Light;

                if ((drawLight == null) || !drawLight.Enabled)
                    continue;

                if (drawLight.Type == PhysicsLightType.Directional)
                {
                    GL.Disable(EnableCap.CullFace);

                    drawLight.GetDirection(ref direction);
                    drawLight.GetDiffuse(ref lightDiffuse);
                    drawLight.GetSpecular(ref lightSpecular);

                    renderLightDirectional.Enable = true;

                    renderLightDirectional.Width = objectBase.Camera.Projection.Width;
                    renderLightDirectional.Height = objectBase.Camera.Projection.Height;

                    renderLightDirectional.SetView(ref view);
                    renderLightDirectional.SetProjection(ref projection);

                    renderLightDirectional.SetLightDirection(ref direction);
                    renderLightDirectional.SetLightDiffuse(ref lightDiffuse);
                    renderLightDirectional.SetLightSpecular(ref lightSpecular);
                    renderLightDirectional.Intensity = drawLight.Intensity;

                    renderLightDirectional.SpecularTexture = demo.SpecularTexture;
                    renderLightDirectional.NormalTexture = demo.NormalTexture;
                    renderLightDirectional.DepthTexture = demo.DepthTexture;

                    quad.Draw(renderLightDirectional);
                }
                else
                    if (drawLight.Type == PhysicsLightType.Point)
                    {
                        GL.Enable(EnableCap.CullFace);
                        GL.CullFace(CullFaceMode.Front);

                        lightPhysicsObject.MainWorldTransform.GetPosition(ref position);
                        lightPhysicsObject.MainWorldTransform.GetTransformMatrix(ref world);
                        drawLight.GetDiffuse(ref lightDiffuse);
                        drawLight.GetSpecular(ref lightSpecular);

                        renderLightPoint.Enable = true;

                        renderLightPoint.Width = objectBase.Camera.Projection.Width;
                        renderLightPoint.Height = objectBase.Camera.Projection.Height;

                        renderLightPoint.SetWorld(ref world);
                        renderLightPoint.SetView(ref view);
                        renderLightPoint.SetProjection(ref projection);

                        renderLightPoint.SetLightPosition(ref position);
                        renderLightPoint.SetLightDiffuse(ref lightDiffuse);
                        renderLightPoint.SetLightSpecular(ref lightSpecular);
                        renderLightPoint.Range = drawLight.Range;
                        renderLightPoint.Intensity = drawLight.Intensity;

                        renderLightPoint.SpecularTexture = demo.SpecularTexture;
                        renderLightPoint.NormalTexture = demo.NormalTexture;
                        renderLightPoint.DepthTexture = demo.DepthTexture;

                        mesh = demo.Meshes[lightPhysicsObject.UserDataStr];

                        mesh.Draw(renderLightPoint);
                    }
                    else
                        if (drawLight.Type == PhysicsLightType.Spot)
                        {
                            GL.Enable(EnableCap.CullFace);
                            GL.CullFace(CullFaceMode.Front);

                            lightPhysicsObject.MainWorldTransform.GetPosition(ref position);
                            lightPhysicsObject.MainWorldTransform.GetTransformMatrix(ref world);
                            drawLight.GetDiffuse(ref lightDiffuse);
                            drawLight.GetSpecular(ref lightSpecular);
                            direction.X = -world.Row1.X;
                            direction.Y = -world.Row1.Y;
                            direction.Z = -world.Row1.Z;
                            Vector3.Subtract(ref position, ref direction, out position);

                            renderLightSpot.Enable = true;

                            renderLightSpot.Width = objectBase.Camera.Projection.Width;
                            renderLightSpot.Height = objectBase.Camera.Projection.Height;

                            renderLightSpot.SetWorld(ref world);
                            renderLightSpot.SetView(ref view);
                            renderLightSpot.SetProjection(ref projection);

                            renderLightSpot.SetLightPosition(ref position);
                            renderLightSpot.SetLightDirection(ref direction);
                            renderLightSpot.SetLightDiffuse(ref lightDiffuse);
                            renderLightSpot.SetLightSpecular(ref lightSpecular);
                            renderLightSpot.Range = drawLight.Range;
                            renderLightSpot.Intensity = drawLight.Intensity;
                            renderLightSpot.InnerRadAngle = drawLight.SpotInnerRadAngle;
                            renderLightSpot.OuterRadAngle = drawLight.SpotOuterRadAngle;

                            renderLightSpot.SpecularTexture = demo.SpecularTexture;
                            renderLightSpot.NormalTexture = demo.NormalTexture;
                            renderLightSpot.DepthTexture = demo.DepthTexture;

                            mesh = demo.Meshes[lightPhysicsObject.UserDataStr];

                            mesh.Draw(renderLightSpot);
                        }
            }

            if ((sceneLight != null) && sceneLight.Enabled)
            {
                GL.Disable(EnableCap.CullFace);

                sceneLight.GetDirection(ref direction);
                sceneLight.GetDiffuse(ref lightDiffuse);
                sceneLight.GetSpecular(ref lightSpecular);

                renderLightDirectional.Enable = true;

                renderLightDirectional.Width = objectBase.Camera.Projection.Width;
                renderLightDirectional.Height = objectBase.Camera.Projection.Height;

                renderLightDirectional.SetView(ref view);
                renderLightDirectional.SetProjection(ref projection);

                renderLightDirectional.SetLightDirection(ref direction);
                renderLightDirectional.SetLightDiffuse(ref lightDiffuse);
                renderLightDirectional.SetLightSpecular(ref lightSpecular);
                renderLightDirectional.Intensity = sceneLight.Intensity;

                renderLightDirectional.SpecularTexture = demo.SpecularTexture;
                renderLightDirectional.NormalTexture = demo.NormalTexture;
                renderLightDirectional.DepthTexture = demo.DepthTexture;

                quad.Draw(renderLightDirectional);
            }

            if (!demo.EnableMenu)
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            }
            else
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, demo.ScreenFrameBuffer);
                GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            }

            GL.Disable(EnableCap.CullFace);
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.Zero);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.Disable(EnableCap.Blend);

            renderScreen.Width = objectBase.Camera.Projection.Width;
            renderScreen.Height = objectBase.Camera.Projection.Height;
            renderScreen.ColorTexture = demo.ColorTexture;
            renderScreen.LightTexture = demo.LightTexture;

            quad.Draw(renderScreen);

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
        }
    }
}
