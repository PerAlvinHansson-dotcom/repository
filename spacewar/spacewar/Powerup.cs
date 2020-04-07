using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacewar
{
    enum PowerUps
    {
        Speed,
        Shield
    }

    class Powerup : GameObject
    {
        public PowerUps pu;

        //Konstruktor
        public Powerup(Texture2D texture, Vector2 startPosition, PowerUps powerUps) : base(texture, startPosition)
        {
            velocity = new Vector2(0, 0);
            pu = powerUps;
        }
    }
}
