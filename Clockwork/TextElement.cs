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
    internal class TextElement : UIElement
    {
        // === Properties ===

        /// <summary>
        /// Text the Element displays
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Font the Element uses
        /// </summary>
        public SpriteFont Font { get; set; }


        // === Constructors ===

        /// <summary>
        /// Create a new TextElement object
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="font">Font to use</param>
        /// <param name="rectangle">Body Rectangle of the Element</param>
        public TextElement(string text, SpriteFont font, Rectangle rectangle) : base(Sprites.Empty, rectangle)
        {
            Text = text;
            Font = font;
        }

        /// <summary>
        /// Create a new TextElement object
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="font">Font to use</param>
        /// <param name="position">Position of the Element</param>
        /// <param name="size">Size of the Element</param>
        public TextElement(string text, SpriteFont font, Vector2 position, Vector2 size) :
            this(text, font, new Rectangle(position.ToPoint(), size.ToPoint()))
        { }

        /// <summary>
        /// Create a new TextElement object
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="font">Font to use</param>
        /// <param name="position">Position of the Element</param>
        /// <param name="size">Size of the Element</param>
        public TextElement(string text, SpriteFont font, Point position, Point size) :
            this(text, font, new Rectangle(position, size))
        { }

        /// <summary>
        /// Create a new TextElement object
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="font">Font to use</param>
        /// <param name="spriteName">Background Sprite to use</param>
        /// <param name="rectangle">Body Rectangle of the Element</param>
        public TextElement(string text, SpriteFont font, Sprites spriteName, Rectangle rectangle) : base(spriteName, rectangle)
        {
            Text = text;
            Font = font;
        }

        /// <summary>
        /// Create a new TextElement object
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="font">Font to use</param>
        /// <param name="spriteName">Background Sprite to use</param>
        /// <param name="position">Position of the Element</param>
        /// <param name="size">Size of the Element</param>
        public TextElement(string text, SpriteFont font, Sprites spriteName, Vector2 position, Vector2 size) :
            this(text, font, spriteName, new Rectangle(position.ToPoint(), size.ToPoint()))
        { }

        /// <summary>
        /// Create a new TextElement object
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="font">Font to use</param>
        /// <param name="spriteName">Background Sprite to use</param>
        /// <param name="position">Position of the Element</param>
        /// <param name="size">Size of the Element</param>
        public TextElement(string text, SpriteFont font, Sprites spriteName, Point position, Point size) :
            this(text, font, spriteName, new Rectangle(position, size))
        { }


        // === Methods ===

        /// <summary>
        /// Draw the Element at the current frame
        /// </summary>
        /// <param name="sb">SpriteBatch to draw with</param>
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sb.DrawString(Font, Text, Position, Color.White);


        }

        /// <summary>
        /// Draw the Element at the current frame
        /// </summary>
        /// <param name="sb">SpriteBatch to draw with</param>
        /// <param name="color">Color to overlay</param>
        /// <param name="rotation">Rotation of the Element</param>
        /// <param name="spriteEffects">Effects on the Element</param>
        /// <param name="layer">Layer to draw on</param>
        public override void Draw(SpriteBatch sb, float scale, Color color, float rotation, SpriteEffects spriteEffects, float layer)
        {
            base.Draw(sb, scale, color, rotation, spriteEffects, layer);
            sb.DrawString(Font, Text, Position, color);
        }
    }
}
