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
    public interface IDemoScene
    {
        string SceneName { get; }
        string SceneInfo { get; }
        PhysicsScene PhysicsScene { get; }

        void Create();
        void Initialize();
        void SetControllers();
        void Refresh(double time);
        void Remove();
        void CreateResources();
        void DisposeResources();
    }
}
