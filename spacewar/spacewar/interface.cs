using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace spacewar
{
    class Interface
    {
        private SpriteFont font;

        public Interface(SpriteFont font)
        {
            this.font = font;
        }

        public void Print(string text, SpriteBatch spritebatch, int X, int Y)
        {
            spritebatch.DrawString(font, text, new Vector2(X, Y), Color.White);
        }
        
    }
}
