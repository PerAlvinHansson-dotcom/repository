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

        string config;

        //Vector2 mittpunkt = new Vector2(900, 540);


        PowerUps pu = new PowerUps();

        Random rng = new Random();

        public Player(Texture2D texture, Vector2 startPosition, string config) : base(texture, startPosition)
        {
            projectiles = new List<Projectile>();
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            this.config = config;

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

            switch (config)
            {
                case "a":
                    break;

                case "b":
                    right = state.IsKeyUp(Keys.D);
                    left = state.IsKeyUp(Keys.A);
                    up = state.IsKeyUp(Keys.W);
                    down = state.IsKeyUp(Keys.S);
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

            //skjut och skapa projectile
            if (newstate.IsKeyDown(Keys.RightControl) && oldstate.IsKeyUp(Keys.RightControl))
            {
                projectiles.Add(new Projectile(Game1.projectileTexture1, position, angle, origin));
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update();
            }

            oldstate = newstate;

            if (speed > 10)
            {
                speed = 10;
            }

            if (angleChange > 0.1f)
            {
                angleChange = 0.1f;
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

        public PowerUps RandomPower()
        {
            Array values = Enum.GetValues(typeof(PowerUps));
            PowerUps randomPower = (PowerUps)values.GetValue(rng.Next(values.Length));

            return randomPower;
        }

        public void SpeedUp()
        {
            speed *= 1.15f;
            angleChange *= 1.05f;
        }

        public void PowerUp(PowerUps powerUps)
        {
            if(pu == PowerUps.Speed)
            {
                SpeedUp();
            }
        }
    }
}
