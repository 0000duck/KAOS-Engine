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
    public sealed class MenuScene : IDemoScene
    {
        Demo demo;
        PhysicsScene scene;
        string name;
        string instanceIndexName;
        string info;

        public PhysicsScene PhysicsScene { get { return scene; } }
        public string SceneName { get { return name; } }
        public string SceneInfo { get { return info; } }
        public MenuAnimation1 MenuAnimation1 { get { return menuAnimation1Instance1; } }

        // Declare objects in the scene
        Cursor cursorInstance;
        Shot shotInstance;
        Menu menuInstance1;
        Camera3 camera3Instance1;

        // Declare controllers in the scene
        CursorDraw1 cursorDraw1Instance;
        MenuAnimation1 menuAnimation1Instance1;
        MenuDraw1 menuDraw1Instance1;
        Camera3Animation1 camera3Animation1Instance1;
        Camera3Draw1 camera3Draw1Instance1;

        public MenuScene(Demo demo, string name, int instanceIndex, string info)
        {
            this.demo = demo;
            this.name = name;
            this.instanceIndexName = " " + instanceIndex.ToString();
            this.info = info;

            // Create a new objects in the scene
            cursorInstance = new Cursor(demo);
            shotInstance = new Shot(demo);
            menuInstance1 = new Menu(demo, 1);
            camera3Instance1 = new Camera3(demo, 1);

            // Create a new controllers in the scene
            cursorDraw1Instance = new CursorDraw1(demo);
            menuAnimation1Instance1 = new MenuAnimation1(demo, 1);
            menuDraw1Instance1 = new MenuDraw1(demo, 1);
            camera3Animation1Instance1 = new Camera3Animation1(demo, 1);
            camera3Draw1Instance1 = new Camera3Draw1(demo, 1);
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
            cursorInstance.Initialize(scene);
            shotInstance.Initialize(scene);
            menuInstance1.Initialize(scene);
            camera3Instance1.Initialize(scene);

            // Initialize controllers in the scene
            cursorDraw1Instance.Initialize(scene);
            menuAnimation1Instance1.Initialize(scene);
            menuDraw1Instance1.Initialize(scene);
            camera3Animation1Instance1.Initialize(scene);
            camera3Draw1Instance1.Initialize(scene);

            // Create shapes shared for all physics objects in the scene
            // These shapes are used by all objects in the scene
            Demo.CreateSharedShapes(demo, scene);

            // Create shapes for objects in the scene
            Cursor.CreateShapes(demo, scene);
            Shot.CreateShapes(demo, scene);
            Menu.CreateShapes(demo, scene);
            Camera3.CreateShapes(demo, scene);

            // Create physics objects for objects in the scene
            cursorInstance.Create();
            shotInstance.Create();
            menuInstance1.Create();
            camera3Instance1.Create(new Vector3(0.0f, 5.0f, -122.0f), Quaternion.Identity, Quaternion.Identity, Quaternion.Identity);

            // Set controllers for objects in the scene
            SetControllers();
        }

        public void Initialize()
        {
            if (scene.Light == null)
            {
                scene.CreateLight(true);
                scene.Light.Type = PhysicsLightType.Directional;
                scene.Light.SetDirection(0.0f, -0.8f, 0.4f, 0.0f);
            }
        }

        public void SetControllers()
        {
            cursorDraw1Instance.SetControllers();
            menuAnimation1Instance1.SetControllers();
            menuDraw1Instance1.SetControllers();
            camera3Animation1Instance1.SetControllers(new Vector3(0.0f, 0.0f, 20.0f), 5);
            camera3Draw1Instance1.SetControllers(false, false, false, false, false);
        }

        public void Refresh(double time)
        {
            menuAnimation1Instance1.RefreshControllers();
            camera3Animation1Instance1.RefreshControllers();

            GL.Clear(ClearBufferMask.DepthBufferBit);

            scene.Simulate(time);

            if (demo.EnableMenu)
                scene.Draw(time);
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
    }
}
