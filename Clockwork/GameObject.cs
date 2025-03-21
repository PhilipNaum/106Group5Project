﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
    internal abstract class GameObject
    {
        private Vector2 position;
        private Vector2 size;
        private Texture2D texture;

        /// <summary>
        /// position of the object
        /// </summary>
        public Vector2 Position { get => position; set { position = value; } }

        /// <summary>
        /// size of the object
        /// </summary>
        public Vector2 Size { get => size; protected set { size = value; } }

        /// <summary>
        /// texture of the object
        /// </summary>
        public Texture2D Texture { get => texture; protected set { texture = value; } }

        /// <summary>
        /// updates the object states
        /// </summary>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// draws the object
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch) { }

        /// <summary>
        /// checks if the object is colliding with another object
        /// </summary>
        /// <param name="other">the object to check</param>
        /// <returns>whether or not it is colliding</returns>
        public virtual bool IsColliding(GameObject other) => createRectangle().Intersects(other.createRectangle());

        /// <summary>
        /// creates a rectangle out of the object's position and size
        /// </summary>
        /// <returns>the rectangle</returns>
        public Rectangle createRectangle() => new Rectangle(position.ToPoint(), size.ToPoint());
    }
}
