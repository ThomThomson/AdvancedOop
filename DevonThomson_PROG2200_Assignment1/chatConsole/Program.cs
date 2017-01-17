using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLib;

namespace ChatConsole { 
    class Program{
        private static String message;
        private static ChatParent chat;
        static void Main(string[] args){
            if(args.Length > 0 && args[0] == "-server"){
                chat = new Server(13000, "127.0.0.1");
                Console.WriteLine("Server startup...");
                Server server = chat as Server;
                Console.WriteLine(server.waitForConnection() + "\n");
            }else if (args.Length == 0){
                chat = new Client(13000);
                Console.WriteLine("Client startup...");
                Client client = chat as Client;
                Console.WriteLine("Waiting for Server");
                while (true) {
                    if (client.waitForServer("127.0.0.1") == "Found Server") {
                        Console.WriteLine("Found a Server\n");
                        break;
                    }
                }
            }else{
                Console.WriteLine("Args not matched. Closing program");
                Environment.Exit(0);
            }
            //infinite L O O P for sending and recieving M E S S A G E S
            while (true) {
                if (Console.KeyAvailable) { 
                    ConsoleKeyInfo inputKey = Console.ReadKey(true);
                    if (inputKey.Key == ConsoleKey.I) {
                        Console.Write("\t>>");
                        message = Console.ReadLine();
                        if (message.Equals("quit")) {
                            chat.sendMessage(message);
                            chat.disconnect();
                            Environment.Exit(0);
                        } else {
                            if(chat.sendMessage(message) == "undeliverable") {
                                Console.WriteLine("The other party has ended the chat.");
                                break;
                            }
                        }                        
                    }
                }
                String currentMessage = chat.recieveMessage();
                if (currentMessage.Equals("quit")) {
                    chat.disconnect();
                    Environment.Exit(0);
                }
                else if (currentMessage.Length > 0) {
                    Console.WriteLine("\t" + currentMessage);
                }
            }
        }//E N D main
    }//E N D class
}//E N D namespace
