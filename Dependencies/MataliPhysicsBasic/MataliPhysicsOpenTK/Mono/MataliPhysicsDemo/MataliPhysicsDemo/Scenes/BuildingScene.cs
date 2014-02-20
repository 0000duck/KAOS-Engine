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
    public sealed class BuildingScene : IDemoScene
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
        Plant2 plant2Instance1;
        Building1 building1Instance1;
        Car1 car1Instance1;
        Ragdoll3 ragdoll3Instance1;
        Box1 box1Instance1;
        Lamp1 lamp1Instance1;
        Lamp1 lamp1Instance2;
        Camera2 camera2Instance1;
        Lights lightInstance;

        // Declare controllers in the scene
        SkyDraw1 skyDraw1Instance1;
        CursorDraw1 cursorDraw1Instance;
        Car1Animation1 car1Animation1Instance1;
        Lamp1Animation1 lamp1Animation1Instance1;
        Lamp1Animation1 lamp1Animation1Instance2;
        Camera2Animation1 camera2Animation1Instance1;
        Camera2Draw1 camera2Draw1Instance1;

        public BuildingScene(Demo demo, string name, int instanceIndex, string info)
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
            plant2Instance1 = new Plant2(demo, 1);
            building1Instance1 = new Building1(demo, 1);
            car1Instance1 = new Car1(demo, 1);
            ragdoll3Instance1 = new Ragdoll3(demo, 1);
            box1Instance1 = new Box1(demo, 1);
            lamp1Instance1 = new Lamp1(demo, 1);
            lamp1Instance2 = new Lamp1(demo, 2);
            camera2Instance1 = new Camera2(demo, 1);
            lightInstance = new Lights(demo);

            // Create a new controllers in the scene
            skyDraw1Instance1 = new SkyDraw1(demo, 1);
            cursorDraw1Instance = new CursorDraw1(demo);
            car1Animation1Instance1 = new Car1Animation1(demo, 1);
            lamp1Animation1Instance1 = new Lamp1Animation1(demo, 1);
            lamp1Animation1Instance2 = new Lamp1Animation1(demo, 2);
            camera2Animation1Instance1 = new Camera2Animation1(demo, 1);
            camera2Draw1Instance1 = new Camera2Draw1(demo, 1);
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
            plant2Instance1.Initialize(scene);
            building1Instance1.Initialize(scene);
            car1Instance1.Initialize(scene);
            ragdoll3Instance1.Initialize(scene);
            box1Instance1.Initialize(scene);
            lamp1Instance1.Initialize(scene);
            lamp1Instance2.Initialize(scene);
            camera2Instance1.Initialize(scene);
            lightInstance.Initialize(scene);

            // Initialize controllers in the scene
            skyDraw1Instance1.Initialize(scene);
            cursorDraw1Instance.Initialize(scene);
            car1Animation1Instance1.Initialize(scene);
            lamp1Animation1Instance1.Initialize(scene);
            lamp1Animation1Instance2.Initialize(scene);
            camera2Animation1Instance1.Initialize(scene);
            camera2Draw1Instance1.Initialize(scene);

            // Create shapes shared for all physics objects in the scene
            // These shapes are used by all objects in the scene
            Demo.CreateSharedShapes(demo, scene);

            // Create shapes for objects in the scene
            Sky.CreateShapes(demo, scene);
            Quad.CreateShapes(demo, scene);
            Cursor.CreateShapes(demo, scene);
            Shot.CreateShapes(demo, scene);
            Plant2.CreateShapes(demo, scene);
            Building1.CreateShapes(demo, scene);
            Car1.CreateShapes(demo, scene);
            Ragdoll3.CreateShapes(demo, scene);
            Box1.CreateShapes(demo, scene);
            Lamp1.CreateShapes(demo, scene);
            Camera2.CreateShapes(demo, scene);
            Lights.CreateShapes(demo, scene);

            // Create physics objects for objects in the scene
            skyInstance1.Create(new Vector3(0.0f, 0.0f, 0.0f));
            quadInstance1.Create(new Vector3(0.0f, -40.0f, 20.0f), new Vector3(1000.0f, 31.0f, 1000.0f), Quaternion.Identity);
            cursorInstance.Create();
            shotInstance.Create();
            plant2Instance1.Create(new Vector3(-30.0f, -9.0f, 20.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(10.0f)));
            building1Instance1.Create(new Vector3(0.0f, 0.0f, 100.0f), Vector3.One, Quaternion.Identity);
            car1Instance1.Create(new Vector3(10.0f, 0.0f, 40.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(180.0f)));
            ragdoll3Instance1.Create(new Vector3(-10.0f, -10.0f, 0.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(0.0f)), false, false, 0.5f);
            box1Instance1.Create(new Vector3(40.0f, 0.0f, 50.0f), Vector3.One, Quaternion.Identity, 4, new Vector3(1.0f, 4.0f, 0.1f), 0.02f);
            lamp1Instance1.Create(new Vector3(-96.0f, 5.0f, 140.0f), Vector3.One, Quaternion.Identity, PhysicsScene.Factory.PhysicsObjectManager.Find("Building 1 Level 1 Wall 3 1"));
            lamp1Instance2.Create(new Vector3(-96.0f, 5.0f, 220.0f), Vector3.One, Quaternion.Identity, PhysicsScene.Factory.PhysicsObjectManager.Find("Building 1 Level 1 Wall 3 1"));
            camera2Instance1.Create(new Vector3(0.0f, 5.0f, -22.0f), Quaternion.Identity, Quaternion.Identity, Quaternion.Identity, true);

            lightInstance.CreateLightPoint(0, "Glass", new Vector3(-30.0f, -3.0f, 90.0f), new Vector3(1.0f, 0.7f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(1, "Glass", new Vector3(0.0f, -3.0f, 90.0f), new Vector3(0.3f, 0.7f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(2, "Glass", new Vector3(30.0f, -3.0f, 90.0f), new Vector3(0.3f, 0.7f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(3, "Glass", new Vector3(-20.0f, -3.0f, 20.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(4, "Glass", new Vector3(20.0f, -3.0f, 10.0f), new Vector3(1.0f, 1.0f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(5, "Glass", new Vector3(30.0f, -3.0f, 40.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(0, "Glass", new Vector3(-30.0f, -3.0f, 15.0f), new Vector3(0.1f, 0.7f, 1.0f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(1, "Glass", new Vector3(0.0f, 8.0f, 20.0f), new Vector3(1.0f, 0.5f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(2, "Glass", new Vector3(45.0f, 0.0f, 45.0f), new Vector3(0.5f, 1.0f, 0.2f), 20.0f, 1.0f);

            // Set controllers for objects in the scene
            SetControllers();
        }

        public void Initialize()
        {
            scene.UserControllers.PostDrawMethods += demo.DrawInfo;

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
            car1Animation1Instance1.SetControllers(true);
            lamp1Animation1Instance1.SetControllers();
            lamp1Animation1Instance2.SetControllers();
            camera2Animation1Instance1.SetControllers(true);
            camera2Draw1Instance1.SetControllers(false, false, false, false, false, false);
        }

        public void Refresh(double time)
        {
            camera2Animation1Instance1.RefreshControllers();
            camera2Draw1Instance1.RefreshControllers();

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
    }
}
