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
    public class Camera3
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Camera3(Demo demo, int instanceIndex)
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
        }

        public void Create(Vector3 objectPosition, Quaternion objectOrientationX, Quaternion objectOrientationY, Quaternion objectOrientationZ)
        {
            Quaternion objectOrientationXY, objectOrientation;

            PhysicsObject objectRoot = scene.Factory.PhysicsObjectManager.Create("Camera 3" + instanceIndexName);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);
            objectRoot.InitLocalTransform.SetOrientation(objectOrientationX * objectOrientationY * objectOrientationZ);
            objectRoot.CreateCamera(true);
            objectRoot.Camera.Active = false;
            objectRoot.InternalControllers.CreateScreenToRayController(true);
            objectRoot.PostTransformPriority = 1;

            objectRoot.UpdateFromInitLocalTransform();

            Vector3 position = Vector3.Zero;
            Matrix4 cameraRotation = Matrix4.Identity;

            Quaternion.Multiply(ref objectOrientationX, ref objectOrientationY, out objectOrientationXY);
            Quaternion.Multiply(ref objectOrientationXY, ref objectOrientationZ, out objectOrientation);
            Matrix4 rotation = Matrix4.CreateFromQuaternion(objectOrientation);

            objectRoot.Camera.SetOrientation(ref objectOrientation);
            objectRoot.Camera.SetRotation(ref rotation);
            objectRoot.Camera.SetEuler(ref rotation);
            objectRoot.Camera.Projection.CreatePerspectiveLH(1.0f, 11000.0f, 70.0f, demo.WindowWidth, demo.WindowHeight);

            objectRoot.MainWorldTransform.GetPosition(ref position);
            objectRoot.Camera.GetTransposeRotation(ref cameraRotation);

            objectRoot.Camera.View.CreateLookAtLH(ref position, ref cameraRotation, 0.0f);
            objectRoot.Camera.UpdateFrustum();

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
