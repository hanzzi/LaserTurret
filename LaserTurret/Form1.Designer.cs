namespace LaserTurret
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Degrees = new System.Windows.Forms.Label();
            this.XY = new System.Windows.Forms.Label();
            this.WarningLabel = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.PortLabel = new System.Windows.Forms.Label();
            this.isPortOpenLabel = new System.Windows.Forms.Label();
            this.InitializeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Degrees
            // 
            this.Degrees.AutoSize = true;
            this.Degrees.Location = new System.Drawing.Point(236, 393);
            this.Degrees.Name = "Degrees";
            this.Degrees.Size = new System.Drawing.Size(53, 13);
            this.Degrees.TabIndex = 0;
            this.Degrees.Text = "Degrees: ";
            // 
            // XY
            // 
            this.XY.AutoSize = true;
            this.XY.Location = new System.Drawing.Point(236, 406);
            this.XY.Name = "XY";
            this.XY.Size = new System.Drawing.Size(24, 13);
            this.XY.TabIndex = 1;
            this.XY.Text = "XY:";
            // 
            // WarningLabel
            // 
            this.WarningLabel.AutoSize = true;
            this.WarningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.WarningLabel.Location = new System.Drawing.Point(10, 10);
            this.WarningLabel.Name = "WarningLabel";
            this.WarningLabel.Size = new System.Drawing.Size(0, 17);
            this.WarningLabel.TabIndex = 2;
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM2";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(236, 380);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(29, 13);
            this.PortLabel.TabIndex = 4;
            this.PortLabel.Text = "Port:";
            // 
            // isPortOpenLabel
            // 
            this.isPortOpenLabel.AutoSize = true;
            this.isPortOpenLabel.Location = new System.Drawing.Point(236, 367);
            this.isPortOpenLabel.Name = "isPortOpenLabel";
            this.isPortOpenLabel.Size = new System.Drawing.Size(66, 13);
            this.isPortOpenLabel.TabIndex = 5;
            this.isPortOpenLabel.Text = "IsPortOpen: ";
            // 
            // InitializeButton
            // 
            this.InitializeButton.Location = new System.Drawing.Point(227, 62);
            this.InitializeButton.Name = "InitializeButton";
            this.InitializeButton.Size = new System.Drawing.Size(75, 23);
            this.InitializeButton.TabIndex = 6;
            this.InitializeButton.Text = "Initialize";
            this.InitializeButton.UseVisualStyleBackColor = true;
            this.InitializeButton.Click += new System.EventHandler(this.InitializeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.InitializeButton);
            this.Controls.Add(this.isPortOpenLabel);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.WarningLabel);
            this.Controls.Add(this.XY);
            this.Controls.Add(this.Degrees);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Degrees;
        private System.Windows.Forms.Label XY;
        private System.Windows.Forms.Label WarningLabel;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label isPortOpenLabel;
        private System.Windows.Forms.Button InitializeButton;
    }
}

