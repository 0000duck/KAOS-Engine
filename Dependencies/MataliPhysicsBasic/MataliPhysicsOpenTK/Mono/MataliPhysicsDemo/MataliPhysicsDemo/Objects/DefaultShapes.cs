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
    public class DefaultShapes
    {
        Demo demo;
        PhysicsScene scene;

        public DefaultShapes(Demo demo)
        {
            this.demo = demo;
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

            triangleMesh = scene.Factory.TriangleMeshManager.Create("TorusMesh1");
            triangleMesh.CreateTorusY(10, 15, 3.0f, 1.0f);
            if (!demo.Meshes.ContainsKey("TorusMesh1"))
                demo.Meshes.Add("TorusMesh1", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            scene.Factory.CreatePhysicsObjectsFromConcave("ConcaveTorus1", triangleMesh);
            scene.Factory.CreatePhysicsObjectsFromConcave("ConcaveTorus2", triangleMesh);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("ConvexTorus1");
            shapePrimitive.CreateConvexHull(triangleMesh);

            shape = scene.Factory.ShapeManager.Create("ConvexTorus1");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("UserMesh1");

            TriangleMeshRegion r01 = triangleMesh.TriangleMeshRegionManager.Create("r01");

            Vertex v1 = r01.VertexManager.Create("v1");
            v1.SetPosition(-1.0f, -1.0f, -1.0f);
            Vertex v2 = r01.VertexManager.Create("v2");
            v2.SetPosition(0.0f, -1.0f, 1.0f);
            Vertex v3 = r01.VertexManager.Create("v3");
            v3.SetPosition(1.0f, -1.0f, -1.0f);
            Vertex v4 = r01.VertexManager.Create("v4");
            v4.SetPosition(0.0f, 1.0f, 0.0f);

            Triangle t01 = r01.TriangleManager.Create("t01");
            t01.Index1 = 0;
            t01.Index2 = 1;
            t01.Index3 = 3;
            Triangle t02 = r01.TriangleManager.Create("t02");
            t02.Index1 = 0;
            t02.Index2 = 3;
            t02.Index3 = 2;
            Triangle t03 = r01.TriangleManager.Create("t03");
            t03.Index1 = 2;
            t03.Index2 = 3;
            t03.Index3 = 1;
            Triangle t04 = r01.TriangleManager.Create("t04");
            t04.Index1 = 0;
            t04.Index2 = 2;
            t04.Index3 = 1;

            triangleMesh.Update(true, true);

            if (!demo.Meshes.ContainsKey("UserMesh1"))
                demo.Meshes.Add("UserMesh1", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, false, CullFaceMode.Back, false, false));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("UserMesh1");
            shapePrimitive.CreateConvex(triangleMesh);

            shape = scene.Factory.ShapeManager.Create("UserMesh1");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Cylinder2RY");
            shapePrimitive.CreateCylinder2RY(2.0f, 2.0f, 1.0f);

            shape = scene.Factory.ShapeManager.Create("Cylinder2RY");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Cylinder2RY");
            triangleMesh.CreateCylinder2RY(1, 15, 2.0f, 2.0f, 1.0f);
            if (!demo.Meshes.ContainsKey("Cylinder2RY"))
                demo.Meshes.Add("Cylinder2RY", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("TubeMesh1");
            triangleMesh.CreateTubeY(1, 15, 2.0f, 1.0f, 0.5f, 1.0f, 0.5f);
            if (!demo.Meshes.ContainsKey("TubeMesh1"))
                demo.Meshes.Add("TubeMesh1", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));
        }

        public void Create()
        {
            Shape point = scene.Factory.ShapeManager.Find("Point");
            Shape edge = scene.Factory.ShapeManager.Find("Edge");
            Shape box = scene.Factory.ShapeManager.Find("Box");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");
            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape hemisphereZ = scene.Factory.ShapeManager.Find("HemisphereZ");
            Shape coneY = scene.Factory.ShapeManager.Find("ConeY");
            Shape capsuleY = scene.Factory.ShapeManager.Find("CapsuleY");
            Shape triangle1 = scene.Factory.ShapeManager.Find("Triangle1");
            Shape triangle2 = scene.Factory.ShapeManager.Find("Triangle2");
            Shape convexTorus1 = scene.Factory.ShapeManager.Find("ConvexTorus1");
            Shape userMesh1 = scene.Factory.ShapeManager.Find("UserMesh1");
            Shape cylinder2RY = scene.Factory.ShapeManager.Find("Cylinder2RY");

            PhysicsObject objectBase = null;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cylinder2R 1");
            objectBase.Shape = cylinder2RY;
            objectBase.UserDataStr = "Cylinder2RY";
            objectBase.CreateSound(true);
            objectBase.Sound.HitPitch = 0.5f;
            objectBase.Sound.RollPitch = 0.5f;
            objectBase.Sound.SlidePitch = 0.5f;
            objectBase.InitLocalTransform.SetPosition(10.0f, 20.0f, 10.0f);
            objectBase.InitLocalTransform.SetScale(1.5f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cylinder 1");
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, 20.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cone 1");
            objectBase.Shape = coneY;
            objectBase.UserDataStr = "ConeY";
            objectBase.Material.RigidGroup = true;
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-10.0f, 20.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(3.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Capsule 1");
            objectBase.Shape = capsuleY;
            objectBase.UserDataStr = "CapsuleY";
            objectBase.CreateSound(true);
            objectBase.Sound.MinNextImpactForce = 1500.0f;
            objectBase.InitLocalTransform.SetPosition(-20.0f, 20.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Sphere 1");
            objectBase.Shape = sphere;
            objectBase.UserDataStr = "Sphere";
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-30.0f, 20.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cylinder 2");
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, 20.0f, 10.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 2.0f, 2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Cone 2");
            objectBase.Shape = coneY;
            objectBase.UserDataStr = "ConeY";
            objectBase.Material.RigidGroup = true;
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-10.0f, 20.0f, 10.0f);
            objectBase.InitLocalTransform.SetScale(2.0f, 3.0f, 3.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Capsule 2");
            objectBase.Shape = capsuleY;
            objectBase.UserDataStr = "CapsuleY";
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-20.0f, 20.0f, 10.0f);
            objectBase.InitLocalTransform.SetScale(2.0f, 2.0f, 1.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Sphere 2");
            objectBase.Shape = sphere;
            objectBase.UserDataStr = "Sphere";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-30.0f, 20.0f, 10.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 2.0f, 2.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.Integral.EnableInertia = false;

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Box 1");
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-5.0f, 40.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 10.0f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Torus 1");
            objectBase.Shape = convexTorus1;
            objectBase.UserDataStr = "TorusMesh1";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-30.0f, 50.0f, 30.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Tube 1");
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "TubeMesh1";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-20.0f, 50.0f, 30.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Hemisphere 1");
            objectBase.Shape = hemisphereZ;
            objectBase.UserDataStr = "HemisphereZ";
            objectBase.Material.SetSpecular(0.1f, 0.1f, 0.1f);
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-40.0f, 20.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Hemisphere 2");
            objectBase.Shape = hemisphereZ;
            objectBase.UserDataStr = "HemisphereZ";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-40.0f, 20.0f, 10.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 2.0f, 2.0f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("User TriangleMesh 1");
            objectBase.Shape = userMesh1;
            objectBase.UserDataStr = "UserMesh1";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(-40.0f, 50.0f, 30.0f);
            objectBase.InitLocalTransform.SetScale(3.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.Integral.EnableInertia = false;

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Point");
            objectBase.Shape = point;
            objectBase.UserDataStr = "Point";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(0.0f, 20.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f);
            objectBase.Integral.SetDensity(2.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Edge");
            objectBase.Shape = edge;
            objectBase.UserDataStr = "Edge";
            objectBase.CreateSound(true);
            objectBase.InitLocalTransform.SetPosition(10.0f, 20.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 1.0f, 0.1f);
            objectBase.Integral.SetDensity(1.0f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Triangle 1");
            objectBase.Shape = triangle1;
            objectBase.UserDataStr = "Triangle1";
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "RollSlide";
            objectBase.InitLocalTransform.SetPosition(-10.0f, 10.0f, 30.0f);
            objectBase.InitLocalTransform.SetScale(3.0f, 10.0f, 3.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(180.0f)));
            objectBase.Integral.SetDensity(0.1f);

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Triangle 2");
            objectBase.Shape = triangle2;
            objectBase.UserDataStr = "Triangle2";
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = "RollSlide";
            objectBase.InitLocalTransform.SetPosition(0.0f, 10.0f, 30.0f);
            objectBase.InitLocalTransform.SetScale(3.0f, 5.0f, 3.0f);
            objectBase.Integral.SetDensity(0.1f);

            scene.UpdateFromInitLocalTransform(objectBase);

            PhysicsObject objectRoot = scene.Factory.PhysicsObjectManager.Create("ConcaveTorus1");
            objectRoot.UserDataStr = "TorusMesh1";

            int i = 1;
            while ((objectBase = scene.Factory.PhysicsObjectManager.Find("ConcaveTorus1 " + i.ToString())) != null)
            {
                objectBase.Shape.CreateMesh(0.0f);

                if (!demo.Meshes.ContainsKey("ConcaveTorus1 " + i.ToString()))
                    demo.Meshes.Add("ConcaveTorus1 " + i.ToString(), new DemoMesh(demo, objectBase.Shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

                objectBase.UserDataStr = "ConcaveTorus1 " + i.ToString();
                objectBase.Material.RigidGroup = true;
                objectBase.CreateSound(true);
                objectBase.Integral.SetMass(1.0f);

                objectRoot.AddChildPhysicsObject(objectBase);

                i++;
            }

            objectRoot.InitLocalTransform.SetScale(0.5f);
            objectRoot.InitLocalTransform.SetPosition(18.0f, 20.0f, 10.0f);

            scene.UpdateFromInitLocalTransform(objectRoot);

            objectRoot = scene.Factory.PhysicsObjectManager.Create("ConcaveTorus2");
            objectRoot.UserDataStr = "TorusMesh1";

            i = 1;
            while ((objectBase = scene.Factory.PhysicsObjectManager.Find("ConcaveTorus2 " + i.ToString())) != null)
            {
                objectBase.Shape.CreateMesh(0.0f);

                if (!demo.Meshes.ContainsKey("ConcaveTorus2 " + i.ToString()))
                    demo.Meshes.Add("ConcaveTorus2 " + i.ToString(), new DemoMesh(demo, objectBase.Shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));

                objectBase.UserDataStr = "ConcaveTorus2 " + i.ToString();
                objectBase.Material.RigidGroup = true;
                objectBase.CreateSound(true);
                objectBase.Integral.SetMass(1.0f);

                objectRoot.AddChildPhysicsObject(objectBase);

                i++;
            }

            objectRoot.InitLocalTransform.SetScale(0.5f);
            objectRoot.InitLocalTransform.SetPosition(19.0f, 20.0f, 10.0f);
            objectRoot.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
