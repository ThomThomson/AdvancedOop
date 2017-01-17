using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib {
    public class Server : ChatParent {
        //G L O B A L variables
        TcpListener server = null;
        IPAddress address;

        //C O N S T R U C T O R
        public Server(Int32 inPort, String inAddress){
            address = IPAddress.Parse(inAddress);
            port = inPort;
            server = new TcpListener(address, port);
        }//E N D constructor

        //M E T H O D S for server-side communication
        public string waitForConnection() {
            try {
                server.Start();
                client = server.AcceptTcpClient();//blocking call that waits for a client connection.
                return "Connection Success";//return to indicate successful connection
            }catch(SocketException e) {
                return "SocketException: " + e.Message;//return stacktrace of error
            }            
        }//E N D method waitForConnection
    }//E N D class
}//E N D namespace
