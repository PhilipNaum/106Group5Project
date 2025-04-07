namespace LevelEditor
{
    partial class FormNewMap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelWidth = new Label();
            labelHeight = new Label();
            textBoxWidth = new TextBox();
            textBoxHeight = new TextBox();
            buttonCreate = new Button();
            SuspendLayout();
            // 
            // labelWidth
            // 
            labelWidth.AutoSize = true;
            labelWidth.Font = new Font("Segoe UI", 12F);
            labelWidth.Location = new Point(12, 15);
            labelWidth.Name = "labelWidth";
            labelWidth.Size = new Size(73, 32);
            labelWidth.TabIndex = 0;
            labelWidth.Text = "width";
            // 
            // labelHeight
            // 
            labelHeight.AutoSize = true;
            labelHeight.Font = new Font("Segoe UI", 12F);
            labelHeight.Location = new Point(12, 60);
            labelHeight.Name = "labelHeight";
            labelHeight.Size = new Size(83, 32);
            labelHeight.TabIndex = 1;
            labelHeight.Text = "height";
            // 
            // textBoxWidth
            // 
            textBoxWidth.Font = new Font("Segoe UI", 12F);
            textBoxWidth.Location = new Point(101, 12);
            textBoxWidth.Name = "textBoxWidth";
            textBoxWidth.Size = new Size(200, 39);
            textBoxWidth.TabIndex = 2;
            // 
            // textBoxHeight
            // 
            textBoxHeight.Font = new Font("Segoe UI", 12F);
            textBoxHeight.Location = new Point(101, 57);
            textBoxHeight.Name = "textBoxHeight";
            textBoxHeight.Size = new Size(200, 39);
            textBoxHeight.TabIndex = 3;
            // 
            // buttonCreate
            // 
            buttonCreate.Font = new Font("Segoe UI", 12F);
            buttonCreate.Location = new Point(12, 102);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(289, 60);
            buttonCreate.TabIndex = 4;
            buttonCreate.Text = "create";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // FormNewMap
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(313, 174);
            Controls.Add(buttonCreate);
            Controls.Add(textBoxHeight);
            Controls.Add(textBoxWidth);
            Controls.Add(labelHeight);
            Controls.Add(labelWidth);
            Name = "FormNewMap";
            Text = "new map";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelWidth;
        private Label labelHeight;
        private TextBox textBoxWidth;
        private TextBox textBoxHeight;
        private Button buttonCreate;
    }
}