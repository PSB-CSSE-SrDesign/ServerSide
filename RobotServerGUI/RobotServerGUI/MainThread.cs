using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets; 
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

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
            int imageNum = 1;
            SocketListener.StartListening();

            while (environment.terminate == false)
            {
                //get message from robot
                float velocity = SocketListener.GetMessage("input" + Convert.ToString(imageNum) + FILETYPE);
                Console.WriteLine("Image Received...");

                //launch opencv to process image
                string cmd = LaunchCV("input" + Convert.ToString(imageNum) + FILETYPE, velocity);

                SendCommand(cmd);
                Console.WriteLine("Command Sent...");

                environment.SetParameters("input" + Convert.ToString(imageNum) + FILETYPE, "input" + Convert.ToString(imageNum) + "_masked" + FILETYPE, velocity);
                if (imageNum > 5)
                    cleanUp(imageNum - 5);
                    //Task.Run(() => cleanUp(imageNum - 5));

                imageNum++;
            }

        }
               
        //launches OpenCV and send the data to interpret as a command
        private static string LaunchCV(string fileName, float velocity)
        {
            //Sets up process to launch OpenCV program through cosole
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\OpenCV.exe";
            pProcess.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\";
            string args =  " " + Convert.ToString(environment.GetRedValue()) + " " + Convert.ToString(environment.GetBlueValue()); //gets red and blue tolerances
            pProcess.StartInfo.Arguments = fileName + args;
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

            string command = getCommand(Convert.ToDouble(angleStrings[0]), Convert.ToDouble(angleStrings[1]), velocity);

            return command;
        }

        private static void SendCommand(string cmd)
        {
            SocketListener.SendMessage(cmd);
        }

        //TODO: Implement Velocity
        private static string getCommand(double redAngle, double blueAngle, float velocity)
        {
            string cmd = "DONO";

            if (redAngle != 0)
            {
                if (Math.Abs(redAngle) < 5) //foe in front
                {
                    cmd = DIRECTIONS[1] + "125";
                }
                else if (redAngle < -5) //foe to left
                {
                    cmd = DIRECTIONS[3] + Math.Abs(redAngle);
                }
                else if (redAngle > 5) //foe to right
                {
                    cmd = DIRECTIONS[2] + redAngle;
                }
            }
            else if (blueAngle != 0)
            {
                if (Math.Abs(blueAngle) < 5) //friend in front
                {
                    cmd = DIRECTIONS[0] + "125";
                }
                else if (blueAngle < -5) //friend to left
                {
                    cmd = DIRECTIONS[3] + Math.Abs(blueAngle);
                }
                else if (blueAngle > 5) //friend to right
                {
                    cmd = DIRECTIONS[2] + blueAngle;
                }
            }

            return cmd;
        }

        private static void cleanUp(int imgNum)
        {        
            //directory to find images
            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\";

            File.Delete(directory + "input" + Convert.ToString(imgNum) + FILETYPE);
            File.Delete(directory + "input" + Convert.ToString(imgNum) + "_masked" + FILETYPE);

            return;
        }
    }
}