using Microsoft.Xna.Framework;

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
        public bool Collidable { get => collidable; set { collidable = value; } }

        /// <summary>
        /// the position of the tile on the level grid
        /// </summary>
        public Vector2 GridPosition { get => gridPosition; }
    }
}
