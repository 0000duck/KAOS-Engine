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
    public sealed class RagdollsScene : IDemoScene
    {
        Demo demo;
        PhysicsScene scene;
        string name;
        string instanceIndexName;
        string info;

        public PhysicsScene PhysicsScene { get { return scene; } }
        public string SceneName { get { return name; } }
        public string SceneInfo { get { return info; } }

        // Declare objects in the scene
        Sky skyInstance1;
        Quad quadInstance1;
        Cursor cursorInstance;
        Shot shotInstance;
        Ragdoll1 ragdoll1Instance1;
        Ragdoll2 ragdoll2Instance1;
        Ragdoll3 ragdoll3Instance1;
        Ragdoll1 ragdoll1Instance2;
        Ragdoll2 ragdoll2Instance2;
        Ragdoll3 ragdoll3Instance2;
        Ragdoll1 ragdoll1Instance3;
        Ragdoll2 ragdoll2Instance3;
        Ragdoll3 ragdoll3Instance3;
        Camera1 camera1Instance1;
        Camera1 camera1Instance2;
        Lights lightInstance;

        // Declare controllers in the scene
        SkyDraw1 skyDraw1Instance1;
        CursorDraw1 cursorDraw1Instance;
        Camera1Animation1 camera1Animation1Instance1;
        Camera1Animation1 camera1Animation1Instance2;
        Camera1Draw1 camera1Draw1Instance1;
        Camera1Draw1 camera1Draw1Instance2;

        DemoKeyboardState oldKeyboardState;

        public RagdollsScene(Demo demo, string name, int instanceIndex, string info)
        {
            this.demo = demo;
            this.name = name;
            this.instanceIndexName = " " + instanceIndex.ToString();
            this.info = info;

            // Create a new objects in the scene
            skyInstance1 = new Sky(demo, 1);
            quadInstance1 = new Quad(demo, 1);
            cursorInstance = new Cursor(demo);
            shotInstance = new Shot(demo);
            ragdoll1Instance1 = new Ragdoll1(demo, 1);
            ragdoll2Instance1 = new Ragdoll2(demo, 1);
            ragdoll3Instance1 = new Ragdoll3(demo, 1);
            ragdoll1Instance2 = new Ragdoll1(demo, 2);
            ragdoll2Instance2 = new Ragdoll2(demo, 2);
            ragdoll3Instance2 = new Ragdoll3(demo, 2);
            ragdoll1Instance3 = new Ragdoll1(demo, 3);
            ragdoll2Instance3 = new Ragdoll2(demo, 3);
            ragdoll3Instance3 = new Ragdoll3(demo, 3);
            camera1Instance1 = new Camera1(demo, 1);
            camera1Instance2 = new Camera1(demo, 2);
            lightInstance = new Lights(demo);

            // Create a new controllers in the scene
            skyDraw1Instance1 = new SkyDraw1(demo, 1);
            cursorDraw1Instance = new CursorDraw1(demo);
            camera1Animation1Instance1 = new Camera1Animation1(demo, 1);
            camera1Animation1Instance2 = new Camera1Animation1(demo, 2);
            camera1Draw1Instance1 = new Camera1Draw1(demo, 1);
            camera1Draw1Instance2 = new Camera1Draw1(demo, 2);

            oldKeyboardState = demo.GetKeyboardState();
        }

        public void Create()
        {
            string sceneInstanceIndexName = name + instanceIndexName;

            if (demo.Engine.Factory.PhysicsSceneManager.Find(sceneInstanceIndexName) != null) return;

            scene = demo.Engine.Factory.PhysicsSceneManager.Create(sceneInstanceIndexName);

            // Initialize maximum number of solver iterations for the scene
            scene.MaxIterationCount = 10;

            // Initialize time of simulation for the scene
            scene.TimeOfSimulation = 1.0f / 15.0f;

            Initialize();

            // Initialize objects in the scene
            skyInstance1.Initialize(scene);
            quadInstance1.Initialize(scene);
            cursorInstance.Initialize(scene);
            shotInstance.Initialize(scene);
            ragdoll1Instance1.Initialize(scene);
            ragdoll2Instance1.Initialize(scene);
            ragdoll3Instance1.Initialize(scene);
            ragdoll1Instance2.Initialize(scene);
            ragdoll2Instance2.Initialize(scene);
            ragdoll3Instance2.Initialize(scene);
            ragdoll1Instance3.Initialize(scene);
            ragdoll2Instance3.Initialize(scene);
            ragdoll3Instance3.Initialize(scene);
            camera1Instance1.Initialize(scene);
            camera1Instance2.Initialize(scene);
            lightInstance.Initialize(scene);

            // Initialize controllers in the scene
            skyDraw1Instance1.Initialize(scene);
            cursorDraw1Instance.Initialize(scene);
            camera1Animation1Instance1.Initialize(scene);
            camera1Animation1Instance2.Initialize(scene);
            camera1Draw1Instance1.Initialize(scene);
            camera1Draw1Instance2.Initialize(scene);

            // Create shapes shared for all physics objects in the scene
            // These shapes are used by all objects in the scene
            Demo.CreateSharedShapes(demo, scene);

            // Create shapes for objects in the scene
            Sky.CreateShapes(demo, scene);
            Quad.CreateShapes(demo, scene);
            Cursor.CreateShapes(demo, scene);
            Shot.CreateShapes(demo, scene);
            Ragdoll1.CreateShapes(demo, scene);
            Ragdoll2.CreateShapes(demo, scene);
            Ragdoll3.CreateShapes(demo, scene);
            Camera1.CreateShapes(demo, scene);
            Lights.CreateShapes(demo, scene);

            // Create physics objects for objects in the scene
            skyInstance1.Create(new Vector3(0.0f, 0.0f, 0.0f));
            quadInstance1.Create(new Vector3(0.0f, -40.0f, 20.0f), new Vector3(1000.0f, 31.0f, 1000.0f), Quaternion.Identity);
            cursorInstance.Create();
            shotInstance.Create();
            ragdoll1Instance1.Create(new Vector3(-30.0f, 0.0f, 0.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(0.0f)), false, false, 1.0f);
            ragdoll2Instance1.Create(new Vector3(0.0f, 0.0f, 0.0f), Vector3.One, Quaternion.Identity, false, true, 1.0f);
            ragdoll3Instance1.Create(new Vector3(30.0f, 0.0f, 0.0f), Vector3.One, Quaternion.Identity, true, false, 0.5f);
            ragdoll1Instance2.Create(new Vector3(-30.0f, 15.0f, 0.0f), Vector3.One, Quaternion.Identity, false, false, 1.0f);
            ragdoll2Instance2.Create(new Vector3(0.0f, 15.0f, 0.0f), Vector3.One, Quaternion.Identity, false, false, 1.0f);
            ragdoll3Instance2.Create(new Vector3(30.0f, 15.0f, 0.0f), Vector3.One, Quaternion.Identity, false, true, 0.5f);
            ragdoll1Instance3.Create(new Vector3(-30.0f, 30.0f, 0.0f), Vector3.One, Quaternion.Identity, false, false, 1.0f);
            ragdoll2Instance3.Create(new Vector3(0.0f, 30.0f, 0.0f), Vector3.One, Quaternion.Identity, false, false, 1.0f);
            ragdoll3Instance3.Create(new Vector3(30.0f, 30.0f, 0.0f), Vector3.One, Quaternion.Identity, false, true, 0.5f);
            camera1Instance1.Create(new Vector3(0.0f, 5.0f, -22.0f), Quaternion.Identity, Quaternion.Identity, Quaternion.Identity, true);
            camera1Instance2.Create(new Vector3(0.0f, 5.0f, -22.0f), Quaternion.Identity, Quaternion.Identity, Quaternion.Identity, false);

            lightInstance.CreateLightPoint(0, "Glass", new Vector3(-30.0f, -5.0f, 45.0f), new Vector3(0.2f, 1.0f, 1.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(1, "Glass", new Vector3(0.0f, -5.0f, 45.0f), new Vector3(1.0f, 0.5f, 0.1f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(2, "Glass", new Vector3(30.0f, -5.0f, 45.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(3, "Glass", new Vector3(-20.0f, -5.0f, -5.0f), new Vector3(1.0f, 0.4f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(4, "Glass", new Vector3(0.0f, -5.0f, -5.0f), new Vector3(1.0f, 1.0f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(5, "Glass", new Vector3(20.0f, -5.0f, -5.0f), new Vector3(0.3f, 0.7f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(0, "Glass", new Vector3(-30.0f, 18.0f, 15.0f), new Vector3(0.1f, 0.7f, 1.0f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(1, "Glass", new Vector3(0.0f, 18.0f, 15.0f), new Vector3(1.0f, 0.5f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(2, "Glass", new Vector3(30.0f, 18.0f, 15.0f), new Vector3(0.5f, 1.0f, 0.2f), 20.0f, 1.0f);

            // Set controllers for objects in the scene
            SetControllers();
        }

        public void Initialize()
        {
            scene.UserControllers.PostDrawMethods += demo.DrawInfo;
            scene.UserControllers.PostDrawMethods += ChangeCamera;

            if (scene.Light == null)
            {
                scene.CreateLight(true);
                scene.Light.Type = PhysicsLightType.Directional;
                scene.Light.SetDirection(-0.4f, -0.8f, 0.4f, 0.0f);
            }
        }

        public void SetControllers()
        {
            skyDraw1Instance1.SetControllers();
            cursorDraw1Instance.SetControllers();
            camera1Animation1Instance1.SetControllers();
            camera1Animation1Instance2.SetControllers();
            camera1Draw1Instance1.SetControllers(false, false, false, false, false, false);
            camera1Draw1Instance2.SetControllers(false, false, false, false, false, false);
        }

        public void Refresh(double time)
        {
            camera1Animation1Instance1.RefreshControllers();
            camera1Animation1Instance2.RefreshControllers();
            camera1Draw1Instance1.RefreshControllers();
            camera1Draw1Instance2.RefreshControllers();

            GL.Clear(ClearBufferMask.DepthBufferBit);

            scene.Simulate(time);
            scene.Draw(time);

            if (demo.EnableMenu)
            {
                GL.Clear(ClearBufferMask.DepthBufferBit);
                demo.MenuScene.PhysicsScene.Draw(time);
            }

            demo.SwapBuffers();
        }

        public void Remove()
        {
            string sceneInstanceIndexName = name + instanceIndexName;

            if (demo.Engine.Factory.PhysicsSceneManager.Find(sceneInstanceIndexName) != null)
                demo.Engine.Factory.PhysicsSceneManager.Remove(sceneInstanceIndexName);
        }

        public void CreateResources()
        {
        }

        public void DisposeResources()
        {
        }

        void ChangeCamera(DrawMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);

            DemoKeyboardState keyboardState = demo.GetKeyboardState();

            if (keyboardState[Key.Tab] && !oldKeyboardState[Key.Tab])
            {
                PhysicsObject activeCameraObject = null;
                int activeCameraInstanceIndex = 0;

                for (int i = 0; i < scene.PhysicsObjectWithCameraCount; i++)
                {
                    PhysicsObject cameraObject = scene.GetPhysicsObjectWithCamera(i);

                    if (cameraObject.Camera.Active)
                    {
                        activeCameraObject = cameraObject;
                        activeCameraInstanceIndex = i;
                        cameraObject.Camera.Active = false;
                    }
                }

                int nextActiveCameraInstanceIndex = (activeCameraInstanceIndex + 1) % scene.PhysicsObjectWithCameraCount;
                PhysicsObject nextCameraObject = scene.GetPhysicsObjectWithCamera(nextActiveCameraInstanceIndex);
                nextCameraObject.Camera.Active = true;
            }

            oldKeyboardState = keyboardState;
        }
    }
}
