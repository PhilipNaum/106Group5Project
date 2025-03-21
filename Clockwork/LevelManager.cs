﻿namespace Clockwork
{
    internal class LevelManager
    {
        #region Singleton stuff
        private LevelManager() { }
        // Only one instance allowed
        private static LevelManager instance = null;

        public static LevelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LevelManager();
                }
                return instance;
            }
        }
        #endregion
    }
}
