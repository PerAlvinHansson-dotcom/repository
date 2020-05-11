using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;



namespace spacewar
{
    enum states
    {
        Menu,
        Playing,
        Gameover
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    public class Game1 : Game
    {
        Texture2D background;
        Player player, player2;
        Interface printText;
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteBatch spriteBatch1;
        List<Powerup> powerups;
        List<Texture2D> powerupTexture;
        int powerupTimer = 5000;
        //int hitboxscore = 0;
        public SoundEffect ExitingMenu;
        public Texture2D gameoverImage;
        public Texture2D texture;
        public Texture2D menuImage;

        int randomPower;

        Random rng = new Random();

        public static Texture2D projectileTexture1;
        public static Texture2D projectileTexture2;
        public static Texture2D projectileTexture3;

        states gameState = states.Menu;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            menuImage = null;
            ExitingMenu = null;
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
            menuImage = Content.Load<Texture2D>("menu");
            ExitingMenu = Content.Load<SoundEffect>("menustart");
            gameoverImage = Content.Load<Texture2D>("gameover");

            // TODO: use this.Content to load your game content here
            powerups = new List<Powerup>();

            powerupTexture = new List<Texture2D>();

            powerupTexture.Add(Content.Load<Texture2D>("ball_1")); //Textur för speedup
            powerupTexture.Add(Content.Load<Texture2D>("ball_2")); //Textur för sköld


            printText = new Interface(Content.Load<SpriteFont>("Font1"));



            projectileTexture1 = Content.Load<Texture2D>("projectile_1");
            projectileTexture2 = Content.Load<Texture2D>("projectile_2");
            projectileTexture3 = Content.Load<Texture2D>("projectile_3");
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

            //Game states
            switch (gameState)
            {

                case states.Playing:
                    {
                        // TODO: Add your update logic here

                        player.CheckHit(player2);
                        player2.CheckHit(player);

                        if (powerupTimer <= 0) //Spawnar en ny powerup när det har gått 5 sekunder //Hugo
                        {
                            randomPower = (int)player.RandomPower();
                            powerups.Add(new Powerup(powerupTexture[randomPower], new Vector2(rng.Next(80, 1840), rng.Next(80, 1000)), (PowerUps)randomPower));
                            powerupTimer = 5000;
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
                        }

                        player.Update(gameTime);
                        player2.Update(gameTime);

                        powerupTimer -= gameTime.ElapsedGameTime.Milliseconds;

                        

                        break;
                    }

                case states.Menu:
                    {
                        KeyboardState keyState = Keyboard.GetState();

                        if (keyState.IsKeyDown(Keys.Enter))
                        {
                            gameState = states.Playing;
                            ExitingMenu.Play();
                        }
                        break;
                    }

                case states.Gameover:
                    {

                        break;
                    }

            }

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

            switch (gameState)
            {
                case states.Playing:
                    {

                        spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
                        foreach (Powerup power in powerups)
                        {
                            power.Draw(spriteBatch);
                        }
                        
                        player.Draw(spriteBatch);
                        player2.Draw(spriteBatch);
                        base.Draw(gameTime);
                        printText.Print("Score P1:" + player.score, spriteBatch, 10, 2);
                        printText.Print(player2.score + ":Score P2", spriteBatch, 1660, 2);
                        break;
                    }

                case states.Menu:
                    {
                        spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                        break;
                    }

                case states.Gameover:
                    {
                        break;
                    }
            }
            spriteBatch.End();
        }
    }
}
