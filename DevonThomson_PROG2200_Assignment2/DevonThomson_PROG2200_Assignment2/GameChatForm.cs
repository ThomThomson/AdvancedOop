using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatLib;
using System.Threading;

namespace ChatGUI {
    public partial class GameChatForm : Form {
        //G L O B A L variables
        Client client;
        Thread listenThread;

        //C O N S T R U C T O R
        public GameChatForm() {
            InitializeComponent();
            client = new Client(13000);
            client.MessageHandler += new ChatLib.MessageReceivedEventHandler(Executor_MessageRecieved);
            ConnectMe();
        }

        public void ConnectMe() {
            while (true) {
                if (client.waitForServer("127.0.0.1")) {
                    ConnectedLabel.Text = "CONNECTED";
                    listenThread = new Thread(client.recieveMessage);
                    listenThread.Name = "listener";
                    listenThread.Start();
                    break;
                }
            }
        }//E N D method ConnectMe

        private void Executor_MessageRecieved(object sender, MessageReceivedEventArgs e) {
            if (ConversationText.InvokeRequired) {
                ConversationText.Invoke(new MethodInvoker(delegate (){
                    if(e.message.Length > 0) {
                        ConversationText.Text += "\r\nThem: " + e.message;
                    }
                }));
            } else {
                if (e.message.Length > 0) {
                    ConversationText.Text += "\r\nThem: " + e.message;
                }
            }
        }//E N D method MessageRecieved

        private void GameChatForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (listenThread != null && listenThread.IsAlive) {
                client.listening = false;
                client.disconnect();
                listenThread.Join();
            }
        }//E N D listener C L O S I N G

        private void SendButton_Click(object sender, EventArgs e) {
            if(SendMessageText.Text.Length > 0) {
                if (client.sendMessage(SendMessageText.Text)) {
                    ConversationText.Text += "\r\nMe: " + SendMessageText.Text;
                }else {
                    ConversationText.Text += "\r\nUNDELIVERABLE MESSAGE";
                }
                SendMessageText.Text = "";
            }
        }//E N D listener S E N D

    }//E N D class GameChatForm
}//E N D namespace ChatGUI
