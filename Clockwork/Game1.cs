/*
 * Who has worked on this file:
 * Leo
 * Philip
 * Emma
 * Nathan
 */
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
        private Collectible _testitem2;
        private List<Collectible> collectibles;

        private Player player;
        private List<Tile> tiles;

        private TileType baseTileType;

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
            tiles = new List<Tile>();
        }

        protected override void Initialize()
        {
            base.Initialize();

            gameState = GameState.Gameplay;

            player = new Player(new Vector2(200, 0), new Vector2(100, 100), enemies);

            _testenemy = new Enemy(new Vector2(400, 50), new Vector2(100, 100), new Vector2(.75f, 0), 200, 10);
            _testenemy2 = new Enemy(new Vector2(200, 50), new Vector2(100, 100), new Vector2(.75f, 0), 400, 10);
            enemies.Add(_testenemy);
            enemies.Add(_testenemy2);

            _testitem = new Collectible(new Vector2(400, 240), new Vector2(50, 50), Type.Gear,0);
            _testitem2 = new Collectible(new Vector2(200, 240), new Vector2(50, 50), Type.Face, 0);
            collectibles.Add(_testitem);
            collectibles.Add(_testitem2);

            baseTileType = new TileType(false, true, Sprites.Tile);

            // Temporary level
            for (int i = 0; i < 16; i++)
            {
                tiles.Add(new Tile(baseTileType, new Point(i, 9)));
            }
            for (int i = 0; i < 5; i++)
            {
                tiles.Add(new Tile(baseTileType, new Point(10 + i, 6)));
            }
            for (int i = 0; i < 3; i++)
            {
                tiles.Add(new Tile(baseTileType, new Point(12 + i, 5)));
            }
            tiles.Add(new Tile(baseTileType, new Point(1, 5)));
            tiles.Add(new Tile(baseTileType, new Point(14, 4)));
            for (int i = 0; i < 2; i++)
            {
                tiles.Add(new Tile(baseTileType, new Point(5, 8 - i)));
            }
            for (int i = 0; i < 9; i++)
            {
                tiles.Add(new Tile(baseTileType, new Point(15, 8 - i)));
            }
            for (int i = 0; i < 9; i++)
            {
                tiles.Add(new Tile(baseTileType, new Point(0, 8 - i)));
            }

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
            // update sprite because player may have moved from collisions.
            player.SpriteUpdate(gameTime);

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

        /// <summary>
        /// Returns a list of all tiles currently colliding with the player.
        /// </summary>
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

        /// <summary>
        /// This resolves all player collisions and updates the player's grounded state.
        /// </summary>
        /// <param name="collisions"></param>
        private void HandlePlayerCollisions(List<Tile> collisions)
        {
            // reset grounded to false incase the player left the ground
            // if there is a vertical collision with the player moving down,
            // grounded will become true
            player.Grounded = false;
            Vector2 playerPos = player.Position;
            Vector2 playerVel = player.Velocity;

            // potentially handling collisions by how large the required offset is 
            // instead of horizontal -> vertical could fix some bugs

            // maybe better to keep track of whether left and right collisions have happened
            // and then check if the vertical collision is on the right/left side and is 
            // large enough, otherwise ignore it
            bool horizontalCollision = false;

            // horizontal collisions
            foreach (Tile collider in collisions)
            {
                // collision rectangle
                Vector4 col = player.GetCollision(collider);

                // ignore a collision if the width or height is 0
                if (col.W == 0 || col.Z == 0)
                    continue;

                // (x: x, y: y, z: width, w: height)
                if (col.W >= col.Z) 
                {
                    if (col.Z > 0.3f)
                    {
                        // moving right
                        if (playerVel.X > 0 && player.Right >= collider.Left
                            && player.Right < collider.Right)
                        {
                            horizontalCollision = true;

                            playerPos.X -= col.Z * Math.Sign(collider.Position.X - playerPos.X);
                            playerVel.X = 0;
                        }
                        // moving left
                        else if (playerVel.X < 0 && player.Left <= collider.Right
                            && player.Left > collider.Left)
                        {
                            horizontalCollision = true;

                            playerPos.X -= col.Z * Math.Sign(collider.Position.X - playerPos.X);
                            playerVel.X = 0;
                        }
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
                    if ((col.Z > 8 && horizontalCollision) || !horizontalCollision)
                    {
                        // moving downwards (collision with feet)
                        if (playerVel.Y > 0 && player.Bottom >= collider.Top
                            && player.Top < collider.Top)
                        {
                            playerPos.Y -= col.W * Math.Sign(collider.Position.Y - playerPos.Y);
                            playerVel.Y = 0;
                            player.Grounded = true;
                        }
                        // moving upwards (collision with head)
                        // player.Bottom > collider.Bottom stops velocity from being
                        // set to 0 when the player clips the top of a platform while
                        // moving upwards
                        else if (playerVel.Y < 0 && player.Top <= collider.Bottom 
                            && player.Bottom > collider.Bottom)
                        {
                            playerPos.Y -= col.W * Math.Sign(collider.Position.Y - playerPos.Y);
                            playerVel.Y = 0;
                        }
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

            //for (int i = 0; i < enemies.Count; i++)
            //{
            //    enemies[i].Draw(_spriteBatch);
            //    collectibles[i].Draw(_spriteBatch);
            //}

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
