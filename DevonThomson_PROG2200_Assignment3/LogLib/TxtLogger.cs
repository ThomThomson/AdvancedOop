using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Logging{
    public class TxtLogger : ILoggingService{
        public string FileName { get; set; }

        /// <summary>
        /// Logger constructor
        /// </summary>
        /// <param name="inFileName">the full path to the file to keep logs in</param>
        public TxtLogger(string inFileName) {
            FileName = inFileName;
        }
        /// <summary>
        /// Logging functionality
        /// </summary>
        /// <param name="inLine">the line to be logged</param>
        public void Log(string inLine) {
            using (FileStream FileStream = new FileStream(FileName, FileMode.Append, FileAccess.Write)) {
                using (StreamWriter Writer = new StreamWriter(FileStream)) {
                    Writer.WriteLine(inLine);
                }
            }   
        }//E N D method Log
    }
}
