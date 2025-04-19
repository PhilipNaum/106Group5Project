namespace LevelEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControlSelection = new TabControl();
            tabPageTiles = new TabPage();
            tabPageItems = new TabPage();
            groupBoxMap = new GroupBox();
            groupBoxSelected = new GroupBox();
            pictureBoxSelected = new PictureBox();
            buttonSave = new Button();
            buttonLoad = new Button();
            buttonNewMap = new Button();
            openFileDialogLoadMap = new OpenFileDialog();
            saveFileDialogSaveMap = new SaveFileDialog();
            GroupBoxExtra = new GroupBox();
            buttonStart = new Button();
            buttonExit = new Button();
            tabControlSelection.SuspendLayout();
            groupBoxSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSelected).BeginInit();
            GroupBoxExtra.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlSelection
            // 
            tabControlSelection.Controls.Add(tabPageTiles);
            tabControlSelection.Controls.Add(tabPageItems);
            tabControlSelection.Location = new Point(12, 552);
            tabControlSelection.Name = "tabControlSelection";
            tabControlSelection.SelectedIndex = 0;
            tabControlSelection.Size = new Size(1154, 130);
            tabControlSelection.TabIndex = 0;
            // 
            // tabPageTiles
            // 
            tabPageTiles.AutoScroll = true;
            tabPageTiles.Location = new Point(4, 34);
            tabPageTiles.Name = "tabPageTiles";
            tabPageTiles.Padding = new Padding(3);
            tabPageTiles.Size = new Size(1146, 92);
            tabPageTiles.TabIndex = 0;
            tabPageTiles.Text = "tiles";
            tabPageTiles.UseVisualStyleBackColor = true;
            // 
            // tabPageItems
            // 
            tabPageItems.AutoScroll = true;
            tabPageItems.Location = new Point(4, 34);
            tabPageItems.Name = "tabPageItems";
            tabPageItems.Padding = new Padding(3);
            tabPageItems.Size = new Size(1146, 92);
            tabPageItems.TabIndex = 1;
            tabPageItems.Text = "items";
            tabPageItems.UseVisualStyleBackColor = true;
            // 
            // groupBoxMap
            // 
            groupBoxMap.Location = new Point(128, 12);
            groupBoxMap.Name = "groupBoxMap";
            groupBoxMap.Size = new Size(1038, 534);
            groupBoxMap.TabIndex = 1;
            groupBoxMap.TabStop = false;
            groupBoxMap.Text = "map";
            // 
            // groupBoxSelected
            // 
            groupBoxSelected.Controls.Add(pictureBoxSelected);
            groupBoxSelected.Location = new Point(12, 436);
            groupBoxSelected.Name = "groupBoxSelected";
            groupBoxSelected.Size = new Size(110, 110);
            groupBoxSelected.TabIndex = 0;
            groupBoxSelected.TabStop = false;
            groupBoxSelected.Text = "selected";
            // 
            // pictureBoxSelected
            // 
            pictureBoxSelected.Location = new Point(14, 22);
            pictureBoxSelected.Name = "pictureBoxSelected";
            pictureBoxSelected.Size = new Size(82, 82);
            pictureBoxSelected.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxSelected.TabIndex = 5;
            pictureBoxSelected.TabStop = false;
            // 
            // buttonSave
            // 
            buttonSave.Font = new Font("Segoe UI", 12F);
            buttonSave.Location = new Point(12, 12);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(110, 40);
            buttonSave.TabIndex = 2;
            buttonSave.Text = "save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonLoad
            // 
            buttonLoad.Font = new Font("Segoe UI", 12F);
            buttonLoad.Location = new Point(12, 58);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(110, 40);
            buttonLoad.TabIndex = 3;
            buttonLoad.Text = "load";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonNewMap
            // 
            buttonNewMap.Font = new Font("Segoe UI", 12F);
            buttonNewMap.Location = new Point(12, 104);
            buttonNewMap.Name = "buttonNewMap";
            buttonNewMap.Size = new Size(110, 40);
            buttonNewMap.TabIndex = 4;
            buttonNewMap.Text = "new map";
            buttonNewMap.UseVisualStyleBackColor = true;
            buttonNewMap.Click += buttonNewMap_Click;
            // 
            // openFileDialogLoadMap
            // 
            openFileDialogLoadMap.Filter = "map files|*.map";
            openFileDialogLoadMap.Title = "open map";
            // 
            // saveFileDialogSaveMap
            // 
            saveFileDialogSaveMap.Filter = "map files|*.map";
            saveFileDialogSaveMap.Title = "save map";
            // 
            // GroupBoxExtra
            // 
            GroupBoxExtra.Controls.Add(buttonExit);
            GroupBoxExtra.Controls.Add(buttonStart);
            GroupBoxExtra.Location = new Point(12, 308);
            GroupBoxExtra.Name = "GroupBoxExtra";
            GroupBoxExtra.Size = new Size(110, 122);
            GroupBoxExtra.TabIndex = 5;
            GroupBoxExtra.TabStop = false;
            GroupBoxExtra.Text = "extra";
            // 
            // buttonStart
            // 
            buttonStart.Font = new Font("Segoe UI", 12F);
            buttonStart.Location = new Point(6, 30);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(98, 40);
            buttonStart.TabIndex = 6;
            buttonStart.Text = "start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonExit
            // 
            buttonExit.Font = new Font("Segoe UI", 12F);
            buttonExit.Location = new Point(6, 76);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(98, 40);
            buttonExit.TabIndex = 7;
            buttonExit.Text = "exit";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1178, 694);
            Controls.Add(GroupBoxExtra);
            Controls.Add(buttonNewMap);
            Controls.Add(buttonLoad);
            Controls.Add(buttonSave);
            Controls.Add(groupBoxSelected);
            Controls.Add(groupBoxMap);
            Controls.Add(tabControlSelection);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "level editor";
            tabControlSelection.ResumeLayout(false);
            groupBoxSelected.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxSelected).EndInit();
            GroupBoxExtra.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlSelection;
        private TabPage tabPageTiles;
        private TabPage tabPageItems;
        private GroupBox groupBoxMap;
        private GroupBox groupBoxSelected;
        private Button buttonSave;
        private Button buttonLoad;
        private Button buttonNewMap;
        private PictureBox pictureBoxSelected;
        private OpenFileDialog openFileDialogLoadMap;
        private SaveFileDialog saveFileDialogSaveMap;
        private GroupBox GroupBoxExtra;
        private Button buttonStart;
        private Button buttonExit;
    }
}
