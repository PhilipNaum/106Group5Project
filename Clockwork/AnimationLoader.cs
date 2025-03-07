using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Clockwork
{
    public enum AnimatedSpriteNames
    {
        player,
        enemy,
        collectible
    }

    internal class AnimationLoader
    {
        // === Animation Library ===

        // Player
        private static Texture2D playerTexture;
        private static List<Frame> playerFrames = new List<Frame>();
        private static Dictionary<string, Animation> playerAnimations = new Dictionary<string, Animation>();

        // Enemy
        private static Texture2D enemyTexture;
        private static List<Frame> enemyFrames = new List<Frame>();
        private static Dictionary<string, Animation> enemyAnimations = new Dictionary<string, Animation>();

        // Collectible
        private static Texture2D collectibleTexture;
        private static List<Frame> collectibleFrames = new List<Frame>();
        private static Dictionary<string, Animation> collectibleAnimations = new Dictionary<string, Animation>();


        // === Content Loading ===

        /// <summary>
        /// Load all of the content in the animation library
        /// </summary>
        public static void LoadContent(ContentManager content)
        {
            // -- Player Setup --

            // Load player textures
            playerTexture = content.Load<Texture2D>("");

            // Add player frames
            playerFrames.Add(new Frame(playerTexture, new Rectangle(0, 0, playerTexture.Width, playerTexture.Height), Vector2.Zero));

            // Add player animations
            playerAnimations.Add("pAnim", new Animation(0, 0, 1));


            // -- Enemy Setup --

            // Load enemy textures
            enemyTexture = content.Load<Texture2D>("");

            // Add enemy frames
            enemyFrames.Add(new Frame(enemyTexture, new Rectangle(0, 0, enemyTexture.Width, enemyTexture.Height), Vector2.Zero));

            // Add enemy animations
            enemyAnimations.Add("eAnim", new Animation(0, 0, 1));


            // -- Collectible Setup --

            // Load collectible textures
            collectibleTexture = content.Load<Texture2D>("");

            // Add collectible frames
            collectibleFrames.Add(new Frame(collectibleTexture, new Rectangle(0, 0, collectibleTexture.Width, collectibleTexture.Height), Vector2.Zero));

            // Add collectible animations
            collectibleAnimations.Add("cAnim", new Animation(0, 0, 1));

        }


        // === Animated Sprite Loading ===

        /// <summary>
        /// Load the given Animated Sprite
        /// </summary>
        /// <param name="name">Sprite to load</param>
        /// <returns>The selected sprite to load</returns>
        public static AnimatedSprite LoadSprite(AnimatedSpriteNames name)
        {
            switch (name)
            {
                // Load player sprite
                case AnimatedSpriteNames.player:
                    return new AnimatedSprite(playerFrames, playerAnimations, playerAnimations["pAnim"], Point.Zero);

                // Load enemy sprite
                case AnimatedSpriteNames.enemy:
                    return new AnimatedSprite(enemyFrames, enemyAnimations, enemyAnimations["eAnim"], Point.Zero);

                // Load collectible sprite
                case AnimatedSpriteNames.collectible:
                    return new AnimatedSprite(collectibleFrames, collectibleAnimations, collectibleAnimations["cAnim"], Point.Zero);

                // This should never happen. It is to appease the compiler gods and rid us of the red squiggle.
                // You should not need to expect a nullref from this
                default:
                    return null;
            }
        }
    }
}
