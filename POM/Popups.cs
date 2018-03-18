using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
 


namespace POM
{
   
    
    public partial class Popups : Form
    {
      

       public delegate void ShowInfo(string p,string s );  //委托用于显示脉搏，血氧值

       Font font;
       Wave wav;
       ShowInfo sInfo;
       string[] arg = new string[2];
       StateAnalysis sAnalysis;
       
       //具体显示 显示脉搏，血氧值 函数
       public void ShowText(string p, string s)
       {
           pictureBox1.Image = null;
           pictureBox1.Refresh();
           Graphics g = pictureBox1.CreateGraphics();

           sAnalysis.SetData("59", s);
           //Console.WriteLine("sAnalysis.PulseColor" + sAnalysis.PulseColor);
           g.DrawString(p, font, new SolidBrush(sAnalysis.PulseColor), 50, 75);
           
           g.DrawString(s, font, new SolidBrush(sAnalysis.OxygenColor), 75, 140);
       }
       
        public Popups()
        {
            InitializeComponent();
            this.SizeChanged += Popups_SizeChanged;
            this.nIcon.Click += nIcon_Click;
            this.nIcon.MouseClick += nIcon_MouseClick;
            this.FormClosing+=Popups_FormClosing;
           
            this.pictureBox1.DoubleClick += pictureBox1_DoubleClick;
            this.pictureBox1.MouseClick += pictureBox1_MouseClick;
            Port.DataReceived += Port_DataReceived;
            sInfo = new ShowInfo(ShowText);
        }
        private void Popups_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;

            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width + 20;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height + 30;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            this.TopMost = true;

            this.TransparencyKey = Color.White;             //设置默认透明色
            this.BackColor = this.TransparencyKey;          //设置当前窗体的背景色为透明
            this.FormBorderStyle = FormBorderStyle.None;    //隐藏窗体边框
            font = new Font("幼圆", 32, FontStyle.Bold);
            sAnalysis = new StateAnalysis();


            OpenPort();
            AddSerialItem();
        }


        /// <summary>
        /// 用于最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
           
            if ((p.X >= 115 && p.X <= 145) && (p.Y >= 20 && p.Y  <= 50))
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }
        /// <summary>
        /// 双击时显示波形图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            showWave();
        }
       
        /// <summary>
        /// 串口事件函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            else if (head.Equals("R:"))
            {
                if (wav != null)
                { 
                    try
                    {
                        wav.UpdateWave(float.Parse(info.Split(',')[0]));
                    }
                    catch
                    {
                        wav.UpdateWave(0);
                    }
                }
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
        /// <summary>
        /// 串口打开函数
        /// </summary>
        void OpenPort()
        {
            //string pName = AppConfig.GetValue("SerialPortName");
            string pName = "COM4";
            if (!pName.Equals(""))
            {
                if (Port.IsOpen)
                {
                    Port.Close();
                }
                
                Port.PortName = pName;
                
                try
                {
                   
                    Port.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show("串口打开失败，请检查设备是否接入，串口选择是否正确。 /n/r ErrorMessage:" + e.Message);
                }

            }
            else
            {
                MessageBox.Show("请先选择设备所在的端口。" );
            }
        }

        void nIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
               // this.nIcon.Visible = false;
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

       

        void Popups_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
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
           
        }

        private void mSetting_Click(object sender, EventArgs e)
        {

        }

        private void cOM1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 根据串口列表，添加右键串口菜单
        /// </summary>
        private void AddSerialItem()

        {
            mSetting.CheckOnClick = true;
            
           
            foreach (var pName in SerialPort.GetPortNames())
            {
                if (pName.Equals(AppConfig.GetValue("SerialPortName")))
                {
                    AddContextMenu(pName, mSetting.DropDownItems, new EventHandler(MenuClicked),true);
                }else
                { 
                
                    AddContextMenu(pName, mSetting.DropDownItems, new EventHandler(MenuClicked),false);
                }
            }
        }

        /// <summary>
        /// 串口选择菜单点击时动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuClicked(object sender, EventArgs e)
        {

            foreach (ToolStripMenuItem tsm in mSetting.DropDownItems)
            {
                tsm.Checked = false;
            }
            ToolStripMenuItem sMenu = (ToolStripMenuItem)sender;
            sMenu.Checked = true;
            AppConfig.SetValue("SerialPortName", sMenu.Text);
            OpenPort();

        }
        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
        /// <param name="cms">要添加到的子菜单集合</param>
        /// <param name="callback">点击时触发的事件</param>
        /// <returns>生成的子菜单，如果为分隔条则返回null</returns>

        ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback,bool Checked)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = text + "TAG";
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);
                tsmi.Checked = Checked;
                return tsmi;
            }

            return null;
        }





        private void mMain_Click(object sender, EventArgs e)
        {
            showWave();
        }
        void showWave()
        {
            if (wav == null)
            {
                wav = new Wave();
                wav.Show();
                wav.FormClosed += wav_FormClosed;
            }
            else
            {
                wav.Show();
            }
        }
        void wav_FormClosed(object sender, FormClosedEventArgs e)
        {
            wav.Dispose();
            wav = null;
        }


    }
}
