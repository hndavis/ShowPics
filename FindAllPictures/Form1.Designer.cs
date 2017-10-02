namespace FindAllPictures
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.pictureList = new System.Windows.Forms.CheckedListBox();
            this.picDisplay = new System.Windows.Forms.PictureBox();
            this.Load = new System.Windows.Forms.Button();
            this.pbSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 710);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Find";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureList
            // 
            this.pictureList.FormattingEnabled = true;
            this.pictureList.Location = new System.Drawing.Point(12, 76);
            this.pictureList.Name = "pictureList";
            this.pictureList.Size = new System.Drawing.Size(397, 619);
            this.pictureList.TabIndex = 1;
            this.pictureList.SelectedIndexChanged += new System.EventHandler(this.pictureList_SelectedIndexChanged);
            // 
            // picDisplay
            // 
            this.picDisplay.Location = new System.Drawing.Point(442, 35);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(1316, 812);
            this.picDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDisplay.TabIndex = 2;
            this.picDisplay.TabStop = false;
            this.picDisplay.Click += new System.EventHandler(this.picDisplay_Click);
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(144, 710);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(75, 23);
            this.Load.TabIndex = 3;
            this.Load.Text = "Load";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.button2_Click);
            // 
            // pbSave
            // 
            this.pbSave.Location = new System.Drawing.Point(291, 710);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(75, 23);
            this.pbSave.TabIndex = 4;
            this.pbSave.Text = "Save";
            this.pbSave.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1801, 850);
            this.Controls.Add(this.pbSave);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.picDisplay);
            this.Controls.Add(this.pictureList);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox pictureList;
        private System.Windows.Forms.PictureBox picDisplay;
        private System.Windows.Forms.Button Load;
        private System.Windows.Forms.Button pbSave;
    }
}

