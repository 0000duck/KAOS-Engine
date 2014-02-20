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
    public class Boat1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Boat1(Demo demo, int instanceIndex)
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
            Shape capsuleY = scene.Factory.ShapeManager.Find("CapsuleY");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            PhysicsObject objectA = null;
            PhysicsObject objectB = null;
            PhysicsObject objectC = null;

            Vector3 position1 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Boat 1" + instanceIndexName);

            objectA = scene.Factory.PhysicsObjectManager.Create("Boat 1 Body" + instanceIndexName);
            objectA.EnableFeedback = true;
            objectRoot.AddChildPhysicsObject(objectA);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Body Box 0" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -92.5f, -319.0f);
            objectBase.InitLocalTransform.SetScale(13.0f, 0.5f, 19.0f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Body Box 1" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -95.0f, -300.0f);
            objectBase.InitLocalTransform.SetScale(15.0f, 2.0f, 40.0f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Body Box 2" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(24.0f, -91.0f, -300.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 2.0f, 40.0f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Body Box 3" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(-4.0f, -91.0f, -300.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 2.0f, 40.0f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Body Box 4" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -91.0f, -259.0f);
            objectBase.InitLocalTransform.SetScale(15.0f, 2.0f, 1.0f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Body Box 5" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -91.0f, -339.0f);
            objectBase.InitLocalTransform.SetScale(13.0f, 2.0f, 1.0f);
            objectBase.Integral.SetDensity(2.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Body Box 6" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -88.75f, -278.0f);
            objectBase.InitLocalTransform.SetScale(15.0f, 0.25f, 20.0f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Body Box 7" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -91.0f, -299.0f);
            objectBase.InitLocalTransform.SetScale(13.0f, 2.0f, 1.0f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectB = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Capsule 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = capsuleY;
            objectBase.UserDataStr = "CapsuleY";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -85.0f, -340.0f);
            objectBase.InitLocalTransform.SetScale(4.0f, 2.0f, 1.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Box 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -87.6f, -340.0f);
            objectBase.InitLocalTransform.SetScale(2.0f, 0.8f, 3.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Box 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -86.2f, -340.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 0.6f, 3.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Cylinder 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -88.7f, -339.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 0.3f, 1.0f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Cylinder 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -87.6f, -335.2f);
            objectBase.InitLocalTransform.SetScale(0.2f, 1.8f, 0.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(3.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Cylinder 3" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -93.4f, -342.0f);
            objectBase.InitLocalTransform.SetScale(0.5f, 5.0f, 0.5f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Cylinder 4" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -98.0f, -342.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 0.5f, 1.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Cylinder 5" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -98.0f, -343.0f);
            objectBase.InitLocalTransform.SetScale(0.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Switch" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.Material.TransparencyFactor = 0.5f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -87.6f, -335.0f);
            objectBase.InitLocalTransform.SetScale(1.5f, 2.0f, 1.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.EnableBreakRigidGroup = false;
            objectBase.EnableCollisionResponse = false;
            objectBase.EnableCursorInteraction = false;

            objectC = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Rotor" + instanceIndexName);
            objectC.MaxPreUpdateAngularVelocity = 10.0f;
            objectC.MaxPostUpdateAngularVelocity = 10.0f;
            objectRoot.AddChildPhysicsObject(objectC);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Rotor Cylinder 1" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Brass";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -98.0f, -344.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 0.5f, 1.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Rotor Box 1" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Brass";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(10.0f, -96.1f, -344.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 1.0f, 0.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45.0f)));
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Rotor Box 2" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Brass";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(11.6f, -98.9f, -344.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 1.0f, 0.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45.0f)) * Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(120.0f)));
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Boat 1 Engine Rotor Box 3" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Brass";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(8.4f, -98.9f, -344.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 1.0f, 0.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45.0f)) * Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(240.0f)));
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Boat 1 Constraint 1" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Boat 1 Engine Cylinder 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Boat 1 Body Box 5" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 0.3f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 0.3f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -15.0f;
            constraint.MaxLimitDegAngleY = 15.0f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Boat 1 Constraint 2" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Boat 1 Engine Rotor Cylinder 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Boat 1 Engine Cylinder 5" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.5f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.5f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableBreak = true;
            constraint.Update();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
