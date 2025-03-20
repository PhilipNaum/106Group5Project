using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;
namespace Clockwork
{
    internal abstract class GameObject
    {
        // position of the object in world space
        private Vector2 position;
        // bounds of the object in world space
        private Vector2 size;
        private Texture2D texture;


        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Size 
        {
            get { return size; }
            set { size = value; }
        }
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
        public virtual bool IsColliding(GameObject other)
        {
            return false;
        }
    }
}
