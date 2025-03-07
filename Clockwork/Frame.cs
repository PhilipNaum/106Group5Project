using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprite
{
    // Emma Rausch
    // 28/2/25
    /// <summary>
    /// Helper class for AnimatedSprite that contains frame information
    /// </summary>
    public class Frame
    {
        // === Fields and Methods ===

        /// <summary>
        /// 
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Rectangle Source { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Vector2 Origin { get; private set; }

        // === Constructors ===


        public Frame(Texture2D texture, Rectangle source, Vector2 origin)
        {
            Texture = texture;
            Source = source;
            Origin = origin;
        }
    }
}
