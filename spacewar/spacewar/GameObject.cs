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
        protected Vector2 position, velocity;

        Vector2 mittpunkt = new Vector2(960, 540);
        float g = 0.75f;

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

            if (position.X < mittpunkt.X)
            {
                position.X += g;
            }

            if (position.X > mittpunkt.X)
            {
                position.X -= g;
            }

            if (position.Y < mittpunkt.Y)
            {
                position.Y += g;
            }

            if (position.Y > mittpunkt.Y)
            {
                position.Y -= g;
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
            if (position.X == mittpunkt.X && position.Y == mittpunkt.Y)
            {
                return true;
            }
            else
                return false;
        }

    

    }
}
