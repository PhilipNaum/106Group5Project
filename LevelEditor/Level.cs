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
        private Dictionary<int, Point> collectibles;

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
        public int[,] Map { get => map; set { map = value; } }

        /// <summary>
        /// list of collectibles (keyed by index of Objects.CollectibleTypes)
        /// </summary>
        public Dictionary<int, Point> Collectibles { get => collectibles; set { collectibles = value; } }

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
            collectibles = new Dictionary<int, Point>();
        }
    }
}
