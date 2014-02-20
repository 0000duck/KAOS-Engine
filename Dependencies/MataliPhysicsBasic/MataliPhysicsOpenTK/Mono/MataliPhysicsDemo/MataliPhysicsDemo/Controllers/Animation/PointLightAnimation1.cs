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
    public class PointLightAnimation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        float intensity;
        float minIntensity;
        float maxIntensity;
        float stepIntensity;

        public PointLightAnimation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers(float minIntensity, float maxIntensity, float stepIntensity)
        {
            this.minIntensity = minIntensity;
            this.maxIntensity = maxIntensity;
            this.stepIntensity = stepIntensity;

            intensity = minIntensity;

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Light Point" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Flash);
        }

        public void Flash(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            if (objectBase.Light == null) return;

            if ((intensity >= minIntensity) && (intensity <= maxIntensity))
                intensity += stepIntensity;

            if (intensity < minIntensity)
            {
                intensity = minIntensity;
                stepIntensity = -stepIntensity;
            }

            if (intensity > maxIntensity)
            {
                intensity = maxIntensity;
                stepIntensity = -stepIntensity;
            }

            objectBase.Light.Intensity = intensity;
        }
    }
}
