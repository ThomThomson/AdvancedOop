using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib {
    public class ChatParent {
        //G L O B A L variables and P R O P E R T I E S
        public Int32 port{ get; set; }
        public TcpClient client = null;
        NetworkStream stream;

        //M E T H O D S  common to server & client
        public String sendMessage(String message) {
            try {
                stream = client.GetStream();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                return "Sent Successfully";
            } catch(SocketException e) {
                return "SocketException: " + e.Message;//return stacktrace of error
            }catch(System.IO.IOException e) {
                return "IOException: " + e.Message;//return stacktrace of error
            }
        }//E N D method sendMessage

        public String recieveMessage() {
            stream = client.GetStream();
            Byte[] data = new Byte[256];//empty byte array to read the message
            String responseData = "";
            while (stream.CanRead && stream.DataAvailable) {
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            }                            
            return responseData;
        }//E N D method recieveMessage

        public void disconnect() {
            stream.Close();
            client.Close();
        }
     }//E N D class
}//E N D namespace
