﻿namespace POM
{
    partial class Wave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wave));
            this.statusChart1 = new UI.StatusChart();
            this.saveWaveFile = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // statusChart1
            // 
            this.statusChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusChart1.BackColor = System.Drawing.Color.Black;
            this.statusChart1.Enabled = false;
            this.statusChart1.GridHeight = 50;
            this.statusChart1.GridShiftting = false;
            this.statusChart1.GridWidth = 50;
            this.statusChart1.Interval = 10;
            this.statusChart1.Location = new System.Drawing.Point(-1, 1);
            this.statusChart1.Mode = UI.StatusChart.ChartMode.Waveform;
            this.statusChart1.Name = "statusChart1";
            this.statusChart1.Range = 500;
            this.statusChart1.Size = new System.Drawing.Size(1008, 540);
            this.statusChart1.TabIndex = 0;
            // 
            // saveWaveFile
            // 
            this.saveWaveFile.Filter = "位图|*.bmp";
            // 
            // Wave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 541);
            this.Controls.Add(this.statusChart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Wave";
            this.Text = "脉搏波形图";
            this.Load += new System.EventHandler(this.Wave_Load);
            this.ResumeLayout(false);

        }

        #endregion

       // private System.Windows.Forms.PictureBox pictureBox1;
        private UI.StatusChart statusChart1;
        private System.Windows.Forms.SaveFileDialog saveWaveFile;
    }
}