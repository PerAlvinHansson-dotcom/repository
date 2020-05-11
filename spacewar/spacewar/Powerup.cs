using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacewar
{
    //Hugo
    enum PowerUps
    {
        Speed,
        Shield
    }

    class Powerup : GameObject
    {
        public PowerUps pu;

        Random rng = new Random();

        //Konstruktor
        public Powerup(Texture2D texture, Vector2 startPosition, PowerUps powerUps) : base(texture, startPosition)
        {
            velocity = new Vector2((float)rng.NextDouble() * rng.Next(-4,4), (float)rng.NextDouble() * rng.Next(-4, 4));
            pu = powerUps;
        }
    }
}
