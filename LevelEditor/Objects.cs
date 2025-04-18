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
            new ObjectType("Textures/Tiles/tileGrassTopEndL.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGrassToDirtTop.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileDirtTop.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileDirtToGrassTop.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGrassTop.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGrassToShadeTop.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileShadeTop.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileShadeToGrassTop.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGrassTopEndR.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGrassEndLBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGrassToDirtBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileDirtBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileDirtToGrassBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGrassBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGrassToShadeBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileShadeBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileShadeToGrassBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGrassEndRBottom.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundEndL.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundRocks1.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundRocksLeaves.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundEmpty.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundRocks2.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundEndR.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundBottomEndL.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundRocks3.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundRocks4.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileGroundBottomEndR.png", ObjectCategory.Tile),
            new ObjectType("Textures/Tiles/tileDestructible.png", ObjectCategory.Tile)
        };

        private static ObjectType[] collectibleTypes =
        {
            new ObjectType("Textures/Collectibles/Gear.png", ObjectCategory.Collectible),
            new ObjectType("Textures/Collectibles/Hand.png", ObjectCategory.Collectible),
            new ObjectType("Textures/Collectibles/Face.png", ObjectCategory.Collectible),
            new ObjectType("Textures/Collectibles/Key.png", ObjectCategory.Collectible),
            new ObjectType("Textures/Collectibles/Chime.png", ObjectCategory.Collectible),
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
