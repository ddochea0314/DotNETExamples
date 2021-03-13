using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsyncAwaitTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("client start");
            Socket client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            while (true)
            {
                var data = Console.ReadLine();
                if (!client.Connected)
                {
                    client.Connect("18.183.87.223", 5000);
                }
                client.Send(Encoding.UTF8.GetBytes(data));
            }
        }
    }
}
