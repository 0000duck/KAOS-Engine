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

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Camera3Animation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string sphereName;
        string cursorName;
        string shotName;

        Shape sphere;

        DemoMouseState oldMouseState;

        Vector3 startPosition;
        Vector3 step;
        int stepCount;
        int maxStepCount;

        Vector3 position;
        Matrix4 rotation;
        Matrix4 cameraRotation;
        Matrix4 projection;
        Matrix4 view;

        Vector2 mousePosition;

        Vector3 vectorZero;
        Matrix4 matrixIdentity;
        Quaternion quaternionIdentity;

        public Camera3Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            sphereName = "Sphere";
            cursorName = "Cursor";
            shotName = "Camera 3 Shot" + instanceIndexName + " ";

            vectorZero = Vector3.Zero;
            matrixIdentity = Matrix4.Identity;
            quaternionIdentity = Quaternion.Identity;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;

            startPosition = Vector3.Zero;
            step = Vector3.Zero;
            stepCount = 0;
            maxStepCount = 0;
        }

        public void SetControllers(Vector3 objectStep, int objectMaxStepCount)
        {
            step = objectStep;
            maxStepCount = objectMaxStepCount;

            sphere = scene.Factory.ShapeManager.Find("Sphere");

            stepCount = 0;

            oldMouseState = demo.GetMouseState();

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Camera 3" + instanceIndexName);

            if (objectBase != null)
            {
                objectBase.Camera.Active = false;

                objectBase.UserControllers.TransformMethods += new SimulateMethod(MoveCursor);
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Move);
            }
        }

        public void RefreshControllers()
        {
            stepCount = 0;

            oldMouseState = demo.GetMouseState();

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Camera 3" + instanceIndexName);
            if (objectBase != null)
                objectBase.Camera.Active = false;
        }

        public void MoveCursor(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.Camera.Enabled) return;
            if (!objectBase.Camera.Active) return;

            float time = (float)args.Time;

            DemoMouseState mouseState = demo.GetMouseState();

            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            objectBase.Camera.View.GetViewMatrix(ref view);
            objectBase.Camera.Projection.GetProjectionMatrix(ref projection);

            ScreenToRayController screenToRayController = objectBase.InternalControllers.ScreenToRayController;
            screenToRayController.SetViewport(0, 0, demo.WindowWidth, demo.WindowHeight, 0.0f, 1.0f);
            screenToRayController.SetViewMatrix(ref view);
            screenToRayController.SetProjectionMatrix(ref projection);
            screenToRayController.SetScreenPosition(ref mousePosition);
            screenToRayController.MouseButton = true;
            screenToRayController.Update();
        }

        void Move(SimulateMethodArgs args)
        {
            Vector3 cameraPosition;

            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.Camera.Enabled) return;

            if (!objectBase.Camera.Active)
            {
                objectBase.InitLocalTransform.GetPosition(ref startPosition);
                Vector3.Multiply(ref step, stepCount, out cameraPosition);
                Vector3.Add(ref cameraPosition, ref startPosition, out cameraPosition);

                if (stepCount <= maxStepCount)
                {
                    objectBase.MainWorldTransform.SetPosition(ref cameraPosition);
                    objectBase.RecalculateMainTransform();
                    stepCount++;
                }
                else
                    objectBase.Camera.Active = true;
            }

            float time = (float)args.Time;

            bool enableShot = false;

            DemoMouseState mouseState = demo.GetMouseState();

            if (mouseState[MouseButton.Left] && !oldMouseState[MouseButton.Left])
                enableShot = true;

            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            oldMouseState = mouseState;

            objectBase.Camera.Projection.CreatePerspectiveLH(1.0f, 11000.0f, 70.0f, demo.WindowWidth, demo.WindowHeight);

            objectBase.MainWorldTransform.GetPosition(ref position);
            objectBase.Camera.GetTransposeRotation(ref cameraRotation);

            objectBase.Camera.View.CreateLookAtLH(ref position, ref cameraRotation, 0.0f);
            objectBase.Camera.UpdateFrustum();

            objectBase.Camera.View.GetViewMatrix(ref view);
            objectBase.Camera.Projection.GetProjectionMatrix(ref projection);

            Vector3 rayPosition, rayDirection;

            rayPosition = rayDirection = vectorZero;

            objectBase.UnProjectToRay(ref mousePosition, 0, 0, demo.WindowWidth, demo.WindowHeight, 0.0f, 1.0f, ref view, ref matrixIdentity, ref projection, ref rayPosition, ref rayDirection);

            PhysicsObject cursor = scene.Factory.PhysicsObjectManager.Find(cursorName);

            if (cursor != null)
            {
                Vector3 cursorPosition = vectorZero;
                Matrix4 cursorLocalRotation = matrixIdentity;
                Matrix4 cursorWorldRotation = matrixIdentity;

                cursor.InitLocalTransform.GetPosition(ref cursorPosition);
                cursor.InitLocalTransform.GetRotation(ref cursorLocalRotation);
                cursor.MainWorldTransform.GetRotation(ref cursorWorldRotation);

                objectBase.Camera.GetTransposeRotation(ref cameraRotation);
                Matrix4.Mult(ref cursorLocalRotation, ref cameraRotation, out rotation);

                Vector3.TransformVector(ref cursorPosition, ref cursorWorldRotation, out position);

                cursor.MainWorldTransform.SetRotation(ref rotation);
                Vector3.Add(ref position, ref rayPosition, out position);
                Vector3.Add(ref position, ref rayDirection, out position);
                cursor.MainWorldTransform.SetPosition(ref position);
            }

            if (enableShot)
            {
                PhysicsObject shot = scene.Factory.PhysicsObjectManager.Create(shotName + scene.SimulationFrameCount.ToString());

                shot.Shape = sphere;
                shot.UserDataStr = sphereName;

                Vector3 shotScale = vectorZero;
                shotScale.X = shotScale.Y = shotScale.Z = 0.5f;

                Vector3.Multiply(ref rayDirection, 1000.0f, out rayDirection);

                shot.InitLocalTransform.SetRotation(ref matrixIdentity);
                shot.InitLocalTransform.SetPosition(ref rayPosition);
                shot.InitLocalTransform.SetScale(ref shotScale);

                shot.InitLocalTransform.SetLinearVelocity(ref rayDirection);
                shot.InitLocalTransform.SetAngularVelocity(ref vectorZero);
                shot.Integral.SetDensity(10.0f);
                shot.MaxSimulationFrameCount = 10;
                shot.EnableCursorInteraction = false;
                shot.EnableCollisionResponse = false;
                shot.EnableDrawing = false;
                shot.EnableCollisions = true;
                shot.DisableCollision(objectBase, true);
                shot.MaxDisableCollisionFrameCount = 10;

                scene.UpdateFromInitLocalTransform(shot);
            }

            objectBase.Camera.UpdatePhysicsObjects(true, true, true);
            objectBase.Camera.SortDrawPhysicsObjects(PhysicsCameraSortOrderType.DrawPriorityShapePrimitiveType);
            objectBase.Camera.SortTransparentPhysicsObjects(PhysicsCameraSortOrderType.DrawPriorityShapePrimitiveType);
        }
    }
}
