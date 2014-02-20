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
    public class Camera3Draw1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string quadName;

        public bool EnableDrawBoundingBoxes;
        public bool EnableDrawContactPoints;
        public bool EnableDrawSlipingObjects;
        public bool EnableDrawLights;
        public bool EnableWireframe;

        DrawBuffersEnum[] targets;

        RenderClearTextureDeferredQuadP renderClearTexture;
        RenderLightDirectionalDeferredQuadP renderLightDirectional;
        RenderLightPointDeferredP renderLightPoint;
        RenderLightSpotDeferredP renderLightSpot;
        RenderScreenDeferredQuadP renderScreen;

        RenderPC render;
        VertexPositionColor[] cameraVertices;

        Vector3 position;
        Vector3 direction;
        Vector3 lightDiffuse;
        Vector3 lightSpecular;
        BoundingBox boundingBox;

        Matrix4 world;
        Matrix4 view;
        Matrix4 projection;

        Matrix4 matrixIdentity;

        public Camera3Draw1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            quadName = "Quad";

            matrixIdentity = Matrix4.Identity;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;

            EnableDrawBoundingBoxes = false;
            EnableDrawContactPoints = false;
            EnableDrawSlipingObjects = false;
            EnableDrawLights = false;
            EnableWireframe = false;

            targets = new DrawBuffersEnum[4];

            cameraVertices = new VertexPositionColor[24];

            renderClearTexture = new RenderClearTextureDeferredQuadP();
            renderLightDirectional = new RenderLightDirectionalDeferredQuadP();
            renderLightPoint = new RenderLightPointDeferredP();
            renderLightSpot = new RenderLightSpotDeferredP();
            renderScreen = new RenderScreenDeferredQuadP();

            render = new RenderPC();
        }

        public void SetControllers(bool enableDrawBoundingBoxes, bool enableDrawContactPoints, bool enableDrawSlipingObjects, bool enableDrawLights, bool enableWireframe)
        {
            this.EnableDrawBoundingBoxes = enableDrawBoundingBoxes;
            this.EnableDrawContactPoints = enableDrawContactPoints;
            this.EnableDrawSlipingObjects = enableDrawSlipingObjects;
            this.EnableDrawLights = enableDrawLights;
            this.EnableWireframe = enableWireframe;

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Camera 3" + instanceIndexName);

            if (objectBase != null)
            {
                objectBase.Camera.UserDataObj = this;
                objectBase.UserControllers.DrawMethods += new DrawMethod(Draw);
                objectBase.UserControllers.DrawMethods += new DrawMethod(DrawBoundingBoxes);
                objectBase.UserControllers.DrawMethods += new DrawMethod(DrawContactPoints);
            }
        }

        void Draw(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.Camera.Enabled) return;

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

            renderClearTexture.Width = objectBase.Camera.Projection.Width;
            renderClearTexture.Height = objectBase.Camera.Projection.Height;
            renderClearTexture.ColorTexture = demo.ScreenTexture;

            quad.Draw(renderClearTexture);

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

                    mesh.Draw(ref world, ref view, ref projection, sceneLight, drawPhysicsObject.Material, objectBase.Camera, drawPhysicsObject.RigidGroupOwner.IsSleeping && EnableDrawSlipingObjects, EnableWireframe);
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

                        mesh.Draw(ref world, ref view, ref projection, sceneLight, transparentPhysicsObject.Material, objectBase.Camera, transparentPhysicsObject.RigidGroupOwner.IsSleeping && EnableDrawSlipingObjects, EnableWireframe);
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

                        mesh.Draw(ref world, ref view, ref projection, sceneLight, transparentPhysicsObject.Material, objectBase.Camera, transparentPhysicsObject.RigidGroupOwner.IsSleeping && EnableDrawSlipingObjects, EnableWireframe);
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
                        lightPhysicsObject.EnableAddToCameraDrawTransparentPhysicsObjects = false;

                        if (EnableDrawLights)
                        {
                            lightPhysicsObject.EnableAddToCameraDrawTransparentPhysicsObjects = true;
                            lightPhysicsObject.Material.TransparencyFactor = 0.5f;
                            lightPhysicsObject.Material.TransparencySecondPass = false;
                        }

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
                            lightPhysicsObject.EnableAddToCameraDrawTransparentPhysicsObjects = false;

                            if (EnableDrawLights)
                            {
                                lightPhysicsObject.EnableAddToCameraDrawTransparentPhysicsObjects = true;
                                lightPhysicsObject.Material.TransparencyFactor = 0.5f;
                                lightPhysicsObject.Material.TransparencySecondPass = false;
                            }

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

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

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

        public void DrawBoundingBoxes(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.Camera.Enabled) return;
            if (!objectBase.Camera.Active) return;
            if (!EnableDrawBoundingBoxes) return;

            float time = args.Time;

            PhysicsObject visiblePhysicsObject;
            Vector3 min, max;

            objectBase.Camera.View.GetViewMatrix(ref view);
            objectBase.Camera.Projection.GetProjectionMatrix(ref projection);
            world = matrixIdentity;

            GL.Color3(1.0f, 1.0f, 1.0f);

            render.SetWorld(ref world);
            render.SetView(ref view);
            render.SetProjection(ref projection);

            render.Apply();

            for (int i = 0; i < objectBase.Camera.VisiblePhysicsObjectCount; i++)
            {
                visiblePhysicsObject = objectBase.Camera.GetVisiblePhysicsObject(i);

                visiblePhysicsObject.GetBoundingBox(ref boundingBox);
                min = boundingBox.Min;
                max = boundingBox.Max;

                cameraVertices[0].Position.X = min.X;
                cameraVertices[0].Position.Y = min.Y;
                cameraVertices[0].Position.Z = min.Z;

                cameraVertices[1].Position.X = max.X;
                cameraVertices[1].Position.Y = min.Y;
                cameraVertices[1].Position.Z = min.Z;

                cameraVertices[2].Position.X = min.X;
                cameraVertices[2].Position.Y = min.Y;
                cameraVertices[2].Position.Z = min.Z;

                cameraVertices[3].Position.X = min.X;
                cameraVertices[3].Position.Y = max.Y;
                cameraVertices[3].Position.Z = min.Z;

                cameraVertices[4].Position.X = min.X;
                cameraVertices[4].Position.Y = min.Y;
                cameraVertices[4].Position.Z = min.Z;

                cameraVertices[5].Position.X = min.X;
                cameraVertices[5].Position.Y = min.Y;
                cameraVertices[5].Position.Z = max.Z;

                cameraVertices[6].Position.X = max.X;
                cameraVertices[6].Position.Y = max.Y;
                cameraVertices[6].Position.Z = max.Z;

                cameraVertices[7].Position.X = min.X;
                cameraVertices[7].Position.Y = max.Y;
                cameraVertices[7].Position.Z = max.Z;

                cameraVertices[8].Position.X = max.X;
                cameraVertices[8].Position.Y = max.Y;
                cameraVertices[8].Position.Z = max.Z;

                cameraVertices[9].Position.X = max.X;
                cameraVertices[9].Position.Y = min.Y;
                cameraVertices[9].Position.Z = max.Z;

                cameraVertices[10].Position.X = max.X;
                cameraVertices[10].Position.Y = max.Y;
                cameraVertices[10].Position.Z = max.Z;

                cameraVertices[11].Position.X = max.X;
                cameraVertices[11].Position.Y = max.Y;
                cameraVertices[11].Position.Z = min.Z;

                cameraVertices[12].Position.X = min.X;
                cameraVertices[12].Position.Y = max.Y;
                cameraVertices[12].Position.Z = min.Z;

                cameraVertices[13].Position.X = min.X;
                cameraVertices[13].Position.Y = max.Y;
                cameraVertices[13].Position.Z = max.Z;

                cameraVertices[14].Position.X = min.X;
                cameraVertices[14].Position.Y = max.Y;
                cameraVertices[14].Position.Z = min.Z;

                cameraVertices[15].Position.X = max.X;
                cameraVertices[15].Position.Y = max.Y;
                cameraVertices[15].Position.Z = min.Z;

                cameraVertices[16].Position.X = max.X;
                cameraVertices[16].Position.Y = min.Y;
                cameraVertices[16].Position.Z = max.Z;

                cameraVertices[17].Position.X = max.X;
                cameraVertices[17].Position.Y = min.Y;
                cameraVertices[17].Position.Z = min.Z;

                cameraVertices[18].Position.X = max.X;
                cameraVertices[18].Position.Y = min.Y;
                cameraVertices[18].Position.Z = max.Z;

                cameraVertices[19].Position.X = min.X;
                cameraVertices[19].Position.Y = min.Y;
                cameraVertices[19].Position.Z = max.Z;

                cameraVertices[20].Position.X = min.X;
                cameraVertices[20].Position.Y = max.Y;
                cameraVertices[20].Position.Z = max.Z;

                cameraVertices[21].Position.X = min.X;
                cameraVertices[21].Position.Y = min.Y;
                cameraVertices[21].Position.Z = max.Z;

                cameraVertices[22].Position.X = max.X;
                cameraVertices[22].Position.Y = max.Y;
                cameraVertices[22].Position.Z = min.Z;

                cameraVertices[23].Position.X = max.X;
                cameraVertices[23].Position.Y = min.Y;
                cameraVertices[23].Position.Z = min.Z;

                GL.Begin(PrimitiveType.Lines);

                for (int j = 0; j < 24; j += 2)
                {
                    GL.Vertex3(cameraVertices[j].Position);
                    GL.Vertex3(cameraVertices[j + 1].Position);
                }

                GL.End();
            }
        }

        public void DrawContactPoints(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.Camera.Enabled) return;
            if (!objectBase.Camera.Active) return;
            if (!EnableDrawContactPoints) return;

            GL.Disable(EnableCap.DepthTest);
            GL.DepthMask(false);

            float time = args.Time;

            PhysicsObject visiblePhysicsObject;
            int contactPointCount;
            Vector3 start1, end1, start2, end2;

            objectBase.Camera.View.GetViewMatrix(ref view);
            objectBase.Camera.Projection.GetProjectionMatrix(ref projection);
            world = matrixIdentity;

            render.SetWorld(ref world);
            render.SetView(ref view);
            render.SetProjection(ref projection);

            render.Apply();

            for (int i = 0; i < objectBase.Camera.VisiblePhysicsObjectCount; i++)
            {
                visiblePhysicsObject = objectBase.Camera.GetVisiblePhysicsObject(i);

                for (int j = 0; j < visiblePhysicsObject.CollisionPairCount; j++)
                {
                    contactPointCount = visiblePhysicsObject.GetCollisionPairContactPointCount(j);

                    for (int k = 0; k < contactPointCount; k++)
                    {
                        visiblePhysicsObject.GetCollisionPairContactPointAnchor2(j, k, ref position);
                        visiblePhysicsObject.GetCollisionPairContactPointNormal(j, k, ref direction);

                        Vector3.Multiply(ref direction, 0.5f, out direction);

                        start1 = position;
                        end1 = direction;
                        Vector3.Add(ref start1, ref end1, out end1);

                        start2 = end1;
                        end2 = direction;
                        Vector3.Add(ref start2, ref end2, out end2);

                        GL.Begin(PrimitiveType.Lines);

                        GL.Color3(1.0f, 1.0f, 1.0f);
                        GL.Vertex3(start1);
                        GL.Vertex3(end1);

                        GL.Color3(0.6f, 0.8f, 0.2f);
                        GL.Vertex3(start2);
                        GL.Vertex3(end2);

                        GL.End();
                    }
                }
            }

            GL.DepthMask(true);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
