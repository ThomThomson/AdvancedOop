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
            client.MessageHandler += new ChatLib.MessageReceivedEventHandler(MessageRecieved);
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

        private void MessageRecieved(string message) {
            if(message != null) {
                Console.WriteLine("Waat");
            }
            if (ConversationText.InvokeRequired) {
                ConversationText.Invoke(new MethodInvoker(delegate (){
                    ConversationText.Text += "\t" + message;
                }));
            } else {
                ConversationText.Text += "\t" + message;
            }
        }//E N D method MessageRecieved

        private void TaskExecutorForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (listenThread != null && listenThread.IsAlive) {
                client.listening = false;
                listenThread.Join();
            }

        }

    }//E N D class GameChatForm
}//E N D namespace ChatGUI
