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
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Diagnostics;
using System.Transactions;

namespace Clockwork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static KeyboardState kb;
        private static KeyboardState kbPrev;
        private static MouseState ms;
        private static MouseState msPrev;

        private Enemy _testenemy;
        private Enemy _testenemy2;
        private List<Enemy> enemies;

        private Collectible _testitem;
        private Collectible _testitem2;
        private Collectible _testitem3;
        private Collectible _testitem4;
        //private List<Collectible> collectibles;

        private Player player;
        private Vector2 playerLastFrame;

        private Menu mainMenu;
        private Menu levelSelect;
        private Menu pauseMenu;
        private Menu levelComplete;
        private Menu creditsMenu;

        private Texture2D scrim;

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
            //collectibles = new List<Collectible>();
        }

        protected override void Initialize()
        {
            base.Initialize();

            gameState = GameState.MainMenu;

            LevelManager.Instance.SetCurrentLevel(0);

            player = new Player(new Vector2(100, 200), new Vector2(32, 64));
            playerLastFrame = player.Position;

            _testenemy = new Enemy(new Vector2(416, 32), new Vector2(32,32), new Vector2(-.5f, 0), 192, 10);
            _testenemy2 = new Enemy(new Vector2(200, 50), new Vector2(100, 100), new Vector2(.75f, 0), 400, 10);
            enemies.Add(_testenemy);
            //enemies.Add(_testenemy2);

            _testitem = new Collectible(new Vector2(400, 240), new Vector2(16, 16), Type.Gear, 0);
            _testitem2 = new Collectible(new Vector2(200, 240), new Vector2(16, 16), Type.Face, 0);
            _testitem3 = new Collectible(new Vector2(400, 240), new Vector2(16, 16), Type.Chime, 0);
            _testitem4 = new Collectible(new Vector2(192,128), new Vector2(16, 16), Type.Hand, 0);
            //collectibles.Add(_testitem);
            //collectibles.Add(_testitem2);
            //collectibles.Add(_testitem3);
            //collectibles.Add(_testitem4);

            mainMenu = UILoader.GetMenu(Menus.Main);
            levelSelect = UILoader.GetMenu(Menus.Select);
            pauseMenu = UILoader.GetMenu(Menus.Pause);
            levelComplete = UILoader.GetMenu(Menus.Complete);
            creditsMenu = UILoader.GetMenu(Menus.Credits);

            LevelManager.Instance.SetCurrentLevel(0);

            kb = Keyboard.GetState();
            ms = Mouse.GetState();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load content
            UILoader.LoadContent(Content, _graphics);
            scrim = this.Content.Load<Texture2D>("Scrim");
        }

        protected override void Update(GameTime gameTime)
        {
            kbPrev = kb;
            kb = Keyboard.GetState();
            msPrev = ms;
            ms = Mouse.GetState();

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
                    UpdateCredits();
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
            {
                LevelManager.Instance.SetCurrentLevel(0);
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel2"].Clicked)
            {
                LevelManager.Instance.SetCurrentLevel(1);
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel3"].Clicked)
            {
                LevelManager.Instance.SetCurrentLevel(2);
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel4"].Clicked)
            {
                LevelManager.Instance.SetCurrentLevel(3);
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel5"].Clicked)
            {
                LevelManager.Instance.SetCurrentLevel(4);
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel6"].Clicked)
            {
                LevelManager.Instance.SetCurrentLevel(5);
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btMenu"].Clicked || SingleKeyPress(Keys.Escape))
                gameState = GameState.MainMenu;
        }

        private void UpdateGame(GameTime gameTime)
        {
            if (SingleKeyPress(Keys.Escape))
            {
                gameState = GameState.Pause;
            }
            if (SingleKeyPress(Keys.X))
            {
                gameState = GameState.LevelComplete;
            }

            player.Update(gameTime);

            LevelManager.Instance.CurrentLevel.Update(gameTime);

            // 2 lines since it's a bit easier to read than one.
            List<Tile> collisions = GetPlayerCollisions();
            HandlePlayerCollisions(collisions);
            // update sprite because player may have moved from collisions.
            player.SpriteUpdate(gameTime);

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

                if (player.CurrentItem != null && player.CurrentItem.Mode != 2)
                {
                    player.CurrentItem.CollisionResponse(enemies[i]);
                    enemies[i].CollisionResponse(player.CurrentItem);
                    if(player.CurrentItem.CollectibleType == Type.Key)
                    {
                        player.CurrentItem.KeyTurn += enemies[i].DeathCheck;
                    }
                }


                for(int j = 0; j < LevelManager.Instance.CurrentLevel.CollidableTiles.Count; j++)
                {
                    if (LevelManager.Instance.CurrentLevel.CollidableTiles[i].Active)
                        enemies[i].CollisionResponse(LevelManager.Instance.CurrentLevel.CollidableTiles[j]);
                }

                player.CollisionResponse(enemies[i]);
            }

            if (player.CurrentItem != null)
            {
                for(int i = 0; i < LevelManager.Instance.CurrentLevel.CollidableTiles.Count; i++)
                {
                    if (LevelManager.Instance.CurrentLevel.CollidableTiles[i].Active)
                        player.CurrentItem.CollisionResponse(LevelManager.Instance.CurrentLevel.CollidableTiles[i]);
                }
            }

            for (int i = 0; i < LevelManager.Instance.CurrentLevel.Collectibles.Count; i++)
            {
                if (LevelManager.Instance.CurrentLevel.Collectibles[i].Mode != 2)
                {
                    LevelManager.Instance.CurrentLevel.Collectibles[i].Update(gameTime);
                    player.CollisionResponse(LevelManager.Instance.CurrentLevel.Collectibles[i]);
                    //collectibles[i].CollisionResponse(player);
                }
            }

            // if the player exits the bounds of the screen reset them and the level.
            if (!player.GetRectangle().Intersects(new Rectangle(0, 0, 
                _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight)))
            {
                player.ResetPlayer();
                LevelManager.Instance.ReloadLevel();
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
            levelComplete.Update();
            if (levelComplete.UIElements["btNext"].Clicked)
            {
                LevelManager.Instance.SetCurrentLevel(LevelManager.Instance.CurrentLevelIndex + 1);
                gameState = GameState.Gameplay;
            }
            if (levelComplete.UIElements["btLevels"].Clicked)
            {
                gameState = GameState.LevelSelect;
            }
            if (levelComplete.UIElements["btMenu"].Clicked)
            {
                LevelManager.Instance.SetCurrentLevel(LevelManager.Instance.CurrentLevelIndex + 1);
                gameState = GameState.MainMenu;
            }
        }

        private void UpdateCredits()
        {
            creditsMenu.Update();
            if (creditsMenu.UIElements["btMenu"].Clicked || SingleKeyPress(Keys.Escape))
                gameState = GameState.MainMenu;
        }

        /// <summary>
        /// Returns a list of all tiles currently colliding with the player.
        /// </summary>
        private List<Tile> GetPlayerCollisions()
        {
            List<Tile> collisions = new List<Tile>();
            foreach (Tile t in LevelManager.Instance.CurrentLevel.CollidableTiles)
            {
                if (t.Active && player.IsCollidingPrecise(t))
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

                            collider.TilePlayerCollision();
                        }
                        // moving left
                        else if (playerVel.X < 0 && player.Left <= collider.Right
                            && player.Left > collider.Left)
                        {
                            horizontalCollision = true;

                            playerPos.X -= col.Z * Math.Sign(collider.Position.X - playerPos.X);
                            playerVel.X = 0;
                            
                            collider.TilePlayerCollision();
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
                // check if the player was above the top of a tile last frame and is below this frame
                if (col.Z >= col.W ||
                    ((playerLastFrame.Y + player.Size.Y) < collider.Top
                    && playerPos.Y + player.Size.Y > collider.Top && playerVel.Y > 0 && col.Z > 1))
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

                            collider.TilePlayerCollision();
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

                            collider.TilePlayerCollision();
                        }
                    }

                }
            }

            player.Position = playerPos;
            player.Velocity = playerVel;
            // position of player last frame used for additional checks
            playerLastFrame = player.Position;
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
            }

            for (int i = 0; i < LevelManager.Instance.CurrentLevel.Collectibles.Count; i++)
            {
                LevelManager.Instance.CurrentLevel.Collectibles[i].Draw(_spriteBatch);
            }

            LevelManager.Instance.CurrentLevel.Draw(_spriteBatch);

        }

        private void DrawPause()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawGame();
            _spriteBatch.Draw(scrim, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            pauseMenu.Draw(_spriteBatch);
        }

        private void DrawLevelComplete()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            player.ResetPlayer();
            DrawGame();
            _spriteBatch.Draw(scrim, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
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
        public static bool SingleKeyPress(Keys key)
        {
            return kb.IsKeyDown(key) && kbPrev.IsKeyUp(key);
        }

        /// <summary>
        /// Checks if the left mouse button has been pressed starting in this frame
        /// </summary>
        /// <returns>Whether the left mouse button has been pressed starting in this frame</returns>
        public static bool SingleLeftClick()
        {
            return ms.LeftButton == ButtonState.Pressed && msPrev.LeftButton == ButtonState.Released;
        }
    }
}
