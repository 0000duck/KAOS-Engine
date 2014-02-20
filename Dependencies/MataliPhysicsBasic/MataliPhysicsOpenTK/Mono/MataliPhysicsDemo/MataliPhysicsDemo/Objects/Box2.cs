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
    public class Box2
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Box2(Demo demo, int instanceIndex)
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
            ShapePrimitive shapePrimitive = null;
            Shape shape = null;

            Vector3[] convexTab = new Vector3[8];
            convexTab[0] = new Vector3(-1.0f, -1.0f, 1.0f);
            convexTab[1] = new Vector3(-1.0f, 1.0f, 1.0f);
            convexTab[2] = new Vector3(1.0f, 0.752f, 1.0f);
            convexTab[3] = new Vector3(1.0f, -0.752f, 1.0f);
            convexTab[4] = new Vector3(-1.0f, -1.0f, -1.0f);
            convexTab[5] = new Vector3(-1.0f, 1.0f, -1.0f);
            convexTab[6] = new Vector3(1.0f, 0.752f, -1.0f);
            convexTab[7] = new Vector3(1.0f, -0.752f, -1.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Box2Convex1");
            shapePrimitive.CreateConvex(convexTab);

            shape = scene.Factory.ShapeManager.Create("Box2Convex1");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Box2Convex1"))
                demo.Meshes.Add("Box2Convex1", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, false, CullFaceMode.Back, false, false));
        }

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, int maxPlank, Vector3 plankScale, float plankDistance)
        {
            Shape box = scene.Factory.ShapeManager.Find("Box");
            Shape box2Convex1 = scene.Factory.ShapeManager.Find("Box2Convex1");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Box 2" + instanceIndexName);

            Vector3 plankScale1 = plankScale;
            Vector3 plankScale2 = plankScale;
            plankScale2.X *= 0.5f;

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Plank Up " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood2";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(i * 2.0f * (plankScale1.X + plankDistance) - maxPlank * (plankScale1.X + plankDistance) + (plankScale1.X + plankDistance), 0.0f, maxPlank * (plankScale1.X + plankDistance) - plankScale1.Z - plankDistance);
                objectBase.InitLocalTransform.SetScale(plankScale1);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Plank Down " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood2";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(i * 2.0f * (plankScale1.X + plankDistance) - maxPlank * (plankScale1.X + plankDistance) + (plankScale1.X + plankDistance), 0.0f, -maxPlank * (plankScale1.X + plankDistance) + plankScale1.Z + plankDistance);
                objectBase.InitLocalTransform.SetScale(plankScale1);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Plank Right " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood2";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(maxPlank * (plankScale1.X + plankDistance) + plankScale1.Z - plankDistance, 0.0f, i * 2.0f * (plankScale1.X + plankDistance) - maxPlank * (plankScale1.X + plankDistance) + (plankScale1.X + plankDistance));
                objectBase.InitLocalTransform.SetScale(plankScale1.Z, plankScale1.Y, plankScale1.X);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Plank Left " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood2";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(-maxPlank * (plankScale1.X + plankDistance) - plankScale1.Z + plankDistance, 0.0f, i * 2.0f * (plankScale1.X + plankDistance) - maxPlank * (plankScale1.X + plankDistance) + (plankScale1.X + plankDistance));
                objectBase.InitLocalTransform.SetScale(plankScale1.Z, plankScale1.Y, plankScale1.X);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Plank Top " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood2";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(i * 2.0f * (plankScale1.X + plankDistance) - maxPlank * (plankScale1.X + plankDistance) + (plankScale1.X + plankDistance), plankScale1.Y - plankScale1.Z - plankDistance, 0.0f);
                objectBase.InitLocalTransform.SetScale(plankScale1.X, plankScale1.Z, maxPlank * (plankScale1.X + plankDistance) - plankDistance);
                objectBase.Integral.SetDensity(1.0f);
            }

            for (int i = 0; i < maxPlank; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Plank Bottom " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood2";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
                objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
                objectBase.CreateSound(true);
                objectBase.InitLocalTransform.SetPosition(i * 2.0f * (plankScale1.X + plankDistance) - maxPlank * (plankScale1.X + plankDistance) + (plankScale1.X + plankDistance), -plankScale1.Y + plankScale1.Z + plankDistance, 0.0f);
                objectBase.InitLocalTransform.SetScale(plankScale1.X, plankScale1.Z, maxPlank * (plankScale1.X + plankDistance) - plankDistance);
                objectBase.Integral.SetDensity(1.0f);
            }

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Up 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-maxPlank * (plankScale1.X + plankDistance) + plankScale2.X + plankDistance, 0.0f, maxPlank * (plankScale1.X + plankDistance) + plankScale2.Z - plankDistance);
            objectBase.InitLocalTransform.SetScale(ref plankScale2);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Up 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(maxPlank * (plankScale1.X + plankDistance) - plankScale2.X - plankDistance, 0.0f, maxPlank * (plankScale1.X + plankDistance) + plankScale2.Z - plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(180.0f)));
            objectBase.InitLocalTransform.SetScale(ref plankScale2);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Up 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, plankScale1.Y - plankScale2.X, maxPlank * (plankScale1.X + plankDistance) + plankScale2.Z - plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Up 4" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, -plankScale1.Y + plankScale2.X, maxPlank * (plankScale1.X + plankDistance) + plankScale2.Z - plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Down 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-maxPlank * (plankScale1.X + plankDistance) + plankScale2.X + plankDistance, 0.0f, -maxPlank * (plankScale1.X + plankDistance) - plankScale2.Z + plankDistance);
            objectBase.InitLocalTransform.SetScale(ref plankScale2);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Down 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(maxPlank * (plankScale1.X + plankDistance) - plankScale2.X - plankDistance, 0.0f, -maxPlank * (plankScale1.X + plankDistance) - plankScale2.Z + plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(180.0f)));
            objectBase.InitLocalTransform.SetScale(ref plankScale2);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Down 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, plankScale1.Y - plankScale2.X, -maxPlank * (plankScale1.X + plankDistance) - plankScale2.Z + plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Down 4" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, -plankScale1.Y + plankScale2.X, -maxPlank * (plankScale1.X + plankDistance) - plankScale2.Z + plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Top 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-maxPlank * (plankScale1.X + plankDistance) + plankScale2.X + plankDistance, plankScale1.Y + plankScale2.Z, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Top 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(maxPlank * (plankScale1.X + plankDistance) - plankScale2.X - plankDistance, plankScale1.Y + plankScale2.Z, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)) * Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(180.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Top 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, plankScale1.Y + plankScale2.Z, maxPlank * (plankScale1.X + plankDistance) - plankScale2.X - plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)) * Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Top 4" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, plankScale1.Y + plankScale2.Z, -maxPlank * (plankScale1.X + plankDistance) + plankScale2.X + plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)) * Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Bottom 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-maxPlank * (plankScale1.X + plankDistance) + plankScale2.X + plankDistance, -plankScale1.Y - plankScale2.Z, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Bottom 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(maxPlank * (plankScale1.X + plankDistance) - plankScale2.X - plankDistance, -plankScale1.Y - plankScale2.Z, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)) * Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(180.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Bottom 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, -plankScale1.Y - plankScale2.Z, maxPlank * (plankScale1.X + plankDistance) - plankScale2.X - plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)) * Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 2 Convex Plank Bottom 4" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box2Convex1;
            objectBase.UserDataStr = "Box2Convex1";
            objectBase.Material.UserDataStr = "Wood2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, -plankScale1.Y - plankScale2.Z, -maxPlank * (plankScale1.X + plankDistance) + plankScale2.X + plankDistance);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)) * Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(plankScale2.X, maxPlank * (plankScale1.X + plankDistance) - plankDistance, plankScale2.Z);
            objectBase.Integral.SetDensity(1.0f);

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
