﻿using System;
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

        PowerUps pu = new PowerUps();

        Random rng = new Random();

        public Player(Texture2D texture, Vector2 startPosition) : base(texture, startPosition)
        {
            projectiles = new List<Projectile>();
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

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

            if (state.IsKeyDown(Keys.Space))
            {
                projectiles.Add(new Projectile(texture, position));
            }

            foreach (Projectile projectile in projectiles)
            {
                projectile.Update();
            }

            oldstate = newstate;

            if(speed > 10)
            {
                speed = 10;
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, angle, origin, 1, SpriteEffects.None, 1);
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
