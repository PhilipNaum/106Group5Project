using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;

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


        // === Constructors ===

        /// <summary>
        /// Create a new Game Object
        /// </summary>
        /// <param name="position">Position Vector2D of the Game Object</param>
        /// <param name="size">Size Vector2D of the Game Object</param>
        /// <param name="texture"></param>
        public GameObject(Vector2 position, Vector2 size, Sprites spriteName)
        {
            this.Position = position;
            this.Size = size;
            this.Sprite = AnimationLoader.GetSprite(spriteName);
        }


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
        /// creates a rectangle out of the object's position and size
        /// </summary>
        /// <returns>the rectangle</returns>
        public Rectangle GetRectangle() => new Rectangle(this.Position.ToPoint(), this.Size.ToPoint());
    }
}
