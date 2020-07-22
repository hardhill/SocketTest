using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ServSocket
{
    public class Laputa : WebSocketBehavior
    {
        protected override Task OnMessage(MessageEventArgs e)
        {
            string msg = "Hello";

            Send(msg);
            return OnMessage(e);
        }
    }
}