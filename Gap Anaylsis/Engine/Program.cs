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
            Managers.InputManager inputManager = new Managers.InputManager();
            GameForm gf = new GameForm(inputManager);
            startup(inputManager, gf.State);
            Application.Run(gf);
        }

        public static void startup(Managers.InputManager inputManager, Managers.StateManager stateManager){
            List<GameObjects.IGameObject> StartingObjectList = new List<GameObjects.IGameObject>();
            //Create Gameobjects present at Game Start
            GameObjects.ObjectTypes.Landscape landscape = new GameObjects.ObjectTypes.Landscape(50, 100, 1, 3, 3, 4);
            StartingObjectList.Add(landscape);
            GameObjects.ObjectTypes.AStarChar character = new GameObjects.ObjectTypes.AStarChar(landscape, inputManager);
            StartingObjectList.Add(character);
            //End startup objects
            stateManager.setObjectList(StartingObjectList);
        }
    }
}
