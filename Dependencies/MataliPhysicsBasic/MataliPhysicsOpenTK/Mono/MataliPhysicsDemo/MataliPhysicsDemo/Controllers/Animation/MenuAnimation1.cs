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
    public class MenuAnimation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string defaultName;

        PhysicsObject infoDescription;
        PhysicsObject infoScreen;

        MenuData userData;

        bool enableStartInfo;
        PhysicsObject switchPhysicsObject;
        int baseSceneIndex;

        public PhysicsObject CurrentSwitch { get { return switchPhysicsObject; } }

        Vector3 vectorZero;
        Vector3 unitZ;

        public MenuAnimation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            defaultName = "Wood1";

            vectorZero = Vector3.Zero;
            unitZ = Vector3.UnitZ;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;

            enableStartInfo = true;
            switchPhysicsObject = null;
            baseSceneIndex = 0;
        }

        public void SetControllers()
        {
            infoDescription = scene.Factory.PhysicsObjectManager.Find("Info Description" + instanceIndexName);
            infoScreen = scene.Factory.PhysicsObjectManager.Find("Info Screen" + instanceIndexName);

            int sceneCount = 0;
            PhysicsObject objectBase = null;
            string switchInstanceName = null;
            string switchSliderConstraintName = null;

            enableStartInfo = true;
            switchPhysicsObject = null;

            userData.OldMouseState = demo.GetMouseState();
            userData.OldSceneIndex = -1;
            baseSceneIndex = 0;

            for (int i = 0; i < 10; i++)
            {
                switchInstanceName = "Switch " + i.ToString();
                switchSliderConstraintName = switchInstanceName + " Slider Constraint" + instanceIndexName;

                objectBase = scene.Factory.PhysicsObjectManager.Find(switchInstanceName + instanceIndexName);

                if (objectBase != null)
                {
                    userData.SwitchSliderConstraintName = switchSliderConstraintName;
                    userData.SceneIndex = sceneCount;
                    userData.SwitchIndex = i;
                    sceneCount++;

                    objectBase.UserControllers.PostTransformMethodArgs.UserDataObj = userData;
                    objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Switch);
                }
            }

            objectBase = scene.Factory.PhysicsObjectManager.Find("Switch Right" + instanceIndexName);

            if (objectBase != null)
            {
                userData.SwitchSliderConstraintName = "Switch Right Slider Constraint" + instanceIndexName;
                userData.SceneIndex = 0;
                userData.SwitchIndex = 0;

                objectBase.UserControllers.PostTransformMethodArgs.UserDataObj = userData;
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(SwitchRight);
            }

            objectBase = scene.Factory.PhysicsObjectManager.Find("Switch Left" + instanceIndexName);

            if (objectBase != null)
            {
                userData.SwitchSliderConstraintName = "Switch Left Slider Constraint" + instanceIndexName;
                userData.SceneIndex = 0;
                userData.SwitchIndex = 0;

                objectBase.UserControllers.PostTransformMethodArgs.UserDataObj = userData;
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(SwitchLeft);
            }
        }

        public void RefreshControllers()
        {
            PhysicsObject objectBase = null;
            string switchInstanceName = null;

            enableStartInfo = true;
            switchPhysicsObject = null;

            for (int i = 0; i < 10; i++)
            {
                switchInstanceName = "Switch " + i.ToString();

                objectBase = scene.Factory.PhysicsObjectManager.Find(switchInstanceName + instanceIndexName);

                if (objectBase != null)
                {
                    userData = (MenuData)objectBase.UserControllers.PostTransformMethodArgs.UserDataObj;

                    userData.OldMouseState = demo.GetMouseState();

                    objectBase.UserControllers.PostTransformMethodArgs.UserDataObj = userData;
                }
            }

            objectBase = scene.Factory.PhysicsObjectManager.Find("Switch Right" + instanceIndexName);

            if (objectBase != null)
            {
                userData = (MenuData)objectBase.UserControllers.PostTransformMethodArgs.UserDataObj;

                userData.OldMouseState = demo.GetMouseState();

                objectBase.UserControllers.PostTransformMethodArgs.UserDataObj = userData;
            }

            objectBase = scene.Factory.PhysicsObjectManager.Find("Switch Left" + instanceIndexName);

            if (objectBase != null)
            {
                userData = (MenuData)objectBase.UserControllers.PostTransformMethodArgs.UserDataObj;

                userData.OldMouseState = demo.GetMouseState();

                objectBase.UserControllers.PostTransformMethodArgs.UserDataObj = userData;
            }
        }

        void Switch(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            MenuData userData = (MenuData)args.UserDataObj;

            string switchSliderConstraintName = userData.SwitchSliderConstraintName;
            int sceneIndex = baseSceneIndex + userData.SceneIndex;
            int oldSceneIndex = userData.OldSceneIndex;
            int switchIndex = userData.SwitchIndex;

            string sceneName = null;
            string sceneScreenName = defaultName;

            if ((sceneIndex >= 0) && (sceneIndex < demo.Scenes.Count))
            {
                sceneName = demo.Scenes[sceneIndex].SceneName;

                if ((switchPhysicsObject != objectBase) || (sceneIndex != oldSceneIndex))
                {
                    if (demo.Textures.ContainsKey(sceneName))
                        sceneScreenName = sceneName;

                    objectBase.Material.UserDataStr = sceneScreenName;
                }
            }

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if (physicsObjectWithActiveCamera == null) return;

            PhysicsCamera activeCamera = physicsObjectWithActiveCamera.Camera;

            if (activeCamera == null) return;

            Vector3 switchVelocity;

            ScreenToRayController screenToRayController = physicsObjectWithActiveCamera.InternalControllers.ScreenToRayController;

            Constraint switchSliderConstraint = scene.Factory.ConstraintManager.Find(switchSliderConstraintName);

            DemoMouseState mouseState = demo.GetMouseState();
            DemoMouseState oldMouseState = userData.OldMouseState;
            int deltaX = mouseState.X - oldMouseState.X;
            int deltaY = mouseState.Y - oldMouseState.Y;

            if (switchSliderConstraint != null)
            {
                if (screenToRayController.IsHitPhysicsObject && (screenToRayController.HitPhysicsObject == objectBase))
                {
                    if (((switchPhysicsObject != objectBase) && !mouseState[MouseButton.Left] && !mouseState[MouseButton.Middle] && !mouseState[MouseButton.Right] && (Math.Abs(deltaX) + Math.Abs(deltaY) != 0)) || ((switchPhysicsObject == objectBase) && (switchSliderConstraint.ControlDistanceZ < -4.95f)) || ((switchPhysicsObject == objectBase) && (Math.Abs(deltaX) + Math.Abs(deltaY) != 0)))
                    {
                        enableStartInfo = false;

                        if (sceneName != null)
                        {
                            if (infoScreen != null)
                            {
                                if ((switchPhysicsObject != objectBase) || (sceneIndex != oldSceneIndex))
                                    infoScreen.Material.UserDataStr = sceneScreenName;

                                infoScreen.EnableDrawing = true;
                            }

                            if (infoDescription != null)
                                infoDescription.EnableDrawing = true;
                        }
                        else
                        {
                            if (infoDescription != null)
                                infoDescription.EnableDrawing = false;

                            if (infoScreen != null)
                                infoScreen.EnableDrawing = false;
                        }

                        switchPhysicsObject = objectBase;

                        Vector3.Multiply(ref unitZ, -10.0f, out switchVelocity);
                        objectBase.MainWorldTransform.SetLinearVelocity(ref switchVelocity);
                        objectBase.MainWorldTransform.SetAngularVelocity(ref vectorZero);
                    }

                    if ((sceneName != null) && (switchPhysicsObject == objectBase) && mouseState[MouseButton.Left] && !oldMouseState[MouseButton.Left] && (switchSliderConstraint.ControlDistanceZ <= -0.5f))
                        demo.SceneIndex = sceneIndex;
                }
                else
                {
                    if ((switchPhysicsObject == objectBase) && (switchSliderConstraint.ControlDistanceZ >= -0.05))
                    {
                        if (infoScreen != null)
                            infoScreen.EnableDrawing = false;

                        if (infoDescription != null)
                            infoDescription.EnableDrawing = false;

                        switchPhysicsObject = null;
                    }
                }
            }

            if ((sceneName != null) && enableStartInfo && (demo.SceneIndex == sceneIndex))
            {
                if (infoScreen != null)
                {
                    if ((switchPhysicsObject != objectBase) || (sceneIndex != oldSceneIndex))
                        infoScreen.Material.UserDataStr = sceneScreenName;

                    infoScreen.EnableDrawing = true;
                }

                if (infoDescription != null)
                    infoDescription.EnableDrawing = true;
            }

            userData.OldMouseState = mouseState;
            userData.OldSceneIndex = sceneIndex;
            args.UserDataObj = userData;
        }

        void SwitchRight(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            MenuData userData = (MenuData)args.UserDataObj;

            string switchSliderConstraintName = userData.SwitchSliderConstraintName;
            int sceneIndex = userData.SceneIndex;
            int switchIndex = userData.SwitchIndex;

            if ((baseSceneIndex + 10) < demo.Scenes.Count)
                objectBase.Material.SetAmbient(0.4f, 0.4f, 0.25f);
            else
                objectBase.Material.SetAmbient(0.4f, 0.4f, 0.4f);

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if (physicsObjectWithActiveCamera == null) return;

            PhysicsCamera activeCamera = physicsObjectWithActiveCamera.Camera;

            if (activeCamera == null) return;

            Vector3 switchVelocity;

            ScreenToRayController screenToRayController = physicsObjectWithActiveCamera.InternalControllers.ScreenToRayController;

            Constraint switchSliderConstraint = scene.Factory.ConstraintManager.Find(switchSliderConstraintName);

            string sceneName = demo.Scenes[sceneIndex].SceneName;

            DemoMouseState mouseState = demo.GetMouseState();
            DemoMouseState oldMouseState = userData.OldMouseState;
            int deltaX = mouseState.X - oldMouseState.X;
            int deltaY = mouseState.Y - oldMouseState.Y;

            if (switchSliderConstraint != null)
            {
                if (screenToRayController.IsHitPhysicsObject && (screenToRayController.HitPhysicsObject == objectBase))
                {
                    if (((switchPhysicsObject != objectBase) && !mouseState[MouseButton.Left] && !mouseState[MouseButton.Middle] && !mouseState[MouseButton.Right] && (Math.Abs(deltaX) + Math.Abs(deltaY) != 0)) || ((switchPhysicsObject == objectBase) && (switchSliderConstraint.ControlDistanceZ < -4.95f)) || ((switchPhysicsObject == objectBase) && (Math.Abs(deltaX) + Math.Abs(deltaY) != 0)))
                    {
                        enableStartInfo = false;

                        if (infoScreen != null)
                            infoScreen.EnableDrawing = false;

                        if (infoDescription != null)
                            infoDescription.EnableDrawing = false;

                        switchPhysicsObject = objectBase;

                        Vector3.Multiply(ref unitZ, -10.0f, out switchVelocity);
                        objectBase.MainWorldTransform.SetLinearVelocity(ref switchVelocity);
                        objectBase.MainWorldTransform.SetAngularVelocity(ref vectorZero);
                    }

                    if ((switchPhysicsObject == objectBase) && mouseState[MouseButton.Left] && !oldMouseState[MouseButton.Left] && (switchSliderConstraint.ControlDistanceZ <= -0.5f))
                        baseSceneIndex = Math.Min(baseSceneIndex + 1, Math.Max(demo.Scenes.Count - 10, 0));
                }
                else
                {
                    if ((switchPhysicsObject == objectBase) && (switchSliderConstraint.ControlDistanceZ >= -0.05))
                    {
                        if (infoScreen != null)
                            infoScreen.EnableDrawing = false;

                        if (infoDescription != null)
                            infoDescription.EnableDrawing = false;

                        switchPhysicsObject = null;
                    }
                }
            }

            userData.OldMouseState = mouseState;
            args.UserDataObj = userData;
        }

        void SwitchLeft(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            MenuData userData = (MenuData)args.UserDataObj;

            string switchSliderConstraintName = userData.SwitchSliderConstraintName;
            int sceneIndex = userData.SceneIndex;
            int switchIndex = userData.SwitchIndex;

            if (baseSceneIndex > 0)
                objectBase.Material.SetAmbient(0.4f, 0.4f, 0.25f);
            else
                objectBase.Material.SetAmbient(0.4f, 0.4f, 0.4f);

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if (physicsObjectWithActiveCamera == null) return;

            PhysicsCamera activeCamera = physicsObjectWithActiveCamera.Camera;

            if (activeCamera == null) return;

            Vector3 switchVelocity;

            ScreenToRayController screenToRayController = physicsObjectWithActiveCamera.InternalControllers.ScreenToRayController;

            Constraint switchSliderConstraint = scene.Factory.ConstraintManager.Find(switchSliderConstraintName);

            string sceneName = demo.Scenes[sceneIndex].SceneName;

            DemoMouseState mouseState = demo.GetMouseState();
            DemoMouseState oldMouseState = userData.OldMouseState;
            int deltaX = mouseState.X - oldMouseState.X;
            int deltaY = mouseState.Y - oldMouseState.Y;

            if (switchSliderConstraint != null)
            {
                if (screenToRayController.IsHitPhysicsObject && (screenToRayController.HitPhysicsObject == objectBase))
                {
                    if (((switchPhysicsObject != objectBase) && !mouseState[MouseButton.Left] && !mouseState[MouseButton.Middle] && !mouseState[MouseButton.Right] && (Math.Abs(deltaX) + Math.Abs(deltaY) != 0)) || ((switchPhysicsObject == objectBase) && (switchSliderConstraint.ControlDistanceZ < -4.95f)) || ((switchPhysicsObject == objectBase) && (Math.Abs(deltaX) + Math.Abs(deltaY) != 0)))
                    {
                        enableStartInfo = false;

                        if (infoScreen != null)
                            infoScreen.EnableDrawing = false;

                        if (infoDescription != null)
                            infoDescription.EnableDrawing = false;

                        switchPhysicsObject = objectBase;

                        Vector3.Multiply(ref unitZ, -10.0f, out switchVelocity);
                        objectBase.MainWorldTransform.SetLinearVelocity(ref switchVelocity);
                        objectBase.MainWorldTransform.SetAngularVelocity(ref vectorZero);
                    }

                    if ((switchPhysicsObject == objectBase) && mouseState[MouseButton.Left] && !oldMouseState[MouseButton.Left] && (switchSliderConstraint.ControlDistanceZ <= -0.5f))
                        baseSceneIndex = Math.Max(baseSceneIndex - 1, 0);
                }
                else
                {
                    if ((switchPhysicsObject == objectBase) && (switchSliderConstraint.ControlDistanceZ >= -0.05))
                    {
                        if (infoScreen != null)
                            infoScreen.EnableDrawing = false;

                        if (infoDescription != null)
                            infoDescription.EnableDrawing = false;

                        switchPhysicsObject = null;
                    }
                }
            }

            userData.OldMouseState = mouseState;
            args.UserDataObj = userData;
        }
    }
}
