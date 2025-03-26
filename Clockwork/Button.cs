/*
 * Who has worked on this file:
 * Emma
 */
using Microsoft.Xna.Framework;


namespace Clockwork
{
    internal class Button : UIElement
    {
        public Button(Sprites spriteName, Rectangle rectangle) : base(spriteName, rectangle) { }

        public Button(Sprites spriteName, Vector2 position, Vector2 size) :
            this(spriteName, new Rectangle(position.ToPoint(), size.ToPoint()))
        { }

        public Button(Sprites spriteName, Point position, Point size) :
            this(spriteName, new Rectangle(position, size))
        { }

        public override void Update()
        {
            base.Update();
            if (Clicked) Sprite.SetAnimation("clicked");
            else if (Hovered) Sprite.SetAnimation("hovered");
            else Sprite.SetAnimation("default");
        }
    }
}
