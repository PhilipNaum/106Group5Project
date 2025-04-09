/*
 * Who has worked on this file:
 * Nathan
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
    /// <summary>
    /// a tile
    /// </summary>
    internal class Tile : GameObject
    {
        /// <summary>
        /// the length of a tile
        /// </summary>
        public const int TileLength = 32;
        // this has to match the size of the tile sprite

        private TileType tileType;
        private bool active;
        private Point gridPosition;

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
        public Point GridPosition { get => gridPosition; }

        /// <summary>
        /// creates a tile
        /// </summary>
        /// <param name="tileType">the tile type</param>
        /// <param name="gridPosition">the position of the tile on the level grid</param>
        public Tile(TileType tileType, Point gridPosition)
            : base(gridPosition.ToVector2() * new Vector2(TileLength), new Vector2(TileLength), tileType.TileSprite)
        {
            // calculate and set position based on grid position
            Position = gridPosition.ToVector2() * TileLength;
            
            // set active
            active = true;

            // set tile type
            this.tileType = tileType;

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
