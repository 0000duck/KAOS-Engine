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
    public class PointCloth
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        float pointWidth;
        float pointHeight;

        Vector3 position1;
        Vector3 position2;

        public PointCloth(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, int pointWidth, int pointHeight, float segmentWidth, float segmentHeight, bool rigidSegment, float density, float force)
        {
            Shape point = scene.Factory.ShapeManager.Find("Point");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Point Cloth " + instanceIndexName);

            float halfLenghtWidth = 0.5f * ((pointWidth - 1) * segmentWidth);
            float halfLenghtHeight = 0.5f * ((pointHeight - 1) * segmentHeight);
            this.pointWidth = pointWidth;
            this.pointHeight = pointHeight;

            for (int i = 0; i < pointHeight; i++)
                for (int j = 0; j < pointWidth; j++)
                {

                    objectBase = scene.Factory.PhysicsObjectManager.Create("Point Cloth Point " + i.ToString() + " " + j.ToString() + instanceIndexName);
                    objectRoot.AddChildPhysicsObject(objectBase);
                    objectBase.Shape = point;
                    objectBase.UserDataStr = "Point";
                    objectBase.InitLocalTransform.SetPosition(j * segmentWidth - halfLenghtWidth, 0.0f, -i * segmentHeight + halfLenghtHeight);
                    objectBase.InitLocalTransform.SetScale(0.1f);
                    objectBase.Integral.SetDensity(density);
                }

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;

            for (int i = 0; i < pointHeight; i++)
                for (int j = 0; j < pointWidth - 1; j++)
                {
                    constraint = scene.Factory.ConstraintManager.Create("Point Cloth Horizontal Constraint " + i.ToString() + " " + j.ToString() + instanceIndexName);
                    constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + i.ToString() + " " + j.ToString() + instanceIndexName);
                    constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + i.ToString() + " " + (j + 1).ToString() + instanceIndexName);
                    constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                    constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
                    constraint.SetAnchor1(position1);
                    constraint.SetAnchor2(position2);
                    constraint.Distance = rigidSegment ? -segmentWidth : segmentWidth;
                    constraint.Force = force;
                    constraint.EnableBreak = true;
                    constraint.MinBreakVelocity = 20.0f;
                    constraint.Update();
                }

            for (int i = 0; i < pointHeight - 1; i++)
                for (int j = 0; j < pointWidth; j++)
                {
                    constraint = scene.Factory.ConstraintManager.Create("Point Cloth Vertical Constraint " + i.ToString() + " " + j.ToString() + instanceIndexName);
                    constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + i.ToString() + " " + j.ToString() + instanceIndexName);
                    constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + (i + 1).ToString() + " " + j.ToString() + instanceIndexName);
                    constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                    constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
                    constraint.SetAnchor1(position1);
                    constraint.SetAnchor2(position2);
                    constraint.Distance = rigidSegment ? -segmentHeight : segmentHeight;
                    constraint.Force = force;
                    constraint.EnableBreak = true;
                    constraint.MinBreakVelocity = 20.0f;
                    constraint.Update();
                }

            float segmentDistance = 0.0f;
            for (int i = 0; i < pointHeight - 1; i++)
                for (int j = 0; j < pointWidth - 1; j++)
                {
                    segmentDistance = rigidSegment ? (float)-Math.Sqrt(segmentWidth * segmentWidth + segmentHeight * segmentHeight) : (float)Math.Sqrt(segmentWidth * segmentWidth + segmentHeight * segmentHeight);

                    constraint = scene.Factory.ConstraintManager.Create("Point Cloth Diagonal 1 Constraint " + i.ToString() + " " + j.ToString() + instanceIndexName);
                    constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + i.ToString() + " " + j.ToString() + instanceIndexName);
                    constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + (i + 1).ToString() + " " + (j + 1).ToString() + instanceIndexName);
                    constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                    constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
                    constraint.SetAnchor1(position1);
                    constraint.SetAnchor2(position2);
                    constraint.Distance = segmentDistance;
                    constraint.Force = force;
                    constraint.EnableBreak = true;
                    constraint.MinBreakVelocity = 20.0f;
                    constraint.Update();

                    constraint = scene.Factory.ConstraintManager.Create("Point Cloth Diagonal 2 Constraint " + i.ToString() + " " + j.ToString() + instanceIndexName);
                    constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + i.ToString() + " " + (j + 1).ToString() + instanceIndexName);
                    constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + (i + 1).ToString() + " " + j.ToString() + instanceIndexName);
                    constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                    constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
                    constraint.SetAnchor1(position1);
                    constraint.SetAnchor2(position2);
                    constraint.Distance = segmentDistance;
                    constraint.Force = force;
                    constraint.EnableBreak = true;
                    constraint.MinBreakVelocity = 20.0f;
                    constraint.Update();
                }

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }

        public void Join(int physicsObjectInstanceIndex, string physicsObjectBottomLeftName, string physicsObjectBottomRightName, string physicsObjectTopRightName, string physicsObjectTopLeftName)
        {
            string physicsObjectInstanceIndexName = "";

            if (physicsObjectInstanceIndex > 0)
                physicsObjectInstanceIndexName = " " + physicsObjectInstanceIndex.ToString();

            Constraint constraint = null;

            PhysicsObject physicsObjectBottomLeft = scene.Factory.PhysicsObjectManager.Find(physicsObjectBottomLeftName + physicsObjectInstanceIndexName);

            if (physicsObjectBottomLeft != null)
            {
                constraint = scene.Factory.ConstraintManager.Create("Point Cloth Bottom Left Constraint " + instanceIndexName);
                constraint.PhysicsObject1 = physicsObjectBottomLeft;
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point 0 0" + instanceIndexName);
                constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
                constraint.SetAnchor1(position2);
                constraint.SetAnchor2(position2);
                constraint.Update();
            }

            PhysicsObject physicsObjectBottomRight = scene.Factory.PhysicsObjectManager.Find(physicsObjectBottomRightName + physicsObjectInstanceIndexName);

            if (physicsObjectBottomRight != null)
            {
                constraint = scene.Factory.ConstraintManager.Create("Point Cloth Bottom Right Constraint " + instanceIndexName);
                constraint.PhysicsObject1 = physicsObjectBottomRight;
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point 0 " + (pointWidth - 1).ToString() + instanceIndexName);
                constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
                constraint.SetAnchor1(position2);
                constraint.SetAnchor2(position2);
                constraint.Update();
            }

            PhysicsObject physicsObjectTopRight = scene.Factory.PhysicsObjectManager.Find(physicsObjectTopRightName + physicsObjectInstanceIndexName);

            if (physicsObjectTopRight != null)
            {
                constraint = scene.Factory.ConstraintManager.Create("Point Cloth Top Right Constraint " + instanceIndexName);
                constraint.PhysicsObject1 = physicsObjectTopRight;
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + (pointHeight - 1).ToString() + " " + (pointWidth - 1).ToString() + instanceIndexName);
                constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
                constraint.SetAnchor1(position2);
                constraint.SetAnchor2(position2);
                constraint.Update();
            }

            PhysicsObject physicsObjectTopLeft = scene.Factory.PhysicsObjectManager.Find(physicsObjectTopLeftName + physicsObjectInstanceIndexName);

            if (physicsObjectTopLeft != null)
            {
                constraint = scene.Factory.ConstraintManager.Create("Point Cloth Top Left Constraint " + instanceIndexName);
                constraint.PhysicsObject1 = physicsObjectTopLeft;
                constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Point Cloth Point " + (pointHeight - 1).ToString() + " 0" + instanceIndexName);
                constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
                constraint.SetAnchor1(position2);
                constraint.SetAnchor2(position2);
                constraint.Update();
            }
        }
    }
}
