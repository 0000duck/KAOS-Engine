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
    public class Rope
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Rope(Demo demo, int instanceIndex)
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

        public void Create(PhysicsObject startObject, PhysicsObject endObject, Vector3 startPosition, Vector3 endPosition, int ropeSegmentCount)
        {
            Shape cylinderY = scene.Factory.ShapeManager.Find("CylinderY");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            Vector3 position1 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Rope " + instanceIndexName);

            endPosition.Y = startPosition.Y;
            Vector3 distance = startPosition - endPosition;
            float length = distance.Length;
            float ropeLength = length / ropeSegmentCount;
            Vector3 scale = new Vector3(0.2f, 0.2f, ropeLength * 0.5f);

            for (int i = 0; i < ropeSegmentCount; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Rope Segment " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = cylinderY;
                objectBase.UserDataStr = "CylinderY";
                objectBase.Material.UserDataStr = "Plastic2";
                objectBase.Material.SetSpecular(0.2f, 0.2f, 0.2f);
                objectBase.InitLocalTransform.SetPosition(startPosition.X, startPosition.Y, startPosition.Z + i * 2.0f * scale.Z + scale.Z);
                objectBase.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));
                objectBase.InitLocalTransform.SetScale(0.2f, ropeLength * 0.5f, 0.2f);
                objectBase.Integral.SetDensity(0.1f);
                objectBase.CreateSound(true);
            }

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Rope Constraint 0" + instanceIndexName);
            constraint.PhysicsObject1 = startObject;
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Rope Segment 0" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref startPosition);
            constraint.SetAnchor2(ref startPosition);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.Update();

            for (int i = 0; i < ropeSegmentCount - 1; i++)
            {
                constraint = scene.Factory.ConstraintManager.Create("Rope Constraint " + (i + 1).ToString() + instanceIndexName);
                constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Rope Segment " + i.ToString() + instanceIndexName);
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Rope Segment " + (i + 1).ToString() + instanceIndexName);
                constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
                constraint.SetAnchor1(position1 + new Vector3(0.0f, 0.0f, scale.Z));
                constraint.SetAnchor2(position1 + new Vector3(0.0f, 0.0f, scale.Z));
                constraint.SetInitWorldOrientation1(ref orientation1);
                constraint.SetInitWorldOrientation2(ref orientation2);
                constraint.EnableLimitAngleX = true;
                constraint.EnableLimitAngleY = true;
                constraint.EnableLimitAngleZ = true;
                constraint.EnableBreak = true;
                constraint.AngularDamping = 0.1f;
                constraint.EnableLimitAngleSpringMode = true;
                constraint.Update();
            }

            constraint = scene.Factory.ConstraintManager.Create("Rope Constraint " + (ropeSegmentCount + 1).ToString() + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Rope Segment " + (ropeSegmentCount - 1).ToString() + instanceIndexName);
            constraint.PhysicsObject2 = endObject;
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(ref endPosition);
            constraint.SetAnchor2(ref endPosition);
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.Update();

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
