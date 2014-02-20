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
    public sealed class StacksScene : IDemoScene
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
        Jenga jengaInstance1;
        Pyramid pyramidInstance1;
        Wall wallInstance1;
        Camera1 camera1Instance1;
        Lights lightInstance;

        // Declare controllers in the scene
        SkyDraw1 skyDraw1Instance1;
        CursorDraw1 cursorDraw1Instance;
        Camera1Animation1 camera1Animation1Instance1;
        Camera1Draw1 camera1Draw1Instance1;

        public StacksScene(Demo demo, string name, int instanceIndex, string info)
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
            jengaInstance1 = new Jenga(demo, 1);
            pyramidInstance1 = new Pyramid(demo, 1);
            wallInstance1 = new Wall(demo, 1);
            camera1Instance1 = new Camera1(demo, 1);
            lightInstance = new Lights(demo);

            // Create a new controllers in the scene
            skyDraw1Instance1 = new SkyDraw1(demo, 1);
            cursorDraw1Instance = new CursorDraw1(demo);
            camera1Animation1Instance1 = new Camera1Animation1(demo, 1);
            camera1Draw1Instance1 = new Camera1Draw1(demo, 1);
        }

        public void Create()
        {
            string sceneInstanceIndexName = name + instanceIndexName;

            if (demo.Engine.Factory.PhysicsSceneManager.Find(sceneInstanceIndexName) != null) return;

            scene = demo.Engine.Factory.PhysicsSceneManager.Create(sceneInstanceIndexName);

            // Initialize maximum number of solver iterations for the scene
            scene.MaxIterationCount = 15;

            // Initialize time of simulation for the scene
            scene.TimeOfSimulation = 1.0f / 15.0f;

            Initialize();

            // Initialize objects in the scene
            skyInstance1.Initialize(scene);
            quadInstance1.Initialize(scene);
            cursorInstance.Initialize(scene);
            shotInstance.Initialize(scene);
            jengaInstance1.Initialize(scene);
            pyramidInstance1.Initialize(scene);
            wallInstance1.Initialize(scene);
            camera1Instance1.Initialize(scene);
            lightInstance.Initialize(scene);

            // Initialize controllers in the scene
            skyDraw1Instance1.Initialize(scene);
            cursorDraw1Instance.Initialize(scene);
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
            Jenga.CreateShapes(demo, scene);
            Pyramid.CreateShapes(demo, scene);
            Wall.CreateShapes(demo, scene);
            Camera1.CreateShapes(demo, scene);
            Lights.CreateShapes(demo, scene);

            // Create physics objects for objects in the scene
            skyInstance1.Create(new Vector3(0.0f, 0.0f, 0.0f));
            quadInstance1.Create(new Vector3(0.0f, -40.0f, 20.0f), new Vector3(1000.0f, 31.0f, 1000.0f), Quaternion.Identity);
            cursorInstance.Create();
            shotInstance.Create();
            jengaInstance1.Create(new Vector3(-30.0f, -9.0f, 20.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(60.0f)), "Box", 16, 3, new Vector3(2.0f, 2.0f, 2.0f), 1.0f, true);
            pyramidInstance1.Create(new Vector3(0.0f, -9.0f, 20.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-45.0f)), "Box", 14, new Vector3(2.0f, 2.0f, 2.0f), 1.0f, true);
            wallInstance1.Create(new Vector3(30.0f, -9.0f, 20.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-100.0f)), "Box", 12, new Vector3(2.0f, 2.0f, 2.0f), 1.0f, true);
            camera1Instance1.Create(new Vector3(0.0f, 5.0f, -22.0f), Quaternion.Identity, Quaternion.Identity, Quaternion.Identity, true);

            lightInstance.CreateLightPoint(0, "Glass", new Vector3(-30.0f, 0.0f, 40.0f), new Vector3(0.2f, 1.0f, 1.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(1, "Glass", new Vector3(0.0f, 0.0f, 40.0f), new Vector3(1.0f, 0.5f, 0.1f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(2, "Glass", new Vector3(20.0f, 0.0f, 40.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(3, "Glass", new Vector3(-30.0f, 7.0f, 15.0f), new Vector3(1.0f, 0.7f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(4, "Glass", new Vector3(0.0f, 7.0f, 15.0f), new Vector3(1.0f, 1.0f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(5, "Glass", new Vector3(20.0f, 7.0f, 15.0f), new Vector3(0.7f, 0.5f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(0, "Glass", new Vector3(-20.0f, 8.0f, 15.0f), new Vector3(0.1f, 0.7f, 1.0f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(1, "Glass", new Vector3(0.0f, 8.0f, 5.0f), new Vector3(1.0f, 0.5f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(2, "Glass", new Vector3(20.0f, 8.0f, 10.0f), new Vector3(0.5f, 1.0f, 0.2f), 20.0f, 1.0f);

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
