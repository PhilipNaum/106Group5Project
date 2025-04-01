using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using Microsoft.Xna.Framework.Input;


namespace Clockwork
{
    internal class ImageElement : UIElement
    {
        /// <summary>
        /// Create a new UIElement
        /// </summary>
        /// <param name="spriteName">Sprite the Element has</param>
        /// <param name="rectangle">Body Rectangle of the Element</param>
        public ImageElement(Sprites spriteName, Rectangle rectangle) : base(spriteName, rectangle) { }

        /// <summary>
        /// Create a new UIElement
        /// </summary>
        /// <param name="spriteName">Sprite the Element has</param>
        /// <param name="position">Position of the Element</param>
        /// <param name="size">Size of the Element</param>
        public ImageElement(Sprites spriteName, Vector2 position, Vector2 size) :
            this(spriteName, new Rectangle(position.ToPoint(), size.ToPoint()))
        { }

        /// <summary>
        /// Create a new UIElement
        /// </summary>
        /// <param name="spriteName">Sprite the Element has</param>
        /// <param name="position">Position of the Element</param>
        /// <param name="size">Size of the Element</param>
        public ImageElement(Sprites spriteName, Point position, Point size) :
            this(spriteName, new Rectangle(position, size))
        { }
    }
}
