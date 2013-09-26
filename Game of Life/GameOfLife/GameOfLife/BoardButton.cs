using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace GameOfLife
{
    public class BoardButton
    {
        private static Vector2 StartBoardPos = new Vector2(775, 375);
        private static Vector2 BlankBoardPos = new Vector2(775, 100);

        public GameMenuButton StartBoard = new GameMenuButton("Start Board", "Button", "Button", StartBoardPos);
        public GameMenuButton BlankBoard = new GameMenuButton("Blank Board", "Button", "ButtonPressed", BlankBoardPos);


    }
}
