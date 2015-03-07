using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameName1
{
    class explosion
    {
        Texture2D Texture;
        public Vector2 Position;
        float movingSpeed;
        public bool Active;
        public int Width
        {
            get { return Texture.Width; }
        }
        public int Height
        {
            get { return Texture.Height; }
        }
        TimeSpan Create;
        TimeSpan Life;
        public void Initializer(Texture2D texture, Vector2 position,TimeSpan create)
        {
            
            Texture = texture;
            Position = position;
            Active = true;
            movingSpeed = 8.0f;
            Create = create;
            Life = TimeSpan.FromSeconds(0.5f);
        }

        public void Draw(SpriteBatch spiteBatch)
        {
            spiteBatch.Draw(Texture, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - Create > Life)
                Active = false;
        }

    }
}
