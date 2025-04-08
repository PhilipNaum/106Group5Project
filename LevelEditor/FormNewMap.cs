namespace LevelEditor
{
    public partial class FormNewMap : Form
    {
        /// <summary>
        /// the maximum width and height of the map
        /// </summary>
        private const int MaximumMapLength = 100;

        public delegate void CreateMap(Size dimensions);

        private event CreateMap MapCreated;

        public FormNewMap(CreateMap mapCreatedHandler)
        {
            InitializeComponent();

            MapCreated += mapCreatedHandler;
        }

        /// <summary>
        /// tries to parse the entered dimensions
        /// </summary>
        /// <returns>the dimensions, null if invalid</returns>
        private Size? TryParseDimensions()
        {
            // try to parse width
            int width;
            if (!int.TryParse(textBoxWidth.Text, out width))
            { return null; }

            // try to parse height
            int height;
            if (!int.TryParse(textBoxHeight.Text, out height))
            { return null; }

            return new Size(width, height);
        }

        /// <summary>
        /// makes sure dimensions are valid and in range
        /// </summary>
        /// <returns>whether the dimensions are in range</returns>
        private bool ValidateDimensions()
        {
            // try to parse dimensions, or set to an out of range size
            Size dimensions = TryParseDimensions() ?? new Size(-1, -1);

            // check if dimensions are in range
            if (
                dimensions.Width <= 0 ||
                dimensions.Width > MaximumMapLength ||
                dimensions.Width <= 0 ||
                dimensions.Height > MaximumMapLength
                ) { return false; }

            return true;
        }

        private void TryCreateMap()
        {
            // if invalid
            if (!ValidateDimensions())
            {
                // show error message
                MessageBox.Show(
                    "dimensions invalid",
                    "error creating map",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );

                return;
            }

            // send event
            MapCreated(TryParseDimensions() ?? new Size());

            // close window
            Close();
        }

        /// <summary>
        /// when the create button is clicked
        /// </summary>
        private void buttonCreate_Click(object sender, EventArgs e) { TryCreateMap(); }
    }
}
