using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
//using LogLib;//MY LOGGING LIBRARY
using Logging;//BEN'S LOGGING LIBRARY

namespace ChatLib{
    public class ChatParent {
        //G L O B A L variables and P R O P E R T I E S
        public Int32 port { get; set; }
        public TcpClient client = null;
        public event MessageReceivedEventHandler MessageHandler;
        public volatile bool listening = true;
        public ILoggingService logger;
        NetworkStream stream;


        //M E T H O D S  common to server & client

        /// <summary>
        /// The method to send a message over the stream
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <returns>boolean representing successful sending of the message</returns>
        public bool sendMessage(String message) {
            try {
                stream = client.GetStream();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                logger.Log(DateTime.Now.ToString(@"MM-dd-yyyy-h\:mm tt") + "- Me: " + message);
                return true;
            } catch (SocketException e) {
                return false;
            } catch (System.IO.IOException e) {
                return false;
            }
        }//E N D method sendMessage

        /// <summary>
        /// The method called when recieving a message from the stream
        /// </summary>
        public void receiveMessage() {
            while (listening) {
                stream = client.GetStream();
                Byte[] data = new Byte[256];//empty byte array to read the message
                String responseData = "";
                while (stream.CanRead && stream.DataAvailable) {
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    if (MessageHandler != null) {
                        MessageHandler(this, new MessageReceivedEventArgs(responseData));
                        logger.Log(DateTime.Now.ToString(@"MM-dd-yyyy-h\:mm tt") + "- Them: " + responseData);
                    }
                }
            }
        }//E N D method recieveMessage

        /// <summary>
        /// The method called to disconnect the chat object
        /// </summary>
        public void disconnect() {
            stream.Close();
            client.Close();
        }
    }//E N D class
}//E N D namespace
