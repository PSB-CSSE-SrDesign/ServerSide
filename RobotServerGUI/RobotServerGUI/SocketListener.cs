using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotServerGUI
{
    public class SocketListener
    {
        private static Socket handler = null;

        public static void StartListening()
        {
            // Establish the local endpoint for the socket. 
            // Dns.GetHostName returns the name of the host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            Console.WriteLine(ipAddress.ToString());
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 27015);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Program is suspended while waiting for an incoming connection. 
                Console.WriteLine("Waiting for a connection...");
                handler = listener.Accept();
                Console.WriteLine("Connection made...");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static double GetMessage(string fileName)
        {
            byte[] message = new byte[700000]; //TODO: make smaller
            int readSum = 0, readNum = 0, fileSize = 0;
            double velocity = 0;
            int j = 0;

            do
            {
                readNum = handler.Receive(message, readSum, 1000, 0);

                if (j == 0)
                {
                    fileSize = BitConverter.ToInt32(message, 4);
                    velocity = BitConverter.ToDouble(message, 0);
                }

                if (readNum > 0)
                {
                    j++;
                    readSum += readNum;
                }
            } while (readSum < fileSize && readSum < 700000);

            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\" + fileName;
            FileStream fos = new FileStream(directory, FileMode.Create);
            fos.Write(message, 8, readSum - 8);
            fos.Close();

            return velocity;
        }

        public static void SendMessage(string m)
        {
            // Set command and send message
            byte[] message = new byte[8];
            message = Encoding.ASCII.GetBytes(m);
            handler.Send(message);
        }

        public static void CloseConnection()
        {
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            handler = null;
        }
    }
}
