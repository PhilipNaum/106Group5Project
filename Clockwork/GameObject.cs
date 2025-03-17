using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;

namespace Clockwork
{
    internal abstract class GameObject
    {
        // === Fields and Properties ===

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


        // === Methods ===

        /// <summary>
        /// Checks if this Game Object is colliding with another Game Object
        /// </summary>
        /// <param name="o">Game Object to check</param>
        /// <returns></returns>
        public bool IsColliding(GameObject o)
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y).Intersects
                (new Rectangle((int)o.Position.X, (int)o.Position.Y, (int)o.Size.X, (int)o.Size.Y));
        }


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
            Sprite.Update(gt);
        }

        /// <summary>
        /// Draw the Game Object
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            Sprite.Draw(sb);
        }
    }
}
