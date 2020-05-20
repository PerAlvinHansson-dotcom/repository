﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        Texture2D background;

        Player player, player2;
        int pointsP1, pointsP2;

        Interface printText;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Interface interface1;
        List<Powerup> powerups;
        List<Texture2D> powerupTexture;
        int powerupTimer = 5000;

        int randomPower;

    

        Random rng = new Random();

        public static List<SoundEffect> soundeffects;

        public static Texture2D projectileTexture1;
        public static Texture2D projectileTexture2;
        public static Texture2D projectileTexture3;

        //Interface interface1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            soundeffects = new List<SoundEffect>();
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
            player = new Player(Content.Load<Texture2D>("ship"), new Vector2(100, 150), "a");
            player2 = new Player(Content.Load<Texture2D>("ship"), new Vector2(1820, 150), "b");
            background = Content.Load<Texture2D>("space2");
            // TODO: use this.Content to load your game content here
            powerups = new List<Powerup>();
            
            powerupTexture = new List<Texture2D>();

            powerupTexture.Add(Content.Load<Texture2D>("ball_1")); //Textur för speedup
            powerupTexture.Add(Content.Load<Texture2D>("ball_2")); //Textur för sköld

            //printText = new Interface(Content.Load<SpriteFont>("sCORE:"));

            printText = new Interface(Content.Load<SpriteFont>("Font1"));


            printText = new Interface(Content.Load<SpriteFont>("Font1"));





            projectileTexture1 = Content.Load<Texture2D>("projectile_1");
            projectileTexture2 = Content.Load<Texture2D>("projectile_2");
            projectileTexture3 = Content.Load<Texture2D>("projectile_3");


            soundeffects.Add(Content.Load<SoundEffect>("spacetheme"));
            soundeffects.Add(Content.Load<SoundEffect>("lasershoot"));
            soundeffects.Add(Content.Load<SoundEffect>("explosion"));

            soundeffects[0].Play();
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

            player.CheckHit(player2);
            player2.CheckHit(player);

            pointsP1 = player.points;
            pointsP2 = player2.points;

           

            if (powerupTimer <= 0) //Spawnar en ny powerup när det har gått 5 sekunder //Hugo
            {
                randomPower = (int)player.RandomPower();
                powerups.Add(new Powerup(powerupTexture[randomPower], new Vector2(rng.Next(80, 1840), rng.Next(80, 1000)), (PowerUps)randomPower));
                powerupTimer = rng.Next(4500, 7000);
            }

            foreach (Powerup power in powerups.ToArray()) //Går igenom varje power och kollar om spelaren åkt in i dem. //Hugo
            {
                power.Update();

                if (player.Intersects(power.Hitbox))
                {
                    player.PowerUp((PowerUps)randomPower);
                    powerups.Remove(power);
                }

                if (player2.Intersects(power.Hitbox))
                {
                    player2.PowerUp((PowerUps)randomPower);
                    powerups.Remove(power);
                }

                if (power.CheckIfInMiddle())
                {
                    powerups.Remove(power);
                }
            }

            player.Update(gameTime);
            player2.Update(gameTime);
            
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
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
            foreach (Powerup power in powerups)
            {
                power.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            base.Draw(gameTime);

            printText.Print("Score: " + pointsP1, spriteBatch, 2, 2);
            printText.Print("Score: " + pointsP2, spriteBatch, 1730, 2);
            //printText.Print("Score:", spriteBatch, 2, 2);
            spriteBatch.End();
        }


    }
}
