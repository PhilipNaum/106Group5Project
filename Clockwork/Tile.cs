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
        /// creates a tile
        /// </summary>
        /// <param name="texture">the texture for the tile</param>
        /// <param name="gridPosition">the position of the tile on the level grid</param>
        /// <param name="collidable">whether this tile is collidable</param>
        public Tile(Texture2D texture, Vector2 gridPosition, bool collidable)
        {
            // set size to size of the texture
            Size = new Vector2(texture.Width, texture.Height);

            // calculate and set position based on grid position
            Position = gridPosition * Size;

            // set texture
            Texture = texture;

            // set active
            active = true;

            // set collidable
            this.collidable = collidable;

            // set grid position
            this.gridPosition = gridPosition;
        }

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
