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
    public class CargoJackAnimation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        Constraint constraint1;
        Constraint constraint2;
        Constraint constraint3;

        PhysicsObject body;
        PhysicsObject panel;
        PhysicsObject panelButton;
        PhysicsObject panelLight;

        bool enableRun;
        bool flipRotation;

        float distanceSpeed;
        float rotationSpeed;

        float maxDistance;
        float maxAngle;

        int frameCount;
        int maxFrameCount;
        bool flipLight;

        public CargoJackAnimation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();

            distanceSpeed = 0.02f;
            rotationSpeed = 0.2f;

            maxDistance = 9.0f;
            maxAngle = 180.0f;

            maxFrameCount = 25;
            flipLight = true;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            PhysicsObject objectBase = null;

            constraint1 = scene.Factory.ConstraintManager.Find("Cargo Jack Constraint 1" + instanceIndexName);
            constraint2 = scene.Factory.ConstraintManager.Find("Cargo Jack Constraint 2" + instanceIndexName);
            constraint3 = scene.Factory.ConstraintManager.Find("Cargo Jack Constraint 3" + instanceIndexName);
            body = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Body" + instanceIndexName);
            panel = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Panel" + instanceIndexName);
            panelButton = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Panel Button" + instanceIndexName);
            panelLight = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Panel Button Light" + instanceIndexName);

            if (constraint3 != null)
                constraint3.MaxLimitDistanceY = maxDistance;

            enableRun = false;
            flipRotation = false;

            objectBase = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Panel Button Switch" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.CollisionMethods += new CollisionMethod(Run);

            objectBase = scene.Factory.PhysicsObjectManager.Find("Cargo Jack Body Motor" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Transport);
        }

        void Run(CollisionMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if ((constraint1 == null) || (panel == null) || constraint1.IsBroken || panel.IsBrokenRigidGroup) return;

            PhysicsObject physicsObjectWithActiveCamera = scene.GetPhysicsObjectWithActiveCamera(0);

            if ((physicsObjectWithActiveCamera == null) || !physicsObjectWithActiveCamera.RigidGroupOwner.IsColliding(objectBase)) return;

            if (!enableRun)
            {
                enableRun = true;

                if ((constraint2 != null) && (constraint2.ControlDegAngleY >= 180.0f))
                    flipRotation = true;
                else
                    flipRotation = false;
            }
        }

        void Transport(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if (panelButton != null)
            {
                if ((constraint1 != null) && (constraint2 != null) && (constraint3 != null) && (body != null) && (panel != null) && !constraint1.IsBroken && !constraint2.IsBroken && !constraint3.IsBroken && !body.IsBrokenRigidGroup && !panel.IsBrokenRigidGroup)
                {
                    if (enableRun)
                    {
                        panelButton.Material.SetAmbient(0.7f, 0.4f, 0.4f);
                        panelButton.Material.SetDiffuse(1.0f, 0.4f, 0.4f);
                        if ((panelLight != null) && (panelLight.Light != null))
                        {
                            panelLight.Light.SetDiffuse(1.0f, 0.2f, 0.2f);

                            frameCount = (frameCount + 1) % maxFrameCount;

                            if (frameCount == 0)
                                flipLight = !flipLight;

                            if (flipLight)
                                panelLight.Light.Intensity = 1.0f;
                            else
                                panelLight.Light.Intensity = 0.2f;
                        }
                    }
                    else
                    {
                        panelButton.Material.SetAmbient(0.4f, 0.7f, 0.4f);
                        panelButton.Material.SetDiffuse(0.4f, 1.0f, 0.4f);
                        if ((panelLight != null) && (panelLight.Light != null))
                        {
                            panelLight.Light.SetDiffuse(0.2f, 1.0f, 0.2f);
                            panelLight.Light.Intensity = 1.0f;
                        }
                    }
                }
                else
                {
                    panelButton.Material.SetAmbient(0.4f, 0.7f, 0.4f);
                    panelButton.Material.SetDiffuse(0.4f, 1.0f, 0.4f);
                    if ((panelLight != null) && (panelLight.Light != null))
                        panelLight.Light.Enabled = false;
                }
            }

            if (!enableRun) return;

            if ((constraint2 == null) || (constraint3 == null) || (body == null) || constraint2.IsBroken || constraint3.IsBroken || body.IsBrokenRigidGroup) return;

            if (!flipRotation)
            {
                if ((constraint3.ControlDistanceY < constraint3.MaxLimitDistanceY) && (constraint2.ControlDegAngleY <= 0.0))
                    constraint3.ControlDistanceY += distanceSpeed;

                if ((constraint3.ControlDistanceY > constraint3.MinLimitDistanceY) && (constraint2.ControlDegAngleY >= maxAngle))
                    constraint3.ControlDistanceY -= distanceSpeed;

                if ((constraint2.ControlDegAngleY < maxAngle) && (constraint3.ControlDistanceY >= constraint3.MaxLimitDistanceY))
                    constraint2.ControlDegAngleY += rotationSpeed;

                if (constraint3.ControlDistanceY <= constraint3.MinLimitDistanceY)
                    enableRun = false;
            }
            else
            {
                if ((constraint3.ControlDistanceY < constraint3.MaxLimitDistanceY) && (constraint2.ControlDegAngleY >= maxAngle))
                    constraint3.ControlDistanceY += distanceSpeed;

                if ((constraint3.ControlDistanceY > constraint3.MinLimitDistanceY) && (constraint2.ControlDegAngleY <= 0.0f))
                    constraint3.ControlDistanceY -= distanceSpeed;

                if ((constraint2.ControlDegAngleY > 0.0f) && (constraint3.ControlDistanceY >= constraint3.MaxLimitDistanceY))
                    constraint2.ControlDegAngleY -= rotationSpeed;

                if (constraint3.ControlDistanceY <= constraint3.MinLimitDistanceY)
                    enableRun = false;
            }
        }
    }
}
