using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Clockwork
{
    public enum Sprites
    {
        player,
        enemy,
        collectible,
        tile
    }

    internal class AnimationLoader
    {
        // === Animation Library ===
        private static Dictionary<Sprites, AnimatedSprite> animationLibrary = new Dictionary<Sprites, AnimatedSprite>();

        // === Content Loading ===

        /// <summary>
        /// Load all of the content and set up the animation library
        /// </summary>
        public static void LoadContent(ContentManager content)
        {
            // -- Player Setup --
            Texture2D playerTexture = content.Load<Texture2D>("Player");
            List<Frame> playerFrames = new List<Frame>();
            playerFrames.Add(new Frame(playerTexture, new Rectangle(0, 0, playerTexture.Width, playerTexture.Height), Vector2.Zero));
            Dictionary<string, Animation> playerAnimations = new Dictionary<string, Animation>();
            playerAnimations.Add("pAnim", new Animation(0, 0, 1));
            animationLibrary.Add(Sprites.player, new AnimatedSprite(playerFrames, playerAnimations, playerAnimations["pAnim"], Point.Zero));

            // -- Enemy Setup --
            Texture2D enemyTexture = content.Load<Texture2D>("Enemy");
            List<Frame> enemyFrames = new List<Frame>();
            enemyFrames.Add(new Frame(enemyTexture, new Rectangle(0, 0, enemyTexture.Width, enemyTexture.Height), Vector2.Zero));
            Dictionary<string, Animation> enemyAnimations = new Dictionary<string, Animation>();
            enemyAnimations.Add("eAnim", new Animation(0, 0, 1));
            animationLibrary.Add(Sprites.enemy, new AnimatedSprite(enemyFrames, enemyAnimations, enemyAnimations["eAnim"], Point.Zero));

            // -- Collectible Setup --
            Texture2D collectibleTexture = content.Load<Texture2D>("Item");
            List<Frame> collectibleFrames = new List<Frame>();
            collectibleFrames.Add(new Frame(collectibleTexture, new Rectangle(0, 0, collectibleTexture.Width, collectibleTexture.Height), Vector2.Zero));
            Dictionary<string, Animation> collectibleAnimations = new Dictionary<string, Animation>();
            collectibleAnimations.Add("cAnim", new Animation(0, 0, 1));
            animationLibrary.Add(Sprites.collectible, new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["cAnim"], Point.Zero));

            // -- Tile Setup --
            Texture2D tileTexture = content.Load<Texture2D>("Tile");
            List<Frame> tileFrames = new List<Frame>();
            tileFrames.Add(new Frame(tileTexture, new Rectangle(0, 0, tileTexture.Width, tileTexture.Height), Vector2.Zero));
            Dictionary<string, Animation> tileAnimations = new Dictionary<string, Animation>();
            tileAnimations.Add("tAnim", new Animation(0, 0, 1));
            animationLibrary.Add(Sprites.tile, new AnimatedSprite(tileFrames, tileAnimations, tileAnimations["tAnim"], Point.Zero));
        }

        public static AnimatedSprite GetSprite(Sprites sprite)
        {
            return animationLibrary[sprite].GetSprite();
        }
    }
}
