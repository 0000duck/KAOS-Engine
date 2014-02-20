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
    public class Pyramid
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Pyramid(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, string shapeName, int pyramidSize, Vector3 shapeSize, float density, bool enableSleeping)
        {
            Shape shape = scene.Factory.ShapeManager.Find(shapeName);

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Pyramid" + instanceIndexName);

            int instanceIndexCount = 0;

            for (int y = 0; y < pyramidSize; y++)
                for (int x = 0; x < pyramidSize - y; x++)
                {
                    objectBase = scene.Factory.PhysicsObjectManager.Create("Pyramid " + instanceIndexCount.ToString() + instanceIndexName);
                    objectRoot.AddChildPhysicsObject(objectBase);
                    objectBase.Shape = shape;
                    objectBase.UserDataStr = shapeName;
                    objectBase.CreateSound(true);
                    objectBase.InitLocalTransform.SetPosition(x * shapeSize.X + 0.5f * shapeSize.X * y - 0.5f * shapeSize.X * pyramidSize, 0.5f * shapeSize.Y + y * shapeSize.Y, 0);
                    objectBase.InitLocalTransform.SetScale(0.5f * shapeSize.X, 0.5f * shapeSize.Y, 0.5f * shapeSize.Z);
                    objectBase.Integral.SetDensity(density);
                    objectBase.EnableSleeping = enableSleeping;
                    instanceIndexCount++;
                }

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
