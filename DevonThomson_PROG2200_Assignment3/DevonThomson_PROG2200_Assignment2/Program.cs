﻿using System;
using System.Windows.Forms;
using LogLib;
using ChatLib;
using Microsoft.Practices.Unity;
using Logger;

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

            //container.RegisterType<iLoggingService, TxtLogger>();
            //container.RegisterType<LogLib.ILoggingService, NSpringLogger>();
            container.RegisterType<Logger.ILoggingService, Logger.Logger>();
           
            container.RegisterInstance<string>("Log" + currentDate + ".txt");//Registering log location
            Application.Run(container.Resolve<GameChatForm>());
        }
    }
}