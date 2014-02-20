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
    public class SimpleCameraAnimation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string sphereName;
        string shotName;
        string shotBaseName;
        string shotLightName;

        Shape sphere;

        DemoMouseState oldMouseState;
        DemoKeyboardState oldKeyboardState;

        Vector3 position;
        Vector3 direction;
        Matrix4 rotation;
        Matrix4 cameraRotation;

        Random random;

        Vector3 vectorZero;
        Matrix4 matrixIdentity;
        Quaternion quaternionIdentity;

        public SimpleCameraAnimation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            sphereName = "Sphere";
            shotName = "Simple Camera Shot" + instanceIndexName + " ";
            shotBaseName = "Simple Camera Shot Object" + instanceIndexName + " ";
            shotLightName = "Simple Camera Shot Light" + instanceIndexName + " ";

            random = new Random();

            vectorZero = Vector3.Zero;
            matrixIdentity = Matrix4.Identity;
            quaternionIdentity = Quaternion.Identity;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            sphere = scene.Factory.ShapeManager.Find("Sphere");

            oldMouseState = demo.GetMouseState();
            oldKeyboardState = demo.GetKeyboardState();

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Simple Camera" + instanceIndexName);
            if (objectBase != null)
            {
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Move);
            }
        }

        public void RefreshControllers()
        {
            oldMouseState = demo.GetMouseState();
            oldKeyboardState = demo.GetKeyboardState();
        }

        public void Move(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.Camera.Enabled) return;
            if (!objectBase.Camera.Active) return;

            float time = (float)args.Time;

            Vector3 deltaRotation = vectorZero;
            Vector3 deltaTranslation = vectorZero;
            float rotationSpeed = 8.0f;
            float translationSpeed = 8.0f;
            bool enableShot = false;

            DemoMouseState mouseState = demo.GetMouseState();
            DemoKeyboardState keyboardState = demo.GetKeyboardState();

            if (mouseState[MouseButton.Right])
            {
                deltaRotation.Y += MathHelper.DegreesToRadians(rotationSpeed * (mouseState.X - oldMouseState.X) * time);
                deltaRotation.X += MathHelper.DegreesToRadians(rotationSpeed * (mouseState.Y - oldMouseState.Y) * time);
            }

            if (mouseState[MouseButton.Middle] && !oldMouseState[MouseButton.Middle])
                enableShot = true;

            if ((keyboardState[Key.ControlRight] && !oldKeyboardState[Key.ControlRight]) ||
               (keyboardState[Key.ControlLeft] && !oldKeyboardState[Key.ControlLeft]))
                enableShot = true;

            if (keyboardState[Key.W])
                deltaTranslation.Z += translationSpeed * time;

            if (keyboardState[Key.S])
                deltaTranslation.Z -= translationSpeed * time;

            if (keyboardState[Key.D])
                deltaTranslation.X += translationSpeed * time;

            if (keyboardState[Key.A])
                deltaTranslation.X -= translationSpeed * time;

            oldMouseState = mouseState;
            oldKeyboardState = keyboardState;

            if (deltaRotation.LengthSquared != 0.0f)
            {
                Vector3 euler = vectorZero;
                objectBase.Camera.GetEuler(ref euler);
                Vector3.Add(ref euler, ref deltaRotation, out euler);
                objectBase.Camera.SetEuler(ref euler);

                Matrix4 rotationX, rotationY;
                Matrix4.CreateRotationX(-euler.X, out rotationX);
                Matrix4.CreateRotationY(-euler.Y, out rotationY);
                Matrix4.Mult(ref rotationY, ref rotationX, out cameraRotation);

                objectBase.Camera.SetRotation(ref cameraRotation);

                objectBase.MainWorldTransform.SetTransposeRotation(ref cameraRotation);
                objectBase.RecalculateMainTransform();
            }

            if (deltaTranslation.LengthSquared != 0.0f)
            {
                objectBase.MainWorldTransform.GetRotation(ref rotation);
                Vector3.TransformVector(ref deltaTranslation, ref rotation, out direction);

                objectBase.MainWorldTransform.GetPosition(ref position);
                Vector3.Add(ref position, ref direction, out position);
                objectBase.MainWorldTransform.SetPosition(ref position);

                objectBase.RecalculateMainTransform();
            }

            objectBase.Camera.Projection.CreatePerspectiveLH(1.0f, 11000.0f, 70.0f, demo.WindowWidth, demo.WindowHeight);

            objectBase.MainWorldTransform.GetPosition(ref position);
            objectBase.Camera.GetTransposeRotation(ref cameraRotation);

            objectBase.Camera.View.CreateLookAtLH(ref position, ref cameraRotation, 0.0f);
            objectBase.Camera.UpdateFrustum();

            if (enableShot)
            {
                Vector3 shotScale, shotColor;

                string frameCountName = scene.SimulationFrameCount.ToString();
                PhysicsObject shot = scene.Factory.PhysicsObjectManager.Create(shotName + frameCountName);
                PhysicsObject shotBase = scene.Factory.PhysicsObjectManager.Create(shotBaseName + frameCountName);
                PhysicsObject shotLight = scene.Factory.PhysicsObjectManager.Create(shotLightName + frameCountName);

                shot.AddChildPhysicsObject(shotBase);
                shot.AddChildPhysicsObject(shotLight);

                shotScale = shotColor = vectorZero;

                shotScale.X = shotScale.Y = shotScale.Z = 0.5f;

                objectBase.MainWorldTransform.GetPosition(ref position);
                objectBase.Camera.GetTransposeRotation(ref cameraRotation);

                direction.X = cameraRotation.Row2.X;
                direction.Y = cameraRotation.Row2.Y;
                direction.Z = cameraRotation.Row2.Z;

                Vector3.Multiply(ref direction, 300.0f, out direction);

                shot.InitLocalTransform.SetRotation(ref matrixIdentity);
                shot.InitLocalTransform.SetPosition(ref position);
                shot.InitLocalTransform.SetLinearVelocity(ref direction);
                shot.InitLocalTransform.SetAngularVelocity(ref vectorZero);
                shot.MaxSimulationFrameCount = 200;
                //shot.EnableLocalGravity = true;

                shotBase.Shape = sphere;
                shotBase.UserDataStr = sphereName;
                shotBase.InitLocalTransform.SetScale(ref shotScale);
                shotBase.Integral.SetDensity(10.0f);
                shotBase.Material.RigidGroup = true;
                shotBase.EnableBreakRigidGroup = false;
                shotBase.EnableCollisions = true;
                shotBase.DisableCollision(objectBase, true);
                shotBase.MaxDisableCollisionFrameCount = 50;

                shotLight.Shape = sphere;
                shotLight.UserDataStr = sphereName;
                shotLight.CreateLight(true);
                shotLight.Light.Type = PhysicsLightType.Point;
                shotLight.Light.Range = 20.0f;

                shotColor.X = (float)Math.Max(random.NextDouble(), random.NextDouble());
                shotColor.Y = (float)Math.Max(random.NextDouble(), random.NextDouble());
                shotColor.Z = (float)Math.Max(random.NextDouble(), random.NextDouble());

                shotLight.Light.SetDiffuse(ref shotColor);
                shotLight.InitLocalTransform.SetScale(20.0f);
                shotLight.Material.RigidGroup = true;
                shotLight.EnableBreakRigidGroup = false;
                shotLight.EnableCollisions = false;
                shotLight.EnableCursorInteraction = false;
                shotLight.EnableAddToCameraDrawTransparentPhysicsObjects = false;

                scene.UpdateFromInitLocalTransform(shot);
            }

            objectBase.Camera.UpdatePhysicsObjects(true, true, true);
            objectBase.Camera.SortDrawPhysicsObjects(PhysicsCameraSortOrderType.DrawPriorityShapePrimitiveType);
            objectBase.Camera.SortTransparentPhysicsObjects(PhysicsCameraSortOrderType.DrawPriorityShapePrimitiveType);
        }
    }
}
