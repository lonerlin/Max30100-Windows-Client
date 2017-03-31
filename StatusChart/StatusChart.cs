#region ** 引用 **
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
#endregion

namespace UI
{
    public partial class StatusChart : UserControl
    {
        /****************************
         * Nobi's StatusChart
         * 野比的状态波形图
         * --------------------------
         * 设计：野比
         * 制作：野比
         * 首发：bbs.bccn.net
         * For evaluations ONLY, NOT for commercial!
         * 仅供评测、交流、学习，请勿用于商业用途！
         * 作者拒绝承担因私自发布、使用所带来的任何法律或社会责任！
         * 
         * 转发请保留以上信息
         * *************************/

        #region ** 私有成员 **
        //私有成员：属性
        private int range = 100;    //波形所容纳的最大值
        private int interval = 2000; //定时器间隔
        private int[] val;//Chart 值数组
        private int currentValue = 0;//当前值
        private int w, h;//画布大小
        private Timer tmrRefresh;//定时器

        //私有成员：绘图
        private ChartMode chartMode = ChartMode.Histogram;    //绘图方式
        private int iOffset = 0;//平移偏移量
        private bool gridShiftting = true;//网格是否平移
        private int gridShifttingIncrement = 1;//网格平移间距
        private int gridWidth = 10;//网格宽度
        private int gridHeight = 10;//网格高度

        private Pen penChart = new Pen(Color.Lime);
        private Pen penGrid = new Pen(Color.Green);
        private Graphics graph;

        #endregion

        #region ** 私有方法 **
        //私有方法：绘制网格
        private void DrawGrids(ref Graphics g, int offset)
        {
            //网格数（不计边缘）
            float div;
            float pos = 0F;
            //先画 垂直 方向
            //可以少画一根线
            div = (float)w / (float)gridWidth + 1;
            for (int i = 0; i < (int)div; i++)
            {
                pos += gridWidth;
                g.DrawLine(penGrid, pos - offset, 0, pos - offset, h);
            }
            //画 水平 方向
            div = (float)h / (float)gridHeight;
            pos = 0F;
            for (int i = 0; i < (int)div; i++)
            {
                pos += gridHeight;
                g.DrawLine(penGrid, 0, pos, w, pos);
            }
        }

        //私有方法：绘制图形
        private void DrawChart(ref Graphics g, Pen p, ref int[] val)
        {
            //从 0 到 w 绘制
            int len = w;
            //根据绘制方式
            if (chartMode == StatusChart.ChartMode.Histogram)
            {
                for (int i = 0; i < len; i++)
                {
                    g.DrawLine(p, i, h - val[i], i, h);
                }
                g.DrawLine(p, len, h - val[len - 1], len, h);
            }
            else
            {
                len--;
                for (int i = 0; i < len; i++)
                {
                    g.DrawLine(p, i, h - val[i], i + 1, h - val[i + 1]);
                }
                len++;
                g.DrawLine(p, len - 1, h - val[len - 2], len, h - val[len - 1]);
            }
        }


        //私有方法：重载方法 
        protected override void OnLoad(EventArgs e)
        {
            //base.OnLoad(e);
            //打开双缓冲，防止闪烁
            DoubleBuffered = true;
            h = base.ClientSize.Height - 1;
            w = base.ClientSize.Width - 1;
            val = new int[w];
            //设置 Timer
            tmrRefresh = new Timer();
            tmrRefresh.Interval = interval;
            tmrRefresh.Enabled = true;
            tmrRefresh.Tick += new EventHandler(tmrRefresh_Tick);
        }
        //重设大小
        protected override void OnResize(EventArgs e)
        {
            h = base.ClientSize.Height;
            w = base.ClientSize.Width;
            Array.Resize(ref val, w);
            Invalidate();
        }
        //绘图
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            graph = e.Graphics;
            DrawGrids(ref graph, iOffset);
            DrawChart(ref graph, penChart, ref  val);
        }

        //定时器更新
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            //更新网格偏移
            //只有启用了网格移动才处理
            if (gridShiftting)
            {
                iOffset += gridShifttingIncrement;
                iOffset %= gridWidth;
            }
            //更新图形（整体左移）
            //必须在这里而不能在画图的同时移动，
            //若在画图中移动，则当画面被遮挡（OnPaint）事件不发生时无法更新
            int len = w;
            for (int i = 0; i < len; i++)
            {
                //判断数组越界
                if (i < len - 1)
                {
                    val[i] = val[i + 1];
                }
                else
                {
                    val[len - 1] = currentValue;
                    //break;
                }
            }
            //val[len] = currentValue;
            Invalidate();
        }

        #endregion

        #region ** 公共成员 **
        /// <summary>
        /// 波形图显示方式枚举。
        /// </summary>
        public enum ChartMode
        {
            /// <summary>
            /// 直方图
            /// </summary>
            Histogram = 0,    //直方图
            /// <summary>
            /// 波形图
            /// </summary>
            Waveform        //波形图
        }
        #endregion

        #region ** 公共属性 **
        //公共成员：属性
        [Browsable(true)]
        /// <summary>
        /// 数据值范围。绘图时将根据此值缩放 Value 值。
        /// </summary>
        [Category("内容"), Description("数据值范围。绘图时将根据此值缩放 Value 值。"), DefaultValue(100)]
        public int Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }
        /// <summary>
        /// 当前值。
        /// </summary>
        [Category("内容"), Description("当前值。"), DefaultValue(0)]
        public int Value
        {
            get
            {
                return currentValue;
            }
            set
            {
                //约束 value
                if (value > range)
                {
                    value = range;
                }
                if (value < 0)
                {
                    value = 0;
                }
                //根据 Range 属性修正 value
                //尽量减小误差
                value = (int)((float)value / (float)range * (float)h);
                currentValue = value;
            }
        }
        /// <summary>
        /// 波形图刷新间隔。
        /// </summary>
        [Category("内容"), Description("波形图刷新间隔。"), DefaultValue(500)]
        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
                if (tmrRefresh != null)
                {
                    tmrRefresh.Interval = interval;
                }
            }
        }
        /// <summary>
        /// 指示波形是否继续更新。
        /// </summary>
        [Category("内容"), Description("指示波形是否继续更新。"), DefaultValue(true)]
        public new bool Enabled
        {
            get
            {
                if (tmrRefresh != null)
                {
                    return tmrRefresh.Enabled;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (tmrRefresh != null)
                {
                    tmrRefresh.Enabled = value;
                }
            }
        }
        /// <summary>
        /// 显示方式。
        /// </summary>
        [Category("外观"), Description("波形显示方式。"), DefaultValue(typeof(StatusChart.ChartMode), "Histogram")]
        public ChartMode Mode
        {
            get
            {
                return chartMode;
            }
            set
            {
                chartMode = value;
            }
        }
        /// <summary>
        /// 网格每次更新时向左平移的距离。
        /// </summary>
        [Category("外观"), Description("网格每次更新时向左平移的距离。"), DefaultValue(1)]
        public int ShifttingIncrement
        {
            get
            {
                return gridShifttingIncrement;
            }
            set
            {
                gridShifttingIncrement = value;
                if (gridShifttingIncrement < 0)
                {
                    gridShifttingIncrement = 0;
                }
            }
        }
        /// <summary>
        /// 指示网格是否每次更新时向左平移。
        /// </summary>
        [Category("外观"), Description("指示网格是否每次更新时向左平移。"), DefaultValue(true)]
        public bool GridShiftting
        {
            get
            {
                return gridShiftting;
            }
            set
            {
                gridShiftting = value;
            }
        }
        /// <summary>
        /// 网格宽度。
        /// </summary>
        [Category("外观"), Description("网格宽度。"), DefaultValue(10)]
        public int GridWidth
        {
            get
            {
                return gridWidth;
            }
            set
            {
                gridWidth = value;
                Invalidate();
            }
        }
        /// <summary>
        /// 网格高度。
        /// </summary>
        [Category("外观"), Description("网格高度。"), DefaultValue(10)]
        public int GridHeight
        {
            get
            {
                return gridHeight;
            }
            set
            {
                gridHeight = value;
                Invalidate();
            }
        }
        /// <summary>
        /// 网格颜色。
        /// </summary>
        [Category("外观"), Description("网格颜色。"), DefaultValue(typeof(Color), "Green")]
        public Color GridColor
        {
            get
            {
                return penGrid.Color;
            }
            set
            {
                penGrid.Color = value;
            }
        }
        /// <summary>
        /// 波形颜色。
        /// </summary>
        [Category("外观"), Description("波形颜色。"), DefaultValue(typeof(Color), "Lime")]
        public override Color ForeColor
        {
            get
            {
                return penChart.Color;
            }
            set
            {
                penChart.Color = value;
            }
        }
        #endregion

        #region ** 公共方法 **
        /// <summary>
        /// 启动计时器更新。返回执行情况。
        /// </summary>
        /// <returns>执行情况。true 成功；false 失败。</returns>
        public bool Start()
        {
            try
            {
                if (tmrRefresh != null)
                {
                    tmrRefresh.Enabled = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 停止计时器更新。返回执行情况。
        /// </summary>
        /// <returns>执行情况。true 成功；false 失败。</returns>
        public bool Stop()
        {
            try
            {
                if (tmrRefresh != null)
                {
                    tmrRefresh.Enabled = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 清除历史状态记录。
        /// </summary>
        public void Clear()
        {
            if (val != null)
            {
                //没用
                //val.Initialize();
                Array.Resize(ref val, 0);
                Array.Resize(ref val, w);
                currentValue = 0;
            }
        }
        #endregion
    }
}
