using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace SocketClient
{
    public partial class Form1 : Form
    {
        WebSocket ws;
        public Form1()
        {
            InitializeComponent();
            ws = new WebSocket("ws://127.0.0.1:9000/Laputa",onError:OnError,onMessage:OnMessage,onClose:OnCloseConnection,onOpen:OnOpenConnection);
            
        }

        private Task OnError(WebSocketSharp.ErrorEventArgs arg)
        {
            label1.Text = arg.Message;
            return Task.FromResult(0);
        }

        private Task OnOpenConnection()
        {
            BackColor = Color.LawnGreen;
            return Task.FromResult(0);
        }

        private Task OnCloseConnection(CloseEventArgs arg)
        {
            BackColor = Color.White;
            return Task.FromResult(0);
        }

        private Task OnMessage(MessageEventArgs arg)
        {
           StreamReader sr = new StreamReader(arg.Data);
            label1.Text = sr.ReadToEnd();
            return Task.FromResult(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ws.Connect().Wait();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ws.Close().Wait();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ws != null && ws.ReadyState == WebSocketState.Open) {
                ws.Send("time");
            }
        }
    }
}
