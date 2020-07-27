using System;
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
                Send($"{DateTime.Now}");
            });
            return task;
        }
    }
}