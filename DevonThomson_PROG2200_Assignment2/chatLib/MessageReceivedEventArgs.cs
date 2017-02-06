using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib {
    public class MessageReceivedEventArgs {
        public String message { get; }
        public MessageReceivedEventArgs(String inMessage) {
            message = inMessage;
        }

    }
}
