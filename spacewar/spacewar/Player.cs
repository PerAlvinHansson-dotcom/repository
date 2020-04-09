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
        string config;
        //Vector2 mittpunkt = new Vector2(900, 540);

        Vector2 startPosition;
        float startSpeed = 5;
        bool isAlive = true;
        bool shield = false;
        int unKillableTimer = 500;

        PowerUps powerups = new PowerUps();

        Random rng = new Random();

        //texturer och strings för vapen
        Texture2D pTexture;
        string weapon = "laser";


        public Player(Texture2D texture, Vector2 startPosition, string config) : base(texture, startPosition)
        {
            projectiles = new List<Projectile>();
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            this.config = config;
            this.startPosition = startPosition;
        }


       
        void alvinssmartametod()
        {
            harTryckt = !harTryckt;
        }

        public void IsAlive()
        {

        }

        public new void Update()
        {
            //double distanceX = Math.Pow(mittpunkt.X - position.X, 2); //Använd avståndet till mittpunkten på något sätt för att skapa dragkraft
            //double distanceY = Math.Pow(mittpunkt.Y - position.Y, 2);

            if (!isAlive) //Kollar om du dog förra updaten
            {
                Respawn();
            }


            newstate = Keyboard.GetState();

            if (harTryckt == false)
            {
                velocity.Y = 0;
                velocity.X = 0;
            }


            harTryckt = false;
            position += velocity;

            KeyboardState state = Keyboard.GetState();

            bool right = state.IsKeyUp(Keys.Right);
            bool left = state.IsKeyUp(Keys.Left);
            bool up = state.IsKeyUp(Keys.Up);
            bool down = state.IsKeyUp(Keys.Down);
            bool shoot = newstate.IsKeyUp(Keys.RightControl) && oldstate.IsKeyDown(Keys.RightControl);

            switch (config)
            {
                case "a":
                    break;

                case "b":
                    right = state.IsKeyUp(Keys.D);
                    left = state.IsKeyUp(Keys.A);
                    up = state.IsKeyUp(Keys.W);
                    down = state.IsKeyUp(Keys.S);
                    shoot = newstate.IsKeyUp(Keys.LeftControl) && oldstate.IsKeyDown(Keys.LeftControl);
                    break;
            }

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

            if (down)
            {
                float hastighetX = (float)Math.Cos((double)angle); //Konverterar till double och hittar cos för vinkeln, konvertar sedan till float för att det ska kunna tillämpas på velocity
                float hastighetY = (float)Math.Sin((double)angle); 
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                alvinssmartametod();
            }


            if (up)
            {
                float hastighetX  = -(float)Math.Cos((double)angle);
                float hastighetY = -(float)Math.Sin((double)angle);
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                alvinssmartametod();
            }

            if (down && right)
            {
                float hastighetX  = (float)Math.Cos((double)angle);
                float hastighetY = (float)Math.Sin((double)angle);
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                angle -= angleChange;
                alvinssmartametod();
            }

            if (down && left)
            {
                float hastighetX  = (float)Math.Cos((double)angle);
                float hastighetY = (float)Math.Sin((double)angle);
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                angle += angleChange;
                alvinssmartametod();
            }

            if (right)
            {
                angle -= angleChange;
                alvinssmartametod();
            }
      
            if (left)
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
            if (shoot)
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

            unKillableTimer -= 10;
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
            if(powerups == PowerUps.Speed)
            {
                SpeedUp();
            }
            else if (powerUps == PowerUps.Shield) //Kollar om det finns en sköld eller ej
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
