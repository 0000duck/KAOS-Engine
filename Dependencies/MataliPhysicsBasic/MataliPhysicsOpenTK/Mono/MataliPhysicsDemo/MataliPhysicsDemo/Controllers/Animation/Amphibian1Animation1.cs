/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Amphibian1Animation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string sphereName;
        string shot1Name;
        string shot1BaseName;
        string shot1LightName;
        string shot2Name;
        string shot2BaseName;
        string shot2LightName;
        string yellowName;

        PhysicsObject shot;
        PhysicsObject shotBase;
        PhysicsObject shotLight;

        Shape sphere;
        PhysicsObject turretGun;
        Constraint turretConstraint;
        Constraint turretGunConstraint;
        PhysicsObject turretGun1;
        PhysicsObject turretGun2;
        PhysicsObject turretBodyDown;

        int shotCount;
        int maxShotCount;

        DemoKeyboardState oldKeyboardState;

        Vector3 vectorZero;
        Vector3 unitY;
        Matrix4 matrixIdentity;

        public Amphibian1Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            sphereName = "Sphere";
            shot1Name = "Amphibian 1 Gun 1 Shot" + instanceIndexName + " ";
            shot1BaseName = "Amphibian 1 Gun 1 Shot Base" + instanceIndexName + " ";
            shot1LightName = "Amphibian 1 Gun 1 Shot Light" + instanceIndexName + " ";
            shot2Name = "Amphibian 1 Gun 2 Shot" + instanceIndexName + " ";
            shot2BaseName = "Amphibian 1 Gun 2 Shot Base" + instanceIndexName + " ";
            shot2LightName = "Amphibian 1 Gun 2 Shot Light" + instanceIndexName + " ";
            yellowName = "Yellow";

            maxShotCount = 10;

            vectorZero = Vector3.Zero;
            unitY = Vector3.UnitY;
            matrixIdentity = Matrix4.Identity;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            sphere = scene.Factory.ShapeManager.Find("Sphere");
            turretGun = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Gun" + instanceIndexName);
            turretConstraint = scene.Factory.ConstraintManager.Find("Amphibian 1 Turret Constraint" + instanceIndexName);
            turretGunConstraint = scene.Factory.ConstraintManager.Find("Amphibian 1 Turret Gun Constraint" + instanceIndexName);

            turretGun1 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Gun 1" + instanceIndexName);
            turretGun2 = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Gun 2" + instanceIndexName);
            turretBodyDown = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Turret Body Down" + instanceIndexName);

            shotCount = -1;

            oldKeyboardState = demo.GetKeyboardState();

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Amphibian 1 Body" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Move);
        }

        public void Move(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if ((turretGun == null) || (turretConstraint == null) || (turretGunConstraint == null))
                return;

            Vector3 deltaTranslation = vectorZero;

            bool turretWorking = true;
            if (objectBase.IsBrokenRigidGroup || turretConstraint.IsBroken)
                turretWorking = false;

            bool turretGunWorking = true;
            if (!turretWorking || turretGun.IsBrokenRigidGroup || turretGunConstraint.IsBroken)
                turretGunWorking = false;

            DemoKeyboardState keyboardState = demo.GetKeyboardState();

            if (keyboardState[Key.Up])
                deltaTranslation.Z += 10000.0f;

            if (keyboardState[Key.Down])
                deltaTranslation.Z -= 10000.0f;

            if (deltaTranslation.LengthSquared != 0.0f)
                objectBase.WorldAccumulator.AddLocalForce(ref deltaTranslation);

            if (turretWorking)
            {
                if (keyboardState[Key.Right])
                    turretConstraint.ControlDegAngleY -= 2.0f;

                if (keyboardState[Key.Left])
                    turretConstraint.ControlDegAngleY += 2.0f;
            }

            if (turretGunWorking)
            {
                if (keyboardState[Key.PageUp])
                    turretGunConstraint.ControlDegAngleX += 1.0f;

                if (keyboardState[Key.PageDown])
                    turretGunConstraint.ControlDegAngleX -= 1.0f;

                if (keyboardState[Key.F] && !oldKeyboardState[Key.F])
                {
                    shotCount = (shotCount + 1) % maxShotCount;
                    string shotCountName = shotCount.ToString();

                    if (turretGun1 != null)
                    {
                        shot = scene.Factory.PhysicsObjectManager.FindOrCreate(shot1Name + shotCountName);
                        shotBase = scene.Factory.PhysicsObjectManager.FindOrCreate(shot1BaseName + shotCountName);
                        shotLight = scene.Factory.PhysicsObjectManager.FindOrCreate(shot1LightName + shotCountName);

                        shot.AddChildPhysicsObject(shotBase);
                        shot.AddChildPhysicsObject(shotLight);

                        Vector3 turretGunPosition = vectorZero;
                        Matrix4 turretGunRotation = matrixIdentity;

                        turretGun1.MainWorldTransform.GetPosition(ref turretGunPosition);
                        turretGun1.MainWorldTransform.GetRotation(ref turretGunRotation);

                        Vector3 shotPosition = vectorZero;
                        Vector3 shotLocalPosition = vectorZero;

                        shotLocalPosition.X = 0.0f;
                        shotLocalPosition.Y = 2.0f;
                        shotLocalPosition.Z = 0.0f;

                        Vector3.TransformVector(ref shotLocalPosition, ref turretGunRotation, out shotPosition);
                        Vector3.Add(ref shotPosition, ref turretGunPosition, out shotPosition);

                        Vector3 shotDirection = vectorZero;
                        Vector3 shotScale = vectorZero;

                        shotDirection.X = turretGunRotation.Row1.X;
                        shotDirection.Y = turretGunRotation.Row1.Y;
                        shotDirection.Z = turretGunRotation.Row1.Z;

                        shotScale.X = shotScale.Y = shotScale.Z = 0.5f;

                        shot.InitLocalTransform.SetRotation(ref matrixIdentity);
                        shot.InitLocalTransform.SetPosition(ref shotPosition);

                        Vector3.Multiply(ref shotDirection, 200.0f, out shotDirection);

                        shot.InitLocalTransform.SetLinearVelocity(ref shotDirection);
                        shot.InitLocalTransform.SetAngularVelocity(ref vectorZero);
                        shot.MaxSimulationFrameCount = 100;
                        shot.EnableRemovePhysicsObjectsFromManagerAfterMaxSimulationFrameCount = false;

                        shotBase.Shape = sphere;
                        shotBase.UserDataStr = sphereName;
                        shotBase.Material.RigidGroup = true;
                        shotBase.InitLocalTransform.SetScale(ref shotScale);
                        shotBase.Integral.SetDensity(1.0f);
                        shotBase.EnableCollisions = true;
                        shotBase.DisableCollision(turretGun1, true);
                        shotBase.MaxDisableCollisionFrameCount = 10;
                        shotBase.EnableBreakRigidGroup = false;
                        shotBase.CreateSound(true);

                        shotLight.Shape = sphere;
                        shotLight.UserDataStr = sphereName;
                        shotLight.CreateLight(true);
                        shotLight.Light.Type = PhysicsLightType.Point;
                        shotLight.Light.Range = 20.0f;
                        shotLight.Light.SetDiffuse(1.0f, 0.7f, 0.0f);
                        shotLight.InitLocalTransform.SetScale(20.0f);
                        shotLight.Material.RigidGroup = true;
                        shotLight.Material.UserDataStr = yellowName;
                        shotLight.EnableBreakRigidGroup = false;
                        shotLight.EnableCollisions = false;
                        shotLight.EnableCursorInteraction = false;
                        shotLight.EnableAddToCameraDrawTransparentPhysicsObjects = false;

                        scene.UpdateFromInitLocalTransform(shot);
                    }

                    if (turretGun2 != null)
                    {
                        shot = scene.Factory.PhysicsObjectManager.FindOrCreate(shot2Name + shotCountName);
                        shotBase = scene.Factory.PhysicsObjectManager.FindOrCreate(shot2BaseName + shotCountName);
                        shotLight = scene.Factory.PhysicsObjectManager.FindOrCreate(shot2LightName + shotCountName);

                        shot.AddChildPhysicsObject(shotBase);
                        shot.AddChildPhysicsObject(shotLight);

                        Vector3 turretGunPosition = vectorZero;
                        Matrix4 turretGunRotation = matrixIdentity;

                        turretGun2.MainWorldTransform.GetPosition(ref turretGunPosition);
                        turretGun2.MainWorldTransform.GetRotation(ref turretGunRotation);

                        Vector3 shotPosition = vectorZero;
                        Vector3 shotLocalPosition = vectorZero;

                        shotLocalPosition.X = 0.0f;
                        shotLocalPosition.Y = 2.0f;
                        shotLocalPosition.Z = 0.0f;

                        Vector3.TransformVector(ref shotLocalPosition, ref turretGunRotation, out shotPosition);
                        Vector3.Add(ref shotPosition, ref turretGunPosition, out shotPosition);

                        Vector3 shotDirection = vectorZero;
                        Vector3 shotScale = vectorZero;

                        shotDirection.X = turretGunRotation.Row1.X;
                        shotDirection.Y = turretGunRotation.Row1.Y;
                        shotDirection.Z = turretGunRotation.Row1.Z;

                        shotScale.X = shotScale.Y = shotScale.Z = 0.5f;

                        shot.InitLocalTransform.SetRotation(ref matrixIdentity);
                        shot.InitLocalTransform.SetPosition(ref shotPosition);

                        Vector3.Multiply(ref shotDirection, 200.0f, out shotDirection);

                        shot.InitLocalTransform.SetLinearVelocity(ref shotDirection);
                        shot.InitLocalTransform.SetAngularVelocity(ref vectorZero);
                        shot.MaxSimulationFrameCount = 100;
                        shot.EnableRemovePhysicsObjectsFromManagerAfterMaxSimulationFrameCount = false;

                        shotBase.Shape = sphere;
                        shotBase.UserDataStr = sphereName;
                        shotBase.Material.RigidGroup = true;
                        shotBase.InitLocalTransform.SetScale(ref shotScale);
                        shotBase.Integral.SetDensity(1.0f);
                        shotBase.EnableCollisions = true;
                        shotBase.DisableCollision(turretGun1, true);
                        shotBase.MaxDisableCollisionFrameCount = 10;
                        shotBase.EnableBreakRigidGroup = false;
                        shotBase.CreateSound(true);

                        shotLight.Shape = sphere;
                        shotLight.UserDataStr = sphereName;
                        shotLight.CreateLight(true);
                        shotLight.Light.Type = PhysicsLightType.Point;
                        shotLight.Light.Range = 20.0f;
                        shotLight.Light.SetDiffuse(1.0f, 0.7f, 0.0f);
                        shotLight.InitLocalTransform.SetScale(20.0f);
                        shotLight.Material.RigidGroup = true;
                        shotLight.Material.UserDataStr = yellowName;
                        shotLight.EnableBreakRigidGroup = false;
                        shotLight.EnableCollisions = false;
                        shotLight.EnableCursorInteraction = false;
                        shotLight.EnableAddToCameraDrawTransparentPhysicsObjects = false;

                        scene.UpdateFromInitLocalTransform(shot);
                    }
                }
            }
            else
            {
                if (turretGunConstraint.IsBroken)
                {
                    turretBodyDown.DisableCollision(turretGun1, false);
                    turretBodyDown.DisableCollision(turretGun2, false);
                }
            }

            oldKeyboardState = keyboardState;
        }
    }
}
