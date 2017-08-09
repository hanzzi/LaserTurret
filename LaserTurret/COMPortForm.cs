using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaserTurret
{
    public partial class COMPortForm : Form
    {
        public COMPortForm()
        {
            InitializeComponent();
        }

        private void COMPortForm_Load(object sender, EventArgs e)
        {

            Dictionary<int, string> ports = new Dictionary<int, string>();

            int i = 0;

           foreach (string port in Properties.Settings.Default.COMPort)
            {
                ports.Add(i, port);

                i++;
            }

            COMCollection.DataSource = new BindingSource(ports, null);
            COMCollection.DisplayMember = "Value";
            COMCollection.ValueMember = "Value";


        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["CurrentCOMPort"] = COMCollection.SelectedValue.ToString();
            this.Dispose();
        }
    }
}
