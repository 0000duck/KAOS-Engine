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
    public class Lights
    {
        Demo demo;
        PhysicsScene scene;

        public Lights(Demo demo)
        {
            this.demo = demo;
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public static void CreateShapes(Demo demo, PhysicsScene scene)
        {
        }

        public void CreateLightPoint(int index, string soundGroup, Vector3 lightPosition, Vector3 lightColor, float lightRange, float minBreakRigidGroupMultiplier)
        {
            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            PhysicsObject objectLight = null;

            Shape sphere = scene.Factory.ShapeManager.Find("Sphere");

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Light Point Root " + index.ToString());
            objectBase = scene.Factory.PhysicsObjectManager.Create("Light Point Base " + index.ToString());
            objectLight = scene.Factory.PhysicsObjectManager.Create("Light Point " + index.ToString());

            objectRoot.AddChildPhysicsObject(objectBase);
            objectRoot.AddChildPhysicsObject(objectLight);
            objectRoot.InitLocalTransform.SetPosition(ref lightPosition);
            objectRoot.EnableLocalGravity = true;

            objectBase.Shape = sphere;
            objectBase.UserDataStr = "Sphere";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.TwoSidedNormals = true;
            objectBase.Material.MinBreakRigidGroupVelocityMultiplier = minBreakRigidGroupMultiplier;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetScale(0.5f);
            objectBase.Integral.SetDensity(10.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = soundGroup;

            objectLight.Shape = sphere;
            objectLight.UserDataStr = "Sphere";
            objectLight.Material.RigidGroup = true;
            objectLight.Material.UserDataStr = "Yellow";

            objectLight.CreateLight(true);
            objectLight.Light.Type = PhysicsLightType.Point;
            objectLight.Light.SetDiffuse(ref lightColor);
            objectLight.Light.Range = lightRange * 0.92f;

            objectLight.InitLocalTransform.SetScale(lightRange);

            objectLight.EnableCollisions = false;
            objectLight.EnableCursorInteraction = false;
            objectLight.EnableBreakRigidGroup = false;
            objectLight.EnableAddToCameraDrawTransparentPhysicsObjects = false;

            scene.UpdateFromInitLocalTransform(objectRoot);
        }

        public void CreateLightSpot(int index, string soundGroup, Vector3 lightPosition, Vector3 lightColor, float lightRange, float minBreakRigidGroupMultiplier)
        {
            PhysicsObject objectRoot = null;
            PhysicsObject objectBase = null;
            PhysicsObject objectLight = null;

            Shape coneZ = scene.Factory.ShapeManager.Find("ConeZ");

            objectRoot = scene.Factory.PhysicsObjectManager.Create("Light Spot Root " + index.ToString());
            objectBase = scene.Factory.PhysicsObjectManager.Create("Light Spot Base " + index.ToString());
            objectLight = scene.Factory.PhysicsObjectManager.Create("Light Spot " + index.ToString());

            objectRoot.AddChildPhysicsObject(objectBase);
            objectRoot.AddChildPhysicsObject(objectLight);
            objectRoot.InitLocalTransform.SetPosition(ref lightPosition);
            objectRoot.EnableLocalGravity = true;
            objectRoot.InitLocalTransform.SetOrientation(Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(90.0f)));

            objectBase.Shape = coneZ;
            objectBase.UserDataStr = "ConeZ";
            objectBase.Material.RigidGroup = true;
            objectBase.Material.TwoSidedNormals = true;
            objectBase.Material.MinBreakRigidGroupVelocityMultiplier = minBreakRigidGroupMultiplier;
            objectBase.Material.UserDataStr = "Yellow";
            objectBase.InitLocalTransform.SetScale(0.5f);
            objectBase.Integral.SetDensity(10.0f);
            objectBase.EnableBreakRigidGroup = false;
            objectBase.CreateSound(true);
            objectBase.Sound.UserDataStr = soundGroup;

            objectLight.Shape = coneZ;
            objectLight.UserDataStr = "ConeZ";
            objectLight.Material.RigidGroup = true;
            objectLight.Material.UserDataStr = "Yellow";

            objectLight.CreateLight(true);
            objectLight.Light.Type = PhysicsLightType.Spot;
            objectLight.Light.SetDiffuse(ref lightColor);
            objectLight.Light.SpotInnerRadAngle = (float)Math.Atan(0.48);
            objectLight.Light.SpotOuterRadAngle = (float)Math.Atan(0.48);
            objectLight.Light.Range = 2.0f * lightRange;

            objectLight.InitLocalTransform.SetScale(lightRange);
            objectLight.InitLocalTransform.SetPosition(0.0f, 0.0f, -lightRange + 0.5f);

            objectLight.EnableCollisions = false;
            objectLight.EnableCursorInteraction = false;
            objectLight.EnableBreakRigidGroup = false;
            objectLight.EnableAddToCameraDrawTransparentPhysicsObjects = false;

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
