using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace spacewar
{
    class Player : GameObject
    {

        KeyboardState oldstate;
        KeyboardState newstate;

        public bool harTryckt;
        public float speed = 5;
        public List<Projectile> projectiles;
        float angle;
        float angleChange = 0.04f;
        Vector2 origin;
        bool nextGenExperience = false;

        Vector2 startPosition;
        float startSpeed = 5;

        bool isAlive = true;
        bool shield = false;

        int unKillableTimer = 500;

        Random rng = new Random();

        public Player(Texture2D texture, Vector2 startPosition) : base(texture, startPosition)
        {
            projectiles = new List<Projectile>();
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

            this.startPosition = startPosition;
        }

       
        void alvinssmartametod()
        {
            harTryckt = !harTryckt;
        }

        public new void Update(GameTime gameTime)
        {
            newstate = Keyboard.GetState();

            if (!isAlive) //Kollar om du dog förra updaten
            {
                Respawn();
            }

            if (harTryckt == false)
            {
                velocity.Y = 0;
                velocity.X = 0;
            }
            harTryckt = false;
            position += velocity;

            KeyboardState state = Keyboard.GetState();
            bool right = state.IsKeyDown(Keys.Right);
            bool left = state.IsKeyDown(Keys.Left);
            bool up = state.IsKeyDown(Keys.Up);
            bool down = state.IsKeyDown(Keys.Down);

            if (position.X > 1920)
            {
                position.X = 1920;
            }

            if (position.X < 0)
            {
                position.X = 0;
            }

            if (position.Y > 1080)
            {
                position.Y = 1080;
            }

            if (position.Y < 0)
            {
                position.Y = 0; 
            }

            if (nextGenExperience == true)
            {
                Thread.Sleep(30);
            }

            if (newstate.IsKeyDown(Keys.L) && oldstate.IsKeyUp(Keys.L))
            {
                nextGenExperience = !nextGenExperience;
            }

            bool bajs = true;

            switch (bajs)
            {
                
            }


            if (down)
            {
                float hastighetX = -(float)Math.Cos((double)angle); //Konverterar till double och hittar cos för vinkeln, konvertar sedan till float för att det ska kunna tillämpas på velocity
                float hastighetY = -(float)Math.Sin((double)angle); 
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                alvinssmartametod();
            }


            if (up)
            {
                float hastighetX  = (float)Math.Cos((double)angle);
                float hastighetY = (float)Math.Sin((double)angle);
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                alvinssmartametod();
            }

            if ((up && (left)))
            {
                float hastighetX  = (float)Math.Cos((double)angle);
                float hastighetY = (float)Math.Sin((double)angle);
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                angle -= angleChange;
                alvinssmartametod();
            }

            if ((up && (right)))
            {
                float hastighetX  = (float)Math.Cos((double)angle);
                float hastighetY = (float)Math.Sin((double)angle);
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                angle += angleChange;
                alvinssmartametod();
            }

            if (state.IsKeyDown(Keys.Left))
            {
                angle -= angleChange;
                alvinssmartametod();
            }
      
            if (state.IsKeyDown(Keys.Right))
            {
                angle += angleChange;
                alvinssmartametod();
            }

            //skjut och skapa projectile
            if (newstate.IsKeyDown(Keys.RightControl) && oldstate.IsKeyUp(Keys.RightControl))
            {
                projectiles.Add(new Projectile(Game1.projectileTexture1, position, angle, origin, "laser"));
            }

            foreach (Projectile projectile in projectiles.ToArray())
            {
                projectile.Update();

                if (Intersects(projectile.Hitbox))//Kollar igenom alla skotten för att se om de har träffat något //Hugo
                {
                    if (unKillableTimer < 0)
                    {
                        if (shield)//Kollar om det finns en sköld eller inte
                        {
                            shield = false;
                        }
                        else
                        {
                            isAlive = false;
                        }
                        unKillableTimer = 500;
                    }
                    //projectiles.Remove(projectile);
                }
            }

            oldstate = newstate;

            if (speed > 10) //Hugo
            {
                speed = 10;
            }

            if (angleChange > 0.07f) //Hugo
            {
                angleChange = 0.07f;
            }

            unKillableTimer -= gameTime.ElapsedGameTime.Milliseconds;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, angle, origin, 1, SpriteEffects.None, 1);
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        public bool Intersects(Rectangle otherObject) //Kollar om man krockar
        {
            return Hitbox.Intersects(otherObject);
        }

        public PowerUps RandomPower() //Skickar en random power när man skapar en powerup //Hugo
        {
            Array values = Enum.GetValues(typeof(PowerUps));
            PowerUps randomPower = (PowerUps)values.GetValue(rng.Next(values.Length));

            return randomPower;
        }

        public void SpeedUp() //Större hastighet //Hugo
        {
            speed *= 1.15f;
            angleChange *= 1.02f;
        }

        public void PowerUp(PowerUps powerUps) //Hugo
        {
            if(powerUps == PowerUps.Speed)
            {
                SpeedUp();
            }
            else if(powerUps == PowerUps.Shield) //Kollar om det finns en sköld eller ej
            {
                shield = true;
            }
        }

        public void Respawn() //Nollställer dina stats och skickar tillbacka dig till start positionen //Hugo
        {
            position = startPosition;
            speed = startSpeed;

            isAlive = true;
        }
    }
}
