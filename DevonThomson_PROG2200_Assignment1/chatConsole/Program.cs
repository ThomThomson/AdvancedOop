using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLib;

namespace ChatConsole { 
    class Program{
        private static String userName = "";
        private static String message;
        private static ChatParent chat;
        static void Main(string[] args){
            if(args.Length > 0 && args[0] == "-server"){
                userName = "ServerUser";
                chat = new Server(13000, "127.0.0.1");
                Console.WriteLine("Server startup...");
                Server server = chat as Server;
                Console.WriteLine(server.waitForConnection());
            }else if (args.Length == 0){
                userName = "clientUser";
                chat = new Client(13000, "127.0.0.1");
                Console.WriteLine("Client startup...");
            } else {
                Console.WriteLine("Args not matched. Closing program");
                Environment.Exit(0);
            }
            //infinite L O O P for sending and recieving M E S S A G E S
            while (true) {
                if (Console.KeyAvailable) { 
                    ConsoleKeyInfo inputKey = Console.ReadKey(true);
                    if (inputKey.Key == ConsoleKey.I) {
                        Console.Write(">>");
                        message = Console.ReadLine();
                        chat.sendMessage(message);
                    }
                }
                String currentMessage = chat.recieveMessage();
                if (currentMessage.Length > 0) {
                    Console.WriteLine(currentMessage);
                }
            }
        }//E N D main
    }//E N D class
}//E N D namespace
