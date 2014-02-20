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
    public sealed class TerrainWithWaterScene : IDemoScene
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
        Cursor cursorInstance;
        Shot shotInstance;
        Terrain terrainInstance1;
        Lake lakeInstance1;
        DefaultShapes defaultShapesInstance;
        Column columnInstance1;
        Column columnInstance2;
        Pier pierInstance1;
        Ragdoll2 ragdoll2Instance1;
        Boat1 boat1Instance1;
        Car1 car1Instance1;
        Box2 box2Instance1;
        Crab1 crab1Instance1;
        TorusMesh torusMesh;
        Camera2 camera2Instance1;
        Lights lightInstance;

        // Declare controllers in the scene
        SkyDraw1 skyDraw1Instance1;
        CursorDraw1 cursorDraw1Instance;
        Boat1Animation1 boat1Animation1Instance1;
        Car1Animation1 car1Animation1Instance1;
        TerrainDraw1 terrainDraw1Instance1;
        LakeDraw1 lakeDraw1Instance1;
        Camera2Animation1 camera2Animation1Instance1;
        Camera2Draw1 camera2Draw1Instance1;

        public TerrainWithWaterScene(Demo demo, string name, int instanceIndex, string info)
        {
            this.demo = demo;
            this.name = name;
            this.instanceIndexName = " " + instanceIndex.ToString();
            this.info = info;

            // Create a new objects in the scene
            skyInstance1 = new Sky(demo, 1);
            cursorInstance = new Cursor(demo);
            shotInstance = new Shot(demo);
            terrainInstance1 = new Terrain(demo, 1);
            lakeInstance1 = new Lake(demo, 1, 8, 8);
            defaultShapesInstance = new DefaultShapes(demo);
            columnInstance1 = new Column(demo, 1);
            columnInstance2 = new Column(demo, 2);
            pierInstance1 = new Pier(demo, 1);
            ragdoll2Instance1 = new Ragdoll2(demo, 1);
            boat1Instance1 = new Boat1(demo, 1);
            car1Instance1 = new Car1(demo, 1);
            box2Instance1 = new Box2(demo, 1);
            crab1Instance1 = new Crab1(demo, 1);
            torusMesh = new TorusMesh(demo, 1);
            camera2Instance1 = new Camera2(demo, 1);
            lightInstance = new Lights(demo);

            // Create a new controllers in the scene
            skyDraw1Instance1 = new SkyDraw1(demo, 1);
            cursorDraw1Instance = new CursorDraw1(demo);
            boat1Animation1Instance1 = new Boat1Animation1(demo, 1);
            car1Animation1Instance1 = new Car1Animation1(demo, 1);
            terrainDraw1Instance1 = new TerrainDraw1(demo, 1);
            lakeDraw1Instance1 = new LakeDraw1(demo, 1, 8, 8);
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
            cursorInstance.Initialize(scene);
            shotInstance.Initialize(scene);
            terrainInstance1.Initialize(scene);
            lakeInstance1.Initialize(scene);
            defaultShapesInstance.Initialize(scene);
            columnInstance1.Initialize(scene);
            columnInstance2.Initialize(scene);
            pierInstance1.Initialize(scene);
            ragdoll2Instance1.Initialize(scene);
            boat1Instance1.Initialize(scene);
            car1Instance1.Initialize(scene);
            box2Instance1.Initialize(scene);
            crab1Instance1.Initialize(scene);
            torusMesh.Initialize(scene);
            camera2Instance1.Initialize(scene);
            lightInstance.Initialize(scene);

            // Initialize controllers in the scene
            skyDraw1Instance1.Initialize(scene);
            cursorDraw1Instance.Initialize(scene);
            boat1Animation1Instance1.Initialize(scene);
            car1Animation1Instance1.Initialize(scene);
            terrainDraw1Instance1.Initialize(scene);
            lakeDraw1Instance1.Initialize(scene);
            camera2Animation1Instance1.Initialize(scene);
            camera2Draw1Instance1.Initialize(scene);

            // Create shapes shared for all physics objects in the scene
            // These shapes are used by all objects in the scene
            Demo.CreateSharedShapes(demo, scene);

            // Create shapes for objects in the scene
            Sky.CreateShapes(demo, scene);
            Cursor.CreateShapes(demo, scene);
            Shot.CreateShapes(demo, scene);
            //terrainInstance1.CreateShapes(demo, scene, 0.0f, false);
            //lakeInstance1.CreateShapes(demo, scene, 2, 2, 50.0f, 0.95f, 0.04f, 0.04f, 0.5f, 1.0f, 1.0f, 0.2f, 1.0f, 0.0f, false);
            terrainInstance1.CreateShapes(demo, scene, 0.0f, true);
            lakeInstance1.CreateShapes(demo, scene, 150, 150, 50.0f, 0.95f, 0.04f, 0.04f, 0.5f, 1.0f, 1.0f, 0.2f, 1.0f, 0.0f, true);
            DefaultShapes.CreateShapes(demo, scene);
            Column.CreateShapes(demo, scene);
            Pier.CreateShapes(demo, scene);
            Ragdoll2.CreateShapes(demo, scene);
            Boat1.CreateShapes(demo, scene);
            Car1.CreateShapes(demo, scene);
            Box2.CreateShapes(demo, scene);
            Crab1.CreateShapes(demo, scene);
            TorusMesh.CreateShapes(demo, scene);
            Camera2.CreateShapes(demo, scene);
            Lights.CreateShapes(demo, scene);

            // Create physics objects for objects in the scene
            skyInstance1.Create(new Vector3(0.0f, 0.0f, 0.0f));
            cursorInstance.Create();
            shotInstance.Create();
            terrainInstance1.Create(new Vector3(-316.0f, -128.0f, -2072.0f), new Vector3(32.0f, 256.0f, 32.0f), Quaternion.Identity, 0.0f);
            //lakeInstance1.Create(new Vector3(-316.0f, -100.0f, -2072.0f), new Vector3(2400.0f, 1.0f, 2400.0f), Quaternion.Identity, 0.0f);
            lakeInstance1.Create(new Vector3(-316.0f, -100.0f, -2072.0f), new Vector3(4.0f, 1.0f, 4.0f), Quaternion.Identity, 0.0f);
            defaultShapesInstance.Create();
            columnInstance1.Create(new Vector3(10.0f, 30.0f, 20.0f), Vector3.One, Quaternion.Identity, "Box", 4, new Vector3(4.0f, 2.0f, 8.0f), 1.0f, true, 0.1f, 0.05f);
            columnInstance2.Create(new Vector3(20.0f, 30.0f, 20.0f), Vector3.One, Quaternion.Identity, "Box", 5, new Vector3(8.0f, 8.0f, 8.0f), 1.0f, true, 0.1f, 0.05f);
            pierInstance1.Create(new Vector3(0.0f, 0.0f, 100.0f), Vector3.One, Quaternion.Identity);
            ragdoll2Instance1.Create(new Vector3(-10.0f, -50.0f, 70.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(45.0f)), false, false, 1.0f);
            boat1Instance1.Create(new Vector3(0.0f, -5.0f, 0.0f), Vector3.One, Quaternion.Identity);
            car1Instance1.Create(new Vector3(-80.0f, -70.0f, 20.0f), Vector3.One, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-115.0f)));
            box2Instance1.Create(new Vector3(-20.0f, 0.0f, 40.0f), Vector3.One, Quaternion.Identity, 4, new Vector3(1.0f, 4.0f, 0.1f), 0.02f);
            crab1Instance1.Create(new Vector3(-90.0f, -75.0f, -40.0f), new Vector3(0.5f, 0.5f, 0.5f), Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(180.0f)));
            torusMesh.Create(new Vector3(10.0f, 20.0f, 50.0f), Vector3.One, Quaternion.Identity, 100000.0f);
            camera2Instance1.Create(new Vector3(50.0f, 5.0f, 40.0f), Quaternion.Identity, Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-220.0f)), Quaternion.Identity, true);

            lightInstance.CreateLightPoint(0, "Glass", new Vector3(-120.0f, -97.0f, -140.0f), new Vector3(0.1f, 0.9f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(1, "Glass", new Vector3(-70.0f, -97.0f, -200.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(2, "Glass", new Vector3(-90.0f, -97.0f, -100.0f), new Vector3(0.0f, 0.7f, 0.8f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(3, "Glass", new Vector3(70.0f, -97.0f, -180.0f), new Vector3(1.0f, 0.4f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(4, "Glass", new Vector3(10.0f, -97.0f, -160.0f), new Vector3(0.2f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(5, "Glass", new Vector3(40.0f, -97.0f, -140.0f), new Vector3(0.4f, 0.6f, 1.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(6, "Glass", new Vector3(-40.0f, -95.0f, -190.0f), new Vector3(1.0f, 0.9f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(7, "Glass", new Vector3(-40.0f, -95.0f, -140.0f), new Vector3(0.5f, 0.7f, 0.1f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(8, "Glass", new Vector3(-40.0f, -85.0f, -90.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(9, "Glass", new Vector3(-10.0f, -95.0f, -190.0f), new Vector3(1.0f, 0.7f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(10, "Glass", new Vector3(-10.0f, -95.0f, -140.0f), new Vector3(0.3f, 0.7f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(11, "Glass", new Vector3(-10.0f, -85.0f, -90.0f), new Vector3(1.0f, 1.0f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(12, "Glass", new Vector3(-40.0f, -85.0f, -290.0f), new Vector3(0.5f, 0.7f, 0.1f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(13, "Glass", new Vector3(-40.0f, -95.0f, -240.0f), new Vector3(1.0f, 1.0f, 0.5f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(14, "Glass", new Vector3(-10.0f, -85.0f, -290.0f), new Vector3(1.0f, 0.9f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(15, "Glass", new Vector3(-10.0f, -95.0f, -240.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(16, "Glass", new Vector3(-60.0f, -69.0f, 20.0f), new Vector3(0.6f, 0.8f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(17, "Glass", new Vector3(-20.0f, -55.0f, 45.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(18, "Glass", new Vector3(10.0f, -72.0f, -20.0f), new Vector3(0.8f, 0.7f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightPoint(19, "Glass", new Vector3(25.0f, -55.0f, 25.0f), new Vector3(1.0f, 0.7f, 0.0f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(0, "Glass", new Vector3(0.0f, -65.0f, 0.0f), new Vector3(1.0f, 0.5f, 0.2f), 20.0f, 1.0f);
            lightInstance.CreateLightSpot(1, "Glass", new Vector3(-5.0f, -55.0f, 40.0f), new Vector3(0.5f, 1.0f, 0.2f), 20.0f, 1.0f);

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
                scene.Light.SetDirection(-0.4f, -0.8f, -0.4f, 0.0f);
            }
        }

        public void SetControllers()
        {
            skyDraw1Instance1.SetControllers();
            cursorDraw1Instance.SetControllers();
            boat1Animation1Instance1.SetControllers(true);
            car1Animation1Instance1.SetControllers(false);
            terrainDraw1Instance1.SetControllers();
            lakeDraw1Instance1.SetControllers();
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
