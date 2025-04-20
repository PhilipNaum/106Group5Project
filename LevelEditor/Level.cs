namespace LevelEditor
{
    /// <summary>
    /// a level map and collectibles
    /// </summary>
    internal class Level
    {
        /// <summary>
        /// loads a level, returns null if failed
        /// </summary>
        /// <param name="filename">the filename of the map file</param>
        /// <returns>the level</returns>
        public static Level? Load(string filename)
        {
            // try to open file stream, return null if failed
            FileStream stream;
            try { stream = File.OpenRead(filename); }
            catch (Exception) { return null; }

            BinaryReader input = new BinaryReader(stream);

            // read the level dimensions
            Size dimensions = new Size(
                input.ReadInt32(),
                input.ReadInt32()
                );

            // create an empty level with dimensions
            Level level = new Level(dimensions);

            // loop for and read each tile on the map
            for (int y = 0; y < dimensions.Height; y++)
            {
                for (int x = 0; x < dimensions.Width; x++)
                { level.map[y, x] = input.ReadByte(); }
            }

            // read the number of collectibles and loop for that
            int collectibleCount = input.ReadInt32();

            for (int i = 0; i < collectibleCount; i++)
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

            // end if at the end of file
            if (input.BaseStream.Position >= input.BaseStream.Length)
            {
                input.Close();
                return level;
            }

            // read the start position
            level.start = new Point(
                input.ReadInt32(),
                input.ReadInt32()
                );

            // read the exit position
            level.exit = new Point(
                input.ReadInt32(),
                input.ReadInt32()
                );

            input.Close();

            return level;
        }

        private Size dimensions;
        private int[,] map;
        private Dictionary<Point, int> collectibles;
        private Point start;
        private Point exit;

        /// <summary>
        /// the dimensions of the level
        /// </summary>
        public Size Dimensions { get => dimensions; }

        /// <summary>
        /// width of the level
        /// </summary>
        public int Width { get => dimensions.Width; }

        /// <summary>
        /// height of the level
        /// </summary>
        public int Height { get => dimensions.Height; }

        /// <summary>
        /// map for tiles (in index of Objects.TileTypes)
        /// </summary>
        public int[,] Map { get => map; }

        /// <summary>
        /// list of collectibles (value as index of Objects.CollectibleTypes)
        /// </summary>
        public Dictionary<Point, int> Collectibles { get => collectibles; }

        /// <summary>
        /// the start for the level
        /// </summary>
        public Point Start { get => start; set { start = value; } }

        /// <summary>
        /// the exit for the level
        /// </summary>
        public Point Exit { get => exit; set { exit = value; } }

        /// <summary>
        /// creates a blank level
        /// </summary>
        /// <param name="dimensions">dimensions of the level</param>
        public Level(Size dimensions)
        {
            this.dimensions = dimensions;

            map = new int[dimensions.Height, dimensions.Width];

            collectibles = new Dictionary<Point, int>();

            start = new Point(-1, -1);
            exit = new Point(-1, -1);
        }

        /// <summary>
        /// creates a blank level
        /// </summary>
        /// <param name="width">width of the level</param>
        /// <param name="height">height of the level</param>
        public Level(int width, int height) : this(new Size(width, height)) { }

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
            => collectibles.ContainsKey(new Point(x, y)) ? Objects.CollectibleTypes[collectibles[new Point(x, y)]] : null;

        /// <summary>
        /// sets the collectible at position
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="collectible">collectible object to set</param>
        public void SetCollectibleAt(int x, int y, ObjectType collectible)
        { collectibles[new Point(x, y)] = Array.IndexOf(Objects.CollectibleTypes, collectible); }

        public void RemoveCollectibleAt(int x, int y)
        { collectibles.Remove(new Point(x, y)); }

        /// <summary>
        /// checks if a position is in range of the map
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>whether the position is in the map</returns>
        public bool IsPositionInMap(int x, int y) =>
          x >= 0 &&
          y >= 0 &&
          x < Width &&
          y < Height;

        /// <summary>
        /// checks if a position is in range of the map
        /// </summary>
        /// <param name="position">position to check</param>
        /// <returns>whether the position is in the map</returns>
        public bool IsPositionInMap(Point position) => IsPositionInMap(position.X, position.Y);

        /// <summary>
        /// saves the level
        /// </summary>
        /// <param name="filename">filename to save as</param>
        /// <returns>success</returns>
        public bool Save(string filename)
        {
            // try to open file stream, return false if failed
            FileStream stream;
            try { stream = File.OpenWrite(filename); }
            catch (Exception) { return false; }

            BinaryWriter output = new BinaryWriter(stream);

            // write dimensions
            output.Write(Width);
            output.Write(Height);

            // write all tiles
            foreach (int tile in map)
            { output.Write((byte)tile); }

            // write the number of collectibles
            output.Write(collectibles.Count);

            // loop for each pair in collectibles
            foreach (KeyValuePair<Point, int> collectiblePair in collectibles)
            {
                // write collectible
                output.Write((byte)collectiblePair.Value);

                // write collection position
                output.Write(collectiblePair.Key.X);
                output.Write(collectiblePair.Key.Y);
            }

            // write start position
            output.Write(start.X);
            output.Write(start.Y);

            // write exit position
            output.Write(exit.X);
            output.Write(exit.Y);

            output.Close();

            return true;
        }
    }
}
