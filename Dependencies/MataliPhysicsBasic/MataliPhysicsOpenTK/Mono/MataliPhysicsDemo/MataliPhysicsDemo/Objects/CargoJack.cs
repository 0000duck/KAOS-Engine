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
    public class CargoJack
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public CargoJack(Demo demo, int instanceIndex)
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
            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape box = scene.Factory.ShapeManager.Find("Box");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            PhysicsObject objectA = null;
            PhysicsObject objectB = null;
            PhysicsObject objectC = null;
            PhysicsObject objectD = null;

            Vector3 position1 = Vector3.Zero;
            Vector3 position2 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Cargo Jack" + instanceIndexName);

            objectA = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Body" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectA);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Body Down" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-5.0f, 0.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(5.0f, 0.5f, 10.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Body Motor" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-5.0f, 1.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(2.0f, 0.5f, 2.0f);
            objectBase.Integral.SetDensity(10.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Motor" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-5.0f, 6.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 5.0f, 1.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectB = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Down" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-5.0f, 7.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.5f, 5.0f, 0.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Up" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 12.7f, 0.0f);
            objectBase.InitLocalTransform.SetScale(15.0f, 0.2f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Counterbalance" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-14.0f, 11.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 1.0f, 1.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectD = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Panel" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectD);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Panel Body" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-6.4f, 5.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 1.0f, 1.0f);
            objectBase.Integral.SetDensity(10.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Panel Down" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-6.2f, 3.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 2.0f, 0.1f);
            objectBase.Integral.SetDensity(10.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Panel Button" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.TwoSidedNormals = true;
            objectBase.Material.SetAmbient(0.4f, 0.7f, 0.4f);
            objectBase.Material.SetDiffuse(0.4f, 1.0f, 0.4f);
            objectBase.InitLocalTransform.SetPosition(-6.6f, 5.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 0.1f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.Integral.InertiaScaleFactor = 10.0f;
            objectBase.EnableBreakRigidGroup = false;
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Panel Button Switch" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.TransparencyFactor = 0.5f;
            objectBase.InitLocalTransform.SetPosition(-6.7f, 5.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.2f, 0.2f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.EnableCollisionResponse = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.EnableDrawing = false;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Panel Button Light" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = sphere;
            objectBase.UserDataStr = "Sphere";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetPosition(-6.6f, 5.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.CreateLight(true);
            objectBase.Light.Type = PhysicsLightType.Point;
            objectBase.Light.SetDiffuse(0.2f, 1.0f, 0.2f);
            objectBase.Light.Range = 2.0f;
            objectBase.EnableBreakRigidGroup = false;
            objectBase.EnableCollisions = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.EnableAddToCameraDrawTransparentPhysicsObjects = false;

            objectC = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Handle" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectC);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Handle Up" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(14.0f, 12.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 0.5f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Handle Middle Bottom" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(14.0f, 9.8f, 7.5f);
            objectBase.InitLocalTransform.SetScale(0.1f, 1.5f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Handle Middle" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(14.0f, 11.4f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 0.1f, 7.6f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Handle Middle Top" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(14.0f, 9.8f, -7.5f);
            objectBase.InitLocalTransform.SetScale(0.1f, 1.5f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Handle Bottom" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(14.0f, 8.2f, 7.5f);
            objectBase.InitLocalTransform.SetScale(7.6f, 0.1f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cargo Jack Arm Handle Top" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(14.0f, 8.2f, -7.5f);
            objectBase.InitLocalTransform.SetScale(7.6f, 0.1f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;

            constraint = scene.Factory.ConstraintManager.Create("Cargo Jack Constraint 1" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Panel Down" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Body Motor" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 2.0f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 2.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 150.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Cargo Jack Constraint 2" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Arm Motor" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Body Motor" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 5.0f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 5.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.EnableControlAngleY = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Cargo Jack Constraint 3" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Arm Down" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Arm Motor" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(0.0f, 5.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(0.0f, 5.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDistanceY = 0.0f;
            constraint.MaxLimitDistanceY = 9.0f;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.EnableControlDistanceY = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Cargo Jack Constraint 4" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Arm Handle Up" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Arm Up" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.5f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.5f, 0.0f));
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
