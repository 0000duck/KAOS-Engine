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
    public class Camera2Animation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string sphereName;
        string cursorName;
        string cursorAName;
        string cursorBName;
        string shotName;
        string shotBaseName;
        string shotLightName;
        string yellowName;
        string hitName;
        string defaultName;

        Shape sphere;
        PhysicsObject cameraUp;
        PhysicsObject cameraBody;
        PhysicsObject cameraDown;

        PhysicsObject[] shotTab;
        bool enableShotTab;
        int shotCount;

        DemoMouseState oldMouseState;
        DemoKeyboardState oldKeyboardState;

        bool enableDistanceCollision;

        bool enableDistance;
        bool enableControl;
        float distance;

        float maxTangentLength;
        float maxDistance;

        Vector3 position;
        Vector3 direction;
        Matrix4 rotation;
        Matrix4 cameraRotation;
        Matrix4 projection;
        Matrix4 view;

        Vector3 startPoint;
        Vector3 endPoint;
        Vector3 hitPoint;
        Vector3 hitDistance;

        Vector3 listenerPosition;
        Vector3 listenerTopDirection;
        Vector3 listenerFrontDirection;
        Vector3 hitPosition;
        Vector3 rollPosition;
        Vector3 slidePosition;
        Vector3 backgroundPosition;

        float hitVolume;
        float rollVolume;
        float slideVolume;
        float backgroundVolume;
        float listenerRange;

        Vector3 fluidNormal;
        Vector3 moveForce;
        Vector2 mousePosition;

        Random random;

        DemoListener listener;
        DemoEmitter emitter;
        DemoSoundGroup shotSoundGroup;
        DemoSound shotSound;

        Vector3 vectorZero;
        Matrix4 matrixIdentity;
        Quaternion quaternionIdentity;

        public Camera2Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            sphereName = "Sphere";
            cursorName = "Cursor";
            cursorAName = "Cursor A";
            cursorBName = "Cursor B";
            shotName = "Camera 2 Shot" + instanceIndexName + " ";
            shotBaseName = "Camera 2 Shot Object" + instanceIndexName + " ";
            shotLightName = "Camera 2 Shot Light" + instanceIndexName + " ";
            yellowName = "Yellow";
            hitName = "Hit";
            defaultName = "Default";

            maxTangentLength = 10.0f;
            maxDistance = -10.0f;

            shotTab = new PhysicsObject[10];

            random = new Random();

            listener = new DemoListener();
            emitter = new DemoEmitter();

            vectorZero = Vector3.Zero;
            matrixIdentity = Matrix4.Identity;
            quaternionIdentity = Quaternion.Identity;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers(bool enableDistanceCollision)
        {
            sphere = scene.Factory.ShapeManager.Find("Sphere");
            cameraUp = scene.Factory.PhysicsObjectManager.Find("Camera 2 Up" + instanceIndexName);
            cameraBody = scene.Factory.PhysicsObjectManager.Find("Camera 2 Body" + instanceIndexName);
            cameraDown = scene.Factory.PhysicsObjectManager.Find("Camera 2 Down" + instanceIndexName);

            oldMouseState = demo.GetMouseState();
            oldKeyboardState = demo.GetKeyboardState();

            this.enableDistanceCollision = enableDistanceCollision;

            enableDistance = false;
            enableControl = false;
            distance = 0;

            shotCount = -1;
            enableShotTab = false;
            for (int i = 0; i < shotTab.Length; i++)
                shotTab[i] = null;

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Camera 2 Up" + instanceIndexName);
            if (objectBase != null)
            {
                objectBase.UserControllers.TransformMethods += new SimulateMethod(MoveCursor);
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Move);
            }
        }

        public void RefreshControllers()
        {
            oldMouseState = demo.GetMouseState();
            oldKeyboardState = demo.GetKeyboardState();
        }

        public void MoveCursor(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.Camera.Enabled) return;
            if (!objectBase.Camera.Active) return;

            float time = (float)args.Time;

            bool mouseButton = false;
            float mouseScrollWheel = 0.0f;

            DemoMouseState mouseState = demo.GetMouseState();

            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;
            mouseScrollWheel = mouseState.Wheel;
            mouseButton = mouseState[MouseButton.Left];

            bool hitMenu = false;
            if (demo.EnableMenu)
                hitMenu = (demo.MenuScene.MenuAnimation1.CurrentSwitch != null);

            objectBase.Camera.View.GetViewMatrix(ref view);
            objectBase.Camera.Projection.GetProjectionMatrix(ref projection);

            CursorController cursorController = objectBase.InternalControllers.CursorController;
            cursorController.SetViewport(0, 0, demo.WindowWidth, demo.WindowHeight, 0.0f, 1.0f);
            cursorController.SetViewMatrix(ref view);
            cursorController.SetProjectionMatrix(ref projection);
            cursorController.SetMousePosition(ref mousePosition);
            cursorController.MouseButton = mouseButton && !hitMenu;
            cursorController.MouseScrollWheel = mouseScrollWheel;
            cursorController.DragWheelSpeed = 50.0f;
            cursorController.WindowActive = true;
            cursorController.Update();
        }

        public void Move(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (!objectBase.Camera.Enabled) return;
            if (!objectBase.Camera.Active) return;

            float time = (float)args.Time;

            Vector3 deltaRotation = vectorZero;
            Vector3 deltaTranslation = vectorZero;
            float rotationSpeed = 8.0f;
            float translationSpeed = 8.0f;
            float jumpSpeed = 8.0f;
            float swimUpSpeed = 0.2f;
            float swimUpOnSurfaceSpeed = 0.06f;
            float translationInFluidFactor = 0.15f;
            float soundPositionFactor = 0.1f;
            bool enableJump = false;
            bool enableSwimUp = false;
            bool enableShot = false;

            bool cameraBodyCollision = cameraBody.IsColliding();
            bool cameraDownCollision = cameraDown.IsColliding();

            DemoMouseState mouseState = demo.GetMouseState();
            DemoKeyboardState keyboardState = demo.GetKeyboardState();

            if (mouseState[MouseButton.Right])
            {
                deltaRotation.Y += MathHelper.DegreesToRadians(rotationSpeed * (mouseState.X - oldMouseState.X) * time);
                deltaRotation.X += MathHelper.DegreesToRadians(rotationSpeed * (mouseState.Y - oldMouseState.Y) * time);
            }

            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            if (!objectBase.Camera.EnableControl)
            {
                if (mouseState[MouseButton.Middle] && !oldMouseState[MouseButton.Middle])
                    enableShot = true;

                if ((keyboardState[Key.ControlRight] && !oldKeyboardState[Key.ControlRight]) ||
                   (keyboardState[Key.ControlLeft] && !oldKeyboardState[Key.ControlLeft]))
                    enableShot = true;
            }

            PhysicsObject cursorA = scene.Factory.PhysicsObjectManager.Find(cursorAName);
            PhysicsObject cursorB = scene.Factory.PhysicsObjectManager.Find(cursorBName);

            if (!demo.EnableMenu)
            {
                if (cursorA != null)
                    cursorA.EnableDrawing = true;

                if (cursorB != null)
                    cursorB.EnableDrawing = true;
            }
            else
            {
                if (cursorA != null)
                    cursorA.EnableDrawing = false;

                if (cursorB != null)
                    cursorB.EnableDrawing = false;
            }

            if (!objectBase.Camera.EnableControl)
            {
                if (keyboardState[Key.W])
                    deltaTranslation.Z += translationSpeed * time;

                if (keyboardState[Key.S])
                    deltaTranslation.Z -= translationSpeed * time;

                if (keyboardState[Key.D])
                    deltaTranslation.X += translationSpeed * time;

                if (keyboardState[Key.A])
                    deltaTranslation.X -= translationSpeed * time;

                if (keyboardState[Key.Space] && !oldKeyboardState[Key.Space])
                    enableJump = true;

                if (keyboardState[Key.Space])
                    enableSwimUp = true;
            }

            if (keyboardState[Key.Tab] && !oldKeyboardState[Key.Tab])
                enableDistance = !enableDistance;

            oldMouseState = mouseState;
            oldKeyboardState = keyboardState;

            Vector3 gravityDirection = vectorZero;
            scene.GetGravityDirection(ref gravityDirection);

            if (!objectBase.Camera.EnableControl)
            {
                if (deltaRotation.LengthSquared != 0.0f)
                {
                    Vector3 euler = vectorZero;
                    objectBase.Camera.GetEuler(ref euler);
                    Vector3.Add(ref euler, ref deltaRotation, out euler);
                    objectBase.Camera.SetEuler(ref euler);

                    Matrix4 rotationX, rotationY;
                    Matrix4.CreateRotationX(-euler.X, out rotationX);
                    Matrix4.CreateRotationY(-euler.Y, out rotationY);
                    Matrix4.Mult(ref rotationY, ref rotationX, out cameraRotation);

                    objectBase.Camera.SetRotation(ref cameraRotation);

                    Matrix4.CreateRotationY(euler.Y, out rotation);

                    objectBase.RigidGroupOwner.MainWorldTransform.SetRotation(ref rotation);
                    objectBase.RigidGroupOwner.RecalculateMainTransform();
                }
            }
            else
            {
                Vector3 euler = vectorZero;
                Matrix4 objectRotation = matrixIdentity;
                objectBase.Camera.GetEuler(ref euler);
                Vector3.Add(ref euler, ref deltaRotation, out euler);
                objectBase.RigidGroupOwner.MainWorldTransform.GetRotation(ref objectRotation);

                Matrix4 rotationX, rotationY;
                Matrix4.CreateRotationX(euler.X, out rotationX);
                Matrix4.CreateRotationY(euler.Y, out rotationY);

                Matrix4.Mult(ref rotationX, ref rotationY, out cameraRotation);
                Matrix4.Mult(ref cameraRotation, ref objectRotation, out rotation);

                objectBase.Camera.SetEuler(ref euler);
                objectBase.Camera.SetTransposeRotation(ref rotation);
            }

            if (deltaTranslation.LengthSquared != 0.0f)
            {
                if (objectBase.RigidGroupOwner.IsUnderFluidSurface)
                {
                    objectBase.RigidGroupOwner.MaxPreUpdateLinearVelocity = 10.0f;
                    objectBase.RigidGroupOwner.MaxPostUpdateLinearVelocity = 10.0f;

                    if (enableSwimUp)
                    {
                        objectBase.InitLocalTransform.GetTransposeRotation(ref rotation);
                        Vector3.TransformVector(ref deltaTranslation, ref rotation, out direction);

                        objectBase.MainWorldTransform.GetRotation(ref rotation);
                        Vector3.TransformVector(ref direction, ref rotation, out moveForce);
                        Vector3.Multiply(ref moveForce, translationInFluidFactor * objectBase.RigidGroupOwner.Integral.Mass / (time * time), out moveForce);

                        objectBase.RigidGroupOwner.WorldAccumulator.AddWorldForce(ref moveForce);
                    }
                    else
                    {
                        objectBase.Camera.GetTransposeRotation(ref rotation);
                        Vector3.TransformVector(ref deltaTranslation, ref rotation, out moveForce);
                        Vector3.Multiply(ref moveForce, translationInFluidFactor * objectBase.RigidGroupOwner.Integral.Mass / (time * time), out moveForce);

                        objectBase.RigidGroupOwner.WorldAccumulator.AddWorldForce(ref moveForce);
                    }
                }
                else
                {
                    if (cameraDownCollision)
                    {
                        objectBase.RigidGroupOwner.MaxPreUpdateLinearVelocity = 100000.0f;
                        objectBase.RigidGroupOwner.MaxPostUpdateLinearVelocity = 100000.0f;

                        objectBase.InitLocalTransform.GetTransposeRotation(ref rotation);
                        Vector3.TransformVector(ref deltaTranslation, ref rotation, out direction);

                        objectBase.MainWorldTransform.GetRotation(ref rotation);
                        Vector3.TransformVector(ref direction, ref rotation, out moveForce);
                        Vector3.Multiply(ref moveForce, objectBase.RigidGroupOwner.Integral.Mass / (time * time), out moveForce);

                        objectBase.RigidGroupOwner.WorldAccumulator.AddWorldForce(ref moveForce);
                        cameraDown.UpdateFeedbackForce(ref moveForce);
                    }
                    else
                    {
                        if (objectBase.RigidGroupOwner.IsInFluid)
                        {
                            objectBase.RigidGroupOwner.MaxPreUpdateLinearVelocity = 10.0f;
                            objectBase.RigidGroupOwner.MaxPostUpdateLinearVelocity = 10.0f;

                            objectBase.InitLocalTransform.GetTransposeRotation(ref rotation);
                            Vector3.TransformVector(ref deltaTranslation, ref rotation, out direction);

                            objectBase.MainWorldTransform.GetRotation(ref rotation);
                            Vector3.TransformVector(ref direction, ref rotation, out moveForce);
                            Vector3.Multiply(ref moveForce, translationInFluidFactor * objectBase.RigidGroupOwner.Integral.Mass / (time * time), out moveForce);

                            objectBase.RigidGroupOwner.WorldAccumulator.AddWorldForce(ref moveForce);
                        }
                    }
                }
            }

            if (enableSwimUp)
            {
                if (cameraDown.IsUnderFluidSurface && cameraBody.IsUnderFluidSurface)
                {
                    objectBase.RigidGroupOwner.MaxPreUpdateLinearVelocity = 10.0f;
                    objectBase.RigidGroupOwner.MaxPostUpdateLinearVelocity = 10.0f;

                    PhysicsObject fluidPhysicsObject = objectBase.RigidGroupOwner.FluidPhysicsObject;
                    fluidPhysicsObject.InternalControllers.FluidController.GetNormal(ref fluidNormal);

                    Vector3.Multiply(ref fluidNormal, swimUpSpeed * objectBase.RigidGroupOwner.Integral.Mass / time, out moveForce);

                    objectBase.RigidGroupOwner.WorldAccumulator.AddWorldForce(ref moveForce);
                }
                else
                    if (!cameraDownCollision && cameraBody.IsInFluid && (deltaTranslation.LengthSquared == 0.0f))
                    {
                        objectBase.RigidGroupOwner.MaxPreUpdateLinearVelocity = 10.0f;
                        objectBase.RigidGroupOwner.MaxPostUpdateLinearVelocity = 10.0f;

                        PhysicsObject fluidPhysicsObject = objectBase.RigidGroupOwner.FluidPhysicsObject;
                        fluidPhysicsObject.InternalControllers.FluidController.GetNormal(ref fluidNormal);

                        Vector3.Multiply(ref fluidNormal, swimUpOnSurfaceSpeed * objectBase.RigidGroupOwner.Integral.Mass / time, out moveForce);

                        objectBase.RigidGroupOwner.WorldAccumulator.AddWorldForce(ref moveForce);
                    }
            }

            if (enableJump)
            {
                if (!enableControl && !objectBase.Camera.EnableControl && cameraDownCollision && !cameraDown.IsUnderFluidSurface && !cameraBody.IsUnderFluidSurface)
                {
                    objectBase.RigidGroupOwner.MaxPreUpdateLinearVelocity = 100000.0f;
                    objectBase.RigidGroupOwner.MaxPostUpdateLinearVelocity = 100000.0f;

                    Vector3.Multiply(ref gravityDirection, -jumpSpeed * objectBase.RigidGroupOwner.Integral.Mass / time, out moveForce);

                    objectBase.RigidGroupOwner.WorldAccumulator.AddWorldForce(ref moveForce);
                    cameraDown.UpdateFeedbackForce(ref moveForce);
                }
            }

            if (enableDistance)
            {
                if (distance > maxDistance)
                    distance -= 2.0f;

                if (enableDistanceCollision)
                {
                    float margin = 1.0f;

                    objectBase.MainWorldTransform.GetPosition(ref startPoint);

                    objectBase.Camera.GetTransposeRotation(ref cameraRotation);

                    direction.X = cameraRotation.Row2.X;
                    direction.Y = cameraRotation.Row2.Y;
                    direction.Z = cameraRotation.Row2.Z;

                    Vector3.Multiply(ref direction, distance, out direction);
                    Vector3.Add(ref startPoint, ref direction, out endPoint);

                    scene.UpdatePhysicsObjectsIntersectedBySegment(ref startPoint, ref endPoint, margin, true);

                    float minDistance = float.MaxValue;
                    float curDistance = 0.0f;

                    for (int i = 0; i < scene.IntersectedPhysicsObjectsCount; i++)
                    {
                        PhysicsObject hitObject = scene.GetIntersectedPhysicsObject(i, ref hitPoint);

                        if (hitObject.RigidGroupOwner == objectBase.RigidGroupOwner)
                            continue;

                        //if ((hitObject.InternalControllers.FluidController != null) && hitObject.InternalControllers.FluidController.Enabled)
                        //    continue;

                        if (!hitObject.EnableCollisions)
                            continue;

                        Vector3.Subtract(ref startPoint, ref hitPoint, out hitDistance);
                        curDistance = hitDistance.Length;

                        if (curDistance < minDistance)
                            minDistance = curDistance;
                    }

                    if (minDistance < Math.Abs(distance))
                        distance = -minDistance;
                }
            }
            else
            {
                if (distance < 0.0f)
                    distance += 2.0f;

                if (distance > 0.0f)
                    distance = 0.0f;
            }

            if (enableDistance)
            {
                if (distance > maxDistance)
                {
                    if ((cameraUp != null) && (cameraBody != null) && (cameraDown != null))
                    {
                        objectBase.RigidGroupOwner.EnableDrawing = true;
                        cameraUp.EnableDrawing = true;
                        cameraBody.EnableDrawing = true;
                        cameraDown.EnableDrawing = true;
                    }
                }
            }
            else
            {
                if (distance >= 0.0f)
                {
                    if ((cameraUp != null) && (cameraBody != null) && (cameraDown != null))
                    {
                        objectBase.RigidGroupOwner.EnableDrawing = false;
                        cameraUp.EnableDrawing = false;
                        cameraBody.EnableDrawing = false;
                        cameraDown.EnableDrawing = false;
                    }
                }
            }

            enableControl = objectBase.Camera.EnableControl;

            float gravityDistance = 0.0f;
            Vector3 gravityLinearVelocity = vectorZero;
            Vector3 tangentLinearVelocity = vectorZero;
            Vector3 velocity = vectorZero;

            objectBase.MainWorldTransform.GetLinearVelocity(ref velocity);
            Vector3.Dot(ref gravityDirection, ref velocity, out gravityDistance);
            Vector3.Multiply(ref gravityDirection, gravityDistance, out gravityLinearVelocity);
            Vector3.Subtract(ref velocity, ref gravityLinearVelocity, out tangentLinearVelocity);

            float tangentLength = tangentLinearVelocity.Length;

            if (tangentLength > maxTangentLength)
                tangentLinearVelocity *= maxTangentLength / tangentLength;

            Vector3.Add(ref gravityLinearVelocity, ref tangentLinearVelocity, out velocity);

            objectBase.RigidGroupOwner.MainWorldTransform.SetLinearVelocity(ref velocity);

            objectBase.Camera.Projection.CreatePerspectiveLH(1.0f, 11000.0f, 70.0f, demo.WindowWidth, demo.WindowHeight);

            objectBase.MainWorldTransform.GetPosition(ref position);
            objectBase.Camera.GetTransposeRotation(ref cameraRotation);

            objectBase.Camera.View.CreateLookAtLH(ref position, ref cameraRotation, distance);
            objectBase.Camera.UpdateFrustum();

            objectBase.Camera.View.GetViewMatrix(ref view);
            objectBase.Camera.Projection.GetProjectionMatrix(ref projection);

            Vector3 rayPosition, rayDirection;

            rayPosition = rayDirection = vectorZero;

            objectBase.UnProjectToRay(ref mousePosition, 0, 0, demo.WindowWidth, demo.WindowHeight, 0.0f, 1.0f, ref view, ref matrixIdentity, ref projection, ref rayPosition, ref rayDirection);

            PhysicsObject cursor = scene.Factory.PhysicsObjectManager.Find(cursorName);

            if (cursor != null)
            {
                Vector3 cursorPosition = vectorZero;
                Matrix4 cursorLocalRotation = matrixIdentity;
                Matrix4 cursorWorldRotation = matrixIdentity;

                cursor.InitLocalTransform.GetPosition(ref cursorPosition);
                cursor.InitLocalTransform.GetRotation(ref cursorLocalRotation);
                cursor.MainWorldTransform.GetRotation(ref cursorWorldRotation);

                objectBase.Camera.GetTransposeRotation(ref cameraRotation);
                Matrix4.Mult(ref cursorLocalRotation, ref cameraRotation, out rotation);

                Vector3.TransformVector(ref cursorPosition, ref cursorWorldRotation, out position);

                cursor.MainWorldTransform.SetRotation(ref rotation);
                Vector3.Add(ref position, ref rayPosition, out position);
                Vector3.Add(ref position, ref rayDirection, out position);
                cursor.MainWorldTransform.SetPosition(ref position);

                cursor.RecalculateMainTransform();
            }

            CursorController cursorController = objectBase.InternalControllers.CursorController;

            if (cursorController.IsDragging)
            {
                if (cursorController.HitPhysicsObject.Integral.IsStatic && (cursorController.HitPhysicsObject.InternalControllers.HeightmapController != null) && cursorController.HitPhysicsObject.InternalControllers.HeightmapController.Enabled)
                {
                    Vector3 cursorStartPosition = vectorZero;
                    Vector3 cursorEndPosition = vectorZero;

                    cursorController.GetAnchor1(ref cursorStartPosition);
                    cursorController.GetAnchor2(ref cursorEndPosition);

                    Vector3.Subtract(ref cursorEndPosition, ref cursorStartPosition, out direction);

                    float dir = direction.Y;

                    if (dir != 0.0f)
                    {
                        Vector3 scale = vectorZero;

                        cursorController.HitPhysicsObject.MainWorldTransform.GetScale(ref scale);

                        float positionX = cursorStartPosition.X + 0.5f * scale.X;
                        float positionY = cursorStartPosition.Y + 0.5f * scale.Y;
                        float positionZ = cursorStartPosition.Z + 0.5f * scale.Z;

                        cursorController.HitPhysicsObject.InternalControllers.HeightmapController.AddHeight(positionX, positionY, positionZ, dir / scale.Y);
                        cursorController.HitPhysicsObject.InternalControllers.HeightmapController.UpdateBounding();

                        // To change the friction and restitution of the heightmap surface by the cursor, add the following lines of code

                        //cursorController.HitPhysicsObject.InternalControllers.HeightmapController.SetFriction(positionX, positionY, positionZ, 0.0f);
                        //cursorController.HitPhysicsObject.InternalControllers.HeightmapController.SetRestitution(positionX, positionY, positionZ, 2.0f);

                        cursorStartPosition.Y += dir;
                        cursorController.SetAnchor1(ref cursorStartPosition);
                    }
                }

                if (cursorController.HitPhysicsObject.Integral.IsStatic && (cursorController.HitPhysicsObject.InternalControllers.HeightmapController != null) && cursorController.HitPhysicsObject.InternalControllers.HeightmapController.Enabled)
                {
                    Vector3 cursorStartPosition = vectorZero;
                    Vector3 cursorEndPosition = vectorZero;

                    cursorController.GetAnchor1(ref cursorStartPosition);
                    cursorController.GetAnchor2(ref cursorEndPosition);

                    Vector3.Subtract(ref cursorEndPosition, ref cursorStartPosition, out direction);

                    if (direction.LengthSquared != 0.0f)
                    {
                        // To move the heightmap surface by the cursor, add the following lines of code

                        //cursorController.HitPhysicsObject.MainWorldTransform.GetPosition(ref cursorStartPosition);

                        //Vector3.Add(ref cursorStartPosition, ref direction, out cursorStartPosition);

                        //cursorController.HitPhysicsObject.MainWorldTransform.SetPosition(ref cursorStartPosition);
                        //cursorController.HitPhysicsObject.RecalculateMainTransform();
                    }
                }

                if (cursorController.HitPhysicsObject.Integral.IsStatic && (cursorController.HitPhysicsObject.InternalControllers.FluidController != null) && cursorController.HitPhysicsObject.InternalControllers.FluidController.Enabled)
                {
                    Vector3 cursorStartPosition = vectorZero;
                    Vector3 cursorEndPosition = vectorZero;

                    cursorController.GetAnchor1(ref cursorStartPosition);
                    cursorController.GetAnchor2(ref cursorEndPosition);

                    Vector3.Subtract(ref cursorEndPosition, ref cursorStartPosition, out direction);

                    if (direction.LengthSquared != 0.0f)
                    {
                        // To move the fluid surface by the cursor, add the following lines of code
                        // and set EnableCursorInteraction flag to true in the Lake class

                        //cursorController.HitPhysicsObject.MainWorldTransform.GetPosition(ref cursorStartPosition);

                        //Vector3.Add(ref cursorStartPosition, ref direction, out cursorStartPosition);

                        //cursorController.HitPhysicsObject.MainWorldTransform.SetPosition(ref cursorStartPosition);
                        //cursorController.HitPhysicsObject.RecalculateMainTransform();
                    }
                }
            }

            objectBase.MainWorldTransform.GetPosition(ref listenerPosition);
            objectBase.Camera.GetTransposeRotation(ref rotation);

            Vector3.Multiply(ref listenerPosition, soundPositionFactor, out position);
            listenerTopDirection.X = rotation.Row1.X;
            listenerTopDirection.Y = rotation.Row1.Y;
            listenerTopDirection.Z = rotation.Row1.Z;
            listenerFrontDirection.X = rotation.Row2.X;
            listenerFrontDirection.Y = rotation.Row2.Y;
            listenerFrontDirection.Z = rotation.Row2.Z;

            listener.Position = position;
            listener.TopDirection = listenerTopDirection;
            listener.FrontDirection = listenerFrontDirection;
            listenerRange = objectBase.Sound.Range;

            if (enableShot)
            {
                Vector3 shotScale, shotColor;

                shotCount = (shotCount + 1) % shotTab.Length;
                string shotCountName = shotCount.ToString();

                PhysicsObject shot = scene.Factory.PhysicsObjectManager.FindOrCreate(shotName + shotCountName);
                PhysicsObject shotBase = scene.Factory.PhysicsObjectManager.FindOrCreate(shotBaseName + shotCountName);
                PhysicsObject shotLight = scene.Factory.PhysicsObjectManager.FindOrCreate(shotLightName + shotCountName);

                shot.AddChildPhysicsObject(shotBase);
                shot.AddChildPhysicsObject(shotLight);

                shotTab[shotCount] = shotBase;
                enableShotTab = true;

                shotScale = shotColor = vectorZero;

                shotScale.X = shotScale.Y = shotScale.Z = 0.5f;
                Vector3.Multiply(ref rayDirection, 300.0f, out rayDirection);

                shot.InitLocalTransform.SetRotation(ref matrixIdentity);
                shot.InitLocalTransform.SetPosition(ref rayPosition);
                shot.InitLocalTransform.SetLinearVelocity(ref rayDirection);
                shot.InitLocalTransform.SetAngularVelocity(ref vectorZero);
                shot.MaxSimulationFrameCount = 200;
                shot.EnableRemovePhysicsObjectsFromManagerAfterMaxSimulationFrameCount = false;
                //shot.EnableLocalGravity = true;

                shotBase.Shape = sphere;
                shotBase.UserDataStr = sphereName;
                shotBase.InitLocalTransform.SetScale(ref shotScale);
                shotBase.Integral.SetDensity(10.0f);
                shotBase.Material.RigidGroup = true;
                shotBase.EnableBreakRigidGroup = false;
                shotBase.EnableCollisions = true;
                shotBase.CreateSound(true);

                if ((cameraUp != null) && (cameraBody != null) && (cameraDown != null))
                {
                    shotBase.DisableCollision(cameraUp, true);
                    shotBase.DisableCollision(cameraBody, true);
                    shotBase.DisableCollision(cameraDown, true);
                    shotBase.MaxDisableCollisionFrameCount = 50;
                }

                shotLight.Shape = sphere;
                shotLight.UserDataStr = sphereName;
                shotLight.CreateLight(true);
                shotLight.Light.Type = PhysicsLightType.Point;
                shotLight.Light.Range = 20.0f;

                shotColor.X = (float)Math.Max(random.NextDouble(), random.NextDouble());
                shotColor.Y = (float)Math.Max(random.NextDouble(), random.NextDouble());
                shotColor.Z = (float)Math.Max(random.NextDouble(), random.NextDouble());

                shotLight.Light.SetDiffuse(ref shotColor);
                shotLight.InitLocalTransform.SetScale(20.0f);
                shotLight.Material.RigidGroup = true;
                shotLight.Material.UserDataStr = yellowName;
                shotLight.EnableBreakRigidGroup = false;
                shotLight.EnableCollisions = false;
                shotLight.EnableCursorInteraction = false;
                shotLight.EnableAddToCameraDrawTransparentPhysicsObjects = false;

                scene.UpdateFromInitLocalTransform(shot);

                shotSoundGroup = demo.SoundGroups[hitName];
                shotSoundGroup.MaxHitRepeatTime = 0.0f;

                if (shotSound == null)
                    shotSound = shotSoundGroup.GetSound(objectBase.Sound, listener, emitter);

                shotSound.Update(time);

                Vector3.Multiply(ref rayPosition, soundPositionFactor, out shotSound.HitPosition);
                shotSound.HitVolume = 1.0f;

                shotSound.FrontDirection.X = rotation.Row2.X;
                shotSound.FrontDirection.Y = rotation.Row2.Y;
                shotSound.FrontDirection.Z = rotation.Row2.Z;

                demo.SoundQueue.EnqueueSound(shotSound);
            }
            else
            {
                PhysicsObject shotBase;
                DemoSound shotBaseSound;

                if (shotSound != null)
                {
                    shotSound.Update(time);

                    if (shotSound.Stop())
                    {
                        shotSound.SoundGroup.SetSound(shotSound);
                        shotSound = null;
                    }
                }

                if (enableShotTab)
                {
                    enableShotTab = false;

                    for (int i = 0; i < shotTab.Length; i++)
                    {
                        shotBase = shotTab[i];

                        if (shotBase != null)
                        {
                            if (shotBase.Sound.UserDataObj != null)
                            {
                                enableShotTab = true;
                                shotBaseSound = (DemoSound)shotBase.Sound.UserDataObj;
                                shotBaseSound.Update(time);

                                if (shotBaseSound.Stop())
                                {
                                    shotBaseSound.SoundGroup.SetSound(shotBaseSound);
                                    shotBase.Sound.UserDataObj = null;
                                    shotTab[i] = null;
                                }
                            }
                        }
                    }
                }
            }

            objectBase.Camera.UpdatePhysicsObjects(true, true, true);
            objectBase.Camera.SortDrawPhysicsObjects(PhysicsCameraSortOrderType.DrawPriorityShapePrimitiveType);
            objectBase.Camera.SortTransparentPhysicsObjects(PhysicsCameraSortOrderType.DrawPriorityShapePrimitiveType);
            objectBase.Camera.SortLightPhysicsObjects(PhysicsCameraSortOrderType.DrawPriorityShapePrimitiveType);

            PhysicsObject currentPhysicsObject;
            PhysicsSound currentSound;
            DemoSoundGroup soundGroup;
            DemoSound sound;

            if (demo.SoundQueue.SoundCount > 0)
                return;

            for (int i = 0; i < scene.TotalPhysicsObjectCount; i++)
            {
                if (demo.SoundQueue.SoundCount >= demo.SoundQueue.MaxSoundCount)
                    break;

                currentPhysicsObject = scene.GetPhysicsObject(i);

                currentSound = !currentPhysicsObject.EnableSoundFromRigidGroupOwner ? currentPhysicsObject.Sound : currentPhysicsObject.RigidGroupOwner.Sound;

                if (currentSound == null)
                    continue;

                if (!currentSound.Enabled)
                    continue;

                if (currentSound.UserDataStr == null)
                    soundGroup = demo.SoundGroups[defaultName];
                else
                    soundGroup = demo.SoundGroups[currentSound.UserDataStr];

                if (currentPhysicsObject.IsSleeping && (currentSound.UserDataObj == null) && !soundGroup.EnableBackground)
                    continue;

                if (currentPhysicsObject.GetSoundData(ref listenerPosition, listenerRange, soundGroup.EnableHit, soundGroup.EnableRoll, soundGroup.EnableSlide, soundGroup.EnableBackground, currentSound.BackgroundVolumeVelocityModulation, ref hitPosition, ref rollPosition, ref slidePosition, ref backgroundPosition, ref hitVolume, ref rollVolume, ref slideVolume, ref backgroundVolume))
                {
                    if (currentSound.UserDataObj == null)
                    {
                        sound = soundGroup.GetSound(currentSound, listener, emitter);
                        currentSound.UserDataObj = sound;
                    }
                    else
                    {
                        sound = (DemoSound)currentSound.UserDataObj;
                    }

                    sound.Update(time);

                    currentPhysicsObject.MainWorldTransform.GetTransposeRotation(ref rotation);

                    Vector3.Multiply(ref hitPosition, soundPositionFactor, out sound.HitPosition);
                    Vector3.Multiply(ref rollPosition, soundPositionFactor, out sound.RollPosition);
                    Vector3.Multiply(ref slidePosition, soundPositionFactor, out sound.SlidePosition);
                    Vector3.Multiply(ref backgroundPosition, soundPositionFactor, out sound.BackgroundPosition);

                    sound.HitVolume = hitVolume;
                    sound.RollVolume = rollVolume;
                    sound.SlideVolume = slideVolume;
                    sound.BackgroundVolume = backgroundVolume;

                    sound.FrontDirection.X = rotation.Row2.X;
                    sound.FrontDirection.Y = rotation.Row2.Y;
                    sound.FrontDirection.Z = rotation.Row2.Z;

                    demo.SoundQueue.EnqueueSound(sound);
                }
                else
                {
                    if (currentSound.UserDataObj != null)
                    {
                        sound = (DemoSound)currentSound.UserDataObj;
                        sound.Update(time);

                        if (sound.Stop())
                        {
                            soundGroup.SetSound(sound);
                            currentSound.UserDataObj = null;
                        }
                    }
                }
            }
        }
    }
}
