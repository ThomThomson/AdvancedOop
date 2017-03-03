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
        MessageReceivedEventHandler Handler;

        /// <summary>
        /// C O N S T R U C T O R method for the creation of a GameChatForm.
        /// </summary>
        public GameChatForm(Client inClient) {
            Handler = new ChatLib.MessageReceivedEventHandler(Executor_MessageRecieved);
            InitializeComponent();
            client = inClient;
            SendButton.Enabled = false;
            SendMessageText.Enabled = false;
            DisconnectMenuItem.Enabled = false;
        }
        /// <summary>
        /// Method called by the event handler each time a message is recieved.
        /// </summary>
        /// <param name="sender">The calling obejct</param>
        /// <param name="e">the MessageRecievedEventArgs argument. It contains the string of the message</param>
        private void Executor_MessageRecieved(object sender, MessageReceivedEventArgs e) {
            if (ConversationText.InvokeRequired) {
                ConversationText.Invoke(new MethodInvoker(delegate (){
                    if(e.message.Length > 0) {
                        if (e.message.Equals("quit")){
                            ConversationText.Text += "\r\nDisconnected From Server";
                            client.listening = false;
                            client.MessageHandler -= Handler;
                            ConnectMenuItem.Enabled = true;
                            SendButton.Enabled = false;
                            SendMessageText.Enabled = false;
                            DisconnectMenuItem.Enabled = false;
                        }
                        ConversationText.Text += "\r\nThem: " + e.message;
                    }
                }));
            } else {
                if (e.message.Length > 0) {
                    ConversationText.Text += "\r\nThem: " + e.message;
                }
            }
        }//E N D method MessageRecieved

        /// <summary>
        /// Method called as the form closes. Used to clean up threads etc.
        /// </summary>
        /// <param name="sender">The calling obejct</param>
        /// <param name="e">the FormClosingEventArgs argument. It contains the reason for the close</param>
        private void GameChatForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (listenThread != null && listenThread.IsAlive) {
                client.listening = false;
                client.sendMessage("quit");
                client.disconnect();
                listenThread.Join();
            }
        }//E N D listener C L O S I N G

        /// <summary>
        /// Method activated when the send button is clicked
        /// </summary>
        /// <param name="sender">The calling obejct</param>
        /// <param name="e">the arguments sent to the event</param>
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

        /// <summary>
        /// Method activated when the connect button in the menu is clicked
        /// </summary>
        /// <param name="sender">The calling obejct</param>
        /// <param name="e">the arguments sent to the event</param>
        private void ConnectMenuItem_Click(object sender, EventArgs e) {
            if (client.waitForServer("127.0.0.1")) {
                client.MessageHandler += Handler;
                listenThread = new Thread(client.receiveMessage);
                listenThread.Name = "listener";
                listenThread.Start();
                ConversationText.Text += "\r\nConnected to Server";
                client.listening = true;
                ConnectMenuItem.Enabled = false;
                SendButton.Enabled = true;
                SendMessageText.Enabled = true;
                DisconnectMenuItem.Enabled = true;
            }
            else {
                ConversationText.Text += "\r\nCould not Connect";
            }
        }//E N D listener C O N N E C T

        /// <summary>
        /// Method activated when the disconnect button in the menu is clicked
        /// </summary>
        /// <param name="sender">The calling obejct</param>
        /// <param name="e">the arguments sent to the event</param>
        private void DisconnectMenuItem_Click(object sender, EventArgs e) {
            ConversationText.Text += "\r\nDisconnected From Server";
            client.listening = false;
            client.MessageHandler -= Handler;
            ConnectMenuItem.Enabled = true;
            listenThread.Join();
            client.disconnect();
            SendButton.Enabled = false;
            SendMessageText.Enabled = false;
            DisconnectMenuItem.Enabled = false;
        }

        /// <summary>
        /// Method activated when the exit button in the menu is clicked
        /// </summary>
        /// <param name="sender">The calling obejct</param>
        /// <param name="e">the arguments sent to the event</param>
        private void ExitMenuItem_Click(object sender, EventArgs e) {
            if (listenThread != null && listenThread.IsAlive) {
                client.listening = false;
                client.sendMessage("quit");
                client.disconnect();
                listenThread.Join();
            }
            Application.Exit();
        }
    }//E N D class GameChatForm
}//E N D namespace ChatGUI
