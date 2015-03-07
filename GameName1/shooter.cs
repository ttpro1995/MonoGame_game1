using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
namespace Player
{
    class shooter
    {
        // Animation representing the player
       // public Texture2D PlayerTexture;
        public GameName1.Animation PlayerAnimation;


        // Position of the Player relative to the upper left side of the screen

        public Vector2 Position;
        // State of the player
        public bool Active;
        // Amount of hit points that player has
        public int Health;
        // Get the width of the player ship
        public int Width
        {
            get { return PlayerAnimation.FrameHeight; }
        }
        // Get the height of the player ship
        public int Height
        {
            get { return PlayerAnimation.FrameWidth; }
        }
        public void Initialize( Vector2 position, GameName1.Animation animation)
        {
     
            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;
            // Set the player to be active
            Active = true;
            // Set the player health
            Health = 100;


            PlayerAnimation = animation;
            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;
            // Set the player to be active
            Active = true;
            // Set the player health
            Health = 100;
        }



        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }
    }
}
