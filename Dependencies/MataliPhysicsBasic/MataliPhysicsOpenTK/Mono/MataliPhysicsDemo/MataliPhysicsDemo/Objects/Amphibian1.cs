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
    public class Amphibian1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Amphibian1(Demo demo, int instanceIndex)
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
            PhysicsObject objectC = null;

            Vector3 position1 = Vector3.Zero;
            Vector3 position2 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Amphibian 1" + instanceIndexName);
            objectRoot.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(0.0f)));
            objectRoot.InitLocalTransform.SetPosition(-10.0f, -7.5f, 20.0f);

            objectA = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Body" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectA);
            objectA.MaxPreUpdateLinearVelocity = 50.0f;
            objectA.MaxPostUpdateLinearVelocity = 50.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Body 1" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 11.8f, 0.0f);
            objectBase.InitLocalTransform.SetScale(2.0f, 0.4f, 2.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Body 2" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 10.7f, 0.0f);
            objectBase.InitLocalTransform.SetScale(6.5f, 0.7f, 10.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Body 3" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 8.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(7.0f, 2.0f, 10.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Body 4" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 4.5f, -0.5f);
            objectBase.InitLocalTransform.SetScale(5.0f, 1.5f, 10.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectB = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Turret" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectB);
            objectB.MaxPreUpdateAngularVelocity = 10.0f;
            objectB.MaxPostUpdateAngularVelocity = 10.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Turret Body Up" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 14.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(3.0f, 0.5f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Turret Body Down" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 13.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(4.0f, 1.0f, 4.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectC = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Turret Gun" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectC);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Turret Gun Control" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 13.0f, 3.3f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(1.0f, 2.0f, 1.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Turret Gun 1" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(-1.0f, 13.0f, 5.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 2.0f, 0.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Turret Gun 2" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(1.0f, 13.0f, 5.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 2.0f, 0.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Wheel 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.InitLocalTransform.SetPosition(-6.0f, 3.0f, 8.4f);
            objectBase.InitLocalTransform.SetScale(2.5f, 1.0f, 2.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Wheel 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.InitLocalTransform.SetPosition(-6.0f, 3.0f, 2.2f);
            objectBase.InitLocalTransform.SetScale(2.5f, 1.0f, 2.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Wheel 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.InitLocalTransform.SetPosition(-6.0f, 3.0f, -3.6f);
            objectBase.InitLocalTransform.SetScale(2.5f, 1.0f, 2.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Wheel 4" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.InitLocalTransform.SetPosition(-6.0f, 3.0f, -9.2f);
            objectBase.InitLocalTransform.SetScale(2.5f, 1.0f, 2.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Wheel 5" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.InitLocalTransform.SetPosition(6.0f, 3.0f, 8.4f);
            objectBase.InitLocalTransform.SetScale(2.5f, 1.0f, 2.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Wheel 6" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.InitLocalTransform.SetPosition(6.0f, 3.0f, 2.2f);
            objectBase.InitLocalTransform.SetScale(2.5f, 1.0f, 2.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Wheel 7" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.InitLocalTransform.SetPosition(6.0f, 3.0f, -3.6f);
            objectBase.InitLocalTransform.SetScale(2.5f, 1.0f, 2.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Amphibian 1 Wheel 8" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.InitLocalTransform.SetPosition(6.0f, 3.0f, -9.2f);
            objectBase.InitLocalTransform.SetScale(2.5f, 1.0f, 2.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectRoot.UpdateFromInitLocalTransform();

            objectBase = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Body Down" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Gun 1" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Gun 2" + instanceIndexName), true);

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Turret Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Body Down" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body 1" + instanceIndexName);
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

            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Turret Gun Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Gun Control" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Body Down" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MaxLimitDegAngleX = 45.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Wheel 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Wheel 1" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(-2.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(-2.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Wheel 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Wheel 2" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(-2.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(-2.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Wheel 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Wheel 3" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(-2.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(-2.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Wheel 4 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Wheel 4" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(-2.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(-2.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Wheel 5 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Wheel 5" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(2.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(2.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Wheel 6 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Wheel 6" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(2.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(2.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Wheel 7 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Wheel 7" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(2.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(2.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Amphibian 1 Wheel 8 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Wheel 8" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(2.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(2.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
