using System.Collections.Generic;
using System.Numerics;

namespace Clockwork
{
    /// <summary>
    /// a level
    /// </summary>
    internal class Level
    {
        private Tile[,] map;
        private List<Collectible> collectibles;

        /// <summary>
        /// the tile map for the level
        /// </summary>
        internal Tile[,] Map { get => map; }

        /// <summary>
        /// a list of collectibles in the level
        /// </summary>
        internal List<Collectible> Collectibles { get => collectibles; }

        /// <summary>
        /// creates an empty level
        /// </summary>
        /// <param name="mapDimensions">the dimensions of the map</param>
        public Level(Vector2 mapDimensions)
        {
            map = new Tile[(int)mapDimensions.Y, (int)mapDimensions.X];

            collectibles = new List<Collectible>();
        }
    }
}
