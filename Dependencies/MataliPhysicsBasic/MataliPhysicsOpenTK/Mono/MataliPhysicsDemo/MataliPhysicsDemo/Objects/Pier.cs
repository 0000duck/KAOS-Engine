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
    public class Pier
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Pier(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation)
        {
            Shape box = scene.Factory.ShapeManager.Find("Box");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Pier 1 Base" + instanceIndexName);

            for (int i = 0; i < 25; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Pier 1 Base Cylinder Left " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = cylinderY;
                objectBase.UserDataStr = "CylinderY";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(-50.0f, -110.0f, -400.0f + i * 10.0f);
                objectBase.InitLocalTransform.SetScale(2.0f, 20.0f, 2.0f);
            }

            for (int i = 0; i < 25; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Pier 1 Base Cylinder Right " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = cylinderY;
                objectBase.UserDataStr = "CylinderY";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(-15.0f, -110.0f, -400.0f + i * 10.0f);
                objectBase.InitLocalTransform.SetScale(2.0f, 20.0f, 2.0f);
            }

            objectRoot.UpdateFromInitLocalTransform();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Pier 1 Platform" + instanceIndexName);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Pier 1 Platform Cylinder 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.InitLocalTransform.SetPosition(-42.0f, -91.0f, -282.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 122.0f, 1.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Pier 1 Platform Cylinder 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.InitLocalTransform.SetPosition(-13.0f, -91.0f, -285.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 118.0f, 1.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);

            for (int i = 0; i < 21; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Pier 1 Platform Box " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
                objectBase.Material.UserDataStr = "Wood2";
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(-27.0f, -89.5f, -400.0f + i * 11.5f);
                objectBase.InitLocalTransform.SetScale(5.0f, 0.5f, 25.0f);
                objectBase.InitLocalTransform.SetRotation(Matrix4.CreateRotationY(MathHelper.DegreesToRadians(90.0f)));
                objectBase.Integral.SetDensity(1.0f);
            }

            objectRoot.UpdateFromInitLocalTransform();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
