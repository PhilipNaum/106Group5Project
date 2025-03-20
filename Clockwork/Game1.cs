using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Clockwork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteBatch _spriteFont;
        private Texture2D enemySprite;
        private Texture2D mario;
        private Enemy _testenemy;
        private Enemy _testenemy2;
        private List<Enemy> enemies;

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

            base.Initialize();
            KeyboardState kb = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            enemySprite = Content.Load<Texture2D>("Enemy");
            mario = Content.Load<Texture2D>("Mario");
            
            _testenemy = new Enemy(10, enemySprite, new Vector2(400, 50), new Vector2(.75f, 0),200);
            _testenemy2 = new Enemy(10, enemySprite, new Vector2(200, 50),new Vector2(.75f,0), 400);
            enemies.Add(_testenemy);
            enemies.Add(_testenemy2);
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.MainMenu:
                    UpdateMainMenu();
                    break;
                case GameState.LevelSelect:
                    UpdateLevelSelect();
                    break;
                case GameState.Gameplay:
                    UpdateGame();
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
            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime);
                
            }
            _testenemy.CollisionResponse(_testenemy2);
            _testenemy2.CollisionResponse(_testenemy);
            base.Update(gameTime);
        }

        private void UpdateMainMenu()
        {

        }

        private void UpdateLevelSelect()
        {

        }

        private void UpdateGame()
        {

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
            _testenemy.Draw(_spriteBatch);
            _testenemy2.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DrawMainMenu()
        {

        }

        private void DrawLevelSelect()
        {

        }

        private void DrawGame()
        {

        }

        private void DrawPause()
        {

        }

        private void DrawLevelComplete()
        {

        }

    }
}
