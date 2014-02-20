/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Demo : GameWindow
    {
        DemoMouseState mouseState;
        DemoKeyboardState keyboardState;

        string infoDrawFPSName;
        string infoPhysicsFPSName;
        string infoPhysicsObjectsName;
        string infoConstraintsName;
        string infoContactPointsName;
        string infoMenuName;

        Color whiteColor;
        public Color4 ClearLightColor;
        public Vector3 ClearScreenColor;

        StringBuilder infoDrawFPSNameBuilder;
        StringBuilder infoPhysicsFPSNameBuilder;
        StringBuilder infoPhysicsObjectsNameBuilder;
        StringBuilder infoConstraintsNameBuilder;
        StringBuilder infoContactPointsNameBuilder;
        int infoDrawFPSNameLength;
        int infoPhysicsFPSNameLength;
        int infoPhysicsObjectsNameLength;
        int infoConstraintsNameLength;
        int infoContactPointsNameLength;

        public DemoFont DemoFont;
        public DemoFont3D DemoFont3D;

        public int SceneFrameBuffer;
        public int SceneDepthBuffer;
        public int LightFrameBuffer;
        public int ScreenFrameBuffer;

        public int ColorTexture;
        public int SpecularTexture;
        public int NormalTexture;
        public int DepthTexture;
        public int LightTexture;
        public int ScreenTexture;

        public int WindowWidth;
        public int WindowHeight;

        public bool EnableVertexBuffer;
        public bool EnableMipmapExtension;
        public bool EnableCubeMapExtension;
        public bool EnableWireframe;

        public int SceneIndex;

        // Declare physics engine
        public PhysicsEngine Engine;

        // Declare scene object
        public IDemoScene Scene;

        // Declare menu object
        public MenuScene MenuScene;

        // Declare list of scene objects
        public List<IDemoScene> Scenes;

        // Declare dictionary of meshes
        public Dictionary<string, DemoMesh> Meshes;

        // Declare dictionary of textures
        public Dictionary<string, DemoTexture> Textures;

        // Declare dictionary of menu descriptions
        public Dictionary<string, List<string>> Descriptions;

        // Declare dictionary of sound samples
        public Dictionary<string, DemoSoundSample> SoundSamples;

        // Declare dictionary of sound groups
        public Dictionary<string, DemoSoundGroup> SoundGroups;

        // Declare queue of sounds
        public DemoSoundQueue SoundQueue;

        AudioContext Audio;

        public bool EnableMenu;
        public Vector3 CursorLightDirection;

        DemoKeyboardState oldKeyboardState;

        public Demo()
            : base(1280, 720, GraphicsMode.Default, "Matali Physics Demo (OpenTK)")
        {
            SetWindowSize(1280, 720);

            Icon = Properties.Resources.MataliPhysicsDemo;

            Mouse.ButtonUp += Mouse_ButtonUp;
            Mouse.ButtonDown += Mouse_ButtonDown;
            Mouse.Move += Mouse_Move;
            Mouse.WheelChanged += Mouse_WheelChanged;
            Keyboard.KeyUp += Keyboard_KeyUp;
            Keyboard.KeyDown += Keyboard_KeyDown;

            infoDrawFPSName = "Draw FPS: ";
            infoPhysicsFPSName = "Physics FPS: ";
            infoPhysicsObjectsName = "Physics objects: ";
            infoConstraintsName = "Constraints: ";
            infoContactPointsName = "Contact points: ";
            infoMenuName = "M: Menu";

            whiteColor = Color.White;
            ClearLightColor = new Color4(0, 0, 0, 0);
            ClearScreenColor = new Vector3(Color.CornflowerBlue.R / 255.0f, Color.CornflowerBlue.G / 255.0f, Color.CornflowerBlue.B / 255.0f);

            infoDrawFPSNameBuilder = new StringBuilder(infoDrawFPSName);
            infoPhysicsFPSNameBuilder = new StringBuilder(infoPhysicsFPSName);
            infoPhysicsObjectsNameBuilder = new StringBuilder(infoPhysicsObjectsName);
            infoConstraintsNameBuilder = new StringBuilder(infoConstraintsName);
            infoContactPointsNameBuilder = new StringBuilder(infoContactPointsName);
            infoDrawFPSNameLength = infoDrawFPSNameBuilder.Length;
            infoPhysicsFPSNameLength = infoPhysicsFPSNameBuilder.Length;
            infoPhysicsObjectsNameLength = infoPhysicsObjectsNameBuilder.Length;
            infoConstraintsNameLength = infoConstraintsNameBuilder.Length;
            infoContactPointsNameLength = infoContactPointsNameBuilder.Length;

            DemoFont = null;
            DemoFont3D = null;

            WindowWidth = 1280;
            WindowHeight = 720;

            EnableVertexBuffer = false;
            EnableMipmapExtension = false;
            EnableCubeMapExtension = false;

            // Create a new physics engine
            Engine = new PhysicsEngine("Engine");

            Scene = null;

            // Create a new menu object
            MenuScene = new MenuScene(this, "Menu", 1, null);

            // Create a new list of scene objects
            Scenes = new List<IDemoScene>();

            // Create a new dictionary of meshes
            Meshes = new Dictionary<string, DemoMesh>();

            // Create a new dictionary of textures
            Textures = new Dictionary<string, DemoTexture>();

            // Create a new dictionary of menu descriptions
            Descriptions = new Dictionary<string, List<string>>();

            // Create a new dictionary of sound samples
            SoundSamples = new Dictionary<string, DemoSoundSample>();

            // Create a new dictionary of sound groups
            SoundGroups = new Dictionary<string, DemoSoundGroup>();

            // Create a new queue of sounds
            SoundQueue = new DemoSoundQueue(50);

            try
            {
                Audio = new AudioContext();
            }
            catch (System.TypeInitializationException e)
            {
                
            }
            SoundQueue.CreateSources(100);

            EnableMenu = false;
            CursorLightDirection = new Vector3(0.0f, 0.0f, 1.0f);

            oldKeyboardState = GetKeyboardState();
        }

        void Mouse_ButtonUp(object sender, MouseButtonEventArgs e)
        {
            mouseState.Set(e.Button, false);
        }

        void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseState.Set(e.Button, true);
        }

        void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            mouseState.Set(e.X, e.Y);
        }

        void Mouse_WheelChanged(object sender, MouseWheelEventArgs e)
        {
            mouseState.Set(e.Value);
        }

        void Keyboard_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            keyboardState.Set(e.Key, false);
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keyboardState.Set(e.Key, true);
        }

        public DemoMouseState GetMouseState()
        {
            return mouseState;
        }

        public DemoKeyboardState GetKeyboardState()
        {
            return keyboardState;
        }

        public void SetWindowSize(int width, int height)
        {
            if ((Width != width) || (Height != height))
            {
                Width = width;
                Height = height;

                Context.Update(WindowInfo);

                GL.Viewport(0, 0, Width, Height);
            }
        }

        public void CreateResources()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out ColorTexture);
            GL.BindTexture(TextureTarget.Texture2D, ColorTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.GenTextures(1, out SpecularTexture);
            GL.BindTexture(TextureTarget.Texture2D, SpecularTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.GenTextures(1, out NormalTexture);
            GL.BindTexture(TextureTarget.Texture2D, NormalTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.GenTextures(1, out DepthTexture);
            GL.BindTexture(TextureTarget.Texture2D, DepthTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.GenTextures(1, out LightTexture);
            GL.BindTexture(TextureTarget.Texture2D, LightTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.GenTextures(1, out ScreenTexture);
            GL.BindTexture(TextureTarget.Texture2D, ScreenTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.GenFramebuffers(1, out SceneFrameBuffer);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, SceneFrameBuffer);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, ColorTexture, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment1, TextureTarget.Texture2D, SpecularTexture, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment2, TextureTarget.Texture2D, NormalTexture, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment3, TextureTarget.Texture2D, DepthTexture, 0);

            GL.GenRenderbuffers(1, out SceneDepthBuffer);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, SceneDepthBuffer);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent24, Width, Height);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, SceneDepthBuffer);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            GL.GenFramebuffers(1, out LightFrameBuffer);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, LightFrameBuffer);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, LightTexture, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            GL.GenFramebuffers(1, out ScreenFrameBuffer);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ScreenFrameBuffer);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, ScreenTexture, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void DisposeResources()
        {
            if (ColorTexture != 0)
            {
                GL.DeleteTextures(1, ref ColorTexture);
                ColorTexture = 0;
            }

            if (SpecularTexture != 0)
            {
                GL.DeleteTextures(1, ref SpecularTexture);
                SpecularTexture = 0;
            }

            if (NormalTexture != 0)
            {
                GL.DeleteTextures(1, ref NormalTexture);
                NormalTexture = 0;
            }

            if (DepthTexture != 0)
            {
                GL.DeleteTextures(1, ref DepthTexture);
                DepthTexture = 0;
            }

            if (LightTexture != 0)
            {
                GL.DeleteTextures(1, ref LightTexture);
                LightTexture = 0;
            }

            if (ScreenTexture != 0)
            {
                GL.DeleteTextures(1, ref ScreenTexture);
                ScreenTexture = 0;
            }

            if (SceneDepthBuffer != 0)
            {
                GL.DeleteRenderbuffers(1, ref SceneDepthBuffer);
                SceneDepthBuffer = 0;
            }

            if (SceneFrameBuffer != 0)
            {
                GL.DeleteFramebuffers(1, ref SceneFrameBuffer);
                SceneFrameBuffer = 0;
            }

            if (LightFrameBuffer != 0)
            {
                GL.DeleteFramebuffers(1, ref LightFrameBuffer);
                LightFrameBuffer = 0;
            }

            if (ScreenFrameBuffer != 0)
            {
                GL.DeleteFramebuffers(1, ref ScreenFrameBuffer);
                ScreenFrameBuffer = 0;
            }
        }

        /// <summary>
        /// Called when the user resizes the window.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            Context.Update(WindowInfo);

            if (Width > 0)
                WindowWidth = Width;
            if (Height > 0)
                WindowHeight = Height;

            GL.Viewport(0, 0, Width, Height);

            DemoFont.Resize();

            DisposeResources();

            for (int i = 0; i < Scenes.Count; i++)
                Scenes[i].DisposeResources();

            MenuScene.DisposeResources();

            CreateResources();

            for (int i = 0; i < Scenes.Count; i++)
                Scenes[i].CreateResources();

            MenuScene.CreateResources();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            string version = GL.GetString(StringName.Version);
            int versionNumber = int.Parse(version[0].ToString()) * 10 + int.Parse(version[2].ToString());
            if (versionNumber < 14)
            {
                Console.WriteLine("OpenGL version: " + version + "\nYou need at least OpenGL 1.4 to run this program.");
                this.Exit();
                return;
            }

            if (versionNumber >= 15)
                EnableVertexBuffer = true;

            string extensions = GL.GetString(StringName.Extensions);

            if (extensions.Contains("GL_SGIS_generate_mipmap"))
                EnableMipmapExtension = true;

            if (extensions.Contains("GL_EXT_texture_cube_map"))
                EnableCubeMapExtension = true;

            CreateResources();

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.ShadeModel(ShadingModel.Smooth);
            GL.Disable(EnableCap.Dither);
            GL.Disable(EnableCap.Lighting);

            string soundPath = "Sounds";
            SoundSamples.Add("Field", new DemoSoundSample(this, soundPath, ".wav"));
            SoundSamples.Add("Footsteps", new DemoSoundSample(this, soundPath, ".wav"));
            SoundSamples.Add("Hit1", new DemoSoundSample(this, soundPath, ".wav"));
            SoundSamples.Add("Hit2", new DemoSoundSample(this, soundPath, ".wav"));
            SoundSamples.Add("Roll", new DemoSoundSample(this, soundPath, ".wav"));
            SoundSamples.Add("Slide", new DemoSoundSample(this, soundPath, ".wav"));

            SoundGroups.Add("Default", new DemoSoundGroup(this, 4, SoundSamples["Hit1"], SoundSamples["Roll"], SoundSamples["Slide"], null));
            SoundGroups.Add("Field", new DemoSoundGroup(this, 4, SoundSamples["Hit2"], SoundSamples["Roll"], SoundSamples["Slide"], SoundSamples["Field"]));
            SoundGroups.Add("Footsteps", new DemoSoundGroup(this, 4, null, null, SoundSamples["Footsteps"], null));
            SoundGroups.Add("Hit", new DemoSoundGroup(this, 10, SoundSamples["Hit1"], null, null, null));
            SoundGroups.Add("Glass", new DemoSoundGroup(this, 4, SoundSamples["Hit2"], SoundSamples["Roll"], SoundSamples["Slide"], null));
            SoundGroups.Add("Roll", new DemoSoundGroup(this, 4, null, SoundSamples["Roll"], SoundSamples["Roll"], null));
            SoundGroups.Add("RollSlide", new DemoSoundGroup(this, 4, null, SoundSamples["Roll"], SoundSamples["Slide"], null));

            string materialPath = "Materials";
            Textures.Add("Default", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Iron", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Brass", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Rubber", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Plastic1", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Plastic2", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Wood1", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Wood2", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Paint1", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Paint2", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Ground", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Blue", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Yellow", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Green", new DemoTexture(this, materialPath, ".jpg", true));
            Textures.Add("Leaf", new DemoTexture(this, materialPath, ".jpg", true));

            string skycubePath = "Skycubes";
            Textures.Add("SkyXZ", new DemoTexture(this, skycubePath, ".jpg", true));
            Textures.Add("SkyPosY", new DemoTexture(this, skycubePath, ".jpg", true));
            Textures.Add("SkyNegY", new DemoTexture(this, skycubePath, ".jpg", true));

            string fontPath = "Fonts";
            Textures.Add("DefaultFont", new DemoTexture(this, fontPath, ".png", false));

            string terrainPath = "Terrains";
            Textures.Add("DefaultHeights", new DemoTexture(this, terrainPath, ".png", false));
            Textures.Add("DefaultFrictions", new DemoTexture(this, terrainPath, ".png", false));
            Textures.Add("DefaultRestitutions", new DemoTexture(this, terrainPath, ".png", false));

            string menuPath = "Menus";
            string menuScreensPath = null;

            menuPath = Path.Combine(menuPath, "Default");
            menuScreensPath = Path.Combine(menuPath, "Screens");

            Textures.Add("DefaultShapes", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("UserShapes", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("Stacks", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("Ragdolls", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("Bridges", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("Building", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("AI", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("Helicopters", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("Buildings", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("TerrainWithWater", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("Animation", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("Cloth", new DemoTexture(this, menuScreensPath, ".jpg", true));
            Textures.Add("Meshes", new DemoTexture(this, menuScreensPath, ".jpg", true));

            string path = null;
            string directoryPath = null;
            string changeDirectorySeparator = "..";
            directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, changeDirectorySeparator);
            directoryPath = Path.Combine(directoryPath, changeDirectorySeparator);
            directoryPath = Path.Combine(directoryPath, changeDirectorySeparator);
            directoryPath = Path.Combine(directoryPath, changeDirectorySeparator);
            directoryPath = Path.Combine(directoryPath, changeDirectorySeparator);
            directoryPath = Path.Combine(directoryPath, changeDirectorySeparator);
            directoryPath = Path.Combine(directoryPath, "Data");
            directoryPath = Path.GetFullPath(directoryPath);

            Dictionary<string, DemoSoundSample>.Enumerator position1 = SoundSamples.GetEnumerator();
            while (position1.MoveNext())
            {
                DemoSoundSample soundSample = position1.Current.Value;
                string soundName = position1.Current.Key + soundSample.FileExt;

                path = directoryPath;

                if (soundSample.FileDirectory != null)
                    path = Path.Combine(path, soundSample.FileDirectory);

                path = Path.Combine(path, soundName);

                if (File.Exists(path))
                {
                    soundSample.Set(File.OpenRead(path));
                }
                else
                {
                    Console.WriteLine("File " + path + " could not be found.");
                    this.Exit();
                    return;
                }
            }

            Dictionary<string, DemoTexture>.Enumerator position2 = Textures.GetEnumerator();
            while (position2.MoveNext())
            {
                DemoTexture texture = position2.Current.Value;
                string textureName = position2.Current.Key + texture.FileExt;

                path = directoryPath;

                if (texture.FileDirectory != null)
                    path = Path.Combine(path, texture.FileDirectory);

                path = Path.Combine(path, textureName);

                if (File.Exists(path))
                {
                    texture.Create(new Bitmap(path));
                }
                else
                {
                    Console.WriteLine("File " + path + " could not be found.");
                    this.Exit();
                    return;
                }
            }

            Textures.Add("Sky", new DemoTexture(this, Textures["SkyXZ"].Bitmap, Textures["SkyPosY"].Bitmap, Textures["SkyNegY"].Bitmap, true));

            DemoFont = new DemoFont(this, "DefaultFont", 150);
            DemoFont3D = new DemoFont3D(this, "DefaultFont", 150);

            // Create a new lists of menu descriptions
            Descriptions.Add("DefaultShapes", new List<string>() {
            "Scene:",      
            "Default shapes",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A: Player control",
            "Mouse right button: Camera control",
            "J, K: Sun position control",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("UserShapes", new List<string>() {
            "Scene:",
            "User shapes, transparent objects, force fields, switches,",
            "triangle meshes",
            null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A: Player control",
            "Mouse right button: Camera control",
            "J, K: Sun position control",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("Stacks", new List<string>() {
            "Scene:",
            "Stacking (Jenga, Pyramid, Wall)",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A: Player control",
            "Mouse right button: Camera control",
            "J, K: Sun position control",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors, ",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("Ragdolls", new List<string>() {
            "Scene:",
            "Ragdolls",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A: Player control",
            "Mouse right button: Camera control",
            "J, K, Tab: Sun position control, Toggle camera",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("Bridges", new List<string>() {
            "Scene:",
            "Bridges, vehicle, ragdoll",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A: Player control",
            "Mouse right button: Camera control",
            "J, K: Sun position control",
            "Arrows up, down, right, left: Vehicle remote control",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("Building", new List<string>() {
            "Scene:",
            "Building, vehicle, ragdoll, plant, particles",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A, Space: Player control",
            "Mouse right button: Camera control",
            "J, K, Tab: Sun position control, Toggle camera mode",
            "Arrows up, down, right, left: Vehicle remote control",
            "Space + Collision with switch: Switch to driver seat",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("AI", new List<string>() {
            "Scene:",
            "Simple physical AI",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A, Space: Player control",
            "Mouse right button: Camera control",
            "J, K, Tab: Sun position control, Toggle camera mode",
            "Arrows up, down, right, left, PgUp/Down, F: Vehicle remote control, vehicle shoot",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("Helicopters", new List<string>() {
            "Scene:",
            "Physical waypoints, vehicles in the air",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A, Space: Player control",
            "Mouse right button: Camera control",
            "J, K, Tab: Sun position control, Toggle camera mode",
            "Collision with switch: Run vehicle",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("Buildings", new List<string>() {
            "Scene:",
            "Buildings, bridge",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A, Space: Player control",
            "Mouse right button: Camera control",
            "J, K, Tab: Sun position control, Toggle camera mode",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("TerrainWithWater", new List<string>() {
            "Scene:",
            "Terrain, water, vehicles",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A, Space: Player control",
            "Mouse right button: Camera control",
            "J, K, Tab: Sun position control, Toggle camera mode",
            "Arrows up, down, right, left: Vehicle remote control",
            "Space + Collision with switch: Switch to driver seat",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("Animation", new List<string>() {
            "Scene:",
            "Physical animation",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A, Space: Player control",
            "Mouse right button: Camera control",
            "J, K, Tab: Sun position control, Toggle camera mode",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("Cloth", new List<string>() {
            "Scene:",
            "Point cloth",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A, Space: Player control",
            "Mouse right button: Camera control",
            "J, K, Tab: Sun position control, Toggle camera mode",
            "Collision with switch: Run machine",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            Descriptions.Add("Meshes", new List<string>() {
            "Scene:",
            "Triangle meshes",
            null, null,
            "Input:",
            "+, -: Toggle scene",
            "R: Reset scene",
            "W, S, D, A, Space: Player control",
            "Mouse right button: Camera control",
            "J, K, Tab: Sun position control, Toggle camera mode",
            "B, C, V, I, G, N: Show bounding boxes, contact points, sleeping objects, impact factors,",
            "lights, wireframe objects",
            "Mouse move: Cursor control",
            "Mouse middle buton, ctrl: Shoot",
            "Mouse move + mouse left button: Grab objects",
            "Mouse wheel + mouse left button: Move objects to and from player",
            "P: Print screen to file" });

            // Create a new scene objects
            Scenes.Add(new DefaultShapesScene(this, "DefaultShapes", 1, "Default shapes"));
            Scenes.Add(new UserShapesScene(this, "UserShapes", 1, "User shapes, transparent objects, force fields, switches, triangle meshes"));
            Scenes.Add(new StacksScene(this, "Stacks", 1, "Stacking (Jenga, Pyramid, Wall)"));
            Scenes.Add(new RagdollsScene(this, "Ragdolls", 1, "Ragdolls"));
            Scenes.Add(new BridgesScene(this, "Bridges", 1, "Bridges, vehicle, ragdoll"));
            Scenes.Add(new BuildingScene(this, "Building", 1, "Building, vehicle, ragdoll, plant, particles"));
            Scenes.Add(new AIScene(this, "AI", 1, "Simple physical AI"));
            Scenes.Add(new HelicoptersScene(this, "Helicopters", 1, "Physical waypoints, vehicles in the air"));
            Scenes.Add(new BuildingsScene(this, "Buildings", 1, "Buildings, bridge"));
            Scenes.Add(new TerrainWithWaterScene(this, "TerrainWithWater", 1, "Terrain, water, vehicles"));
            Scenes.Add(new AnimationScene(this, "Animation", 1, "Physical animation"));
            Scenes.Add(new ClothScene(this, "Cloth", 1, "Point cloth"));
            Scenes.Add(new MeshesScene(this, "Meshes", 1, "Triangle meshes"));
            //Scenes.Add(new SimpleScene(this, "Simple", 1, "Simple scene"));

            // Create a new physics scene for the menu object
            MenuScene.Create();
            MenuScene.Refresh(0.0);

            // Create a new physics scene for the scene object
            SceneIndex = 0;
            Scene = Scenes[SceneIndex];
            Scene.Create();
            Scene.Refresh(0.0);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void OnUnload(EventArgs e)
        {
            Meshes.Clear();
            Scenes.Clear();
            Textures.Clear();

            Engine.Exit();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
        }

        protected void OnUpdateFrame(double time)
        {
            DemoKeyboardState keyboardState = GetKeyboardState();

            if (keyboardState[Key.Escape])
                this.Exit();

            if (keyboardState[Key.M] && !oldKeyboardState[Key.M])
            {
                EnableMenu = !EnableMenu;

                if (EnableMenu)
                {
                    MenuScene.Create();
                    MenuScene.Refresh(0.0);
                }
            }

            if (keyboardState[Key.R] && !oldKeyboardState[Key.R])
            {
                ClearAllSounds();

                Scene.Remove();
                Scene.Create();
                Scene.Refresh(0.0);
            }

            if (keyboardState[Key.P] && !oldKeyboardState[Key.P])
            {
                int screenCount = 1;
                while (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Screen" + screenCount.ToString("d3") + ".bmp"))
                    screenCount++;

                Bitmap texture = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                BitmapData data = texture.LockBits(new Rectangle(0, 0, texture.Width, texture.Height), ImageLockMode.WriteOnly, texture.PixelFormat);
                GL.ReadPixels(0, 0, texture.Width, texture.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                texture.UnlockBits(data);
                texture.RotateFlip(RotateFlipType.RotateNoneFlipY);
                texture.Save(AppDomain.CurrentDomain.BaseDirectory + "Screen" + screenCount.ToString("d3") + ".bmp", ImageFormat.Bmp);
            }

            if (keyboardState[Key.Plus] && !oldKeyboardState[Key.Plus])
            {
                ClearAllSounds();

                SceneIndex = (SceneIndex + 1) % Scenes.Count;
                Scene = Scenes[SceneIndex];
                Scene.Create();
                Scene.Refresh(0.0);
            }

            if (keyboardState[Key.Minus] && !oldKeyboardState[Key.Minus])
            {
                ClearAllSounds();

                SceneIndex = (SceneIndex - 1) < 0 ? Scenes.Count - 1 : SceneIndex - 1;
                Scene = Scenes[SceneIndex];
                Scene.Create();
                Scene.Refresh(0.0);
            }

            oldKeyboardState = keyboardState;

            // Simulate with the synchronization
            bool isSimulateSynchronized = Scene.PhysicsScene.IsSimulateSynchronized(time);

            if (EnableMenu && isSimulateSynchronized)
            {
                int oldSceneIndex = SceneIndex;

                MenuScene.PhysicsScene.Simulate(time);

                if (SceneIndex != oldSceneIndex)
                {
                    ClearAllSounds();

                    Scene = Scenes[SceneIndex];
                    Scene.Create();
                    Scene.Refresh(0.0);
                }
            }

            Scene.PhysicsScene.SimulateWithSynchronization(time);

            UpdateSoundQueue();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            double time = e.Time;

            OnUpdateFrame(time);

            // Draw with the synchronization
            bool isDrawSynchronized = Scene.PhysicsScene.IsDrawSynchronized(time);

            if (isDrawSynchronized) GL.Clear(ClearBufferMask.DepthBufferBit);

            Scene.PhysicsScene.DrawWithSynchronization(time);

            if (EnableMenu && isDrawSynchronized)
            {
                GL.Clear(ClearBufferMask.DepthBufferBit);
                MenuScene.PhysicsScene.Draw(time);
            }

            if (isDrawSynchronized) SwapBuffers();
        }

        public void DrawInfo(DrawMethodArgs args)
        {
            PhysicsScene scene = Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);

            int offsetX = 0;
            int offsetY = 0;

            int physicsObjectCount = scene.TotalPhysicsObjectCount;
            int constraintCount = scene.TotalConstraintCount;
            int contactPointCount = scene.TotalContactPointCount;

            if (EnableMenu)
            {
                physicsObjectCount += MenuScene.PhysicsScene.TotalPhysicsObjectCount;
                constraintCount += MenuScene.PhysicsScene.TotalConstraintCount;
                contactPointCount += MenuScene.PhysicsScene.TotalContactPointCount;
            }

            infoDrawFPSNameBuilder.Remove(infoDrawFPSNameLength, infoDrawFPSNameBuilder.Length - infoDrawFPSNameLength);
            infoDrawFPSNameBuilder.Append((int)(scene.DrawFPS + 0.5f));

            infoPhysicsFPSNameBuilder.Remove(infoPhysicsFPSNameLength, infoPhysicsFPSNameBuilder.Length - infoPhysicsFPSNameLength);
            infoPhysicsFPSNameBuilder.Append((int)(scene.SimulationFPS + 0.5f));

            infoPhysicsObjectsNameBuilder.Remove(infoPhysicsObjectsNameLength, infoPhysicsObjectsNameBuilder.Length - infoPhysicsObjectsNameLength);
            infoPhysicsObjectsNameBuilder.Append(physicsObjectCount);

            infoConstraintsNameBuilder.Remove(infoConstraintsNameLength, infoConstraintsNameBuilder.Length - infoConstraintsNameLength);
            infoConstraintsNameBuilder.Append(constraintCount);

            infoContactPointsNameBuilder.Remove(infoContactPointsNameLength, infoContactPointsNameBuilder.Length - infoContactPointsNameLength);
            infoContactPointsNameBuilder.Append(contactPointCount);

            GL.CullFace(CullFaceMode.Back);

            DemoFont.Begin();

            DemoFont.Draw(offsetX + 15, offsetY + 15, 1.0f, 1.0f, Scene.SceneInfo, whiteColor);
            DemoFont.Draw(offsetX + 15, offsetY + 30, 1.0f, 1.0f, infoDrawFPSNameBuilder.ToString(), whiteColor);
            DemoFont.Draw(offsetX + 15, offsetY + 45, 1.0f, 1.0f, infoPhysicsFPSNameBuilder.ToString(), whiteColor);
            DemoFont.Draw(offsetX + 15, offsetY + 60, 1.0f, 1.0f, infoPhysicsObjectsNameBuilder.ToString(), whiteColor);
            DemoFont.Draw(offsetX + 15, offsetY + 75, 1.0f, 1.0f, infoConstraintsNameBuilder.ToString(), whiteColor);
            DemoFont.Draw(offsetX + 15, offsetY + 90, 1.0f, 1.0f, infoContactPointsNameBuilder.ToString(), whiteColor);
            DemoFont.Draw(offsetX + 15, offsetY + 105, 1.0f, 1.0f, infoMenuName, whiteColor);

            DemoFont.End();
        }

        public void UpdateSoundQueue()
        {
            DemoSound sound;

            while (SoundQueue.SoundCount > 0)
            {
                sound = SoundQueue.DequeueSound();
                sound.Start();
            }
        }

        public void ClearAllSounds()
        {
            SoundQueue.Clear();

            Dictionary<string, DemoSoundGroup>.Enumerator position = SoundGroups.GetEnumerator();
            while (position.MoveNext())
                position.Current.Value.ClearAllSounds();
        }

        public static void CreateSharedShapes(Demo demo, PhysicsScene scene)
        {
            ShapePrimitive shapePrimitive = null;
            Shape shape = null;

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Point");
            shapePrimitive.CreatePoint(0.0f, 0.0f, 0.0f);
            shape = scene.Factory.ShapeManager.Create("Point");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.1f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Edge");
            shapePrimitive.CreateEdge(-Vector3.UnitY, Vector3.UnitY);
            shape = scene.Factory.ShapeManager.Create("Edge");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.1f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Box");
            shapePrimitive.CreateBox(1.0f);
            shape = scene.Factory.ShapeManager.Create("Box");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("CylinderY");
            shapePrimitive.CreateCylinderY(2.0f, 1.0f);
            shape = scene.Factory.ShapeManager.Create("CylinderY");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Sphere");
            shapePrimitive.CreateSphere(1.0f);
            shape = scene.Factory.ShapeManager.Create("Sphere");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            // Declare and initialize the displacement vector of geometric center 
            // with negated value returned by GetCenterTranslation function
            // after use TranslateToCenter function in TriangleMesh class for hemisphere
            Vector3 centerTranslation = new Vector3(0.0f, -0.03638184f, -0.4571095f);
            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("HemisphereZ");
            shapePrimitive.CreateHemisphereZ(1.0f);
            shape = scene.Factory.ShapeManager.Create("HemisphereZ");
            shape.Set(shapePrimitive, Matrix4.CreateTranslation(centerTranslation), 0.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("ConeY");
            shapePrimitive.CreateConeY(2.0f, 1.0f);
            shape = scene.Factory.ShapeManager.Create("ConeY");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("ConeZ");
            shapePrimitive.CreateConeZ(2.0f, 1.0f);
            shape = scene.Factory.ShapeManager.Create("ConeZ");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("CapsuleY");
            shapePrimitive.CreateCapsuleY(2.0f, 1.0f);
            shape = scene.Factory.ShapeManager.Create("CapsuleY");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.0f);

            TriangleMesh triangleMesh = null;
            triangleMesh = scene.Factory.TriangleMeshManager.Create("Point");
            triangleMesh.CreateSphere(5, 10, 1.0f);
            if (!demo.Meshes.ContainsKey("Point"))
                demo.Meshes.Add("Point", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Edge");
            triangleMesh.CreateCylinderY(1, 10, 2.2f, 1.0f);
            if (!demo.Meshes.ContainsKey("Edge"))
                demo.Meshes.Add("Edge", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Box");
            triangleMesh.CreateBox(1.0f);
            if (!demo.Meshes.ContainsKey("Box"))
                demo.Meshes.Add("Box", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, false, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("CylinderY");
            triangleMesh.CreateCylinderY(1, 15, 2.0f, 1.0f);
            if (!demo.Meshes.ContainsKey("CylinderY"))
                demo.Meshes.Add("CylinderY", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Sphere");
            triangleMesh.CreateSphere(10, 15, 1.0f);
            if (!demo.Meshes.ContainsKey("Sphere"))
                demo.Meshes.Add("Sphere", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("HemisphereZ");
            triangleMesh.CreateHemisphereZ(5, 15, 1.0f);
            triangleMesh.TranslateToCenter();
            if (!demo.Meshes.ContainsKey("HemisphereZ"))
                demo.Meshes.Add("HemisphereZ", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("ConeY");
            triangleMesh.CreateConeY(1, 15, 2.0f, 1.0f);
            if (!demo.Meshes.ContainsKey("ConeY"))
                demo.Meshes.Add("ConeY", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("ConeZ");
            triangleMesh.CreateConeZ(1, 15, 2.0f, 1.0f);
            if (!demo.Meshes.ContainsKey("ConeZ"))
                demo.Meshes.Add("ConeZ", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("CapsuleY");
            triangleMesh.CreateCapsuleY(10, 15, 2.0f, 1.0f);
            if (!demo.Meshes.ContainsKey("CapsuleY"))
                demo.Meshes.Add("CapsuleY", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, true, true, true, false, true, CullFaceMode.Back, false, false));

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Sky");
            triangleMesh.CreateSphere(10, 10, 1.0f);
            if (!demo.Meshes.ContainsKey("Sky"))
                demo.Meshes.Add("Sky", new DemoMesh(demo, triangleMesh, demo.Textures["Sky"], Vector2.One, true, true, true, false, true, CullFaceMode.Front, false, false));

            Vector3 point1, point2, point3;
            point1 = new Vector3(-1.0f, -1.0f, 0.0f);
            point2 = new Vector3(-1.0f, 1.0f, 0.0f);
            point3 = new Vector3(1.0f, 1.0f, 0.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Triangle1");
            triangleMesh.CreateTriangle(point1, point2, point3);
            if (!demo.Meshes.ContainsKey("Triangle1"))
                demo.Meshes.Add("Triangle1", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, true, CullFaceMode.FrontAndBack, false, false));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Triangle1");
            shapePrimitive.CreateTriangle(point1, point2, point3);

            shape = scene.Factory.ShapeManager.Create("Triangle1");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.01f);

            point1 = new Vector3(1.0f, 1.0f, 0.0f);
            point2 = new Vector3(1.0f, -1.0f, 0.0f);
            point3 = new Vector3(-1.0f, -1.0f, 0.0f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Triangle2");
            triangleMesh.CreateTriangle(point1, point2, point3);
            if (!demo.Meshes.ContainsKey("Triangle2"))
                demo.Meshes.Add("Triangle2", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, true, CullFaceMode.FrontAndBack, false, false));

            shapePrimitive = scene.Factory.ShapePrimitiveManager.Create("Triangle2");
            shapePrimitive.CreateTriangle(point1, point2, point3);

            shape = scene.Factory.ShapeManager.Create("Triangle2");
            shape.Set(shapePrimitive, Matrix4.Identity, 0.01f);

            triangleMesh = scene.Factory.TriangleMeshManager.Create("Quad");

            float[] positionsX = { -1.0f, -1.0f, 1.0f, 1.0f };
            float[] positionsY = { -1.0f, 1.0f, 1.0f, -1.0f };

            triangleMesh.CreateWall(Vector3.Zero, Vector3.UnitZ, positionsX, positionsY);

            if (!demo.Meshes.ContainsKey("Quad"))
                demo.Meshes.Add("Quad", new DemoMesh(demo, triangleMesh, demo.Textures["Default"], Vector2.One, false, false, true, false, true, CullFaceMode.FrontAndBack, false, false));
        }
    }
}
