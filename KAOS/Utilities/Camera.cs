using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.Utilities
{
    /// <summary>
    /// Camera: http://neokabuto.blogspot.co.uk/2014/01/opentk-tutorial-5-basic-camera.html (slightly modified)
    /// </summary>
    public static class Camera
    {
        public static Vector3 Position = Vector3.Zero;
        public static Vector3 Orientation = new Vector3((float)Math.PI, 0f, 0f);
        public static float MoveSpeed = 0.2f;
        public static float MouseSensitivity = 0.01f;

        public static Matrix4 GetViewMatrix()
        {
            var lookat = new Vector3
            {
                X = (float) (Math.Sin((float) Orientation.X)*Math.Cos((float) Orientation.Y)),
                Y = (float) Math.Sin((float) Orientation.Y),
                Z = (float) (Math.Cos((float) Orientation.X)*Math.Cos((float) Orientation.Y))
            };

            return Matrix4.LookAt(Position, Position + lookat, Vector3.UnitY);
        }

        public static void Move(float x, float y, float z)
        {
            var offset = new Vector3();

            var forward = new Vector3((float)Math.Sin((float)Orientation.X), 0, (float)Math.Cos((float)Orientation.X));
            var right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset.NormalizeFast();
            offset = Vector3.Multiply(offset, MoveSpeed);

            Position += offset;

            Logger.WriteLine("Camera Position = " + Position);
        }

        public static void AddRotation(float x, float y)
        {
            x = x * MouseSensitivity;
            y = y * MouseSensitivity;

            Orientation.X = (Orientation.X + x) % ((float)Math.PI * 2.0f);
            Orientation.Y = Math.Max(Math.Min(Orientation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }
    }
}
