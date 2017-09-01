using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;

namespace LaserTurret
{
    public partial class Form1 : Form
    {
        public static SerialPort SerialPort;
        private int _servoX = 0;
        private int _servoY = 0;

        public Stopwatch Watch { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializePort()
        {
            Watch = Stopwatch.StartNew();

            // initializes a serialport used throughout the applications lifetime.
            SerialController controller = new SerialController();
            SerialPort port = controller.SetupSerialPort();
            SerialPort = port;

            COMPortForm form = new COMPortForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
            /*
            Watch = Stopwatch.StartNew();
            try
            {

                SerialController controller = new SerialController();
                SerialPort port = controller.SetupSerialPort();
                SerialPort = port;

                Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;

                COMPortForm changeCom = new COMPortForm();
                changeCom.StartPosition = FormStartPosition.CenterParent;
                changeCom.BringToFront();
                changeCom.Focus();
                changeCom.Show();
                
                SerialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCOMPort")
            {   
                SerialController controller = new SerialController();
                controller.ChangeComPort(SerialPort, Properties.Settings.Default.CurrentCOMPort);
            }
        }

        public void WriteToPort(Point coordinates)
        {
            // used to prevent the serial port of overloading the target device
            if (Watch.ElapsedMilliseconds > 15)
            {
                Watch = Stopwatch.StartNew();

                // Debug code used to display the degrees sent to the device
                int degreeX = coordinates.X / (Size.Width / 180);
                int degreeY = coordinates.Y / (Size.Height / 180);
                // displays the degrees and XY coordinates for debugging purposes
                Degrees.Text = $"Degrees: X{degreeX} Y{degreeY}";
                XY.Text = $"XY: {Form1.MousePosition}";

                // writes the data to the COM port 
                if (SerialPort.IsOpen)
                {
                    SerialPort.Write(String.Format("X{0}Y{1}",
                        coordinates.X / (Size.Width / 180),
                        coordinates.Y / (Size.Height / 180))
                    );
                    _servoX = degreeX;
                    _servoY = degreeY;
                }
            }
        }

        // write to the serialport whenever the mouse moves.
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            isPortOpenLabel.Text = "IsPortOpen: " + SerialPort.IsOpen;
            PortLabel.Text = "Port: " + SerialPort.PortName;
            WriteToPort(new Point(e.X, e.Y));
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
                
                e.Cancel = false;
                // reset the servos position to zero.
                SerialPort.Write(String.Format("X{0}Y{1}",
                   180 - _servoX / (Size.Width / 180),
                   180 - _servoY / (Size.Height / 180))
                );
                // dispose of resources just to be safe
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

        private void changeCOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            COMPortForm comForm = new COMPortForm();
            comForm.Show();
        }

        private void InitializeButton_Click(object sender, EventArgs e)
        {
            InitializePort();
            InitializeButton.Visible = false;
            MouseMove += Form1_MouseMove;
        }
    }
}
