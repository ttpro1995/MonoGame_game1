using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
namespace Ammo
{
    class Laser
    {
        Texture2D laser_texture;
        public Vector2 Position;
        float movingSpeed;
        public bool Active;
        public int Width
        {
            get { return laser_texture.Width; }
        }
        public int Height
        {
            get { return laser_texture.Height; }
        }

        public void Initializer(Texture2D texture, Vector2 position)
        {
            laser_texture = texture;
            Position = position;
            Active = true;
            movingSpeed = 8.0f;
        }

        public void Draw(SpriteBatch spiteBatch)
        {
            spiteBatch.Draw(laser_texture, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Position.X += movingSpeed;
            if (Position.X < 0)
            {
                Active = false;
            }
        }

       
    }
}

