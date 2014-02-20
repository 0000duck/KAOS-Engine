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
    public struct MenuData
    {
        public string SwitchSliderConstraintName;
        public DemoMouseState OldMouseState;
        public int SceneIndex;
        public int OldSceneIndex;
        public int SwitchIndex;
    }
}
