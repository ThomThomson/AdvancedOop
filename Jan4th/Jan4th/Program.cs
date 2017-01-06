using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jan4thIO {
    class Program {
        static void Main(string[] args) {
            while (true) {
                if (Console.KeyAvailable) {
                    Console.WriteLine("HIT DAT I KEY PLZ");
                    ConsoleKeyInfo fartKey = Console.ReadKey(true);
                    if (fartKey.Key == ConsoleKey.X) {
                        Console.WriteLine("AW SHIT YOU TYPED X");
                    } else {
                        Console.WriteLine("YOU DID NOT TYPE X");
                    }
                }
                //This will only run due to the key available if statement. Otherwise it will wait each time.
                Console.WriteLine("Listening...");
            }
        }
    }
}
