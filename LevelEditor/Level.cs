﻿namespace LevelEditor
{
    /// <summary>
    /// a level map and collectibles
    /// </summary>
    internal class Level
    {
        private int width;
        private int height;
        private int[,] map;
        private Dictionary<Point, int> collectibles;

        /// <summary>
        /// width of the level
        /// </summary>
        public int Width { get => width; }

        /// <summary>
        /// height of the level
        /// </summary>
        public int Height { get => height; }

        /// <summary>
        /// map for tiles (in index of Objects.TileTypes)
        /// </summary>
        public int[,] Map { get => map; }

        /// <summary>
        /// list of collectibles (value as index of Objects.CollectibleTypes)
        /// </summary>
        public Dictionary<Point, int> Collectibles { get => collectibles; }

        /// <summary>
        /// creates a blank level
        /// </summary>
        /// <param name="width">width of the level</param>
        /// <param name="height">height of the level</param>
        public Level(int width, int height)
        {
            // set dimensions
            this.width = width;
            this.height = height;

            // create empty map
            map = new int[height, width];

            // create list of collectibles
            collectibles = new Dictionary<Point, int>();
        }

        /// <summary>
        /// gets the tile at position
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>retrieved tile object</returns>
        public ObjectType GetTileAt(int x, int y) => Objects.TileTypes[map[y, x]];

        /// <summary>
        /// sets the tile at position
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="tile">tile object to set</param>
        public void SetTileAt(int x, int y, ObjectType tile)
        { map[y, x] = Array.IndexOf(Objects.TileTypes, tile); }

        /// <summary>
        /// gets the collectible at position
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>retrieved collectible object</returns>
        public ObjectType? GetCollectibleAt(int x, int y)
            => Collectibles.ContainsKey(new Point(x, y)) ? Objects.CollectibleTypes[Collectibles[new Point(x, y)]] : null;

        /// <summary>
        /// sets the collectible at position
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="collectible">collectible object to set</param>
        public void SetCollectibleAt(int x, int y, ObjectType collectible)
        { Collectibles[new Point(x, y)] = Array.IndexOf(Objects.CollectibleTypes, collectible); }

        /// <summary>
        /// loads a level, returns null if failed
        /// </summary>
        /// <param name="filename">the filename of the map file</param>
        /// <returns>the level</returns>
        public static Level? LoadLevel(string filename)
        {
            // try to open file stream, return null if failed
            FileStream stream;
            try { stream = File.OpenRead(filename); }
            catch (Exception) { return null; }

            BinaryReader input = new BinaryReader(stream);

            // read the level dimensions
            int width = input.ReadInt32();
            int height = input.ReadInt32();

            // create an empty level with dimensions
            Level level = new Level(width, height);

            // loop for and read each tile on the map
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                { level.Map[y, x] = input.ReadByte(); }
            }

            // read the number of collectibles and loop for that
            for (int i = 0; i < input.ReadInt32(); i++)
            {
                // read collectible information
                int collectible = input.ReadByte();
                Point collectiblePosition = new Point(
                    input.ReadInt32(),
                    input.ReadInt32()
                    );

                // add collectible to list
                level.collectibles.Add(collectiblePosition, collectible);
            }

            input.Close();

            return level;
        }
    }
}
