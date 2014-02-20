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
    public class ForceField2Animation1
    {
        Demo demo;
        PhysicsScene scene;
        ForceField2 forceField;

        Vector3 vectorZero;

        public ForceField2Animation1(Demo demo, ForceField2 forceField)
        {
            this.demo = demo;
            this.forceField = forceField;

            vectorZero = Vector3.Zero;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            PhysicsObject objectBase = null;

            objectBase = scene.Factory.PhysicsObjectManager.Find("ForceField 2 Field");
            if (objectBase != null)
                objectBase.UserControllers.CollisionMethods += new CollisionMethod(ForceField);
        }

        void ForceField(CollisionMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            Vector3 axis = vectorZero;
            Vector3 objectPosition = vectorZero;
            Vector3 collisionObjectPosition = vectorZero;

            PhysicsObject collisionPhysicsObject = null;

            for (int i = 0; i < args.Collisions.Count; i++)
            {
                collisionPhysicsObject = scene.Factory.PhysicsObjectManager.Get(args.Collisions[i]);

                if (!collisionPhysicsObject.IsStatic)
                {
                    objectBase.MainWorldTransform.GetPosition(ref objectPosition);
                    collisionPhysicsObject.MainWorldTransform.GetPosition(ref collisionObjectPosition);

                    Vector3.Subtract(ref collisionObjectPosition, ref objectPosition, out axis);
                    axis.Normalize();

                    Vector3.Multiply(ref axis, -40000.0f, out axis);

                    collisionPhysicsObject.RigidGroupOwner.WorldAccumulator.AddWorldForce(ref axis);
                }
            }
        }
    }
}
