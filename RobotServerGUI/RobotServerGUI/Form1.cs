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
        private string distance;
        private string objectDetention;
        private Image still;



        public Form1()
        {
            Thread connection = new Thread(new ThreadStart(MainThread.LaunchThread));
            connection.Start();
            MainThread.SetEnvironment(this);

            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);

            LoadNewPict();

            GetRedValue();

            GetBlueValue();
        }

      

        public void GetBlueValue()
        {
            double blueValue;
        }

        public void GetRedValue()
        {
            double redValue;
        }

        Bitmap b = new Bitmap(1600, 900);
        Rectangle rect;
        Rectangle sourceRect;
        Image picture;
        Graphics g;
        Image pictureRendered;
        private float brightness;

        private void LoadNewPict()
        {
            
        }

        public void InitializeParameters(string NormalPictureFile, string RenderedPictureFile, float velocity)
        {
            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\";
            FileStream fin1 = new FileStream(directory + NormalPictureFile, FileMode.Open, FileAccess.Read);
            NormalPictureBox.Image = Image.FromStream(fin1);
            //picture = Image.FromFile(directory +RenderedPictureFile);
            //NormalPictureBox.Image = picture;

            pictureRendered = Image.FromFile(directory + RenderedPictureFile);
            RenderedpictureBox.Image = pictureRendered;
            //textBox1.Text = velocity.ToString();
            SetTextBox1(velocity.ToString());
            //textBox2.Text = "Friend";
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
    }
}

     
