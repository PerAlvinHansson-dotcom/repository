using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar
{
    class Player : GameObject
    {
        public bool harTryckt;
        public int speed = 6;
        List<Projectile> projectiles;
        float angle;
        Vector2 origin;
        



        //var rotationorigin = new Vector2(texture.Width / 2f, texture.Height / 2f);

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
            if(harTryckt == false)
            {
                velocity.Y = 0;
                velocity.X = 0;
            }
            harTryckt = false;
            position += velocity;

            KeyboardState state = Keyboard.GetState();

            /*if (state.IsKeyDown(Keys.Down) & state.IsKeyDown(Keys.Right))
            {
                velocity.Y = 6;
                velocity.X = 6;
                alvinssmartametod();
            }*/


     
            
            if (state.IsKeyDown(Keys.Down))
            {
                double rotationshastighetX = Math.Cos((double)angle); //Konverterar till double och hittar cos för vinkeln
                double rotationshastighetY = Math.Sin((double)angle); //Konverterar till double och hittar sin för vinkeln
                float hastighetplusX = -(float)rotationshastighetX; //Konverterar till float för att det ska kunna tillämpas på velocity
                float hastighetplusY = -(float)rotationshastighetY;
                velocity.X = hastighetplusX * 3;
                velocity.Y = hastighetplusY * 3;
                alvinssmartametod();
            }

            if (state.IsKeyDown(Keys.Up))
            {
                double rotationshastighetX = Math.Cos((double)angle); 
                double rotationshastighetY = Math.Sin((double)angle); 
                float hastighetplusX = (float)rotationshastighetX; 
                float hastighetplusY = (float)rotationshastighetY; 
                velocity.X = hastighetplusX * 3;
                velocity.Y = hastighetplusY * 3;
                alvinssmartametod();
            }

            if ((state.IsKeyDown(Keys.Up) && (state.IsKeyDown(Keys.Left))))
            {
                double rotationshastighetX = Math.Cos((double)angle);
                double rotationshastighetY = Math.Sin((double)angle);
                float hastighetplusX = (float)rotationshastighetX;
                float hastighetplusY = (float)rotationshastighetY;
                velocity.X = hastighetplusX * 5;
                velocity.Y = hastighetplusY * 5;
                angle -= 0.02f;
                alvinssmartametod();
            }

            if ((state.IsKeyDown(Keys.Up) && (state.IsKeyDown(Keys.Right))))
            {
                double rotationshastighetX = Math.Cos((double)angle);
                double rotationshastighetY = Math.Sin((double)angle);
                float hastighetplusX = (float)rotationshastighetX;
                float hastighetplusY = (float)rotationshastighetY;
                velocity.X = hastighetplusX * 5;
                velocity.Y = hastighetplusY * 5;
                angle += 0.02f;
                alvinssmartametod();
            }

            if (state.IsKeyDown(Keys.Left))
            {
                angle -= 0.07f;
                alvinssmartametod();
            }


            if (state.IsKeyDown(Keys.Right))
            {
                angle += 0.07f;
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



        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, angle, origin, 1, SpriteEffects.None, 1);
        }




    }
}
