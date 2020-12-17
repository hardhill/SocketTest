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
        delegate string MessageRes();
        public Form1()
        {
            InitializeComponent();
            ws = new WebSocket("ws://127.0.0.1:9189/wsrpa",onError:OnError,onMessage:OnMessage,onClose:OnCloseConnection,onOpen:OnOpenConnection);


        }

        private Task OnError(WebSocketSharp.ErrorEventArgs arg)
        {
            this.label1.BeginInvoke((MethodInvoker)delegate { this.label1.Text = arg.Message; });
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
            label1.Invoke((MethodInvoker)delegate { label1.Text = sr.ReadToEnd(); });
            return Task.FromResult(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ws.Connect().Wait();
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ws.Close().Wait();
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ws != null ) {
                ws.Send("time");
            }
        }
    }
}
