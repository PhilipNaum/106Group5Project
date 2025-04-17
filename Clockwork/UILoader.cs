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

        tileDirtL,
        tileDirtToGrassR,
        tileGrass,
        tileGrassDark,
        tileGrassDarkToLight,
        tileGrassEndL,
        tileGrassEndR,
        tileGrassLightToDark,
        tileGrassToDirtL,
        tileGroundBlank,
        tileGroundEndL1,
        tileGroundEndL2,
        tileGroundEndR1,
        tileGroundEndR2,
        tileGroundRocks,
        tileGroundRocks2,
        tileGroundRocksLeavesBottom,
        tileGroundRocksLeavesTop,
        tileGroundTop1,
        tileGroundTop2,
        tileGroundTop3,
        tileGroundTopRocks,
        tileGroundTopRocksVines,
        tileGroundTopVines1
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

            // -- Tile Setup --
            {
                // Load Textures
                Texture2D tileDirtL = content.Load<Texture2D>("tileDirtL");
                Texture2D tileDirtToGrassR = content.Load<Texture2D>("tileDirtToGrassR");
                Texture2D tileGrass = content.Load<Texture2D>("tileGrass");
                Texture2D tileGrassDark = content.Load<Texture2D>("tileGrassDark");
                Texture2D tileGrassDarkToLight = content.Load<Texture2D>("tileGrassDarkToLight");
                Texture2D tileGrassEndL = content.Load<Texture2D>("tileGrassEndL");
                Texture2D tileGrassEndR = content.Load<Texture2D>("tileGrassEndR");
                Texture2D tileGrassLightToDark = content.Load<Texture2D>("tileGrassLightToDark");
                Texture2D tileGrassToDirtL = content.Load<Texture2D>("tileGrassToDirtL");
                Texture2D tileGroundBlank = content.Load<Texture2D>("tileGroundBlank");
                Texture2D tileGroundEndL1 = content.Load<Texture2D>("tileGroundEndL1");
                Texture2D tileGroundEndL2 = content.Load<Texture2D>("tileGroundEndL2");
                Texture2D tileGroundEndR1 = content.Load<Texture2D>("tileGroundEndR1");
                Texture2D tileGroundEndR2 = content.Load<Texture2D>("tileGroundEndR2");
                Texture2D tileGroundRocks = content.Load<Texture2D>("tileGroundRocks");
                Texture2D tileGroundRocks2 = content.Load<Texture2D>("tileGroundRocks2");
                Texture2D tileGroundRocksLeavesBottom = content.Load<Texture2D>("tileGroundRocksLeavesBottom");
                Texture2D tileGroundRocksLeavesTop = content.Load<Texture2D>("tileGroundRocksLeavesTop");
                Texture2D tileGroundTop1 = content.Load<Texture2D>("tileGroundTop1");
                Texture2D tileGroundTop2 = content.Load<Texture2D>("tileGroundTop2");
                Texture2D tileGroundTop3 = content.Load<Texture2D>("tileGroundTop3");
                Texture2D tileGroundTopRocks = content.Load<Texture2D>("tileGroundTopRocks");
                Texture2D tileGroundTopRocksVines = content.Load<Texture2D>("tileGroundTopRocksVines");
                Texture2D tileGroundTopVines1 = content.Load<Texture2D>("tileGroundTopVines1");

                // Set up Frames
                List<Frame> tileFrames = new List<Frame>();
                tileFrames.Add(new Frame(tileDirtL, GetRect(tileDirtL), Vector2.Zero));
                tileFrames.Add(new Frame(tileDirtToGrassR, GetRect(tileDirtToGrassR), Vector2.Zero));
                tileFrames.Add(new Frame(tileGrass, GetRect(tileGrass), Vector2.Zero));
                tileFrames.Add(new Frame(tileGrassDark, GetRect(tileGrassDark), Vector2.Zero));
                tileFrames.Add(new Frame(tileGrassDarkToLight, GetRect(tileGrassDarkToLight), Vector2.Zero));
                tileFrames.Add(new Frame(tileGrassEndL, GetRect(tileGrassEndL), Vector2.Zero));
                tileFrames.Add(new Frame(tileGrassEndR, GetRect(tileGrassEndR), Vector2.Zero));
                tileFrames.Add(new Frame(tileGrassLightToDark, GetRect(tileGrassLightToDark), Vector2.Zero));
                tileFrames.Add(new Frame(tileGrassToDirtL, GetRect(tileGrassToDirtL), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundBlank, GetRect(tileGroundBlank), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundEndL1, GetRect(tileGroundEndL1), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundEndL2, GetRect(tileGroundEndL2), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundEndR1, GetRect(tileGroundEndR1), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundEndR2, GetRect(tileGroundEndR2), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundRocks, GetRect(tileGroundRocks), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundRocks2, GetRect(tileGroundRocks2), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundRocksLeavesBottom, GetRect(tileGroundRocksLeavesBottom), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundRocksLeavesTop, GetRect(tileGroundRocksLeavesTop), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundTop1, GetRect(tileGroundTop1), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundTop2, GetRect(tileGroundTop2), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundTop3, GetRect(tileGroundTop3), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundTopRocks, GetRect(tileGroundTopRocks), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundTopRocksVines, GetRect(tileGroundTopRocksVines), Vector2.Zero));
                tileFrames.Add(new Frame(tileGroundTopVines1, GetRect(tileGroundTopVines1), Vector2.Zero));

                // Set up Animations
                Dictionary<string, Animation> tileAnimations = new Dictionary<string, Animation>();
                tileAnimations.Add("tileDirtL", new Animation(0, 0, 1));
                tileAnimations.Add("tileDirtToGrassR", new Animation(1, 1, 1));
                tileAnimations.Add("tileGrass", new Animation(2, 2, 1));
                tileAnimations.Add("tileGrassDark", new Animation(3, 3, 1));
                tileAnimations.Add("tileGrassDarkToLight", new Animation(4, 4, 1));
                tileAnimations.Add("tileGrassEndL", new Animation(5, 5, 1));
                tileAnimations.Add("tileGrassEndR", new Animation(6, 6, 1));
                tileAnimations.Add("tileGrassLightToDark", new Animation(7, 7, 1));
                tileAnimations.Add("tileGrassToDirtL", new Animation(8, 8, 1));
                tileAnimations.Add("tileGroundBlank", new Animation(9, 9, 1));
                tileAnimations.Add("tileGroundEndL1", new Animation(10, 10, 1));
                tileAnimations.Add("tileGroundEndL2", new Animation(11, 11, 1));
                tileAnimations.Add("tileGroundEndR1", new Animation(12, 12, 1));
                tileAnimations.Add("tileGroundEndR2", new Animation(13, 13, 1));
                tileAnimations.Add("tileGroundRocks", new Animation(14, 14, 1));
                tileAnimations.Add("tileGroundRocks2", new Animation(15, 15, 1));
                tileAnimations.Add("tileGroundRocksLeavesBottom", new Animation(16, 16, 1));
                tileAnimations.Add("tileGroundRocksLeavesTop", new Animation(17, 17, 1));
                tileAnimations.Add("tileGroundTop1", new Animation(18, 18, 1));
                tileAnimations.Add("tileGroundTop2", new Animation(19, 19, 1));
                tileAnimations.Add("tileGroundTop3", new Animation(20, 20, 1));
                tileAnimations.Add("tileGroundTopRocks", new Animation(21, 21, 1));
                tileAnimations.Add("tileGroundTopRocksVines", new Animation(22, 22, 1));
                tileAnimations.Add("tileGroundTopVines1", new Animation(23, 23, 1));

                // Create AnimatedSprites in Animation Library
                animationLibrary.Add(Sprites.tileDirtL, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileDirtL"], Point.Zero));
                animationLibrary.Add(Sprites.tileDirtToGrassR, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileDirtToGrassR"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrass, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrass"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassDark, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassDark"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassDarkToLight, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassDarkToLight"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassEndL, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassEndL"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassEndR, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassEndR"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassLightToDark, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassLightToDark"], Point.Zero));
                animationLibrary.Add(Sprites.tileGrassToDirtL, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGrassToDirtL"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundBlank, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundBlank"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundEndL1, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundEndL1"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundEndL2, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundEndL2"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundEndR1, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundEndR1"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundEndR2, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundEndR2"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundRocks, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundRocks"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundRocks2, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundRocks2"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundRocksLeavesBottom, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundRocksLeavesBottom"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundRocksLeavesTop, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundRocksLeavesTop"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundTop1, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundTop1"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundTop2, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundTop2"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundTop3, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundTop3"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundTopRocks, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundTopRocks"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundTopRocksVines, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundTopRocksVines"], Point.Zero));
                animationLibrary.Add(Sprites.tileGroundTopVines1, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tileGroundTopVines1"], Point.Zero));
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
                animationsbtStart.Add("hovered", new Animation(0, 0, 1));
                animationsbtStart.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btStart, new AnimatedSprite(framesbtStart, animationsbtStart, animationsbtStart["default"], Point.Zero));

                List<Frame> framesbtResume = new List<Frame>();
                framesbtResume.Add(new Frame(btResume, basic, Vector2.Zero));
                framesbtResume.Add(new Frame(btResume, hover, Vector2.Zero));
                framesbtResume.Add(new Frame(btResume, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtResume = new Dictionary<string, Animation>();
                animationsbtResume.Add("default", new Animation(0, 0, 1));
                animationsbtResume.Add("hovered", new Animation(0, 0, 1));
                animationsbtResume.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btResume, new AnimatedSprite(framesbtResume, animationsbtResume, animationsbtResume["default"], Point.Zero));

                List<Frame> framesbtNext = new List<Frame>();
                framesbtNext.Add(new Frame(btNext, basic, Vector2.Zero));
                framesbtNext.Add(new Frame(btNext, hover, Vector2.Zero));
                framesbtNext.Add(new Frame(btNext, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtNext = new Dictionary<string, Animation>();
                animationsbtNext.Add("default", new Animation(0, 0, 1));
                animationsbtNext.Add("hovered", new Animation(0, 0, 1));
                animationsbtNext.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btNext, new AnimatedSprite(framesbtNext, animationsbtNext, animationsbtNext["default"], Point.Zero));

                List<Frame> framesbtExit = new List<Frame>();
                framesbtExit.Add(new Frame(btExit, basic, Vector2.Zero));
                framesbtExit.Add(new Frame(btExit, hover, Vector2.Zero));
                framesbtExit.Add(new Frame(btExit, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtExit = new Dictionary<string, Animation>();
                animationsbtExit.Add("default", new Animation(0, 0, 1));
                animationsbtExit.Add("hovered", new Animation(0, 0, 1));
                animationsbtExit.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btExit, new AnimatedSprite(framesbtExit, animationsbtExit, animationsbtExit["default"], Point.Zero));

                List<Frame> framesbtMenu = new List<Frame>();
                framesbtMenu.Add(new Frame(btMenu, basic, Vector2.Zero));
                framesbtMenu.Add(new Frame(btMenu, hover, Vector2.Zero));
                framesbtMenu.Add(new Frame(btMenu, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtMenu = new Dictionary<string, Animation>();
                animationsbtMenu.Add("default", new Animation(0, 0, 1));
                animationsbtMenu.Add("hovered", new Animation(0, 0, 1));
                animationsbtMenu.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btMenu, new AnimatedSprite(framesbtMenu, animationsbtMenu, animationsbtMenu["default"], Point.Zero));

                List<Frame> framesbtLevels = new List<Frame>();
                framesbtLevels.Add(new Frame(btLevels, basic, Vector2.Zero));
                framesbtLevels.Add(new Frame(btLevels, hover, Vector2.Zero));
                framesbtLevels.Add(new Frame(btLevels, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevels = new Dictionary<string, Animation>();
                animationsbtLevels.Add("default", new Animation(0, 0, 1));
                animationsbtLevels.Add("hovered", new Animation(0, 0, 1));
                animationsbtLevels.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btLevels, new AnimatedSprite(framesbtLevels, animationsbtLevels, animationsbtLevels["default"], Point.Zero));

                List<Frame> framesbtCredits = new List<Frame>();
                framesbtCredits.Add(new Frame(btCredits, basic, Vector2.Zero));
                framesbtCredits.Add(new Frame(btCredits, hover, Vector2.Zero));
                framesbtCredits.Add(new Frame(btCredits, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtCredits = new Dictionary<string, Animation>();
                animationsbtCredits.Add("default", new Animation(0, 0, 1));
                animationsbtCredits.Add("hovered", new Animation(0, 0, 1));
                animationsbtCredits.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btCredits, new AnimatedSprite(framesbtCredits, animationsbtCredits, animationsbtCredits["default"], Point.Zero));

                List<Frame> framesbtControls = new List<Frame>();
                framesbtControls.Add(new Frame(btControls, basic, Vector2.Zero));
                framesbtControls.Add(new Frame(btControls, hover, Vector2.Zero));
                framesbtControls.Add(new Frame(btControls, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtControls = new Dictionary<string, Animation>();
                animationsbtControls.Add("default", new Animation(0, 0, 1));
                animationsbtControls.Add("hovered", new Animation(0, 0, 1));
                animationsbtControls.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btControls, new AnimatedSprite(framesbtControls, animationsbtControls, animationsbtControls["default"], Point.Zero));

                List<Frame> framesbtReset = new List<Frame>();
                framesbtReset.Add(new Frame(btReset, basic, Vector2.Zero));
                framesbtReset.Add(new Frame(btReset, hover, Vector2.Zero));
                framesbtReset.Add(new Frame(btReset, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtReset = new Dictionary<string, Animation>();
                animationsbtReset.Add("default", new Animation(0, 0, 1));
                animationsbtReset.Add("hovered", new Animation(0, 0, 1));
                animationsbtReset.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btReset, new AnimatedSprite(framesbtReset, animationsbtReset, animationsbtReset["default"], Point.Zero));

                List<Frame> framesbtLevel1 = new List<Frame>();
                framesbtLevel1.Add(new Frame(btLevel1, basic, Vector2.Zero));
                framesbtLevel1.Add(new Frame(btLevel1, hover, Vector2.Zero));
                framesbtLevel1.Add(new Frame(btLevel1, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel1 = new Dictionary<string, Animation>();
                animationsbtLevel1.Add("default", new Animation(0, 0, 1));
                animationsbtLevel1.Add("hovered", new Animation(0, 0, 1));
                animationsbtLevel1.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btLevel1, new AnimatedSprite(framesbtLevel1, animationsbtLevel1, animationsbtLevel1["default"], Point.Zero));

                List<Frame> framesbtLevel2 = new List<Frame>();
                framesbtLevel2.Add(new Frame(btLevel2, basic, Vector2.Zero));
                framesbtLevel2.Add(new Frame(btLevel2, hover, Vector2.Zero));
                framesbtLevel2.Add(new Frame(btLevel2, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel2 = new Dictionary<string, Animation>();
                animationsbtLevel2.Add("default", new Animation(0, 0, 1));
                animationsbtLevel2.Add("hovered", new Animation(0, 0, 1));
                animationsbtLevel2.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btLevel2, new AnimatedSprite(framesbtLevel2, animationsbtLevel2, animationsbtLevel2["default"], Point.Zero));

                List<Frame> framesbtLevel3 = new List<Frame>();
                framesbtLevel3.Add(new Frame(btLevel3, basic, Vector2.Zero));
                framesbtLevel3.Add(new Frame(btLevel3, hover, Vector2.Zero));
                framesbtLevel3.Add(new Frame(btLevel3, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel3 = new Dictionary<string, Animation>();
                animationsbtLevel3.Add("default", new Animation(0, 0, 1));
                animationsbtLevel3.Add("hovered", new Animation(0, 0, 1));
                animationsbtLevel3.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btLevel3, new AnimatedSprite(framesbtLevel3, animationsbtLevel3, animationsbtLevel3["default"], Point.Zero));

                List<Frame> framesbtLevel4 = new List<Frame>();
                framesbtLevel4.Add(new Frame(btLevel4, basic, Vector2.Zero));
                framesbtLevel4.Add(new Frame(btLevel4, hover, Vector2.Zero));
                framesbtLevel4.Add(new Frame(btLevel4, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel4 = new Dictionary<string, Animation>();
                animationsbtLevel4.Add("default", new Animation(0, 0, 1));
                animationsbtLevel4.Add("hovered", new Animation(0, 0, 1));
                animationsbtLevel4.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btLevel4, new AnimatedSprite(framesbtLevel4, animationsbtLevel4, animationsbtLevel4["default"], Point.Zero));

                List<Frame> framesbtLevel5 = new List<Frame>();
                framesbtLevel5.Add(new Frame(btLevel5, basic, Vector2.Zero));
                framesbtLevel5.Add(new Frame(btLevel5, hover, Vector2.Zero));
                framesbtLevel5.Add(new Frame(btLevel5, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel5 = new Dictionary<string, Animation>();
                animationsbtLevel5.Add("default", new Animation(0, 0, 1));
                animationsbtLevel5.Add("hovered", new Animation(0, 0, 1));
                animationsbtLevel5.Add("clicked", new Animation(0, 0, 1));
                animationLibrary.Add(Sprites.btLevel5, new AnimatedSprite(framesbtLevel5, animationsbtLevel5, animationsbtLevel5["default"], Point.Zero));

                List<Frame> framesbtLevel6 = new List<Frame>();
                framesbtLevel6.Add(new Frame(btLevel6, basic, Vector2.Zero));
                framesbtLevel6.Add(new Frame(btLevel6, hover, Vector2.Zero));
                framesbtLevel6.Add(new Frame(btLevel6, press, Vector2.Zero));
                Dictionary<string, Animation> animationsbtLevel6 = new Dictionary<string, Animation>();
                animationsbtLevel6.Add("default", new Animation(0, 0, 1));
                animationsbtLevel6.Add("hovered", new Animation(0, 0, 1));
                animationsbtLevel6.Add("clicked", new Animation(0, 0, 1));
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
