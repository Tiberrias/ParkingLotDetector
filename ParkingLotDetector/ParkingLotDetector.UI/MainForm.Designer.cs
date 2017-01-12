namespace ParkingLotDetector.UI
{
    partial class MainForm
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
            this.buttonLearn = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonLoadEmpty = new System.Windows.Forms.Button();
            this.buttonLoadOccupied = new System.Windows.Forms.Button();
            this.buttonClassify = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.buttonClassifySet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(12, 41);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(94, 23);
            this.buttonLearn.TabIndex = 0;
            this.buttonLearn.Text = "Learn";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.OnLearnClick);
            // 
            // buttonLoadEmpty
            // 
            this.buttonLoadEmpty.Location = new System.Drawing.Point(12, 12);
            this.buttonLoadEmpty.Name = "buttonLoadEmpty";
            this.buttonLoadEmpty.Size = new System.Drawing.Size(94, 23);
            this.buttonLoadEmpty.TabIndex = 1;
            this.buttonLoadEmpty.Text = "Load Empty Set";
            this.buttonLoadEmpty.UseVisualStyleBackColor = true;
            this.buttonLoadEmpty.Click += new System.EventHandler(this.OnLoadEmptyClick);
            // 
            // buttonLoadOccupied
            // 
            this.buttonLoadOccupied.Location = new System.Drawing.Point(112, 12);
            this.buttonLoadOccupied.Name = "buttonLoadOccupied";
            this.buttonLoadOccupied.Size = new System.Drawing.Size(108, 23);
            this.buttonLoadOccupied.TabIndex = 2;
            this.buttonLoadOccupied.Text = "Load Occupied Set";
            this.buttonLoadOccupied.UseVisualStyleBackColor = true;
            this.buttonLoadOccupied.Click += new System.EventHandler(this.OnLoadOccupiedClick);
            // 
            // buttonClassify
            // 
            this.buttonClassify.Location = new System.Drawing.Point(112, 41);
            this.buttonClassify.Name = "buttonClassify";
            this.buttonClassify.Size = new System.Drawing.Size(108, 23);
            this.buttonClassify.TabIndex = 3;
            this.buttonClassify.Text = "Classify";
            this.buttonClassify.UseVisualStyleBackColor = true;
            this.buttonClassify.Click += new System.EventHandler(this.OnClassifyClick);
            // 
            // logBox
            // 
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logBox.Location = new System.Drawing.Point(12, 70);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(427, 180);
            this.logBox.TabIndex = 4;
            this.logBox.Text = "";
            // 
            // openImageDialog
            // 
            this.openImageDialog.FileName = "openFileDialog1";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(227, 13);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.OnClearClick);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelResult.Location = new System.Drawing.Point(226, 46);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(0, 13);
            this.labelResult.TabIndex = 6;
            // 
            // buttonClassifySet
            // 
            this.buttonClassifySet.Location = new System.Drawing.Point(308, 41);
            this.buttonClassifySet.Name = "buttonClassifySet";
            this.buttonClassifySet.Size = new System.Drawing.Size(131, 23);
            this.buttonClassifySet.TabIndex = 7;
            this.buttonClassifySet.Text = "Classify Set";
            this.buttonClassifySet.UseVisualStyleBackColor = true;
            this.buttonClassifySet.Click += new System.EventHandler(this.OnClassifySetClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 262);
            this.Controls.Add(this.buttonClassifySet);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.buttonClassify);
            this.Controls.Add(this.buttonLoadOccupied);
            this.Controls.Add(this.buttonLoadEmpty);
            this.Controls.Add(this.buttonLearn);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonLoadEmpty;
        private System.Windows.Forms.Button buttonLoadOccupied;
        private System.Windows.Forms.Button buttonClassify;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Button buttonClassifySet;
    }
}

