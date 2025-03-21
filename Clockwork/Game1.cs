using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Clockwork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState kb;
        private SpriteFont _arial36;
        private SpriteFont _arial24;

        private Texture2D enemySprite;
        private Texture2D itemSprite;

        private Enemy _testenemy;
        private Enemy _testenemy2;
        private List<Enemy> enemies;

        private Collectible _testitem;

        private Player player;
        private Texture2D playerTexture;

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
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameState = GameState.MainMenu;

            playerTexture = new Texture2D(GraphicsDevice, 1, 1);
            playerTexture.SetData(new Color[] { Color.Black });
            player = new(playerTexture);
            base.Initialize();
            KeyboardState kb = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            enemySprite = Content.Load<Texture2D>("Enemy");
            itemSprite = Content.Load<Texture2D>("Item");
            _arial36 = Content.Load<SpriteFont>("ARIAL36");
            _arial24 = Content.Load<SpriteFont>("ARIAL24");
            _testenemy = new Enemy(enemySprite, new Vector2(400, 50), new Vector2(.75f, 0), 200, 10);
            _testenemy2 = new Enemy(enemySprite, new Vector2(200, 50), new Vector2(.75f, 0), 400, 10);
            _testitem = new Collectible(itemSprite, new Vector2(400, 240), Type.Gear);
            enemies.Add(_testenemy);
            enemies.Add(_testenemy2);
            
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
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
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
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                gameState = GameState.MainMenu;
            }
            player.Update(gameTime);
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
            _spriteBatch.DrawString(_arial36,"Clock Work",
                new Vector2(_graphics.PreferredBackBufferWidth/2-120,
                _graphics.PreferredBackBufferHeight/2-50),Color.White);
            _spriteBatch.DrawString(_arial24, "Press Enter to begin Debug mode",
                new Vector2(_graphics.PreferredBackBufferWidth / 2 - 220,
                _graphics.PreferredBackBufferHeight / 2 +50), Color.White);
        }

        private void DrawLevelSelect()
        {

        }

        private void DrawGame()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            player.Draw(_spriteBatch);

            _testenemy.Draw(_spriteBatch);
            _testenemy2.Draw(_spriteBatch);
            _testitem.Draw(_spriteBatch);
        }

        private void DrawPause()
        {

        }

        private void DrawLevelComplete()
        {

        }

    }
}
