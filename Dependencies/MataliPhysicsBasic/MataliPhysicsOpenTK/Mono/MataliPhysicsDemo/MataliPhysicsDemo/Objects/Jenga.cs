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
    public class Jenga
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Jenga(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, string shapeName, int jengaHeight, int jengaWidth, Vector3 shapeSize, float density, bool enableSleeping)
        {
            Shape shape = scene.Factory.ShapeManager.Find(shapeName);

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            int instanceIndexCount = 0;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Jenga " + instanceIndexName);

            for (int i = 0; i < jengaHeight; i++)
                if (i % 2 == 0)
                {
                    for (int j = 0; j < jengaWidth; j++)
                    {
                        objectBase = scene.Factory.PhysicsObjectManager.Create("Jenga " + instanceIndexCount.ToString() + instanceIndexName);
                        objectRoot.AddChildPhysicsObject(objectBase);
                        objectBase.Shape = shape;
                        objectBase.UserDataStr = shapeName;
                        objectBase.CreateSound(true);
                        objectBase.InitLocalTransform.SetPosition((j - 0.5f * jengaWidth + 0.5f) * shapeSize.X, i * shapeSize.Y + 0.5f * shapeSize.Y, 0.0f);
                        objectBase.InitLocalTransform.SetScale(0.5f * shapeSize.X, 0.5f * shapeSize.Y, 0.5f * shapeSize.Z * jengaWidth);
                        objectBase.Integral.SetDensity(density);
                        objectBase.EnableSleeping = enableSleeping;
                        instanceIndexCount++;
                    }
                }
                else
                {
                    for (int j = 0; j < jengaWidth; j++)
                    {
                        objectBase = scene.Factory.PhysicsObjectManager.Create("Jenga " + instanceIndexCount.ToString() + instanceIndexName);
                        objectRoot.AddChildPhysicsObject(objectBase);
                        objectBase.Shape = shape;
                        objectBase.UserDataStr = shapeName;
                        objectBase.CreateSound(true);
                        objectBase.InitLocalTransform.SetPosition(0.0f, i * shapeSize.Y + 0.5f * shapeSize.Y, (j - 0.5f * jengaWidth + 0.5f) * shapeSize.Z);
                        objectBase.InitLocalTransform.SetScale(0.5f * shapeSize.X * jengaWidth, 0.5f * shapeSize.Y, 0.5f * shapeSize.Z);
                        objectBase.Integral.SetDensity(density);
                        objectBase.EnableSleeping = enableSleeping;
                        instanceIndexCount++;
                    }
                }

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
