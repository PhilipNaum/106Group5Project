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
        private KeyboardState kbPrev;

        private Enemy _testenemy;
        private Enemy _testenemy2;
        private List<Enemy> enemies;

        private Collectible _testitem;
        private Collectible _testitem2;
        private List<Collectible> collectibles;

        private Player player;
        private List<Tile> tiles;

        private TileType baseTileType;

        private Menu mainMenu;
        private Menu levelSelect;
        private Menu pauseMenu;
        private Menu levelComplete;
        private Menu creditsMenu;

        private GameState gameState;
        private enum GameState
        {
            MainMenu,
            LevelSelect,
            Gameplay,
            Pause,
            LevelComplete,
            Credits
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

            gameState = GameState.MainMenu;

            player = new Player(Vector2.Zero, new Vector2(100, 100), enemies);
            player = new Player(new Vector2(200, 0), new Vector2(100, 100), enemies);

            _testenemy = new Enemy(new Vector2(400, 50), new Vector2(100, 100), new Vector2(.75f, 0), 200, 10);
            _testenemy2 = new Enemy(new Vector2(200, 50), new Vector2(100, 100), new Vector2(.75f, 0), 400, 10);
            enemies.Add(_testenemy);
            enemies.Add(_testenemy2);

            _testitem = new Collectible(new Vector2(400, 240), new Vector2(50, 50), Type.Gear, 0);
            _testitem2 = new Collectible(new Vector2(200, 240), new Vector2(50, 50), Type.Face, 0);
            collectibles.Add(_testitem);
            collectibles.Add(_testitem2);

            mainMenu = UILoader.GetMenu(Menus.Main);
            levelSelect = UILoader.GetMenu(Menus.Select);
            pauseMenu = UILoader.GetMenu(Menus.Pause);
            levelComplete = UILoader.GetMenu(Menus.Complete);
            creditsMenu = UILoader.GetMenu(Menus.Credits);

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

            kb = Keyboard.GetState();
            kbPrev = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load content
            UILoader.LoadContent(Content, _graphics);
        }

        protected override void Update(GameTime gameTime)
        {
            kbPrev = kb;
            kb = Keyboard.GetState();
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
                case GameState.Credits:

                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }

        private void UpdateMainMenu()
        {
            mainMenu.Update();
            if (mainMenu.UIElements["btStart"].Clicked)
                gameState = GameState.Gameplay;
            if (mainMenu.UIElements["btLevels"].Clicked)
                gameState = GameState.LevelSelect;
            if (mainMenu.UIElements["btCredits"].Clicked)
                gameState = GameState.Credits;
            if (mainMenu.UIElements["btExit"].Clicked || SingleKeyPress(Keys.Escape))
                Exit();
        }

        private void UpdateLevelSelect()
        {
            levelSelect.Update();
            if (levelSelect.UIElements["btLevel1"].Clicked)
                gameState = GameState.Gameplay;
            if (levelSelect.UIElements["btLevel2"].Clicked)
                gameState = GameState.Gameplay;
            if (levelSelect.UIElements["btLevel3"].Clicked)
                gameState = GameState.Gameplay;
            if (levelSelect.UIElements["btLevel4"].Clicked)
                gameState = GameState.Gameplay;
            if (levelSelect.UIElements["btLevel5"].Clicked)
                gameState = GameState.Gameplay;
            if (levelSelect.UIElements["btLevel6"].Clicked)
                gameState = GameState.Gameplay;
            if (levelSelect.UIElements["btMenu"].Clicked || SingleKeyPress(Keys.Escape))
                gameState = GameState.MainMenu;
        }

        private void UpdateGame(GameTime gameTime)
        {
            if (SingleKeyPress(Keys.Escape))
            {
                gameState = GameState.Pause;
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

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime);

                for (int j = 0; j < enemies.Count; j++)
                {
                    if (j != i)
                    {
                        enemies[i].CollisionResponse(enemies[j]);
                    }
                }
            }

            for (int i = 0; i < collectibles.Count; i++)
            {
                collectibles[i].Update(gameTime);
                player.CollisionResponse(collectibles[i]);
                //collectibles[i].CollisionResponse(player);
            }
        }

        private void UpdatePause()
        {
            pauseMenu.Update();
            if (pauseMenu.UIElements["btResume"].Clicked || SingleKeyPress(Keys.Escape))
                gameState = GameState.Gameplay;
            if (pauseMenu.UIElements["btMenu"].Clicked)
                gameState = GameState.MainMenu;
        }

        private void UpdateLevelComplete()
        {

        }

        private void UpdateCredits()
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
                        player.Grounded = true;
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
                case GameState.Credits:
                    DrawCredits();
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
            mainMenu.Draw(_spriteBatch);
        }

        private void DrawLevelSelect()
        {
            GraphicsDevice.Clear(Color.Black);
            levelSelect.Draw(_spriteBatch);
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

            foreach (Tile t in tiles)
            {
                t.Draw(_spriteBatch);
            }
        }

        private void DrawPause()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawGame();
            pauseMenu.Draw(_spriteBatch);
        }

        private void DrawLevelComplete()
        {
            GraphicsDevice.Clear(Color.Black);
            levelComplete.Draw(_spriteBatch);
        }

        private void DrawCredits()
        {

            GraphicsDevice.Clear(Color.Black);
            creditsMenu.Draw(_spriteBatch);
        }

        /// <summary>
        /// Checks if a key has been pressed starting in this frame
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>Whether the key has been pressed starting in this frame</returns>
        public bool SingleKeyPress(Keys key)
        {
            return kb.IsKeyDown(key) && kbPrev.IsKeyUp(key);
        }
    }
}
