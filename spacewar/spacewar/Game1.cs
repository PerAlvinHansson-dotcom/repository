using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

        //Interface interface1;
        List<Powerup> powerups;
        List<Texture2D> powerupTexture;
        int powerupTimer = 5000;

        int randomPower;

        public static Texture2D projectileTexture1;
        
        //Interface interface1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
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
            
            powerupTexture = new List<Texture2D>();

            powerupTexture.Add(Content.Load<Texture2D>("ball_1"));
            powerupTexture.Add(Content.Load<Texture2D>("ball_2"));

            //printText = new Interface(Content.Load<SpriteFont>("sCORE:"));
            printText = new Interface(Content.Load<SpriteFont>("Font1"));

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

            //int i = 0;

            if (powerupTimer <= 0)
            {
                randomPower = (int)player.RandomPower();
                powerups.Add(new Powerup(powerupTexture[randomPower], new Vector2(0, 0)));
                powerupTimer = 1000;
            }

            foreach (Powerup power in powerups.ToArray())
            {
                power.Update();

                if (player.Intersects(power.Hitbox))
                {
                    player.PowerUp((PowerUps)randomPower);
                    powerups.Remove(power);
                }
            }

            player.Update();

            powerupTimer -= gameTime.ElapsedGameTime.Milliseconds;
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
            printText.Print("Score:", spriteBatch, 2, 2);
            spriteBatch.End();
        }
    }
}
