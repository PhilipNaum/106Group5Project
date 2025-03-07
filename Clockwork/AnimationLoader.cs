using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using System.Collections.Generic;

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



        // === Animation Loading ===

        /// <summary>
        /// Load the given Animated Sprite
        /// </summary>
        /// <param name="name">Sprite to load</param>
        /// <returns></returns>
        public static AnimatedSprite LoadSprite(AnimatedSpriteNames name)
        {
            return new AnimatedSprite(new List<Frame>(), new Dictionary<string, Animation>(), new Animation(0, 0, 1), Point.Zero);
        }
    }
}
