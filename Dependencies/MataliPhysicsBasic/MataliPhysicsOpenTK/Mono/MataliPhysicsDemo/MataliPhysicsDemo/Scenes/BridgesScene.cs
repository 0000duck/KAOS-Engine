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
    public sealed class BridgesScene : IDemoScene
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
        Car1 car1Instance1;
        Bridge1 bridge1Instance1;
        Bridge1 bridge1Instance2;
        Ragdoll1 ragdoll1Instance1;
        Camera1 camera1Instance1;
        Lights lightInstance;

        // Declare controllers in the scene
        SkyDraw1 skyDraw1Instance1;
        CursorDraw1 cursorDraw1Instance;
        Car1Animation1 car1Animation1Instance1;
        Camera1Animation1 camera1Animation1Instance1;
        Camera1Draw1 camera1Draw1Instance1;

        public BridgesScene(Demo demo, string name, int instanceIndex, string info)
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
            car1Instance1 = new Car1(demo, 1);
            bridge1Instance1 = new Bridge1(demo, 1);
            bridge1Instance2 = new Bridge1(demo, 2);
            ragdoll1Instance1 = new Ragdoll1(demo, 1);
            camera1Instance1 = new Camera1(demo, 1);
            lightInstance = new Lights(demo);

            // Create a new controllers in the scene
            skyDraw1Instance1 = new SkyDraw1(demo, 1);
            cursorDraw1Instance = new CursorDraw1(demo);
            car1Animation1Instance1 = new Car1Animation1(demo, 1);
            camera1Animation1Instance1 = new Camera1Animation1(demo, 1);
            camera1Draw1Instance1 = new Camera1Draw1(demo, 1);
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
            car1Instance1.Initialize(scene);
            bridge1Instance1.Initialize(scene);
            bridge1Instance2.Initialize(scene);
            ragdoll1Instance1.Initialize(scene);
            camera1Instance1.Initialize(scene);
            lightInstance.Initialize(scene);

            // Initialize controllers in the scene
            skyDraw1Instance1.Initialize(scene);
            cursorDraw1Instance.Initialize(scene);
            car1Animation1Instance1.Initialize(scene);
            camera1Animation1Instance1.Initialize(scene);
            camera1Draw1Instance1.Initialize(scene);

            // Create shapes shared for all physics objects in the scene
            // These shapes are used by all objects in the scene
            Demo.CreateSharedShapes(demo, scene);

            // Create shapes for objects in the scene
            Sky.CreateShapes(demo, scene);
            Quad.CreateShapes(demo, scene);
            Cursor.CreateShapes(demo, scene);
            Shot.CreateShapes(demo, scene);
            Car1.CreateShapes(demo, scene);
            Bridge1.CreateShapes(demo, scene);
            Ragdoll1.CreateShapes(demo, scene);
            Camera1.CreateShapes(demo, scene);
            Lights.CreateShapes(demo, scene);

            // Create physics objects for objects in the scene
            skyInstance1.Create(new Vector3(0.0f, 0.0f, 0.0f));
            quadInstance1.Create(new Vector3(0.0f, -40.0f, 20.0f), new Vector3(1000.0f, 31.0f, 1000.0f), Quaternion.Identity);
            cursorInstance.Create();
            shotInstance.Create();
            car1Instance1.Create(new Vector3(20.0f, 60.0f, 20.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(90.0f)));
            bridge1Instance1.Create(new Vector3(0.0f, 10.0f, 0.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-5.0f)), 50, new Vector3(5.0f, 0.4f, 15.0f));
            bridge1Instance2.Create(new Vector3(0.0f, 10.0f, 100.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(5.0f)), 50, new Vector3(5.0f, 0.4f, 15.0f));
            ragdoll1Instance1.Create(new Vector3(0.0f, 30.0f, 100.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(0.0f)), false, false, 1.0f);
            camera1Instance1.Create(new Vector3(0.0f, 65.0f, -22.0f), Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-30.0f)), Quaternion.Identity, Quaternion.Identity, true);

            lightInstance.CreateLightPoint(0, "Glass", new Vector3(-50.0f, 15.0f, 100.0f), new Vector3(0.2f, 1.0f, 1.0f), 40.0f, 1.0f);
            lightInstance.CreateLightPoint(1, "Glass", new Vector3(0.0f, 15.0f, 100.0f), new Vector3(1.0f, 0.5f, 0.1f), 40.0f, 1.0f);
            lightInstance.CreateLightPoint(2, "Glass", new Vector3(50.0f, 15.0f, 105.0f), new Vector3(1.0f, 0.7f, 0.0f), 40.0f, 1.0f);
            lightInstance.CreateLightPoint(3, "Glass", new Vector3(-50.0f, 15.0f, 45.0f), new Vector3(1.0f, 0.7f, 0.5f), 40.0f, 1.0f);
            lightInstance.CreateLightPoint(4, "Glass", new Vector3(0.0f, 15.0f, 40.0f), new Vector3(1.0f, 1.0f, 0.5f), 40.0f, 1.0f);
            lightInstance.CreateLightPoint(5, "Glass", new Vector3(50.0f, 15.0f, 35.0f), new Vector3(0.3f, 0.7f, 0.5f), 40.0f, 1.0f);
            lightInstance.CreateLightSpot(0, "Glass", new Vector3(-60.0f, 50.0f, 45.0f), new Vector3(0.1f, 0.7f, 1.0f), 40.0f, 1.0f);
            lightInstance.CreateLightSpot(1, "Glass", new Vector3(10.0f, 50.0f, 105.0f), new Vector3(1.0f, 0.5f, 0.2f), 40.0f, 1.0f);
            lightInstance.CreateLightSpot(2, "Glass", new Vector3(60.0f, 50.0f, 35.0f), new Vector3(0.5f, 1.0f, 0.2f), 40.0f, 1.0f);

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
            camera1Animation1Instance1.SetControllers();
            camera1Draw1Instance1.SetControllers(false, false, false, false, false, false);
        }

        public void Refresh(double time)
        {
            camera1Animation1Instance1.RefreshControllers();
            camera1Draw1Instance1.RefreshControllers();

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
