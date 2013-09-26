using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife
{
    public abstract class GameEntity
    {
        public Vector2 Position;
        public Boolean IsAlive;
        private Texture2D sprite;
        private static float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
       
            
    }
}
