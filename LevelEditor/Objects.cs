namespace LevelEditor
{
    /// <summary>
    /// keeps track of possible objects
    /// </summary>
    internal class Objects
    {
        private static ObjectType[] tileTypes =
        {
            new ObjectType(null, ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/DirtL.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/DirtToGrassR.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/Grass.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GrassDark.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GrassDarkToLight.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GrassEndL.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GrassEndR.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GrassLightToDark.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GrassToDirtL.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundBlank.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundEndL1.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundEndL2.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundEndR1.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundEndR2.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundRocks.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundRocks2.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundRocksLeavesBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundRocksLeavesTop.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundTop1.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundTop2.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundTop3.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundTopRocks.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundTopRocksVines.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/GroundTopVines1.png", ObjectCategory.Tile),
        };

        private static ObjectType[] collectibleTypes =
        {
            new ObjectType("Textures/Collectibles/Gear.png", ObjectCategory.Collectible),
            new ObjectType("Textures/Collectibles/Hand.png", ObjectCategory.Collectible),
            new ObjectType("Textures/Collectibles/Face.png", ObjectCategory.Collectible),
            new ObjectType("Textures/Collectibles/Key.png", ObjectCategory.Collectible),
            new ObjectType("Textures/Collectibles/Chime.png", ObjectCategory.Collectible),
        };

        private static ObjectType exit = new ObjectType("Textures/Other/Exit.png", ObjectCategory.Exit);

        /// <summary>
        /// array of all tile types
        /// </summary>
        public static ObjectType[] TileTypes { get => tileTypes; }

        /// <summary>
        /// array of all collectible types
        /// </summary>
        public static ObjectType[] CollectibleTypes { get => collectibleTypes; }

        /// <summary>
        /// the exit object type
        /// </summary>
        public static ObjectType Exit { get => exit; }
    }
}
