using System;
using System.IO;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ServSocket
{
    public class Laputa : WebSocketBehavior
    {
        protected override Task OnMessage(MessageEventArgs e)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Stream st = e.Data;
                StreamReader reader = new StreamReader(st);
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
                Send($"{DateTime.Now}");
            });
            return task;
        }

        protected override Task OnOpen()
        {
            var task = Task.Factory.StartNew(() => {
                Console.WriteLine($"Active connections:{Sessions.Count}");
            });
            return task;
        }

        protected override Task OnClose(CloseEventArgs e)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Active connections:{Sessions.Count}");
            });
            return task;
        }


    }
}