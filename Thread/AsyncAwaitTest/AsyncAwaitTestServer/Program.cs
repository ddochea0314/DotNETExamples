using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitTestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("server start");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 5000));

            socket.Listen(10);
            while(true)
            {
                var client = socket.Accept();
                Task.Run(async () =>
                {
                    while (true)
                    {
                        byte[] buffer = new byte[1024];
                        int size = client.Receive(buffer);

                        string str = Encoding.UTF8.GetString(buffer, 0, size);
                        Console.WriteLine($"client:{str}");
                    }
                });
            }
        }
    }
}
