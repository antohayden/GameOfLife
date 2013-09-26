using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife
{
    class GridClick:GameEntity
    {
        public static bool isClicked;
        public static Vector2 CellPos;

        public override void  LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("WhiteSquare");
        }

        public override void Draw(GameTime gameTime)
        {
            if (isClicked )
            {
                Game1.Instance.spriteBatch.Draw(Sprite, CellPos, Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            checkClick();
        }
        private void checkClick()
        {
            Rectangle pixel;
            MouseState mouseState = Mouse.GetState();
            Rectangle GridArea;
            int gridWidth, gridHeight;
            int cellX, cellY;

            gridWidth = Board.SpriteTemp.Width * Board.rows;
            gridHeight = Board.SpriteTemp.Height * Board.columns;

            //create two rectangles. 1 using mouse position, the other cell size + mouse position

            pixel = new Rectangle(mouseState.X, mouseState.Y, 1, 1);


            //create rectangle the mouse can click on 
            GridArea = new Rectangle(0, 0, gridWidth, gridHeight);

            if (( pixel.Intersects(GridArea) && (mouseState.LeftButton == ButtonState.Pressed )))
            {
                cellX = mouseState.X;
                cellY = mouseState.Y;
                CellPos = new Vector2(cellX, cellY);
                isClicked = true;
            }
            else
            {
                isClicked = false;
            }
        }
    }
}
