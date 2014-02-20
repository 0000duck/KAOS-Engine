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
    public class Column
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Column(Demo demo, int instanceIndex)
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

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, string shapeName, int columnSize, Vector3 shapeSize, float density, bool enableSleeping, float soundTotalVelocityFactor, float soundAmplitudeFactor)
        {
            Shape shape = scene.Factory.ShapeManager.Find(shapeName);

            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Column" + instanceIndexName);

            for (int i = 0; i < columnSize; i++)
            {
                objectBase = scene.Factory.PhysicsObjectManager.Create("Column " + i.ToString() + instanceIndexName);
                objectRoot.AddChildPhysicsObject(objectBase);
                objectBase.Shape = shape;
                objectBase.UserDataStr = shapeName;
                objectBase.CreateSound(true);
                objectBase.Sound.TotalVelocityFactor = soundTotalVelocityFactor;
                objectBase.Sound.AmplitudeFactor = soundAmplitudeFactor;
                objectBase.InitLocalTransform.SetPosition(0.0f, i * shapeSize.Y + 0.5f * shapeSize.Y, 0.0f);
                objectBase.InitLocalTransform.SetScale(0.5f * shapeSize.X, 0.5f * shapeSize.Y, 0.5f * shapeSize.Z);
                objectBase.Integral.SetDensity(density);
                objectBase.EnableSleeping = enableSleeping;
            }

            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
