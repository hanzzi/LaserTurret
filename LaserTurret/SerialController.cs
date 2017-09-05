using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaserTurret
{
    class SerialController
    {
        public SerialPort SetupSerialPort()
        {
            SerialPort serial = new SerialPort();

            // Mostly default settings for the serialport except for the PortName which is whatever the user chose.
            serial.BaudRate = 9600;
            serial.PortName = Properties.Settings.Default["CurrentCOMPort"].ToString();
            serial.DataBits = 8;
            serial.DtrEnable = false;
            serial.Handshake = Handshake.None;
            serial.Parity = Parity.None;
            serial.ParityReplace = 63;
            serial.ReadBufferSize = 4096;
            serial.ReadTimeout = -1;
            serial.ReceivedBytesThreshold = 1;
            serial.WriteBufferSize = 2048;

            return serial;
        }
        
        public void ChangeComPort(SerialPort port, string targetComPort)
        {
            
            // if the port is open close the port and try again
            if (port.IsOpen)
            {
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                Thread closePort = new Thread(() => port.Close());
                closePort.Start();
                ChangeComPort(port, targetComPort);
            }

            port.PortName = Properties.Settings.Default.CurrentCOMPort;
            if (!port.IsOpen)
            {
                port.Open();
                return;
            }
        }
    }
}