namespace LevelEditor
{
    /// <summary>
    /// keeps track of possible objects
    /// </summary>
    internal class Objects
    {
        private static ObjectType[] tileTypes =
        {
        };

        private static ObjectType[] collectibleTypes =
        {
        };

        /// <summary>
        /// array of all tile types
        /// </summary>
        internal static ObjectType[] TileTypes { get => tileTypes; }

        /// <summary>
        /// array of all collectible types
        /// </summary>
        internal static ObjectType[] CollectibleTypes { get => collectibleTypes; }
    }
}
