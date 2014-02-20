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
    public class Helicopter1Animation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        float maxRotorUpAngularVelocity;
        float maxRotorUpFlyForce;

        float maxRotorBackAngularVelocity;
        float maxRotorBackFlyCompensationForce;
        float maxRotorBackStartCompensationForce;

        float maxFlyHeight;
        float maxHeightDamping;
        float maxMoveDirectionDamping;

        float maxFlyTime;
        float maxStartTime;
        Vector3 flyPosition1;
        Vector3 flyPosition2;
        Vector3 flyPosition3;
        Vector3 flyPosition4;

        bool enableFly;
        double totalStartTime;
        double totalFlyTime;
        double totalSwitchRightTime;
        double totalSwitchLeftTime;
        Vector3 endFlyPosition;
        Vector3 landingPosition;

        float maxSwitchRightTime;
        float maxSwitchLeftTime;

        Vector3 force;
        Vector3 torque;

        Vector3 vectorZero;
        Vector3 unitY;
        Vector3 unitZ;

        string cameraBodyName;

        Constraint cabinFrontButtonConstraint;
        Constraint upRotorConstraint;
        Constraint backRotorConstraint;

        PhysicsObject body;
        PhysicsObject upRotor;
        PhysicsObject backRotor;
        PhysicsObject upRotorBody;
        PhysicsObject upBody2;
        PhysicsObject tail4;
        PhysicsObject tail3;

        PhysicsObject cabinBodyUp1;
        PhysicsObject cabinBodyUp2;
        PhysicsObject cabinBodyDown1;
        PhysicsObject cabinBodyDown2;

        Constraint leftDoorConstraint;
        PhysicsObject cabinBodyLeft1;
        PhysicsObject cabinBodyLeft2;
        PhysicsObject cabinBodyFrontLeft;
        PhysicsObject leftDoorRight;
        PhysicsObject leftDoorLeft;
        PhysicsObject leftDoorUp;
        PhysicsObject leftDoorDown;

        Constraint rightDoorConstraint;
        PhysicsObject cabinBodyRight1;
        PhysicsObject cabinBodyRight2;
        PhysicsObject cabinBodyFrontRight;
        PhysicsObject rightDoorRight;
        PhysicsObject rightDoorLeft;
        PhysicsObject rightDoorUp;
        PhysicsObject rightDoorDown;

        public Helicopter1Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            maxRotorUpAngularVelocity = 10.0f;
            maxRotorUpFlyForce = 2500.0f;

            maxRotorBackAngularVelocity = 10.0f;
            maxRotorBackFlyCompensationForce = 10000.0f;
            maxRotorBackStartCompensationForce = 1000.0f;

            maxFlyHeight = 100.0f;

            maxHeightDamping = 0.001f;
            maxMoveDirectionDamping = 0.998f;

            maxStartTime = 30.0f;
            maxFlyTime = 500.0f;
            flyPosition1 = new Vector3(500.0f, 100.0f, 500.0f);
            flyPosition2 = new Vector3(500.0f, 100.0f, -500.0f);
            flyPosition3 = new Vector3(-500.0f, 100.0f, -500.0f);
            flyPosition4 = new Vector3(-500.0f, 100.0f, 500.0f);

            maxSwitchRightTime = 10.0f;
            maxSwitchLeftTime = 10.0f;

            vectorZero = Vector3.Zero;
            unitY = Vector3.UnitY;
            unitZ = Vector3.UnitZ;

            cameraBodyName = "Camera 2 Body";
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers(Vector3 endFlyPosition, Vector3 landingPosition)
        {
            cabinFrontButtonConstraint = scene.Factory.ConstraintManager.Find("Helicopter 1 Cabin Front Button Constraint" + instanceIndexName);
            upRotorConstraint = scene.Factory.ConstraintManager.Find("Helicopter 1 Up Rotor Constraint" + instanceIndexName);
            backRotorConstraint = scene.Factory.ConstraintManager.Find("Helicopter 1 Back Rotor Constraint" + instanceIndexName);

            body = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Body" + instanceIndexName);
            upRotor = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Rotor" + instanceIndexName);
            backRotor = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Back Rotor" + instanceIndexName);
            upRotorBody = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Rotor Body" + instanceIndexName);
            upBody2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Up Body 2" + instanceIndexName);
            tail4 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Tail 4" + instanceIndexName);
            tail3 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Tail 3" + instanceIndexName);

            cabinBodyUp1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Up 1" + instanceIndexName);
            cabinBodyUp2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Up 2" + instanceIndexName);
            cabinBodyDown1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Down 1" + instanceIndexName);
            cabinBodyDown2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Down 2" + instanceIndexName);

            leftDoorConstraint = scene.Factory.ConstraintManager.Find("Helicopter 1 Left Door Constraint" + instanceIndexName);
            cabinBodyLeft1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Left 1" + instanceIndexName);
            cabinBodyLeft2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Left 2" + instanceIndexName);
            cabinBodyFrontLeft = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Front Left" + instanceIndexName);
            leftDoorRight = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Right" + instanceIndexName);
            leftDoorLeft = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Left" + instanceIndexName);
            leftDoorUp = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Up" + instanceIndexName);
            leftDoorDown = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Down" + instanceIndexName);

            rightDoorConstraint = scene.Factory.ConstraintManager.Find("Helicopter 1 Right Door Constraint" + instanceIndexName);
            cabinBodyRight1 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Right 1" + instanceIndexName);
            cabinBodyRight2 = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Right 2" + instanceIndexName);
            cabinBodyFrontRight = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Body Front Right" + instanceIndexName);
            rightDoorRight = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Right" + instanceIndexName);
            rightDoorLeft = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Left" + instanceIndexName);
            rightDoorUp = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Up" + instanceIndexName);
            rightDoorDown = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Down" + instanceIndexName);

            enableFly = false;
            totalStartTime = 0.0;
            totalFlyTime = 0.0;

            this.endFlyPosition = endFlyPosition;
            this.landingPosition = landingPosition;

            totalSwitchLeftTime = 2.0f * maxSwitchLeftTime;
            totalSwitchRightTime = 2.0f * maxSwitchRightTime;

            PhysicsObject objectBase = null;

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Cabin Front Button Switch" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.CollisionMethods += new CollisionMethod(Run);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Body" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Fly);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Left Door Switch" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(SwitchLeft);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Helicopter 1 Right Door Switch" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(SwitchRight);
        }

        void Run(CollisionMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if ((cabinFrontButtonConstraint == null) || (body == null) || cabinFrontButtonConstraint.IsBroken || body.IsBrokenRigidGroup) return;

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if ((physicsObjectWithActiveCamera == null) || !physicsObjectWithActiveCamera.RigidGroupOwner.IsColliding(objectBase)) return;

            if (!enableFly)
            {
                enableFly = true;
                cabinFrontButtonConstraint.ControlDistanceX = 0.4f;
            }
        }

        void Fly(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if (!enableFly) return;

            totalStartTime += time;

            bool rotorUpWorking = true;
            bool rotorBackWorking = true;
            float rotorSpeedFactor = 0.0f;

            if (totalStartTime > maxStartTime)
            {
                totalFlyTime += time;
                rotorSpeedFactor = 1.2f;
            }
            else
            {
                totalFlyTime = 0.0f;
                rotorSpeedFactor = 0.6f;
            }

            if (totalFlyTime >= maxFlyTime)
            {
                totalStartTime = 0.0f;
                enableFly = false;

                if (cabinFrontButtonConstraint != null)
                    cabinFrontButtonConstraint.ControlDistanceX = 0.0f;
            }

            if (upRotorConstraint != null)
            {
                if (upRotorConstraint.PhysicsObject1 != null)
                    if (upRotorConstraint.PhysicsObject1.IsBrokenRigidGroup || upRotorConstraint.IsBroken || (totalFlyTime >= maxFlyTime))
                        rotorUpWorking = false;

                if (upRotorConstraint.PhysicsObject2 != null)
                    if (upRotorConstraint.PhysicsObject2.IsBrokenRigidGroup || upRotorConstraint.IsBroken || (totalFlyTime >= maxFlyTime))
                        rotorUpWorking = false;
            }
            else
            {
                rotorUpWorking = false;
            }

            if (backRotorConstraint != null)
            {
                if (rotorUpWorking)
                {
                    if (backRotorConstraint.PhysicsObject1 != null)
                        if (backRotorConstraint.PhysicsObject1.IsBrokenRigidGroup || backRotorConstraint.IsBroken)
                            rotorBackWorking = false;

                    if (backRotorConstraint.PhysicsObject2 != null)
                        if (backRotorConstraint.PhysicsObject2.IsBrokenRigidGroup || backRotorConstraint.IsBroken)
                            rotorBackWorking = false;
                }
                else
                {
                    rotorBackWorking = false;
                }
            }
            else
            {
                rotorBackWorking = false;
            }

            float rotorUpAngularVelocity = 0.0f;
            float rotorBackAngularVelocity = 0.0f;
            Vector3 velocity = vectorZero;

            if ((upRotor != null) && (totalFlyTime < maxFlyTime) && rotorUpWorking)
            {
                Vector3.Multiply(ref unitY, -rotorSpeedFactor * maxRotorUpAngularVelocity, out velocity);
                upRotor.MainWorldTransform.SetLocalAngularVelocity(ref velocity);
                rotorUpAngularVelocity = velocity.Length;
            }

            if ((backRotor != null) && (totalFlyTime < maxFlyTime) && rotorBackWorking)
            {
                Vector3.Multiply(ref unitZ, rotorSpeedFactor * maxRotorBackAngularVelocity, out velocity);
                backRotor.MainWorldTransform.SetLocalAngularVelocity(ref velocity);
                rotorBackAngularVelocity = velocity.Length;
            }

            Vector3 gravityDirection = vectorZero;
            scene.GetGravityDirection(ref gravityDirection);

            if (rotorUpWorking)
            {
                if ((rotorUpAngularVelocity > maxRotorUpAngularVelocity))
                {
                    float upFactor = 1.0f;
                    if (!rotorBackWorking)
                        upFactor = 0.5f;

                    Vector3.Multiply(ref gravityDirection, -maxRotorUpFlyForce * scene.GravityAcceleration * upFactor, out force);

                    objectBase.WorldAccumulator.AddWorldForce(ref force);
                }
            }

            if (rotorBackWorking && rotorUpWorking)
            {
                if (rotorUpAngularVelocity > maxRotorUpAngularVelocity)
                {
                    Vector3.Multiply(ref unitY, maxRotorBackFlyCompensationForce, out force);

                    objectBase.WorldAccumulator.AddLocalTorque(ref force);
                }
                else
                {
                    Vector3.Multiply(ref unitY, maxRotorBackStartCompensationForce, out force);

                    objectBase.WorldAccumulator.AddLocalTorque(ref force);
                }
            }

            if ((rotorUpAngularVelocity > maxRotorUpAngularVelocity) && (rotorBackAngularVelocity > maxRotorBackAngularVelocity))
            {
                Vector3 deltaVelocity = vectorZero;
                Vector3 moveDirection = vectorZero;
                Vector3 position = vectorZero;

                objectBase.MainWorldTransform.GetPosition(ref position);

                if (totalFlyTime < maxFlyTime * 0.2f)
                {
                    objectBase.MainWorldTransform.GetLinearVelocity(ref velocity);
                    Vector3.Multiply(ref velocity, maxMoveDirectionDamping, out velocity);
                    objectBase.MainWorldTransform.SetLinearVelocity(ref velocity);

                    Vector3.Subtract(ref flyPosition1, ref position, out moveDirection);
                    moveDirection.Normalize();
                    Vector3.Multiply(ref moveDirection, objectBase.Integral.Mass, out force);

                    objectBase.WorldAccumulator.AddWorldForce(ref force);
                }
                else
                    if (totalFlyTime < maxFlyTime * 0.4f)
                    {
                        objectBase.MainWorldTransform.GetLinearVelocity(ref velocity);
                        Vector3.Multiply(ref velocity, maxMoveDirectionDamping, out velocity);
                        objectBase.MainWorldTransform.SetLinearVelocity(ref velocity);

                        Vector3.Subtract(ref flyPosition2, ref position, out moveDirection);
                        moveDirection.Normalize();
                        Vector3.Multiply(ref moveDirection, objectBase.Integral.Mass, out force);

                        objectBase.WorldAccumulator.AddWorldForce(ref force);
                    }
                    else
                        if (totalFlyTime < maxFlyTime * 0.6f)
                        {
                            objectBase.MainWorldTransform.GetLinearVelocity(ref velocity);
                            Vector3.Multiply(ref velocity, maxMoveDirectionDamping, out velocity);
                            objectBase.MainWorldTransform.SetLinearVelocity(ref velocity);

                            Vector3.Subtract(ref flyPosition3, ref position, out moveDirection);
                            moveDirection.Normalize();
                            Vector3.Multiply(ref moveDirection, objectBase.Integral.Mass, out force);

                            objectBase.WorldAccumulator.AddWorldForce(ref force);
                        }
                        else
                            if (totalFlyTime < maxFlyTime * 0.8f)
                            {
                                objectBase.MainWorldTransform.GetLinearVelocity(ref velocity);
                                Vector3.Multiply(ref velocity, maxMoveDirectionDamping, out velocity);
                                objectBase.MainWorldTransform.SetLinearVelocity(ref velocity);

                                Vector3.Subtract(ref flyPosition4, ref position, out moveDirection);
                                moveDirection.Normalize();
                                Vector3.Multiply(ref moveDirection, objectBase.Integral.Mass, out force);

                                objectBase.WorldAccumulator.AddWorldForce(ref force);
                            }
                            else
                                if (totalFlyTime < maxFlyTime * 0.95f)
                                {
                                    objectBase.MainWorldTransform.GetLinearVelocity(ref velocity);
                                    Vector3.Multiply(ref velocity, maxMoveDirectionDamping, out velocity);
                                    objectBase.MainWorldTransform.SetLinearVelocity(ref velocity);

                                    Vector3.Subtract(ref endFlyPosition, ref position, out moveDirection);
                                    moveDirection.Normalize();
                                    Vector3.Multiply(ref moveDirection, objectBase.Integral.Mass, out force);

                                    objectBase.WorldAccumulator.AddWorldForce(ref force);
                                }
                                else
                                    if (totalFlyTime < maxFlyTime)
                                    {
                                        objectBase.WorldAccumulator.GetTotalWorldForce(ref force);
                                        Vector3.Multiply(ref force, 0.9f, out force);
                                        objectBase.WorldAccumulator.SetTotalWorldForce(ref force);

                                        objectBase.MainWorldTransform.GetLinearVelocity(ref velocity);
                                        Vector3.Multiply(ref velocity, maxMoveDirectionDamping, out velocity);
                                        objectBase.MainWorldTransform.SetLinearVelocity(ref velocity);

                                        Vector3.Subtract(ref landingPosition, ref position, out moveDirection);
                                        moveDirection.Normalize();
                                        Vector3.Multiply(ref moveDirection, objectBase.Integral.Mass, out force);

                                        objectBase.WorldAccumulator.AddWorldForce(ref force);
                                    }

                if ((upRotorBody != null) && (upBody2 != null) && (tail4 != null) && (tail3 != null) && (totalFlyTime < maxFlyTime))
                {
                    Vector3 axis = vectorZero;
                    Vector3 forwardDirection = vectorZero;
                    Vector3 tailStartPosition = vectorZero;
                    Vector3 tailEndPosition = vectorZero;

                    tail4.MainWorldTransform.GetPosition(ref tailStartPosition);
                    tail3.MainWorldTransform.GetPosition(ref tailEndPosition);

                    Vector3.Subtract(ref tailEndPosition, ref tailStartPosition, out forwardDirection);
                    forwardDirection.Normalize();

                    float bodyAngle = 0.0f;

                    Vector3.Dot(ref forwardDirection, ref moveDirection, out bodyAngle);
                    bodyAngle = (float)Math.Acos(bodyAngle);
                    Vector3.Cross(ref moveDirection, ref forwardDirection, out axis);

                    objectBase.MainWorldTransform.GetAngularVelocity(ref velocity);
                    Vector3.Multiply(ref velocity, 0.5f, out velocity);
                    Vector3.Multiply(ref axis, bodyAngle * (float)time, out axis);
                    Vector3.Add(ref velocity, ref axis, out velocity);
                    objectBase.MainWorldTransform.SetAngularVelocity(ref velocity);

                    Vector3 upDirection = vectorZero;

                    Vector3 upBodyStartPosition = vectorZero;
                    Vector3 upBodyEndPosition = vectorZero;

                    upRotorBody.MainWorldTransform.GetPosition(ref upBodyStartPosition);
                    upBody2.MainWorldTransform.GetPosition(ref upBodyEndPosition);

                    Vector3.Subtract(ref upBodyEndPosition, ref upBodyStartPosition, out upDirection);
                    upDirection.Normalize();

                    Vector3.Dot(ref upDirection, ref gravityDirection, out bodyAngle);
                    bodyAngle = (float)Math.Acos(bodyAngle);
                    Vector3.Cross(ref gravityDirection, ref upDirection, out axis);

                    objectBase.MainWorldTransform.GetAngularVelocity(ref velocity);
                    Vector3.Multiply(ref axis, bodyAngle * (float)time, out axis);
                    Vector3.Add(ref velocity, ref axis, out velocity);
                    objectBase.MainWorldTransform.SetAngularVelocity(ref velocity);
                }

                objectBase.MainWorldTransform.GetPosition(ref position);

                if (position.Y > maxFlyHeight)
                {
                    float distance = position.Y - maxFlyHeight;

                    Vector3.Multiply(ref gravityDirection, distance * maxHeightDamping, out deltaVelocity);

                    objectBase.MainWorldTransform.GetLinearVelocity(ref velocity);
                    Vector3.Add(ref velocity, ref deltaVelocity, out velocity);
                    objectBase.MainWorldTransform.SetLinearVelocity(ref velocity);
                }
            }
        }

        void SwitchRight(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if (rightDoorConstraint.IsBroken || rightDoorConstraint.PhysicsObject1.IsBrokenRigidGroup || rightDoorConstraint.PhysicsObject2.IsBrokenRigidGroup)
            {
                rightDoorConstraint.EnableControlAngleY = false;

                cabinBodyRight1.DisableCollision(rightDoorRight, false);

                cabinBodyRight2.DisableCollision(rightDoorUp, false);
                cabinBodyRight2.DisableCollision(rightDoorDown, false);
                cabinBodyRight2.DisableCollision(rightDoorLeft, false);

                cabinBodyUp1.DisableCollision(rightDoorUp, false);

                cabinBodyUp2.DisableCollision(rightDoorUp, false);
                cabinBodyUp2.DisableCollision(rightDoorRight, false);

                cabinBodyFrontRight.DisableCollision(rightDoorUp, false);
                cabinBodyFrontRight.DisableCollision(rightDoorRight, false);
                cabinBodyFrontRight.DisableCollision(rightDoorDown, false);

                cabinBodyDown1.DisableCollision(rightDoorDown, false);

                cabinBodyDown2.DisableCollision(rightDoorDown, false);

                return;
            }

            PhysicsObject cameraBody = scene.GetPhysicsObjectWithActiveCamera(0).RigidGroupOwner.FindChildPhysicsObject(cameraBodyName, true, true);

            if (cameraBody == null)
            {
                rightDoorConstraint.EnableControlAngleY = true;
                return;
            }

            bool switchColliding = objectBase.IsColliding(cameraBody);
            bool doorColliding = objectBase.RigidGroupOwner.IsColliding(cameraBody);

            if (switchColliding)
            {
                totalSwitchRightTime = 0.0;
                rightDoorConstraint.EnableControlAngleY = false;
            }

            if (totalSwitchRightTime < maxSwitchRightTime)
            {
                Vector3.Multiply(ref unitY, 2000.0f, out torque);

                objectBase.RigidGroupOwner.WorldAccumulator.SetLocalTorque(ref torque);
            }
            else
                if (totalSwitchRightTime < 2.0f * maxSwitchRightTime)
                {
                    if (!doorColliding)
                    {
                        Vector3.Multiply(ref unitY, -2000.0f, out torque);

                        objectBase.RigidGroupOwner.WorldAccumulator.SetLocalTorque(ref torque);
                    }
                    else
                    {
                        totalSwitchRightTime = 0.0;
                    }
                }
                else
                    if (!doorColliding)
                    {
                        rightDoorConstraint.EnableControlAngleY = true;
                    }
                    else
                    {
                        totalSwitchRightTime = 0.0;
                    }

            totalSwitchRightTime += time;

            if (totalSwitchRightTime > 2.0f * maxSwitchRightTime)
                totalSwitchRightTime = 2.0f * maxSwitchRightTime;
        }

        void SwitchLeft(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if (leftDoorConstraint.IsBroken || leftDoorConstraint.PhysicsObject1.IsBrokenRigidGroup || leftDoorConstraint.PhysicsObject2.IsBrokenRigidGroup)
            {
                leftDoorConstraint.EnableControlAngleY = false;

                cabinBodyLeft1.DisableCollision(leftDoorRight, false);

                cabinBodyLeft2.DisableCollision(leftDoorUp, false);
                cabinBodyLeft2.DisableCollision(leftDoorDown, false);
                cabinBodyLeft2.DisableCollision(leftDoorLeft, false);

                cabinBodyUp1.DisableCollision(leftDoorUp, false);

                cabinBodyUp2.DisableCollision(leftDoorUp, false);
                cabinBodyUp2.DisableCollision(leftDoorRight, false);

                cabinBodyFrontLeft.DisableCollision(leftDoorUp, false);
                cabinBodyFrontLeft.DisableCollision(leftDoorRight, false);
                cabinBodyFrontLeft.DisableCollision(leftDoorDown, false);

                cabinBodyDown1.DisableCollision(leftDoorDown, false);

                cabinBodyDown2.DisableCollision(leftDoorDown, false);

                return;
            }

            PhysicsObject cameraBody = scene.GetPhysicsObjectWithActiveCamera(0).RigidGroupOwner.FindChildPhysicsObject(cameraBodyName, true, true);

            if (cameraBody == null)
            {
                leftDoorConstraint.EnableControlAngleY = true;
                return;
            }

            bool switchColliding = objectBase.IsColliding(cameraBody);
            bool doorColliding = objectBase.RigidGroupOwner.IsColliding(cameraBody);

            if (switchColliding)
            {
                totalSwitchLeftTime = 0.0;
                leftDoorConstraint.EnableControlAngleY = false;
            }

            if (totalSwitchLeftTime < maxSwitchLeftTime)
            {
                Vector3.Multiply(ref unitY, -2000.0f, out torque);

                objectBase.RigidGroupOwner.WorldAccumulator.SetLocalTorque(ref torque);
            }
            else
                if (totalSwitchLeftTime < 2.0f * maxSwitchLeftTime)
                {
                    if (!doorColliding)
                    {
                        Vector3.Multiply(ref unitY, 2000.0f, out torque);

                        objectBase.RigidGroupOwner.WorldAccumulator.SetLocalTorque(ref torque);
                    }
                    else
                    {
                        totalSwitchLeftTime = 0.0;
                    }
                }
                else
                    if (!doorColliding)
                    {
                        leftDoorConstraint.EnableControlAngleY = true;
                    }
                    else
                    {
                        totalSwitchLeftTime = 0.0;
                    }

            totalSwitchLeftTime += time;

            if (totalSwitchLeftTime > 2.0f * maxSwitchLeftTime)
                totalSwitchLeftTime = 2.0f * maxSwitchLeftTime;
        }
    }
}
