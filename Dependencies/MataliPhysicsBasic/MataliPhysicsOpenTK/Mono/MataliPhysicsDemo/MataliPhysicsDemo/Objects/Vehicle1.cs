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
    public class Vehicle1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Vehicle1(Demo demo, int instanceIndex)
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
            PhysicsObject objectA = null;
            PhysicsObject objectB = null;

            Vector3 position1 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Vehicle 1" + instanceIndexName);

            objectA = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Body" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectA);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Body Up" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 1.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(3.0f, 0.5f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Body Down" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetScale(4.0f, 1.0f, 4.0f);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Wing Right" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(4.0f, -1.0f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(10.0f)));
            objectBase.InitLocalTransform.SetScale(4.0f, 0.1f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Wing Left" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(-4.0f, -1.0f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-10.0f)));
            objectBase.InitLocalTransform.SetScale(4.0f, 0.1f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectB = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Turret" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Turret Body Up" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 4.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(3.0f, 0.5f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Turret Body Down" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 2.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(4.0f, 1.0f, 4.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Turret Gun 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(-1.0f, 2.5f, 5.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 2.0f, 0.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Vehicle 1 Turret Gun 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(1.0f, 2.5f, 5.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 2.0f, 0.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Vehicle 1 Turret Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Vehicle 1 Turret Body Down" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Vehicle 1 Body Up" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, -1.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, -1.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.Update();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
