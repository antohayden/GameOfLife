using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

/*Creates the Pause Screen*/

namespace GameOfLife
{
    public class GamePaused: GameEntity
    {
        private Texture2D background;
        private Vector2 bgPosition = new Vector2( 0, 0);
        
        //set color and opacity
        private Color bgColor = new Color(255, 255, 255, 200);

        public override void LoadContent()
        {
            background = Game1.Instance.Content.Load<Texture2D>("PausedScreenBackground");
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(background, bgPosition, bgColor);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //do nothing
        }
    }
}
