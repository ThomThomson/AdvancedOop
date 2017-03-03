using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSpring.Logging.Loggers;

namespace Logging{
    public class NSpringLogger : ILoggingService{
        FileLogger logger;
        /// <summary>
        /// Logger constructor
        /// </summary>
        /// <param name="inFileName">the full path to the file to keep logs in</param>
        public NSpringLogger(string inFileName){
            logger = new FileLogger(inFileName);
            logger.Open();
        }
        public void Log(string message){
            logger.Log(message);
        }
    }
}
