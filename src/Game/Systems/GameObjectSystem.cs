using System;

using Microsoft.Xna.Framework;

using ClassicUO.Game.Map;
using ClassicUO.Input;
using ClassicUO.Renderer;
using ClassicUO.Game;

namespace ClassicUO.Systems
{
    struct GameObjectDataComponent
    {
        public Position Position;
        public Vector3 Offset;
        public Tile Tile;
        //public GameObjectDataComponent Left;
        //public GameObjectDataComponent Right;
        public Vector3 ScreenPosition;
        public Vector3 RealScreenPosition;
        public bool IsPositionChanged;
        public Hue Hue;
        public Graphic Graphic;
        public sbyte AnimIndex;
        int CurrentRenderIndex;
        public short PriorityZ;
        public int Distance;

    }

    struct GameObjectView
    {
        public Vector3          StoredHue;
        public Rectangle        Bounds;
        public Rectangle        FrameInfo;
        public bool             HasShadow;
        public bool             IsFlipped;
        public bool             IsSelected;
        public float            Rotation;
        public Vector3          HueVector;
        public bool             AllowedToDraw;
        public SpriteTexture    Texture;
        public Vector3[]        Normals;
        public SpriteVertex[]   Vertices;
        public Vector3          Vertex0YOffset;
        public Vector3          Vertex1YOffset;
        public Vector3          Vertex2YOffset;
        public Vector3          Vertex3YOffset;
        public byte             AlphaHue;
        public bool             DrawTransparent;
    }

    sealed class GameObjectSystem
    {
        GameObjectView[] gameObjectViews;
        GameObjectDataComponent[] data;
        int _numComponents;

        public GameObjectSystem(int size)
        {
            gameObjectViews = new GameObjectView[size];
            data = new GameObjectDataComponent[size];

            _numComponents = 0;
        }

        public void AddNew()
        {
            _numComponents++;
        }

        public void Draw(Batcher2D batcher, Vector3 position, MouseOverList list)
        {
            for(int i = 0; i < _numComponents; i++)
            {
                gameObjectViews[i].Texture.Ticks = Engine.Ticks;

                SpriteVertex[]  vertex;
                Rectangle       Bounds = gameObjectViews[i].Bounds;
                float           Rotation = gameObjectViews[i].Rotation;

                if (Rotation != 0.0f)
                {

                    float w = Bounds.Width / 2f;
                    float h = Bounds.Height / 2f;
                    Vector3 center = position - new Vector3(Bounds.X - 44 + w, Bounds.Y + h, 0);
                    float sinx = (float)Math.Sin(Rotation) * w;
                    float cosx = (float)Math.Cos(Rotation) * w;
                    float siny = (float)Math.Sin(Rotation) * h;
                    float cosy = (float)Math.Cos(Rotation) * h;

                    vertex = SpriteVertex.PolyBufferFlipped;
                    vertex[0].Position = center;
                    vertex[0].Position.X += cosx - -siny;
                    vertex[0].Position.Y -= sinx + -cosy;
                    vertex[1].Position = center;
                    vertex[1].Position.X += cosx - siny;
                    vertex[1].Position.Y += -sinx + -cosy;
                    vertex[2].Position = center;
                    vertex[2].Position.X += -cosx - -siny;
                    vertex[2].Position.Y += sinx + cosy;
                    vertex[3].Position = center;
                    vertex[3].Position.X += -cosx - siny;
                    vertex[3].Position.Y += sinx + -cosy;
                }
                else if (gameObjectViews[i].IsFlipped)
                {
                    vertex = SpriteVertex.PolyBufferFlipped;
                    vertex[0].Position = position;
                    vertex[0].Position.X += Bounds.X + 44f;
                    vertex[0].Position.Y -= Bounds.Y;
                    vertex[0].TextureCoordinate.Y = 0;
                    vertex[1].Position = vertex[0].Position;
                    vertex[1].Position.Y += Bounds.Height;
                    vertex[2].Position = vertex[0].Position;
                    vertex[2].Position.X -= Bounds.Width;
                    vertex[2].TextureCoordinate.Y = 0;
                    vertex[3].Position = vertex[1].Position;
                    vertex[3].Position.X -= Bounds.Width;
                }
                else
                {
                    vertex = SpriteVertex.PolyBuffer;
                    vertex[0].Position = position;
                    vertex[0].Position.X -= Bounds.X;
                    vertex[0].Position.Y -= Bounds.Y;
                    vertex[0].TextureCoordinate.Y = 0;
                    vertex[1].Position = vertex[0].Position;
                    vertex[1].Position.X += Bounds.Width;
                    vertex[1].TextureCoordinate.Y = 0;
                    vertex[2].Position = vertex[0].Position;
                    vertex[2].Position.Y += Bounds.Height;
                    vertex[3].Position = vertex[1].Position;
                    vertex[3].Position.Y += Bounds.Height;
                }

                if (gameObjectViews[i].DrawTransparent)
                {
                    int dist = data[i].Distance;
                    int maxDist = Engine.Profile.Current.CircleOfTransparencyRadius + 1;

                    if (dist <= maxDist)
                        gameObjectViews[i].HueVector.Z = 1f - (dist / (float)maxDist);
                    else
                        gameObjectViews[i].HueVector.Z = 1f - gameObjectViews[i].AlphaHue / 255f;
                }
                else
                    gameObjectViews[i].HueVector.Z = 1f - gameObjectViews[i].AlphaHue / 255f;

                if (Engine.Profile.Current.HighlightGameObjects)
                {
                    if (gameObjectViews[i].IsSelected)
                    {
                        if (gameObjectViews[i].StoredHue == Vector3.Zero)
                        {
                            gameObjectViews[i].StoredHue = gameObjectViews[i].StoredHue;
                        }
                        gameObjectViews[i].HueVector = ShaderHuesTraslator.SelectedHue;
                    }
                    else if (gameObjectViews[i].StoredHue != Vector3.Zero)
                    {
                        gameObjectViews[i].HueVector = gameObjectViews[i].StoredHue;
                        gameObjectViews[i].StoredHue = Vector3.Zero;
                    }
                }

                if (vertex[0].Hue != gameObjectViews[i].HueVector)
                {
                    vertex[0].Hue = vertex[1].Hue = vertex[2].Hue = vertex[3].Hue = gameObjectViews[i].HueVector;
                }

                if (!batcher.DrawSprite(gameObjectViews[i].Texture, gameObjectViews[i].Vertices))
                {
                    continue;
                }

                //MousePick(list, gameObjectDrawComponents[i].Vertices);
            }
        }

        public void Update(double totalMS, double frameMS)
        {

        }

        public int Distance(Position position)
        {
            throw new NotImplementedException();
            return 0;
            //if (World.Player.IsMoving && this != World.Player)
            //{
            //    Mobile.Step step = World.Player.Steps.Back();

            //    return position.DistanceTo(step.X, step.Y);
            //}

            //return position.DistanceTo(World.Player.Position);
        }
    }
}
