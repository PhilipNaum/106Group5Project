/*
 * Who has worked on this file:
 * Emma
 */
using Microsoft.Xna.Framework;


namespace Clockwork
{
    internal class Button : UIElement
    {
        // === Constructors ===

        /// <summary>
        /// Create a new Button object
        /// </summary>
        /// <param name="spriteName">Name of the Sprite the Button uses</param>
        /// <param name="rectangle">Body Rectangle of the Button</param>
        public Button(Sprites spriteName, Rectangle rectangle) : base(spriteName, rectangle) { }

        /// <summary>
        /// Create a new Button object
        /// </summary>
        /// <param name="spriteName">Name of the Sprite the Button uses</param>
        /// <param name="position">Position of the Button</param>
        /// <param name="size">Size of the Button</param>
        public Button(Sprites spriteName, Vector2 position, Vector2 size) :
            this(spriteName, new Rectangle(position.ToPoint(), size.ToPoint()))
        { }

        /// <summary>
        /// Create a new Button object
        /// </summary>
        /// <param name="spriteName">Name of the Sprite the Button uses</param>
        /// <param name="position">Position of the Button</param>
        /// <param name="size">Size of the Button</param>
        public Button(Sprites spriteName, Point position, Point size) :
            this(spriteName, new Rectangle(position, size))
        { }

        /// <summary>
        /// Create a new Button object that is 96x23
        /// </summary>
        /// <param name="spriteName">Name of the Sprite the Button uses</param>
        /// <param name="location">Location of the button</param>
        public Button(Sprites spriteName, Point location) : base(spriteName, new Rectangle(location, new Point(96, 32))) { }


        // === Methods ===

        /// <summary>
        /// Update the Button
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (Clicked) Sprite.SetAnimation("clicked");
            else if (Hovered) Sprite.SetAnimation("hovered");
            else Sprite.SetAnimation("default");
        }
    }
}
