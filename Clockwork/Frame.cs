/*
 * Who has worked on this file:
 * Emma
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimationHelper
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
        /// Texture containing the frame
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// Source Rectangle on the Texture where the frame is contained
        /// </summary>
        public Rectangle Source { get; private set; }

        /// <summary>
        /// Origin of the drawn rectangle
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
