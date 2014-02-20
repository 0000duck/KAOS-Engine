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
    public class Bridge2
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Bridge2(Demo demo, int instanceIndex)
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

        public void Create(PhysicsObject startObject, PhysicsObject endObject, Vector3 startPosition, Vector3 endPosition, int boardCount, Vector2 boardXYScale)
        {
            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");
            Shape box = scene.Factory.ShapeManager.Find("Box");
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            PhysicsObject objectTruss = null;

            Vector3 position1 = Vector3.Zero;
            Vector3 position2 = Vector3.Zero;
            Vector3 scale1 = Vector3.One;
            Vector3 scale2 = Vector3.One;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Bridge 2 " + instanceIndexName);

            endPosition.Y = startPosition.Y;
            Vector3 distance = startPosition - endPosition;
            float length = distance.Length;
            float boardLength = length / boardCount;
            Vector3 boardScale = new Vector3(boardXYScale.X, boardXYScale.Y, boardLength * 0.5f);

            for (int i = 0; i < boardCount; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 2 Board " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.InitLocalTransform.SetPosition(startPosition.X, startPosition.Y, startPosition.Z + i * 2.0f * boardScale.Z + boardScale.Z);
                objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(90.0f)));
                objectBase.InitLocalTransform.SetScale(boardLength * 0.5f, 0.4f, 15.0f);
                objectBase.Integral.SetDensity(1.0f);
                objectBase.CreateSound(true);
            }

            int trussCount = boardCount / 2;
            for (int i = 0; i < trussCount; i++)
            {
                objectTruss = scene.Factory.PhysicsObjectManager.Create("Bridge 2 Truss Right " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectTruss);

                objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 2 Truss Right Base " + i.ToString() + instanceIndexName);
                objectTruss.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.RigidGroup = true;
                objectBase.InitLocalTransform.SetPosition(startPosition.X + 14.0f, startPosition.Y + boardLength * 0.25f + 0.4f, startPosition.Z + i * 4.0f * boardScale.Z + boardScale.Z);
                objectBase.InitLocalTransform.SetScale(0.4f, boardLength * 0.25f, 0.4f);
                objectBase.Integral.SetDensity(1.0f);
                objectBase.EnableBreakRigidGroup = false;
                objectBase.CreateSound(true);

                objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 2 Truss Right Light Base " + i.ToString() + instanceIndexName);
                objectTruss.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.TwoSidedNormals = true;
                objectBase.Material.UserDataStr = "Yellow";
                objectBase.InitLocalTransform.SetPosition(startPosition.X + 14.0f, startPosition.Y + boardLength * 0.5f + 1.0f, startPosition.Z + i * 4.0f * boardScale.Z + boardScale.Z);
                objectBase.InitLocalTransform.SetScale(0.6f);
                objectBase.Integral.SetDensity(1.0f);
                objectBase.EnableBreakRigidGroup = false;
                objectBase.CreateSound(true);
                objectBase.Sound.UserDataStr = "Glass";

                objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 2 Truss Right Light " + i.ToString() + instanceIndexName);
                objectTruss.AddChildPhysicsObject(objectBase);
                objectBase.Shape = sphere;
                objectBase.UserDataStr = "Sphere";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.UserDataStr = "Yellow";
                objectBase.InitLocalTransform.SetPosition(startPosition.X + 14.0f, startPosition.Y + boardLength * 0.5f + 1.0f, startPosition.Z + i * 4.0f * boardScale.Z + boardScale.Z);
                objectBase.InitLocalTransform.SetScale(15.0f);
                objectBase.CreateLight(true);
                objectBase.Light.Type = PhysicsLightType.Point;
                objectBase.Light.SetDiffuse(1.0f, 0.7f, 0.0f);
                objectBase.Light.Range = 15.0f;
                objectBase.EnableBreakRigidGroup = false;
                objectBase.EnableCollisions = false;
                objectBase.EnableCursorInteraction = false;
                objectBase.EnableAddToCameraDrawTransparentPhysicsObjects = false;
            }

            for (int i = 0; i < trussCount; i++)
            {
                objectTruss = scene.Factory.PhysicsObjectManager.Create("Bridge 2 Truss Left " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectTruss);

                objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 2 Truss Left Base " + i.ToString() + instanceIndexName);
                objectTruss.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.UserDataStr = "Wood1";
                objectBase.Material.RigidGroup = true;
                objectBase.InitLocalTransform.SetPosition(startPosition.X - 14.0f, startPosition.Y + boardLength * 0.25f + 0.4f, startPosition.Z + i * 4.0f * boardScale.Z + boardScale.Z);
                objectBase.InitLocalTransform.SetScale(0.4f, boardLength * 0.25f, 0.4f);
                objectBase.Integral.SetDensity(1.0f);
                objectBase.EnableBreakRigidGroup = false;
                objectBase.CreateSound(true);

                objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 2 Truss Left Light Base " + i.ToString() + instanceIndexName);
                objectTruss.AddChildPhysicsObject(objectBase);
                objectBase.Shape = box;
                objectBase.UserDataStr = "Box";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.TwoSidedNormals = true;
                objectBase.Material.UserDataStr = "Yellow";
                objectBase.InitLocalTransform.SetPosition(startPosition.X - 14.0f, startPosition.Y + boardLength * 0.5f + 1.0f, startPosition.Z + i * 4.0f * boardScale.Z + boardScale.Z);
                objectBase.InitLocalTransform.SetScale(0.6f);
                objectBase.Integral.SetDensity(1.0f);
                objectBase.EnableBreakRigidGroup = false;
                objectBase.CreateSound(true);
                objectBase.Sound.UserDataStr = "Glass";

                objectBase = scene.Factory.PhysicsObjectManager.Create("Bridge 2 Truss Left Light " + i.ToString() + instanceIndexName);
                objectTruss.AddChildPhysicsObject(objectBase);
                objectBase.Shape = sphere;
                objectBase.UserDataStr = "Sphere";
                objectBase.Material.RigidGroup = true;
                objectBase.Material.UserDataStr = "Yellow";
                objectBase.InitLocalTransform.SetPosition(startPosition.X - 14.0f, startPosition.Y + boardLength * 0.5f + 1.0f, startPosition.Z + i * 4.0f * boardScale.Z + boardScale.Z);
                objectBase.InitLocalTransform.SetScale(15.0f);
                objectBase.CreateLight(true);
                objectBase.Light.Type = PhysicsLightType.Point;
                objectBase.Light.SetDiffuse(1.0f, 0.7f, 0.0f);
                objectBase.Light.Range = 15.0f;
                objectBase.EnableBreakRigidGroup = false;
                objectBase.EnableCollisions = false;
                objectBase.EnableCursorInteraction = false;
                objectBase.EnableAddToCameraDrawTransparentPhysicsObjects = false;
            }

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;

            for (int i = 0; i < trussCount; i++)
            {
                constraint = scene.Factory.ConstraintManager.Create("Bridge 2 Truss Right Constraint " + i.ToString() + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Truss Right Base " + i.ToString() + instanceIndexName);
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Board " + (i * 2).ToString() + instanceIndexName);
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
                constraint.SetAnchor1(position1 - new Vector3(0.0f, boardLength * 0.25f, 0.0f));
                constraint.SetAnchor2(position1 - new Vector3(0.0f, boardLength * 0.25f, 0.0f));
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.SetInitWorldOrientation2(ref orientation2);
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.EnableBreak = true;
                constraint.Update();
            }

            for (int i = 0; i < trussCount; i++)
            {
                constraint = scene.Factory.ConstraintManager.Create("Bridge 2 Truss Left Constraint " + i.ToString() + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Truss Left Base " + i.ToString() + instanceIndexName);
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Board " + (i * 2).ToString() + instanceIndexName);
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
                constraint.SetAnchor1(position1 - new Vector3(0.0f, boardLength * 0.25f, 0.0f));
                constraint.SetAnchor2(position1 - new Vector3(0.0f, boardLength * 0.25f, 0.0f));
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.SetInitWorldOrientation2(ref orientation2);
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.EnableBreak = true;
                constraint.Update();
            }

            constraint = scene.Factory.ConstraintManager.Create("Bridge 2 Constraint 0" + instanceIndexName);
            constraint.PhysicsObject1 = startObject;
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Board 0" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref startPosition);
            constraint.SetAnchor2(ref startPosition);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.AngularDamping = 0.1f;
            constraint.Update();

            for (int i = 0; i < boardCount - 1; i++)
            {
                constraint = scene.Factory.ConstraintManager.Create("Bridge 2 Constraint " + (i + 1).ToString() + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Board " + i.ToString() + instanceIndexName);
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Board " + (i + 1).ToString() + instanceIndexName);
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
                constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, boardScale.Z));
                constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, boardScale.Z));
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.SetInitWorldOrientation2(ref orientation2);
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.EnableBreak = true;
                constraint.AngularDamping = 0.1f;
                constraint.Update();
            }

            constraint = scene.Factory.ConstraintManager.Create("Bridge 2 Constraint " + (boardCount + 1).ToString() + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Board " + (boardCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject2 = endObject;
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref endPosition);
            constraint.SetAnchor2(ref endPosition);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.EnableBreak = true;
            constraint.AngularDamping = 0.1f;
            constraint.Update();

            scene.UpdateFromInitLocalTransform(objectRoot);

            Rope rope = null;
            int ropeSegmentCount = 4;
            PhysicsObject ropeStartObject = null;
            PhysicsObject ropeEndObject = null;

            for (int i = 0; i < trussCount - 1; i++)
            {
                ropeStartObject = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Truss Left Base " + i.ToString() + instanceIndexName);
                ropeEndObject = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Truss Left Base " + (i + 1).ToString() + instanceIndexName);

                ropeStartObject.MainWorldTransform.GetPosition(ref position1);
                ropeEndObject.MainWorldTransform.GetPosition(ref position2);
                ropeStartObject.MainWorldTransform.GetScale(ref scale1);
                ropeEndObject.MainWorldTransform.GetScale(ref scale2);

                position1 += new Vector3(0.0f, scale1.Y - scale1.Z, scale1.Z);
                position2 += new Vector3(0.0f, scale2.Y - scale1.Z, -scale2.Z);

                rope = new Rope(demo, i);
                rope.Initialize(scene);
                rope.Create(ropeStartObject, ropeEndObject, position1, position2, ropeSegmentCount);
            }

            for (int i = 0; i < trussCount - 1; i++)
            {
                ropeStartObject = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Truss Right Base " + i.ToString() + instanceIndexName);
                ropeEndObject = scene.Factory.PhysicsObjectManager.Find("Bridge 2 Truss Right Base " + (i + 1).ToString() + instanceIndexName);

                ropeStartObject.MainWorldTransform.GetPosition(ref position1);
                ropeEndObject.MainWorldTransform.GetPosition(ref position2);
                ropeStartObject.MainWorldTransform.GetScale(ref scale1);
                ropeEndObject.MainWorldTransform.GetScale(ref scale2);

                position1 += new Vector3(0.0f, scale1.Y - scale1.Z, scale1.Z);
                position2 += new Vector3(0.0f, scale2.Y - scale1.Z, -scale2.Z);

                rope = new Rope(demo, i + trussCount);
                rope.Initialize(scene);
                rope.Create(ropeStartObject, ropeEndObject, position1, position2, ropeSegmentCount);
            }
        }
    }
}
