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
        

        public Player(Texture2D texture, Vector2 startPosition) : base(texture, startPosition)
        {
            projectiles = new List<Projectile>();
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
          
        }

       
        void alvinssmartametod()
        {
            harTryckt = !harTryckt;
        }

    

        public new void Update()
        {
            newstate = Keyboard.GetState();
            if (harTryckt == false)
            {
                velocity.Y = 0;
                velocity.X = 0;
            }
            harTryckt = false;
            position += velocity;

            KeyboardState state = Keyboard.GetState();


            if (nextGenExperience == true)
            {
                Thread.Sleep(30);
            }

            if (newstate.IsKeyDown(Keys.L) && oldstate.IsKeyUp(Keys.L))
            {
                nextGenExperience = !nextGenExperience;
            }


            if (state.IsKeyDown(Keys.Down))
            {
                float rotationshastighetX = -(float)Math.Cos((double)angle); //Konverterar till double och hittar cos för vinkeln, konvertar sedan till float för att det ska kunna tillämpas på velocity
                float rotationshastighetY = -(float)Math.Sin((double)angle); 
                velocity.X = rotationshastighetX * speed;
                velocity.Y = rotationshastighetY * speed;
                alvinssmartametod();
            }


            if (state.IsKeyDown(Keys.Up))
            {
                float rotationshastighetX = (float)Math.Cos((double)angle);
                float rotationshastighetY = (float)Math.Sin((double)angle);
                velocity.X = rotationshastighetX * speed;
                velocity.Y = rotationshastighetY * speed;
                alvinssmartametod();
            }

            if ((state.IsKeyDown(Keys.Up) && (state.IsKeyDown(Keys.Left))))
            {
                float rotationshastighetX = (float)Math.Cos((double)angle);
                float rotationshastighetY = (float)Math.Sin((double)angle);
                velocity.X = rotationshastighetX * speed;
                velocity.Y = rotationshastighetY * speed;
                angle -= angleChange;
                alvinssmartametod();
            }

            if ((state.IsKeyDown(Keys.Up) && (state.IsKeyDown(Keys.Right))))
            {
                float rotationshastighetX = (float)Math.Cos((double)angle);
                float rotationshastighetY = (float)Math.Sin((double)angle);
                velocity.X = rotationshastighetX * speed;
                velocity.Y = rotationshastighetY * speed;
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
                projectiles.Add(new Projectile(Game1.projectileTexture1, new Vector2(50, 150)));
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update();
            }

            oldstate = newstate;


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

        public void SpeedUp()
        {
            speed *= 1.2f;
            angleChange *= 1.05f;
        }

        public void PowerUp(int i)
        {
            if(i == 0)
            {
                SpeedUp();
            }
        }
    }
}
