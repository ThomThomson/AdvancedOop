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
            startup(10, inputManager, gf.State);
            Application.Run(gf);
        }

        public static void startup(int ballNum, Managers.InputManager inputManager, Managers.StateManager stateManager){
           
            List<GameObjects.IGameObject> StartingObjectList = new List<GameObjects.IGameObject>();

            //Create Gameobjects present at Game Start
            GameObjects.ObjectTypes.Landscape landscape = new GameObjects.ObjectTypes.Landscape(50, 100, 10, 5, 3, 4);
            StartingObjectList.Add(landscape);

            GameObjects.ObjectTypes.BallManager BallPlayer = new GameObjects.ObjectTypes.BallManager(inputManager, stateManager, landscape, ballNum);

            GameObjects.ObjectTypes.KeyPlayer player = new GameObjects.ObjectTypes.KeyPlayer(inputManager, stateManager, landscape, 4);
            BallPlayer.setPlayer(player);
            StartingObjectList.Add(player);
            StartingObjectList.Add(BallPlayer);
            //End startup objects

            stateManager.setObjectList(StartingObjectList, ballNum);
        }
    }
}
