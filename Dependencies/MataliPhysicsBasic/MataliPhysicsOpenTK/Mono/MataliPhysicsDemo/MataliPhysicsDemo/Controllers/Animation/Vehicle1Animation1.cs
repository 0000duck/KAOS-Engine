/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Vehicle1Animation1
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
        string amphibian1BodyName;
        string amphibian1TurretBodyDownName;
        string amphibian1TurretConstraintName;
        string yellowName;

        Shape sphere;
        PhysicsObject bodyUp;
        PhysicsObject bodyDown;
        PhysicsObject turretBodyUp;
        PhysicsObject turretBodyDown;
        PhysicsObject turretGun1;
        PhysicsObject turretGun2;
        Constraint turretConstraint;

        double totalTime;
        int frameCount;
        int shotFrameCount;

        PhysicsObject shot1;
        PhysicsObject shot1Base;
        PhysicsObject shot1Light;
        PhysicsObject shot2;
        PhysicsObject shot2Base;
        PhysicsObject shot2Light;
        StringBuilder shot1NameBuilder;
        StringBuilder shot1BaseNameBuilder;
        StringBuilder shot1LightNameBuilder;
        StringBuilder shot2NameBuilder;
        StringBuilder shot2BaseNameBuilder;
        StringBuilder shot2LightNameBuilder;
        string shotInstanceIndexName;
        string shotBaseInstanceIndexName;
        string shotLightInstanceIndexName;
        int shot1NameLength;
        int shot1BaseNameLength;
        int shot1LightNameLength;
        int shot2NameLength;
        int shot2BaseNameLength;
        int shot2LightNameLength;
        int shotCount;
        int maxShotSimulationFrameCount;
        int maxShotFrameCount;
        int maxShotCount;

        Vector3 vectorZero;
        Vector3 unitY;
        Matrix4 matrixIdentity;

        public Vehicle1Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            sphereName = "Sphere";

            shot1Name = "Vehicle 1 Gun 1 Shot" + instanceIndexName + " ";
            shot1BaseName = "Vehicle 1 Gun 1 Shot Base" + instanceIndexName + " ";
            shot1LightName = "Vehicle 1 Gun 1 Shot Light" + instanceIndexName + " ";

            shot2Name = "Vehicle 1 Gun 2 Shot" + instanceIndexName + " ";
            shot2BaseName = "Vehicle 1 Gun 2 Shot Base" + instanceIndexName + " ";
            shot2LightName = "Vehicle 1 Gun 2 Shot Light" + instanceIndexName + " ";

            amphibian1BodyName = "Amphibian 1 Body" + instanceIndexName;
            amphibian1TurretBodyDownName = "Amphibian 1 Turret Body Down" + instanceIndexName;
            amphibian1TurretConstraintName = "Amphibian 1 Turret Constraint" + instanceIndexName;
            yellowName = "Yellow";

            shot1NameBuilder = new StringBuilder(shot1Name);
            shot1BaseNameBuilder = new StringBuilder(shot1BaseName);
            shot1LightNameBuilder = new StringBuilder(shot1LightName);

            shot2NameBuilder = new StringBuilder(shot2Name);
            shot2BaseNameBuilder = new StringBuilder(shot2BaseName);
            shot2LightNameBuilder = new StringBuilder(shot2LightName);

            shot1NameLength = shot1NameBuilder.Length;
            shot1BaseNameLength = shot1BaseNameBuilder.Length;
            shot1LightNameLength = shot1LightNameBuilder.Length;

            shot2NameLength = shot2NameBuilder.Length;
            shot2BaseNameLength = shot2BaseNameBuilder.Length;
            shot2LightNameLength = shot2LightNameBuilder.Length;

            maxShotSimulationFrameCount = 100;
            maxShotFrameCount = 10;
            maxShotCount = maxShotSimulationFrameCount / maxShotFrameCount;

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
            bodyUp = scene.Factory.PhysicsObjectManager.Find("Vehicle 1 Body Up" + instanceIndexName);
            bodyDown = scene.Factory.PhysicsObjectManager.Find("Vehicle 1 Body Down" + instanceIndexName);
            turretBodyUp = scene.Factory.PhysicsObjectManager.Find("Vehicle 1 Turret Body Up" + instanceIndexName);
            turretBodyDown = scene.Factory.PhysicsObjectManager.Find("Vehicle 1 Turret Body Down" + instanceIndexName);
            turretGun1 = scene.Factory.PhysicsObjectManager.Find("Vehicle 1 Turret Gun 1" + instanceIndexName);
            turretGun2 = scene.Factory.PhysicsObjectManager.Find("Vehicle 1 Turret Gun 2" + instanceIndexName);
            turretConstraint = scene.Factory.ConstraintManager.Find("Vehicle 1 Turret Constraint" + instanceIndexName);

            totalTime = 0.0;
            frameCount = 0;
            shotFrameCount = 0;
            shotCount = -1;

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Vehicle 1 Body" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Move);
        }

        public void Move(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if ((bodyUp == null) || (bodyDown == null) || (turretBodyUp == null) || (turretBodyDown == null) || (turretGun1 == null) || (turretGun2 == null) || (turretConstraint == null))
                return;

            totalTime += time;

            PhysicsObject turret = turretBodyUp.RigidGroupOwner;

            if (objectBase.IsBrokenRigidGroup || turret.IsBrokenRigidGroup || turretConstraint.IsBroken)
                return;

            Vector3 gravityDirection = vectorZero;
            Vector3 upObjectForce = vectorZero;
            Vector3 upTurretForce = vectorZero;
            Vector3 distance = vectorZero;
            Vector3 direction = vectorZero;

            scene.GetGravityDirection(ref gravityDirection);

            Vector3.Multiply(ref gravityDirection, -scene.GravityAcceleration * objectBase.Integral.Mass, out upObjectForce);
            Vector3.Multiply(ref gravityDirection, -scene.GravityAcceleration * turret.Integral.Mass, out upTurretForce);

            objectBase.WorldAccumulator.AddWorldForce(ref upObjectForce);
            turret.WorldAccumulator.AddWorldForce(ref upTurretForce);

            PhysicsObject amphibian1Body = scene.Factory.PhysicsObjectManager.Find(amphibian1BodyName);
            PhysicsObject amphibian1TurretBodyDown = scene.Factory.PhysicsObjectManager.Find(amphibian1TurretBodyDownName);
            Constraint amphibian1TurretConstraint = scene.Factory.ConstraintManager.Find(amphibian1TurretConstraintName);
            Vector3 bodyDownPosition = vectorZero;

            if ((amphibian1Body != null) && (amphibian1TurretBodyDown != null) && (amphibian1TurretConstraint != null) && !amphibian1Body.IsBrokenRigidGroup && !amphibian1TurretConstraint.IsBroken)
            {
                Vector3 amphibianTurretPosition = vectorZero;

                amphibian1TurretBodyDown.RigidGroupOwner.MainWorldTransform.GetPosition(ref amphibianTurretPosition);
                bodyDown.MainWorldTransform.GetPosition(ref bodyDownPosition);

                Vector3.Subtract(ref amphibianTurretPosition, ref bodyDownPosition, out distance);
                Vector3.Normalize(ref distance, out direction);
            }

            Vector3 force = vectorZero;
            Vector3 right = vectorZero;

            Vector3.Cross(ref direction, ref unitY, out right);
            Vector3.Multiply(ref right, 100.0f, out right);

            if (frameCount < 200)
                Vector3.Subtract(ref force, ref right, out force);
            else
                Vector3.Add(ref force, ref right, out force);

            if (frameCount > 400)
                frameCount = 0;

            if (distance.Length > 100.0f)
            {
                Vector3.Multiply(ref direction, 1000.0f, out right);
                Vector3.Add(ref force, ref right, out force);
            }

            if (distance.Y > 0.0f)
                force.Y += distance.Y * 100.0f;

            objectBase.WorldAccumulator.AddWorldForce(ref force);
            turret.WorldAccumulator.AddWorldForce(ref force);

            frameCount++;
            shotFrameCount++;

            Vector3 turretAxis = vectorZero;
            Vector3 turretDirection = vectorZero;
            Vector3 turretGunPosition = vectorZero;

            Vector3 turretBodyDownPosition = vectorZero;
            Vector3 turretGun1Position = vectorZero;
            Vector3 turretGun2Position = vectorZero;

            turretBodyDown.MainWorldTransform.GetPosition(ref turretBodyDownPosition);
            turretGun1.MainWorldTransform.GetPosition(ref turretGun1Position);
            turretGun2.MainWorldTransform.GetPosition(ref turretGun2Position);

            Vector3.Add(ref turretGun1Position, ref turretGun2Position, out turretGunPosition);
            Vector3.Multiply(ref turretGunPosition, 0.5f, out turretGunPosition);
            Vector3.Subtract(ref turretGunPosition, ref turretBodyDownPosition, out turretDirection);
            turretDirection.Normalize();
            Vector3.Cross(ref turretDirection, ref direction, out turretAxis);

            turretDirection.Y = 0.0f;
            direction.Y = 0.0f;
            float turretAngle = 0.0f;

            Vector3.Dot(ref turretDirection, ref direction, out turretAngle);
            turretAngle = (float)Math.Acos(turretAngle);
            Vector3.Multiply(ref turretAxis, turretAngle, out turretAxis);

            Vector3 velocity = vectorZero;

            turret.MainWorldTransform.GetAngularVelocity(ref velocity);
            Vector3.Subtract(ref velocity, ref turretAxis, out velocity);
            turret.MainWorldTransform.SetAngularVelocity(ref velocity);

            if ((Math.Abs(turretAngle) < 0.1f) && (distance.Length < 100.0f) && (shotFrameCount > maxShotFrameCount))
            {
                shotFrameCount = 0;

                shotCount = (shotCount + 1) % maxShotCount;
                shot1NameBuilder.Remove(shot1NameLength, shot1NameBuilder.Length - shot1NameLength);
                shot1NameBuilder.Append(shotCount);
                shotInstanceIndexName = shot1NameBuilder.ToString();

                shot1BaseNameBuilder.Remove(shot1BaseNameLength, shot1BaseNameBuilder.Length - shot1BaseNameLength);
                shot1BaseNameBuilder.Append(shotCount);
                shotBaseInstanceIndexName = shot1BaseNameBuilder.ToString();

                shot1LightNameBuilder.Remove(shot1LightNameLength, shot1LightNameBuilder.Length - shot1LightNameLength);
                shot1LightNameBuilder.Append(shotCount);
                shotLightInstanceIndexName = shot1LightNameBuilder.ToString();

                shot1 = scene.Factory.PhysicsObjectManager.FindOrCreate(shotInstanceIndexName);
                shot1Base = scene.Factory.PhysicsObjectManager.FindOrCreate(shotBaseInstanceIndexName);
                shot1Light = scene.Factory.PhysicsObjectManager.FindOrCreate(shotLightInstanceIndexName);

                shot1.AddChildPhysicsObject(shot1Base);
                shot1.AddChildPhysicsObject(shot1Light);

                Matrix4 turretGun1Rotation = matrixIdentity;
                Vector3 shot1Position = vectorZero;
                Vector3 shot1LocalPosition = vectorZero;

                turretGun1.MainWorldTransform.GetRotation(ref turretGun1Rotation);

                shot1LocalPosition.X = 0.0f;
                shot1LocalPosition.Y = 2.0f;
                shot1LocalPosition.Z = 0.0f;

                Vector3.TransformVector(ref shot1LocalPosition, ref turretGun1Rotation, out shot1Position);
                Vector3.Add(ref shot1Position, ref turretGun1Position, out shot1Position);
                Vector3 shot1Direction = vectorZero;
                Vector3 shot1Scale = vectorZero;

                shot1Direction.X = turretGun1Rotation.Row1.X;
                shot1Direction.Y = turretGun1Rotation.Row1.Y;
                shot1Direction.Z = turretGun1Rotation.Row1.Z;

                shot1Scale.X = shot1Scale.Y = shot1Scale.Z = 0.5f;

                shot1.InitLocalTransform.SetRotation(ref matrixIdentity);
                shot1.InitLocalTransform.SetPosition(ref shot1Position);

                Vector3.Multiply(ref shot1Direction, 200.0f, out shot1Direction);

                shot1.InitLocalTransform.SetLinearVelocity(ref shot1Direction);
                shot1.InitLocalTransform.SetAngularVelocity(ref vectorZero);
                shot1.MaxSimulationFrameCount = maxShotSimulationFrameCount;
                shot1.EnableRemovePhysicsObjectsFromManagerAfterMaxSimulationFrameCount = false;

                shot1Base.Shape = sphere;
                shot1Base.UserDataStr = sphereName;
                shot1Base.Material.RigidGroup = true;
                shot1Base.InitLocalTransform.SetScale(ref shot1Scale);
                shot1Base.Integral.SetDensity(1.0f);
                shot1Base.EnableCollisions = true;
                shot1Base.DisableCollision(turretGun1, true);
                shot1Base.MaxDisableCollisionFrameCount = 10;
                shot1Base.EnableBreakRigidGroup = false;
                shot1Base.CreateSound(true);

                shot1Light.Shape = sphere;
                shot1Light.UserDataStr = sphereName;
                shot1Light.CreateLight(true);
                shot1Light.Light.Type = PhysicsLightType.Point;
                shot1Light.Light.Range = 20.0f;
                shot1Light.Light.SetDiffuse(1.0f, 0.7f, 0.0f);
                shot1Light.InitLocalTransform.SetScale(20.0f);
                shot1Light.Material.RigidGroup = true;
                shot1Light.Material.UserDataStr = yellowName;
                shot1Light.EnableBreakRigidGroup = false;
                shot1Light.EnableCollisions = false;
                shot1Light.EnableCursorInteraction = false;
                shot1Light.EnableAddToCameraDrawTransparentPhysicsObjects = false;

                scene.UpdateFromInitLocalTransform(shot1);

                shot2NameBuilder.Remove(shot2NameLength, shot2NameBuilder.Length - shot2NameLength);
                shot2NameBuilder.Append(shotCount);
                shotInstanceIndexName = shot2NameBuilder.ToString();

                shot2BaseNameBuilder.Remove(shot2BaseNameLength, shot2BaseNameBuilder.Length - shot2BaseNameLength);
                shot2BaseNameBuilder.Append(shotCount);
                shotBaseInstanceIndexName = shot2BaseNameBuilder.ToString();

                shot2LightNameBuilder.Remove(shot2LightNameLength, shot2LightNameBuilder.Length - shot2LightNameLength);
                shot2LightNameBuilder.Append(shotCount);
                shotLightInstanceIndexName = shot2LightNameBuilder.ToString();

                shot2 = scene.Factory.PhysicsObjectManager.FindOrCreate(shotInstanceIndexName);
                shot2Base = scene.Factory.PhysicsObjectManager.FindOrCreate(shotBaseInstanceIndexName);
                shot2Light = scene.Factory.PhysicsObjectManager.FindOrCreate(shotLightInstanceIndexName);

                shot2.AddChildPhysicsObject(shot2Base);
                shot2.AddChildPhysicsObject(shot2Light);

                Matrix4 turretGun2Rotation = matrixIdentity;
                Vector3 shot2Position = vectorZero;
                Vector3 shot2LocalPosition = vectorZero;

                turretGun2.MainWorldTransform.GetRotation(ref turretGun2Rotation);

                shot2LocalPosition.X = 0.0f;
                shot2LocalPosition.Y = 2.0f;
                shot2LocalPosition.Z = 0.0f;

                Vector3.TransformVector(ref shot2LocalPosition, ref turretGun2Rotation, out shot2Position);
                Vector3.Add(ref shot2Position, ref turretGun1Position, out shot2Position);
                Vector3 shot2Direction = vectorZero;
                Vector3 shot2Scale = vectorZero;

                shot2Direction.X = turretGun2Rotation.Row1.X;
                shot2Direction.Y = turretGun2Rotation.Row1.Y;
                shot2Direction.Z = turretGun2Rotation.Row1.Z;

                shot2Scale.X = shot2Scale.Y = shot2Scale.Z = 0.5f;

                shot2.InitLocalTransform.SetRotation(ref matrixIdentity);
                shot2.InitLocalTransform.SetPosition(ref shot2Position);

                Vector3.Multiply(ref shot2Direction, 200.0f, out shot2Direction);

                shot2.InitLocalTransform.SetLinearVelocity(ref shot2Direction);
                shot2.InitLocalTransform.SetAngularVelocity(ref vectorZero);
                shot2.MaxSimulationFrameCount = maxShotSimulationFrameCount;
                shot2.EnableRemovePhysicsObjectsFromManagerAfterMaxSimulationFrameCount = false;

                shot2Base.Shape = sphere;
                shot2Base.UserDataStr = sphereName;
                shot2Base.Material.RigidGroup = true;
                shot2Base.InitLocalTransform.SetScale(ref shot2Scale);
                shot2Base.Integral.SetDensity(10.0f);
                shot2Base.EnableCollisions = true;
                shot2Base.DisableCollision(turretGun2, true);
                shot2Base.MaxDisableCollisionFrameCount = 10;
                shot2Base.EnableBreakRigidGroup = false;
                shot2Base.CreateSound(true);

                shot2Light.Shape = sphere;
                shot2Light.UserDataStr = sphereName;
                shot2Light.CreateLight(true);
                shot2Light.Light.Type = PhysicsLightType.Point;
                shot2Light.Light.Range = 20.0f;
                shot2Light.Light.SetDiffuse(1.0f, 0.7f, 0.0f);
                shot2Light.InitLocalTransform.SetScale(20.0f);
                shot2Light.Material.RigidGroup = true;
                shot2Light.Material.UserDataStr = yellowName;
                shot2Light.EnableBreakRigidGroup = false;
                shot2Light.EnableCollisions = false;
                shot2Light.EnableCursorInteraction = false;
                shot2Light.EnableAddToCameraDrawTransparentPhysicsObjects = false;

                scene.UpdateFromInitLocalTransform(shot2);
            }

            Vector3 bodyUpPosition = vectorZero;
            Vector3 upDirection = vectorZero;

            bodyDown.MainWorldTransform.GetPosition(ref bodyDownPosition);
            bodyUp.MainWorldTransform.GetPosition(ref bodyUpPosition);

            Vector3.Subtract(ref bodyDownPosition, ref bodyUpPosition, out upDirection);
            upDirection.Normalize();

            float bodyAngle = 0.0f;
            Vector3 axis = vectorZero;

            Vector3.Dot(ref upDirection, ref gravityDirection, out bodyAngle);
            bodyAngle = (float)Math.Acos(bodyAngle);

            if (Math.Abs(bodyAngle) > 0.005f)
                bodyAngle *= 0.005f / Math.Abs(bodyAngle);

            Vector3.Cross(ref gravityDirection, ref upDirection, out axis);

            objectBase.MainWorldTransform.GetAngularVelocity(ref velocity);
            Vector3.Multiply(ref velocity, 0.98f, out velocity);
            Vector3.Multiply(ref axis, bodyAngle * 400.0f, out axis);
            Vector3.Add(ref velocity, ref axis, out velocity);
            objectBase.MainWorldTransform.SetAngularVelocity(ref velocity);

            Vector3 turretBodyUpPosition = vectorZero;

            turretBodyUp.MainWorldTransform.GetPosition(ref turretBodyUpPosition);

            Vector3.Subtract(ref turretBodyDownPosition, ref turretBodyUpPosition, out upDirection);
            upDirection.Normalize();

            Vector3.Dot(ref upDirection, ref gravityDirection, out bodyAngle);
            bodyAngle = (float)Math.Acos(bodyAngle);

            if (Math.Abs(bodyAngle) > 0.005f)
                bodyAngle *= 0.005f / Math.Abs(bodyAngle);

            Vector3.Cross(ref gravityDirection, ref upDirection, out axis);

            turret.MainWorldTransform.GetAngularVelocity(ref velocity);
            Vector3.Multiply(ref velocity, 0.98f, out velocity);
            Vector3.Multiply(ref axis, bodyAngle * 400.0f, out axis);
            Vector3.Add(ref velocity, ref axis, out velocity);
            turret.MainWorldTransform.SetAngularVelocity(ref velocity);
        }
    }
}
