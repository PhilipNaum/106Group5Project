/*
 * Who has worked on this file:
 * Nathan
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Clockwork
{
    /// <summary>
    /// a level
    /// </summary>
    internal class Level
    {
        private Tile[,] map;
        private List<Tile> collidableTiles;
        private List<Collectible> collectibles;

        /// <summary>
        /// the tile map for the level
        /// </summary>
        public Tile[,] Map { get => map; }

        /// <summary>
        /// a list of the collidable tiles in the level, used for collision checks
        /// </summary>
        public List<Tile> CollidableTiles { get => collidableTiles; }

        /// <summary>
        /// a list of collectibles in the level
        /// </summary>
        public List<Collectible> Collectibles { get => collectibles; }

        /// <summary>
        /// creates an empty level
        /// </summary>
        /// <param name="mapDimensions">the dimensions of the map</param>
        public Level(Point mapDimensions)
        {
            map = new Tile[mapDimensions.Y, mapDimensions.X];

            collectibles = new List<Collectible>();
            collidableTiles = new List<Tile>();
        }

        /// <summary>
        /// draws all objects in the level
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            // draw all tiles
            foreach (Tile tile in map) { tile.Draw(spriteBatch); }

            // draw all collectibles
            foreach (Collectible collectible in collectibles) { collectible.Draw(spriteBatch); }
        }

        /// <summary>
        /// updates the collectibles
        /// </summary>
        public void Update(GameTime gameTime)
        { 
            foreach (Collectible collectible in collectibles) 
            { 
                collectible.Update(gameTime); 
            }
            foreach (Tile tile in map) 
            { 
                tile.Update(gameTime); 
            }
        }
    }
}
