#region license
//  Copyright (C) 2019 ClassicUO Development Community on Github
//
//	This project is an alternative client for the game Ultima Online.
//	The goal of this is to develop a lightweight client considering 
//	new technologies.  
//      
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
#endregion

using System;

using ClassicUO.Configuration;
using ClassicUO.Game.Data;
using ClassicUO.Game.GameObjects;
using ClassicUO.Input;
using ClassicUO.IO;
using ClassicUO.IO.Resources;
using ClassicUO.Renderer;
using ClassicUO.Utility;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MathHelper = Microsoft.Xna.Framework.MathHelper;

namespace ClassicUO.Game.GameObjects
{
    internal partial class Static
    {
        private readonly bool _isFoliage, _isPartialHue, _isTransparent;
        private readonly int _canBeTransparent;

        private Graphic _oldGraphic;

        public bool CharacterIsBehindFoliage { get; set; }


        public override bool TransparentTest(int z)
        {
            bool r = true;

            if (Z <= z - ItemData.Height)
                r = false;
            else if (z < Z && (_canBeTransparent & 0xFF) == 0)
                r = false;

            return r;
        }


        public override bool Draw(Batcher2D batcher, Vector3 position, MouseOverList objectList)
        {
            if (!AllowedToDraw || IsDisposed)
                return false;

            if (Texture == null || Texture.IsDisposed || _oldGraphic != Graphic)
            {
                _oldGraphic = Graphic;

                ArtTexture texture = FileManager.Art.GetTexture(Graphic);
                Texture = texture;
                Bounds = new Rectangle((Texture.Width >> 1) - 22, Texture.Height - 44, Texture.Width, Texture.Height);

                FrameInfo.Width = texture.ImageRectangle.Width;
                FrameInfo.Height = texture.ImageRectangle.Height;

                FrameInfo.X = (Texture.Width >> 1) - 22 - texture.ImageRectangle.X;
                FrameInfo.Y = Texture.Height - 44 - texture.ImageRectangle.Y;
            }

            if (_isFoliage)
            {
                if (CharacterIsBehindFoliage)
                {      
                    if (AlphaHue != 76)
                        ProcessAlpha(76);                    
                }
                else
                {
                    if (AlphaHue != 0xFF)
                        ProcessAlpha(0xFF);
                }
            }

            //int distance = Distance;

            //if (Engine.Profile.Current.UseCircleOfTransparency)
            //{
            //    int z = World.Player.Z + 5;

            //    bool r = true;

            //    if (!_isFoliage)
            //    {
            //        if (Z <= z - ItemData.Height)
            //            r = false;
            //        else if (z < Z && (_canBeTransparent & 0xFF) == 0)
            //            r = false;
            //    }

            //    if (r)
            //    {
            //        int distanceMax = Engine.Profile.Current.CircleOfTransparencyRadius;

            //        if (distance <= distanceMax)
            //        {
            //            if (distance <= 0)
            //                distance = 1;

            //            ProcessAlpha((byte) (235 - (200 / distance)));
            //        }
            //        else if (AlphaHue != 0xFF)
            //            ProcessAlpha(0xFF);
            //    }
            //}


            if (Engine.Profile.Current.NoColorObjectsOutOfRange && Distance > World.ViewRange)
                HueVector = new Vector3(0x038E, 1, HueVector.Z);
            else
                HueVector = ShaderHuesTraslator.GetHueVector(Hue, _isPartialHue, 0, false);
            MessageOverHead(batcher, position, Bounds.Y - 44);
            Engine.DebugInfo.StaticsRendered++;
            base.Draw(batcher, position, objectList);
            //if (_isFoliage)
            //{
            //    if (_texture == null)
            //    {
            //        _texture = new Texture2D(batcher.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            //        _texture.SetData(new Color[] {Color.Red});
            //    }

            //    batcher.DrawRectangle(_texture, new Rectangle( (int) position.X - FrameInfo.X, (int)position.Y - FrameInfo.Y, FrameInfo.Width, FrameInfo.Height), Vector3.Zero);
            //}

            return true;
        }

        //private static Texture2D _texture;


        protected override void MousePick(MouseOverList list, SpriteVertex[] vertex)
        {
            int x = list.MousePosition.X - (int) vertex[0].Position.X;
            int y = list.MousePosition.Y - (int) vertex[0].Position.Y;
            if (Texture.Contains(x, y))
                list.Add(this, vertex[0].Position);
        }
    }
}