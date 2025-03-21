using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
    /// <summary>
    /// a tile
    /// </summary>
    internal class Tile : GameObject
    {
        private const float SizeScale = 2;

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
        public Tile(Vector2 gridPosition, Vector2 size, bool collidable) : base(gridPosition * size, size, Sprites.tile)
        {
            // calculate and set position based on grid position
            Position = gridPosition * Size;

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
            if (active) { base.Draw(spriteBatch); }
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
