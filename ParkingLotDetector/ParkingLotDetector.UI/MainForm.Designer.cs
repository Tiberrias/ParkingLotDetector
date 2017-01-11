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
            this.buttonLoadMe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLoadMe
            // 
            this.buttonLoadMe.Location = new System.Drawing.Point(13, 13);
            this.buttonLoadMe.Name = "buttonLoadMe";
            this.buttonLoadMe.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadMe.TabIndex = 0;
            this.buttonLoadMe.Text = "Click me!";
            this.buttonLoadMe.UseVisualStyleBackColor = true;
            this.buttonLoadMe.Click += new System.EventHandler(this.OnLoadMeClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.buttonLoadMe);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadMe;
    }
}

