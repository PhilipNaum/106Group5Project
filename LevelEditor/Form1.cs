namespace LevelEditor
{
    public partial class Form1 : Form
    {
        private Level? level;
        private PictureBox[,]? pictureBoxMap;
        private ObjectType? selected;
        private bool unsaved;

        public Form1() { InitializeComponent(); }

        /// <summary>
        /// sets up an empty map
        /// </summary>
        /// <param name="dimensions">dimensions of the map</param>
        private void InitializeMap(Size dimensions)
        {
            // create an empty level
            level = new Level(dimensions);

            // create a picture box map
            pictureBoxMap = new PictureBox[dimensions.Height, dimensions.Width];

            // calculate the length of tiles for the picture boxes
            int largestDimension = dimensions.Width > dimensions.Height ? dimensions.Width : dimensions.Height;


        }
    }
}
