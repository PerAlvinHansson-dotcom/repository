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
        List<Projectile> projectiles;
        float angle;
        float angleChange = 0.04f;
        Vector2 origin;
        bool nextGenExperience = false;

        Vector2 startPosition;
        float startSpeed = 5;

        bool isAlive = true;
        bool shield = false;

        Random rng = new Random();

        //texturer och strings för vapen
        Texture2D pTexture;
        string weapon = "laser";

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

        public new void Update()
        {
            newstate = Keyboard.GetState();

            if (!isAlive)
            {
                position = startPosition;
                speed = startSpeed;

                isAlive = true;
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

            //välj vapen
            if (newstate.IsKeyDown(Keys.Q) && oldstate.IsKeyUp(Keys.Q))
            {
                weapon = "laser";
            }
            if (newstate.IsKeyDown(Keys.W) && oldstate.IsKeyUp(Keys.W))
            {
                weapon = "coilgun";
            }
            if (newstate.IsKeyDown(Keys.E) && oldstate.IsKeyUp(Keys.E))
            {
                weapon = "plasma";
            }

            //skjut och skapa projectile
            if (newstate.IsKeyDown(Keys.RightControl) && oldstate.IsKeyUp(Keys.RightControl))
            {
                if (weapon == "laser")
                {
                    pTexture = Game1.projectileTexture1;
                    projectiles.Add(new Projectile(pTexture, position, angle, origin, weapon));
                }
                if (weapon == "coilgun")
                {
                    pTexture = Game1.projectileTexture2;
                    projectiles.Add(new Projectile(pTexture, position, angle, origin, weapon));
                }
                if (weapon == "plasma")
                {
                    pTexture = Game1.projectileTexture3;
                    projectiles.Add(new Projectile(pTexture, position, angle, origin, weapon));
                }

            }
            foreach (Projectile projectile in projectiles.ToArray())
            {
                projectile.Update();

                /*if (Intersects(projectile.Hitbox))
                {
                    if (shield)
                    {
                        shield = false;
                    }
                    else
                    {
                        isAlive = false;
                    }
                    projectiles.Remove(projectile);
                }*/
            }

            oldstate = newstate;

            if (speed > 10)
            {
                speed = 10;
            }

            if (angleChange > 0.07f)
            {
                angleChange = 0.07f;
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, angle, origin, 1, SpriteEffects.None, 1);
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        public bool Intersects(Rectangle otherObject)
        {
            return Hitbox.Intersects(otherObject);
        }

        public PowerUps RandomPower() //Skickar en random powerup när man skapar/krockar med en powerup
        {
            Array values = Enum.GetValues(typeof(PowerUps));
            PowerUps randomPower = (PowerUps)values.GetValue(rng.Next(values.Length));

            return randomPower;
        }

        public void SpeedUp() //Mer speed
        {
            speed *= 1.15f;
            angleChange *= 1.02f;
        }

        public void PowerUp(PowerUps powerUps)
        {
            if(powerUps == PowerUps.Speed)
            {
                SpeedUp();
            }
            else if(powerUps == PowerUps.shield) //Kollar om det finns en sköld eller ej
            {
                shield = true;
            }
        }
    }
}
