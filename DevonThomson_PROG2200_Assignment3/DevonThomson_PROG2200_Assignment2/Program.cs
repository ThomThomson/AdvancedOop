using System;
using System.Windows.Forms;
using ChatLib;
using Microsoft.Practices.Unity;
//using Ninject;
//using LogLib;//MY LOGGING LIBRARY
using Logging;//BEN'S LOGGING LIBRARY

namespace ChatGUI {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string currentDate = DateTime.Now.ToString(@"MM-dd-yyyy-h_mmtt");

            //C O N S T R U C T O R  I N J E C T I O N
            //Application.Run(new GameChatForm(new Client(new TxtLogger("Log" + currentDate + ".txt"))));

            //C O N T A I N E R  I N J E C T I O N -- U N I T Y
            UnityContainer container = new UnityContainer();
            //container.RegisterType<ILoggingService, TxtLogger>();//MY TEXTLOGGER
            //container.RegisterType<ILoggingService, NSpringLogger>();//NSPRING LOGGER
            container.RegisterType<ILoggingService, LogLib.TextLogger>();//BEN'S TEXTLOGGER
            container.RegisterInstance<string>("Log" + currentDate + ".txt");//Registering log location
            Application.Run(container.Resolve<GameChatForm>());

            //C O N T A I N E R  I N J E C T I O N -- S T R U C T U R E M A P
            //IKernel njContainer = new StandardKernel();
            ////kernel.Bind<ILoggingService>().To<ChatLogger>();
            //njContainer.Bind<LogLib.ILoggingService>().To<TxtLogger>();
            //njContainer.Bind<string>().ToConstant("Log" + currentDate + ".txt");
            //Application.Run(njContainer.Get<GameChatForm>());
        }
    }
}
