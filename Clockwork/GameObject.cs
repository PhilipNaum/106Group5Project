using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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




        // === Constructors ===

        /// <summary>
        /// Create a new Game Object
        /// </summary>
        /// <param name="position">Position Vector2D of the Game Object</param>
        /// <param name="size">Size Vector2D of the Game Object</param>
        /// <param name="texture"></param>
        public GameObject(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;
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

        /// <summary>
        /// Update the Game Object
        /// </summary>
        /// <param name="gt">Game time to do updates with</param>
        public virtual void Update(GameTime gt)
        {

        }

        /// <summary>
        /// Draw the Game Object
        /// </summary>
        public void Draw()
        {

        }
    }
}
