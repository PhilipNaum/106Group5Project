using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
    internal abstract class GameObject
    {
        // position of the object in world space
        private Vector2 position;
        // bounds of the object in world space
        private Vector2 size;
        private Texture2D texture;

        // updates game object
        public virtual void Update(GameTime gt)
        {

        }

        // displays the game object on the screen
        public virtual void Draw(SpriteBatch sp)
        {

        }

        /// <summary>
        /// Returns a boolean on whether this object is colliding with another
        /// </summary>
        public bool IsColliding(GameObject other)
        {
            return false;
        }
    }
}
