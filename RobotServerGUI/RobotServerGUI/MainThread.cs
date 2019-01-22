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
        private static Form1 environment = null;
        private static int imageNum = 0;

        public static void SetEnvironment(Form1 f)
        {
            environment = f;
        }

        public static void LaunchThread()
        {
            SocketListener.StartListening();
            float velocity = SocketListener.GetMessage("input" + FILETYPE);
            Console.WriteLine("Image Received...");

            Stopwatch s = new Stopwatch();
            s.Start();
            LaunchCV("input.jpg");
            s.Stop();
            Console.WriteLine(s.Elapsed);


            SendCommand();
            Console.WriteLine("Command Sent...");
            environment.SetParameters("input.jpg", "input_masked.jpg", velocity);

        }

        public static void SendCommand()
        {
            SocketListener.SendMessage("FORW0252");
        }

        public static void LaunchCV(string fileName)
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
            String[] angles = lines[2].Split(' ');
            Console.WriteLine(angles[0]);
            Console.WriteLine(angles[1]);
        }
    }
}