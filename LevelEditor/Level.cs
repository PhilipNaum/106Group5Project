namespace LevelEditor
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
    }
}
