using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationHelper;
using Microsoft.Xna.Framework.Input;

namespace Clockwork
{
    internal class TextElement : UIElement
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }

        public TextElement(string text, SpriteFont font, Rectangle rectangle) : base(Sprites.Empty, rectangle)
        {
            Text = text;
            Font = font;
        }

        public TextElement(string text, SpriteFont font, Vector2 position, Vector2 size) :
            this(text, font, new Rectangle(position.ToPoint(), size.ToPoint()))
        { }

        public TextElement(string text, SpriteFont font, Point position, Point size) :
            this(text, font, new Rectangle(position, size))
        { }

        public TextElement(string text, SpriteFont font, Sprites spriteName, Rectangle rectangle) : base(spriteName, rectangle)
        {
            Text = text;
            Font = font;
        }

        public TextElement(string text, SpriteFont font, Sprites spriteName, Vector2 position, Vector2 size) :
            this(text, font, spriteName, new Rectangle(position.ToPoint(), size.ToPoint()))
        { }

        public TextElement(string text, SpriteFont font, Sprites spriteName, Point position, Point size) :
            this(text, font, spriteName, new Rectangle(position, size))
        { }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sb.DrawString(Font, Text, Position, Color.White);


        }

        /// <summary>
        /// Draw the sprite at the current frame
        /// </summary>
        /// <param name="sb"></param
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="spriteEffects"></param>
        /// <param name="layer"></param>
        public override void Draw(SpriteBatch sb, Color color, float rotation, SpriteEffects spriteEffects, float layer)
        {
            base.Draw(sb, color, rotation, spriteEffects, layer);
            sb.DrawString(Font, Text, Position, color);
        }
    }
}
