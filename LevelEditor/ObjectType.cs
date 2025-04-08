namespace LevelEditor
{
    /// <summary>
    /// a type of object (tile or collectible)
    /// </summary>
    internal class ObjectType
    {
        private Image? texture;

        /// <summary>
        /// texture of the object
        /// </summary>
        public Image? Texture { get => texture; }

        /// <summary>
        /// creates the object and loads the texture
        /// </summary>
        /// <param name="textureFile">the filename of the texture</param>
        public ObjectType(string? textureFile)
        {
            if (textureFile == null)
            { texture = null; }
            else
            { texture = Image.FromFile(textureFile); }
        }
    }
}
