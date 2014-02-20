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
    public class UserShapes
    {
        Demo demo;
        PhysicsScene scene;

        public UserShapes(Demo demo)
        {
            this.demo = demo;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public static void CreateShapes(Demo demo, PhysicsScene scene)
        {
            ShapePrimitive shapePrimitive = null;
            Shape shape = null;
            Shape userShape = null;

            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape box = scene.Factory.ShapeManager.Find("Box");
            Shape coneY = scene.Factory.ShapeManager.Find("ConeY");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            shape = scene.Factory.ShapeManager.Create("UserShape 1");
            shape.Add(box, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("UserShape1"))
                demo.Meshes.Add("UserShape1", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

            shape = scene.Factory.ShapeManager.Create("UserShape 2");
            shape.Add(coneY, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("UserShape2"))
                demo.Meshes.Add("UserShape2", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

            shape = scene.Factory.ShapeManager.Create("UserShape 3");
            shape.Add(cylinderY, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("UserShape3"))
                demo.Meshes.Add("UserShape3", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

            shape = scene.Factory.ShapeManager.Create("UserShape 4");
            shape.Add(coneY, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(cylinderY, Matrix4.CreateScale(4.0f, 0.5f, 4.0f), 0.0f, ShapeCompoundType.ConvexHull);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("UserShape4"))
                demo.Meshes.Add("UserShape4", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

            Vector3[] convexTab1 = new Vector3[6];
            convexTab1[0] = new Vector3(0.0f, -2.0f, 0.0f);
            convexTab1[1] = new Vector3(0.0f, 4.0f, 0.0f);
            convexTab1[2] = new Vector3(2.0f, 0.0f, 0.0f);
            convexTab1[3] = new Vector3(-4.0f, 0.0f, 0.0f);
            convexTab1[4] = new Vector3(0.0f, 0.0f, 4.0f);
            convexTab1[5] = new Vector3(0.0f, 0.0f, -2.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Convex 1");
            shapePrimitive.CreateConvex(convexTab1);

            userShape = scene.Factory.ShapeManager.Create("UserShape 5");
            userShape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
            userShape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("UserShape5"))
                demo.Meshes.Add("UserShape5", new DemoMesh(demo, userShape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

            shape = scene.Factory.ShapeManager.Create("UserShape 6");
            shape.Add(userShape, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.CreateScale(0.5f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("UserShape6"))
                demo.Meshes.Add("UserShape6", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

            Vector3[] convexTab2 = new Vector3[10];
            convexTab2[0] = new Vector3(-2.0f, -0.5f, -1.0f);
            convexTab2[1] = new Vector3(-1.5f, -0.5f, 0.5f);
            convexTab2[2] = new Vector3(0.0f, -0.5f, 2.0f);
            convexTab2[3] = new Vector3(1.5f, -0.5f, 0.5f);
            convexTab2[4] = new Vector3(2.0f, -0.5f, -1.0f);
            convexTab2[5] = new Vector3(-2.0f, 0.5f, -1.0f);
            convexTab2[6] = new Vector3(-1.5f, 0.5f, 0.5f);
            convexTab2[7] = new Vector3(0.0f, 0.5f, 2.0f);
            convexTab2[8] = new Vector3(1.5f, 0.5f, 0.5f);
            convexTab2[9] = new Vector3(2.0f, 0.5f, -1.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Convex 2");
            shapePrimitive.CreateConvex(convexTab2);
            userShape = scene.Factory.ShapeManager.Create("UserShape 7");
            userShape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
            userShape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("UserShape7"))
                demo.Meshes.Add("UserShape7", new DemoMesh(demo, userShape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

            shape = scene.Factory.ShapeManager.Create("UserShape 8");
            shape.Add(userShape, Matrix4.Identity, 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.Add(sphere, Matrix4.CreateScale(0.5f), 0.0f, ShapeCompoundType.MinkowskiSum);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("UserShape8"))
                demo.Meshes.Add("UserShape8", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));
        }

        public void Create()
        {
            PhysicsObject objectBase = null;

            Shape userShape1 = scene.Factory.ShapeManager.Find("UserShape 1");
            Shape userShape2 = scene.Factory.ShapeManager.Find("UserShape 2");
            Shape userShape3 = scene.Factory.ShapeManager.Find("UserShape 3");
            Shape userShape4 = scene.Factory.ShapeManager.Find("UserShape 4");
            Shape userShape5 = scene.Factory.ShapeManager.Find("UserShape 5");
            Shape userShape6 = scene.Factory.ShapeManager.Find("UserShape 6");
            Shape userShape7 = scene.Factory.ShapeManager.Find("UserShape 7");
            Shape userShape8 = scene.Factory.ShapeManager.Find("UserShape 8");

            objectBase = scene.Factory.PhysicsObjectManager.Create("UserShape 1");
            objectBase.Shape = userShape1;
            objectBase.UserDataStr = "UserShape1";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-20.0f, 20.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("UserShape 2");
            objectBase.Shape = userShape2;
            objectBase.UserDataStr = "UserShape2";
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.InitLocalTransform.SetPosition(20.0f, 20.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("UserShape 3");
            objectBase.Shape = userShape3;
            objectBase.UserDataStr = "UserShape3";
            objectBase.Material.TransparencyFactor = 0.5f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.InitLocalTransform.SetPosition(20.0f, 20.0f, 5.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("UserShape 4");
            objectBase.Shape = userShape4;
            objectBase.UserDataStr = "UserShape4";
            objectBase.Material.TransparencyFactor = 0.5f;
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 7000.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 20.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("UserShape 5");
            objectBase.Shape = userShape5;
            objectBase.UserDataStr = "UserShape5";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-20.0f, 20.0f, 40.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("UserShape 6");
            objectBase.Shape = userShape6;
            objectBase.UserDataStr = "UserShape6";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(20.0f, 20.0f, 40.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("UserShape 7");
            objectBase.Shape = userShape7;
            objectBase.UserDataStr = "UserShape7";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(6.0f, 20.0f, 5.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("UserShape 8");
            objectBase.Shape = userShape8;
            objectBase.UserDataStr = "UserShape8";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, 20.0f, 40.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);
        }
    }
}
