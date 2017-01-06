using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLib;

namespace ChatConsole { 
    class Program
    {
        static void Main(string[] args){
            if(args.Length > 0){
                Server server = new Server();
            }
            else{
                Client client = new Client();
            }

        }
    }
}
