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
    public sealed class UserShapesScene : IDemoScene
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
        UserShapes userShapesInstance;
        TorusMesh torusMesh1;
        TorusMesh torusMesh2;
        TorusMesh torusMesh3;
        TorusMesh torusMesh4;
        ForceField1 forceField1Instance;
        ForceField2 forceField2Instance;
        Switch1 switch1Instance;
        Switch2 switch2Instance;
        Camera1 camera1Instance1;
        Lights lightInstance;

        // Declare controllers in the scene
        SkyDraw1 skyDraw1Instance1;
        CursorDraw1 cursorDraw1Instance;
        ForceField1Animation1 forceField1Animation1Instance;
        ForceField2Animation1 forceField2Animation1Instance;
        Switch1Animation1 switch1Animation1Instance;
        Switch2Animation1 switch2Animation1Instance;
        Camera1Animation1 camera1Animation1Instance1;
        Camera1Draw1 camera1Draw1Instance1;

        public UserShapesScene(Demo demo, string name, int instanceIndex, string info)
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
            userShapesInstance = new UserShapes(demo);
            torusMesh1 = new TorusMesh(demo, 1);
            torusMesh2 = new TorusMesh(demo, 2);
            torusMesh3 = new TorusMesh(demo, 3);
            torusMesh4 = new TorusMesh(demo, 4);
            forceField1Instance = new ForceField1(demo);
            forceField2Instance = new ForceField2(demo);
            switch1Instance = new Switch1(demo);
            switch2Instance = new Switch2(demo);
            camera1Instance1 = new Camera1(demo, 1);
            lightInstance = new Lights(demo);

            // Create a new controllers in the scene
            skyDraw1Instance1 = new SkyDraw1(demo, 1);
            cursorDraw1Instance = new CursorDraw1(demo);
            forceField1Animation1Instance = new ForceField1Animation1(demo, forceField1Instance);
            forceField2Animation1Instance = new ForceField2Animation1(demo, forceField2Instance);
            switch1Animation1Instance = new Switch1Animation1(demo, switch1Instance);
            switch2Animation1Instance = new Switch2Animation1(demo, switch2Instance);
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
            userShapesInstance.Initialize(scene);
            torusMesh1.Initialize(scene);
            torusMesh2.Initialize(scene);
            torusMesh3.Initialize(scene);
            torusMesh4.Initialize(scene);
            forceField1Instance.Initialize(scene);
            forceField2Instance.Initialize(scene);
            switch1Instance.Initialize(scene);
            switch2Instance.Initialize(scene);
            camera1Instance1.Initialize(scene);
            lightInstance.Initialize(scene);

            // Initialize controllers in the scene
            skyDraw1Instance1.Initialize(scene);
            cursorDraw1Instance.Initialize(scene);
            forceField1Animation1Instance.Initialize(scene);
            forceField2Animation1Instance.Initialize(scene);
            switch1Animation1Instance.Initialize(scene);
            switch2Animation1Instance.Initialize(scene);
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
            UserShapes.CreateShapes(demo, scene);
            TorusMesh.CreateShapes(demo, scene);
            ForceField1.CreateShapes(demo, scene);
            ForceField2.CreateShapes(demo, scene);
            Switch1.CreateShapes(demo, scene);
            Switch2.CreateShapes(demo, scene);
            Camera1.CreateShapes(demo, scene);
            Lights.CreateShapes(demo, scene);

            // Create physics objects for objects in the scene
            skyInstance1.Create(new Vector3(0.0f, 0.0f, 0.0f));
            quadInstance1.Create(new Vector3(0.0f, -40.0f, 20.0f), new Vector3(1000.0f, 31.0f, 1000.0f), Quaternion.Identity);
            cursorInstance.Create();
            shotInstance.Create();
            userShapesInstance.Create();
            torusMesh1.Create(new Vector3(-5.0f, -5.0f, 5.0f), Vector3.One, Quaternion.Identity, 0.0f);
            torusMesh2.Create(new Vector3(-20.0f, -5.0f, 5.0f), new Vector3(2.0f, 1.0f, 1.0f), Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(45.0f)), 0.0f);
            torusMesh3.Create(new Vector3(-5.0f, 10.0f, 5.0f), Vector3.One, Quaternion.Identity, 100000.0f);
            torusMesh4.Create(new Vector3(-5.0f, 20.0f, 5.0f), Vector3.One, Quaternion.Identity, 100000.0f);
            forceField1Instance.Create();
            forceField2Instance.Create();
            switch1Instance.Create();
            switch2Instance.Create();
            camera1Instance1.Create(new Vector3(0.0f, 5.0f, -22.0f), Quaternion.Identity, Quaternion.Identity, Quaternion.Identity, true);

            lightInstance.CreateLightPoint(0, "Glass", new Vector3(-30.0f, 0.0f, 60.0f), new Vector3(0.2f, 1.0f, 1.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(1, "Glass", new Vector3(0.0f, 7.0f, 60.0f), new Vector3(1.0f, 0.5f, 0.1f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(2, "Glass", new Vector3(30.0f, 0.0f, 60.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(3, "Glass", new Vector3(-30.0f, 0.0f, 15.0f), new Vector3(1.0f, 0.7f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(4, "Glass", new Vector3(10.0f, 7.0f, 10.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(5, "Glass", new Vector3(40.0f, 0.0f, 15.0f), new Vector3(0.3f, 0.7f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(0, "Glass", new Vector3(-30.0f, 8.0f, 0.0f), new Vector3(0.1f, 0.7f, 1.0f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(1, "Glass", new Vector3(5.0f, 8.0f, -5.0f), new Vector3(1.0f, 0.5f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(2, "Glass", new Vector3(30.0f, 8.0f, 0.0f), new Vector3(0.5f, 1.0f, 0.2f), 20.0f, 1.0f);

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
            forceField1Animation1Instance.SetControllers();
            forceField2Animation1Instance.SetControllers();
            switch1Animation1Instance.SetControllers();
            switch2Animation1Instance.SetControllers();
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
