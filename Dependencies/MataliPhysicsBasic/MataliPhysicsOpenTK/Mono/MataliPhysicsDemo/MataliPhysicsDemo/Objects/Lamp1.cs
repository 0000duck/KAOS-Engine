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
    public class Lamp1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Lamp1(Demo demo, int instanceIndex)
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

            Vector3[] convexTab1 = new Vector3[16];
            convexTab1[0] = new Vector3(-0.5f, 1.0f, -1.0f);
            convexTab1[1] = new Vector3(-1.0f, 1.0f, -0.5f);
            convexTab1[2] = new Vector3(-1.0f, 1.0f, 0.5f);
            convexTab1[3] = new Vector3(-0.5f, 1.0f, 1.0f);
            convexTab1[4] = new Vector3(0.5f, 1.0f, 1.0f);
            convexTab1[5] = new Vector3(1.0f, 1.0f, 0.5f);
            convexTab1[6] = new Vector3(1.0f, 1.0f, -0.5f);
            convexTab1[7] = new Vector3(0.5f, 1.0f, -1.0f);
            convexTab1[8] = new Vector3(-0.25f, -1.0f, -0.5f);
            convexTab1[9] = new Vector3(-0.5f, -1.0f, -0.25f);
            convexTab1[10] = new Vector3(-0.5f, -1.0f, 0.25f);
            convexTab1[11] = new Vector3(-0.25f, -1.0f, 0.5f);
            convexTab1[12] = new Vector3(0.25f, -1.0f, 0.5f);
            convexTab1[13] = new Vector3(0.5f, -1.0f, 0.25f);
            convexTab1[14] = new Vector3(0.5f, -1.0f, -0.25f);
            convexTab1[15] = new Vector3(0.25f, -1.0f, -0.5f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Lamp1Convex");
            shapePrimitive.CreateConvex(convexTab1);

            shape = scene.Factory.ShapeManager.Create("Lamp1Convex");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Lamp1Convex"))
                demo.Meshes.Add("Lamp1Convex", new DemoMesh(demo, shape, demo.Textures["Default"], Vector2.One, false, false, false, false, true, CullFaceMode.Back, false, false));
        }

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, PhysicsObject joinPhysicsObject)
        {
            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");
            Shape convex = scene.Factory.ShapeManager.Find("Lamp1Convex");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            PhysicsObject objectA = null;

            Vector3 position1 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Lamp 1" + instanceIndexName);

            objectA = scene.Factory.PhysicsObjectManager.Create("Lamp 1 Body" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectA);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Lamp 1 Body Main" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = convex;
            objectBase.UserDataStr = "Lamp1Convex";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 0.0f, 0.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 2.0f, 1.0f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Lamp 1 Body Emitter" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 2.2f, 0.0f);
            objectBase.InitLocalTransform.SetScale(0.1f, 0.2f, 0.1f);
            objectBase.Integral.InertiaScaleFactor = 3.0f;
            objectBase.Integral.SetDensity(1.0f);
            objectBase.MinResponseAngularVelocity = 0.05f;
            objectBase.MinResponseLinearVelocity = 0.05f;
            objectBase.CreateSound(true);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Lamp 1 Body Light" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = sphere;
            objectBase.UserDataStr = "Sphere";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetPosition(0.0f, 2.2f, 0.0f);
            objectBase.InitLocalTransform.SetScale(15.0f);
            objectBase.CreateLight(true);
            objectBase.Light.Type = PhysicsLightType.Point;
            objectBase.Light.SetDiffuse(1.0f, 0.8f, 0.1f);
            objectBase.Light.Range = 15.0f;
            objectBase.EnableBreakRigidGroup = false;
            objectBase.EnableCollisions = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.EnableAddToCameraDrawTransparentPhysicsObjects = false;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Lamp 1 Body Handle" + instanceIndexName);
            objectA.AddChildPhysicsObject(objectBase);
            objectBase.Shape = cylinderY;
            objectBase.UserDataStr = "CylinderY";
            objectBase.Material.UserDataStr = "Iron";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.MinBreakRigidGroupVelocity = 400.0f;
            objectBase.InitLocalTransform.SetPosition(-2.0f, 0.0f, 0.0f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.InitLocalTransform.SetScale(0.5f, 2.0f, 0.5f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.CreateSound(true);

            objectRoot.UpdateFromInitLocalTransform();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Lamp 1 Constraint 1" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Lamp 1 Body Handle" + instanceIndexName);
            constraint.PhysicsObject2 = joinPhysicsObject;
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.SetAnchor1(position1 + new Vector3(-2.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(-2.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(orientation1);
            constraint.SetInitWorldOrientation2(orientation1);
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.Update();
        }
    }
}
