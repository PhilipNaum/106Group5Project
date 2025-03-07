using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Clockwork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load content for all animated sprites
            AnimationLoader.LoadContent(Content);
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
