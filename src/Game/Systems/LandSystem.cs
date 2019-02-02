using System;
using ClassicUO.IO.Resources;
using ClassicUO.Renderer;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.Systems
{
    struct LandData
    {
        public Rectangle Rectangle;
        public LandTiles TileData;
        public sbyte MinZ;
        public sbyte AverageZ;
        public bool IsStretched;
        public Position Position;

    }

    struct LandView
    {
        public Vector3[] Normals;
        public SpriteVertex[] Vertices;
        public Vector3 StoredHue;
        public Vector3 Vertex0YOffset;
        public Vector3 Vertex1YOffset;
        public Vector3 Vertex2YOffset;
        public Vector3 Vertex3YOffset;
    }

    sealed class LandSystem
    {
        LandData[] landComponents;
        LandView[] landViews;
        int _numLands;

        public LandSystem()
        {
            landComponents = new LandData[100];
        }

        public void DisposeComponent(int Index)
        {

        }

        public void Update(double totalMS, double frameMS)
        {
            // Needs GO update first.
            // Debug.Assert(GameObjectSystem.CalledThisFrame);

            for (int i = 0; i < _numLands; i++)
            {

            }
        }

        public void Calculate(int x, int y, sbyte z)
        {
            //UpdateStretched(x, y, z);
        }

        public void UpdateZ(int index, int zTop, int zRight, int zBottom, sbyte currentZ)
        {
            if (landComponents[index].IsStretched)
            {
                int x = currentZ * 4 + 1;
                int y = zTop * 4;
                int w = zRight * 4 - x;
                int h = zBottom * 4 + 1 - y;
                landComponents[index].Rectangle = new Rectangle(x, y, w, h);

                if (Math.Abs(currentZ - zRight) <= Math.Abs(zBottom - zTop))
                {
                    landComponents[index].AverageZ = (sbyte)((currentZ + zRight) >> 1);
                }
                else
                {
                    landComponents[index].AverageZ = (sbyte)((zBottom + zTop) >> 1);
                }

                landComponents[index].MinZ = currentZ;

                if (zTop < landComponents[index].MinZ)
                {
                    landComponents[index].MinZ = (sbyte)zTop;
                }

                if (zRight < landComponents[index].MinZ)
                {
                    landComponents[index].MinZ = (sbyte)zRight;
                }

                if (zBottom < landComponents[index].MinZ)
                {
                    landComponents[index].MinZ = (sbyte)zBottom;
                }
            }
        }

        public int CalculateCurrentAverageZ(int index, int direction)
        {
            int result = GetDirectionZ(index, ((byte)(direction >> 1) + 1) & 3);

            if ((direction & 1) > 0)
                return result;

            return (result + GetDirectionZ(index, direction >> 1)) >> 1;
        }

        private int GetDirectionZ(int index, int direction)
        {
            switch (direction)
            {
                case 1:
                {
	                return landComponents[index].Rectangle.Bottom >> 2;
                }
                case 2:
                {
                    return landComponents[index].Rectangle.Right >> 2;
                }
                case 3:
                    return landComponents[index].Rectangle.Top >> 2;
                default:
                    return landComponents[index].Position.Z;
            }
        }
    }
}
