using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class Projectile : GameObject
{
	public Projectile(Texture2D texture, Vector2 startPosition) : base (texture, startPosition)
	{
        
	}

        List<Projectile> projectiles;

        public void Update()
        {

        }
    }
}
