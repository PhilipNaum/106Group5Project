using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;

namespace Clockwork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState kb;
        private SpriteFont _arial36;
        private SpriteFont _arial24;

        private Enemy _testenemy;
        private Enemy _testenemy2;
        private List<Enemy> enemies;

        private Collectible _testitem;

        private Player player;
        private List<Tile> tiles;

        private GameState gameState;
        private enum GameState
        {
            MainMenu,
            LevelSelect,
            Gameplay,
            Pause,
            LevelComplete
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            enemies = new List<Enemy>();
            tiles = new List<Tile>();
        }

        protected override void Initialize()
        {
            base.Initialize();

            gameState = GameState.Gameplay;

            player = new Player(Vector2.Zero, new Vector2(100, 100));

            //_testenemy = new Enemy(new Vector2(400, 50), new Vector2(100, 100), new Vector2(.75f, 0), 200, 10);
            //_testenemy2 = new Enemy(new Vector2(200, 50), new Vector2(100, 100), new Vector2(.75f, 0), 400, 10);
            //enemies.Add(_testenemy);
            //enemies.Add(_testenemy2);

            //_testitem = new Collectible(new Vector2(400, 240), new Vector2(50, 50), Type.Gear);

            // Player tile collision testing stuff
            for (int i = 0; i < 16; i++)
            {
                tiles.Add(new Tile(new Vector2(i, 8), new Vector2(50, 50), true));
            }
            //tiles.Add(new Tile(new Vector2(2, 6), new Vector2(50, 50), true));
            //tiles.Add(new Tile(new Vector2(3, 6), new Vector2(50, 50), true));
            //tiles.Add(new Tile(new Vector2(5, 6), new Vector2(50, 50), true));

            KeyboardState kb = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load content for all animated sprites
            AnimationLoader.LoadContent(Content);

            _arial36 = Content.Load<SpriteFont>("ARIAL36");
            _arial24 = Content.Load<SpriteFont>("ARIAL24");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            switch (gameState)
            {
                case GameState.MainMenu:
                    UpdateMainMenu();
                    break;
                case GameState.LevelSelect:
                    UpdateLevelSelect();
                    break;
                case GameState.Gameplay:
                    UpdateGame(gameTime);
                    break;
                case GameState.Pause:
                    UpdatePause();
                    break;
                case GameState.LevelComplete:
                    UpdateLevelComplete();
                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }

        private void UpdateMainMenu()
        {
            kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Enter))
            {
                gameState = GameState.Gameplay;
            }
        }

        private void UpdateLevelSelect()
        {

        }

        private void UpdateGame(GameTime gameTime)
        {
            kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Escape))
            {
                gameState = GameState.MainMenu;
            }
            
            player.Update(gameTime);

            
            //_testenemy.Update(gameTime);
            //_testenemy2.Update(gameTime);
            //_testitem.Update(gameTime);
        }

        private void UpdatePause()
        {

        }

        private void UpdateLevelComplete()
        {

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.MainMenu:
                    DrawMainMenu();
                    break;
                case GameState.LevelSelect:
                    DrawLevelSelect();
                    break;
                case GameState.Gameplay:
                    DrawGame();
                    break;
                case GameState.Pause:
                    DrawPause();
                    break;
                case GameState.LevelComplete:
                    DrawLevelComplete();
                    break;
                default:
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void DrawMainMenu()
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.DrawString(_arial36, "Clock Work",
                new Vector2(_graphics.PreferredBackBufferWidth / 2 - 120,
                _graphics.PreferredBackBufferHeight / 2 - 50), Color.White);
            _spriteBatch.DrawString(_arial24, "Press Enter to begin Debug mode",
                new Vector2(_graphics.PreferredBackBufferWidth / 2 - 220,
                _graphics.PreferredBackBufferHeight / 2 + 50), Color.White);
        }

        private void DrawLevelSelect()
        {

        }

        private void DrawGame()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            player.Draw(_spriteBatch);

            //_testenemy.Draw(_spriteBatch);
            //_testenemy2.Draw(_spriteBatch);
            //_testitem.Draw(_spriteBatch);

            foreach (Tile t in tiles)
            {
                t.Draw(_spriteBatch);
            }
        }

        private void DrawPause()
        {

        }

        private void DrawLevelComplete()
        {

        }

    }
}
