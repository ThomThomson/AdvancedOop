using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogLib{
    public class Logger{
        public string FileName { get; set; }

        //C O N S T R U C T O R
        public Logger(string inFileName) {
            FileName = inFileName;
        }

        //the O N L Y functionality required by Logger.
        public void Log(string inLine) {
            using (FileStream FileStream = new FileStream(FileName, FileMode.Append, FileAccess.Write)) {
                using (StreamWriter Writer = new StreamWriter(FileStream)) {
                    Writer.WriteLine(inLine);
                }
            }   
        }//E N D method Log
    }
}
