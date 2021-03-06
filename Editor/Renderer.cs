﻿using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Editor
{
    static class Renderer
    {
        internal static int texture;
        internal static readonly Font TextFont = new Font(FontFamily.GenericSansSerif, 11);
        internal static Bitmap TextBitmap;
        internal static StringBuilder TypedText = new StringBuilder();
        internal static int viewportWidth = 0;
        internal static int viewportHeight = 0;
        internal static OpenTK.GLControl renderView;
        internal static bool mouse_in_glControl = false;
        internal static bool viewport_changed = true;

        #region Text Debug Example       
        // timing information
        static double update_time, render_time, timestamp;
        static int update_count, update_fps, render_count, render_fps;
        #endregion

        internal static void Load()
        {
            GL.ClearColor(Color.MidnightBlue);
            LoadDebugDisplay();
        }

        internal static void Resize(ref ModernGLControl glControl1)
        {
            viewport_changed = true;

            renderView = glControl1;
            viewportWidth = renderView.Width;
            viewportHeight = renderView.Height;

            renderView.Invalidate();
        }

        internal static void SetupViewport()
        {
            GL.Viewport(0, 0, viewportWidth, viewportHeight); // Use all of the glControl painting area
        }

        #region Immediate Mode
        internal static void DefaultRender(float rotation)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (viewport_changed)
            {
                viewport_changed = false;
                SetupViewport();
            }

            DrawText();

            DrawCube(viewportWidth / (float)viewportHeight, rotation);

            renderView.SwapBuffers();
        }

        // Uploads our text Bitmap to an OpenGL texture
        // and displays is to screen.
        private static void DrawText()
        {
            System.Drawing.Imaging.BitmapData data = TextBitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, TextBitmap.Width, TextBitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, TextBitmap.Width, TextBitmap.Height, PixelFormat.Bgra,
                PixelType.UnsignedByte, data.Scan0);
            TextBitmap.UnlockBits(data);

            Matrix4 text_projection = Matrix4.CreateOrthographicOffCenter(0, viewportWidth, viewportHeight, 0, -1, 1);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref text_projection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Color4(Color4.White);
            GL.Enable(EnableCap.Texture2D);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(0, 0);
            GL.TexCoord2(1, 0); GL.Vertex2(TextBitmap.Width, 0);
            GL.TexCoord2(1, 1); GL.Vertex2(TextBitmap.Width, TextBitmap.Height);
            GL.TexCoord2(0, 1); GL.Vertex2(0, TextBitmap.Height);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private static void DrawMovingObjects()
        {
            Matrix4 thing_projection = Matrix4.CreateOrthographic(2, 2, -1, 1);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref thing_projection);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0, -0.2, 0);
            GL.Color4(Color4.Red);
            DrawRectangle();

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0, -0.4, 0);
            GL.Color4(Color4.DarkGoldenrod);
            DrawRectangle();

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0, -0.8, 0);
            GL.Color4(Color4.DarkGreen);
            DrawRectangle();
        }

        private static void DrawRectangle()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.05, -0.05);
            GL.Vertex2(+0.05, -0.05);
            GL.Vertex2(+0.05, +0.05);
            GL.Vertex2(-0.05, +0.05);
            GL.End();
        }

        private static void DrawCube(float aspect_ratio, float rotation)
        {
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);

            GL.Enable(EnableCap.DepthTest);

            Matrix4 lookat = Matrix4.LookAt(0, 5, 5, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Rotate(rotation, Vector3.UnitY);

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();

            GL.Disable(EnableCap.DepthTest);
        }
        #endregion

        #region Debug Text Rendering Methods

        private static void LoadDebugDisplay()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.OneMinusSrcColor);

            TextBitmap = new Bitmap(viewportWidth, viewportHeight);
            texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, TextBitmap.Width, TextBitmap.Height,
                0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
        }

        internal static void UpdateTextDisplay(ref Stopwatch watch)
        {
            double clock_time = watch.Elapsed.TotalSeconds;
            //update_time += e.Time;
            //timestamp += e.Time;
            //update_count++;

            using (Graphics gfx = Graphics.FromImage(TextBitmap))
            {
                int line = 0;

                gfx.Clear(Color.Black);
                gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                // OpenGL information
                DrawString(gfx, Renderer.GetVendor(), line++);
                DrawString(gfx, Renderer.GetVerion(), line++);
                DrawString(gfx, renderView.GraphicsMode.ToString(), line++);

                // GameWindow information
                line++;
                DrawString(gfx, "GLControl:", line++);

                DrawString(gfx, String.Format("[V]: VSync.{0}.", renderView.VSync), line++);
                DrawString(gfx, String.Format("Bounds: {0}", renderView.Bounds), line++);
                DrawString(gfx, String.Format("ClientRectangle: {0}", renderView.ClientRectangle), line++);
                DrawString(gfx, String.Format("Mouse {0}: {1}.",
                    mouse_in_glControl ? "inside" : "outside",
                    renderView.Focused ? "Focused" : "Not focused"), line++);
                DrawString(gfx, String.Format("Mouse coordinates: {0}", new Vector3(
                    OpenTK.Input.Mouse.GetState().X,
                    OpenTK.Input.Mouse.GetState().Y,
                    OpenTK.Input.Mouse.GetState().WheelPrecise)),
                    line++
                    );

                #region Timing Information
                /*
                line++;
                DrawString(gfx, "Timing:", line++);
                DrawString(gfx,
                    String.Format("Frequency: update {4} ({0:f2}/{1:f2}); render {5} ({2:f2}/{3:f2})",
                        UpdateFrequency, TargetUpdateFrequency,
                        RenderFrequency, TargetRenderFrequency,
                        update_fps, render_fps),
                    line++);
                DrawString(gfx,
                    String.Format("Period: update {4:N4} ({0:f4}/{1:f4}); render {5:N4} ({2:f4}/{3:f4})",
                        UpdatePeriod, TargetUpdatePeriod,
                        RenderPeriod, TargetRenderPeriod,
                        1.0 / update_fps, 1.0 / render_fps),
                    line++);
                DrawString(gfx, String.Format("Time: update {0:f4}; render {1:f4}",
                    UpdateTime, RenderTime), line++);
                DrawString(gfx, String.Format("Drift: clock {0:f4}; update {1:f4}; render {2:f4}",
                    clock_time, clock_time - update_time, clock_time - render_time), line++);
                DrawString(gfx, String.Format("Text: {0}", TypedText.ToString()), line++);

                if (timestamp >= 1)
                {
                    timestamp -= 1;
                    update_fps = update_count;
                    render_fps = render_count;
                    update_count = 0;
                    render_count = 0;

                }
                 * 
                 * */
                #endregion

                // Input information
                line = DrawKeyboards(gfx, line);
                line = DrawMice(gfx, line);
                // line = DrawJoysticks(gfx, line);
                // line = DrawLegacyJoysticks(gfx, Joysticks, line);
            }

            /*
            fixed_update_timestep_pos += TargetUpdatePeriod;
            variable_update_timestep_pos += e.Time;
            if (fixed_update_timestep_pos >= 1)
                fixed_update_timestep_pos -= 2;
            if (variable_update_timestep_pos >= 1)
                variable_update_timestep_pos -= 2;
             * */
        }

        private static float DrawString(Graphics gfx, string str, int line)
        {
            return DrawString(gfx, str, line, 0);
        }

        private static float DrawString(Graphics gfx, string str, int line, float offset)
        {
            gfx.DrawString(str, TextFont, Brushes.White, new PointF(offset, line * TextFont.Height));
            return offset + gfx.MeasureString(str, TextFont).Width;
        }

        private static int DrawKeyboards(Graphics gfx, int line)
        {
            line++;
            DrawString(gfx, "Keyboard:", line++);
            for (int i = 0; i < 4; i++)
            {
                var state = OpenTK.Input.Keyboard.GetState(i);
                if (state.IsConnected)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(i);
                    sb.Append(": ");
                    for (int key_index = 0; key_index < (int)Key.LastKey; key_index++)
                    {
                        Key k = (Key)key_index;
                        if (state[k])
                        {
                            sb.Append(k);
                            sb.Append(" ");
                        }
                    }
                    DrawString(gfx, sb.ToString(), line++);
                }
            }
            return line;
        }

        private static int DrawMice(Graphics gfx, int line)
        {
            line++;
            DrawString(gfx, "Mouse:", line++);
            for (int i = 0; i < 4; i++)
            {
                var state = OpenTK.Input.Mouse.GetState(i);
                if (state.IsConnected)
                {
                    StringBuilder sb = new StringBuilder();
                    Vector3 pos = new Vector3(state.X, state.Y, state.WheelPrecise);
                    sb.Append(i);
                    sb.Append(": ");
                    sb.Append(pos);
                    for (int button_index = 0; button_index < (int)MouseButton.LastButton; button_index++)
                    {
                        MouseButton b = (MouseButton)button_index;
                        if (state[b])
                        {
                            sb.Append(b);
                            sb.Append(" ");
                        }
                    }
                    DrawString(gfx, sb.ToString(), line++);
                }
            }
            return line;
        }

        private static int DrawJoysticks(Graphics gfx, int line)
        {
            line++;
            DrawString(gfx, "GamePad:", line++);
            for (int i = 0; i < 4; i++)
            {
                GamePadCapabilities caps = GamePad.GetCapabilities(i);
                GamePadState state = GamePad.GetState(i);
                if (state.IsConnected)
                {
                    DrawString(gfx, String.Format("{0}: {1}", i, caps), line++);
                    DrawString(gfx, state.ToString(), line++);
                }
            }

            line++;
            DrawString(gfx, "Joystick:", line++);
            for (int i = 0; i < 4; i++)
            {
                JoystickCapabilities caps = Joystick.GetCapabilities(i);
                JoystickState state = Joystick.GetState(i);
                if (state.IsConnected)
                {
                    DrawString(gfx, String.Format("{0}: {1}", i, caps), line++);
                    DrawString(gfx, state.ToString(), line++);
                }
            }

            return line;
        }

        private static int DrawLegacyJoysticks(Graphics gfx, IList<JoystickDevice> joysticks, int line)
        {
            line++;
            DrawString(gfx, "Legacy Joystick:", line++);

            int joy_index = -1;
            foreach (var joy in joysticks)
            {
                joy_index++;
                if (!String.IsNullOrEmpty(joy.Description))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(joy_index);
                    sb.Append(": '");
                    sb.Append(joy.Description);
                    sb.Append("' ");

                    for (int i = 0; i < joy.Axis.Count; i++)
                    {
                        sb.Append(joy.Axis[i]);
                        sb.Append(" ");
                    }

                    for (int i = 0; i < joy.Button.Count; i++)
                    {
                        sb.Append(joy.Button[i]);
                        sb.Append(" ");
                    }
                    DrawString(gfx, sb.ToString(), line++);
                }
            }

            return line;
        }

        #endregion

        #region Version Info

        internal static string GetVersionInfo()
        {
            return GetVendor() + " " + GetRenderer() + " " + GetVerion();
        }

        internal static string GetVendor()
        {
            return GL.GetString(StringName.Vendor);
        }

        internal static string GetRenderer()
        {
            return GL.GetString(StringName.Renderer);
        }

        internal static string GetVerion()
        {
            return GL.GetString(StringName.Version);
        }

        #endregion
    }
}
