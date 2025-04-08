namespace LevelEditor
{
    public partial class Form1 : Form
    {
        private const int mapPadding = 30;

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
            int tileLength = Math.Min(
                groupBoxMap.Width - 2 * mapPadding / dimensions.Width,
                groupBoxMap.Height - 2 * mapPadding / dimensions.Height
                );

            // loop for each tile on the map
            for (int y = 0; y < dimensions.Height; y++)
            {
                for (int x = 0; x < dimensions.Width; x++)
                {
                    // create picture box
                    PictureBox tile = new PictureBox();

                    // set up tile
                    tile.Location = new Point(
                        x * tileLength + mapPadding,
                        y * tileLength + mapPadding
                        );
                    tile.Size = new Size(
                        tileLength,
                        tileLength
                        );

                    // add click response
                    //tile.Click += pictureBoxMapTile_Click;

                    // add tile to group box
                    groupBoxMap.Controls.Add(tile);

                    // add tile to map
                    pictureBoxMap[y, x] = tile;
                }
            }
        }

        private void buttonNewMap_Click(object sender, EventArgs e)
        { new FormNewMap(InitializeMap).ShowDialog(); }
    }
}
