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
    public class Sky
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Sky(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public static void CreateShapes(Demo demo, PhysicsScene scene)
        {
        }

        public void Create(Vector3 objectPosition)
        {
            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");

            PhysicsObject objectRoot = scene.Factory.PhysicsObjectManager.Create("Sky" + instanceIndexName);
            objectRoot.Shape = sphere;
            objectRoot.UserDataStr = "Sky";
            objectRoot.Material.SetDiffuse(Vector3.One);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);
            objectRoot.InitLocalTransform.SetScale(5500.0f);
            objectRoot.EnableCollisions = false;
            objectRoot.EnableCursorInteraction = false;
            objectRoot.DrawPriority = 3;

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
