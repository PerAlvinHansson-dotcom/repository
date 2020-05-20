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

        //Jag har gjort styrning för player, gravitation och grejer relaterade till skärmen(bakgrundsbild och väggar). Jag har även utvecklat delar av game1 och större delen av gameobject. 
        //Alvin

        public bool harTryckt;
        float speed = 3;
        public List<Projectile> projectiles;
        float angle;
        float angleChange = 0.04f;
        Vector2 origin;
        bool nextGenExperience = false;
        string config;

        public int points;
        public int wins;


        Vector2 startPosition;
        float startSpeed = 3;
        public bool isAlive = true;
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
       

        public new void Update(GameTime gameTime)
        {
          

            if (!isAlive) //Kollar om du dog förra updaten
            {
                Respawn();
            }


            newstate = Keyboard.GetState();

            if (harTryckt == false) //Så länge knappen inte är nedtryckt kommer inte player att ha någon hastighet bortsett från gravitationen
                //Alvin
            {
                velocity.X *= 0.98f;
                velocity.Y *= 0.98f;
            
            }


            base.Update();
            harTryckt = false;

            KeyboardState state = Keyboard.GetState();

            bool right = state.IsKeyDown(Keys.Right);
            bool left = state.IsKeyDown(Keys.Left);
            bool up = state.IsKeyDown(Keys.Up);
            bool down = state.IsKeyDown(Keys.Down);
            bool shoot = newstate.IsKeyUp(Keys.RightControl) && oldstate.IsKeyDown(Keys.RightControl);
            bool change1 = newstate.IsKeyUp(Keys.U) && oldstate.IsKeyDown(Keys.U);
            bool change2 = newstate.IsKeyUp(Keys.I) && oldstate.IsKeyDown(Keys.I);
            bool change3 = newstate.IsKeyUp(Keys.O) && oldstate.IsKeyDown(Keys.O);

            switch (config)
            {
                case "a": //Anges kontrollkonfigurationen "a" bryter den ur switch och använder standardkontrollerna.
                    //Alvin
                    break;

                case "b":
                    right = state.IsKeyDown(Keys.D);
                    left = state.IsKeyDown(Keys.A);
                    up = state.IsKeyDown(Keys.W);
                    down = state.IsKeyDown(Keys.S);
                    shoot = newstate.IsKeyUp(Keys.LeftControl) && oldstate.IsKeyDown(Keys.LeftControl);
                    change1 = newstate.IsKeyUp(Keys.Z) && oldstate.IsKeyDown(Keys.Z);
                    change2 = newstate.IsKeyUp(Keys.X) && oldstate.IsKeyDown(Keys.X);
                    change3 = newstate.IsKeyUp(Keys.C) && oldstate.IsKeyDown(Keys.C);
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
                float hastighetX = -(float)Math.Cos((double)angle); //Konverterar till double och hittar cos för vinkeln, konvertar sedan till float för att det ska kunna tillämpas på velocity
                float hastighetY = -(float)Math.Sin((double)angle); 
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                harTryckt = !harTryckt; //Anger att knappen har tryckts som sant, då booleanvärdet harTryckt återställs till false efter varje uppdatering.
                //Alvin
            }


            if (up)
            {
                float hastighetX  = (float)Math.Cos((double)angle);
                float hastighetY = (float)Math.Sin((double)angle);
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                harTryckt = !harTryckt;
            }

            if (up && right)
            {
                float hastighetX  = (float)Math.Cos((double)angle);
                float hastighetY = (float)Math.Sin((double)angle);
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                angle += angleChange;
                harTryckt = !harTryckt;
            }

            if (up && left)
            {
                float hastighetX  = (float)Math.Cos((double)angle);
                float hastighetY = (float)Math.Sin((double)angle);
                velocity.X = hastighetX  * speed;
                velocity.Y = hastighetY * speed;
                angle -= angleChange;
                harTryckt = !harTryckt;
            }

            if (right)
            {
                angle += angleChange;
                harTryckt = !harTryckt;
                velocity.X *= 0.99f;
                velocity.Y *= 0.99f;

            }
      
            if (left)
            {
                angle -= angleChange;
                harTryckt = !harTryckt;
                velocity.X *= 0.99f;
                velocity.Y *= 0.90f;
            }

            //sebastian
            //välj vapen
            if (change1)
            {
                weapon = "laser";
            }
            if (change2)
            {
                weapon = "coilgun";
            }
            if (change3)
            {
                weapon = "plasma";
            }

            //sebastian
            //skjut och skapa projectile med valt vapen
            if (shoot)
            {
                if (weapon == "laser")
                {
                    pTexture = Game1.projectileTexture1;
                    projectiles.Add(new Projectile(pTexture, position, angle, origin, weapon));

                    Game1.soundeffects[1].Play();
                }
                if (weapon == "coilgun")
                {
                    pTexture = Game1.projectileTexture2;                  
                    projectiles.Add(new Projectile(pTexture, position, angle, origin, weapon));

                    Game1.soundeffects[2].Play();

                }
                if (weapon == "plasma")
                {
                    
                    pTexture = Game1.projectileTexture3;
                    projectiles.Add(new Projectile(pTexture, position, angle, origin, weapon));

                    Game1.soundeffects[3].Play();

                }
            }

            foreach (Projectile projectile in projectiles)
            {
                projectile.Update();
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
            else if (powerUps == PowerUps.Shield)
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

        public void CheckHit(Player enemy) //Hugo
        {
            foreach (Projectile projectile in projectiles.ToArray())
            {
                if (enemy.isAlive == true)
                {
                    if (projectile.Intersects(enemy.Hitbox))//Kollar igenom alla skotten för att se om de har träffat motståndaren
                    {
                        projectiles.Remove(projectile);
                        if (!enemy.CheckIfKillable())
                        {
                            Random r = new Random();
                            Game1.explosions[r.Next(0, 4)].Play();
                            points += 1;
                            enemy.Respawn();
                        }
                        else
                        {
                            enemy.isAlive = true;
                        }
                    }
                    else
                    {
                        enemy.isAlive = true;
                    }
                }
            }
        }

        public bool CheckIfKillable() //Kollar om det går att död eller inte//Hugo
        {
            if (unKillableTimer < 0)
            {
                if (shield) //Kollar om det finns en sköld
                {
                    shield = false;
                    unKillableTimer = 500;
                    return true;
                }
                else
                {
                    unKillableTimer = 500;
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
