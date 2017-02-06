using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib{
    public class ChatParent {
        //G L O B A L variables and P R O P E R T I E S
        public Int32 port { get; set; }
        public TcpClient client = null;
        public event MessageReceivedEventHandler MessageHandler;
        public volatile bool listening = true;
        NetworkStream stream;

        //M E T H O D S  common to server & client
        public bool sendMessage(String message) {
            try {
                stream = client.GetStream();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                return true;
            } catch (SocketException e) {
                return false;
            } catch (System.IO.IOException e) {
                return false;
            }
        }//E N D method sendMessage

        public void recieveMessage() {
            while (listening) {
                stream = client.GetStream();
                Byte[] data = new Byte[256];//empty byte array to read the message
                String responseData = "";
                while (stream.CanRead && stream.DataAvailable) {
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    if (MessageHandler != null) {
                        MessageHandler(this, new MessageReceivedEventArgs(responseData));
                    }
                }
            }
        }//E N D method recieveMessage

        public void disconnect() {
            stream.Close();
            client.Close();
        }
    }//E N D class
}//E N D namespace
