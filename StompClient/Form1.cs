using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace StompClient
{
    public partial class Form1 : Form
    {
        WebSocket client;
        StompMessageSerializer serializer;
        public Form1()
        {
            InitializeComponent();
            client = new WebSocket("ws://localhost:9189/wsrpa/websocket");
            client.OnOpen += Client_OnOpen;
            client.OnClose += Client_OnClose;
            client.OnError += Client_OnError;
            client.OnMessage += Client_OnMessage;
            serializer = new StompMessageSerializer();
        }

        private void Client_OnMessage(object sender, MessageEventArgs e)
        {
            textBox1.Text = textBox1.Text + "Message: " +e.Data+ "\r\n";
        }

        private void Client_OnError(object sender, ErrorEventArgs e)
        {
            textBox1.Text = textBox1.Text + "Error: "+e.Message + "\r\n";
        }

        private void Client_OnClose(object sender, CloseEventArgs e)
        {
            textBox1.Text = textBox1.Text + "Closed" + "\r\n";
        }

        private void Client_OnOpen(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "Opened" + "\r\n";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
			try{
                client.Connect();
            }catch(Exception err)
            {
                textBox1.Text = textBox1.Text + err.Message+"\r\n";
            }

		}

        private void button2_Click(object sender, EventArgs e)
        {
            client.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var content = new Content() { from = "BotX", message = "Hello World!!" };
            var broad = new StompMessage("SEND", JsonConvert.SerializeObject(content));
            broad["content-type"] = "application/json";
            broad["destination"] = "/app/botmessage";
            string msg = serializer.Serialize(broad);
            client.Send(msg);
        }
    }
}
