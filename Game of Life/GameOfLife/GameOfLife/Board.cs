using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameOfLife
{
    public class Board:GameEntity
    {
        //Used for positioning of side menu
        public static Texture2D SpriteTemp;
        public static bool blankBoard = false, boardCreated = true;
        public static int rows = 125, columns = 100;
        private Random number = new Random();
        private float elapsed;


        public  Boolean[,] prevGeneration = new Boolean[rows, columns];
        public  Boolean[,] nextGeneration = new Boolean[rows, columns];

        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("WhiteSquare");
            SpriteTemp = Sprite;
            initialBoard(ref prevGeneration);
         
        }

        public override void Update(GameTime gameTime)
        {

            //if user is creating own board
            if (blankBoard && boardCreated)
            {
                int x = (int)GridClick.CellPos.X / 6;
                int y = (int)GridClick.CellPos.Y / 6;

                nextGeneration[x, y] = true;
            }
            //resume updating board
            else
            {
                float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

                elapsed = timeDelta + elapsed;

                //Depending on speed game has been set to, the next generation is created
                if (elapsed >= Speed)
                {
                    NextGenCreate nextGenCreate = new NextGenCreate();
                    nextGeneration = nextGenCreate.GenCreate(prevGeneration, rows, columns);
                    prevGeneration = nextGeneration;
                    elapsed = 0;
                    SideMenu.Generation++;
                    boardCreated = true;
                }

            }

        }

        public override void Draw(GameTime gameTime)
        {
            float spriteWidth = Sprite.Width;
            float spriteHeight = Sprite.Height;
            
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    //positions dependant on cell size
                    float xCellPos = x * spriteWidth;
                    float yCellPos = y * spriteHeight;

                    Position = new Vector2(xCellPos, yCellPos);

                    //Draws the white cells for true, black for false
                    if (prevGeneration[x, y] == true)
                    {
                        Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.White);
                    }
                    else
                    {
                        Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.Black);
                    }
                }
            }
        }

        //assigns a random 50% of board to be alive or dead ( true or false) initially
        public void initialBoard(ref Boolean[,] prevGeneration)
        {
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    int isAlive = number.Next();

                    if (isAlive % 2 == 0)
                    {
                        prevGeneration[x, y] = true;
                    }
                    else
                    {
                        prevGeneration[x, y] = false;
                    }

                }
            }
        }

        //sets all to false to be clicked by user
        public void userBoard(ref Boolean[,] prevGeneration)
        {
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    prevGeneration[x, y] = false;
                }
            }
        }

    }
}
