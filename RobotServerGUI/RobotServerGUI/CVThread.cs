using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotServerGUI
{
    class CVThread
    {
        private int imageNum;
        private string fileName;
        private static string friendFoeText;
        private double velocity;
        private static readonly String[] DIRECTIONS = { "FORW", "BACK", "LEFT", "RIGT" };
        private const string FILETYPE = ".jpg";

        public CVThread(int imageNum, string fileName, double velocity)
        {
            this.imageNum = imageNum;
            this.fileName = fileName;
            this.velocity = velocity;
        }

        //launches OpenCV and send the data to interpret as a command
        public void LaunchCV()
        {
            //Sets up process to launch OpenCV program through cosole
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\OpenCV.exe";
            pProcess.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\";
            string args = " " + Convert.ToString(MainThread.environment.GetRedValue()) + " " + Convert.ToString(MainThread.environment.GetBlueValue()); //gets red and blue tolerances
            pProcess.StartInfo.Arguments = fileName + args;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pProcess.StartInfo.CreateNoWindow = true; //not diplay a windows

            //create timer
            Stopwatch s = new Stopwatch();
            s.Reset();
            s.Start();

            //launches process
            pProcess.Start();
            string output = pProcess.StandardOutput.ReadToEnd(); //The output result
            pProcess.WaitForExit();

            //display time to get message
            s.Stop();
            TimeSpan ts = s.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime + '\n');

            //parses output to get angles
            Console.WriteLine(output);
            if (output != "")
            {
                String[] angleStrings = output.Split(' ');

                string command = getCommand(Convert.ToDouble(angleStrings[0]), Convert.ToDouble(angleStrings[1]), velocity);
                Console.WriteLine(imageNum + ": " + command);
                MainThread.SendCommand(command);
            }

            this.velocity = Math.Round(this.velocity,2);
            MainThread.environment.SetParameters("input" + Convert.ToString(imageNum) + FILETYPE, "input" + Convert.ToString(imageNum) + "_masked" + FILETYPE, velocity);
            MainThread.environment.SetTextBox2(friendFoeText);

            //if (imageNum > 5)
            //    CleanUp(imageNum - 5);

            return;
        }

        //TODO: Implement Velocity
        private static string getCommand(double redAngle, double blueAngle, double velocity)
        {
            string cmd = "DONO0000";
            string redAngleZ = "";
            string blueAngleZ = "";

            if (Math.Abs(redAngle) < 10)
            {
                redAngleZ = "0";
            }
            if (Math.Abs(blueAngle) < 10)
            {
                blueAngleZ = "0";
            }

            if (redAngle != 0)
            {
                friendFoeText = "Foe";
                if (Math.Abs(redAngle) < 5) //foe in front
                {
                    cmd = DIRECTIONS[1] + "0125";
                }
                else if (redAngle < -5) //foe to left
                {
                    redAngle *= 100;
                    cmd = DIRECTIONS[3] + redAngleZ + Math.Abs(redAngle);
                }
                else if (redAngle > 5) //foe to right
                {
                    redAngle *= 100;
                    cmd = DIRECTIONS[2] + redAngleZ + redAngle;
                }
            }
            else if (blueAngle != 0)
            {
                friendFoeText = "Friend";
                if (Math.Abs(blueAngle) < 5) //friend in front
                {
                    cmd = DIRECTIONS[0] + "0125";
                }
                else if (blueAngle < -5) //friend to left
                {
                    blueAngle *= 100;
                    cmd = DIRECTIONS[3] + blueAngleZ + Math.Abs(blueAngle);
                }
                else if (blueAngle > 5) //friend to right
                {
                    blueAngle *= 100;
                    cmd = DIRECTIONS[2] + blueAngleZ + blueAngle;
                }
            }
            else
            {
                friendFoeText = "";
            }

            return cmd;
        }

        private static void CleanUp(int imgNum)
        {
            //directory to find images
            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\";

            File.Delete(directory + "input" + Convert.ToString(imgNum) + FILETYPE);
            File.Delete(directory + "input" + Convert.ToString(imgNum) + "_masked" + FILETYPE);

            return;
        }
    }
}
