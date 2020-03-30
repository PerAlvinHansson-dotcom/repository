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
        public Projectile(Texture2D texture, Vector2 startPosition) : base(texture, startPosition)
        {
            velocity = new Vector2(8, 0);
        }

        /*public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }*/

        /*public void Update()
        {

        }*/
    }
}
