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
using System.Runtime.InteropServices;
using System.Transactions;

namespace Clockwork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static KeyboardState KeyboardState { get; set; }
        public static KeyboardState PrevKeyboardState { get; set; }
        public static MouseState MouseState { get; set; }
        public static MouseState PrevMouseState { get; set; }

        private Player player;
        private Vector2 playerLastFrame;

        private Menu mainMenu;
        private Menu levelSelect;
        private Menu pauseMenu;
        private Menu levelComplete;
        private Menu creditsMenu;
        private Menu controlsMenu;

        private Texture2D scrim;

        private GameState gameState;
        private enum GameState
        {
            MainMenu,
            LevelSelect,
            Gameplay,
            Pause,
            LevelComplete,
            Credits,
            Controls
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //collectibles = new List<Collectible>();
        }

        protected override void Initialize()
        {
            base.Initialize();

            gameState = GameState.MainMenu;

            LevelManager.Instance.SetCurrentLevel(0);

            player = new Player(new Vector2(100, 200), new Vector2(32, 64));
            playerLastFrame = player.Position;


            mainMenu = UILoader.GetMenu(Menus.Main);
            levelSelect = UILoader.GetMenu(Menus.Select);
            pauseMenu = UILoader.GetMenu(Menus.Pause);
            levelComplete = UILoader.GetMenu(Menus.Complete);
            creditsMenu = UILoader.GetMenu(Menus.Credits);
            controlsMenu = UILoader.GetMenu(Menus.Controls);

            LevelManager.Instance.SetCurrentLevel(0);

            KeyboardState = Keyboard.GetState();
            MouseState = Mouse.GetState();
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
            PrevKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
            PrevMouseState = MouseState;
            MouseState = Mouse.GetState();

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
                case GameState.Controls:
                    UpdateControls();
                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }

        private void UpdateMainMenu()
        {
            mainMenu.Update();
            if (mainMenu.UIElements["btStart"].Activated)
            {
                player.ResetPlayer();
                LevelManager.Instance.SetCurrentLevel(LevelManager.Instance.CurrentLevelIndex);
                gameState = GameState.Gameplay;
            }
            if (mainMenu.UIElements["btLevels"].Activated)
                gameState = GameState.LevelSelect;
            if (mainMenu.UIElements["btControls"].Activated)
                gameState = GameState.Controls;
            if (mainMenu.UIElements["btCredits"].Activated)
                gameState = GameState.Credits;
            if (mainMenu.UIElements["btExit"].Activated || SingleKeyPress(Keys.Escape))
                Exit();
        }

        private void UpdateLevelSelect()
        {
            levelSelect.Update();
            if (levelSelect.UIElements["btLevel1"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(0);
                player.ResetPlayer();
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel2"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(1);
                player.ResetPlayer();
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel3"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(2);
                player.ResetPlayer();
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel4"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(3);
                player.ResetPlayer();
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel5"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(4);
                player.ResetPlayer();
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel6"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(5);
                player.ResetPlayer();
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel7"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(6);
                player.ResetPlayer();
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btLevel8"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(7);
                player.ResetPlayer();
                gameState = GameState.Gameplay;
            }
            if (levelSelect.UIElements["btMenu"].Activated || SingleKeyPress(Keys.Escape))
                gameState = GameState.MainMenu;

        }


        private void UpdateGame(GameTime gameTime)
        {
            

            if (SingleKeyPress(Keys.Escape))
            {
                gameState = GameState.Pause;
            }

            player.Update(gameTime);

            LevelManager.Instance.CurrentLevel.Update(gameTime);

            // End Level check
            if (LevelManager.Instance.CurrentLevel.Exit.CanExit && player.IsCollidingPrecise(LevelManager.Instance.CurrentLevel.Exit))
            {
                gameState = GameState.LevelComplete;
            }

            // 2 lines since it's a bit easier to read than one.
            List<Tile> collisions = GetPlayerCollisions();
            HandlePlayerCollisions(collisions);

            // make left and right side of screen act as walls
            if (player.Left < 0)
            {
                player.Position = new Vector2(0, player.Position.Y);
                if (player.Velocity.X < 0)
                    player.Velocity = new Vector2(0, player.Velocity.Y);
            }
            else if (player.Right > _graphics.PreferredBackBufferWidth)
            {
                player.Position = new Vector2(_graphics.PreferredBackBufferWidth - player.Size.X, player.Position.Y);
                if (player.Velocity.X > 0)
                    player.Velocity = new Vector2(0, player.Velocity.Y);
            }
            // reset player and level when falling below the screen
            if (player.Top > _graphics.PreferredBackBufferHeight)
            {
                LevelManager.Instance.ReloadLevel();
                player.ResetPlayer();
            }

            // update sprite because player may have moved from collisions.
            player.SpriteUpdate(gameTime);

            for (int i = 0; i < LevelManager.Instance.CurrentLevel.Enemies.Count; i++)
            {
                for (int j = 0; j < LevelManager.Instance.CurrentLevel.Enemies.Count; j++)
                {
                    if (j != i)
                    {
                        LevelManager.Instance.CurrentLevel.Enemies[i].CollisionResponse(LevelManager.Instance.CurrentLevel.Enemies[j]);
                    }
                }

                if (player.CurrentItem != null && player.CurrentItem.Mode != 2)
                {
                    player.CurrentItem.CollisionResponse(LevelManager.Instance.CurrentLevel.Enemies[i]);
                    LevelManager.Instance.CurrentLevel.Enemies[i].CollisionResponse(player.CurrentItem);
                   
                }

                for (int j = 0; j < LevelManager.Instance.CurrentLevel.CollidableTiles.Count; j++)
                {
                    if (LevelManager.Instance.CurrentLevel.CollidableTiles[i].Active)
                        LevelManager.Instance.CurrentLevel.Enemies[i].CollisionResponse(LevelManager.Instance.CurrentLevel.CollidableTiles[j]);
                }

                player.CollisionResponse(LevelManager.Instance.CurrentLevel.Enemies[i]);
            }

            if (player.CurrentItem != null)
            {
                for (int i = 0; i < LevelManager.Instance.CurrentLevel.CollidableTiles.Count; i++)
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
                    LevelManager.Instance.CurrentLevel.Collectibles[i].CollisionResponse(player);
                }
            }
        }

        private void UpdatePause()
        {
            pauseMenu.Update();
            if (pauseMenu.UIElements["btResume"].Activated || SingleKeyPress(Keys.Escape))
                gameState = GameState.Gameplay;
            if (pauseMenu.UIElements["btReset"].Activated)
            {
                player.ResetPlayer();
                LevelManager.Instance.ReloadLevel();
                gameState = GameState.Gameplay;
            }
            if (pauseMenu.UIElements["btMenu"].Activated)
                gameState = GameState.MainMenu;
        }

        private void UpdateLevelComplete()
        {
            levelComplete.Update();
            if (levelComplete.UIElements["btNext"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(LevelManager.Instance.CurrentLevelIndex + 1);
                player.ResetPlayer();
                gameState = GameState.Gameplay;
            }
            if (levelComplete.UIElements["btLevels"].Activated)
            {
                gameState = GameState.LevelSelect;
            }
            if (levelComplete.UIElements["btMenu"].Activated)
            {
                LevelManager.Instance.SetCurrentLevel(LevelManager.Instance.CurrentLevelIndex + 1);
                player.ResetPlayer();
                gameState = GameState.MainMenu;
            }
        }

        private void UpdateCredits()
        {
            creditsMenu.Update();
            if (creditsMenu.UIElements["btMenu"].Activated || SingleKeyPress(Keys.Escape))
                gameState = GameState.MainMenu;
        }

        private void UpdateControls()
        {
            controlsMenu.Update();
            if (controlsMenu.UIElements["btMenu"].Activated || SingleKeyPress(Keys.Escape))
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
                            player.HasDash = true;

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
                case GameState.Controls:
                    DrawControls();
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

            for (int i = 0; i < LevelManager.Instance.CurrentLevel.Enemies.Count; i++)
            {
                LevelManager.Instance.CurrentLevel.Enemies[i].Draw(_spriteBatch);
            }

            for (int i = 0; i < LevelManager.Instance.CurrentLevel.Collectibles.Count; i++)
            {
                LevelManager.Instance.CurrentLevel.Collectibles[i].Draw(_spriteBatch);
            }

            LevelManager.Instance.CurrentLevel.Draw(_spriteBatch);

            player.Draw(_spriteBatch);
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

        private void DrawControls()
        {
            GraphicsDevice.Clear(Color.Black);
            controlsMenu.Draw(_spriteBatch);
        }

        /// <summary>
        /// Checks if a key has been pressed starting in this frame
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>Whether the key has been pressed starting in this frame</returns>
        public static bool SingleKeyPress(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && PrevKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Checks if the left mouse button has been pressed starting in this frame
        /// </summary>
        /// <returns>Whether the left mouse button has been pressed starting in this frame</returns>
        public static bool SingleLeftClick()
        {
            return MouseState.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Released;
        }

        public static bool LeftClickRelease()
        {
            return MouseState.LeftButton == ButtonState.Released && PrevMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
