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
    public class Crab1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Crab1(Demo demo, int instanceIndex)
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

            Vector3[] convexTab1 = new Vector3[12];
            convexTab1[0] = new Vector3(-0.1f, -1.0f, -1.0f);
            convexTab1[1] = new Vector3(-1.0f, -1.0f, -0.5f);
            convexTab1[2] = new Vector3(-1.0f, -1.0f, 0.5f);
            convexTab1[3] = new Vector3(-0.1f, -1.0f, 1.0f);
            convexTab1[4] = new Vector3(0.1f, -1.0f, 1.0f);
            convexTab1[5] = new Vector3(1.0f, -1.0f, 0.7f);
            convexTab1[6] = new Vector3(1.0f, -1.0f, -0.7f);
            convexTab1[7] = new Vector3(0.1f, -1.0f, -1.0f);
            convexTab1[8] = new Vector3(0.0f, 1.0f, -0.2f);
            convexTab1[9] = new Vector3(-0.2f, 1.0f, 0.0f);
            convexTab1[10] = new Vector3(0.0f, 1.0f, 0.2f);
            convexTab1[11] = new Vector3(0.2f, 1.0f, 0.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Crab1Convex1");
            shapePrimitive.CreateConvex(convexTab1);

            shape = scene.Factory.ShapeManager.Create("Crab1Convex1");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape crab1Convex1 = scene.Factory.ShapeManager.Find("Crab1Convex1");

            shape = scene.Factory.ShapeManager.Create("Crab1ConvexUp");
            shape.Add(crab1Convex1, Matrix4.CreateScale(1.0f, 0.2f, 1.0f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.CreateScale(1.5f, 0.5f, 1.5f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Crab1ConvexUp"))
                demo.Meshes.Add("Crab1ConvexUp", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

            convexTab1[0] = new Vector3(-0.8f, -1.0f, -1.0f);
            convexTab1[1] = new Vector3(-1.0f, -1.0f, -0.8f);
            convexTab1[2] = new Vector3(-1.0f, -1.0f, 0.8f);
            convexTab1[3] = new Vector3(-0.8f, -1.0f, 1.0f);
            convexTab1[4] = new Vector3(0.8f, -1.0f, 1.0f);
            convexTab1[5] = new Vector3(1.0f, -1.0f, 0.8f);
            convexTab1[6] = new Vector3(1.0f, -1.0f, -0.8f);
            convexTab1[7] = new Vector3(0.8f, -1.0f, -1.0f);
            convexTab1[8] = new Vector3(-1.0f, 1.0f, -0.2f);
            convexTab1[9] = new Vector3(-1.0f, 1.0f, 0.2f);
            convexTab1[10] = new Vector3(0.2f, 1.0f, 0.2f);
            convexTab1[11] = new Vector3(0.2f, 1.0f, -0.2f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Crab1Convex2");
            shapePrimitive.CreateConvex(convexTab1);

            shape = scene.Factory.ShapeManager.Create("Crab1Convex2");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            Shape crab1Convex2 = scene.Factory.ShapeManager.Find("Crab1Convex2");

            shape = scene.Factory.ShapeManager.Create("Crab1ConvexDown");
            shape.Add(crab1Convex2, Matrix4.CreateScale(1.0f, 0.2f, 1.0f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.CreateScale(1.5f, 0.5f, 1.5f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Crab1ConvexDown"))
                demo.Meshes.Add("Crab1ConvexDown", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

            convexTab1[0] = new Vector3(-4.0f, -6.0f, 1.0f);
            convexTab1[1] = new Vector3(0.0f, -4.0f, 0.5f);
            convexTab1[2] = new Vector3(1.0f, -1.0f, 0.2f);
            convexTab1[3] = new Vector3(1.0f, -1.0f, -0.2f);
            convexTab1[4] = new Vector3(0.0f, -4.0f, -0.5f);
            convexTab1[5] = new Vector3(-4.0f, -6.0f, -1.0f);
            convexTab1[6] = new Vector3(-4.0f, 6.0f, 1.0f);
            convexTab1[7] = new Vector3(0.0f, 4.0f, 0.5f);
            convexTab1[8] = new Vector3(1.0f, 1.0f, 0.2f);
            convexTab1[9] = new Vector3(1.0f, 1.0f, -0.2f);
            convexTab1[10] = new Vector3(0.0f, 4.0f, -0.5f);
            convexTab1[11] = new Vector3(-4.0f, 6.0f, -1.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Crab1Convex3");
            shapePrimitive.CreateConvex(convexTab1);

            shape = scene.Factory.ShapeManager.Create("Crab1Convex3");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            Shape crab1Convex3 = scene.Factory.ShapeManager.Find("Crab1Convex3");

            shape = scene.Factory.ShapeManager.Create("Crab1ConvexTop");
            shape.Add(crab1Convex3, Matrix4.CreateScale(1.0f, 0.2f, 1.0f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.CreateScale(1.5f, 0.5f, 1.5f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Crab1ConvexTop"))
                demo.Meshes.Add("Crab1ConvexTop", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));
        }

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation)
        {
            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");
            Shape crab1ConvexUp = scene.Factory.ShapeManager.Find("Crab1ConvexUp");
            Shape crab1ConvexDown = scene.Factory.ShapeManager.Find("Crab1ConvexDown");
            Shape crab1ConvexTop = scene.Factory.ShapeManager.Find("Crab1ConvexTop");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            PhysicsObject objectA = null;

            Vector3 position1 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Crab 1" + instanceIndexName);

            objectA = scene.Factory.PhysicsObjectManager.Create("Crab 1 Shell" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectA);
            objectA.FluidPressureFactor = 0.4f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Front Shell" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = crab1ConvexTop;
            objectBase.UserDataStr = "Crab1ConvexTop";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(2.5f, -0.3f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 0.2f, 0.4f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.Integral.SetDensity(1.0f);
            objectBase.FluidPressureFactor = 0.4f;
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "RollSlide";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Upper Shell" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = crab1ConvexUp;
            objectBase.UserDataStr = "Crab1ConvexUp";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 0.0f, 0.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.Integral.SetDensity(0.5f);
            objectBase.FluidPressureFactor = 0.4f;
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "RollSlide";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Lower Shell" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = crab1ConvexDown;
            objectBase.UserDataStr = "Crab1ConvexDown";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.2f, -0.7f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.9f, 1.0f, 0.6f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(180.0f)));
            objectBase.EnableBreakRigidGroup = false;
            objectBase.Integral.SetDensity(0.5f);
            objectBase.FluidPressureFactor = 0.4f;
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "RollSlide";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Eye" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(2.6f, 0.0f, -0.8f);
            objectBase.InitLocalTransform.SetScale(0.1f, 0.2f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(45.0f)));
            objectBase.Integral.InertiaScaleFactor = 3.0f;
            objectBase.Integral.SetDensity(1.0f);
            objectBase.MinResponseAngularVelocity = 0.05f;
            objectBase.MinResponseLinearVelocity = 0.05f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Eye" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(2.6f, 0.0f, 0.8f);
            objectBase.InitLocalTransform.SetScale(0.1f, 0.2f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            objectBase.Integral.InertiaScaleFactor = 3.0f;
            objectBase.Integral.SetDensity(1.0f);
            objectBase.MinResponseAngularVelocity = 0.05f;
            objectBase.MinResponseLinearVelocity = 0.05f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Front 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.6f, -1.2f, -1.5f);
            objectBase.InitLocalTransform.SetScale(0.25f, 0.8f, 0.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Front 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.6f, -1.2f, -3.1f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.8f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Front 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.6f, -1.2f, -4.3f);
            objectBase.InitLocalTransform.SetScale(0.15f, 0.4f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinFirstImpactForce = 5.0f;
            objectBase.Sound.MinNextImpactForce = 100000.0f;
            objectBase.Sound.RollVolume = 0.05f;
            objectBase.Sound.SlideVolume = 0.05f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Middle Front 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.0f, -1.2f, -1.5f);
            objectBase.InitLocalTransform.SetScale(0.25f, 0.8f, 0.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Middle Front 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.0f, -1.2f, -3.1f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.8f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Middle Front 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.0f, -1.2f, -4.3f);
            objectBase.InitLocalTransform.SetScale(0.15f, 0.4f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinFirstImpactForce = 5.0f;
            objectBase.Sound.MinNextImpactForce = 100000.0f;
            objectBase.Sound.RollVolume = 0.05f;
            objectBase.Sound.SlideVolume = 0.05f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Middle Back 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-0.6f, -1.2f, -1.5f);
            objectBase.InitLocalTransform.SetScale(0.25f, 0.8f, 0.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Middle Back 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-0.6f, -1.2f, -3.1f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.8f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Middle Back 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-0.6f, -1.2f, -4.3f);
            objectBase.InitLocalTransform.SetScale(0.15f, 0.4f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinFirstImpactForce = 5.0f;
            objectBase.Sound.MinNextImpactForce = 100000.0f;
            objectBase.Sound.RollVolume = 0.05f;
            objectBase.Sound.SlideVolume = 0.05f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Back 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-1.2f, -1.2f, -1.5f);
            objectBase.InitLocalTransform.SetScale(0.25f, 0.8f, 0.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Back 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-1.2f, -1.2f, -3.1f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.8f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Right Limb Back 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-1.2f, -1.2f, -4.3f);
            objectBase.InitLocalTransform.SetScale(0.15f, 0.4f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinFirstImpactForce = 5.0f;
            objectBase.Sound.MinNextImpactForce = 100000.0f;
            objectBase.Sound.RollVolume = 0.05f;
            objectBase.Sound.SlideVolume = 0.05f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Front 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.6f, -1.2f, 1.5f);
            objectBase.InitLocalTransform.SetScale(0.25f, 0.8f, 0.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Front 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.6f, -1.2f, 3.1f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.8f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Front 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.6f, -1.2f, 4.3f);
            objectBase.InitLocalTransform.SetScale(0.15f, 0.4f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinFirstImpactForce = 5.0f;
            objectBase.Sound.MinNextImpactForce = 100000.0f;
            objectBase.Sound.RollVolume = 0.05f;
            objectBase.Sound.SlideVolume = 0.05f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Middle Front 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.0f, -1.2f, 1.5f);
            objectBase.InitLocalTransform.SetScale(0.25f, 0.8f, 0.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Middle Front 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.0f, -1.2f, 3.1f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.8f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Middle Front 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(0.0f, -1.2f, 4.3f);
            objectBase.InitLocalTransform.SetScale(0.15f, 0.4f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinFirstImpactForce = 5.0f;
            objectBase.Sound.MinNextImpactForce = 100000.0f;
            objectBase.Sound.RollVolume = 0.05f;
            objectBase.Sound.SlideVolume = 0.05f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Middle Back 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-0.6f, -1.2f, 1.5f);
            objectBase.InitLocalTransform.SetScale(0.25f, 0.8f, 0.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Middle Back 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-0.6f, -1.2f, 3.1f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.8f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Middle Back 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-0.6f, -1.2f, 4.3f);
            objectBase.InitLocalTransform.SetScale(0.15f, 0.4f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinFirstImpactForce = 5.0f;
            objectBase.Sound.MinNextImpactForce = 100000.0f;
            objectBase.Sound.RollVolume = 0.05f;
            objectBase.Sound.SlideVolume = 0.05f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Back 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-1.2f, -1.2f, 1.5f);
            objectBase.InitLocalTransform.SetScale(0.25f, 0.8f, 0.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Back 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-1.2f, -1.2f, 3.1f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.8f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(10.0f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Crab 1 Left Limb Back 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.InitLocalTransform.SetPosition(-1.2f, -1.2f, 4.3f);
            objectBase.InitLocalTransform.SetScale(0.15f, 0.4f, 0.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinFirstImpactForce = 5.0f;
            objectBase.Sound.MinNextImpactForce = 100000.0f;
            objectBase.Sound.RollVolume = 0.05f;
            objectBase.Sound.SlideVolume = 0.05f;

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Eye 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Eye" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Front Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(orientation1 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            constraint.SetInitWorldOrientation2(orientation2 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Eye 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Eye" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Upper Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(orientation1 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            constraint.SetInitWorldOrientation2(orientation2 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Eye 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Eye" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Front Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(orientation1 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            constraint.SetInitWorldOrientation2(orientation2 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Eye 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Eye" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Upper Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(orientation1 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            constraint.SetInitWorldOrientation2(orientation2 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Front 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Front 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Lower Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -50.0f;
            constraint.MaxLimitDegAngleY = 90.0f;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 30.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 20.0f;
            constraint.ControlDegAngleY = 20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Front 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Front 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Front 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 150.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 50.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Front 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Front 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Front 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.4f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.4f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 150.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 50.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Middle Front 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Front 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Lower Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -50.0f;
            constraint.MaxLimitDegAngleY = 50.0f;
            constraint.MinLimitDegAngleX = -10.0f;
            constraint.MaxLimitDegAngleX = 30.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 10.0f;
            constraint.ControlDegAngleY = 0.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Middle Front 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Front 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Front 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 150.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Middle Front 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Front 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Front 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.4f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.4f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 150.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Middle Back 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Back 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Lower Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -50.0f;
            constraint.MaxLimitDegAngleY = 50.0f;
            constraint.MinLimitDegAngleX = -10.0f;
            constraint.MaxLimitDegAngleX = 30.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 10.0f;
            constraint.ControlDegAngleY = 0.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Middle Back 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Back 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Back 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 150.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Middle Back 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Back 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Middle Back 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.4f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.4f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 150.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Back 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Back 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Lower Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -50.0f;
            constraint.MaxLimitDegAngleY = 50.0f;
            constraint.MinLimitDegAngleX = -10.0f;
            constraint.MaxLimitDegAngleX = 30.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 10.0f;
            constraint.ControlDegAngleY = 0.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Back 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Back 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Back 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 150.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Right Limb Back 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Back 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Right Limb Back 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.4f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.4f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 150.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = 20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Front 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Front 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Lower Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -90.0f;
            constraint.MaxLimitDegAngleY = 50.0f;
            constraint.MinLimitDegAngleX = -30.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -20.0f;
            constraint.ControlDegAngleY = -20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Front 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Front 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Front 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -150.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -50.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Front 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Front 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Front 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.4f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.4f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -150.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -50.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Middle Front 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Front 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Lower Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -50.0f;
            constraint.MaxLimitDegAngleY = 50.0f;
            constraint.MinLimitDegAngleX = -30.0f;
            constraint.MaxLimitDegAngleX = 10.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -10.0f;
            constraint.ControlDegAngleY = 0.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Middle Front 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Front 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Front 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -150.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Middle Front 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Front 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Front 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.4f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.4f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -150.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Middle Back 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Back 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Lower Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -50.0f;
            constraint.MaxLimitDegAngleY = 50.0f;
            constraint.MinLimitDegAngleX = -30.0f;
            constraint.MaxLimitDegAngleX = 10.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -10.0f;
            constraint.ControlDegAngleY = 0.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Middle Back 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Back 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Back 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -150.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Middle Back 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Back 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Middle Back 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.4f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.4f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -150.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Back 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Back 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Lower Shell" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -50.0f;
            constraint.MaxLimitDegAngleY = 50.0f;
            constraint.MinLimitDegAngleX = -30.0f;
            constraint.MaxLimitDegAngleX = 10.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -10.0f;
            constraint.ControlDegAngleY = 0.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Back 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Back 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Back 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.8f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -150.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Crab 1 Left Limb Back 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Back 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Crab 1 Left Limb Back 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.4f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.4f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -150.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableControlAngleX = true;
            constraint.EnableControlAngleY = true;
            constraint.EnableControlAngleZ = true;
            constraint.ControlDegAngleX = -20.0f;
            constraint.LimitAngleForce = 1.5f;
            constraint.Update();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
