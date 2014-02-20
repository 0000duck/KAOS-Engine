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
    public class Cursor
    {
        Demo demo;
        PhysicsScene scene;

        public Cursor(Demo demo)
        {
            this.demo = demo;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public static void CreateShapes(Demo demo, PhysicsScene scene)
        {
        }

        public void Create()
        {
            Shape coneY = scene.Factory.ShapeManager.Find("ConeY");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Cursor");
            objectRoot.InitLocalTransform.SetPosition(0.0f, -0.036f, 0.0f);
            objectRoot.InitLocalTransform.SetRotation(Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(45.0f)));
            objectRoot.InitLocalTransform.SetScale(0.018f, 0.036f, 0.009f);
            objectRoot.EnableCollisions = false;
            objectRoot.DrawPriority = 5;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cursor A");
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = coneY;
            objectBase.UserDataStr = "ConeY";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.SetDiffuse(1.0f, 1.0f, 0.0f);
            objectBase.EnableCollisions = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.DrawPriority = 7;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cursor B");
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.SetDiffuse(1.0f, 1.0f, 0.0f);
            objectBase.InitLocalTransform.SetPosition(0.0f, -1.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.3f, 0.7f, 0.3f);
            objectBase.EnableCollisions = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.DrawPriority = 6;

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
