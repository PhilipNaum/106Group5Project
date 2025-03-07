using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AnimationHelper
{
    // Emma Rausch
    // 27/2/25
    /// <summary>
    /// A class for handling sprite animations in MonoGame
    /// </summary>
    public class AnimatedSprite
    {
        // === Fields and Properties

        // List of frames the sprite has
        private List<Frame> frames;

        // List of Animations the sprite has
        private Dictionary<string, Animation> animations;

        // Current Animation
        private Animation currentAnimation;

        // Time tracking
        private float time;

        /// <summary>
        /// Loaction of the sprite
        /// </summary>
        public Point Location { get; set; }


        // === Constructors ===


        public AnimatedSprite(List<Frame> frames, Dictionary<string, Animation> animations, Animation startingAnimation, Point startingLocation)
        {
            this.frames = frames;
            this.animations = animations;
            this.currentAnimation = startingAnimation;
            this.Location = startingLocation;
            this.time = 0;
        }

        // === Methods ===

        /// <summary>
        /// Set the current animation to the one at the given index in the animations dict
        /// </summary>
        /// <param name="index">Index of the animation in the dictionary</param>
        public void SetAnimation(string index)
        {
            currentAnimation = animations[index];
            currentAnimation.StartAnimation();
        }

        /// <summary>
        /// Update the animation of the sprite
        /// </summary>
        /// <param name="gt"></param>
        public void Update(GameTime gt)
        {
            time += (float)gt.ElapsedGameTime.TotalSeconds;
            if (time >= 1000 / currentAnimation.FPS)
            {
                currentAnimation.NextFrame();
                time -= 1000 / currentAnimation.FPS;
            }
        }

        // -- Draw methods --

        /// <summary>
        /// Draw the sprite at the current frame
        /// </summary>
        /// <param name="sb"></param
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(frames[currentAnimation.CurrentFrame].Texture, new Rectangle(Location, new Point(frames[currentAnimation.CurrentFrame].Source.Width, frames[currentAnimation.CurrentFrame].Source.Height)),
                frames[currentAnimation.CurrentFrame].Source, Color.White, 0, frames[currentAnimation.CurrentFrame].Origin, SpriteEffects.None, 1);
        }

        /// <summary>
        /// Draw the sprite at the current frame
        /// </summary>
        /// <param name="sb"></param
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="spriteEffects"></param>
        /// <param name="layer"></param>
        public void Draw(SpriteBatch sb, Color color, float rotation, SpriteEffects spriteEffects, float layer)
        {
            sb.Draw(frames[currentAnimation.CurrentFrame].Texture, new Rectangle(Location, new Point(frames[currentAnimation.CurrentFrame].Source.Width, frames[currentAnimation.CurrentFrame].Source.Height)),
                frames[currentAnimation.CurrentFrame].Source, color, rotation, frames[currentAnimation.CurrentFrame].Origin, spriteEffects, layer);
        }
    }
}
