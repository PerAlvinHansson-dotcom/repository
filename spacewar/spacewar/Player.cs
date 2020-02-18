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
        List<Projectile> projectiles;


        public Player(Texture2D texture, Vector2 startPosition) : base(texture, startPosition)
        {
            projectiles = new List<Projectile>();

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

            if (state.IsKeyDown(Keys.Down))
            {
                velocity.Y = 3;
                alvinssmartametod();
            }

            if (state.IsKeyDown(Keys.Up))
            {
                velocity.Y = -3;
                alvinssmartametod();
            }


            if (state.IsKeyDown(Keys.Right))
            {
                velocity.X = 3;
                alvinssmartametod();
            }

            if (state.IsKeyDown(Keys.Left))
            {
                velocity.X = -3;
                alvinssmartametod();
            }

            if (state.IsKeyDown(Keys.Space))
            {
                
            }


        }   




    }
}
