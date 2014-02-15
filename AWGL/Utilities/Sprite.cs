using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAOS.Utilities
{
    public class Sprite
    {
        internal const int VertexAmount = 6;
        Vector3d[] vertexPositions = new Vector3d[VertexAmount];
        Color4[] vertexColours = new Color4[VertexAmount];
        Vector2[] vertexUVs = new Vector2[VertexAmount];
        Texture texture = new Texture();

        public Texture Texture
        {
            get { return texture; }
            set
            {
                texture = value;

                InitVertexPositions(GetCentre(), texture.Width, texture.Height);
            }
        }
        public Vector3d[] VertexPositions { get { return vertexPositions; } }

        public Color4[] VertexColours { get { return vertexColours; } }

        public Vector2[] VertexUVs { get { return vertexUVs; } }

        public Sprite()
        {
            InitVertexPositions(new Vector3d(0, 0, 0 ), 1, 1);
            //SetColour(new Color4(1, 1, 1, 1));
            SetUVs(new Vector2(0, 0), new Vector2(1, 1));
        }

        private Vector3d GetCentre()
        {
            double halfWidth = GetWidth() / 2;
            double halfHeight = GetHeight() / 2;

            return new Vector3d(
                vertexPositions[0].X + halfWidth,
                vertexPositions[0].Y - halfHeight,
                vertexPositions[0].Z);
        }

        private void InitVertexPositions(Vector3d position, double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;

            vertexPositions[0] = new Vector3d(position.X - halfWidth, position.Y + halfHeight, position.Z); //top left
            vertexPositions[1] = new Vector3d(position.X + halfWidth, position.Y + halfHeight, position.Z); //top right
            vertexPositions[2] = new Vector3d(position.X - halfWidth, position.Y - halfHeight, position.Z); //bottom left

            vertexPositions[3] = new Vector3d(position.X + halfWidth, position.Y + halfHeight, position.Z); //top right
            vertexPositions[4] = new Vector3d(position.X + halfWidth, position.Y + -halfHeight, position.Z); //bottom right
            vertexPositions[5] = new Vector3d(position.X - halfWidth, position.Y - halfHeight, position.Z); //bottom left
        }

        public double GetWidth()
        {
            //top right -> top left
            return vertexPositions[1].X - vertexPositions[0].X;
        }

        public double GetHeight()
        {
            //top left -> bottom left
            return vertexPositions[0].Y - vertexPositions[2].Y;
        }

        public void SetWidth(double width)
        {
            InitVertexPositions(GetCentre(), width, GetHeight());
        }

        public void SetHeight(double height)
        {
            InitVertexPositions(GetCentre(), GetWidth(), height);
        }

        public void SetPosition(double x, double y)
        {
            SetPosition(new Vector3d(x, y, 0));
        }

        public void SetPosition(Vector3d position)
        {
            InitVertexPositions(position, GetWidth(), GetHeight());
        }

        public void SetColour(Color4 color4)
        {
            for (int i = 0; i < Sprite.VertexAmount; i++)
            {
                vertexColours[i] = color4;
            }
        }

        private void SetUVs(Vector2 topLeft, Vector2 bottomRight)
        {
            vertexUVs[0] = topLeft;
            vertexUVs[1] = new Vector2(bottomRight.X, topLeft.Y);
            vertexUVs[2] = new Vector2(topLeft.X, bottomRight.Y);

            vertexUVs[3] = new Vector2(bottomRight.X, topLeft.Y);
            vertexUVs[4] = bottomRight;
            vertexUVs[5] = new Vector2(topLeft.X, bottomRight.Y);
        }
    }
}
