namespace POM
{
    partial class Popups
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Popups));
            this.Port = new System.IO.Ports.SerialPort(this.components);
            this.nIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mPopups = new System.Windows.Forms.ToolStripMenuItem();
            this.mMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.lPulse = new System.Windows.Forms.Label();
            this.lSpo = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Port
            // 
            this.Port.BaudRate = 115200;
            this.Port.PortName = "COM7";
            // 
            // nIcon
            // 
            this.nIcon.ContextMenuStrip = this.menu;
            this.nIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("nIcon.Icon")));
            this.nIcon.Text = "脉搏血氧仪";
            this.nIcon.Visible = true;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mPopups,
            this.mMain,
            this.mSetting,
            this.mExit});
            this.menu.Name = "contextMenuStrip1";
            this.menu.Size = new System.Drawing.Size(131, 92);
            // 
            // mPopups
            // 
            this.mPopups.Name = "mPopups";
            this.mPopups.Size = new System.Drawing.Size(130, 22);
            this.mPopups.Text = "显示小窗";
            this.mPopups.Click += new System.EventHandler(this.mPopups_Click);
            // 
            // mMain
            // 
            this.mMain.Name = "mMain";
            this.mMain.Size = new System.Drawing.Size(130, 22);
            this.mMain.Text = "显示主窗口";
            // 
            // mSetting
            // 
            this.mSetting.Name = "mSetting";
            this.mSetting.Size = new System.Drawing.Size(130, 22);
            this.mSetting.Text = "设置";
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(130, 22);
            this.mExit.Text = "退出";
            this.mExit.Click += new System.EventHandler(this.mExit_Click);
            // 
            // lPulse
            // 
            this.lPulse.AutoSize = true;
            this.lPulse.Location = new System.Drawing.Point(23, 28);
            this.lPulse.Name = "lPulse";
            this.lPulse.Size = new System.Drawing.Size(41, 12);
            this.lPulse.TabIndex = 1;
            this.lPulse.Text = "label1";
            // 
            // lSpo
            // 
            this.lSpo.AutoSize = true;
            this.lSpo.Location = new System.Drawing.Point(135, 55);
            this.lSpo.Name = "lSpo";
            this.lSpo.Size = new System.Drawing.Size(41, 12);
            this.lSpo.TabIndex = 2;
            this.lSpo.Text = "label2";
            // 
            // Popups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 94);
            this.Controls.Add(this.lSpo);
            this.Controls.Add(this.lPulse);
            this.MaximizeBox = false;
            this.Name = "Popups";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Popups_Load);
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.IO.Ports.SerialPort Port;
        private System.Windows.Forms.NotifyIcon nIcon;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mPopups;
        private System.Windows.Forms.ToolStripMenuItem mMain;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.ToolStripMenuItem mSetting;
        private System.Windows.Forms.Label lPulse;
        private System.Windows.Forms.Label lSpo;
    }
}

