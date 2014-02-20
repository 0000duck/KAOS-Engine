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
    public class Box1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Box1(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, int maxPlank, Vector3 plankScale, float plankDistance)
        {
            Shape box = scene.Factory.ShapeManager.Find("Box");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Box 1" + instanceIndexName);

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 1 Plank Up " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(i * 2.0f * (plankScale.X + plankDistance) - maxPlank * (plankScale.X + plankDistance) + (plankScale.X + plankDistance), 0.0f, maxPlank * (plankScale.X + plankDistance) + plankScale.Z - plankDistance);
                objectBase.InitLocalTransform.SetScale(plankScale);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 1 Plank Down " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(i * 2.0f * (plankScale.X + plankDistance) - maxPlank * (plankScale.X + plankDistance) + (plankScale.X + plankDistance), 0.0f, -maxPlank * (plankScale.X + plankDistance) - plankScale.Z + plankDistance);
                objectBase.InitLocalTransform.SetScale(plankScale);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 1 Plank Right " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(maxPlank * (plankScale.X + plankDistance) + plankScale.Z - plankDistance, 0.0f, i * 2.0f * (plankScale.X + plankDistance) - maxPlank * (plankScale.X + plankDistance) + (plankScale.X + plankDistance));
                objectBase.InitLocalTransform.SetScale(plankScale.Z, plankScale.Y, plankScale.X);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 1 Plank Left " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(-maxPlank * (plankScale.X + plankDistance) - plankScale.Z + plankDistance, 0.0f, i * 2.0f * (plankScale.X + plankDistance) - maxPlank * (plankScale.X + plankDistance) + (plankScale.X + plankDistance));
                objectBase.InitLocalTransform.SetScale(plankScale.Z, plankScale.Y, plankScale.X);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 1 Plank Top " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(i * 2.0f * (plankScale.X + plankDistance) - maxPlank * (plankScale.X + plankDistance) + (plankScale.X + plankDistance), plankScale.Y + plankScale.Z - plankDistance, 0.0f);
                objectBase.InitLocalTransform.SetScale(plankScale.X, plankScale.Z, maxPlank * (plankScale.X + plankDistance) - plankDistance);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 1 Plank Bottom " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(i * 2.0f * (plankScale.X + plankDistance) - maxPlank * (plankScale.X + plankDistance) + (plankScale.X + plankDistance), -plankScale.Y - plankScale.Z + plankDistance, 0.0f);
                objectBase.InitLocalTransform.SetScale(plankScale.X, plankScale.Z, maxPlank * (plankScale.X + plankDistance) - plankDistance);
                objectBase.Integral.SetDensity(1.0f);
            }

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
