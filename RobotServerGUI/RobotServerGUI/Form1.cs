using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotServerGUI
{
    public partial class Form1 : Form
    {
        static Thread connection = null;
        static Bitmap b = new Bitmap(1600, 900);

        public Form1()
        {
            connection = new Thread(new ThreadStart(MainThread.LaunchThread));
            connection.Start();
            MainThread.SetEnvironment(this);

            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public double GetBlueValue()
        {
            return redScrollBar.Value/10.0;
        }

        public double GetRedValue()
        {
           return blueScrollBar.Value/10.0;
        }

        public void SetParameters(string NormalPictureFile, string MaskedPictureFile, float velocity)
        {
            //directory to find images
            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\";

            //opens filestream for input image and sets picture box
            FileStream fin1 = new FileStream(directory + NormalPictureFile, FileMode.Open, FileAccess.Read);
            NormalPictureBox.Image = Image.FromStream(fin1);
            fin1.Close();
            //opens filestream for masked image and sets picture box
            FileStream fin2 = new FileStream(directory + MaskedPictureFile, FileMode.Open, FileAccess.Read);
            MaskedPictureBox.Image = Image.FromStream(fin2);
            fin2.Close();

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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("window closed");
        }
    }
}

     
