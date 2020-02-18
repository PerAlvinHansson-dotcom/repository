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
        private int score = 10;

            
            public Interface (ContentManager Content)
	{
                font = Content.Load<SpriteFont>("Score");
	}

    }
}
