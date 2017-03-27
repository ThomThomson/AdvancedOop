using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Managers.InputManager InputManager = new Managers.InputManager();
            Managers.StateManager stateManager = new Managers.StateManager();
            List<GameObjects.IGameObject> StartingObjectList = new List<GameObjects.IGameObject>();

            //Create Gameobjects present at Game Start
            GameObjects.ObjectTypes.Landscape landscape = new GameObjects.ObjectTypes.Landscape(50, 100, 10, 5, 3, 4);
            StartingObjectList.Add(landscape);

            GameObjects.ObjectTypes.KeyPlayer player = new GameObjects.ObjectTypes.KeyPlayer(InputManager, landscape, stateManager);
            StartingObjectList.Add(player);
            //End startup objects

            stateManager.setObjectList(StartingObjectList);
            Application.Run(new GameForm(stateManager, InputManager));
        }
    }
}
