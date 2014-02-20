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
    public class Bridge1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Bridge1(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, int boardCount, Vector3 boardScale)
        {
            Shape box = scene.Factory.ShapeManager.Find("Box");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            Vector3 position1 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Bridge 1 " + instanceIndexName);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 1 Board 0" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood1";
            objectBase.InitLocalTransform.SetPosition(-boardCount * boardScale.X, 50.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(boardScale);

            for (int i = 1; i < boardCount - 1; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 1 Board " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.InitLocalTransform.SetPosition(-boardCount * boardScale.X + i * 2.0f * boardScale.X, 50.0f, 20.0f);
                objectBase.InitLocalTransform.SetScale(boardScale);
                objectBase.Integral.SetDensity(1.0f);
                objectBase.CreateSound(true);
            }

            objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 1 Board " + (boardCount - 1).ToString() + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood1";
            objectBase.InitLocalTransform.SetPosition(-boardCount * boardScale.X + (boardCount - 1) * 2.0f * boardScale.X, 50.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(boardScale);

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;

            for (int i = 0; i < boardCount - 1; i++)
            {
                constraint = scene.Factory.ConstraintManager.Create("Bridge 1 Constraint " + (i + 1).ToString() + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Bridge 1 Board " + i.ToString() + instanceIndexName);
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Bridge 1 Board " + (i + 1).ToString() + instanceIndexName);
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
                constraint.SetAnchor1(position1 + new Vector3(boardScale.X, 0.0f, 0.0f));
                constraint.SetAnchor2(position1 + new Vector3(boardScale.X, 0.0f, 0.0f));
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.SetInitWorldOrientation2(ref orientation2);
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableBreak = true;

                if (i == 0 || i == boardCount - 2)
                    constraint.AngularDamping = 0.1f;

                constraint.Update();
            }

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
