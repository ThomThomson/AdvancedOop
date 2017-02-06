using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatLib {
    public class Client : ChatParent {
        //C O N S T R U C T O R
        public Client(Int32 inPort) {
            port = inPort;
        }//E N D constructor

        public bool waitForServer(String inServer) {
            try {
                client = new TcpClient(inServer, port);
                return true;
            } catch (SocketException) {
                Thread.Sleep(1000);
                return false;
            }
        }//E N D method waitForServer
    }//E N D class
}//E N D namespace
