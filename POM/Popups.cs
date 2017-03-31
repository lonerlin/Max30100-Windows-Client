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

       public delegate void ShowInfo(string p,string s );

       Font font;
       public void ShowText(string p, string s)
       {
           pictureBox1.Image = null;
           pictureBox1.Refresh();
           Graphics g = pictureBox1.CreateGraphics();

           g.DrawString(p, font, new SolidBrush(Color.FromArgb(1, 177, 242)), 50, 75);
           
           g.DrawString(s, font, new SolidBrush(Color.FromArgb(1, 177, 242)), 75, 140);
       }
       ShowInfo sInfo;
        public Popups()
        {
            InitializeComponent();
            this.SizeChanged += Popups_SizeChanged;
            this.nIcon.Click += nIcon_Click;
            this.nIcon.MouseClick += nIcon_MouseClick;
            this.FormClosing+=Popups_FormClosing;
           // this.Resize += Popups_Resize;
            
            Port.DataReceived += Port_DataReceived;
            sInfo = new ShowInfo(ShowText);
        }
        string[] arg = new string[2];
        void Port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string value = ReadSerialData();
            String head = value.Substring(0, 2);
            String info = value.Substring(2);
           
            if (head.Equals("H:"))
            {
                
                arg[0]=Math.Round(decimal.Parse(info)).ToString();
                this.Invoke(sInfo, arg);
            }
            
            else if(head.Equals("O:")) 
            {
                arg[1] = info;
                this.Invoke(sInfo,arg);
            }
        }

        /// <summary>
        /// 从串口读取数据并转换为字符串形式
        /// </summary>
        /// <returns></returns>
        private string ReadSerialData()
        {
            string value = "";
            try
            {
                if (Port != null && Port.BytesToRead > 0)
                {
                   // value = Port.ReadExisting();
                    value = Port.ReadLine();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取串口数据发生错误：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return value;
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
            
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width+20;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height+30;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            this.TopMost = true;
           
           this.TransparencyKey = Color.White;             //设置默认透明色
            this.BackColor = this.TransparencyKey;          //设置当前窗体的背景色为透明
            this.FormBorderStyle = FormBorderStyle.None;    //隐藏窗体边框
            font = new Font("幼圆", 32, FontStyle.Bold); 
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);


            Port.Open();
            Wave wav = new Wave();
            wav.Port = Port;
            wav.Show();
           
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
