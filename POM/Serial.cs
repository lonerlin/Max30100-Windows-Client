using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO.Ports;


namespace POM
{
    public class Serial:SerialPort
    {
        Serial(string portName)
        {
            this.PortName = "Com7";
            this.BaudRate = 115200;
            this.Open();
        }


    }
}
