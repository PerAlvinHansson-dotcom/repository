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

        public Player(Texture2D texture, Vector2 startPosition) : base(texture, startPosition)
        {


        }

        public new void Update()
        {

            position += velocity;

            KeyboardState state = Keyboard.GetState();



           

            if (state.IsKeyDown(Keys.Down))
            {
                velocity.Y = 3;
            }

            if (state.IsKeyDown(Keys.Up))
            {
                velocity.Y = -3;
            }

       

        }   




    }
}
