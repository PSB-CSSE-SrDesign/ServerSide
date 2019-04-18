using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotServerGUI
{
    public partial class Form1 : Form
    {
        public bool terminate = false;
        private IPAddress ipaddress;
        private static Thread connection = null;

        private double redValue = .75;
        private double blueValue = .75;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);

            // Instantiate the dialog box
            IPSelectBox dlg = new IPSelectBox
            {
                Owner = this,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            // Open the dialog box modally 
            dlg.ShowDialog();

            MainThread m = new MainThread(this);
            connection = new Thread(new ThreadStart(m.LaunchThread));
            connection.Start();
        }

        public double GetBlueValue()
        {
            return blueValue;
        }

        public double GetRedValue()
        {
           return redValue;
        }

        public void SetIPAddress(IPAddress ip)
        {
            this.ipaddress = ip;
        }

        public IPAddress GetIPAddress()
        {
            return this.ipaddress;
        }

        public void SetParameters(string NormalPictureFile, string MaskedPictureFile, double velocity)
        {
            //directory to find images
            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\";
            string normalImageLocation = NormalPictureBox.ImageLocation;
            string maskedImageLocation = MaskedPictureBox.ImageLocation;

            //opens filestream for input image and sets picture box
            NormalPictureBox.ImageLocation = directory + NormalPictureFile;

            //opens filestream for masked image and sets picture box
            MaskedPictureBox.ImageLocation = directory + MaskedPictureFile;

            //delete file previously used
            if (normalImageLocation != null)
            {
                File.Delete(normalImageLocation);
                File.Delete(maskedImageLocation);
            }

            //sets textbox to velocity
            SetTextBox1(velocity.ToString());
        }

        delegate void StringArgReturningVoidDelegate(string text);

        public void SetTextBox1(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetTextBox1);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }

        public void SetTextBox2(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox2.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetTextBox2);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox2.Text = text;
            }
        }

        delegate void NoArgReturningVoidDelegate();

        public void ConnectionMade()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox2.InvokeRequired)
            {
                NoArgReturningVoidDelegate d = new NoArgReturningVoidDelegate(ConnectionMade);
                this.Invoke(d, new object[] {  });
            }
            else
            {
                this.connectionLabel.Text = "Connection Made";
                this.connectionLabel.ForeColor = Color.Black;
            }

            
        }

        private void redScrollBar_ValueChanged(object sender, EventArgs e)
        {
            redValue = redScrollBar.Value / 100.0;
        }

        private void blueScrollBar_ValueChanged(object sender, EventArgs e)
        {
            blueValue = blueScrollBar.Value / 100.0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Confirm Termination",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

            e.Cancel = (result == DialogResult.No);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            terminate = true;
            if(SocketListener.CheckConnection())
                connection.Join();
            Process.GetCurrentProcess().Kill();
        }
    }
}

     
