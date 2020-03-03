﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace spacewar
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Player player;
        Interface printText;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Powerup> powerups;
        Interface interface1;
        Texture2D powerupTexture;

        Texture2D projectileTexture1;

        public Game1()
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
            player = new Player(Content.Load<Texture2D>("ship"), new Vector2(10, 150));

            // TODO: use this.Content to load your game content here
            powerups = new List<Powerup>();

            powerupTexture = Content.Load<Texture2D>("ball_1");
            //printText = new Interface(Content.Load<SpriteFont>("sCORE:"));

            projectileTexture1 = Content.Load<Texture2D>("projectile_1");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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

            for (int i = 0; i < 5; i++)
            {
                powerups.Add(new Powerup(powerupTexture, new Vector2(0, 0)));
            }

            foreach (Powerup power in powerups)
            {
                power.Update();
            }

            player.Update();
            base.Update(gameTime);
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
            foreach (Powerup power in powerups)
            {
                power.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            base.Draw(gameTime);
            //printText.Print("tEST", spriteBatch, 2, 2);
            spriteBatch.End();
        }
    }
}
