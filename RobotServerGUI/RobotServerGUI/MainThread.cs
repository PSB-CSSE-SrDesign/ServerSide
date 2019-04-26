using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets; 
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace RobotServerGUI
{
    class MainThread
    {
        private const string FILETYPE = ".jpg";
        public static Form1 environment = null;

        public MainThread(Form1 f)
        {
            environment = f;
        }

        public void LaunchThread()
        {
            
            //initial setup for connection
            int imageNum = 0;
            SocketListener.StartListening(environment.GetIPAddress());
            environment.ConnectionMade();
            //environment.ConnectionMade();

            do
            {
                imageNum++;
                //get message from robot
                double velocity = SocketListener.GetMessage("input" + Convert.ToString(imageNum) + FILETYPE);
                Console.WriteLine("Image Received...");

                if (velocity != 0) {
                    //launch opencv to process image
                    CVThread m = new CVThread(imageNum, "input" + Convert.ToString(imageNum) + FILETYPE, velocity);
                    Thread cv = new Thread(new ThreadStart(m.LaunchCV));
                    cv.Start();
                    //LaunchCV();
                }


            } while (environment.terminate == false);

            //Clean up files and connection
            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\";
            string filesToDelete = @"input*.jpg";
            string[] fileList = System.IO.Directory.GetFiles(directory, filesToDelete);
            foreach(string file in fileList)
            {
                Console.WriteLine(file + " was deleted.");
                System.IO.File.Delete(file);
            }

            SocketListener.CloseConnection();

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void SendCommand(string cmd)
        {
            if (environment.terminate == false)
                SocketListener.SendMessage(cmd);
        }

    }
}