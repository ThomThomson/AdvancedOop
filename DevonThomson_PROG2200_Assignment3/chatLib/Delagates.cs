using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib {
    /// <summary>
    /// A delegate that connects the messageRecieved function in chat parent to the GUI layer
    /// </summary>
    /// <param name="sender">the calling object</param>
    /// <param name="e">The MessageReceivedEventArgs to pass on</param>
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
}
