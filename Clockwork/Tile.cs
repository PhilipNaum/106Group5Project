using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
    /// <summary>
    /// a tile
    /// </summary>
    internal class Tile : GameObject
    {
        private bool active;
        private bool collidable;
        private Vector2 gridPosition;

        /// <summary>
        /// whether the tile is active (not destroyed)
        /// </summary>
        public bool Active { get => active; set { active = value; } }

        /// <summary>
        /// whether the tile is collidable
        /// </summary>
        public bool Collidable { get => collidable; }

        /// <summary>
        /// the position of the tile on the level grid
        /// </summary>
        public Vector2 GridPosition { get => gridPosition; }

        /// <summary>
        /// draws the tile
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // draw the tile if active
            if (active) { spriteBatch.Draw(Texture, createRectangle(), Color.White); }
        }

        /// <summary>
        /// checks collision of the tile with another object, returns false if it's inactive or non-collidable
        /// </summary>
        /// <param name="other">the object to check</param>
        /// <returns>whether or not it is colliding</returns>
        public override bool IsColliding(GameObject other)
            => active && collidable ?
            base.IsColliding(other) :
            false
            ;
    }
}
