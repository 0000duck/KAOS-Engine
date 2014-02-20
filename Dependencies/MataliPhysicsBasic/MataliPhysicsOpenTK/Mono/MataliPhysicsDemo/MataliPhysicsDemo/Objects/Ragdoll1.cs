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
    public class Ragdoll1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Ragdoll1(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, bool enableControl, bool enableControlWithDeformation, float minAngleDeformationVelocity)
        {
            Shape box = scene.Factory.ShapeManager.Find("Box");

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            Vector3 position1 = Vector3.Zero;
            Vector3 position2 = Vector3.Zero;
            Quaternion orientation1 = Quaternion.Identity;
            Quaternion orientation2 = Quaternion.Identity;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1" + instanceIndexName);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Head" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.InitLocalTransform.SetPosition(0.0f, 42.7f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.7f);
            objectBase.Integral.SetDensity(1.64f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Upper Torso" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.CreateSound(true);
            objectBase.Sound.HitVolume = 0.25f;
            objectBase.Sound.RollVolume = 0.25f;
            objectBase.Sound.SlideVolume = 0.25f;
            objectBase.Sound.MinFirstImpactForce = 100.0f;
            objectBase.InitLocalTransform.SetPosition(0.0f, 41.0f, 20.0f);
            objectBase.Integral.SetDensity(1.44f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Lower Torso" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.CreateSound(true);
            objectBase.Sound.HitVolume = 0.25f;
            objectBase.Sound.RollVolume = 0.25f;
            objectBase.Sound.SlideVolume = 0.25f;
            objectBase.Sound.MinFirstImpactForce = 100.0f; 
            objectBase.InitLocalTransform.SetPosition(0.0f, 39.0f, 20.0f);
            objectBase.Integral.SetDensity(1.84f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Right Upper Arm" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.InitLocalTransform.SetPosition(2.0f, 41.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 0.5f, 0.5f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Right Lower Arm" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.InitLocalTransform.SetPosition(4.0f, 41.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 0.5f, 0.5f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Right Hand" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.CreateSound(true);
            objectBase.Sound.HitVolume = 0.25f;
            objectBase.Sound.RollVolume = 0.25f;
            objectBase.Sound.SlideVolume = 0.25f;
            objectBase.Sound.MinFirstImpactForce = 100.0f;
            objectBase.InitLocalTransform.SetPosition(5.5f, 41.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.5f, 0.4f, 0.2f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Left Upper Arm" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.InitLocalTransform.SetPosition(-2.0f, 41.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 0.5f, 0.5f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Left Lower Arm" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.InitLocalTransform.SetPosition(-4.0f, 41.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(1.0f, 0.5f, 0.5f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Left Hand" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.CreateSound(true);
            objectBase.Sound.HitVolume = 0.25f;
            objectBase.Sound.RollVolume = 0.25f;
            objectBase.Sound.SlideVolume = 0.25f;
            objectBase.Sound.MinFirstImpactForce = 100.0f;
            objectBase.InitLocalTransform.SetPosition(-5.5f, 41.5f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.5f, 0.4f, 0.2f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Right Upper Leg" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.InitLocalTransform.SetPosition(0.6f, 36.75f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.5f, 1.25f, 0.5f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Right Lower Leg" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.InitLocalTransform.SetPosition(0.6f, 34.25f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.5f, 1.25f, 0.5f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Right Foot" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.CreateSound(true);
            objectBase.Sound.HitVolume = 0.25f;
            objectBase.Sound.RollVolume = 0.25f;
            objectBase.Sound.SlideVolume = 0.25f;
            objectBase.Sound.MinFirstImpactForce = 100.0f;
            objectBase.InitLocalTransform.SetPosition(0.6f, 32.8f, 19.7f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.2f, 0.8f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Left Upper Leg" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.InitLocalTransform.SetPosition(-0.6f, 36.75f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.5f, 1.25f, 0.5f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Left Lower Leg" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.InitLocalTransform.SetPosition(-0.6f, 34.25f, 20.0f);
            objectBase.InitLocalTransform.SetScale(0.5f, 1.25f, 0.5f);
            objectBase.Integral.SetDensity(2.145f);

            objectBase = scene.Factory.PhysicsObjectManager.Create("Ragdoll 1 Left Foot" + instanceIndexName);
            objectRoot.AddChildPhysicsObject(objectBase);
            objectBase.Shape = box;
            objectBase.UserDataStr = "Box";
            objectBase.CreateSound(true);
            objectBase.Sound.HitVolume = 0.25f;
            objectBase.Sound.RollVolume = 0.25f;
            objectBase.Sound.SlideVolume = 0.25f;
            objectBase.Sound.MinFirstImpactForce = 100.0f;
            objectBase.InitLocalTransform.SetPosition(-0.6f, 32.8f, 19.7f);
            objectBase.InitLocalTransform.SetScale(0.4f, 0.2f, 0.8f);
            objectBase.Integral.SetDensity(2.145f);

            objectRoot.UpdateFromInitLocalTransform();

            Constraint constraint = null;
            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 1" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Head" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Upper Torso" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 0.7f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 0.7f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -20.0f;
            constraint.MaxLimitDegAngleX = 20.0f;
            constraint.MinLimitDegAngleY = -60.0f;
            constraint.MaxLimitDegAngleY = 60.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 2" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Upper Torso" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Lower Torso" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 1.0f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 1.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -10.0f;
            constraint.MaxLimitDegAngleX = 45.0f;
            constraint.MinLimitDegAngleZ = -10.0f;
            constraint.MaxLimitDegAngleZ = 10.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 3" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Upper Torso" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Upper Arm" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -20.0f;
            constraint.MaxLimitDegAngleY = 90.0f;
            constraint.MinLimitDegAngleZ = -90.0f;
            constraint.MaxLimitDegAngleZ = 45.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 4" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Upper Torso" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Upper Arm" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -90.0f;
            constraint.MaxLimitDegAngleY = 20.0f;
            constraint.MinLimitDegAngleZ = -45.0f;
            constraint.MaxLimitDegAngleZ = 90.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 5" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Lower Torso" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Upper Leg" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -10.0f;
            constraint.MaxLimitDegAngleX = 100.0f;
            constraint.MinLimitDegAngleY = -20.0f;
            constraint.MaxLimitDegAngleY = 0.0f;
            constraint.MinLimitDegAngleZ = -45.0f;
            constraint.MaxLimitDegAngleZ = 45.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 6" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Lower Torso" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Upper Leg" + instanceIndexName);
            constraint.PhysicsObject2.MainWorldTransform.GetPosition(ref position2);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position2 + new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetAnchor2(position2 + new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -10.0f;
            constraint.MaxLimitDegAngleX = 100.0f;
            constraint.MinLimitDegAngleY = -20.0f;
            constraint.MaxLimitDegAngleY = 0.0f;
            constraint.MinLimitDegAngleZ = -45.0f;
            constraint.MaxLimitDegAngleZ = 45.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 7" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Upper Arm" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Lower Arm" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -2.0f;
            constraint.MaxLimitDegAngleY = 135.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 8" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Lower Arm" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Hand" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 + new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -20.0f;
            constraint.MaxLimitDegAngleY = 60.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 9" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Upper Arm" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Lower Arm" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -135.0f;
            constraint.MaxLimitDegAngleY = 2.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 10" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Lower Arm" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Hand" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(1.0f, 0.0f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleY = -60.0f;
            constraint.MaxLimitDegAngleY = 20.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 11" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Upper Leg" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Lower Leg" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -135.0f;
            constraint.MaxLimitDegAngleX = 2.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 12" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Lower Leg" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Right Foot" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -45.0f;
            constraint.MaxLimitDegAngleX = 2.0f;
            constraint.MinLimitDegAngleZ = -10.0f;
            constraint.MaxLimitDegAngleZ = 10.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 13" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Upper Leg" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Lower Leg" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -135.0f;
            constraint.MaxLimitDegAngleX = 2.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            constraint = scene.Factory.ConstraintManager.Create("Ragdoll 1 Constraint 14" + instanceIndexName);
            constraint.PhysicsObject1 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Lower Leg" + instanceIndexName);
            constraint.PhysicsObject2 = scene.Factory.PhysicsObjectManager.Find("Ragdoll 1 Left Foot" + instanceIndexName);
            constraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
            constraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
            constraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);
            constraint.SetAnchor1(position1 - new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetAnchor2(position1 - new Vector3(0.0f, 1.25f, 0.0f));
            constraint.SetInitWorldOrientation1(ref orientation1);
            constraint.SetInitWorldOrientation2(ref orientation2);
            constraint.EnableBreak = true;
            constraint.MinBreakVelocity = 300.0f;
            constraint.EnableLimitAngleX = true;
            constraint.EnableLimitAngleY = true;
            constraint.EnableLimitAngleZ = true;
            constraint.MinLimitDegAngleX = -45.0f;
            constraint.MaxLimitDegAngleX = 2.0f;
            constraint.MinLimitDegAngleZ = -10.0f;
            constraint.MaxLimitDegAngleZ = 10.0f;
            constraint.LimitAngleMode = LimitAngleMode.EulerYZX;

            if (enableControl)
            {
                constraint.EnableControlAngleX = true;
                constraint.EnableControlAngleY = true;
                constraint.EnableControlAngleZ = true;
            }
            else
                if (enableControlWithDeformation)
                {
                    constraint.EnableControlAngleX = true;
                    constraint.EnableControlAngleY = true;
                    constraint.EnableControlAngleZ = true;

                    constraint.EnableControlAngleWithDeformation = true;
                    constraint.MinAngleDeformationVelocity = minAngleDeformationVelocity;
                    constraint.AngularDamping = 1.0f;
                }

            constraint.Update();

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
