using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using Microsoft.Xna.Framework.Input;


/*
 * Who has worked on this file:
 * Emma
 */
namespace Clockwork
{
    internal abstract class UIElement
    {
        public AnimatedSprite Sprite { get; private set; }
        public Rectangle Rectangle { get; set; }
        public Vector2 Position { get => new Vector2(Rectangle.X, Rectangle.Y); }
        public Vector2 Size { get => new Vector2(Rectangle.Width, Rectangle.Height); }
        public bool Hovered { get; private set; }
        public bool Clicked { get; private set; }

        public UIElement(Sprites spriteName, Rectangle rectangle)
        {
            Sprite = UILoader.GetSprite(spriteName);
            Sprite.Location = rectangle.Location;
            Rectangle = rectangle;
        }

        public UIElement(Sprites spriteName, Vector2 position, Vector2 size) :
            this(spriteName, new Rectangle(position.ToPoint(), size.ToPoint()))
        { }

        public UIElement(Sprites spriteName, Point position, Point size) :
            this(spriteName, new Rectangle(position, size))
        { }

        public virtual void Update()
        {
            // Check if mouse is hovering the UIElement
            Hovered = Rectangle.Contains(Mouse.GetState().Position);
            // Check if the mouse clicked on the UIELement
            if (Hovered) Clicked = (Mouse.GetState().LeftButton == ButtonState.Pressed);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            Sprite.Draw(sb);
        }

        /// <summary>
        /// Draw the sprite at the current frame
        /// </summary>
        /// <param name="sb"></param
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="spriteEffects"></param>
        /// <param name="layer"></param>
        public virtual void Draw(SpriteBatch sb, Color color, float rotation, SpriteEffects spriteEffects, float layer)
        {
            Sprite.Draw(sb, color, rotation, spriteEffects, layer);
        }
    }
}
