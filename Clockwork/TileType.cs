using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
    /// <summary>
    /// a type of tile
    /// </summary>
    internal class TileType
    {
        private bool breakable;
        private bool collidable;
        private Texture2D texture;

        /// <summary>
        /// whether the tile can be broken
        /// </summary>
        public bool Breakable { get => breakable; set => breakable = value; }

        /// <summary>
        /// whether the tile can be collided with
        /// </summary>
        public bool Collidable { get => collidable; set => collidable = value; }

        /// <summary>
        /// the texture of the tile
        /// </summary>
        public Texture2D Texture { get => texture; set => texture = value; }

        /// <summary>
        /// creates a tile
        /// </summary>
        /// <param name="breakable">whether the tile is breakable</param>
        /// <param name="collidable">whether the tile is collidable</param>
        /// <param name="texture">the texure of the tile</param>
        public TileType(bool breakable, bool collidable, Texture2D texture)
        {
            this.breakable = breakable;
            this.collidable = collidable;
            this.texture = texture;
        }
    }
}
