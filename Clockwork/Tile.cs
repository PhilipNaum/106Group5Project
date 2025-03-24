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

        private TileType tileType;
        private bool active;
        private Vector2 gridPosition;

        /// <summary>
        /// the type of the tile
        /// </summary>
        public TileType TileType { get => tileType; }

        /// <summary>
        /// whether the tile is active (not destroyed)
        /// </summary>
        public bool Active { get => active; set { active = value; } }

        /// <summary>
        /// the position of the tile on the level grid
        /// </summary>
        public Vector2 GridPosition { get => gridPosition; }

        /// <summary>
        /// creates a tile
        /// </summary>
        /// <param name="tileType">the tile type</param>
        /// <param name="gridPosition">the position of the tile on the level grid</param>
        /// <param name="collidable">whether this tile is collidable</param>
        public Tile(Vector2 gridPosition, Vector2 size, bool collidable) : base(gridPosition * size, size, Sprites.Tile)
        {
            // calculate and set position based on grid position
            Position = gridPosition * Size;
            
            // set active
            active = true;

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
            => active && tileType.Collidable ?
            base.IsColliding(other) :
            false
            ;
    }
}
