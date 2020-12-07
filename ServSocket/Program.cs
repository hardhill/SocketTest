using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;


namespace ServSocket
{
    class Program
    {
        static IPAddress serverIp = IPAddress.Any;
        static int serverPort = 9000;
        static bool ssl = false;

        static void Main(string[] args)
        {
            var wssv = new WebSocketServer(serverIp,serverPort);
            wssv.AddWebSocketService<Laputa>("/Laputa");
            wssv.Start();
            Console.ReadKey(true);
            wssv.Stop();

        }


    }

}
