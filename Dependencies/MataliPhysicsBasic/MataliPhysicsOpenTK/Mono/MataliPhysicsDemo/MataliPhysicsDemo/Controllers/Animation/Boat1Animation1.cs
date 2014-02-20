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
    public class Boat1Animation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string cameraBodyName;
        string cameraDownName;
        string cameraConstraintName;

        PhysicsObject bodyBox0;
        PhysicsObject engineSwitch;

        Constraint constraint1;
        Constraint constraint2;

        PhysicsObject engine;
        PhysicsObject engineRotor;

        DemoKeyboardState oldKeyboardState;
        bool enableExternalMoving;

        Vector3 driverLocalPosition;

        Vector3 position;
        Matrix4 rotation;
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
        Vector3 unitZ;

        public Boat1Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            cameraBodyName = "Camera 2 Body";
            cameraDownName = "Camera 2 Down";
            cameraConstraintName = "Boat 1 Camera Constraint" + instanceIndexName;

            driverLocalPosition = new Vector3(0.0f, 4.3f, -11.0f);

            vectorZero = Vector3.Zero;
            matrixIdentity = Matrix4.Identity;
            quaternionIdentity = Quaternion.Identity;
            unitZ = Vector3.UnitZ;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers(bool enableExternalMoving)
        {
            bodyBox0 = scene.Factory.PhysicsObjectManager.Find("Boat 1 Body Box 0" + instanceIndexName);
            engineSwitch = scene.Factory.PhysicsObjectManager.Find("Boat 1 Engine Switch" + instanceIndexName);

            constraint1 = scene.Factory.ConstraintManager.Find("Boat 1 Constraint 1" + instanceIndexName);
            constraint2 = scene.Factory.ConstraintManager.Find("Boat 1 Constraint 2" + instanceIndexName);

            engine = scene.Factory.PhysicsObjectManager.Find("Boat 1 Engine" + instanceIndexName);
            engineRotor = scene.Factory.PhysicsObjectManager.Find("Boat 1 Engine Rotor" + instanceIndexName);

            oldKeyboardState = demo.GetKeyboardState();
            this.enableExternalMoving = enableExternalMoving;

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Boat 1 Body" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Move);
        }

        void Move(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if (physicsObjectWithActiveCamera == null) return;

            PhysicsObject cameraBody = physicsObjectWithActiveCamera.RigidGroupOwner.FindChildPhysicsObject(cameraBodyName, true, true);
            PhysicsObject cameraDown = physicsObjectWithActiveCamera.RigidGroupOwner.FindChildPhysicsObject(cameraDownName, true, true);

            if ((bodyBox0 == null) || (engineSwitch == null) || (cameraBody == null) || (cameraDown == null)) return;

            Constraint CameraConstraint = objectBase.Scene.Factory.ConstraintManager.Find(cameraConstraintName);

            bool rotorWorking = true;

            if (constraint2 != null)
            {
                if (constraint2.PhysicsObject1 != null)
                    if (constraint2.PhysicsObject1.IsBrokenRigidGroup || constraint2.IsBroken)
                        rotorWorking = false;

                if (constraint2.PhysicsObject2 != null)
                    if (constraint2.PhysicsObject2.IsBrokenRigidGroup || constraint2.IsBroken)
                        rotorWorking = false;
            }
            else
            {
                rotorWorking = false;
            }

            if (objectBase.IsBrokenRigidGroup || !rotorWorking)
            {
                if (constraint1 != null)
                    constraint1.EnableControlAngleY = false;

                if ((CameraConstraint != null) && (CameraConstraint.PhysicsObject1 == cameraDown))
                {
                    objectBase.Scene.Factory.ConstraintManager.Remove(CameraConstraint);
                    physicsObjectWithActiveCamera.Camera.EnableControl = false;

                    cameraDown.RigidGroupOwner.MaxPreUpdateAngularVelocity = 0.0f;
                    cameraDown.RigidGroupOwner.MaxPostUpdateAngularVelocity = 0.0f;

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
                    if (constraint1 != null)
                        constraint1.EnableControlAngleY = false;

                    objectBase.Scene.Factory.ConstraintManager.Remove(CameraConstraint);
                    physicsObjectWithActiveCamera.Camera.EnableControl = false;

                    cameraDown.RigidGroupOwner.MaxPreUpdateAngularVelocity = 0.0f;
                    cameraDown.RigidGroupOwner.MaxPostUpdateAngularVelocity = 0.0f;

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
                    if ((engine != null) && (engineRotor != null))
                    {
                        Vector3.Multiply(ref unitZ, -10.0f, out velocity);

                        engineRotor.MainWorldTransform.SetLocalAngularVelocity(ref velocity);

                        if (engineRotor.IsUnderFluidSurface)
                        {
                            Vector3.Multiply(ref unitZ, 5.0f, out velocity);

                            engine.MainWorldTransform.AddLocalLinearVelocity(ref velocity);
                        }
                    }
                }

                if (keyboardState[Key.S])
                {
                    if ((engine != null) && (engineRotor != null))
                    {
                        Vector3.Multiply(ref unitZ, 10.0f, out velocity);

                        engineRotor.MainWorldTransform.SetLocalAngularVelocity(ref velocity);

                        if (engineRotor.IsUnderFluidSurface)
                        {
                            Vector3.Multiply(ref unitZ, -5.0f, out velocity);

                            engine.MainWorldTransform.AddLocalLinearVelocity(ref velocity);
                        }
                    }
                }

                if (keyboardState[Key.D])
                {
                    if (constraint1 != null)
                        constraint1.ControlDegAngleY += 0.5f;
                }

                if (keyboardState[Key.A])
                {
                    if (constraint1 != null)
                        constraint1.ControlDegAngleY -= 0.5f;
                }
            }
            else
            {
                if (constraint1 != null)
                    constraint1.EnableControlAngleY = false;

                if (engineSwitch.IsColliding(cameraBody))
                {
                    if (keyboardState[Key.Space] && !oldKeyboardState[Key.Space])
                    {
                        physicsObjectWithActiveCamera.Camera.EnableControl = true;

                        if (constraint1 != null)
                            constraint1.EnableControlAngleY = true;

                        constraint1.ControlDegAngleY = 0.0f;

                        cameraDown.RigidGroupOwner.MaxPreUpdateAngularVelocity = 1000.0f;
                        cameraDown.RigidGroupOwner.MaxPostUpdateAngularVelocity = 1000.0f;

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

                        objectOrientation = quaternionIdentity;
                        objectRotation = Matrix4.CreateFromQuaternion(objectOrientation);

                        Vector3 boatDeckPosition = vectorZero;
                        Matrix4 boatDeckRotation = matrixIdentity;

                        bodyBox0.MainWorldTransform.GetPosition(ref boatDeckPosition);
                        bodyBox0.MainWorldTransform.GetRotation(ref boatDeckRotation);

                        Matrix4.Mult(ref objectRotation, ref boatDeckRotation, out rotation);

                        Vector3.TransformVector(ref driverLocalPosition, ref boatDeckRotation, out position);
                        Vector3.Add(ref boatDeckPosition, ref position, out position);

                        physicsObjectWithActiveCamera.RigidGroupOwner.MainWorldTransform.SetRotation(ref rotation);
                        physicsObjectWithActiveCamera.RigidGroupOwner.MainWorldTransform.SetPosition(ref position);
                        physicsObjectWithActiveCamera.RigidGroupOwner.RecalculateMainTransform();

                        if (CameraConstraint == null)
                        {
                            CameraConstraint = scene.Factory.ConstraintManager.Create(cameraConstraintName);
                            CameraConstraint.PhysicsObject1 = cameraDown;
                            CameraConstraint.PhysicsObject2 = bodyBox0;
                            CameraConstraint.PhysicsObject1.MainWorldTransform.GetPosition(ref position1);
                            CameraConstraint.PhysicsObject1.MainWorldTransform.GetOrientation(ref orientation1);
                            CameraConstraint.PhysicsObject2.MainWorldTransform.GetOrientation(ref orientation2);

                            boatDeckPosition.X = boatDeckPosition.Z = 0.0f;
                            boatDeckPosition.Y = 2.0f;
                            Vector3.Subtract(ref position1, ref boatDeckPosition, out position1);

                            CameraConstraint.SetAnchor1(ref position1);
                            CameraConstraint.SetAnchor2(ref position1);
                            CameraConstraint.SetInitWorldOrientation1(ref orientation1);
                            CameraConstraint.SetInitWorldOrientation2(ref orientation2);
                            CameraConstraint.EnableLimitAngleX = true;
                            CameraConstraint.EnableLimitAngleY = true;
                            CameraConstraint.EnableLimitAngleZ = true;
                            CameraConstraint.LimitAngleForce = 0.6f;
                            CameraConstraint.Update();
                        }
                    }
                }

                if (enableExternalMoving)
                {
                    if (keyboardState[Key.Up])
                    {
                        if (constraint1 != null)
                            constraint1.EnableControlAngleY = true;

                        if ((engine != null) && (engineRotor != null))
                        {
                            Vector3.Multiply(ref unitZ, -10.0f, out velocity);

                            engineRotor.MainWorldTransform.SetLocalAngularVelocity(ref velocity);

                            if (engineRotor.IsUnderFluidSurface)
                            {
                                Vector3.Multiply(ref unitZ, 5.0f, out velocity);

                                engine.MainWorldTransform.AddLocalLinearVelocity(ref velocity);
                            }
                        }
                    }

                    if (keyboardState[Key.Down])
                    {
                        if (constraint1 != null)
                            constraint1.EnableControlAngleY = true;

                        if ((engine != null) && (engineRotor != null))
                        {
                            Vector3.Multiply(ref unitZ, 10.0f, out velocity);

                            engineRotor.MainWorldTransform.SetLocalAngularVelocity(ref velocity);

                            if (engineRotor.IsUnderFluidSurface)
                            {
                                Vector3.Multiply(ref unitZ, -5.0f, out velocity);

                                engine.MainWorldTransform.AddLocalLinearVelocity(ref velocity);
                            }
                        }
                    }

                    if (keyboardState[Key.Right])
                    {
                        if (constraint1 != null)
                            constraint1.EnableControlAngleY = true;

                        if (constraint1 != null)
                            constraint1.ControlDegAngleY += 0.5f;
                    }

                    if (keyboardState[Key.Left])
                    {
                        if (constraint1 != null)
                            constraint1.EnableControlAngleY = true;

                        if (constraint1 != null)
                            constraint1.ControlDegAngleY -= 0.5f;
                    }
                }
            }

            oldKeyboardState = keyboardState;
        }
    }
}
