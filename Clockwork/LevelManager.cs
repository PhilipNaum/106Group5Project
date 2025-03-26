namespace Clockwork
{
    internal class LevelManager
    {
        #region Singleton stuff

        private LevelManager() { }

        private static LevelManager instance = null;

        public static LevelManager Instance
        {
            get
            {
                if (instance == null)
                { instance = new LevelManager(); }

                return instance;
            }
        }

        #endregion

        /// <summary>
        /// an array of all tile types
        /// </summary>
        private static TileType[] tileTypes = {
        };

        /// <summary>
        /// an array of all filenames of levels
        /// </summary>
        private static string[] levelFilenames = {
        };
    }
}
