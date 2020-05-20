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
    abstract class GameObject
    {
       
        protected Texture2D texture;
        public Vector2 position, velocity;

        Vector2 centerpoint = new Vector2(960, 540);
        public float g = 0.5f;

        public Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle();
                hitbox.Height = texture.Height;
                hitbox.Width = texture.Width;
                hitbox.X = (int)position.X;
                hitbox.Y = (int)position.Y;

                return hitbox;
            }


        }

        public GameObject(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            position = startPosition;

        }

        public void Update()
        {
            position += velocity;
            
            float distanceToCenter = (float)Math.Sqrt(Math.Pow((position.X - centerpoint.X), 2) + Math.Pow((position.Y - centerpoint.Y), 2));

            float proportion = 1 * (1 - (distanceToCenter * 0.001f));

            float gravity = g * proportion;


            if (position.X < centerpoint.X)
            {
                position.X += gravity;
            }

            if (position.X > centerpoint.X)
            {
                position.X -= gravity;
            }

            if (position.Y < centerpoint.Y)
            {
                position.Y += gravity;
            }

            if (position.Y > centerpoint.Y)
            {
                position.Y -= gravity;
            }


            if(position.Y > centerpoint.Y && position.X > centerpoint.X)
            {
                position.Y -= gravity;
                position.X -= gravity;
            }

            if(position.Y < centerpoint.Y && position.X > centerpoint.X)
            {
                position.X -= gravity;
                position.Y += gravity;
            }

            if (position.Y < centerpoint.Y && position.X < centerpoint.X)
            {
                position.Y += gravity;
                position.X += gravity;
            }

            if (position.Y > centerpoint.Y && position.X < centerpoint.X)
            {
                position.X += gravity;
                position.Y -= gravity;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public bool Intersects(Rectangle otherObject) //Kollar om man krockar
        {
            return Hitbox.Intersects(otherObject);
        }

        public bool CheckIfInMiddle()
        {
            if (position.X == centerpoint.X && position.Y == centerpoint.Y)
            {
                return true;
            }
            else
                return false;
        }

    

    }
}
