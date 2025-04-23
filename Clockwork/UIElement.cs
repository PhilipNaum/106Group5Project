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
        // === Properties

        /// <summary>
        /// Sprite of the Element
        /// </summary>
        public AnimatedSprite Sprite { get; private set; }

        /// <summary>
        /// Body Rectangle of the Element
        /// </summary>
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Position of the Element
        /// </summary>
        public Vector2 Position { get => new Vector2(Rectangle.X, Rectangle.Y); }

        /// <summary>
        /// Size of the Element
        /// </summary>
        public Vector2 Size { get => new Vector2(Rectangle.Width, Rectangle.Height); }

        /// <summary>
        /// Is the Element being hovered over by the mouse
        /// </summary>
        public bool Hovered { get; private set; }

        /// <summary>
        /// Is the Element being clicked by the mouse
        /// </summary>
        public bool Clicked { get; private set; }

        public bool Activated { get; private set; }


        // === Constructors ===

        /// <summary>
        /// Create a new UIElement
        /// </summary>
        /// <param name="spriteName">Sprite the Element has</param>
        /// <param name="rectangle">Body Rectangle of the Element</param>
        public UIElement(Sprites spriteName, Rectangle rectangle)
        {
            Sprite = UILoader.GetSprite(spriteName);
            Sprite.Location = rectangle.Location;
            Rectangle = rectangle;
        }

        /// <summary>
        /// Create a new UIElement
        /// </summary>
        /// <param name="spriteName">Sprite the Element has</param>
        /// <param name="position">Position of the Element</param>
        /// <param name="size">Size of the Element</param>
        public UIElement(Sprites spriteName, Vector2 position, Vector2 size) :
            this(spriteName, new Rectangle(position.ToPoint(), size.ToPoint()))
        { }

        /// <summary>
        /// Create a new UIElement
        /// </summary>
        /// <param name="spriteName">Sprite the Element has</param>
        /// <param name="position">Position of the Element</param>
        /// <param name="size">Size of the Element</param>
        public UIElement(Sprites spriteName, Point position, Point size) :
            this(spriteName, new Rectangle(position, size))
        { }


        // === Methods ===

        /// <summary>
        /// Update the Element
        /// </summary>
        public virtual void Update()
        {
            this.ResetState();
            Sprite.Location = Rectangle.Location;
            // Check if mouse is hovering the UIElement
            Hovered = Rectangle.Contains(Mouse.GetState().Position);
            // Check if the mouse clicked on the UIELement
            if (Hovered) Clicked = Game1.MouseState.LeftButton == ButtonState.Pressed;
            if (Hovered) Activated = Game1.LeftClickRelease();
        }

        /// <summary>
        /// Draw the Element at the current frame
        /// </summary>
        /// <param name="sb">SpriteBatch to draw with</param>
        public virtual void Draw(SpriteBatch sb)
        {
            Sprite.Draw(sb);
        }

        /// <summary>
        /// Draw the Element at the current frame
        /// </summary>
        /// <param name="sb">SpriteBatch to draw with</param>
        /// <param name="color">Color to overlay</param>
        /// <param name="rotation">Rotation of the Element</param>
        /// <param name="spriteEffects">Effects on the Element</param>
        /// <param name="layer">Layer to draw on</param>
        public virtual void Draw(SpriteBatch sb, float scale, Color color, float rotation, SpriteEffects spriteEffects, float layer)
        {
            Sprite.Draw(sb, scale, color, rotation, spriteEffects, layer);
        }

        /// <summary>
        /// Resets the Hovered and Clicked states to false
        /// </summary>
        private void ResetState()
        {
            Hovered = false;
            Clicked = false;
            Activated = false;
        }
    }
}
