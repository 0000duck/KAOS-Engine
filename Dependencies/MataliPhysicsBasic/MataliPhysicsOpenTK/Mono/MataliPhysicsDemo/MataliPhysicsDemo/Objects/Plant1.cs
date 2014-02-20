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
    public class Plant1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        Vector3 position1;
        Quaternion orientation1;
        Quaternion orientation2;

        public Plant1(Demo demo, int instanceIndex)
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
            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Plant" + instanceIndexName);

            string leafInstanceIndexName1 = " 1";
            string leafInstanceIndexName2 = " 2";
            string leafInstanceIndexName3 = " 3";
            string leafInstanceIndexName4 = " 4";

            int trunkCount = 8;
            Vector3 trunkScale = new Vector3(1.0f, 5.0f, 1.0f);

            int leafCount = 4;
            Vector3 leafScale = new Vector3(4.0f, 0.1f, 1.0f);

            CreateTrunk(scene, instanceIndexName, trunkCount, trunkScale, Vector3.Zero, Vector3.One, Quaternion.Identity);
            CreateLeaf(scene, instanceIndexName, leafInstanceIndexName1, trunkCount, trunkScale, leafCount, leafScale, new Vector3(0.0f, 0.0f, 0.0f), Vector3.One, Quaternion.Identity);
            CreateLeaf(scene, instanceIndexName, leafInstanceIndexName2, trunkCount, trunkScale, leafCount, leafScale, new Vector3(0.0f, 0.0f, 0.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(90.0f)));
            CreateLeaf(scene, instanceIndexName, leafInstanceIndexName3, trunkCount, trunkScale, leafCount, leafScale, new Vector3(0.0f, 0.0f, 0.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(180.0f)));
            CreateLeaf(scene, instanceIndexName, leafInstanceIndexName4, trunkCount, trunkScale, leafCount, leafScale, new Vector3(0.0f, 0.0f, 0.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(270.0f)));

            objectBase = scene.Factory.PhysicsObjectManager.Find("Plant Trunk" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName1 + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName2 + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName3 + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName4 + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Leaf Constraint" + leafInstanceIndexName1 + leafCount.ToString() + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName1 + (leafCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Trunk " + (trunkCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleZ = -10.0f;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 200.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("LeafC Constraint" + leafInstanceIndexName2 + leafCount.ToString() + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName2 + (leafCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Trunk " + (trunkCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, leafScale.X));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, leafScale.X));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MaxLimitDegAngleX = 10.0f;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 200.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Leaf Constraint" + leafInstanceIndexName3 + leafCount.ToString() + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName3 + (leafCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Trunk " + (trunkCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(leafScale.X, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(leafScale.X, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleZ = -10.0f;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 200.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("LeafC Constraint" + leafInstanceIndexName4 + leafCount.ToString() + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName4 + (leafCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Trunk " + (trunkCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 0.0f, leafScale.X));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 0.0f, leafScale.X));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MaxLimitDegAngleX = 10.0f;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 200.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Leaf Constraint" + leafInstanceIndexName1 + leafInstanceIndexName2 + leafCount.ToString() + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName1 + (leafCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName2 + (leafCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 200.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Leaf Constraint" + leafInstanceIndexName3 + leafInstanceIndexName4 + leafCount.ToString() + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName3 + (leafCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName4 + (leafCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, leafScale.X));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, leafScale.X));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 200.0f;
            constraint.Update();

            PhysicsObject objectA = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName1 + (leafCount - 1).ToString() + instanceIndexName);
            PhysicsObject objectB = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName2 + (leafCount - 1).ToString() + instanceIndexName);
            PhysicsObject objectC = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName3 + (leafCount - 1).ToString() + instanceIndexName);
            PhysicsObject objectD = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName4 + (leafCount - 1).ToString() + instanceIndexName);

            if (objectA != null)
            {
                objectA.DisableCollision(objectB, true);
                objectA.DisableCollision(objectC, true);
                objectA.DisableCollision(objectD, true);
            }

            if (objectB != null)
            {
                objectB.DisableCollision(objectC, true);
                objectB.DisableCollision(objectD, true);
            }

            if (objectC != null)
                objectC.DisableCollision(objectD, true);

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            objectRoot.UpdateFromInitLocalTransform();

            constraint = scene.Factory.ConstraintManager.Create("Trunk Constraint " + trunkCount.ToString() + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Trunk 0" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Quad  1");
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 0.5f * trunkScale.Y, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 0.5f * trunkScale.Y, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 30.0f;
            constraint.Update();

            scene.UpdateFromInitLocalTransform(objectRoot);
        }

        void CreateTrunk(PhysicsScene scene, string instanceIndexName, int trunkCount, Vector3 trunkScale, Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation)
        {
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Plant Trunk" + instanceIndexName);

            for (int i = 0; i < trunkCount; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Plant Trunk " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                //objectBase.Material.RigidGroup = true;
                objectBase.Shape = cylinderY;
                objectBase.UserDataStr = "CylinderY";
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(new Vector3(0.0f, 0.5f * trunkScale.Y + i * trunkScale.Y, 0.0f) + objectPosition);
                objectBase.InitLocalTransform.SetScale(trunkScale.X * 0.1f + 0.1f * (trunkCount - i), 0.5f * trunkScale.Y, trunkScale.Z * 0.1f + 0.1f * (trunkCount - i));
                objectBase.Integral.SetDensity(10.0f);
            }

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;
            for (int i = 0; i < trunkCount - 1; i++)
            {
                constraint = scene.Factory.ConstraintManager.Create("Trunk Constraint " + i.ToString() + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Trunk " + i.ToString() + instanceIndexName);
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Trunk " + (i + 1).ToString() + instanceIndexName);
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
                constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.5f * trunkScale.Y, 0.0f));
                constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.5f * trunkScale.Y, 0.0f));
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.SetInitWorldOrientation2(ref orientation2);
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.EnableBreak = true;
                constraint.MinBreakVelocity = 200.0f;
                constraint.Update();
            }

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            objectRoot.UpdateFromInitLocalTransform();
        }

        void CreateLeaf(PhysicsScene scene, string instanceIndexName, string leafInstanceIndexName, int trunkCount, Vector3 trunkScale, int leafCount, Vector3 leafScale, Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation)
        {
            Shape box = scene.Factory.ShapeManager.Find("Box");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Plant Leaf" + leafInstanceIndexName + instanceIndexName);

            for (int i = 0; i < leafCount; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Plant Leaf" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Leaf";
                objectBase.InitLocalTransform.SetPosition(new Vector3(-leafScale.X * 2.0f * leafCount + leafScale.X + i * 2.0f * leafScale.X, trunkCount * trunkScale.Y, 0.0f) + objectPosition);
                objectBase.InitLocalTransform.SetScale(leafScale.X, leafScale.Y, leafScale.Z + (float)Math.Tan(1.0 / (leafCount - i + 1.0) - 0.9));
                objectBase.Integral.SetDensity(0.1f);
            }

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;
            for (int i = 0; i < leafCount - 1; i++)
            {
                constraint = scene.Factory.ConstraintManager.Create("Leaf Constraint" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName + (i + 1).ToString() + instanceIndexName);
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
                constraint.SetAnchor1(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
                constraint.SetAnchor2(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.SetInitWorldOrientation2(ref orientation2);
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.MinLimitDegAngleZ = -10.0f;
                constraint.EnableBreak = true;
                constraint.MinBreakVelocity = 300.0f;
                constraint.LimitAngleForce = 0.5f;
                constraint.Update();
            }

            for (int i = 0; i < leafCount - 1; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Plant Leaf Sub A" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Leaf";
                objectBase.InitLocalTransform.SetPosition(new Vector3(-leafScale.X * 2.0f * leafCount + leafScale.X + i * 2.0f * leafScale.X, trunkCount * trunkScale.Y, -1.2f + (float)Math.Exp(10.0 / (leafCount - i + 5.8f))) + objectPosition);
                objectBase.InitLocalTransform.SetScale(leafScale.Z + (float)Math.Tan(1.0f / (leafCount - i + 1.0) - 0.5), leafScale.Y, leafScale.X);
                objectBase.InitLocalTransform.SetRotation(Matrix4.CreateFromAxisAngle(Vector3.UnitY, -MathHelper.DegreesToRadians(45.0f + (leafCount - i) * 6.0f)));
                objectBase.Integral.SetDensity(0.001f);
            }

            for (int i = 0; i < leafCount - 1; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Plant Leaf Sub B" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Leaf";
                objectBase.InitLocalTransform.SetPosition(new Vector3(-leafScale.X * 2.0f * leafCount + leafScale.X + i * 2.0f * leafScale.X, trunkCount * trunkScale.Y, 1.2f - (float)Math.Exp(10.0 / (leafCount - i + 5.8f))) + objectPosition);
                objectBase.InitLocalTransform.SetScale(leafScale.Z + (float)Math.Tan(1.0f / (leafCount - i + 1.0) - 0.5), leafScale.Y, leafScale.X);
                objectBase.InitLocalTransform.SetRotation(Matrix4.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45.0f + (leafCount - i) * 6.0f)));
                objectBase.Integral.SetDensity(0.001f);
            }

            objectRoot.UpdateFromInitLocalTransform();

            for (int i = 0; i < leafCount - 1; i++)
            {
                constraint = scene.Factory.ConstraintManager.Create("Leaf Constraint Sub A" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf Sub A" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
                constraint.SetAnchor1(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
                constraint.SetAnchor2(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.SetInitWorldOrientation2(ref orientation2);
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.MinLimitDegAngleZ = -10.0f;
                constraint.EnableBreak = true;
                constraint.MinBreakVelocity = 400.0f;
                constraint.LimitAngleForce = 0.5f;
                constraint.Update();
            }

            for (int i = 0; i < leafCount - 1; i++)
            {
                constraint = scene.Factory.ConstraintManager.Create("Leaf Constraint Sub B" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf Sub B" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Plant Leaf" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
                constraint.SetAnchor1(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
                constraint.SetAnchor2(position1 + new Vector3(leafScale.X, 0.0f, 0.0f));
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.SetInitWorldOrientation2(ref orientation2);
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.MinLimitDegAngleZ = -10.0f;
                constraint.EnableBreak = true;
                constraint.MinBreakVelocity = 400.0f;
                constraint.LimitAngleForce = 0.5f;
                constraint.Update();
            }

            PhysicsObject objectA = null;
            PhysicsObject objectB = null;

            for (int i = 0; i < leafCount - 1; i++)
            {
                objectA = scene.Factory.PhysicsObjectManager.Find("Plant Leaf Sub A" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                objectB = scene.Factory.PhysicsObjectManager.Find("Plant Leaf Sub B" + leafInstanceIndexName + i.ToString() + instanceIndexName);
                objectA.DisableCollision(objectB, true);
            }

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            objectRoot.UpdateFromInitLocalTransform();
        }
    }
}
