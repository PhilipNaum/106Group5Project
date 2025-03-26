/*
 * Who has worked on this file:
 * Nathan
 */
namespace Clockwork
{
    /// <summary>
    /// a type of tile
    /// </summary>
    internal class TileType
    {
        private bool breakable;
        private bool collidable;
        private Sprites tileSprite;

        /// <summary>
        /// whether the tile can be broken
        /// </summary>
        public bool Breakable { get => breakable; }

        /// <summary>
        /// whether the tile can be collided with
        /// </summary>
        public bool Collidable { get => collidable; }

        /// <summary>
        /// the sprite for the tile
        /// </summary>
        public Sprites TileSprite { get => tileSprite; }

        /// <summary>
        /// creates a tile
        /// </summary>
        /// <param name="breakable">whether the tile is breakable</param>
        /// <param name="collidable">whether the tile is collidable</param>
        /// <param name="tileSprite">the sprite enum for the tile</param>
        public TileType(bool breakable, bool collidable, Sprites tileSprite)
        {
            this.breakable = breakable;
            this.collidable = collidable;
            this.tileSprite = tileSprite;
        }
    }
}
