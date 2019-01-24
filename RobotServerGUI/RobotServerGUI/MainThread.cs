using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets; 
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace RobotServerGUI
{
    class MainThread
    {
        private const string FILETYPE = ".jpg";
        private static readonly String[] DIRECTIONS = { "FORW", "BACK", "LEFT", "RIGT" };
        private static Form1 environment = null;

        public static void SetEnvironment(Form1 f)
        {
            environment = f;
        }

        public static void LaunchThread()
        {
            //initial setup for connection
            int imageNum = 0;
            SocketListener.StartListening();

            //get message from robot
            float velocity = SocketListener.GetMessage("input" + FILETYPE);
            Console.WriteLine("Image Received...");
            
            //launch opencv to process image
            string cmd = LaunchCV("input.jpg");

            SendCommand(cmd);
            Console.WriteLine("Command Sent...");
            environment.SetParameters("input.jpg", "input_masked.jpg", velocity);

        }

        public static void SendCommand(string cmd)
        {
            SocketListener.SendMessage(cmd);
        }

        //launches OpenCV and send the data to interpret as a command
        public static string LaunchCV(string fileName)
        {
            //Sets up process to launch OpenCV program through cosole
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\OpenCV.exe";
            pProcess.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\";
            pProcess.StartInfo.Arguments = fileName + " .75 .75"; //argument
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pProcess.StartInfo.CreateNoWindow = true; //not diplay a windows

            //launches process and st
            pProcess.Start();
            string output = pProcess.StandardOutput.ReadToEnd(); //The output result
            pProcess.WaitForExit();
            //Console.Write(output);

            //parses output to get angles
            String[] splitter = { "\r\n" };
            String[] lines = output.Split(splitter, 10, StringSplitOptions.RemoveEmptyEntries);
            String[] angleStrings = lines[2].Split(' ');

            string command = getCommand(Convert.ToDouble(angleStrings[0]), Convert.ToDouble(angleStrings[1]));

            return command;
        }

        public static string getCommand(double redAngle, double blueAngle)
        {
            //If red is detected, priority 1
            //If object is within +-5 degrees, priority 2
            //If object outside range, priority 3

            return "string";
        }
    }
}