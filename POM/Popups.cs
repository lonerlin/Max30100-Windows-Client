using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
 


namespace POM
{
   
    
    public partial class Popups : Form
    {
       [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
 
        //下面是可用的常量,按照不合的动画结果声明本身须要的
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记该标记
        private const int AW_CENTER = 0x0010;//若应用了AW_HIDE标记,则使窗口向内重叠;不然向外扩大
        private const int AW_HIDE = 0x10000;//隐蔽窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口,在应用了AW_HIDE标记后不要应用这个标记
        private const int AW_SLIDE = 0x40000;//应用滑动类型动画结果,默认为迁移转变动画类型,当应用AW_CENTER标记时,这个标记就被忽视
        private const int AW_BLEND = 0x80000;//应用淡入淡出结果

       

        public Popups()
        {
            InitializeComponent();
            this.SizeChanged += Popups_SizeChanged;
            this.nIcon.Click += nIcon_Click;
            this.nIcon.MouseClick += nIcon_MouseClick;
            this.FormClosing+=Popups_FormClosing;
           // this.Resize += Popups_Resize;
            
        }

        

        

        void nIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.nIcon.Visible = false;
            }
            else
            { 
                
            }
        }

        void nIcon_Click(object sender, EventArgs e)
        {
            
            
           
        }

        void Popups_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
                //this.Hide();
                nIcon.Visible = true;
            }
        }

        private void Popups_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            this.TopMost = true;
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);
           
        }

        void Popups_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
            MessageBox.Show("程序将最小化到系统托盘区");
            e.Cancel = true; // 取消关闭窗体
            this.Hide();
            this.ShowInTaskbar = false;//取消窗体在任务栏的显示
            this.nIcon.Visible = true;//显示托盘图标
        }

        private void mExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void mPopups_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            this.TopMost = true;
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);
        }

    }
}
