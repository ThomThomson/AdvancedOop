using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Engine.Managers {
    public class StateManager {
        public bool victoryConditionAchieved = false;
        public List<GameObjects.IGameObject> GameObjectList;
        public int ballsNum;
        public int originalBalls;
        public int level = 1;
        Timer gameTimer;
        GameForm parentForm;

        public StateManager(GameForm parentform){
            this.parentForm = parentform;
        }

        public void setTimer(Timer inTimer){
            gameTimer = inTimer;
        }

        public void setObjectList(List<GameObjects.IGameObject> inGameObjectList, int numBalls) {
            GameObjectList = inGameObjectList;
            ballsNum = numBalls;
            originalBalls = numBalls;
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
            level = 1;
            parentForm.message = "You hecked up. Press enter to restart game";
            parentForm.Invalidate();
            ballsNum = 10;
        }

        public void RestartGame(Managers.InputManager inInputManager, StateManager stateManager){
            Program.startup(ballsNum, inInputManager, stateManager);
            parentForm.message = "";
            StartAll();
            gameTimer.Enabled = true;
        }

        public void keyboardVictory(Managers.InputManager inInputManager, StateManager stateManager) {
            gameTimer.Enabled = false;
            level++;
            parentForm.message = "VICTORY. Press enter to load level " + level;
            ballsNum += 5;
        }
    }
}
