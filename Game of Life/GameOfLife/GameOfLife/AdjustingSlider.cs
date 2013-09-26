using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameOfLife
{
    public class AdjustingSlider
    {
        private Texture2D SliderButton, SliderBar;
        private Vector2 BarPosition, minPosition, maxPosition;
        private static Vector2 position;
        private string buttonPosition;

        /// <summary>
        /// Constructor takes in parameters for creating a horizontal adjusting slider button properities
        /// </summary>
        /// <param name="SliderButtonIn">Texture2D of sliding button</param>
        /// <param name="SliderBarIn">Texture2D of the bar upon which the button is sliding</param>
        /// <param name="BarPositionIn">Position on screen of where the bar is to be placed</param>
        /// <param name="ButtonPositionIn">Where the default position of the button is to be on the bar, "start", "middle" or "end"</param>

        public AdjustingSlider (Texture2D SliderButtonIn, Texture2D SliderBarIn, Vector2 BarPositionIn, string ButtonPositionIn)
        {
            SliderButton = SliderButtonIn;
            SliderBar = SliderBarIn;
            BarPosition = BarPositionIn;
            buttonPosition = ButtonPositionIn;

            //button centre 
            float buttonCentreY = SliderButton.Height / 2;
            float buttonCentreX = SliderButton.Width / 2;

            float barLength = SliderBar.Width;

            /*Assign default position of button relative to the bar*/
            Vector2 DefaultPos = new Vector2((SliderBar.Bounds.X + (barLength / 2)) + BarPosition.X,
                                            (SliderBar.Bounds.Y + BarPosition.Y) - buttonCentreY);

            minPosition = new Vector2( ( SliderBar.Bounds.X + BarPosition.X ), 
                                            ( SliderBar.Bounds.Y + BarPosition.Y ) - buttonCentreY);
            
            maxPosition = new Vector2( ( SliderBar.Bounds.X + barLength ) + BarPosition.X,
                                            ( SliderBar.Bounds.Y + BarPosition.Y) - buttonCentreY );

            switch ( buttonPosition )
            {
                case ( "start" ):
                    DefaultPos = minPosition;
                    break;
                case ( "middle"):
                    break;
                case ( "end"):
                    DefaultPos = maxPosition;
                    break;   
            }

            position = DefaultPos;

        }

        public Vector2 Position
        {
            set
            {
                position = value;
            }
            get
            {
                return ( position );
            }
        }


    }
}
