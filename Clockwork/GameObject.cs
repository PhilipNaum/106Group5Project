/*
 * Who has worked on this file:
 * Leo
 * Emma
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using System;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public static Stack<GameObject> deadObjects = new Stack<GameObject>();


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
            this.Sprite = UILoader.GetSprite(spriteName);
            Sprite.Location = position.ToPoint();
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
            this.Sprite = UILoader.GetSprite(spriteName);
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
            SpriteUpdate(gt);
        }

        /// <summary>
        /// Updates the sprite without running any subclass Update methods
        /// </summary>
        /// <param name="gt"></param>
        public void SpriteUpdate(GameTime gt)
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
        /// Draw the sprite at the current frame
        /// </summary>
        /// <param name="sb"></param
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="spriteEffects"></param>
        /// <param name="layer"></param>
        public void Draw(SpriteBatch sb, float scale, Color color, float rotation, SpriteEffects spriteEffects, float layer)
        {
            Sprite.Draw(sb, scale, color, rotation, spriteEffects, layer);
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

        public static void ReverseTime(GameTime gt)
        {
            System.Diagnostics.Debug.WriteLine("method called");
            //only do this if that stack has objects in it
            if (deadObjects.Count == 0)
            {
                return;
            }
            //double reverseTimer = .25;
                //reverseTimer -= 1 / 60;
                //if (reverseTimer <= 0)
                //{
                    if (deadObjects.Peek() is Enemy)
                    {
                        //revive the enemy if it's an enemy
                        Enemy currentEnemy = (Enemy)deadObjects.Peek();
                        currentEnemy.IsDead = false;
                        deadObjects.Pop();
                    }
                    if (deadObjects.Peek() is Tile)
                    {
                        //set the tile to active if it's a tile
                        Tile currentTile = (Tile)deadObjects.Peek();
                        System.Diagnostics.Debug.WriteLine("tile made active");
                        currentTile.Active = true;

                        deadObjects.Pop();
                    }
                    //reverseTimer = .25f;
                //}
            
        }
    }
}
