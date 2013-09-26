using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameOfLife
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        KeyboardState oldKeyState;
        Boolean pausedPressed = false;

        //main area where the cell display is calculated
        Board grid;
        //side menu for controlling speed and displaying generations
        SideMenu sideMenu;
        //Displays message when game is paused ( using spacebar )
        GamePaused gamePaused;
        //Menu buttons to pick a board
        public BoardButton BoardButtons;
        //Checks if board has been clicked and flags variables
        GridClick gridClick;

        private static Game1 instance;

        public static Game1 Instance
        {
            set
            {
                instance = value;
            }
            get
            {
                return instance;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            instance = this;

            instance.graphics.PreferredBackBufferHeight = 600;
            instance.graphics.PreferredBackBufferWidth = 900;
            instance.IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            grid = new Board();
            sideMenu = new SideMenu();
            gamePaused = new GamePaused();
            BoardButtons = new BoardButton();
            gridClick = new GridClick();

            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            grid.LoadContent();
            sideMenu.LoadContent();
            gamePaused.LoadContent();
            BoardButtons.StartBoard.LoadContent();
            BoardButtons.BlankBoard.LoadContent();
            gridClick.LoadContent();
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (BoardButtons.BlankBoard.IsClicked)
            {
                Board.blankBoard = true;
                Board.boardCreated = false;
                SideMenu.Generation = 0;
            }

            else if (BoardButtons.StartBoard.IsClicked)
            {
                Board.blankBoard = false;
                Board.boardCreated = true;
                
            }

            // Allows the game to exit
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            
            //Check if spacebar has been pressed for pausing screen

            if (!checkPause(keyState, oldKeyState))
            {
                grid.Update(gameTime);
                sideMenu.Update(gameTime);
                BoardButtons.StartBoard.Update(gameTime);
                BoardButtons.BlankBoard.Update(gameTime);
                gridClick.Update(gameTime);
            }
            else
            {
                gamePaused.Update(gameTime);
            }

            oldKeyState = keyState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            grid.Draw(gameTime);
            sideMenu.Draw(gameTime);
            BoardButtons.StartBoard.Draw(gameTime);
            BoardButtons.BlankBoard.Draw(gameTime);
            gridClick.Draw(gameTime);

            if (pausedPressed == true)
            {
                gamePaused.Draw(gameTime);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        bool checkPause(KeyboardState keyState, KeyboardState oldKeyState)
        {
            if ((keyState.IsKeyUp(Keys.Space) && oldKeyState.IsKeyDown(Keys.Space)))
            {
                if (pausedPressed == true)
                {
                    pausedPressed = false;
                }
                else
                    pausedPressed = true;
            }

            return pausedPressed;

        }
    }
}
