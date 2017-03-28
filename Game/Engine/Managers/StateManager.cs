using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Managers {
    public class StateManager {
        public bool victoryConditionAchieved = false;
        public List<GameObjects.IGameObject> GameObjectList;
        Timer gameTimer;

        public void setTimer(Timer inTimer){
            gameTimer = inTimer;
        }

        public void setObjectList(List<GameObjects.IGameObject> inGameObjectList) {
            GameObjectList = inGameObjectList;
        }

        public void StartAll() {
            foreach (GameObjects.IGameObject GO in GameObjectList) {
                GO.Start();
            }
        }

        public void TickAll() {
            foreach(GameObjects.IGameObject GO in GameObjectList) {
                GO.Tick();
            }
        }

        public void ballVictory(Managers.InputManager inInputManager, StateManager stateManager) {
            gameTimer.Enabled = false;
            Program.startup(30, inInputManager, stateManager);
            StartAll();
            gameTimer.Enabled = true;
            //victoryConditionAchieved = true;
        }

        public void keyboardVictory(Managers.InputManager inInputManager, StateManager stateManager) {
            gameTimer.Enabled = false;
            //victoryConditionAchieved = true;
        }
    }
}
