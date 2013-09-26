using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework; 


namespace GameOfLife
{
    class SideMenu: GameEntity
    {
        public static Vector2 SideMenuPosition;
        private Texture2D sliderButton;
        private Texture2D sliderBar;
        private Vector2 sliderBarPosition, buttonPosition, counterPosition;
        private AdjustingSlider slider;
        
        //for displying the number of generations
        public static int Generation;
        private string counterString;
        private SpriteFont font;
        //used when calculating SideMenuPosition

        private float xPosition, yPosition; 

        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("MenuBackground");
            sliderButton = Game1.Instance.Content.Load<Texture2D>("sliderButton");
            sliderBar = Game1.Instance.Content.Load<Texture2D>("sliderBar");
            font = Game1.Instance.Content.Load<SpriteFont>("Spritefont1");
            AssignPositions();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Aligns menu to always be at the right side of the Board
            //regardless of sprite size and rows use ( NOTE: doesn't adjust screen or sidemenu size )

            SideMenuPosition = new Vector2( xPosition, yPosition );

            Game1.Instance.spriteBatch.Draw(Sprite, SideMenuPosition, Color.White);
            Game1.Instance.spriteBatch.Draw(sliderBar, sliderBarPosition, Color.White);
            Game1.Instance.spriteBatch.Draw(sliderButton, slider.Position, Color.White);
            Game1.Instance.spriteBatch.DrawString(font, counterString, counterPosition, Color.White);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Counting generations
            counterString = Generation.ToString();

            updateSlider(gameTime);

            updateSpeed();
            
        }

        //assigns the positions of various sprites
        void AssignPositions()
        {
            //position for side menu
            xPosition = Board.SpriteTemp.Width * Board.rows;
            yPosition = 0;

            //create the slider bar for controlling speed halfway down menu 
            float xSliderPosition = ((Sprite.Width - sliderBar.Width) / 2) + xPosition;
            float ySliderPosition = ((Sprite.Height / 2) - 50) + yPosition;
            sliderBarPosition = new Vector2(xSliderPosition, ySliderPosition);

            //creates values for positioning of slider button
            slider = new AdjustingSlider(sliderButton, sliderBar, sliderBarPosition, "middle");

            buttonPosition = slider.Position;
            
            //positioning of counter text
            counterPosition = new Vector2(slider.Position.X - 20, (Sprite.Height - 60));
        }

        //updates the position of the slider button
        void updateSlider(GameTime gameTime)
        {
            //Creating a float for use when updating positions
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Rectangle pixel, buttonBar;
            MouseState mouseState = Mouse.GetState();

            //create two rectangles. 1 using mouse position, the other using slider button

            pixel = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            Vector2 buttonBarPosition = new Vector2(sliderBarPosition.X, sliderBarPosition.Y - (sliderButton.Height / 2));

            //create rectangle of height of button and width of bar to be the area the mouse can click on
            buttonBar = new Rectangle((int)buttonBarPosition.X, (int)buttonBarPosition.Y,
                sliderBar.Width, sliderButton.Height);


            //update button to move to mouse position X axis within bounds of rectangle buttonBar;

            if (pixel.Intersects(buttonBar) && mouseState.LeftButton == ButtonState.Pressed)
            {
                slider.Position = new Vector2(mouseState.X, slider.Position.Y);
            }
        }

        //updates game speed
        void updateSpeed()
        {
            /*assigning values to the slider to be used to control the speed */
            float startPoint = sliderBarPosition.X;
            float endPoint = sliderBarPosition.X + sliderBar.Width;

            //the number of pixels in the bar
            float speedLength = endPoint - startPoint;

            //with 1 being the maximum amount of time ( secs ) that the screen will take to update
            float pointLength = 1 / speedLength;

            float currentSpeed;

            //adjust speed according to where on slider the button is
            if (slider.Position.X >= startPoint &&
                slider.Position.X <= endPoint)
            {
                currentSpeed = ((endPoint - slider.Position.X) * ( pointLength / 2 ) );
                Speed = currentSpeed;
            }
        }
    }
}
