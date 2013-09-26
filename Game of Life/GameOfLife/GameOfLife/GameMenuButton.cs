using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfLife
{
    public class GameMenuButton: GameEntity
    {
        private string buttonText;
        private string buttonName, buttonClickName;
        private SpriteFont TextSprite;
        private Texture2D Button, ButtonClicked;
        private Vector2 position, ButtonTextPos;
        private bool isClicked;

        public bool IsClicked
        {
            get
            {
                return isClicked;
            }
            set
            {
                isClicked = value;
            }
        }


        /// <summary>
        /// Constructor to set up a clickable button
        /// </summary>
        /// <param name="buttonTextIn">Text to appear in button</param>
        /// <param name="buttonNameIn">Name of sprite to be loaded</param>
        /// <param name="ButtonAreaIn">Area button is positioned</param>
        /// 
        public GameMenuButton(string buttonTextIn, string buttonNameIn, string buttonClickNameIn, Vector2 positionIn)
        {
            this.buttonText = buttonTextIn;
            this.buttonName = buttonNameIn;
            this.buttonClickName = buttonClickNameIn;
            this.position = positionIn;
        }

        public override void  LoadContent()
        {
            Button = Game1.Instance.Content.Load<Texture2D>(buttonName);
            ButtonClicked = Game1.Instance.Content.Load<Texture2D>(buttonClickName);
            TextSprite = Game1.Instance.Content.Load<SpriteFont>("SpriteFont2");
            ButtonTextPos = new Vector2(position.X + 2, position.Y + 5);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (isClicked == false)
            {

                Game1.Instance.spriteBatch.Draw(Button, position, Color.White);
                Game1.Instance.spriteBatch.DrawString(TextSprite, buttonText, ButtonTextPos, Color.White);
            }
            
            else
            {
                Game1.Instance.spriteBatch.Draw(ButtonClicked, position, Color.White);
                Game1.Instance.spriteBatch.DrawString(TextSprite, buttonText, ButtonTextPos, Color.Blue);
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            checkClick();
        }

        private void checkClick()
        {

            Rectangle pixel, buttonRec;
            MouseState mouseState = Mouse.GetState();

            //create two rectangles. 1 using mouse position, the other using slider button

            pixel = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            //create rectangle the mouse can click on
            buttonRec = new Rectangle ( (int)position.X, (int)position.Y, Button.Width, Button.Height);

            if (pixel.Intersects(buttonRec) && ( mouseState.LeftButton == ButtonState.Pressed) )
            {
                this.isClicked = true;
            }
            else 
            {
                this.isClicked = false;
            }
        }
    }
}
