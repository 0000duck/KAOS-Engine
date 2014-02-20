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
    public class Quad
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Quad(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation)
        {
            Shape box = scene.Factory.ShapeManager.Find("Box");

            PhysicsObject objectRoot = scene.Factory.PhysicsObjectManager.Create("Quad " + instanceIndexName);
            objectRoot.Shape = box;
            objectRoot.UserDataStr = "Box";
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.EnableCursorInteraction = false;
            objectRoot.DrawPriority = 1;

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
