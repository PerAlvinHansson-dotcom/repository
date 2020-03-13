using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacewar
{
    class Powerup : GameObject
    {
        //Konstruktor
        public Powerup(Texture2D texture, Vector2 startPosition) : base(texture, startPosition)
        {
            velocity = new Vector2(2, 0);
        }
    }
}
