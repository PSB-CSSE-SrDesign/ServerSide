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

        public static float GetMessage(string fileName)
        {
            // Get header from message and parse into velocity and file size
            byte[] header = new byte[8];
            float velocity;
            int fileSize;
            int sentFileSize;
            int readHeader = handler.Receive(header, 8, 0);
            velocity = BitConverter.ToSingle(header, 0);
            fileSize = BitConverter.ToInt32(header, 4);
            if (fileSize > 200000)
            {
                sentFileSize = fileSize;
                //fileSize = 200000;
            }

            // Get body of message and save as file
            byte[] body = new byte[fileSize];
            int readBody = handler.Receive(body, fileSize, 0);
            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\" + fileName;
            FileStream fos = new FileStream(directory, FileMode.Create);
            fos.Write(body, 0, fileSize);
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
