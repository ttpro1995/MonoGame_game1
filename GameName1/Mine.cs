using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Enemies
{
    class Mine
    {
        Texture2D mine_texture;
        public Vector2 Position;
        float movingSpeed;
        public bool Active;
        public int Width
        {
            get { return mine_texture.Width; }
        }
        public int Height
        {
            get { return mine_texture.Height; }
        }

        public void Initializer(Texture2D texture, Vector2 position){
            mine_texture = texture;
            Position = position;
            Active = true;
            movingSpeed = 6.0f;
        }

        public void Draw(SpriteBatch spiteBatch) {
            spiteBatch.Draw(mine_texture, Position, Color.White);
        }

        public void Update(GameTime gameTime) {
            Position.X -= movingSpeed;
            if (Position.X < 0)
            {
                Active = false;
            }
        }

    }
}
