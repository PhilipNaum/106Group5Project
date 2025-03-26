/*
 * Who has worked on this file:
 * Leo
 * Emma
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using System;

namespace Clockwork
{
    internal abstract class GameObject
    {

        /// <summary>
        /// Position of the Game Object
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Size of the Game Object
        /// </summary>
        public Vector2 Size { get; private set; }

        /// <summary>
        /// Animated Sprite for the Game Object
        /// </summary>
        public AnimatedSprite Sprite { get; private set; }

        public float Left => Position.X;
        public float Right => Position.X + Size.X;
        public float Top => Position.Y;
        public float Bottom => Position.Y + Size.Y;

        // === Constructors ===

        /// <summary>
        /// Create a new Game Object
        /// </summary>
        /// <param name="position">Position Vector2D of the Game Object</param>
        /// <param name="size">Size Vector2D of the Game Object</param>
        /// <param name="spriteName">Name of the sprite this object uses</param>
        public GameObject(Vector2 position, Vector2 size, Sprites spriteName)
        {
            this.Position = position;
            this.Size = size;
            this.Sprite = AnimationLoader.GetSprite(spriteName);
        }

        /// <summary>
        /// Create a new Game Object using the Collectible Type enum
        /// </summary>
        /// <param name="position">Position Vector2D of the Game Object</param>
        /// <param name="size">Size Vector2D of the Game Object</param>
        /// <param name="collectibleType">Name of the sprite this object uses</param>
        public GameObject(Vector2 position, Vector2 size, Type collectibleType)
        {
            Sprites spriteName = default;
            switch (collectibleType)
            {
                case Type.Gear:
                    spriteName = Sprites.Gear;
                    break;
                case Type.Hand:
                    spriteName = Sprites.Hand;
                    break;
                case Type.Face:
                    spriteName = Sprites.Face;
                    break;
                case Type.Key:
                    spriteName = Sprites.Key;
                    break;
                case Type.Chime:
                    spriteName = Sprites.Chime;
                    break;
                default:
                    throw new ArgumentException("Invalid collectibleType passed to GameObject constructor");
            }
            this.Position = position;
            this.Size = size;
            this.Sprite = AnimationLoader.GetSprite(spriteName);
        }


        // === Methods ===

        /// <summary>
        /// Set the animation to be played
        /// </summary>
        /// <param name="animationName"></param>
        public void SetAnimation(string animationName)
        {
            Sprite.SetAnimation(animationName);
        }

        /// <summary>
        /// Update the Game Object
        /// </summary>
        /// <param name="gt">Game time to do updates with</param>
        public virtual void Update(GameTime gt)
        {
            Sprite.Location = new Point((int)Position.X, (int)Position.Y);
            Sprite.Update(gt);
        }

        /// <summary>
        /// Draw the Game Object
        /// </summary>
        public virtual void Draw(SpriteBatch sb)
        {
            Sprite.Draw(sb);
        }


        /// <summary>
        /// checks if the object is colliding with another object
        /// </summary>
        /// <param name="other">the object to check</param>
        /// <returns>whether or not it is colliding</returns>
        public virtual bool IsColliding(GameObject other) => GetRectangle().Intersects(other.GetRectangle());

        /// <summary>
        /// checks if the object is colliding with another object using position and size vectors
        /// </summary>
        /// <param name="other">the object to check</param>
        /// <returns>whether or not it is colliding</returns>
        public virtual bool IsCollidingPrecise(GameObject other)
        {
            float rightestLeft = MathF.Max(Left, other.Left);
            float leftestRight = MathF.Min(Right, other.Right);
            float lowestTop = MathF.Max(Top, other.Top);
            float highestBot = MathF.Min(Bottom, other.Bottom);

            if (leftestRight >= rightestLeft && highestBot >= lowestTop)
            {
                return true;
                // rectangle
                //return new Vector4(rightestLeft, lowestTop, leftestRight - rightestLeft, highestBot - lowestTop);
            }

            return false;
        }

        /// <summary>
        /// Returns the rectangle of collision represented as a vector4 (x: x, y: y, z: width, w: height).
        /// Zero vector if no collision.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual Vector4 GetCollision(GameObject other)
        {
            //float rightestLeft = MathF.Max(Position.X, other.Position.X);
            //float leftestRight = MathF.Min(Position.X + Size.X, other.Position.X + other.Size.X);
            //float lowestTop = MathF.Max(Position.Y, other.Position.Y);
            //float highestBot = MathF.Min(Position.Y + Size.Y, other.Position.Y + other.Size.Y);
            float rightestLeft = MathF.Max(Left, other.Left);
            float leftestRight = MathF.Min(Right, other.Right);
            float lowestTop = MathF.Max(Top, other.Top);
            float highestBot = MathF.Min(Bottom, other.Bottom);

            if (leftestRight >= rightestLeft && highestBot >= lowestTop)
            {
                return new Vector4(rightestLeft, lowestTop, leftestRight - rightestLeft, highestBot - lowestTop);
            }

            return Vector4.Zero;
        }

        /// <summary>
        /// creates a rectangle out of the object's position and size
        /// </summary>
        /// <returns>the rectangle</returns>
        public virtual Rectangle GetRectangle() => new Rectangle(this.Position.ToPoint(), this.Size.ToPoint());

        
    }
}
