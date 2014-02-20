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
    public class Helicopter1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Helicopter1(Demo demo, int instanceIndex)
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
            TriangleMesh triangleMesh = null;
            ShapePrimitive shapePrimitive = null;
            Shape shape = null;

            Vector3 point1, point2, point3;
            point1 = new Vector3(1.0f, -1.0f, 0.0f);
            point2 = new Vector3(-1.0f, -1.0f, 0.0f);
            point3 = new Vector3(-1.0f, 1.0f, 0.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Helicopter1DoorTriangle1");
            triangleMesh.CreateTriangle(point1, point2, point3);
            if (!demo.Meshes.ContainsKey("Helicopter1DoorTriangle1"))
                demo.Meshes.Add("Helicopter1DoorTriangle1", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, true, CullFaceMode.FrontAndBack, false, true));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1DoorTriangle1");
            shapePrimitive.CreateTriangle(point1, point2, point3);

            shape = scene.Factory.ShapeManager.Create("Helicopter1DoorTriangle1");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.01f);

            point1 = new Vector3(-1.0f, 1.0f, 0.0f);
            point2 = new Vector3(1.0f, 1.0f, 0.0f);
            point3 = new Vector3(1.0f, -1.0f, 0.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Helicopter1DoorTriangle2");
            triangleMesh.CreateTriangle(point1, point2, point3);
            if (!demo.Meshes.ContainsKey("Helicopter1DoorTriangle2"))
                demo.Meshes.Add("Helicopter1DoorTriangle2", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, true, CullFaceMode.FrontAndBack, false, true));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1DoorTriangle2");
            shapePrimitive.CreateTriangle(point1, point2, point3);

            shape = scene.Factory.ShapeManager.Create("Helicopter1DoorTriangle2");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.01f);

            Vector3[] convexTab1 = new Vector3[12];
            convexTab1[0] = new Vector3(-1.1f, 1.0f, -1.0f);
            convexTab1[1] = new Vector3(-1.1f, 1.0f, 1.0f);
            convexTab1[2] = new Vector3(-0.3f, 1.0f, 1.0f);
            convexTab1[3] = new Vector3(0.7f, 0.5f, 0.5f);
            convexTab1[4] = new Vector3(0.7f, 0.5f, -0.5f);
            convexTab1[5] = new Vector3(-0.3f, 1.0f, -1.0f);
            convexTab1[6] = new Vector3(-1.0f, -1.0f, -1.0f);
            convexTab1[7] = new Vector3(-1.0f, -1.0f, 1.0f);
            convexTab1[8] = new Vector3(0.0f, -1.0f, 1.0f);
            convexTab1[9] = new Vector3(1.0f, -1.0f, 0.5f);
            convexTab1[10] = new Vector3(1.0f, -1.0f, -0.5f);
            convexTab1[11] = new Vector3(0.0f, -1.0f, -1.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1ConvexUp");
            shapePrimitive.CreateConvex(convexTab1);

            shape = scene.Factory.ShapeManager.Create("Helicopter1ConvexUp");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Helicopter1ConvexUp"))
                demo.Meshes.Add("Helicopter1ConvexUp", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, false, CullFaceMode.Back, false, false));

            Vector3[] convexTab2 = new Vector3[8];
            convexTab2[0] = new Vector3(-1.0f, 1.0f, -1.0f);
            convexTab2[1] = new Vector3(-1.0f, 1.0f, 1.0f);
            convexTab2[2] = new Vector3(1.0f, 0.5f, 0.5f);
            convexTab2[3] = new Vector3(1.0f, 0.5f, -0.5f);
            convexTab2[4] = new Vector3(-1.1f, -1.0f, -1.0f);
            convexTab2[5] = new Vector3(-1.1f, -1.0f, 1.0f);
            convexTab2[6] = new Vector3(1.0f, -1.0f, 0.5f);
            convexTab2[7] = new Vector3(1.0f, -1.0f, -0.5f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1ConvexDown");
            shapePrimitive.CreateConvex(convexTab2);

            shape = scene.Factory.ShapeManager.Create("Helicopter1ConvexDown");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Helicopter1ConvexDown"))
                demo.Meshes.Add("Helicopter1ConvexDown", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, false, CullFaceMode.Back, false, false));

            convexTab2[0] = new Vector3(-0.82f, -1.0f, 1.0f);
            convexTab2[1] = new Vector3(-0.82f, 1.0f, 1.0f);
            convexTab2[2] = new Vector3(0.76f, 1.0f, 1.0f);
            convexTab2[3] = new Vector3(0.76f, -0.74f, 1.0f);
            convexTab2[4] = new Vector3(-1.0f, -1.0f, -1.0f);
            convexTab2[5] = new Vector3(-1.0f, 1.0f, -1.0f);
            convexTab2[6] = new Vector3(1.0f, 1.0f, -1.0f);
            convexTab2[7] = new Vector3(1.0f, -0.74f, -1.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1ConvexRight");
            shapePrimitive.CreateConvex(convexTab2);

            shape = scene.Factory.ShapeManager.Create("Helicopter1ConvexRight");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Helicopter1ConvexRight"))
                demo.Meshes.Add("Helicopter1ConvexRight", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, false, CullFaceMode.Back, false, false));

            convexTab2[0] = new Vector3(-1.0f, -1.0f, 1.0f);
            convexTab2[1] = new Vector3(-1.0f, 1.0f, 1.0f);
            convexTab2[2] = new Vector3(1.0f, 1.0f, 1.0f);
            convexTab2[3] = new Vector3(1.0f, -0.74f, 1.0f);
            convexTab2[4] = new Vector3(-0.82f, -1.0f, -1.0f);
            convexTab2[5] = new Vector3(-0.82f, 1.0f, -1.0f);
            convexTab2[6] = new Vector3(0.76f, 1.0f, -1.0f);
            convexTab2[7] = new Vector3(0.76f, -0.74f, -1.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1ConvexLeft");
            shapePrimitive.CreateConvex(convexTab2);

            shape = scene.Factory.ShapeManager.Create("Helicopter1ConvexLeft");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Helicopter1ConvexLeft"))
                demo.Meshes.Add("Helicopter1ConvexLeft", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, false, CullFaceMode.Back, false, false));

            convexTab2[0] = new Vector3(-1.0f, 0.5f, -1.5f);
            convexTab2[1] = new Vector3(-1.0f, 0.5f, 1.5f);
            convexTab2[2] = new Vector3(1.0f, 1.0f, 1.0f);
            convexTab2[3] = new Vector3(1.0f, 1.0f, -1.0f);
            convexTab2[4] = new Vector3(-1.0f, -1.2f, -1.5f);
            convexTab2[5] = new Vector3(-1.0f, -1.2f, 1.5f);
            convexTab2[6] = new Vector3(1.0f, -1.0f, 1.0f);
            convexTab2[7] = new Vector3(1.0f, -1.0f, -1.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1ConvexFront");
            shapePrimitive.CreateConvex(convexTab2);

            shape = scene.Factory.ShapeManager.Create("Helicopter1ConvexFront");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Helicopter1ConvexFront"))
                demo.Meshes.Add("Helicopter1ConvexFront", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, false, CullFaceMode.Back, false, false));

            point1 = new Vector3(-0.8f, -2.5f, -7.0f);
            point2 = new Vector3(-0.8f, 1.0f, -7.0f);
            point3 = new Vector3(0.6f, 0.25f, -5.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Helicopter1CabinTriangle1");
            triangleMesh.CreateTriangle(point1, point2, point3);
            if (!demo.Meshes.ContainsKey("Helicopter1CabinTriangle1"))
                demo.Meshes.Add("Helicopter1CabinTriangle1", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, true, CullFaceMode.FrontAndBack, false, true));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1CabinTriangle1");
            shapePrimitive.CreateTriangle(point1, point2, point3);

            shape = scene.Factory.ShapeManager.Create("Helicopter1CabinTriangle1");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.01f);

            point1 = new Vector3(0.6f, 0.25f, -5.0f);
            point2 = new Vector3(0.8f, -3.2f, -15.0f);
            point3 = new Vector3(-0.8f, -2.5f, -7.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Helicopter1CabinTriangle2");
            triangleMesh.CreateTriangle(point1, point2, point3);
            if (!demo.Meshes.ContainsKey("Helicopter1CabinTriangle2"))
                demo.Meshes.Add("Helicopter1CabinTriangle2", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, true, CullFaceMode.FrontAndBack, false, true));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1CabinTriangle2");
            shapePrimitive.CreateTriangle(point1, point2, point3);

            shape = scene.Factory.ShapeManager.Create("Helicopter1CabinTriangle2");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.01f);

            point1 = new Vector3(-0.8f, -2.5f, 7.0f);
            point2 = new Vector3(-0.8f, 1.0f, 7.0f);
            point3 = new Vector3(0.6f, 0.25f, 5.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Helicopter1CabinTriangle3");
            triangleMesh.CreateTriangle(point1, point2, point3);
            if (!demo.Meshes.ContainsKey("Helicopter1CabinTriangle3"))
                demo.Meshes.Add("Helicopter1CabinTriangle3", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, true, CullFaceMode.FrontAndBack, false, true));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1CabinTriangle3");
            shapePrimitive.CreateTriangle(point1, point2, point3);

            shape = scene.Factory.ShapeManager.Create("Helicopter1CabinTriangle3");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.01f);

            point1 = new Vector3(0.6f, 0.25f, 5.0f);
            point2 = new Vector3(0.8f, -3.2f, 15.0f);
            point3 = new Vector3(-0.8f, -2.5f, 7.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Helicopter1CabinTriangle4");
            triangleMesh.CreateTriangle(point1, point2, point3);
            if (!demo.Meshes.ContainsKey("Helicopter1CabinTriangle4"))
                demo.Meshes.Add("Helicopter1CabinTriangle4", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, true, CullFaceMode.FrontAndBack, false, true));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Helicopter1CabinTriangle4");
            shapePrimitive.CreateTriangle(point1, point2, point3);

            shape = scene.Factory.ShapeManager.Create("Helicopter1CabinTriangle4");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.01f);

            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            shape = scene.Factory.ShapeManager.Create("Helicopter1Wheel");
            shape.Add(cylinderY, Matrix4.CreateScale(0.5f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.CreateScale(0.5f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Helicopter1Wheel"))
                demo.Meshes.Add("Helicopter1Wheel", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));
        }

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation)
        {
            Shape box = scene.Factory.ShapeManager.Find("Box");
            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape capsuleY = scene.Factory.ShapeManager.Find("CapsuleY");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");
            Shape triangle1 = scene.Factory.ShapeManager.Find("Triangle1");
            Shape triangle2 = scene.Factory.ShapeManager.Find("Triangle2");
            Shape doorTriangle1 = scene.Factory.ShapeManager.Find("Helicopter1DoorTriangle1");
            Shape doorTriangle2 = scene.Factory.ShapeManager.Find("Helicopter1DoorTriangle2");
            Shape convexUp = scene.Factory.ShapeManager.Find("Helicopter1ConvexUp");
            Shape convexDown = scene.Factory.ShapeManager.Find("Helicopter1ConvexDown");
            Shape convexRight = scene.Factory.ShapeManager.Find("Helicopter1ConvexRight");
            Shape convexLeft = scene.Factory.ShapeManager.Find("Helicopter1ConvexLeft");
            Shape convexFront = scene.Factory.ShapeManager.Find("Helicopter1ConvexFront");
            Shape cabinTriangle1 = scene.Factory.ShapeManager.Find("Helicopter1CabinTriangle1");
            Shape cabinTriangle2 = scene.Factory.ShapeManager.Find("Helicopter1CabinTriangle2");
            Shape cabinTriangle3 = scene.Factory.ShapeManager.Find("Helicopter1CabinTriangle3");
            Shape cabinTriangle4 = scene.Factory.ShapeManager.Find("Helicopter1CabinTriangle4");
            Shape wheel = scene.Factory.ShapeManager.Find("Helicopter1Wheel");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            PhysicsObject objectA = null;
            PhysicsObject objectB = null;
            PhysicsObject objectC = null;
            PhysicsObject objectD = null;
            PhysicsObject objectE = null;
            PhysicsObject objectF = null;
            PhysicsObject objectG = null;

            Vector3 position1 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Helicopter 1" + instanceIndexName);

            objectA = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Body" + instanceIndexName);
            objectA.EnableFeedback = true;
            objectRoot.AddChildPhysicsObject(objectA);

            objectB = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin" + instanceIndexName);
            objectB.Material.RigidGroup = true;
            objectA.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Up 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(2.85f, 13.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(2.75f, 0.5f, 4.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Up 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = convexUp;
            objectBase.UserDataStr = "Helicopter1ConvexUp";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(7.14f, 13.01f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(10.0f)));
            objectBase.InitLocalTransform.SetScale(1.72f, 0.3f, 4.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Down 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(4.4f, 5.2f, 0.0f);
            objectBase.InitLocalTransform.SetScale(3.9f, 0.2f, 4.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Down 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = convexDown;
            objectBase.UserDataStr = "Helicopter1ConvexDown";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(9.5f, 5.4f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-12.0f)));
            objectBase.InitLocalTransform.SetScale(1.2f, 0.2f, 4.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Right 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = convexRight;
            objectBase.UserDataStr = "Helicopter1ConvexRight";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(9.55f, 7.1f, -3.08f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(41.0f)));
            objectBase.InitLocalTransform.SetScale(1.85f, 2.0f, 0.165f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Right 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(1.8f, 9.0f, -4.2f);
            objectBase.InitLocalTransform.SetScale(1.7f, 4.0f, 0.2f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Left 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = convexLeft;
            objectBase.UserDataStr = "Helicopter1ConvexLeft";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(9.55f, 7.1f, 3.08f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-41.0f)));
            objectBase.InitLocalTransform.SetScale(1.85f, 2.0f, 0.165f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Left 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(1.8f, 9.0f, 4.2f);
            objectBase.InitLocalTransform.SetScale(1.7f, 4.0f, 0.2f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Front 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(10.9f, 7.22f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.2f, 1.75f, 2.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Front 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(9.7f, 10.7f, 1.8f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-31.0f)));
            objectBase.InitLocalTransform.SetScale(0.2f, 2.26f, 0.2f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Front 3" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(9.7f, 10.7f, -1.8f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-31.0f)));
            objectBase.InitLocalTransform.SetScale(0.2f, 2.26f, 0.2f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectC = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Front Pane" + instanceIndexName);
            objectC.Material.RigidGroup = true;
            objectB.AddChildPhysicsObject(objectC);
            objectC.InitLocalTransform.SetPosition(9.8f, 10.7f, 0.0f);
            objectC.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(90.0f)) * Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-31.0f)));

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Front Pane 1" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle1;
            objectBase.UserDataStr = "Triangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(-1.8f, 0.0f, 0.0f) + new Vector3(-1.8f, 2.26f, 0.0f) + new Vector3(1.8f, 2.26f, 0.0f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.8f, 1.13f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Front Pane 2" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle2;
            objectBase.UserDataStr = "Triangle2";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(1.8f, 2.26f, 0.0f) + new Vector3(1.8f, 0.0f, 0.0f) + new Vector3(-1.8f, 0.0f, 0.0f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.8f, 1.13f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Front Pane 3" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle1;
            objectBase.UserDataStr = "Triangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(-1.8f, -2.26f, 0.0f) + new Vector3(-1.8f, 0.0f, 0.0f) + new Vector3(1.8f, 0.0f, 0.0f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.8f, 1.13f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Front Pane 4" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle2;
            objectBase.UserDataStr = "Triangle2";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(1.8f, 0.0f, 0.0f) + new Vector3(1.8f, -2.26f, 0.0f) + new Vector3(-1.8f, -2.26f, 0.0f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.8f, 1.13f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Front Right" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(7.6f, 10.9f, -3.9f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-20.0f)));
            objectBase.InitLocalTransform.SetScale(0.2f, 2.2f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectC = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Right Pane" + instanceIndexName);
            objectC.Material.RigidGroup = true;
            objectB.AddChildPhysicsObject(objectC);
            objectC.InitLocalTransform.SetPosition(8.1f, 11.9f, -2.6f);
            objectC.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(50.0f)) * Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-20.0f)));

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Right Pane 1" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cabinTriangle1;
            objectBase.UserDataStr = "Helicopter1CabinTriangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(-0.8f * 1.8f, -2.5f * 1.15f, -7.0f * 0.05f) + new Vector3(-0.8f * 1.8f, 1.0f * 1.15f, -7.0f * 0.05f) + new Vector3(0.6f * 1.8f, 0.25f * 1.15f, -5.0f * 0.05f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.8f, 1.15f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Right Pane 2" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cabinTriangle2;
            objectBase.UserDataStr = "Helicopter1CabinTriangle2";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(0.6f * 1.8f, 0.25f * 1.15f, -5.0f * 0.05f) + new Vector3(0.8f * 1.8f, -3.2f * 1.15f, -15.0f * 0.05f) + new Vector3(-0.8f * 1.8f, -2.5f * 1.15f, -7.0f * 0.05f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.8f, 1.15f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Front Left" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(7.6f, 10.9f, 3.9f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-20.0f)));
            objectBase.InitLocalTransform.SetScale(0.2f, 2.2f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectC = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Left Pane" + instanceIndexName);
            objectC.Material.RigidGroup = true;
            objectB.AddChildPhysicsObject(objectC);
            objectC.InitLocalTransform.SetPosition(8.1f, 11.9f, 2.6f);
            objectC.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-50.0f)) * Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-20.0f)));

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Left Pane 1" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cabinTriangle3;
            objectBase.UserDataStr = "Helicopter1CabinTriangle3";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(-0.8f * 1.8f, -2.5f * 1.15f, 7.0f * 0.05f) + new Vector3(-0.8f * 1.8f, 1.0f * 1.15f, 7.0f * 0.05f) + new Vector3(0.6f * 1.8f, 0.25f * 1.15f, 5.0f * 0.05f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.8f, 1.15f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Left Pane 2" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cabinTriangle4;
            objectBase.UserDataStr = "Helicopter1CabinTriangle4";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(0.6f * 1.8f, 0.25f * 1.15f, 5.0f * 0.05f) + new Vector3(0.8f * 1.8f, -3.2f * 1.15f, 15.0f * 0.05f) + new Vector3(-0.8f * 1.8f, -2.5f * 1.15f, 7.0f * 0.05f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.8f, 1.15f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Front Panel 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(10.6f, 7.37f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 1.6f, 2.0f);
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.Integral.SetDensity(2.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Front Panel 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = convexFront;
            objectBase.UserDataStr = "Helicopter1ConvexFront";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(9.9f, 7.16f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.5f, 1.6f, 2.0f);
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.Integral.SetDensity(2.0f);
            objectBase.CreateSound(true);

            objectC = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Front Button" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectC);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Front Button 1" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(8.95f, 7.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.05f, 0.2f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Front Button 2" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(9.2f, 7.5f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(0.1f, 0.2f, 0.1f);
            objectBase.Integral.InertiaScaleFactor = 3.0f;
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Front Button Switch" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.Material.TransparencyFactor = 0.5f;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(8.8f, 7.5f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.3f, 0.3f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.EnableCollisionResponse = false;
            objectBase.EnableCursorInteraction = false;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Body Back" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(0.3f, 9.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.2f, 4.0f, 4.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectC = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Lamp" + instanceIndexName);
            objectC.Material.RigidGroup = true;
            objectB.AddChildPhysicsObject(objectC);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Lamp Base" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.TwoSidedNormals = true;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(5.0f, 12.9f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.1f, 0.4f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Cabin Lamp Light" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = sphere;
            objectBase.UserDataStr = "Sphere";
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(5.0f, 12.9f, 0.0f);
            objectBase.InitLocalTransform.SetScale(10.0f);
            objectBase.CreateLight(true);
            objectBase.Light.Type = PhysicsLightType.Point;
            objectBase.Light.SetDiffuse(1.0f, 0.9f, 0.8f);
            objectBase.Light.Range = 10.0f;
            objectBase.EnableCollisions = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.EnableBreakRigidGroup = false;
            objectBase.EnableAddToCameraDrawTransparentPhysicsObjects = false;

            objectC = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectC);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Up" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(5.2f, 12.9f, -4.2f);
            objectBase.InitLocalTransform.SetScale(1.7f, 0.3f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Down" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(5.9f, 7.0f, -4.2f);
            objectBase.InitLocalTransform.SetScale(2.4f, 2.0f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Left" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(3.7f, 10.8f, -4.2f);
            objectBase.InitLocalTransform.SetScale(0.2f, 1.8f, 0.15f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Right" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(7.4f, 10.9f, -4.2f);
            objectBase.InitLocalTransform.SetScale(0.2f, 2.1f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-20.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Switch" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(4.2f, 8.0f, -4.5f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.2f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectD = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Pane" + instanceIndexName);
            objectD.Material.RigidGroup = true;
            objectC.AddChildPhysicsObject(objectD);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Pane 1" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle1;
            objectBase.UserDataStr = "Triangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(5.2f - 1.4f, 10.8f, -4.2f) + new Vector3(5.2f - 1.4f, 10.8f + 1.8f, -4.2f) + new Vector3(5.2f + 1.4f, 10.8f + 1.8f, -4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.4f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Pane 2" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle2;
            objectBase.UserDataStr = "Triangle2";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(5.2f + 1.4f, 10.8f + 1.8f, -4.2f) + new Vector3(5.2f + 1.4f, 10.8f, -4.2f) + new Vector3(5.2f - 1.4f, 10.8f, -4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.4f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Pane 3" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle1;
            objectBase.UserDataStr = "Triangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(5.55f - 1.75f, 10.8f - 1.8f, -4.2f) + new Vector3(5.55f - 1.75f, 10.8f, -4.2f) + new Vector3(5.55f + 1.75f, 10.8f, -4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.75f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Pane 4" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle2;
            objectBase.UserDataStr = "Triangle2";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(5.55f + 1.75f, 10.8f, -4.2f) + new Vector3(5.55f + 1.75f, 10.8f - 1.8f, -4.2f) + new Vector3(5.55f - 1.75f, 10.8f - 1.8f, -4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.75f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Pane 5" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = doorTriangle1;
            objectBase.UserDataStr = "Helicopter1DoorTriangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(7.0f + 0.4f, 10.8f, -4.2f) + new Vector3(7.0f - 0.4f, 10.8f, -4.2f) + new Vector3(7.0f - 0.4f, 10.8f + 1.8f, -4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Door Pane 6" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = doorTriangle1;
            objectBase.UserDataStr = "Helicopter1DoorTriangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(7.7f + 0.4f, 10.8f - 1.8f, -4.2f) + new Vector3(7.7f - 0.4f, 10.8f - 1.8f, -4.2f) + new Vector3(7.7f - 0.4f, 10.8f, -4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectC = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectC);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Up" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(5.2f, 12.9f, 4.2f);
            objectBase.InitLocalTransform.SetScale(1.7f, 0.3f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Down" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(5.9f, 7.0f, 4.2f);
            objectBase.InitLocalTransform.SetScale(2.4f, 2.0f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Left" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(3.7f, 10.8f, 4.2f);
            objectBase.InitLocalTransform.SetScale(0.2f, 1.8f, 0.15f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Right" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(7.4f, 10.9f, 4.2f);
            objectBase.InitLocalTransform.SetScale(0.2f, 2.1f, 0.15f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-20.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Switch" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(4.2f, 8.0f, 4.5f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.2f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectD = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Pane" + instanceIndexName);
            objectD.Material.RigidGroup = true;
            objectC.AddChildPhysicsObject(objectD);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Pane 1" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle1;
            objectBase.UserDataStr = "Triangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(5.2f - 1.4f, 10.8f, 4.2f) + new Vector3(5.2f - 1.4f, 10.8f + 1.8f, 4.2f) + new Vector3(5.2f + 1.4f, 10.8f + 1.8f, 4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.4f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Pane 2" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle2;
            objectBase.UserDataStr = "Triangle2";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(5.2f + 1.4f, 10.8f + 1.8f, 4.2f) + new Vector3(5.2f + 1.4f, 10.8f, 4.2f) + new Vector3(5.2f - 1.4f, 10.8f, 4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.4f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Pane 3" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle1;
            objectBase.UserDataStr = "Triangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(5.55f - 1.75f, 10.8f - 1.8f, 4.2f) + new Vector3(5.55f - 1.75f, 10.8f, 4.2f) + new Vector3(5.55f + 1.75f, 10.8f, 4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.75f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Pane 4" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle2;
            objectBase.UserDataStr = "Triangle2";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(5.55f + 1.75f, 10.8f, 4.2f) + new Vector3(5.55f + 1.75f, 10.8f - 1.8f, 4.2f) + new Vector3(5.55f - 1.75f, 10.8f - 1.8f, 4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(1.75f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Pane 5" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = doorTriangle1;
            objectBase.UserDataStr = "Helicopter1DoorTriangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(7.0f + 0.4f, 10.8f, 4.2f) + new Vector3(7.0f - 0.4f, 10.8f, 4.2f) + new Vector3(7.0f - 0.4f, 10.8f + 1.8f, 4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Door Pane 6" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = doorTriangle1;
            objectBase.UserDataStr = "Helicopter1DoorTriangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 50.0f;
            objectBase.InitLocalTransform.SetPosition((new Vector3(7.7f + 0.4f, 10.8f - 1.8f, 4.2f) + new Vector3(7.7f - 0.4f, 10.8f - 1.8f, 4.2f) + new Vector3(7.7f - 0.4f, 10.8f, 4.2f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.9f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Axle 1" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(9.0f, 4.2f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.2f, 1.0f, 0.1f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);

            objectB = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Right Absorber" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Right Absorber 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 6.5f, -4.8f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-30.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 1.2f, 0.5f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Right Absorber 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 8.0f, -4.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(0.8f, 0.6f, 0.8f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);

            objectB = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Left Absorber" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Left Absorber 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 6.5f, 4.8f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(30.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 1.2f, 0.5f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Left Absorber 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 8.0f, 4.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(0.8f, 0.6f, 0.8f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);

            objectB = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Absorber" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Absorber Axle 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 3.8f, -6.5f);
            objectBase.InitLocalTransform.SetScale(0.5f, 0.2f, 0.8f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Absorber Axle 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.55f, 4.6f, -3.9f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(22.0f)));
            objectBase.InitLocalTransform.SetScale(0.3f, 0.1f, 2.2f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Absorber 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 5.2f, -5.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-30.0f)));
            objectBase.InitLocalTransform.SetScale(0.3f, 1.5f, 0.3f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Right Absorber 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 5.5f, -1.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 0.6f, 0.5f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);

            objectB = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Absorber" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Absorber Axle 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 3.8f, 6.5f);
            objectBase.InitLocalTransform.SetScale(0.5f, 0.2f, 0.8f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Absorber Axle 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.55f, 4.6f, 3.9f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-22.0f)));
            objectBase.InitLocalTransform.SetScale(0.3f, 0.1f, 2.2f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Absorber 1" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 5.2f, 5.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(30.0f)));
            objectBase.InitLocalTransform.SetScale(0.3f, 1.5f, 0.3f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Left Absorber 2" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-0.5f, 5.5f, 1.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 0.6f, 0.5f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Wheel 1" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = wheel;
            objectBase.UserDataStr = "Helicopter1Wheel";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.InitLocalTransform.SetPosition(9.0f, 3.7f, 0.4f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(0.9f, 0.3f, 0.9f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.MinResponseLinearVelocity = 0.05f;
            objectBase.MinResponseAngularVelocity = 0.05f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 14000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Wheel 2" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = wheel;
            objectBase.UserDataStr = "Helicopter1Wheel";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.InitLocalTransform.SetPosition(9.0f, 3.7f, -0.4f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(0.9f, 0.3f, 0.9f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.MinResponseLinearVelocity = 0.05f;
            objectBase.MinResponseAngularVelocity = 0.05f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 14000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Wheel 3" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = wheel;
            objectBase.UserDataStr = "Helicopter1Wheel";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.InitLocalTransform.SetPosition(-0.5f, 3.8f, -7.8f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(1.0f, 0.5f, 1.0f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.MinResponseLinearVelocity = 0.05f;
            objectBase.MinResponseAngularVelocity = 0.05f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 14000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Wheel 4" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = wheel;
            objectBase.UserDataStr = "Helicopter1Wheel";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.InitLocalTransform.SetPosition(-0.5f, 3.8f, 7.8f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(1.0f, 0.5f, 1.0f);
            objectBase.Integral.SetDensity(40.0f);
            objectBase.MinResponseLinearVelocity = 0.05f;
            objectBase.MinResponseAngularVelocity = 0.05f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 14000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectD = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Tail" + instanceIndexName);
            objectD.Material.RigidGroup = true;
            objectA.AddChildPhysicsObject(objectD);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Tail 1" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-1.0f, 14.0f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(3.0f, 1.1f, 2.0f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Tail 2" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-3.0f, 14.5f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(2.0f, 1.0f, 1.0f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Tail 3" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-6.0f, 15.2f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(1.0f, 2.0f, 0.7f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Tail 4" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 2000.0f;
            objectBase.InitLocalTransform.SetPosition(-14.0f, 15.4f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(0.6f, 6.0f, 0.4f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Tail 5" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-22.5f, 16.4f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-70.0f)));
            objectBase.InitLocalTransform.SetScale(0.6f, 3.0f, 0.3f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Tail 6" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(-24.0f, 17.0f, -0.4f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(0.2f, 0.2f, 0.2f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Stabilizer Right" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.InitLocalTransform.SetPosition(-18.0f, 15.4f, -1.2f);
            objectBase.InitLocalTransform.SetScale(0.5f, 0.1f, 1.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Stabilizer Left" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.InitLocalTransform.SetPosition(-18.0f, 15.4f, 1.2f);
            objectBase.InitLocalTransform.SetScale(0.5f, 0.1f, 1.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectE = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Body" + instanceIndexName);
            objectE.Material.RigidGroup = true;
            objectA.AddChildPhysicsObject(objectE);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Body 1" + instanceIndexName);
            objectE.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(1.0f, 16.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(1.5f, 1.0f, 1.5f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Body 2" + instanceIndexName);
            objectE.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(1.0f, 17.2f, 0.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 0.2f, 1.0f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Body 3" + instanceIndexName);
            objectE.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(2.0f, 15.0f, -1.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(1.5f, 2.5f, 1.5f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Body 4" + instanceIndexName);
            objectE.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Paint2";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 800.0f;
            objectBase.InitLocalTransform.SetPosition(2.0f, 15.0f, 1.5f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(1.5f, 2.5f, 1.5f);
            objectBase.Integral.SetDensity(3.0f);
            objectBase.CreateSound(true);

            objectF = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Rotor" + instanceIndexName);
            objectF.MaxPreUpdateAngularVelocity = 20.0f;
            objectF.MaxPostUpdateAngularVelocity = 20.0f;
            objectA.AddChildPhysicsObject(objectF);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Rotor Body" + instanceIndexName);
            objectF.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(1.0f, 17.9f, 0.0f);
            objectBase.InitLocalTransform.SetScale(1.5f, 0.5f, 1.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Rotor Airscrew 1" + instanceIndexName);
            objectF.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(11.0f, 17.9f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            objectBase.InitLocalTransform.SetScale(9.0f, 0.05f, 0.5f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Rotor Airscrew 2" + instanceIndexName);
            objectF.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-4.0f, 17.9f, 9.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)) * Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(120.0f)));
            objectBase.InitLocalTransform.SetScale(9.0f, 0.05f, 0.5f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Up Rotor Airscrew 3" + instanceIndexName);
            objectF.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-4.0f, 17.9f, -9.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)) * Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(240.0f)));
            objectBase.InitLocalTransform.SetScale(9.0f, 0.05f, 0.5f);
            objectBase.Integral.SetDensity(0.5f);
            objectBase.CreateSound(true);

            objectG = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Back Rotor" + instanceIndexName);
            objectG.MaxPreUpdateAngularVelocity = 20.0f;
            objectG.MaxPostUpdateAngularVelocity = 20.0f;
            objectA.AddChildPhysicsObject(objectG);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Back Rotor Body" + instanceIndexName);
            objectG.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-24.0f, 17.0f, -0.9f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 0.3f, 0.5f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Back Rotor Airscrew 1" + instanceIndexName);
            objectG.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-24.0f, 19.0f, -0.9f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45.0f)));
            objectBase.InitLocalTransform.SetScale(0.2f, 2.0f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Back Rotor Airscrew 2" + instanceIndexName);
            objectG.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-22.0f, 16.0f, -0.9f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45.0f)) * Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(120.0f)));
            objectBase.InitLocalTransform.SetScale(0.2f, 2.0f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Helicopter 1 Back Rotor Airscrew 3" + instanceIndexName);
            objectG.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 200.0f;
            objectBase.InitLocalTransform.SetPosition(-26.0f, 16.0f, -0.9f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45.0f)) * Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(240.0f)));
            objectBase.InitLocalTransform.SetScale(0.2f, 2.0f, 0.05f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);

            objectRoot.UpdateFromInitLocalTransform();

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Left 1" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Right" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Up 2" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Up" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Right" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Front Left" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Up" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Right" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Down" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Left 2" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Up" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Down" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Left" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Right 1" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Right" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Up 2" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Up" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Right" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Front Right" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Up" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Right" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Down" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Right 2" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Up" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Down" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Left" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Up 1" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Up" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Up" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Down 1" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Down" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Down" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Down 2" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Down" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Down" + instanceIndexName), true);

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Up Rotor Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Rotor Body" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Body 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 0.5f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 0.5f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Back Rotor Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Back Rotor Body" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Tail 6" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.2f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.2f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Right Door Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Down" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Right 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(2.4f, 0.0f, -0.2f));
            constraint.SetAnchor2(position1 + new Vector3(2.4f, 0.0f, -0.2f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = 0.0f;
            constraint.MaxLimitDegAngleY = 80.0f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Left Door Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Down" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Left 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(2.4f, 0.0f, 0.2f));
            constraint.SetAnchor2(position1 + new Vector3(2.4f, 0.0f, 0.2f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -80.0f;
            constraint.MaxLimitDegAngleY = 0.0f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Stabilizer Right Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Stabilizer Right" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Tail 4" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 1.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 1.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 30.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Stabilizer Left Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Stabilizer Left" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Tail 4" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -1.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -1.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 30.0f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Up Right Absorber 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Right Absorber 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Back" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 10.0f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Up Left Absorber 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Left Absorber 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Back" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -10.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Up Right Absorber 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Right Absorber 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Right 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 10.0f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Up Left Absorber 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Left Absorber 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Left 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -10.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Right Absorber Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Absorber 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Back" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = 0.0f;
            constraint.MaxLimitDegAngleX = 10.0f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Left Absorber Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Absorber 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Back" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.6f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -10.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Right Absorber Absorber Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Right Absorber 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Absorber 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, -1.0f, -0.5f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, -1.0f, -0.5f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Distance = 0.5f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Left Absorber Absorber Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Left Absorber 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Absorber 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, -1.0f, 0.5f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, -1.0f, 0.5f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Distance = 0.5f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Right Absorber Absorber Spring Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Right Absorber 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Absorber 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, -1.0f, -0.5f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, -1.0f, -0.5f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Force = 0.2f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Left Absorber Absorber Spring Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Left Absorber 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Absorber 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, -1.0f, 0.5f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, -1.0f, 0.5f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Force = 0.2f;
            constraint.EnableBreak = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Wheel 1 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Wheel 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Axle 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.3f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.3f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Wheel 2 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Wheel 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Axle 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.3f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.3f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Wheel 12 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Wheel 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Wheel 2" + instanceIndexName);
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
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Wheel 3 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Wheel 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Absorber Axle 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -0.5f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -0.5f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.AngularDamping = 0.1f;
            constraint.MinResponseLinearVelocity = 0.05f;
            constraint.MinResponseAngularVelocity = 0.05f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Wheel 4 Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Wheel 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Absorber Axle 1" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, 0.5f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, 0.5f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.AngularDamping = 0.1f;
            constraint.MinResponseLinearVelocity = 0.05f;
            constraint.MinResponseAngularVelocity = 0.05f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Helicopter 1 Cabin Front Button Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Front Button 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Front Panel 2" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.2f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.2f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MaxLimitDistanceX = 0.4f;
            constraint.EnableControlDistanceX = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 200.0f;
            constraint.Update();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
