using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Drawing ;

namespace POM
{
    class StateAnalysis
    {
        Timer redPulseTimer, redOxygenTimer, yellowPulseTimer, yellowOxygenTimer, yellowColorTimer, redColorTimer;
        static int YellowPulseBottom=60, RedPulseBottom=50, YellowOxygenBottom=94, RedOxygenBottom=90;
        static int YellowPulseTop = 90, RedPulseTop = 110;
        int yellowPulseTime=0, redPulseTime=0, yellowOxygenTime=0, redOxygenTime=0;
        Color pulseColor = Color.FromArgb(1, 177, 242), oxygenColor = Color.FromArgb(1, 177, 242);
        public StateAnalysis()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 1000;
            redPulseTimer = new Timer(interval);
            redOxygenTimer = new Timer(interval);
            yellowOxygenTimer = new Timer(interval);
            yellowPulseTimer=new Timer(interval);
            yellowColorTimer = new Timer(500);
            redColorTimer = new Timer(500);
             //绑定Elapsed事件
            redPulseTimer.Elapsed += redPulseTimer_Elapsed;
            redOxygenTimer.Elapsed +=redOxygenTimer_Elapsed;
            yellowPulseTimer.Elapsed += yellowPulseTimer_Elapsed;
            yellowOxygenTimer.Elapsed += yellowOxygenTimer_Elapsed;
            yellowColorTimer.Elapsed += yellowColorTimer_Elapsed;
            redColorTimer.Elapsed += redColorTimer_Elapsed;
        }

        

        void redColorTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("redOxygenTime:" + redOxygenTime);
            Console.WriteLine("redPulseTime:" + redPulseTime);
            if (redOxygenTime > 5) 
            {
                if (oxygenColor == Color.Red) oxygenColor = Color.FromArgb(1, 177, 242);
                else oxygenColor = Color.Red;
                
            }
            if(redPulseTime>5)
            {
                Console.WriteLine(pulseColor.ToString());
                if (pulseColor == Color.Red) pulseColor = Color.FromArgb(1, 177, 242);
                else pulseColor = Color.Red;
            }
        }

        void yellowColorTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
          
            if(yellowOxygenTime>5)
            {
                if (oxygenColor == Color.Yellow) oxygenColor = Color.FromArgb(1, 177, 242);
                else oxygenColor = Color.Yellow;
                Console.WriteLine("yellowOxygenTime:" + yellowOxygenTime);
            }
            if(yellowPulseTime>5)
            {
                if (pulseColor == Color.Yellow) pulseColor = Color.FromArgb(1, 177, 242);
                else pulseColor = Color.Yellow;
                Console.WriteLine("yellowPulseTime:" + yellowPulseTime);
            }
            

        }
        public Color PulseColor
        {
            get { return pulseColor; }
        }
        public Color OxygenColor
        {
            get { return oxygenColor; }
        }
        void yellowOxygenTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
                yellowOxygenTime++;
               // Console.WriteLine("yellowOxygenTimer_Elapsed");
            
        }

        void yellowPulseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
           
                yellowPulseTime++;
               // Console.WriteLine("yellowPulseTimer_Elapsed");
        }

        void redOxygenTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
       
                redOxygenTime++;
               // Console.WriteLine(" redOxygenTimer_Elapsed");
            
        }

        void redPulseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            redPulseTime++;
            //Console.WriteLine(" redPulseTimer_Elapsed");
        }


        void initPulseState()
        {
            yellowPulseTimer.Enabled = false;
            redPulseTimer.Enabled = false;
            yellowPulseTime = 0;
            redPulseTime = 0;
            pulseColor = Color.FromArgb(1, 177, 242);
           
        }
        void initOxygenState()
        {
            yellowOxygenTimer.Enabled = false;
            redOxygenTimer.Enabled = false;
            yellowOxygenTime = 0;
            redOxygenTime = 0;
            oxygenColor = Color.FromArgb(1, 177, 242);
        }

        public void SetData(string pStr,string oStr)
        {
            int p,o;
            if (int.TryParse(pStr, out p))
            {
                if (p > 40)
                {
                    if (p <= RedPulseBottom || p >= RedPulseTop)
                    {
                        if (!redPulseTimer.Enabled) redPulseTimer.Enabled = true;
                        Console.WriteLine("redPulseTimer.Enabled=true;");
                        if (!redColorTimer.Enabled) redColorTimer.Enabled = true;
                        
                    }
                    else if (p <= YellowPulseBottom || p >= YellowPulseTop)
                    {
                        redPulseTimer.Enabled = false;
                        redPulseTime = 0;
                        if (!yellowPulseTimer.Enabled) yellowPulseTimer.Enabled = true;
                        yellowColorTimer.Enabled = true;
                    }
                    else
                    {
                        initPulseState();
                    }
                }
                else
                {
                   initPulseState();
                }
            }
            if (int.TryParse(oStr,out o))
            {
                if (o > 80)
                {
                    if (o <= RedOxygenBottom)
                    {
                        if (!redOxygenTimer.Enabled) redOxygenTimer.Enabled = true;
                        redColorTimer.Enabled = true;
                    }
                    else if (o <= YellowOxygenBottom)
                    {
                        redOxygenTimer.Enabled = false;
                        redOxygenTime = 0;
                        if (!yellowOxygenTimer.Enabled) yellowOxygenTimer.Enabled = true;
                        yellowColorTimer.Enabled = true;
                    }
                    else
                    {
                        initOxygenState();
                    }
                }
                else
                {
                    initOxygenState();
                }
            }
        }

        

       

    }
}
