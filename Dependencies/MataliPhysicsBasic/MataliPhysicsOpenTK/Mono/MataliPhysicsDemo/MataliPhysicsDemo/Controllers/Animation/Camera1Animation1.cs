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
    public class Camera1Animation1
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

        PhysicsObject[] shotTab;
        bool enableShotTab;
        int shotCount;

        DemoMouseState oldMouseState;
        DemoKeyboardState oldKeyboardState;

        Vector3 position;
        Vector3 direction;
        Matrix4 rotation;
        Matrix4 cameraRotation;
        Matrix4 projection;
        Matrix4 view;

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

        Vector2 mousePosition;

        Random random;

        DemoListener listener;
        DemoEmitter emitter;
        DemoSoundGroup shotSoundGroup;
        DemoSound shotSound;

        Vector3 vectorZero;
        Matrix4 matrixIdentity;
        Quaternion quaternionIdentity;

        public Camera1Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            sphereName = "Sphere";
            cursorName = "Cursor";
            cursorAName = "Cursor A";
            cursorBName = "Cursor B";
            shotName = "Camera 1 Shot" + instanceIndexName + " ";
            shotBaseName = "Camera 1 Shot Object" + instanceIndexName + " ";
            shotLightName = "Camera 1 Shot Light" + instanceIndexName + " ";
            yellowName = "Yellow";
            hitName = "Hit";
            defaultName = "Default";

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

        public void SetControllers()
        {
            sphere = scene.Factory.ShapeManager.Find("Sphere");

            oldMouseState = demo.GetMouseState();
            oldKeyboardState = demo.GetKeyboardState();

            shotCount = -1;
            enableShotTab = false;
            for (int i = 0; i < shotTab.Length; i++)
                shotTab[i] = null;

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Camera 1" + instanceIndexName);
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
            float soundPositionFactor = 0.1f;
            bool enableShot = false;

            DemoMouseState mouseState = demo.GetMouseState();
            DemoKeyboardState keyboardState = demo.GetKeyboardState();

            if (mouseState[MouseButton.Right])
            {
                deltaRotation.Y += MathHelper.DegreesToRadians(rotationSpeed * (mouseState.X - oldMouseState.X) * time);
                deltaRotation.X += MathHelper.DegreesToRadians(rotationSpeed * (mouseState.Y - oldMouseState.Y) * time);
            }

            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            if (mouseState[MouseButton.Middle] && !oldMouseState[MouseButton.Middle])
                enableShot = true;

            if ((keyboardState[Key.ControlRight] && !oldKeyboardState[Key.ControlRight]) ||
               (keyboardState[Key.ControlLeft] && !oldKeyboardState[Key.ControlLeft]))
                enableShot = true;

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

            if (keyboardState[Key.W])
                deltaTranslation.Z += translationSpeed * time;

            if (keyboardState[Key.S])
                deltaTranslation.Z -= translationSpeed * time;

            if (keyboardState[Key.D])
                deltaTranslation.X += translationSpeed * time;

            if (keyboardState[Key.A])
                deltaTranslation.X -= translationSpeed * time;

            oldMouseState = mouseState;
            oldKeyboardState = keyboardState;

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

                objectBase.MainWorldTransform.SetTransposeRotation(ref cameraRotation);
                objectBase.RecalculateMainTransform();
            }

            if (deltaTranslation.LengthSquared != 0.0f)
            {
                objectBase.MainWorldTransform.GetRotation(ref rotation);
                Vector3.TransformVector(ref deltaTranslation, ref rotation, out direction);

                objectBase.MainWorldTransform.GetPosition(ref position);
                Vector3.Add(ref position, ref direction, out position);
                objectBase.MainWorldTransform.SetPosition(ref position);

                objectBase.RecalculateMainTransform();
            }

            objectBase.Camera.Projection.CreatePerspectiveLH(1.0f, 11000.0f, 70.0f, demo.WindowWidth, demo.WindowHeight);

            objectBase.MainWorldTransform.GetPosition(ref position);
            objectBase.Camera.GetTransposeRotation(ref cameraRotation);

            objectBase.Camera.View.CreateLookAtLH(ref position, ref cameraRotation, 0.0f);
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
                if (cursorController.HitPhysicsObject.Integral.IsStatic && (cursorController.HitPhysicsObject.Shape != null) && (cursorController.HitPhysicsObject.Shape.ShapePrimitive != null) && (cursorController.HitPhysicsObject.Shape.ShapePrimitive.ShapePrimitiveType == ShapePrimitiveType.TriangleMesh))
                {
                    Vector3 cursorStartPosition = vectorZero;
                    Vector3 cursorEndPosition = vectorZero;

                    cursorController.GetAnchor1(ref cursorStartPosition);
                    cursorController.GetAnchor2(ref cursorEndPosition);

                    Vector3.Subtract(ref cursorEndPosition, ref cursorStartPosition, out direction);

                    if (direction.LengthSquared != 0.0f)
                    {
                        cursorController.HitPhysicsObject.MainWorldTransform.GetPosition(ref cursorStartPosition);

                        Vector3.Add(ref cursorStartPosition, ref direction, out cursorStartPosition);

                        cursorController.HitPhysicsObject.MainWorldTransform.SetPosition(ref cursorStartPosition);
                        cursorController.HitPhysicsObject.RecalculateMainTransform();
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
                shotBase.DisableCollision(objectBase, true);
                shotBase.MaxDisableCollisionFrameCount = 50;
                shotBase.CreateSound(true);

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
                shotLight.Material.UserDataStr = yellowName;
                shotLight.Material.RigidGroup = true;
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
