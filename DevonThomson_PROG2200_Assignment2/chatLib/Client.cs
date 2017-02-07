using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogLib;

namespace ChatLib {
    public class Client : ChatParent {
        //C O N S T R U C T O R
        public Client(Int32 inPort) {
            string currentDate = DateTime.Now.ToString(@"MM-dd-yyyy-h_mmtt");
            logger = new Logger("H:\\NSCC\\Winter 2017\\TestingContent\\logs\\" + currentDate + ".txt");
            port = inPort;
        }//E N D constructor

        public bool waitForServer(String inServer) {
            try {
                client = new TcpClient(inServer, port);
                logger.Log(DateTime.Now.ToString(@"MM-dd-yyyy-h\:mm tt") + " Client Connected to server at " + inServer + " port " + port);
                return true;
            } catch (SocketException) {
                return false;
            }
        }//E N D method waitForServer
    }//E N D class
}//E N D namespace
