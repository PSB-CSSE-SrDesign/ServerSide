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

        public static void StartListening(IPAddress ipAddress)
        {
            // Establish the local endpoint for the socket. 
            // Dns.GetHostName returns the name of the host running the application.  
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
            byte[] message = new byte[700000]; 
            int readSum = 0, readNum = 0, fileSize = 0;
            float velocity = 0;
            List<int> readNums = new List<int>();

            while (readSum < 8)
            {
                readNum = handler.Receive(message, readSum, 8, 0);
                readNums.Add(readNum);
                readSum += readNum;
            }

            fileSize = BitConverter.ToInt32(message, 4);
            velocity = BitConverter.ToSingle(message, 0);

            if (fileSize <= 700000)
            {
                do
                {
                    //number of bytes remaining to read, with max 1000
                    int remaining = 1000;
                    if (fileSize > 0 && fileSize - readSum + 8 < 1000)
                        remaining = fileSize - readSum + 8;

                    readNum = handler.Receive(message, readSum, remaining, 0);

                    readNums.Add(readNum);
                    readSum += readNum;
                } while (readSum < fileSize);

                string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\OpenCV\" + fileName;
                FileStream fos = new FileStream(directory, FileMode.Create);
                fos.Write(message, 8, readSum - 8);
                fos.Close();

                return velocity;
            }
            else
            {

                byte[] eof = {0xFF, 0xD9};
                do
                {
                    message[0] = message[1];
                    readNum = handler.Receive(message, 1, 1, 0);
                } while (message[0] != eof[0] || message[1] != eof[1]);

                return 0;
            }
        }

        public static void TossMessage()
        {
            byte[] message = new byte[8];
            while (BitConverter.ToString(message) != "CONFIRM!")
            {
                while (message[0] != 'C')
                {
                    handler.Receive(message, 0, 1, 0);
                }
                handler.Receive(message, 1, 7, 0);
            }
            return;
        }

        public static void SendMessage(string m)
        {
            // Set command and send message
            byte[] message = new byte[8];
            message = Encoding.ASCII.GetBytes(m);
            handler.Send(message);
        }

        public static bool CheckConnection()
        {
            if (handler == null)
                return false;
            else
                return handler.Connected;
        }

        public static void CloseConnection()
        {
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            handler = null;
        }
    }
}
