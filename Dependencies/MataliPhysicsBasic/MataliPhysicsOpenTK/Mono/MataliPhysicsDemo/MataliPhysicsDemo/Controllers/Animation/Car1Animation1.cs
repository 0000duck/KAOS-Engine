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
    public class Car1Animation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string cameraBodyName;
        string cameraDownName;
        string cameraConstraintName;

        PhysicsObject steeringGearSwitch;
        PhysicsObject steeringGearDown;
        PhysicsObject chassisUpPanel;
        PhysicsObject chassisDownPanel;

        PhysicsObject steeringWheel;
        PhysicsObject chassisBack;

        PhysicsObject wheel1;
        PhysicsObject wheel2;

        Constraint steeringGearConstraint;
        Constraint constraint1;
        Constraint constraint4;

        Constraint rightDoorConstraint;
        PhysicsObject rightDoorBody;

        Constraint leftDoorConstraint;
        PhysicsObject leftDoorBody;

        PhysicsObject chassisMiddleDown;
        PhysicsObject chassisMiddleFront;
        PhysicsObject chassisMiddleBack;
        PhysicsObject upBodyFront;
        PhysicsObject upBodyDownBack;

        DemoKeyboardState oldKeyboardState;

        double totalSwitchRightTime;
        double totalSwitchLeftTime;
        bool enableExternalMoving;

        float maxSwitchRightTime;
        float maxSwitchLeftTime;
        Vector3 driverLocalPosition;

        Vector3 position;
        Matrix4 rotation;
        Vector3 torque;
        Vector3 velocity;
        Quaternion objectOrientation;
        Matrix4 objectRotation;
        Matrix4 objectInitRotation;

        Vector3 position1;
        Quaternion orientation1;
        Quaternion orientation2;

        Vector3 vectorZero;
        Matrix4 matrixIdentity;
        Quaternion quaternionIdentity;
        Vector3 unitY;

        public Car1Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            cameraBodyName = "Camera 2 Body";
            cameraDownName = "Camera 2 Down";
            cameraConstraintName = "Car 1 Camera Constraint" + instanceIndexName;

            maxSwitchRightTime = 10.0f;
            maxSwitchLeftTime = 10.0f;
            driverLocalPosition = new Vector3(2.0f, 4.3f, 0.7f);

            vectorZero = Vector3.Zero;
            matrixIdentity = Matrix4.Identity;
            quaternionIdentity = Quaternion.Identity;
            unitY = Vector3.UnitY;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers(bool enableExternalMoving)
        {
            steeringGearSwitch = scene.Factory.PhysicsObjectManager.Find("Car 1 Steering Gear Switch" + instanceIndexName);
            steeringGearDown = scene.Factory.PhysicsObjectManager.Find("Car 1 Steering Gear Down" + instanceIndexName);
            chassisUpPanel = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Up Panel" + instanceIndexName);
            chassisDownPanel = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Down Panel" + instanceIndexName);

            steeringWheel = scene.Factory.PhysicsObjectManager.Find("Car 1 Steering Wheel" + instanceIndexName);
            chassisBack = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Back" + instanceIndexName);

            wheel1 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 1" + instanceIndexName);
            wheel2 = scene.Factory.PhysicsObjectManager.Find("Car 1 Wheel 2" + instanceIndexName);

            steeringGearConstraint = scene.Factory.ConstraintManager.Find("Car 1 Steering Gear Constraint" + instanceIndexName);
            constraint1 = scene.Factory.ConstraintManager.Find("Car 1 Constraint 1" + instanceIndexName);
            constraint4 = scene.Factory.ConstraintManager.Find("Car 1 Constraint 4" + instanceIndexName);

            rightDoorConstraint = scene.Factory.ConstraintManager.Find("Car 1 Right Door Constraint" + instanceIndexName);
            rightDoorBody = scene.Factory.PhysicsObjectManager.Find("Car 1 Right Door Body" + instanceIndexName);

            leftDoorConstraint = scene.Factory.ConstraintManager.Find("Car 1 Left Door Constraint" + instanceIndexName);
            leftDoorBody = scene.Factory.PhysicsObjectManager.Find("Car 1 Left Door Body" + instanceIndexName);

            chassisMiddleDown = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Down" + instanceIndexName);
            chassisMiddleFront = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Front" + instanceIndexName);
            chassisMiddleBack = scene.Factory.PhysicsObjectManager.Find("Car 1 Chassis Middle Back" + instanceIndexName);
            upBodyFront = scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Front" + instanceIndexName);
            upBodyDownBack = scene.Factory.PhysicsObjectManager.Find("Car 1 Up Body Down Back" + instanceIndexName);

            oldKeyboardState = demo.GetKeyboardState();
            this.enableExternalMoving = enableExternalMoving;

            totalSwitchLeftTime = 2.0f * maxSwitchLeftTime;
            totalSwitchRightTime = 2.0f * maxSwitchRightTime;

            PhysicsObject objectBase = null;

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Body" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Move);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Left Door Switch" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(SwitchLeft);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Car 1 Right Door Switch" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(SwitchRight);
        }

        public void Move(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if (physicsObjectWithActiveCamera == null) return;

            PhysicsObject cameraBody = physicsObjectWithActiveCamera.RigidGroupOwner.FindChildPhysicsObject(cameraBodyName, true, true);
            PhysicsObject cameraDown = physicsObjectWithActiveCamera.RigidGroupOwner.FindChildPhysicsObject(cameraDownName, true, true);

            if ((chassisMiddleDown == null) || (steeringGearSwitch == null)) return;

            Constraint CameraConstraint = objectBase.Scene.Factory.ConstraintManager.Find(cameraConstraintName);

            bool steeringGearWorking = true;

            if (steeringGearConstraint != null)
            {
                if (steeringGearConstraint.PhysicsObject1 != null)
                    if (steeringGearConstraint.PhysicsObject1.IsBrokenRigidGroup || steeringGearConstraint.IsBroken)
                        steeringGearWorking = false;

                if (steeringGearConstraint.PhysicsObject2 != null)
                    if (steeringGearConstraint.PhysicsObject2.IsBrokenRigidGroup || steeringGearConstraint.IsBroken)
                        steeringGearWorking = false;
            }
            else
            {
                steeringGearWorking = false;
            }

            if (!steeringGearWorking)
            {
                steeringGearDown.DisableCollision(chassisUpPanel, false);
                steeringGearDown.DisableCollision(chassisDownPanel, false);
                steeringGearDown.DisableCollision(upBodyFront, false);
                steeringGearDown.DisableCollision(upBodyDownBack, false);
            }

            if (objectBase.IsBrokenRigidGroup || !steeringGearWorking)
            {
                if ((CameraConstraint != null) && (CameraConstraint.PhysicsObject1 == cameraDown))
                {
                    objectBase.Scene.Factory.ConstraintManager.Remove(CameraConstraint);
                    physicsObjectWithActiveCamera.Camera.EnableControl = false;

                    cameraDown.RigidGroupOwner.MaxPreUpdateAngularVelocity = 0.0f;
                    cameraDown.RigidGroupOwner.MaxPostUpdateAngularVelocity = 0.0f;

                    cameraDown.DisableCollision(steeringWheel, false);
                    cameraDown.DisableCollision(chassisBack, false);
                    cameraDown.DisableCollision(chassisMiddleBack, false);
                    cameraDown.DisableCollision(upBodyFront, false);
                    cameraDown.DisableCollision(upBodyDownBack, false);

                    cameraBody.DisableCollision(steeringGearDown, false);
                    cameraBody.DisableCollision(steeringWheel, false);
                    cameraBody.DisableCollision(chassisBack, false);
                    cameraBody.DisableCollision(chassisMiddleBack, false);
                    cameraBody.DisableCollision(upBodyFront, false);
                    cameraBody.DisableCollision(upBodyDownBack, false);

                    Vector3 euler = vectorZero;
                    Vector3 cameraEuler = vectorZero;
                    Vector3 objectEuler = vectorZero;

                    physicsObjectWithActiveCamera.Camera.GetEuler(ref cameraEuler);
                    physicsObjectWithActiveCamera.MainWorldTransform.GetTransposeRotation(ref objectRotation);
                    physicsObjectWithActiveCamera.InitLocalTransform.GetRotation(ref objectInitRotation);
                    Matrix4.Mult(ref objectRotation, ref objectInitRotation, out rotation);

                    physicsObjectWithActiveCamera.Camera.SetEuler(ref rotation);
                    physicsObjectWithActiveCamera.Camera.GetEuler(ref objectEuler);
                    Vector3.Add(ref objectEuler, ref cameraEuler, out euler);

                    Matrix4 rotationX, rotationY;
                    Matrix4.CreateRotationX(-euler.X, out rotationX);
                    Matrix4.CreateRotationY(-euler.Y, out rotationY);
                    Matrix4.Mult(ref rotationY, ref rotationX, out rotation);

                    physicsObjectWithActiveCamera.Camera.SetEuler(ref euler);
                    physicsObjectWithActiveCamera.Camera.SetRotation(ref rotation);

                    Matrix4.CreateRotationY(euler.Y, out rotation);

                    physicsObjectWithActiveCamera.RigidGroupOwner.MainWorldTransform.SetRotation(ref rotation);
                    physicsObjectWithActiveCamera.RigidGroupOwner.RecalculateMainTransform();
                }

                return;
            }

            DemoKeyboardState keyboardState = demo.GetKeyboardState();

            if (physicsObjectWithActiveCamera.Camera.EnableControl && (CameraConstraint != null) && (CameraConstraint.PhysicsObject1 == cameraDown))
            {
                if (keyboardState[Key.Space] && !oldKeyboardState[Key.Space])
                {
                    objectBase.Scene.Factory.ConstraintManager.Remove(CameraConstraint);
                    physicsObjectWithActiveCamera.Camera.EnableControl = false;

                    cameraDown.RigidGroupOwner.MaxPreUpdateAngularVelocity = 0.0f;
                    cameraDown.RigidGroupOwner.MaxPostUpdateAngularVelocity = 0.0f;

                    cameraDown.DisableCollision(steeringWheel, false);
                    cameraDown.DisableCollision(chassisBack, false);
                    cameraDown.DisableCollision(chassisMiddleBack, false);
                    cameraDown.DisableCollision(upBodyFront, false);
                    cameraDown.DisableCollision(upBodyDownBack, false);

                    cameraBody.DisableCollision(steeringGearDown, false);
                    cameraBody.DisableCollision(steeringWheel, false);
                    cameraBody.DisableCollision(chassisBack, false);
                    cameraBody.DisableCollision(chassisMiddleBack, false);
                    cameraBody.DisableCollision(upBodyFront, false);
                    cameraBody.DisableCollision(upBodyDownBack, false);

                    Vector3 euler = vectorZero;
                    Vector3 cameraEuler = vectorZero;
                    Vector3 objectEuler = vectorZero;

                    physicsObjectWithActiveCamera.Camera.GetEuler(ref cameraEuler);
                    physicsObjectWithActiveCamera.MainWorldTransform.GetTransposeRotation(ref objectRotation);
                    physicsObjectWithActiveCamera.InitLocalTransform.GetRotation(ref objectInitRotation);
                    Matrix4.Mult(ref objectRotation, ref objectInitRotation, out rotation);

                    physicsObjectWithActiveCamera.Camera.SetEuler(ref rotation);
                    physicsObjectWithActiveCamera.Camera.GetEuler(ref objectEuler);
                    Vector3.Add(ref objectEuler, ref cameraEuler, out euler);
                    euler.Z = 0.0f;

                    Matrix4 rotationX, rotationY;
                    Matrix4.CreateRotationX(-euler.X, out rotationX);
                    Matrix4.CreateRotationY(-euler.Y, out rotationY);
                    Matrix4.Mult(ref rotationY, ref rotationX, out rotation);

                    physicsObjectWithActiveCamera.Camera.SetEuler(ref euler);
                    physicsObjectWithActiveCamera.Camera.SetRotation(ref rotation);

                    Matrix4.CreateRotationY(euler.Y, out rotation);

                    physicsObjectWithActiveCamera.RigidGroupOwner.MainWorldTransform.SetRotation(ref rotation);
                    physicsObjectWithActiveCamera.RigidGroupOwner.RecalculateMainTransform();

                    oldKeyboardState = keyboardState;

                    return;
                }

                if (keyboardState[Key.W])
                {
                    if (wheel1 != null)
                    {
                        Vector3.Multiply(ref unitY, 5.0f, out velocity);

                        wheel1.MainWorldTransform.AddLocalAngularVelocity(ref velocity);
                    }

                    if (wheel2 != null)
                    {
                        Vector3.Multiply(ref unitY, 5.0f, out velocity);

                        wheel2.MainWorldTransform.AddLocalAngularVelocity(ref velocity);
                    }
                }

                if (keyboardState[Key.S])
                {
                    if (wheel1 != null)
                    {
                        Vector3.Multiply(ref unitY, -5.0f, out velocity);

                        wheel1.MainWorldTransform.AddLocalAngularVelocity(ref velocity);
                    }

                    if (wheel2 != null)
                    {
                        Vector3.Multiply(ref unitY, -5.0f, out velocity);

                        wheel2.MainWorldTransform.AddLocalAngularVelocity(ref velocity);
                    }
                }

                if (keyboardState[Key.D])
                {
                    if (steeringGearConstraint != null)
                        steeringGearConstraint.ControlDegAngleY -= 2.0f;

                    if (constraint1 != null)
                        constraint1.ControlDegAngleY += 1.0f;

                    if (constraint4 != null)
                        constraint4.ControlDegAngleY += 1.0f;
                }

                if (keyboardState[Key.A])
                {
                    if (steeringGearConstraint != null)
                        steeringGearConstraint.ControlDegAngleY += 2.0f;

                    if (constraint1 != null)
                        constraint1.ControlDegAngleY -= 1.0f;

                    if (constraint4 != null)
                        constraint4.ControlDegAngleY -= 1.0f;
                }
            }
            else
            {
                if ((cameraBody != null) && steeringGearSwitch.IsColliding(cameraBody))
                {
                    if (keyboardState[Key.Space] && !oldKeyboardState[Key.Space])
                    {
                        physicsObjectWithActiveCamera.Camera.EnableControl = true;

                        cameraDown.RigidGroupOwner.MaxPreUpdateAngularVelocity = 1000.0f;
                        cameraDown.RigidGroupOwner.MaxPostUpdateAngularVelocity = 1000.0f;

                        cameraDown.DisableCollision(steeringWheel, true);
                        cameraDown.DisableCollision(chassisBack, true);
                        cameraDown.DisableCollision(chassisMiddleBack, true);
                        cameraDown.DisableCollision(upBodyFront, true);
                        cameraDown.DisableCollision(upBodyDownBack, true);

                        cameraBody.DisableCollision(steeringGearDown, true);
                        cameraBody.DisableCollision(steeringWheel, true);
                        cameraBody.DisableCollision(chassisBack, true);
                        cameraBody.DisableCollision(chassisMiddleBack, true);
                        cameraBody.DisableCollision(upBodyFront, true);
                        cameraBody.DisableCollision(upBodyDownBack, true);

                        Quaternion cameraOrientationX = quaternionIdentity;
                        Quaternion cameraOrientationY = quaternionIdentity;
                        Quaternion cameraOrientationZ = quaternionIdentity;
                        Quaternion cameraOrientationXY = quaternionIdentity;
                        Quaternion cameraOrientation = quaternionIdentity;

                        Quaternion.Multiply(ref cameraOrientationX, ref cameraOrientationY, out cameraOrientationXY);
                        Quaternion.Multiply(ref cameraOrientationXY, ref cameraOrientationZ, out cameraOrientation);
                        rotation = Matrix4.CreateFromQuaternion(cameraOrientation);

                        physicsObjectWithActiveCamera.Camera.SetOrientation(ref cameraOrientation);
                        physicsObjectWithActiveCamera.Camera.SetRotation(ref rotation);
                        physicsObjectWithActiveCamera.Camera.SetEuler(ref rotation);

                        objectOrientation = Quaternion.FromAxisAngle(unitY, MathHelper.DegreesToRadians(180.0f));
                        objectRotation = Matrix4.CreateFromQuaternion(objectOrientation);

                        Vector3 carChassisPosition = vectorZero;
                        Matrix4 carChassisRotation = matrixIdentity;

                        chassisMiddleDown.MainWorldTransform.GetPosition(ref carChassisPosition);
                        chassisMiddleDown.MainWorldTransform.GetRotation(ref carChassisRotation);

                        Matrix4.Mult(ref objectRotation, ref carChassisRotation, out rotation);

                        Vector3.TransformVector(ref driverLocalPosition, ref carChassisRotation, out position);
                        Vector3.Add(ref carChassisPosition, ref position, out position);

                        physicsObjectWithActiveCamera.RigidGroupOwner.MainWorldTransform.SetRotation(ref rotation);
                        physicsObjectWithActiveCamera.RigidGroupOwner.MainWorldTransform.SetPosition(ref position);
                        physicsObjectWithActiveCamera.RigidGroupOwner.RecalculateMainTransform();

                        if (CameraConstraint == null)
                        {
                            CameraConstraint = scene.Factory.ConstraintManager.Create(cameraConstraintName);
                            CameraConstraint.PhysicsObject1 = cameraDown;
                            CameraConstraint.PhysicsObject2 = chassisMiddleDown;
                            CameraConstraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                            CameraConstraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                            CameraConstraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);

                            carChassisPosition.X = carChassisPosition.Z = 0.0f;
                            carChassisPosition.Y = 2.0f;
                            Vector3.Subtract(ref position1, ref carChassisPosition, out position1);

                            CameraConstraint.SetAnchor1(ref position1);
                            CameraConstraint.SetAnchor2(ref position1);
                            CameraConstraint.SetInitWorldOrientation1(ref orientation1);
                            CameraConstraint.SetInitWorldOrientation2(ref orientation2);
                            CameraConstraint.EnableLimitAngleX = true;
                            CameraConstraint.EnableLimitAngleY = true;
                            CameraConstraint.EnableLimitAngleZ = true;
                            CameraConstraint.LimitAngleForce = 0.4f;
                            CameraConstraint.MinResponseLinearVelocity = 0.005f;
                            CameraConstraint.MinResponseAngularVelocity = 0.005f;
                            CameraConstraint.Update();
                        }
                    }
                }

                if (enableExternalMoving)
                {
                    if (keyboardState[Key.Up])
                    {
                        if (wheel1 != null)
                        {
                            Vector3.Multiply(ref unitY, 5.0f, out velocity);

                            wheel1.MainWorldTransform.AddLocalAngularVelocity(ref velocity);
                        }

                        if (wheel2 != null)
                        {
                            Vector3.Multiply(ref unitY, 5.0f, out velocity);

                            wheel2.MainWorldTransform.AddLocalAngularVelocity(ref velocity);
                        }
                    }

                    if (keyboardState[Key.Down])
                    {
                        if (wheel1 != null)
                        {
                            Vector3.Multiply(ref unitY, -5.0f, out velocity);

                            wheel1.MainWorldTransform.AddLocalAngularVelocity(ref velocity);
                        }

                        if (wheel2 != null)
                        {
                            Vector3.Multiply(ref unitY, -5.0f, out velocity);

                            wheel2.MainWorldTransform.AddLocalAngularVelocity(ref velocity);
                        }
                    }

                    if (keyboardState[Key.Right])
                    {
                        if (steeringGearConstraint != null)
                            steeringGearConstraint.ControlDegAngleY -= 2.0f;

                        if (constraint1 != null)
                            constraint1.ControlDegAngleY += 1.0f;

                        if (constraint4 != null)
                            constraint4.ControlDegAngleY += 1.0f;
                    }

                    if (keyboardState[Key.Left])
                    {
                        if (steeringGearConstraint != null)
                            steeringGearConstraint.ControlDegAngleY += 2.0f;

                        if (constraint1 != null)
                            constraint1.ControlDegAngleY -= 1.0f;

                        if (constraint4 != null)
                            constraint4.ControlDegAngleY -= 1.0f;
                    }
                }
            }

            oldKeyboardState = keyboardState;
        }

        public void SwitchRight(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if (rightDoorConstraint.IsBroken || rightDoorConstraint.PhysicsObject1.IsBrokenRigidGroup || rightDoorConstraint.PhysicsObject2.IsBrokenRigidGroup)
            {
                rightDoorConstraint.EnableControlAngleY = false;

                rightDoorBody.DisableCollision(chassisMiddleDown, false);
                rightDoorBody.DisableCollision(chassisMiddleFront, false);
                rightDoorBody.DisableCollision(chassisMiddleBack, false);
                rightDoorBody.DisableCollision(upBodyFront, false);
                rightDoorBody.DisableCollision(upBodyDownBack, false);

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

        public void SwitchLeft(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if (leftDoorConstraint.IsBroken || leftDoorConstraint.PhysicsObject1.IsBrokenRigidGroup || leftDoorConstraint.PhysicsObject2.IsBrokenRigidGroup)
            {
                leftDoorConstraint.EnableControlAngleY = false;

                leftDoorBody.DisableCollision(chassisMiddleDown, false);
                leftDoorBody.DisableCollision(chassisMiddleFront, false);
                leftDoorBody.DisableCollision(chassisMiddleBack, false);
                leftDoorBody.DisableCollision(upBodyFront, false);
                leftDoorBody.DisableCollision(upBodyDownBack, false);

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
