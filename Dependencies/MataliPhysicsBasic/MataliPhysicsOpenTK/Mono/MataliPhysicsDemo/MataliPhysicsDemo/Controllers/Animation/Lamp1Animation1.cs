/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Lamp1Animation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        string sphereName;
        string yellowName;
        string particleName;
        string soundName;

        Shape sphere;
        PhysicsObject bodyMain;
        PhysicsObject bodyEmitter;
        PhysicsObject bodyLight;
        Constraint constraint1;

        int frameCount;

        PhysicsObject particle;
        StringBuilder particleNameBuilder;
        string particleInstanceIndexName;
        int particleNameLength;
        int particleCount;
        int maxParticleSimulationFrameCount;
        int maxParticleFrameCount;
        int maxParticleCount;

        Random random;

        Vector3 position;

        Vector3 vectorZero;
        Matrix4 matrixIdentity;

        public Lamp1Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            sphereName = "Sphere";
            yellowName = "Yellow";
            particleName = "Lamp 1 Particle" + instanceIndexName + " ";
            soundName = "Roll";

            particleNameBuilder = new StringBuilder(particleName);
            particleNameLength = particleNameBuilder.Length;
            maxParticleSimulationFrameCount = 80;
            maxParticleFrameCount = 4;
            maxParticleCount = maxParticleSimulationFrameCount / maxParticleFrameCount;

            random = new Random(instanceIndex);

            vectorZero = Vector3.Zero;
            matrixIdentity = Matrix4.Identity;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            sphere = scene.Factory.ShapeManager.Find("Sphere");
            bodyMain = scene.Factory.PhysicsObjectManager.Find("Lamp 1 Body Main" + instanceIndexName);
            bodyEmitter = scene.Factory.PhysicsObjectManager.Find("Lamp 1 Body Emitter" + instanceIndexName);
            bodyLight = scene.Factory.PhysicsObjectManager.Find("Lamp 1 Body Light" + instanceIndexName);
            constraint1 = scene.Factory.ConstraintManager.Find("Lamp 1 Constraint 1" + instanceIndexName);

            frameCount = 0;
            particleCount = -1;

            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Find("Lamp 1 Body" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Flame);
        }

        public void Flame(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if (objectBase.IsBrokenRigidGroup || (bodyEmitter == null) || (constraint1 == null) || constraint1.IsBroken)
            {
                if ((bodyLight != null) && (bodyLight.Light != null) && bodyLight.Light.Enabled)
                    bodyLight.Light.Enabled = false;

                return;
            }

            frameCount++;

            if (frameCount > maxParticleFrameCount)
            {
                frameCount = 0;

                bodyLight.Light.Intensity = (float)random.NextDouble() * 0.2f + 0.8f;
                
                particleCount = (particleCount + 1) % maxParticleCount;
                particleNameBuilder.Remove(particleNameLength, particleNameBuilder.Length - particleNameLength);
                particleNameBuilder.Append(particleCount);
                particleInstanceIndexName = particleNameBuilder.ToString();

                particle = scene.Factory.PhysicsObjectManager.FindOrCreate(particleInstanceIndexName);

                particle.Shape = sphere;
                particle.UserDataStr = sphereName;
                particle.Material.UserDataStr = yellowName;
                particle.Material.TransparencyFactor = 0.9f;
                particle.Material.TransparencyStepFactor = -0.01f;
                particle.CreateSound(true);
                particle.Sound.Range = 50.0f;
                particle.Sound.AmplitudeFactor = 1000.0f;
                particle.Sound.MinAmplitudeFactor = 0.0f;
                particle.Sound.UserDataStr = soundName;

                bodyEmitter.MainWorldTransform.GetPosition(ref position);

                particle.InitLocalTransform.SetRotation(ref matrixIdentity);
                particle.InitLocalTransform.SetPosition(ref position);

                Vector3 velocity = vectorZero;

                scene.GetGravityDirection(ref velocity);

                Vector3.Multiply(ref velocity, -10.0f, out velocity);

                particle.InitLocalTransform.SetLinearVelocity(ref velocity);
                particle.InitLocalTransform.SetAngularVelocity(ref vectorZero);
                particle.Integral.SetDensity(0.002f);
                particle.DisableCollision(bodyMain, true);
                particle.MaxSimulationFrameCount = maxParticleSimulationFrameCount;
                particle.MaxDisableCollisionFrameCount = 10;
                particle.EnableCursorInteraction = false;
                particle.EnableLocalGravity = true;
                particle.EnableRemovePhysicsObjectsFromManagerAfterMaxSimulationFrameCount = false;
                particle.Material.TransparencySecondPass = false;

                scene.UpdateFromInitLocalTransform(particle);
            }
        }
    }
}
