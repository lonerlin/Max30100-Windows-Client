using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;


namespace POM
{
    class Serial:SerialPort
    {
        Serial()
        {
            this.PortName = "Com7";
        }
    }
}
