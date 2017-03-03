using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib {
    /// <summary>
    /// A class to contain the data arguments that need to be 
    /// </summary>
    public class MessageReceivedEventArgs {
        public String message { get; }
        public MessageReceivedEventArgs(String inMessage) {
            message = inMessage;
        }

    }
}
