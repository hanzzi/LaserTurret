using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace LaserTurret
{
    public partial class Form1 : Form
    {
        public static System.IO.Ports.SerialPort SerialPort;

        public Stopwatch watch { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            watch = Stopwatch.StartNew();
            try
            {
                /*
                System.IO.Ports.SerialPort tempSerial = new System.IO.Ports.SerialPort();

                // Mostly default settings for the serialport except for the PortName which is whatever the user chose.
                tempSerial.BaudRate = 9600;
                tempSerial.PortName = Properties.Settings.Default["CurrentCOMPort"].ToString();
                tempSerial.DataBits = 8;
                tempSerial.DtrEnable = false;
                tempSerial.Handshake = System.IO.Ports.Handshake.None;
                tempSerial.Parity = System.IO.Ports.Parity.None;
                tempSerial.ParityReplace = 63;
                tempSerial.ReadBufferSize = 4096;
                tempSerial.ReadTimeout = -1;
                tempSerial.ReceivedBytesThreshold = 1;
                tempSerial.WriteBufferSize = 2048;

                tempSerial.Open();
                
                // Assign the correctly configured serial port to the uninitialized serialport
                SerialPort = tempSerial;
                */

                serialPort1.Open();

                SerialPort = serialPort1;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void WriteToPort(Point coordinates)
        {
            // used to prevent the serial port of overloading the target device
            if (watch.ElapsedMilliseconds > 15)
            {
                watch = Stopwatch.StartNew();

                // Debug code used to display the degrees sent to the device
                int DegreeX = coordinates.X / (Size.Width / 180);
                int DegreeY = coordinates.Y / (Size.Height / 180);

                // displays the degrees and XY coordinates for debugging purposes
                Degrees.Text = $"Degrees: X{DegreeX} Y{DegreeY}";
                XY.Text = $"XY: {Form1.MousePosition}";
                /*
                // writes the data to the COM port 
                if (SerialPort.IsOpen)
                    SerialPort.Write(String.Format("X{0}Y{1}",
                        (coordinates.X / (Size.Width / 180)),
                        coordinates.Y / (Size.Height / 180))
                        );*/
                        
            }
        }

        // Dispose resources before program shuts down, just to be safe, cant have wild serial connections running
        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult isCancel = FormClosingDialog();

            // if the user cancels the program will not be shutdown
            if (isCancel == DialogResult.Cancel)
                e.Cancel = true;
            else if (isCancel == DialogResult.OK)
            {
                // dispose of resources just to be safe
                e.Cancel = false;
                SerialPort.Close();
                SerialPort.Dispose();
                this.Dispose();
            }
        }


        // Confirmation form before the form closes 
        private static DialogResult FormClosingDialog()
        {
            #region Construction of form: prompt
            // Form Size and startupposition
            Form prompt = new Form();
            prompt.Width = 400;
            prompt.Height = 200;
            prompt.StartPosition = FormStartPosition.CenterParent;

            // Form Items initialized
            Label promptInformation = new Label() { Width = 250, Height = 20, Text = "Are you sure you wish to leave?", Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular), TextAlign = ContentAlignment.TopCenter };
            Button confirmationButton = new Button() { Height = 25, Width = 65, Text = "Ok", Left = 245, Top = 135 };
            Button cancelButton = new Button() { Height = 25, Width = 65, Text = "Cancel", Left = 315, Top = 135 };

            // Controls added to form but not connected to the items
            prompt.Controls.Add(promptInformation);
            prompt.Controls.Add(confirmationButton);
            prompt.Controls.Add(cancelButton);

            // assigned the items to the controls and the dialogresults for OK and Cancel
            prompt.AcceptButton = confirmationButton;
            prompt.CancelButton = cancelButton;
            prompt.MinimizeBox = false;
            prompt.MaximizeBox = false;
            confirmationButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
            #endregion

            // returns dialogresult
            DialogResult dialogResult = prompt.ShowDialog();
            return dialogResult;
        }

        // write to the serialport whenever the mouse moves.
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            PortLabel.Text = "Port: " + Properties.Settings.Default.CurrentCOMPort;

            if (watch.ElapsedMilliseconds > 15)
                WriteToPort(new Point(e.X, e.Y));
        }

        private void changeCOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            COMPortForm COMForm = new COMPortForm();
            COMForm.Show();

        }
    }
}
