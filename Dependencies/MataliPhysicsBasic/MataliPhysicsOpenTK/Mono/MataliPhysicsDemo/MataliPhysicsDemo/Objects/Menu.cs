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
    public class Menu
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Menu(Demo demo, int instanceIndex)
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

        public void Create()
        {
            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape box = scene.Factory.ShapeManager.Find("Box");
            Shape coneY = scene.Factory.ShapeManager.Find("ConeY");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            Constraint constraint = null;
            string switchInstanceName = null;

            Vector3 position1 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;

            for (int i = 0; i < 5; i++)
            {
                switchInstanceName = "Switch " + i.ToString();

                objectBase = scene.Factory.PhysicsObjectManager.Create(switchInstanceName + instanceIndexName);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.SetDiffuse(1.0f, 1.0f, 1.0f);
                objectBase.Material.TransparencyFactor = 0.9f;
                objectBase.InitLocalTransform.SetPosition(-22.0f + 11.0f * i, 25.0f, 20.0f);
                objectBase.InitLocalTransform.SetScale(5.0f, 4.0f, 0.4f);
                objectBase.Integral.SetDensity(10.0f);
                objectBase.EnableScreenToRayInteraction = true;

                objectBase.UpdateFromInitLocalTransform();

                constraint = scene.Factory.ConstraintManager.Create(switchInstanceName + " Slider Constraint" + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find(switchInstanceName + instanceIndexName);
                constraint.PhysicsObject2 = null;
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.SetAnchor1(ref position1);
                constraint.SetAnchor2(ref position1);
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.MinLimitDistanceZ = -5.0f;
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.Update();

                constraint = scene.Factory.ConstraintManager.Create(switchInstanceName + " Spring Constraint" + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find(switchInstanceName + instanceIndexName);
                constraint.PhysicsObject2 = null;
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.SetAnchor1(ref position1);
                constraint.SetAnchor2(ref position1);
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.EnableSpringMode = true;
                constraint.Force = 0.01f;
                constraint.Update();

                scene.UpdateFromInitLocalTransform(objectBase);
            }

            for (int i = 0; i < 5; i++)
            {
                switchInstanceName = "Switch " + (i + 5).ToString();

                objectBase = scene.Factory.PhysicsObjectManager.Create(switchInstanceName + instanceIndexName);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.SetDiffuse(1.0f, 1.0f, 1.0f);
                objectBase.Material.TransparencyFactor = 0.9f;
                objectBase.InitLocalTransform.SetPosition(-22.0f + 11.0f * i, 16.0f, 20.0f);
                objectBase.InitLocalTransform.SetScale(5.0f, 4.0f, 0.4f);
                objectBase.Integral.SetDensity(10.0f);
                objectBase.EnableScreenToRayInteraction = true;

                objectBase.UpdateFromInitLocalTransform();

                constraint = scene.Factory.ConstraintManager.Create(switchInstanceName + " Slider Constraint" + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find(switchInstanceName + instanceIndexName);
                constraint.PhysicsObject2 = null;
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.SetAnchor1(ref position1);
                constraint.SetAnchor2(ref position1);
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.MinLimitDistanceZ = -5.0f;
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.Update();

                constraint = scene.Factory.ConstraintManager.Create(switchInstanceName + " Spring Constraint" + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find(switchInstanceName + instanceIndexName);
                constraint.PhysicsObject2 = null;
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.SetAnchor1(ref position1);
                constraint.SetAnchor2(ref position1);
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.EnableSpringMode = true;
                constraint.Force = 0.01f;
                constraint.Update();

                scene.UpdateFromInitLocalTransform(objectBase);
            }

            objectBase = scene.Factory.PhysicsObjectManager.Create("Switch Right Light" + instanceIndexName);
            objectBase.Shape = sphere;
            objectBase.UserDataStr = "Sphere";
            objectBase.CreateLight(true);
            objectBase.Light.Type = PhysicsLightType.Point;
            objectBase.Light.SetDiffuse(0.5f, 0.5f, 0.5f);
            objectBase.Light.Range = 20.0f;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetPosition(34.0f, 20.5f, 10.0f);
            objectBase.InitLocalTransform.SetScale(20.0f);
            objectBase.EnableCollisions = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.EnableAddToCameraDrawTransparentPhysicsObjects = false;

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Switch Right" + instanceIndexName);
            objectBase.Shape = coneY;
            objectBase.UserDataStr = "ConeY";
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.Material.SetDiffuse(1.0f, 1.0f, 1.0f);
            objectBase.Material.TransparencyFactor = 0.9f;
            objectBase.InitLocalTransform.SetPosition(32.0f, 20.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(5.0f, 3.0f, 0.4f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)) * Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-10.0f)));
            objectBase.Integral.SetDensity(10.0f);
            objectBase.EnableScreenToRayInteraction = true;

            objectBase.UpdateFromInitLocalTransform();

            constraint = scene.Factory.ConstraintManager.Create("Switch Right Slider Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Switch Right" + instanceIndexName);
            constraint.PhysicsObject2 = null;
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.MinLimitDistanceZ = -5.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Switch Right Spring Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Switch Right" + instanceIndexName);
            constraint.PhysicsObject2 = null;
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.EnableSpringMode = true;
            constraint.Force = 0.01f;
            constraint.Update();

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Switch Left Light" + instanceIndexName);
            objectBase.Shape = sphere;
            objectBase.UserDataStr = "Sphere";
            objectBase.CreateLight(true);
            objectBase.Light.Type = PhysicsLightType.Point;
            objectBase.Light.SetDiffuse(0.5f, 0.5f, 0.5f);
            objectBase.Light.Range = 20.0f;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetPosition(-34.0f, 20.5f, 10.0f);
            objectBase.InitLocalTransform.SetScale(20.0f);
            objectBase.EnableCollisions = false;
            objectBase.EnableCursorInteraction = false;
            objectBase.EnableAddToCameraDrawTransparentPhysicsObjects = false;

            scene.UpdateFromInitLocalTransform(objectBase);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Switch Left" + instanceIndexName);
            objectBase.Shape = coneY;
            objectBase.UserDataStr = "ConeY";
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.Material.SetDiffuse(1.0f, 1.0f, 1.0f);
            objectBase.Material.TransparencyFactor = 0.9f;
            objectBase.InitLocalTransform.SetPosition(-32.0f, 20.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(5.0f, 3.0f, 0.4f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-90.0f)) * Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(10.0f)));
            objectBase.Integral.SetDensity(10.0f);
            objectBase.EnableScreenToRayInteraction = true;

            objectBase.UpdateFromInitLocalTransform();

            constraint = scene.Factory.ConstraintManager.Create("Switch Left Slider Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Switch Left" + instanceIndexName);
            constraint.PhysicsObject2 = null;
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.MinLimitDistanceZ = -5.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Switch Left Spring Constraint" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Switch Left" + instanceIndexName);
            constraint.PhysicsObject2 = null;
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.SetAnchor1(ref position1);
            constraint.SetAnchor2(ref position1);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.EnableSpringMode = true;
            constraint.Force = 0.01f;
            constraint.Update();

            scene.UpdateFromInitLocalTransform(objectBase);

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Info" + instanceIndexName);
            objectRoot.EnableMoving = false;
            objectRoot.DrawPriority = 1;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Info Description" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.SetAmbient(0.51f, 0.52f, 0.51f);
            objectBase.Material.SetDiffuse(1.0f, 1.0f, 1.0f);
            objectBase.Material.TransparencyFactor = 0.9f;
            objectBase.InitLocalTransform.SetPosition(0.0f, -6.0f, 20.0f);
            objectBase.InitLocalTransform.SetScale(15.0f, 27.0f, 0.4f);
            objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(90.0f)));
            objectBase.Integral.SetDensity(1.0f);
            objectBase.EnableDrawing = false;
            objectBase.EnableCollisionResponse = false;
            objectBase.EnableMoving = false;

            objectBase = scene.Factory.PhysicsObjectManager.Create("Info Screen" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.Material.UserDataStr = "Wood1";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.SetDiffuse(1.0f, 1.0f, 1.0f);
            objectBase.Material.TransparencyFactor = 0.9f;
            objectBase.InitLocalTransform.SetPosition(18.2f, 1.6f, 20.0f - 0.4f - 0.01f);
            objectBase.InitLocalTransform.SetScale(7.0f, 6.0f, 0.01f);
            objectBase.Integral.SetDensity(1.0f);
            objectBase.EnableDrawing = false;
            objectBase.EnableCollisionResponse = false;
            objectBase.EnableMoving = false;

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
