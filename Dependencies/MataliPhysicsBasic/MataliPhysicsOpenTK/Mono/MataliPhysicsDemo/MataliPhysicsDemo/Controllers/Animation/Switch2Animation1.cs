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
    public class Switch2Animation1
    {
        Demo demo;
        PhysicsScene scene;
        Switch2 switchObject;

        string switchShapeName;

        public Switch2Animation1(Demo demo, Switch2 switchObject)
        {
            this.demo = demo;
            this.switchObject = switchObject;

            switchShapeName = "Switch 2 Shape";
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            PhysicsObject objectBase = null;

            objectBase = scene.Factory.PhysicsObjectManager.Find("Switch 2 Switch");
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Switch);
        }

        void Switch(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            objectBase.UpdateCollidingPhysicsObjects(false);

            PhysicsObject switchShape = objectBase.RigidGroupOwner.FindChildPhysicsObject(switchShapeName, true, false);

            if (switchShape != null)
            {
                if (objectBase.CollidingPhysicsObjectCount != 0)
                    switchShape.Material.TransparencyFactor = 0.5f;
                else
                    switchShape.Material.TransparencyFactor = 1.0f;
            }
        }
    }
}
