/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Terrain
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        public Terrain(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void CreateShapes(Demo demo, PhysicsScene scene, float margin, bool dynamic)
        {
            Bitmap heightmapHeights = demo.Textures["DefaultHeights"].Bitmap;
            Bitmap heightmapFrictions = demo.Textures["DefaultFrictions"].Bitmap;
            Bitmap heightmapRestitutions = demo.Textures["DefaultRestitutions"].Bitmap;

            int cellCountX = heightmapHeights.Width;
            int cellCountZ = heightmapHeights.Height;

            ShapePrimitive shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Terrain" + instanceIndexName);
            shapePrimitive.CreateHeightmap(0, 0, cellCountX, cellCountZ, heightmapHeights, heightmapFrictions, heightmapRestitutions, 1.0f, 0.0f, true, dynamic);

            Shape shape = scene.Factory.ShapeManager.Create("Terrain" + instanceIndexName);
            shape.Set(shapePrimitive, Matrix4.Identity, margin);
            shape.CreateMesh(0.0f);

            if (!demo.Meshes.ContainsKey("Terrain" + instanceIndexName))
                demo.Meshes.Add("Terrain" + instanceIndexName, new DemoMesh(demo, shape, demo.Textures["Ground"], Vector2.One, true, true, false, false, true, CullFaceMode.Back, dynamic, true));
        }

        public void Create(Vector3 objectPosition, Vector3 objectScale, Quaternion objectOrientation, float density)
        {
            Shape shape = scene.Factory.ShapeManager.Find("Terrain" + instanceIndexName);

            PhysicsObject objectRoot = scene.Factory.PhysicsObjectManager.Create("Terrain" + instanceIndexName);
            objectRoot.EnableCursorInteraction = true;
            objectRoot.DrawPriority = 2;
            objectRoot.Shape = shape;
            objectRoot.UserDataStr = "Terrain" + instanceIndexName;
            objectRoot.InitLocalTransform.SetPosition(ref objectPosition);
            objectRoot.InitLocalTransform.SetScale(ref objectScale);
            objectRoot.InitLocalTransform.SetOrientation(ref objectOrientation);
            objectRoot.Integral.SetDensity(density);
            objectRoot.EnableLocalGravity = true;
            objectRoot.InternalControllers.CreateHeightmapController(true);

            scene.UpdateFromInitLocalTransform(objectRoot);
        }
    }
}
