using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace POM
{
    public partial class Wave : Form
    {

        public SerialPort Port { get; set; }
        public delegate void DrawWave(int value);

        public void draw(int value)
        {
            this.statusChart1.Value = value;
        }

        DrawWave dWave;
        public Wave()
        {
            InitializeComponent();
            
        }

        private void Wave_Load(object sender, EventArgs e)
        {
           
                
                dWave = new DrawWave(this.draw);
            
        }

        public void UpdateWave(float value)
        {
            Console.WriteLine(value);
            this.statusChart1.Value = Convert.ToInt32(Math.Round(value))+150;
        }

       
       

      
    }
}
