using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

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

            //player = new Player(Vector2.Zero, new Vector2(100, 100));
            player = new Player(new Vector2(200, 0), new Vector2(100, 100));

            //_testenemy = new Enemy(new Vector2(400, 50), new Vector2(100, 100), new Vector2(.75f, 0), 200, 10);
            //_testenemy2 = new Enemy(new Vector2(200, 50), new Vector2(100, 100), new Vector2(.75f, 0), 400, 10);
            //enemies.Add(_testenemy);
            //enemies.Add(_testenemy2);

            //_testitem = new Collectible(new Vector2(400, 240), new Vector2(50, 50), Type.Gear);

            // Player tile collision testing stuff
            for (int i = 0; i < 16; i++)
            {
                tiles.Add(new Tile(new Vector2(i, 6), new Vector2(50, 50), true));
            }
            for (int i = 0; i < 4; i++)
            {
                tiles.Add(new Tile(new Vector2(2, 5 - i), new Vector2(50, 50), true));
            }
            for (int i = 0; i < 9; i++)
            {
                tiles.Add(new Tile(new Vector2(15, 5 - i), new Vector2(50, 50), true));
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

            // tiles will not be drawn if not updated
            foreach (Tile t in tiles)
            {
                t.Update(gameTime);
            }

            // 2 lines since it's a bit easier to read than one.
            List<Tile> collisions = GetPlayerCollisions();
            HandlePlayerCollisions(collisions);
            
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

        private List<Tile> GetPlayerCollisions()
        {
            List<Tile> collisions = new List<Tile>();
            foreach (Tile t in tiles)
            {
                if (player.IsCollidingPrecise(t))
                {
                    collisions.Add(t);
                }
            }

            return collisions;
        }

        private void HandlePlayerCollisions(List<Tile> collisions)
        {
            Vector2 playerPos = player.Position;
            Vector2 playerVel = player.Velocity;

            // horizontal collisions
            foreach (Tile collider in collisions)
            {
                // collision rectangle
                Vector4 col = player.GetCollision(collider);

                // (x: x, y: y, z: width, w: height)
                if (col.W >= col.Z)
                {
                    // moving right
                    if (playerVel.X > 0 && player.Right >= collider.Left)
                    {
                        playerPos.X -= col.Z * Math.Sign(collider.Position.X - playerPos.X);
                        playerVel.X = 0;
                    }
                    // moving left
                    else if (playerVel.X < 0 && player.Left <= collider.Right)
                    {
                        playerPos.X -= col.Z * Math.Sign(collider.Position.X - playerPos.X);
                        playerVel.X = 0;
                    }
                }
            }

            // vertical collisions
            foreach (Tile collider in collisions)
            {
                // collision rectangle
                Vector4 col = player.GetCollision(collider);

                // (x: x, y: y, z: width, w: height)
                if (col.Z >= col.W)
                {
                    // moving downwards (collision with feet)
                    if (playerVel.Y > 0 && player.Bottom >= collider.Top)
                    {
                        playerPos.Y -= col.W * Math.Sign(collider.Position.Y - playerPos.Y);
                        playerVel.Y = 0;
                    }
                    // moving upwards (collision with head)
                    else if (playerVel.Y < 0 && player.Top <= collider.Bottom)
                    {
                        playerPos.Y -= col.W * Math.Sign(collider.Position.Y - playerPos.Y);
                        playerVel.Y = 0;
                    }
                }
            }

            player.Position = playerPos;
            player.Velocity = playerVel;
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
