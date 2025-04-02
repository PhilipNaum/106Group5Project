/*
 * Who has worked on this file:
 * Emma
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Clockwork
{
    public enum Sprites
    {
        Empty,

        Player,
        Enemy,

        Gear,
        Hand,
        Face,
        Key,
        Chime,

        Tile
    }

    public enum Menus
    {
        Main,
        Select,
        Pause,
        Complete,
        Credits
    }

    internal class UILoader
    {
        // === Animation Library ===
        private static Dictionary<Sprites, AnimatedSprite> animationLibrary = new Dictionary<Sprites, AnimatedSprite>();

        // === Menu Library ===
        private static Dictionary<Menus, Menu> menuLibrary = new Dictionary<Menus, Menu>();

        public static SpriteFont _arial36 { get; private set; }
        public static SpriteFont _arial24 { get; private set; }

        /// <summary>
        /// Load all of the content and set up the Animation library and Menu Library
        /// </summary>
        public static void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            // Load Fonts
            _arial36 = content.Load<SpriteFont>("ARIAL36");
            _arial24 = content.Load<SpriteFont>("ARIAL24");

            // -- Empty sprite --
            {
                Texture2D emptyTexture = content.Load<Texture2D>("empty");
                List<Frame> emptyFrames = new List<Frame>();
                emptyFrames.Add(new Frame(emptyTexture, GetRect(emptyTexture), Vector2.Zero));
                Dictionary<string, Animation> emptyAnimations = new Dictionary<string, Animation>();
                emptyAnimations.Add("empty", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.Empty, new AnimatedSprite(emptyFrames, emptyAnimations, emptyAnimations["empty"], Point.Zero));
            }

            // -- Player Setup --
            {
                // Load Textures
                Texture2D playerTexture = content.Load<Texture2D>("Player");

                // Set up Frames
                List<Frame> playerFrames = new List<Frame>();
                playerFrames.Add(new Frame(playerTexture, GetRect(playerTexture), Vector2.Zero));

                // Set up Animations
                Dictionary<string, Animation> playerAnimations = new Dictionary<string, Animation>();
                playerAnimations.Add("player", new Animation(0, 0, 1));

                // Create AnimatedSprites in Animation Library
                animationLibrary.Add(Sprites.Player, new AnimatedSprite(playerFrames, playerAnimations, playerAnimations["player"], Point.Zero));
            }

            // -- Enemy Setup --
            {
                // Load Textures
                Texture2D enemyTexture = content.Load<Texture2D>("Enemy");

                // Set up Frames
                List<Frame> enemyFrames = new List<Frame>();
                enemyFrames.Add(new Frame(enemyTexture, GetRect(enemyTexture), Vector2.Zero));

                // Set up Animations
                Dictionary<string, Animation> enemyAnimations = new Dictionary<string, Animation>();
                enemyAnimations.Add("enemy", new Animation(0, 0, 1));

                // Create AnimatedSprites in Animation Library
                animationLibrary.Add(Sprites.Enemy, new AnimatedSprite(enemyFrames, enemyAnimations, enemyAnimations["enemy"], Point.Zero));
            }

            // -- Collectible Setup --
            {
                // Load Textures
                Texture2D gearTexture = content.Load<Texture2D>("Item");
                Texture2D dashTexture = content.Load<Texture2D>("Dash");

                // Set up Frames
                List<Frame> collectibleFrames = new List<Frame>();
                collectibleFrames.Add(new Frame(gearTexture, GetRect(gearTexture), Vector2.Zero));
                collectibleFrames.Add(new Frame(dashTexture, GetRect(dashTexture), Vector2.Zero));

                // Set up Animations
                Dictionary<string, Animation> collectibleAnimations = new Dictionary<string, Animation>();
                collectibleAnimations.Add("gear", new Animation(0, 0, 1));
                collectibleAnimations.Add("face", new Animation(1, 1, 1));
                collectibleAnimations.Add("hand", new Animation(0, 0, 1));
                collectibleAnimations.Add("key", new Animation(0, 0, 1));
                collectibleAnimations.Add("chime", new Animation(0, 0, 1));

                // Create AnimatedSprites in Animation Library
                animationLibrary.Add(Sprites.Gear, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["gear"], Point.Zero));
                animationLibrary.Add(Sprites.Hand, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["hand"], Point.Zero));
                animationLibrary.Add(Sprites.Face, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["face"], Point.Zero));
                animationLibrary.Add(Sprites.Key, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["key"], Point.Zero));
                animationLibrary.Add(Sprites.Chime, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["chime"], Point.Zero));
            }

            // -- Tile Setup --
            {
                // Load Textures
                Texture2D tileTexture = content.Load<Texture2D>("Tile");

                // Set up Frames
                List<Frame> tileFrames = new List<Frame>();
                tileFrames.Add(new Frame(tileTexture, GetRect(tileTexture), Vector2.Zero));

                // Set up Animations
                Dictionary<string, Animation> tileAnimations = new Dictionary<string, Animation>();
                tileAnimations.Add("tile", new Animation(0, 0, 1));

                // Create AnimatedSprites in Animation Library
                animationLibrary.Add(Sprites.Tile, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tile"], Point.Zero));
            }

            // -- Main Menu --
            {
                Dictionary<string, UIElement> mainMenuElements = new Dictionary<string, UIElement>();
                mainMenuElements.Add("lbTitle", new TextElement("Clock Work", _arial36, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 120,
                graphics.PreferredBackBufferHeight / 2 - 50, 1, 1)));
                mainMenuElements.Add("btStart", new Button(Sprites.Empty, new Rectangle(0, 0, 0, 0)));
                menuLibrary.Add(Menus.Main, new Menu(mainMenuElements));
            }

            // Level Select Menu
            {
                Dictionary<string, UIElement> levelSelectElements = new Dictionary<string, UIElement>();

                menuLibrary.Add(Menus.Select, new Menu(levelSelectElements));
            }

            // Pause Menu
            {
                Dictionary<string, UIElement> pauseMenuElements = new Dictionary<string, UIElement>();
                pauseMenuElements.Add("btResume", new Button(Sprites.Empty, new Rectangle(0, 0, 0, 0)));
                pauseMenuElements.Add("btQuit", new Button(Sprites.Empty, new Rectangle(0, 0, 0, 0)));
                menuLibrary.Add(Menus.Pause, new Menu(pauseMenuElements));
            }

            // Level Complete Menu
            {
                Dictionary<string, UIElement> levelCompleteElements = new Dictionary<string, UIElement>();

                menuLibrary.Add(Menus.Complete, new Menu(levelCompleteElements));
            }

            // Credits Menu
            {
                Dictionary<string, UIElement> creditsElements = new Dictionary<string, UIElement>();
                creditsElements.Add("lbCreditsTitle", new TextElement("Credits", _arial36, new Rectangle()));
                creditsElements.Add("lbCredits", new TextElement("", _arial24, new Rectangle()));
                menuLibrary.Add(Menus.Complete, new Menu(creditsElements));
            }
        }

        /// <summary>
        /// Get a rectangle for a full texture
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        private static Rectangle GetRect(Texture2D texture) => new Rectangle(0, 0, texture.Width, texture.Height);

        /// <summary>
        /// Get a copy of a sprite from the Animation Library
        /// </summary>
        /// <param name="sprite">Sprite to get</param>
        /// <returns>Selected Sprite from the Animation Library</returns>
        public static AnimatedSprite GetSprite(Sprites sprite)
        {
            return animationLibrary[sprite].GetSprite();
        }

        /// <summary>
        /// Get a Menu from the Menu Library
        /// </summary>
        /// <param name="menu">Menu to get</param>
        /// <returns>Selected Menu from the Menu Library</returns>
        public static Menu GetMenu(Menus menu)
        {
            return menuLibrary[menu];
        }
    }
}
