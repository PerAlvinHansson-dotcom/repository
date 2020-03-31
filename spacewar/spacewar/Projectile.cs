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

        public Projectile(Texture2D texture, Vector2 startPosition, float angle, Vector2 origin) : base(texture, startPosition)
        {
            //velocity = new Vector2(8, 0);
            pAngle = angle;
            //pOrigin = origin;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, pAngle, pOrigin, 1, SpriteEffects.None, 1);
        }

        /*public void Update()
        {

        }*/
    }
}
