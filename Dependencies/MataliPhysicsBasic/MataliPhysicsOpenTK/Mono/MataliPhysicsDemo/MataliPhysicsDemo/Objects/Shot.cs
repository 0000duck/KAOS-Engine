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
    public class Shot
    {
        Demo demo;
        PhysicsScene scene;

        public Shot(Demo demo)
        {
            this.demo = demo;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public static void CreateShapes(Demo demo, PhysicsScene scene)
        {
        }

        public void Create()
        {
            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");

            PhysicsObject objectRoot = scene.Factory.PhysicsObjectManager.Create("Shot");
            objectRoot.Shape = sphere;
            objectRoot.UserDataStr = "Sphere";
            objectRoot.InitLocalTransform.SetPosition(0.0f, -1000.0f, 0.0f);
            objectRoot.InitLocalTransform.SetScale(0.5f);
            objectRoot.EnableCollisions = false;

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
