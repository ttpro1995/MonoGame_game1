﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Player;
#endregion

namespace GameName1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        //Mouse states used to track Mouse button press
        MouseState currentMouseState;
        MouseState previousMouseState;
        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;
        // A movement speed for the player
        float playerMoveSpeed;
 
        //Enemies here 
        Enemies.Mine enemy_mine;
        List<Enemies.Mine> enemies_list;
        TimeSpan prevSpawm;
        TimeSpan RespawnCD; // respawn cooldown
        Texture2D Mine_Texture;
        Texture2D Laser_Texture;
        //ammo here

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        

        //game object
        Player.shooter TTpro;
   

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            TTpro = new shooter();
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

      
           

     
            // Set a constant player move speed
            playerMoveSpeed = 8.0f;
        
            //init enemies list 
            enemies_list = new List<Enemies.Mine> ();

            //
            prevSpawm = TimeSpan.Zero;
            RespawnCD = TimeSpan.FromSeconds(1.0f);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load the player resources

            Animation playerAnimation = new Animation();

            Texture2D playerTexture = Content.Load<Texture2D>("Graphics\\shipAnimation");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,GraphicsDevice.Viewport.TitleSafeArea.Y+ GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            TTpro.Initialize(playerPosition, playerAnimation);
            Laser_Texture = Content.Load<Texture2D>("Graphics\\laser");


            Mine_Texture = Content.Load<Texture2D>("Graphics\\mine");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            TTpro.Update(gameTime);
            UpdatePlayer(gameTime);
            base.Update(gameTime);
        }
        
  
        
        private void UpdatePlayer(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();  // must 
            //left
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                TTpro.Position.X = TTpro.Position.X - playerMoveSpeed;
            }
            //right
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                TTpro.Position.X += playerMoveSpeed;
            }
            //up
            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                TTpro.Position.Y -= playerMoveSpeed;
            }
            //down
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                TTpro.Position.Y += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                TTpro.Position.Y += playerMoveSpeed;
            }

            TTpro.Position.X = MathHelper.Clamp(TTpro.Position.X, 0, GraphicsDevice.Viewport.Width);
            TTpro.Position.Y = MathHelper.Clamp(TTpro.Position.Y, 0, GraphicsDevice.Viewport.Height);
            //enemy update
            UpdateEnemies_list(gameTime);
            UpdateCollision();//air mine disappear when i hit
        }

        private void AddEnemies()
        {
            Random random = new Random();
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width ,random.Next(100,GraphicsDevice.Viewport.Height-100)) ;


            Enemies.Mine mine = new Enemies.Mine();
            mine.Initializer(Mine_Texture, position);
            enemies_list.Add(mine);

        }

        private void RemoveEnemies()
        {
            for (int i = enemies_list.Count -1;i>=0;i--)
            {
                if (enemies_list[i].Active == false)
                    enemies_list.RemoveAt(i);
            }

        }

        private void UpdateEnemies_list(GameTime gameTime)
        {

            for (int i = 0; i < enemies_list.Count;i++)
            {
                enemies_list[i].Update(gameTime);
            }

                if (gameTime.TotalGameTime - prevSpawm >= RespawnCD)
                {
                    prevSpawm = gameTime.TotalGameTime;
                    AddEnemies();
                }
            RemoveEnemies();

        }

        private void UpdateCollision()
        {
            Rectangle player_rectangle = new Rectangle((int)TTpro.Position.X, (int)TTpro.Position.Y, (int)TTpro.Width, (int)TTpro.Height);
            for (int i=0;i<enemies_list.Count;i++)
            {
                Enemies.Mine tmp = enemies_list[i];
                Rectangle mine_range = new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Width, (int)tmp.Height);
                if (player_rectangle.Intersects(mine_range))
                {
                    //hit mine
                    tmp.Active = false;
                }
            }

        }

        private void DrawEnemies(SpriteBatch spriteBatch)
        {
            for (int i=0;i<enemies_list.Count;i++)
            {
                enemies_list[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            TTpro.Draw(spriteBatch);
            DrawEnemies(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
