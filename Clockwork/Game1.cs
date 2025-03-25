using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Collectible _testitem2;
        private List<Collectible> collectibles;

        private Player player;

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
            collectibles = new List<Collectible>();
        }

        protected override void Initialize()
        {
            base.Initialize();

            gameState = GameState.MainMenu;

            player = new Player(Vector2.Zero, new Vector2(100, 100),enemies);

            _testenemy = new Enemy(new Vector2(400, 50), new Vector2(100, 100), new Vector2(.75f, 0), 200, 10);
            _testenemy2 = new Enemy(new Vector2(200, 50), new Vector2(100, 100), new Vector2(.75f, 0), 400, 10);
            enemies.Add(_testenemy);
            enemies.Add(_testenemy2);

            _testitem = new Collectible(new Vector2(400, 240), new Vector2(50, 50), Type.Gear,0);
            _testitem2 = new Collectible(new Vector2(200, 240), new Vector2(50, 50), Type.Face, 0);
            collectibles.Add(_testitem);
            collectibles.Add(_testitem2);

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

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime);

                for(int j = 0; j < enemies.Count; j++)
                {
                    if (j != i)
                    {
                        enemies[i].CollisionResponse(enemies[j]);
                    }
                }
            }

            for(int i=0; i< collectibles.Count; i++)
            {
                collectibles[i].Update(gameTime);
                player.CollisionResponse(collectibles[i]);
                //collectibles[i].CollisionResponse(player);
            }

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

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(_spriteBatch);
                collectibles[i].Draw(_spriteBatch);
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
