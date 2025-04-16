namespace LevelEditor
{
    /// <summary>
    /// category of the object type
    /// </summary>
    enum ObjectCategory
    {
        Tile,
        Collectible,
        Exit
    }

    /// <summary>
    /// a type of object (tile or collectible)
    /// </summary>
    internal class ObjectType
    {
        private Image? texture;
        private ObjectCategory category;

        /// <summary>
        /// texture of the object
        /// </summary>
        public Image? Texture { get => texture; }

        /// <summary>
        /// the category of the object
        /// </summary>
        public ObjectCategory Category { get => category; }

        /// <summary>
        /// creates the object and loads the texture
        /// </summary>
        /// <param name="textureFile">the filename of the texture</param>
        public ObjectType(string? textureFile, ObjectCategory category)
        {
            this.category = category;

            if (textureFile == null)
            { texture = null; }
            else
            { texture = Image.FromFile(textureFile); }
        }
    }
}
