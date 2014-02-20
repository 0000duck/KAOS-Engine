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
    public class Camera2
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Camera2(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Quaternion objectOrientationX, Quaternion objectOrientationY, Quaternion objectOrientationZ, bool actived)
        {
            Quaternion objectOrientationXY, objectOrientation;

            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            PhysicsObject objectRoot = null;
            PhysicsObject objectUp = null;
            PhysicsObject objectBody = null;
            PhysicsObject objectDown = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Camera 2" + instanceIndexName);
            objectRoot.MaxPreUpdateAngularVelocity = 0.0f;
            objectRoot.MaxPostUpdateAngularVelocity = 0.0f;
            objectRoot.EnableBreakRigidGroup = false;
            objectRoot.EnableCursorInteraction = false;
            objectRoot.EnableDrawing = false;
            objectRoot.MaxSleepLinearVelocity = 0.1f;
            objectRoot.MaxSleepAngularVelocity = 0.1f;
            objectRoot.FluidPressureFactor = 0.46f;
            objectRoot.CreateSound(true);
            objectRoot.Sound.UserDataStr = "Footsteps";

            objectUp = scene.Factory.PhysicsObjectManager.Create("Camera 2 Up" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectUp);
            objectUp.Material.RigidGroup = true;
            objectUp.Shape = sphere;
            objectUp.UserDataStr = "Sphere";
            objectUp.InitLocalTransform.SetPosition(0.0f, 2.0f, 0.0f);
            objectUp.InitLocalTransform.SetScale(1.5f, 1.0f, 1.5f);
            objectUp.Integral.SetDensity(0.1f);
            objectUp.EnableBreakRigidGroup = false;
            objectUp.EnableCursorInteraction = false;
            objectUp.EnableDrawing = false;
            objectUp.PostTransformPriority = 1;
            objectUp.MinResponseLinearVelocity = 0.005f;
            objectUp.MinResponseAngularVelocity = 0.005f;

            objectUp.CreateCamera(true);
            objectUp.Camera.Active = actived;
            objectUp.InternalControllers.CreateCursorController(true);
            objectUp.CreateSound(false);
            objectUp.Sound.Range = 100.0f;
            objectUp.Sound.HitPitch = -0.8f;

            objectBody = scene.Factory.PhysicsObjectManager.Create("Camera 2 Body" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBody);
            objectBody.Material.RigidGroup = true;
            objectBody.Shape = cylinderY;
            objectBody.UserDataStr = "CylinderY";
            objectBody.InitLocalTransform.SetPosition(0.0f, 0.0f, 0.0f);
            objectBody.InitLocalTransform.SetScale(2.0f);
            objectBody.Integral.SetDensity(1.0f);
            objectBody.EnableBreakRigidGroup = false;
            objectBody.EnableCursorInteraction = false;
            objectBody.EnableDrawing = false;
            objectBody.MinResponseLinearVelocity = 0.005f;
            objectBody.MinResponseAngularVelocity = 0.005f;

            objectDown = scene.Factory.PhysicsObjectManager.Create("Camera 2 Down" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectDown);
            objectDown.Material.RigidGroup = true;
            objectDown.Shape = sphere;
            objectDown.UserDataStr = "Sphere";
            objectDown.InitLocalTransform.SetPosition(0.0f, -2.0f, 0.0f);
            objectDown.InitLocalTransform.SetScale(1.5f, 2.0f, 1.5f);
            objectDown.Integral.SetDensity(0.5f);
            objectDown.EnableBreakRigidGroup = false;
            objectDown.EnableCursorInteraction = false;
            objectDown.EnableDrawing = false;
            objectDown.MinResponseLinearVelocity = 0.005f;
            objectDown.MinResponseAngularVelocity = 0.005f;

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientationY);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            objectRoot.UpdateFromInitLocalTransform();

            Vector3 position = Vector3.Zero;
            Matrix4 cameraRotation = Matrix4.Identity;

            Quaternion.Multiply(ref objectOrientationX, ref objectOrientationY, out objectOrientationXY);
            Quaternion.Multiply(ref objectOrientationXY, ref objectOrientationZ, out objectOrientation);
            Matrix4 rotation = Matrix4.CreateFromQuaternion(objectOrientation);

            objectUp.Camera.SetOrientation(ref objectOrientation);
            objectUp.Camera.SetRotation(ref rotation);
            objectUp.Camera.SetEuler(ref rotation);
            objectUp.Camera.Projection.CreatePerspectiveLH(1.0f, 11000.0f, 70.0f, demo.WindowWidth, demo.WindowHeight);

            objectUp.MainWorldTransform.GetPosition(ref position);
            objectUp.Camera.GetTransposeRotation(ref cameraRotation);

            objectUp.Camera.View.CreateLookAtLH(ref position, ref cameraRotation, 0.0f);
            objectUp.Camera.UpdateFrustum();

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
