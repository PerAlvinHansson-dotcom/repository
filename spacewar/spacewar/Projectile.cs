//sebastian
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
    class Projectile : GameObject
    {
        float pAngle;
        Vector2 pOrigin;

        public Projectile(Texture2D texture, Vector2 startPosition, float angle, Vector2 origin, string weapon) : base(texture, startPosition)
        {
            pAngle = angle;
            pOrigin = origin;

            //räknar ut var skottet ska vända sig
            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            
            //olika vapen
            if (weapon == "laser")
            {
                velocity = direction * 20;
            }
            if (weapon == "coilgun")
            {
                velocity = direction * 15;
            }
            if (weapon == "plasma")
            {
                velocity = direction * 10;
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, pAngle, pOrigin, 1, SpriteEffects.None, 1);
        }
    }
}
