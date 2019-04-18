using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotServerGUI
{
    public partial class IPSelectBox : Form
    {
        public IPSelectBox()
        {
            InitializeComponent();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            ipListBox.BeginUpdate();
            foreach (IPAddress ipAddress in ipHostInfo.AddressList)
            {
                if(ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    ipListBox.Items.Add(ipAddress);
            }
            ipListBox.EndUpdate();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void IPSelectBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ipListBox.SelectedIndex == -1)
                ipListBox.SetSelected(0, true);            
            ((Form1)Owner).SetIPAddress((IPAddress)ipListBox.SelectedItem);
        }
    }
}
