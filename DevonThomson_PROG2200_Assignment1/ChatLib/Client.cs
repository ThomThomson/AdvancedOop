using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib {
    public class Client : ChatParent{
        //G L O B A L variables
      
        //C O N S T R U C T O R
        public Client(Int32 inPort, String inServer){
            port = inPort;
            client = new TcpClient(inServer, port);
        }//E N D constructor


    }//E N D class
}//E N D namespace
