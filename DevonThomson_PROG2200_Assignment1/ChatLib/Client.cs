using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ChatLib {
    public class Client : ChatParent{
        //G L O B A L variables
      
        //C O N S T R U C T O R
        public Client(Int32 inPort){
            port = inPort;
        }//E N D constructor

        public String waitForServer(String inServer) {
            try {
                client = new TcpClient(inServer, port);
                return "Found Server";
            } catch (SocketException) {
                Thread.Sleep(1000);
                return "No Server";
            }
        }//E N D method waitForServer
    }//E N D class
}//E N D namespace
