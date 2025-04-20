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

        Exit,

        btStart,
        btResume,
        btNext,
        btExit,
        btMenu,
        btLevels,
        btCredits,
        btControls,
        btReset,
        btLevel1,
        btLevel2,
        btLevel3,
        btLevel4,
        btLevel5,
        btLevel6,

        tileGrassTopEndL,
        tileGrassToDirtTop,
        tileDirtTop,
        tileDirtToGrassTop,
        tileGrassTop,
        tileGrassToShadeTop,
        tileShadeTop,
        tileShadeToGrassTop,
        tileGrassTopEndR,
        tileGrassEndLBottom,
        tileGrassToDirtBottom,
        tileDirtBottom,
        tileDirtToGrassBottom,
        tileGrassBottom,
        tileGrassToShadeBottom,
        tileShadeBottom,
        tileShadeToGrassBottom,
        tileGrassEndRBottom,
        tileGroundEndL,
        tileGroundRocks1,
        tileGroundRocksLeaves,
        tileGroundEmpty,
        tileGroundRocks2,
        tileGroundEndR,
        tileGroundBottomEndL,
        tileGroundRocks3,
        tileGroundRocks4,
        tileGroundBottomEndR,
        tileDestructible
    }

    public enum Menus
    {
        Main,
        Select,
        Pause,
        Complete,
        Credits,
        Controls
    }

    internal class UILoader
    {
        // === Animation Library ===
        private static Dictionary<Sprites, AnimatedSprite> animationLibrary = new Dictionary<Sprites, AnimatedSprite>();

        // === Menu Library ===
        private static Dictionary<Menus, Menu> menuLibrary = new Dictionary<Menus, Menu>();

        // Fonts
        public static SpriteFont Medodica18 { get; private set; }
        public static SpriteFont Medodica24 { get; private set; }
        public static SpriteFont Medodica48 { get; private set; }
        public static SpriteFont Medodica72 { get; private set; }


        /// <summary>
        /// Load all of the content and set up the Animation library and Menu Library
        /// </summary>
        public static void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            // Load Fonts
            Medodica18 = content.Load<SpriteFont>("MEDODICA18");
            Medodica24 = content.Load<SpriteFont>("MEDODICA24");
            Medodica48 = content.Load<SpriteFont>("MEDODICA48");
            Medodica72 = content.Load<SpriteFont>("MEDODICA72");


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
                Texture2D playerIdleBaseTexture = content.Load<Texture2D>("PC_Idle_Base");
                Texture2D playerRunBaseTexture = content.Load<Texture2D>("PC_Run_Base");

                // Set up Frames
                List<Frame> playerFrames = new List<Frame>();
                playerFrames.Add(new Frame(playerIdleBaseTexture, new Rectangle(0, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerIdleBaseTexture, new Rectangle(0, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerIdleBaseTexture, new Rectangle(0, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerIdleBaseTexture, new Rectangle(0, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerIdleBaseTexture, new Rectangle(0, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerIdleBaseTexture, new Rectangle(64, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerIdleBaseTexture, new Rectangle(128, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerIdleBaseTexture, new Rectangle(192, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerRunBaseTexture, new Rectangle(0, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerRunBaseTexture, new Rectangle(64, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerRunBaseTexture, new Rectangle(128, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerRunBaseTexture, new Rectangle(192, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerRunBaseTexture, new Rectangle(256, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerRunBaseTexture, new Rectangle(320, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerRunBaseTexture, new Rectangle(384, 0, 64, 64), new Vector2(16, 0)));
                playerFrames.Add(new Frame(playerRunBaseTexture, new Rectangle(448, 0, 64, 64), new Vector2(16, 0)));

                // Set up Animations
                Dictionary<string, Animation> playerAnimations = new Dictionary<string, Animation>();
                playerAnimations.Add("idleBase", new Animation(0, 7, 3));
                playerAnimations.Add("runBase", new Animation(8, 15, 6));

                // Create AnimatedSprites in Animation Library
                animationLibrary.Add(Sprites.Player, new AnimatedSprite(playerFrames, playerAnimations, playerAnimations["idleBase"], Point.Zero));
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
                Texture2D gearTexture = content.Load<Texture2D>("GearPickup");
                Texture2D faceTexture = content.Load<Texture2D>("facePickup");
                Texture2D handTexture = content.Load<Texture2D>("HandPickup");
                Texture2D keyTexture = content.Load<Texture2D>("KeyPickup");
                Texture2D bellTexture = content.Load<Texture2D>("BellPickup");

                // Set up Frames
                List<Frame> collectibleFrames = new List<Frame>();
                collectibleFrames.Add(new Frame(gearTexture, new Rectangle(0, 0, 32, 32), Vector2.Zero));
                collectibleFrames.Add(new Frame(gearTexture, new Rectangle(32, 0, 32, 32), Vector2.Zero));
                collectibleFrames.Add(new Frame(faceTexture, GetRect(faceTexture), Vector2.Zero));
                collectibleFrames.Add(new Frame(handTexture, GetRect(handTexture), Vector2.Zero));
                collectibleFrames.Add(new Frame(keyTexture, GetRect(keyTexture), Vector2.Zero));
                collectibleFrames.Add(new Frame(bellTexture, GetRect(bellTexture), Vector2.Zero));

                // Set up Animations
                Dictionary<string, Animation> collectibleAnimations = new Dictionary<string, Animation>();
                collectibleAnimations.Add("gear", new Animation(0, 0, 1));
                collectibleAnimations.Add("gearSpin", new Animation(0, 1, 12));
                collectibleAnimations.Add("face", new Animation(2, 2, 1));
                collectibleAnimations.Add("hand", new Animation(3, 3, 1));
                collectibleAnimations.Add("key", new Animation(4, 4, 1));
                collectibleAnimations.Add("bell", new Animation(5, 5, 1));

                // Create AnimatedSprites in Animation Library
                animationLibrary.Add(Sprites.Gear, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["gear"], Point.Zero));
                animationLibrary.Add(Sprites.Hand, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["hand"], Point.Zero));
                animationLibrary.Add(Sprites.Face, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["face"], Point.Zero));
                animationLibrary.Add(Sprites.Key, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["key"], Point.Zero));
                animationLibrary.Add(Sprites.Chime, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["bell"], Point.Zero));
            }

            // -- Exit Setup --
            {
                Texture2D exitTexture = content.Load<Texture2D>("exitPlaceholder");
                List<Frame> exitFrames = new List<Frame>();
                exitFrames.Add(new Frame(exitTexture, GetRect(exitTexture), Vector2.Zero));
                Dictionary<string, Animation> exitAnimations = new Dictionary<string, Animation>();
                exitAnimations.Add("exit", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.Exit, new AnimatedSprite(exitFrames, exitAnimations, exitAnimations["exit"], Point.Zero));
            }

            // -- Tile Setup --
            {
                // Load Texture
                Texture2D tileset = content.Load<Texture2D>("tileset");

                // Create frames
                List<Frame> tileFrames = new List<Frame>();
                tileFrames.Add(new Frame(tileset, new Rectangle(0, 0, 32, 32), Vector2.Zero));    // tileGrassTopEndL
                tileFrames.Add(new Frame(tileset, new Rectangle(32, 0, 32, 32), Vector2.Zero));   // tileGrassToDirtTop
                tileFrames.Add(new Frame(tileset, new Rectangle(64, 0, 32, 32), Vector2.Zero));   // tileDirtTop
                tileFrames.Add(new Frame(tileset, new Rectangle(96, 0, 32, 32), Vector2.Zero));   // tileDirtToGrassTop
                tileFrames.Add(new Frame(tileset, new Rectangle(128, 0, 32, 32), Vector2.Zero));  // tileGrassTop
                tileFrames.Add(new Frame(tileset, new Rectangle(160, 0, 32, 32), Vector2.Zero));  // tileGrassToShadeTop
                tileFrames.Add(new Frame(tileset, new Rectangle(192, 0, 32, 32), Vector2.Zero));  // tileShadeTop
                tileFrames.Add(new Frame(tileset, new Rectangle(224, 0, 32, 32), Vector2.Zero));  // tileShadeToGrassTop
                tileFrames.Add(new Frame(tileset, new Rectangle(256, 0, 32, 32), Vector2.Zero));  // tileGrassTopEndR
                tileFrames.Add(new Frame(tileset, new Rectangle(0, 32, 32, 32), Vector2.Zero));   // tileGrassEndLBottom
                tileFrames.Add(new Frame(tileset, new Rectangle(32, 32, 32, 32), Vector2.Zero));  // tileGrassToDirtBottom
                tileFrames.Add(new Frame(tileset, new Rectangle(64, 32, 32, 32), Vector2.Zero));  // tileDirtBottom
                tileFrames.Add(new Frame(tileset, new Rectangle(96, 32, 32, 32), Vector2.Zero));  // tileDirtToGrassBottom
                tileFrames.Add(new Frame(tileset, new Rectangle(128, 32, 32, 32), Vector2.Zero)); // tileGrassBottom
                tileFrames.Add(new Frame(tileset, new Rectangle(160, 32, 32, 32), Vector2.Zero)); // tileGrassToShadeBottom
                tileFrames.Add(new Frame(tileset, new Rectangle(192, 32, 32, 32), Vector2.Zero)); // tileShadeBottom
                tileFrames.Add(new Frame(tileset, new Rectangle(224, 32, 32, 32), Vector2.Zero)); // tileShadeToGrassBottom
                tileFrames.Add(new Frame(tileset, new Rectangle(256, 32, 32, 32), Vector2.Zero)); // tileGrassEndRBottom
                tileFrames.Add(new Frame(tileset, new Rectangle(0, 64, 32, 32), Vector2.Zero));   // tileGroundEndL
                tileFrames.Add(new Frame(tileset, new Rectangle(32, 64, 32, 32), Vector2.Zero));  // tileGroundRocks1
                tileFrames.Add(new Frame(tileset, new Rectangle(64, 64, 32, 32), Vector2.Zero));  // tileGroundRocksLeaves
                tileFrames.Add(new Frame(tileset, new Rectangle(192, 64, 32, 32), Vector2.Zero)); // tileGroundEmpty
                tileFrames.Add(new Frame(tileset, new Rectangle(224, 64, 32, 32), Vector2.Zero)); // tileGroundRocks2
                tileFrames.Add(new Frame(tileset, new Rectangle(256, 64, 32, 32), Vector2.Zero)); // tileGroundEndR
                tileFrames.Add(new Frame(tileset, new Rectangle(0, 96, 32, 32), Vector2.Zero));   // tileGroundBottomEndL
                tileFrames.Add(new Frame(tileset, new Rectangle(32, 96, 32, 32), Vector2.Zero));  // tileGroundRocks3
                tileFrames.Add(new Frame(tileset, new Rectangle(64, 96, 32, 32), Vector2.Zero));  // tileGroundRocks4
                tileFrames.Add(new Frame(tileset, new Rectangle(256, 96, 32, 32), Vector2.Zero)); // tileGroundBottomEndR
                tileFrames.Add(new Frame(tileset, new Rectangle(192, 96, 32, 32), Vector2.Zero)); // tileDestructible

                // Create Aniamtions
                Dictionary<string, Animation> tileAnimations = new Dictionary<string, Animation>();
                tileAnimations.Add("tileGrassTopEndL", new Animation(0, 0, 1));
                tileAnimations.Add("tileGrassToDirtTop", new Animation(1, 1, 1));
                tileAnimations.Add("tileDirtTop", new Animation(2, 2, 1));
                tileAnimations.Add("tileDirtToGrassTop", new Animation(3, 3, 1));
                tileAnimations.Add("tileGrassTop", new Animation(4, 4, 1));
                tileAnimations.Add("tileGrassToShadeTop", new Animation(5, 5, 1));
                tileAnimations.Add("tileShadeTop", new Animation(6, 6, 1));
                tileAnimations.Add("tileShadeToGrassTop", new Animation(7, 7, 1));
                tileAnimations.Add("tileGrassTopEndR", new Animation(8, 8, 1));
                tileAnimations.Add("tileGrassEndLBottom", new Animation(9, 9, 1));
                tileAnimations.Add("tileGrassToDirtBottom", new Animation(10, 10, 1));
                tileAnimations.Add("tileDirtBottom", new Animation(11, 11, 1));
                tileAnimations.Add("tileDirtToGrassBottom", new Animation(12, 12, 1));
                tileAnimations.Add("tileGrassBottom", new Animation(13, 13, 1));
                tileAnimations.Add("tileGrassToShadeBottom", new Animation(14, 14, 1));
                tileAnimations.Add("tileShadeBottom", new Animation(15, 15, 1));
                tileAnimations.Add("tileShadeToGrassBottom", new Animation(16, 16, 1));
                tileAnimations.Add("tileGrassEndRBottom", new Animation(17, 17, 1));
                tileAnimations.Add("tileGroundEndL", new Animation(18, 18, 1));
                tileAnimations.Add("tileGroundRocks1", new Animation(19, 19, 1));
                tileAnimations.Add("tileGroundRocksLeaves", new Animation(20, 20, 1));
                tileAnimations.Add("tileGroundEmpty", new Animation(21, 21, 1));
                tileAnimations.Add("tileGroundRocks2", new Animation(22, 22, 1));
                tileAnimations.Add("tileGroundEndR", new Animation(23, 23, 1));
                tileAnimations.Add("tileGroundBottomEndL", new Animation(24, 24, 1));
                tileAnimations.Add("tileGroundRocks3", new Animation(25, 25, 1));
                tileAnimations.Add("tileGroundRocks4", new Animation(26, 26, 1));
                tileAnimations.Add("tileGroundBottomEndR", new Animation(27, 27, 1));
                tileAnimations.Add("tileDestructible", new Animation(28, 28, 1));

                // Create sprites
                animationLibrary.Add(Sprites.tileGrassTopEndL, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassTopEndL"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassToDirtTop, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassToDirtTop"], Point.Zero));
                animationLibrary.Add(Sprites.tileDirtTop, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileDirtTop"], Point.Zero));
                animationLibrary.Add(Sprites.tileDirtToGrassTop, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileDirtToGrassTop"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassTop, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassTop"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassToShadeTop, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassToShadeTop"], Point.Zero));
                animationLibrary.Add(Sprites.tileShadeTop, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileShadeTop"], Point.Zero));
                animationLibrary.Add(Sprites.tileShadeToGrassTop, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileShadeToGrassTop"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassTopEndR, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassTopEndR"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassEndLBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassEndLBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassToDirtBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassToDirtBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileDirtBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileDirtBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileDirtToGrassBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileDirtToGrassBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassToShadeBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassToShadeBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileShadeBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileShadeBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileShadeToGrassBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileShadeToGrassBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassEndRBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassEndRBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundEndL, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundEndL"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundRocks1, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundRocks1"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundRocksLeaves, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundRocksLeaves"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundEmpty, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundEmpty"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundRocks2, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundRocks2"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundEndR, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundEndR"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundBottomEndL, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundBottomEndL"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundRocks3, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundRocks3"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundRocks4, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundRocks4"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundBottomEndR, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundBottomEndR"], Point.Zero));
                animationLibrary.Add(Sprites.tileDestructible, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileDestructible"], Point.Zero));
            }

            // -- Buttons Setup --
            {
                Texture2D btStart = content.Load<Texture2D>("btStart");
                Texture2D btResume = content.Load<Texture2D>("btResume");
                Texture2D btNext = content.Load<Texture2D>("btNext");
                Texture2D btExit = content.Load<Texture2D>("btExit");
                Texture2D btMenu = content.Load<Texture2D>("btMenu");
                Texture2D btLevels = content.Load<Texture2D>("btLevelSelect");
                Texture2D btCredits = content.Load<Texture2D>("btCredits");
                Texture2D btControls = content.Load<Texture2D>("btControls");
                Texture2D btReset = content.Load<Texture2D>("btReset");
                Texture2D btLevel1 = content.Load<Texture2D>("btLevel1");
                Texture2D btLevel2 = content.Load<Texture2D>("btLevel2");
                Texture2D btLevel3 = content.Load<Texture2D>("btLevel3");
                Texture2D btLevel4 = content.Load<Texture2D>("btLevel4");
                Texture2D btLevel5 = content.Load<Texture2D>("btLevel5");
                Texture2D btLevel6 = content.Load<Texture2D>("btLevel6");

                Rectangle basic = new Rectangle(0, 0, 96, 32);
                Rectangle hover = new Rectangle(0, 32, 96, 32);
                Rectangle press = new Rectangle(0, 64, 96, 32);

                List<Frame> framesbtStart = new List<Frame>();
                framesbtStart.Add(new Frame(btStart, basic, Vector2.Zero));
                framesbtStart.Add(new Frame(btStart, hover, Vector2.Zero));
                framesbtStart.Add(new Frame(btStart, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtStart = new Dictionary<string, Animation>();
                animationsbtStart.Add("default", new Animation(0, 0, 1));
                animationsbtStart.Add("hovered", new Animation(1, 1, 1));
                animationsbtStart.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btStart, new AnimatedSprite(framesbtStart, animationsbtStart, animationsbtStart["default"], Point.Zero));

                List<Frame> framesbtResume = new List<Frame>();
                framesbtResume.Add(new Frame(btResume, basic, Vector2.Zero));
                framesbtResume.Add(new Frame(btResume, hover, Vector2.Zero));
                framesbtResume.Add(new Frame(btResume, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtResume = new Dictionary<string, Animation>();
                animationsbtResume.Add("default", new Animation(0, 0, 1));
                animationsbtResume.Add("hovered", new Animation(1, 1, 1));
                animationsbtResume.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btResume, new AnimatedSprite(framesbtResume, animationsbtResume, animationsbtResume["default"], Point.Zero));

                List<Frame> framesbtNext = new List<Frame>();
                framesbtNext.Add(new Frame(btNext, basic, Vector2.Zero));
                framesbtNext.Add(new Frame(btNext, hover, Vector2.Zero));
                framesbtNext.Add(new Frame(btNext, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtNext = new Dictionary<string, Animation>();
                animationsbtNext.Add("default", new Animation(0, 0, 1));
                animationsbtNext.Add("hovered", new Animation(1, 1, 1));
                animationsbtNext.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btNext, new AnimatedSprite(framesbtNext, animationsbtNext, animationsbtNext["default"], Point.Zero));

                List<Frame> framesbtExit = new List<Frame>();
                framesbtExit.Add(new Frame(btExit, basic, Vector2.Zero));
                framesbtExit.Add(new Frame(btExit, hover, Vector2.Zero));
                framesbtExit.Add(new Frame(btExit, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtExit = new Dictionary<string, Animation>();
                animationsbtExit.Add("default", new Animation(0, 0, 1));
                animationsbtExit.Add("hovered", new Animation(1, 1, 1));
                animationsbtExit.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btExit, new AnimatedSprite(framesbtExit, animationsbtExit, animationsbtExit["default"], Point.Zero));

                List<Frame> framesbtMenu = new List<Frame>();
                framesbtMenu.Add(new Frame(btMenu, basic, Vector2.Zero));
                framesbtMenu.Add(new Frame(btMenu, hover, Vector2.Zero));
                framesbtMenu.Add(new Frame(btMenu, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtMenu = new Dictionary<string, Animation>();
                animationsbtMenu.Add("default", new Animation(0, 0, 1));
                animationsbtMenu.Add("hovered", new Animation(1, 1, 1));
                animationsbtMenu.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btMenu, new AnimatedSprite(framesbtMenu, animationsbtMenu, animationsbtMenu["default"], Point.Zero));

                List<Frame> framesbtLevels = new List<Frame>();
                framesbtLevels.Add(new Frame(btLevels, basic, Vector2.Zero));
                framesbtLevels.Add(new Frame(btLevels, hover, Vector2.Zero));
                framesbtLevels.Add(new Frame(btLevels, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevels = new Dictionary<string, Animation>();
                animationsbtLevels.Add("default", new Animation(0, 0, 1));
                animationsbtLevels.Add("hovered", new Animation(1, 1, 1));
                animationsbtLevels.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btLevels, new AnimatedSprite(framesbtLevels, animationsbtLevels, animationsbtLevels["default"], Point.Zero));

                List<Frame> framesbtCredits = new List<Frame>();
                framesbtCredits.Add(new Frame(btCredits, basic, Vector2.Zero));
                framesbtCredits.Add(new Frame(btCredits, hover, Vector2.Zero));
                framesbtCredits.Add(new Frame(btCredits, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtCredits = new Dictionary<string, Animation>();
                animationsbtCredits.Add("default", new Animation(0, 0, 1));
                animationsbtCredits.Add("hovered", new Animation(1, 1, 1));
                animationsbtCredits.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btCredits, new AnimatedSprite(framesbtCredits, animationsbtCredits, animationsbtCredits["default"], Point.Zero));

                List<Frame> framesbtControls = new List<Frame>();
                framesbtControls.Add(new Frame(btControls, basic, Vector2.Zero));
                framesbtControls.Add(new Frame(btControls, hover, Vector2.Zero));
                framesbtControls.Add(new Frame(btControls, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtControls = new Dictionary<string, Animation>();
                animationsbtControls.Add("default", new Animation(0, 0, 1));
                animationsbtControls.Add("hovered", new Animation(1, 1, 1));
                animationsbtControls.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btControls, new AnimatedSprite(framesbtControls, animationsbtControls, animationsbtControls["default"], Point.Zero));

                List<Frame> framesbtReset = new List<Frame>();
                framesbtReset.Add(new Frame(btReset, basic, Vector2.Zero));
                framesbtReset.Add(new Frame(btReset, hover, Vector2.Zero));
                framesbtReset.Add(new Frame(btReset, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtReset = new Dictionary<string, Animation>();
                animationsbtReset.Add("default", new Animation(0, 0, 1));
                animationsbtReset.Add("hovered", new Animation(1, 1, 1));
                animationsbtReset.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btReset, new AnimatedSprite(framesbtReset, animationsbtReset, animationsbtReset["default"], Point.Zero));

                List<Frame> framesbtLevel1 = new List<Frame>();
                framesbtLevel1.Add(new Frame(btLevel1, basic, Vector2.Zero));
                framesbtLevel1.Add(new Frame(btLevel1, hover, Vector2.Zero));
                framesbtLevel1.Add(new Frame(btLevel1, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel1 = new Dictionary<string, Animation>();
                animationsbtLevel1.Add("default", new Animation(0, 0, 1));
                animationsbtLevel1.Add("hovered", new Animation(1, 1, 1));
                animationsbtLevel1.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btLevel1, new AnimatedSprite(framesbtLevel1, animationsbtLevel1, animationsbtLevel1["default"], Point.Zero));

                List<Frame> framesbtLevel2 = new List<Frame>();
                framesbtLevel2.Add(new Frame(btLevel2, basic, Vector2.Zero));
                framesbtLevel2.Add(new Frame(btLevel2, hover, Vector2.Zero));
                framesbtLevel2.Add(new Frame(btLevel2, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel2 = new Dictionary<string, Animation>();
                animationsbtLevel2.Add("default", new Animation(0, 0, 1));
                animationsbtLevel2.Add("hovered", new Animation(1, 1, 1));
                animationsbtLevel2.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btLevel2, new AnimatedSprite(framesbtLevel2, animationsbtLevel2, animationsbtLevel2["default"], Point.Zero));

                List<Frame> framesbtLevel3 = new List<Frame>();
                framesbtLevel3.Add(new Frame(btLevel3, basic, Vector2.Zero));
                framesbtLevel3.Add(new Frame(btLevel3, hover, Vector2.Zero));
                framesbtLevel3.Add(new Frame(btLevel3, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel3 = new Dictionary<string, Animation>();
                animationsbtLevel3.Add("default", new Animation(0, 0, 1));
                animationsbtLevel3.Add("hovered", new Animation(1, 1, 1));
                animationsbtLevel3.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btLevel3, new AnimatedSprite(framesbtLevel3, animationsbtLevel3, animationsbtLevel3["default"], Point.Zero));

                List<Frame> framesbtLevel4 = new List<Frame>();
                framesbtLevel4.Add(new Frame(btLevel4, basic, Vector2.Zero));
                framesbtLevel4.Add(new Frame(btLevel4, hover, Vector2.Zero));
                framesbtLevel4.Add(new Frame(btLevel4, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel4 = new Dictionary<string, Animation>();
                animationsbtLevel4.Add("default", new Animation(0, 0, 1));
                animationsbtLevel4.Add("hovered", new Animation(1, 1, 1));
                animationsbtLevel4.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btLevel4, new AnimatedSprite(framesbtLevel4, animationsbtLevel4, animationsbtLevel4["default"], Point.Zero));

                List<Frame> framesbtLevel5 = new List<Frame>();
                framesbtLevel5.Add(new Frame(btLevel5, basic, Vector2.Zero));
                framesbtLevel5.Add(new Frame(btLevel5, hover, Vector2.Zero));
                framesbtLevel5.Add(new Frame(btLevel5, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel5 = new Dictionary<string, Animation>();
                animationsbtLevel5.Add("default", new Animation(0, 0, 1));
                animationsbtLevel5.Add("hovered", new Animation(1, 1, 1));
                animationsbtLevel5.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btLevel5, new AnimatedSprite(framesbtLevel5, animationsbtLevel5, animationsbtLevel5["default"], Point.Zero));

                List<Frame> framesbtLevel6 = new List<Frame>();
                framesbtLevel6.Add(new Frame(btLevel6, basic, Vector2.Zero));
                framesbtLevel6.Add(new Frame(btLevel6, hover, Vector2.Zero));
                framesbtLevel6.Add(new Frame(btLevel6, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel6 = new Dictionary<string, Animation>();
                animationsbtLevel6.Add("default", new Animation(0, 0, 1));
                animationsbtLevel6.Add("hovered", new Animation(1, 1, 1));
                animationsbtLevel6.Add("clicked", new Animation(2, 2, 1));
                animationLibrary.Add(Sprites.btLevel6, new AnimatedSprite(framesbtLevel6, animationsbtLevel6, animationsbtLevel6["default"], Point.Zero));

            }

            // -- Main Menu --
            {
                Dictionary<string, UIElement> mainMenuElements = new Dictionary<string, UIElement>();
                mainMenuElements.Add("lbTitle", new TextElement("Clockwork", Medodica72, new Rectangle(
                    (graphics.PreferredBackBufferWidth / 2) - ((int)Medodica72.MeasureString("Clockwork").X / 2), (graphics.PreferredBackBufferHeight / 8), 0, 0)));
                mainMenuElements.Add("btStart", new Button(Sprites.btStart, new Point(graphics.PreferredBackBufferWidth / 2 - 48, (graphics.PreferredBackBufferHeight / 2))));
                mainMenuElements.Add("btLevels", new Button(Sprites.btLevels, new Point(graphics.PreferredBackBufferWidth / 2 - 48, (graphics.PreferredBackBufferHeight / 2) + 34)));
                mainMenuElements.Add("btControls", new Button(Sprites.btControls, new Point(graphics.PreferredBackBufferWidth / 2 - 48, (graphics.PreferredBackBufferHeight / 2) + 68)));
                mainMenuElements.Add("btCredits", new Button(Sprites.btCredits, new Point(graphics.PreferredBackBufferWidth / 2 - 48, (graphics.PreferredBackBufferHeight / 2) + 102)));
                mainMenuElements.Add("btExit", new Button(Sprites.btExit, new Point(graphics.PreferredBackBufferWidth / 2 - 48, (graphics.PreferredBackBufferHeight / 2) + 136)));
                menuLibrary.Add(Menus.Main, new Menu(mainMenuElements));
            }

            // Level Select Menu
            {
                Dictionary<string, UIElement> levelSelectElements = new Dictionary<string, UIElement>();
                levelSelectElements.Add("lbTitle", new TextElement("Level Select:", Medodica48, new Rectangle(graphics.PreferredBackBufferWidth / 32, 0, 0, 0)));
                levelSelectElements.Add("btLevel1", new Button(Sprites.btLevel1, new Point(graphics.PreferredBackBufferWidth / 4 - 48, graphics.PreferredBackBufferHeight / 3)));
                levelSelectElements.Add("btLevel2", new Button(Sprites.btLevel2, new Point(graphics.PreferredBackBufferWidth / 2 - 48, graphics.PreferredBackBufferHeight / 3)));
                levelSelectElements.Add("btLevel3", new Button(Sprites.btLevel3, new Point(graphics.PreferredBackBufferWidth * 3 / 4 - 48, graphics.PreferredBackBufferHeight / 3)));
                levelSelectElements.Add("btLevel4", new Button(Sprites.btLevel4, new Point(graphics.PreferredBackBufferWidth / 4 - 48, graphics.PreferredBackBufferHeight * 2 / 3)));
                levelSelectElements.Add("btLevel5", new Button(Sprites.btLevel5, new Point(graphics.PreferredBackBufferWidth / 2 - 48, graphics.PreferredBackBufferHeight * 2 / 3)));
                levelSelectElements.Add("btLevel6", new Button(Sprites.btLevel6, new Point(graphics.PreferredBackBufferWidth * 3 / 4 - 48, graphics.PreferredBackBufferHeight * 2 / 3)));
                levelSelectElements.Add("btMenu", new Button(Sprites.btMenu, new Point(graphics.PreferredBackBufferWidth * 7 / 8 - 48, graphics.PreferredBackBufferHeight * 15 / 16 - 16)));
                menuLibrary.Add(Menus.Select, new Menu(levelSelectElements));
            }

            // Pause Menu
            {
                Dictionary<string, UIElement> pauseMenuElements = new Dictionary<string, UIElement>();
                pauseMenuElements.Add("btResume", new Button(Sprites.btResume, new Point(graphics.PreferredBackBufferWidth / 2 - 48, graphics.PreferredBackBufferHeight * 2 / 5)));
                pauseMenuElements.Add("btReset", new Button(Sprites.btReset, new Point(graphics.PreferredBackBufferWidth / 2 - 48, graphics.PreferredBackBufferHeight * 2 / 5 + 34)));
                pauseMenuElements.Add("btMenu", new Button(Sprites.btMenu, new Point(graphics.PreferredBackBufferWidth / 2 - 48, (graphics.PreferredBackBufferHeight * 2 / 5) + 68)));
                menuLibrary.Add(Menus.Pause, new Menu(pauseMenuElements));
            }

            // Level Complete Menu
            {
                Dictionary<string, UIElement> levelCompleteElements = new Dictionary<string, UIElement>();
                levelCompleteElements.Add("lbLevelComplete", new TextElement("Level Complete!", Medodica72,
                    new Rectangle((graphics.PreferredBackBufferWidth / 2) - ((int)Medodica72.MeasureString("Level Complete!").X / 2), graphics.PreferredBackBufferHeight / 16, 0, 0)));
                levelCompleteElements.Add("btNext", new Button(Sprites.btNext, new Point(graphics.PreferredBackBufferWidth / 2 - 48, graphics.PreferredBackBufferHeight * 2 / 5)));
                levelCompleteElements.Add("btLevels", new Button(Sprites.btLevels, new Point(graphics.PreferredBackBufferWidth / 2 - 48, graphics.PreferredBackBufferHeight * 2 / 5 + 34)));
                levelCompleteElements.Add("btMenu", new Button(Sprites.btMenu, new Point(graphics.PreferredBackBufferWidth / 2 - 48, graphics.PreferredBackBufferHeight * 2 / 5 + 68)));
                menuLibrary.Add(Menus.Complete, new Menu(levelCompleteElements));
            }

            // Credits Menu
            {
                Dictionary<string, UIElement> creditsElements = new Dictionary<string, UIElement>();
                creditsElements.Add("lbCredits1", new TextElement("Environment Art by the Open Pixel Platformer Project:", Medodica24,
                    new Rectangle(graphics.PreferredBackBufferWidth / 32, 0, 0, 0)));
                creditsElements.Add("lbCredits2", new TextElement("Daniel Simu(Hapiel)\nDamian\nDawnbringer\nEllian\nSquirrelsquid\nRileyFiery\nNoburo\nNumberplay\nSkeddles\na3um\nSurt\nStava\nScarab",
                    Medodica18, new Rectangle(graphics.PreferredBackBufferWidth / 32, (int)Medodica24.MeasureString("Environment Art by the Open Pixel Platformer Project:").Y + 4, 0, 0)));
                creditsElements.Add("lbCredits3", new TextElement("guima1901\nConzeit\ngogglecrab\nanodomani\nyaomon17\nAils\nLetmethink\nGrimsane\nDiggyspiff\nPypeBros\npistachio\nnickthem\nCrow",
                    Medodica18, new Rectangle(graphics.PreferredBackBufferWidth * 2 / 5, (int)Medodica24.MeasureString("Environment Art by the Open Pixel Platformer Project:").Y + 4, 0, 0)));
                creditsElements.Add("lbCredits4", new TextElement("Medodica font by Roberto Mocci", Medodica24,
                    new Rectangle(graphics.PreferredBackBufferWidth / 32, graphics.PreferredBackBufferHeight * 13 / 16, 0, 0)));
                creditsElements.Add("btMenu", new Button(Sprites.btMenu, new Point(graphics.PreferredBackBufferWidth * 7 / 8 - 48, graphics.PreferredBackBufferHeight * 15 / 16 - 16)));
                menuLibrary.Add(Menus.Credits, new Menu(creditsElements));
            }

            // Controls Menu
            {
                Dictionary<string, UIElement> controlsElements = new Dictionary<string, UIElement>();
                controlsElements.Add("lbControls", new TextElement("A,D: Move Player\nW: Jump\nSpace: Use Ability", Medodica48, new Rectangle(16, 0, 0, 0)));
                controlsElements.Add("btMenu", new Button(Sprites.btMenu, new Point(graphics.PreferredBackBufferWidth * 7 / 8 - 48, graphics.PreferredBackBufferHeight * 15 / 16 - 16)));
                menuLibrary.Add(Menus.Controls, new Menu(controlsElements));
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
