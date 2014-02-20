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
    public class Car1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Car1(Demo demo, int instanceIndex)
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

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Car1Torus1");
            triangleMesh.CreateTorusY(10, 15, 3.0f, 0.5f);
            if (!demo.Meshes.ContainsKey("Car1Torus1"))
                demo.Meshes.Add("Car1Torus1", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Car1Torus1");
            shapePrimitive.CreateConvex(triangleMesh);

            shape = scene.Factory.ShapeManager.Create("Car1Torus1");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            shape = scene.Factory.ShapeManager.Create("Car1Wheel");
            shape.Add(cylinderY, Matrix4.CreateScale(0.5f, 1.0f, 0.5f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.CreateScale(0.5f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Car1Wheel"))
                demo.Meshes.Add("Car1Wheel", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));
        }

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation)
        {
            Shape box = scene.Factory.ShapeManager.Find("Box");
            Shape coneZ = scene.Factory.ShapeManager.Find("ConeZ");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");
            Shape triangle1 = scene.Factory.ShapeManager.Find("Triangle1");
            Shape triangle2 = scene.Factory.ShapeManager.Find("Triangle2");
            Shape Torus1 = scene.Factory.ShapeManager.Find("Car1Torus1");
            Shape Wheel = scene.Factory.ShapeManager.Find("Car1Wheel");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            PhysicsObject objectA = null;
            PhysicsObject objectB = null;
            PhysicsObject objectC = null;
            PhysicsObject objectD = null;
            PhysicsObject objectE = null;
            PhysicsObject objectF = null;

            Vector3 position1 = Vector3.Zero;
            Vector3 position2 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Car 1" + instanceIndexName);

            objectA = scene.Factory.PhysicsObjectManager.Create("Car 1 Body" + instanceIndexName);
            objectA.EnableFeedback = true;
            objectRoot.AddChildPhysicsObject(objectA);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Chassis Back" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 10.0f, 26.0f);
            objectBase.InitLocalTransform.SetScale(3.0f, 1.5f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Chassis Middle Down" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 9.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(5.0f, 0.5f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Chassis Middle Front" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 11.0f, 17.1f);
            objectBase.InitLocalTransform.SetScale(5.0f, 1.5f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Chassis Up Panel" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.Material.SetDiffuse(0.5f, 0.5f, 0.5f);
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(2.0f, 12.5f, 16.2f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(45.0f)));
            objectBase.InitLocalTransform.SetScale(1.5f, 0.6f, 0.6f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Chassis Down Panel" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.Material.SetDiffuse(0.5f, 0.5f, 0.5f);
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(2.0f, 12.4f, 17.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(45.0f)));
            objectBase.InitLocalTransform.SetScale(0.4f, 0.4f, 0.25f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Chassis Middle Back" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 11.0f, 22.9f);
            objectBase.InitLocalTransform.SetScale(5.0f, 1.5f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Chassis Front" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 10.0f, 14.0f);
            objectBase.InitLocalTransform.SetScale(3.0f, 1.5f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Front Body" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 10.0f, 10.5f);
            objectBase.InitLocalTransform.SetScale(5.0f, 1.5f, 0.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Back Body" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 10.0f, 29.5f);
            objectBase.InitLocalTransform.SetScale(5.0f, 1.5f, 0.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Up Body Down Back" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 12.0f, 26.5f);
            objectBase.InitLocalTransform.SetScale(6.0f, 0.5f, 3.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Up Body Down Front" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 12.0f, 13.5f);
            objectBase.InitLocalTransform.SetScale(6.0f, 0.5f, 3.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Up Body Front" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 13.0f, 23.2f);
            objectBase.InitLocalTransform.SetScale(5.6f, 0.5f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Up Body Right" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(-5.8f, 13.0f, 26.3f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.5f, 3.3f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Up Body Left" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(5.8f, 13.0f, 26.3f);
            objectBase.InitLocalTransform.SetScale(0.2f, 0.5f, 3.3f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Up Body Back" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 13.0f, 29.8f);
            objectBase.InitLocalTransform.SetScale(6.0f, 0.5f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Up Body Spare Wheel" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(-1.0f, 12.0f, 30.2f);
            objectBase.InitLocalTransform.SetScale(1.0f, 1.0f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectB = scene.Factory.PhysicsObjectManager.Create("Car 1 Front Right Lamp" + instanceIndexName);
            objectB.Material.RigidGroup = true;
            objectA.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Front Right Lamp Base" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.TwoSidedNormals = true;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetPosition(-3.5f, 11.0f, 9.9f);
            objectBase.InitLocalTransform.SetScale(0.7f, 0.7f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Front Right Lamp Light" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = coneZ;
            objectBase.UserDataStr = "ConeZ";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetPosition(-3.5f, 11.0f, -19.0f);
            objectBase.InitLocalTransform.SetScale(30.0f);
            objectBase.CreateLight(true);
            objectBase.Light.Type = PhysicsLightType.Spot;
            objectBase.Light.SetDiffuse(0.8f, 0.8f, 0.8f);
            objectBase.Light.SpotInnerRadAngle = (float)Math.Atan(0.35);
            objectBase.Light.SpotOuterRadAngle = (float)Math.Atan(0.5);
            objectBase.Light.Range = 60.0f;
            objectBase.EnableCollisions = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.EnableBreakRigidGroup = false;
            objectBase.EnableAddToCameraDrawTransparentPhysicsObjects = false;

            objectB = scene.Factory.PhysicsObjectManager.Create("Car 1 Front Left Lamp" + instanceIndexName);
            objectB.Material.RigidGroup = true;
            objectA.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Front Left Lamp Base" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.TwoSidedNormals = true;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetPosition(3.5f, 11.0f, 9.9f);
            objectBase.InitLocalTransform.SetScale(0.7f, 0.7f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Front Left Lamp Light" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = coneZ;
            objectBase.UserDataStr = "ConeZ";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetPosition(3.5f, 11.0f, -19.0f);
            objectBase.InitLocalTransform.SetScale(30.0f);
            objectBase.CreateLight(true);
            objectBase.Light.Type = PhysicsLightType.Spot;
            objectBase.Light.SetDiffuse(0.8f, 0.8f, 0.8f);
            objectBase.Light.SpotInnerRadAngle = (float)Math.Atan(0.35);
            objectBase.Light.SpotOuterRadAngle = (float)Math.Atan(0.5);
            objectBase.Light.Range = 60.0f;
            objectBase.EnableCollisions = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.EnableBreakRigidGroup = false;
            objectBase.EnableAddToCameraDrawTransparentPhysicsObjects = false;

            objectB = scene.Factory.PhysicsObjectManager.Create("Car 1 Right Door" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectB);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Right Door Body" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(-5.2f, 10.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.2f, 2.0f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Right Door Switch" + instanceIndexName);
            objectB.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(-5.5f, 11.5f, 22.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 0.2f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectC = scene.Factory.PhysicsObjectManager.Create("Car 1 Left Door" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectC);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Left Door Body" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(5.2f, 10.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.2f, 2.0f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Left Door Switch" + instanceIndexName);
            objectC.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(5.5f, 11.5f, 22.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 0.2f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectD = scene.Factory.PhysicsObjectManager.Create("Car 1 Steering Gear" + instanceIndexName);
            objectD.InitLocalTransform.SetPosition(2.0f, 13.0f, 17.6f);
            objectD.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-45.0f)));
            objectA.AddChildPhysicsObject(objectD);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Steering Wheel" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = Torus1;
            objectBase.UserDataStr = "Car1Torus1";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.Material.SetDiffuse(0.5f, 0.5f, 0.5f);
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 0.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.4f, 0.4f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Steering Gear Left" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.Material.SetDiffuse(0.5f, 0.5f, 0.5f);
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.6f, 0.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.05f, 0.15f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Steering Gear Right" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.Material.SetDiffuse(0.5f, 0.5f, 0.5f);
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(-0.6f, 0.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.05f, 0.15f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Steering Gear Down" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Plastic1";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.Material.SetDiffuse(0.5f, 0.5f, 0.5f);
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, -0.26f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.34f, 0.4f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Steering Gear Switch" + instanceIndexName);
            objectD.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.Material.TransparencyFactor = 0.5f;
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 0.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(2.0f, 0.4f, 2.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.EnableCollisionResponse = false;
            objectBase.EnableCursorInteraction = false;

            objectE = scene.Factory.PhysicsObjectManager.Create("Car 1 Windscreen" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectE);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Windscreen Up" + instanceIndexName);
            objectE.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 16.3f, 15.0f);
            objectBase.InitLocalTransform.SetScale(5.4f, 0.2f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Windscreen Down" + instanceIndexName);
            objectE.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(0.0f, 12.7f, 15.0f);
            objectBase.InitLocalTransform.SetScale(5.4f, 0.2f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Windscreen Right" + instanceIndexName);
            objectE.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(-5.2f, 14.5f, 15.0f);
            objectBase.InitLocalTransform.SetScale(0.2f, 1.6f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Windscreen Left" + instanceIndexName);
            objectE.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Paint1";
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition(5.2f, 14.5f, 15.0f);
            objectBase.InitLocalTransform.SetScale(0.2f, 1.6f, 0.2f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectF = scene.Factory.PhysicsObjectManager.Create("Car 1 Pane" + instanceIndexName);
            objectF.Material.RigidGroup = true;
            objectE.AddChildPhysicsObject(objectF);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Pane 1" + instanceIndexName);
            objectF.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle1;
            objectBase.UserDataStr = "Triangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition((new Vector3(0.0f - 5.4f, 14.5f - 1.6f, 15.0f) + new Vector3(0.0f - 5.4f, 14.5f + 1.6f, 15.0f) + new Vector3(0.0f, 14.5f + 1.6f, 15.0f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(2.7f, 1.6f, 0.2f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Pane 2" + instanceIndexName);
            objectF.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle2;
            objectBase.UserDataStr = "Triangle2";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition((new Vector3(0.0f, 14.5f + 1.6f, 15.0f) + new Vector3(0.0f, 14.5f - 1.6f, 15.0f) + new Vector3(0.0f - 5.4f, 14.5f - 1.6f, 15.0f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(2.7f, 1.6f, 0.2f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Pane 3" + instanceIndexName);
            objectF.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle1;
            objectBase.UserDataStr = "Triangle1";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition((new Vector3(0.0f, 14.5f - 1.6f, 15.0f) + new Vector3(0.0f, 14.5f + 1.6f, 15.0f) + new Vector3(0.0f + 5.4f, 14.5f + 1.6f, 15.0f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(2.7f, 1.6f, 0.2f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Pane 4" + instanceIndexName);
            objectF.AddChildPhysicsObject(objectBase);
            objectBase.Shape = triangle2;
            objectBase.UserDataStr = "Triangle2";
            objectBase.Material.UserDataStr = "Green";
            objectBase.Material.TransparencyFactor = 0.2f;
            objectBase.Material.TransparencyRigidGroupSorting = true;
            objectBase.Material.TransparencySecondPass = false;
            objectBase.Material.RigidGroup = true;
            objectBase.InitLocalTransform.SetPosition((new Vector3(0.0f + 5.4f, 14.5f + 1.6f, 15.0f) + new Vector3(0.0f + 5.4f, 14.5f - 1.6f, 15.0f) + new Vector3(0.0f, 14.5f - 1.6f, 15.0f)) / 3.0f);
            objectBase.InitLocalTransform.SetScale(2.7f, 1.6f, 0.2f);
            objectBase.Integral.SetDensity(0.1f);
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "Glass";

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Spare Wheel" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = Wheel;
            objectBase.UserDataStr = "Car1Wheel";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.InitLocalTransform.SetPosition(-1.0f, 12.0f, 31.1f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(2.0f, 0.5f, 2.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Wheel 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = Wheel;
            objectBase.UserDataStr = "Car1Wheel";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.InitLocalTransform.SetPosition(5.0f, 9.0f, 14.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(2.0f, 0.5f, 2.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.MaxPreUpdateAngularVelocity = 10.0f;
            objectBase.MaxPostUpdateAngularVelocity = 10.0f;
            objectBase.MinResponseLinearVelocity = 0.005f;
            objectBase.MinResponseAngularVelocity = 0.005f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Axle 1" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.InitLocalTransform.SetPosition(4.0f, 9.0f, 14.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(5.0f);
            objectBase.MinResponseLinearVelocity = 0.005f;
            objectBase.MinResponseAngularVelocity = 0.005f;
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Wheel 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = Wheel;
            objectBase.UserDataStr = "Car1Wheel";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.InitLocalTransform.SetPosition(-5.0f, 9.0f, 14.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(2.0f, 0.5f, 2.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.MaxPreUpdateAngularVelocity = 10.0f;
            objectBase.MaxPostUpdateAngularVelocity = 10.0f;
            objectBase.MinResponseLinearVelocity = 0.005f;
            objectBase.MinResponseAngularVelocity = 0.005f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Axle 2" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.InitLocalTransform.SetPosition(-4.0f, 9.0f, 14.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(5.0f);
            objectBase.MinResponseLinearVelocity = 0.005f;
            objectBase.MinResponseAngularVelocity = 0.005f;
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Wheel 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = Wheel;
            objectBase.UserDataStr = "Car1Wheel";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.InitLocalTransform.SetPosition(5.0f, 9.0f, 26.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(2.0f, 0.5f, 2.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.MaxPreUpdateAngularVelocity = 10.0f;
            objectBase.MaxPostUpdateAngularVelocity = 10.0f;
            objectBase.MinResponseLinearVelocity = 0.005f;
            objectBase.MinResponseAngularVelocity = 0.005f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Axle 3" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.InitLocalTransform.SetPosition(4.0f, 9.0f, 26.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(5.0f);
            objectBase.MinResponseLinearVelocity = 0.005f;
            objectBase.MinResponseAngularVelocity = 0.005f;
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Wheel 4" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = Wheel;
            objectBase.UserDataStr = "Car1Wheel";
            objectBase.Material.UserDataStr = "Rubber";
            objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
            objectBase.InitLocalTransform.SetPosition(-5.0f, 9.0f, 26.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(2.0f, 0.5f, 2.0f);
            objectBase.Integral.SetDensity(5.0f);
            objectBase.MaxPreUpdateAngularVelocity = 10.0f;
            objectBase.MaxPostUpdateAngularVelocity = 10.0f;
            objectBase.MinResponseLinearVelocity = 0.005f;
            objectBase.MinResponseAngularVelocity = 0.005f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.Sound.MinSlideVelocityMagnitude = 1.0f;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Car 1 Axle 4" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.InitLocalTransform.SetPosition(-4.0f, 9.0f, 26.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(5.0f);
            objectBase.MinResponseLinearVelocity = 0.005f;
            objectBase.MinResponseAngularVelocity = 0.005f;
            objectBase.CreateSound(true);

            objectRoot.UpdateFromInitLocalTransform();

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Right Door Body" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Down" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Front" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Back" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Front" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Down Back" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Left Door Body" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Down" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Front" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Back" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Front" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Down Back" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Front" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 1" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 2" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 1" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 2" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Back" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 3" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 4" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 3" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 4" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Front Body" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 1" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 2" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 1" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 2" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Back Body" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 3" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 4" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 3" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 4" + instanceIndexName), true);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Steering Gear Down" + instanceIndexName);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Up Panel" + instanceIndexName), true);
            objectBase.DisableCollision(scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Down Panel" + instanceIndexName), true);

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Car 1 Windscreen Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Windscreen Down" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Down Front" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, -0.2f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, -0.2f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -20.0f;
            constraint.MaxLimitDegAngleX = 0.0f;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Right Door Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Right Door Body" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Down Front" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -2.95f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -2.95f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = 0.0f;
            constraint.MaxLimitDegAngleY = 80.0f;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Left Door Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Left Door Body" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Down Front" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, -2.95f));
            constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, -2.95f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -80.0f;
            constraint.MaxLimitDegAngleY = 0.0f;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Steering Gear Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Steering Gear Down" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Front" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(orientation1 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(45.0f)));
            constraint.SetInitWorldOrientation2(orientation2 * Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(45.0f)));
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -60.0f;
            constraint.MaxLimitDegAngleY = 60.0f;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 100.0f;
            constraint.EnableControlAngleY = true;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Vehicle Spare Wheel Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Spare Wheel" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Spare Wheel" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(0.0f, 0.0f, 0.2f));
            constraint.SetAnchor2(position2 + new Vector3(0.0f, 0.0f, 0.2f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 50.0f;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 1" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Front" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 1" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreakRigidGroup = false;
            constraint.MinLimitDegAngleY = -30.0f;
            constraint.MaxLimitDegAngleY = 30.0f;
            constraint.EnableControlAngleY = true;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 2" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 1" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 1" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 - new Vector3(0.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 - new Vector3(0.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 3" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Front" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 1" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position2);
            constraint.SetAnchor2(ref position2);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreakRigidGroup = false;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 4" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Front" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 2" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreakRigidGroup = false;
            constraint.MinLimitDegAngleY = -30.0f;
            constraint.MaxLimitDegAngleY = 30.0f;
            constraint.EnableControlAngleY = true;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 5" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 2" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 2" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(0.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(0.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 6" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Front" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 2" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position2);
            constraint.SetAnchor2(ref position2);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreakRigidGroup = false;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 7" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Back" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 3" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreakRigidGroup = false;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 8" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 3" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 3" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 - new Vector3(0.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 - new Vector3(0.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 9" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Back" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 3" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position2);
            constraint.SetAnchor2(ref position2);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreakRigidGroup = false;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 10" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Back" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 4" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreakRigidGroup = false;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 11" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Axle 4" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 4" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(0.5f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(0.5f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Car 1 Constraint 12" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Back" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 4" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref position2);
            constraint.SetAnchor2(ref position2);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.MaxLimitDistanceY = 0.5f;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreakRigidGroup = false;
            constraint.MinResponseLinearVelocity = 0.005f;
            constraint.MinResponseAngularVelocity = 0.005f;
            constraint.Update();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
