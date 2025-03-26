using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using System;

namespace Clockwork
{
    internal abstract class UIElement
    {
        public AnimatedSprite Sprite { get; private set; }
        public Rectangle Rectangle { get; set; }
        public Vector2 Position { get => new Vector2(Rectangle.X, Rectangle.Y); }
        public Vector2 Size { get => new Vector2(Rectangle.Width, Rectangle.Height); }

        public UIElement(Sprites spriteName, Rectangle rectangle)
        {
            Sprite = AnimationLoader.GetSprite(spriteName);
            Sprite.Location = rectangle.Location;
            Rectangle = rectangle;
        }

        public UIElement(Sprites spriteName, Vector2 position, Vector2 size) :
            this(spriteName, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y))
        { }

        public UIElement(Sprites spriteName, Point position, Point size) :
            this(spriteName, new Rectangle(position.X, position.Y, size.X, size.Y))
        { }
    }
}
